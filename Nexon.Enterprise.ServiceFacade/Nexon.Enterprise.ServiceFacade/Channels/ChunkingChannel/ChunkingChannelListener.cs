using System;
using System.Collections.Generic;
using System.ServiceModel.Channels;

namespace Nexon.Enterprise.ServiceFacade.Channels.ChunkingChannel
{
	internal class ChunkingChannelListener : ChannelListenerBase<IDuplexSessionChannel>
	{
		public ChunkingChannelListener(IChannelListener<IDuplexSessionChannel> innerListener, ICollection<string> operationParams, int maxBufferedChunks)
		{
			this.innerListener = innerListener;
			this.operationParams = operationParams;
			this.maxBufferedChunks = maxBufferedChunks;
		}

		public override Uri Uri
		{
			get
			{
				return this.innerListener.Uri;
			}
		}

		public override T GetProperty<T>()
		{
			return this.innerListener.GetProperty<T>();
		}

		protected override void OnOpen(TimeSpan timeout)
		{
			this.innerListener.Open(timeout);
		}

		protected override void OnAbort()
		{
			this.innerListener.Abort();
		}

		protected override void OnClose(TimeSpan timeout)
		{
			this.innerListener.Close(timeout);
		}

		protected override IAsyncResult OnBeginOpen(TimeSpan timeout, AsyncCallback callback, object state)
		{
			return this.innerListener.BeginOpen(timeout, callback, state);
		}

		protected override IAsyncResult OnBeginClose(TimeSpan timeout, AsyncCallback callback, object state)
		{
			return this.innerListener.BeginClose(timeout, callback, state);
		}

		protected override void OnEndClose(IAsyncResult result)
		{
			this.innerListener.EndClose(result);
		}

		protected override void OnEndOpen(IAsyncResult result)
		{
			this.innerListener.EndOpen(result);
		}

		protected override IDuplexSessionChannel OnAcceptChannel(TimeSpan timeout)
		{
			IDuplexSessionChannel innerChannel = this.innerListener.AcceptChannel();
			return this.WrapChannel(innerChannel);
		}

		protected override IAsyncResult OnBeginAcceptChannel(TimeSpan timeout, AsyncCallback callback, object state)
		{
			return this.innerListener.BeginAcceptChannel(timeout, callback, state);
		}

		protected override IDuplexSessionChannel OnEndAcceptChannel(IAsyncResult result)
		{
			IDuplexSessionChannel innerChannel = this.innerListener.EndAcceptChannel(result);
			return this.WrapChannel(innerChannel);
		}

		private IDuplexSessionChannel WrapChannel(IDuplexSessionChannel innerChannel)
		{
			if (innerChannel == null)
			{
				return null;
			}
			return new ChunkingDuplexSessionChannel(this, innerChannel, this.operationParams, this.maxBufferedChunks);
		}

		protected override IAsyncResult OnBeginWaitForChannel(TimeSpan timeout, AsyncCallback callback, object state)
		{
			return this.innerListener.BeginWaitForChannel(timeout, callback, state);
		}

		protected override bool OnEndWaitForChannel(IAsyncResult result)
		{
			return this.innerListener.EndWaitForChannel(result);
		}

		protected override bool OnWaitForChannel(TimeSpan timeout)
		{
			return this.innerListener.WaitForChannel(timeout);
		}

		private IChannelListener<IDuplexSessionChannel> innerListener;

		private ICollection<string> operationParams;

		private int maxBufferedChunks;
	}
}
