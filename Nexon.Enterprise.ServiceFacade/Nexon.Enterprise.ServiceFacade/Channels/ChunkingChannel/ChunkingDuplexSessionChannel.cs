using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Threading;

namespace Nexon.Enterprise.ServiceFacade.Channels.ChunkingChannel
{
	public class ChunkingDuplexSessionChannel : ChannelBase, IDuplexSessionChannel, IDuplexChannel, IInputChannel, IOutputChannel, IChannel, ICommunicationObject, ISessionChannel<IDuplexSession>
	{
		internal ChunkingDuplexSessionChannel(ChannelManagerBase channelManager, IDuplexSessionChannel innerChannel, ICollection<string> operationParams, int maxBufferedChunks) : base(channelManager)
		{
			this.Initialize(channelManager, innerChannel, operationParams, maxBufferedChunks);
		}

		private void Initialize(ChannelManagerBase channelManager, IDuplexSessionChannel innerChannel, ICollection<string> operationParams, int maxBufferedChunks)
		{
			this.innerChannel = innerChannel;
			this.operationParams = operationParams;
			this.maxBufferedChunks = maxBufferedChunks;
		}

		protected override void OnOpen(TimeSpan timeout)
		{
			this.innerChannel.Open(timeout);
		}

		protected override void OnClose(TimeSpan timeout)
		{
			this.stopReceive = true;
			TimeoutHelper timeoutHelper = new TimeoutHelper(timeout);
			if (this.receiveStopped.WaitOne(TimeoutHelper.ToMilliseconds(timeoutHelper.RemainingTime()), false))
			{
				this.innerChannel.Close(timeoutHelper.RemainingTime());
				return;
			}
			throw new TimeoutException("Close timeout exceeded");
		}

		protected override void OnAbort()
		{
			this.innerChannel.Abort();
		}

		protected override IAsyncResult OnBeginOpen(TimeSpan timeout, AsyncCallback callback, object state)
		{
			return this.innerChannel.BeginOpen(timeout, callback, state);
		}

		protected override void OnEndOpen(IAsyncResult result)
		{
			this.innerChannel.EndOpen(result);
		}

		protected override IAsyncResult OnBeginClose(TimeSpan timeout, AsyncCallback callback, object state)
		{
			return this.innerChannel.BeginClose(timeout, callback, state);
		}

		protected override void OnEndClose(IAsyncResult result)
		{
			this.innerChannel.EndClose(result);
		}

		public override T GetProperty<T>()
		{
			return this.innerChannel.GetProperty<T>();
		}

        private void ReceiveChunkLoop(object state)
        {
            TimeoutHelper timeoutHelper = (TimeoutHelper)state;
            Message message = null;
            do
            {
                message = this.innerChannel.Receive(timeoutHelper.RemainingTime());
                if (message == null)
                {
                    throw new CommunicationException("A null chunk message was received. Null message indicates the end of a session so a null chunk should never be received");
                }
                this.ProcessReceivedChunk(message);
                if (!this.IsLastChunk(message))
                {
                    continue;
                }
                this.currentMessageCompleted.Set();
                this.currentInputMessage = null;
                this.stopReceive = true;
            }
            while (!this.stopReceive && message != null);
            this.receiveStopped.Set();
        }

        private ChunkingMessage GetNewChunkingMessage(Message receivedMessage, TimeoutHelper timeoutHelper)
		{
			if (receivedMessage == null)
			{
				return null;
			}
			Guid messageId = this.GetMessageId(receivedMessage);
			if (this.currentInputMessage != null && messageId == this.currentInputMessage.MessageId)
			{
				throw new InvalidOperationException("A new ChunkingMessage was requested but the received message's id matches the current message id. The received message is a chunk of the current message");
			}
			ChunkingReader reader = new ChunkingReader(receivedMessage, this.maxBufferedChunks, timeoutHelper);
			string header = receivedMessage.Headers.GetHeader<string>("OriginalAction", "http://bam.nexon.com/chunking");
			ChunkingMessage chunkingMessage = new ChunkingMessage(receivedMessage.Version, header, reader, messageId);
			string addressingNamespace = ChunkingUtils.GetAddressingNamespace(receivedMessage.Version);
			for (int i = 0; i < receivedMessage.Headers.Count; i++)
			{
				MessageHeaderInfo messageHeaderInfo = receivedMessage.Headers[i];
				if (messageHeaderInfo.Namespace != "http://bam.nexon.com/chunking" && (!(messageHeaderInfo.Name == "Action") || !(messageHeaderInfo.Namespace == addressingNamespace)))
				{
					chunkingMessage.Headers.CopyHeaderFrom(receivedMessage.Headers, i);
				}
			}
			return chunkingMessage;
		}

		private bool IsNewMessage(Message receivedMessage)
		{
			if (receivedMessage == null)
			{
				throw new ArgumentException("IsNewMessage should not be passed a null message", "receivedMessage");
			}
			Guid messageId = this.GetMessageId(receivedMessage);
			return this.currentInputMessage == null || this.currentInputMessage.MessageId != messageId;
		}

		private bool IsLastChunk(Message receivedMessage)
		{
			if (receivedMessage == null)
			{
				throw new ArgumentException("IsLastChunk should not be passed a null message", "receivedMessage");
			}
			return receivedMessage.Headers.FindHeader("ChunkingEnd", "http://bam.nexon.com/chunking") > -1;
		}

		private bool IsChunkInCurrentMessage(Message receivedMessage)
		{
			if (receivedMessage == null)
			{
				throw new ArgumentException("IsChunkInCurrentMessage should not be passed a null message", "receivedMessage");
			}
			return !this.IsNewMessage(receivedMessage);
		}

		private Guid GetMessageId(Message receivedMessage)
		{
			if (receivedMessage == null)
			{
				throw new ArgumentException("GetMessageId should not be passed a null message", "receivedMessage");
			}
			return receivedMessage.Headers.GetHeader<Guid>("MessageId", "http://bam.nexon.com/chunking");
		}

		private void ProcessReceivedChunk(Message receivedMessage)
		{
			if (receivedMessage == null)
			{
				throw new ArgumentException("ProcessReceivedChunk should not be passed a null message", "receivedMessage");
			}
			if (this.IsChunkInCurrentMessage(receivedMessage))
			{
				this.currentInputMessage.UnderlyingReader.AddMessage(receivedMessage);
				return;
			}
			throw new InvalidOperationException("ProcessReceivedChunk was passed a chunk that doesn't belong to the current message");
		}

		public Message Receive(TimeSpan timeout)
		{
			base.ThrowIfDisposedOrNotOpen();
			TimeoutHelper timeoutHelper = new TimeoutHelper(timeout);
			if (!this.currentMessageCompleted.WaitOne(TimeoutHelper.ToMilliseconds(timeout), false))
			{
				throw new TimeoutException("Receive timed out waiting for previous message receive to complete");
			}
			Message message = this.innerChannel.Receive(timeoutHelper.RemainingTime());
			if (message != null && message.Headers.Action == "http://bam.nexon.com/chunkingAction")
			{
				this.currentInputMessage = this.GetNewChunkingMessage(message, timeoutHelper);
				this.currentMessageCompleted.Reset();
				this.stopReceive = false;
				ThreadPool.QueueUserWorkItem(new WaitCallback(this.ReceiveChunkLoop), timeoutHelper);
				return this.currentInputMessage;
			}
			return message;
		}

		public Message Receive()
		{
			return this.Receive(base.DefaultReceiveTimeout);
		}

		public bool TryReceive(TimeSpan timeout, out Message message)
		{
			base.ThrowIfDisposedOrNotOpen();
			message = null;
			bool flag = false;
			try
			{
				message = this.Receive(timeout);
			}
			catch (TimeoutException)
			{
				flag = true;
			}
			return !flag;
		}

		public bool WaitForMessage(TimeSpan timeout)
		{
			return this.innerChannel.WaitForMessage(timeout);
		}

		public void Send(Message message)
		{
			this.Send(message, base.DefaultSendTimeout);
		}

		public void Send(Message message, TimeSpan timeout)
		{
			base.ThrowIfDisposedOrNotOpen();
			if (!this.sendingDone.WaitOne(TimeoutHelper.ToMilliseconds(timeout), false))
			{
				throw new TimeoutException("Send timed out waiting for the previous message send to complete");
			}
			lock (base.ThisLock)
			{
				base.ThrowIfDisposedOrNotOpen();
				if (this.operationParams.Contains(message.Headers.Action))
				{
					TimeoutHelper chunkingTimeout = new TimeoutHelper(timeout);
					this.sendingDone.Reset();
					ChunkingWriter writer = new ChunkingWriter(message, chunkingTimeout, this.innerChannel);
					message.WriteBodyContents(writer);
					this.sendingDone.Set();
				}
				else
				{
					this.innerChannel.Send(message, timeout);
				}
			}
		}

		public IAsyncResult BeginSend(Message message, TimeSpan timeout, AsyncCallback callback, object state)
		{
			throw new Exception("The method or operation is not implemented.");
		}

		public IAsyncResult BeginSend(Message message, AsyncCallback callback, object state)
		{
			throw new Exception("The method or operation is not implemented.");
		}

		public void EndSend(IAsyncResult result)
		{
			throw new Exception("The method or operation is not implemented.");
		}

		public EndpointAddress RemoteAddress
		{
			get
			{
				return this.innerChannel.RemoteAddress;
			}
		}

		public Uri Via
		{
			get
			{
				return this.innerChannel.Via;
			}
		}

		public IDuplexSession Session
		{
			get
			{
				return this.innerChannel.Session;
			}
		}

		public IAsyncResult BeginReceive(TimeSpan timeout, AsyncCallback callback, object state)
		{
			throw new Exception("The method or operation is not implemented.");
		}

		public IAsyncResult BeginReceive(AsyncCallback callback, object state)
		{
			throw new Exception("The method or operation is not implemented.");
		}

		public IAsyncResult BeginTryReceive(TimeSpan timeout, AsyncCallback callback, object state)
		{
			throw new Exception("The method or operation is not implemented.");
		}

		public IAsyncResult BeginWaitForMessage(TimeSpan timeout, AsyncCallback callback, object state)
		{
			throw new Exception("The method or operation is not implemented.");
		}

		public Message EndReceive(IAsyncResult result)
		{
			throw new Exception("The method or operation is not implemented.");
		}

		public bool EndTryReceive(IAsyncResult result, out Message message)
		{
			throw new Exception("The method or operation is not implemented.");
		}

		public bool EndWaitForMessage(IAsyncResult result)
		{
			throw new Exception("The method or operation is not implemented.");
		}

		public EndpointAddress LocalAddress
		{
			get
			{
				return this.innerChannel.LocalAddress;
			}
		}

		private IDuplexSessionChannel innerChannel;

		private ICollection<string> operationParams;

		private int maxBufferedChunks;

		private bool stopReceive;

		private ChunkingMessage currentInputMessage;

		private ManualResetEvent receiveStopped = new ManualResetEvent(true);

		private ManualResetEvent currentMessageCompleted = new ManualResetEvent(true);

		private ManualResetEvent sendingDone = new ManualResetEvent(true);
	}
}
