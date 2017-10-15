using System;
using System.ServiceModel;
using log4net;
using WcfChatRelay.Whisper;

namespace WcfChatRelay.Server.Whisper
{
	public class RelayClient
	{
		public string URI { get; private set; }

		public string Name { get; private set; }

		public ILog Logger { get; set; }

		public bool GracefullyClosed { get; set; }

		public event EventHandler Disconnected;

		public event EventHandler<WhisperAsyncEventArg> WhisperedAsync;

		public event EventHandler<WhisperResultAsyncEventArg> WhisperResultAsync;

		public event EventHandler<WhisperEventArg> Whispered;

		public event EventHandler WebClosed;

		public RelayClient(string uri, string name)
		{
			this.URI = uri;
			this.Name = name;
		}

		public bool ConnectToService()
		{
			ServiceCallback serviceCallback = new ServiceCallback();
			serviceCallback.WebClosed += delegate(object s, EventArgs e)
			{
				if (this.WebClosed != null)
				{
					this.WebClosed(this, e);
				}
			};
			serviceCallback.WhisperedAsync += delegate(object s, WhisperAsyncEventArg e)
			{
				if (this.WhisperedAsync != null)
				{
					this.WhisperedAsync(this, e);
				}
			};
			serviceCallback.WhisperResultAsync += delegate(object s, WhisperResultAsyncEventArg e)
			{
				if (this.WhisperResultAsync != null)
				{
					this.WhisperResultAsync(this, e);
				}
			};
			serviceCallback.Whispered += delegate(object s, WhisperEventArg e)
			{
				if (this.Whispered != null)
				{
					this.Whispered(this, e);
				}
			};
			DuplexChannelFactory<ITalkService> duplexChannelFactory = new DuplexChannelFactory<ITalkService>(serviceCallback, new NetTcpBinding(SecurityMode.None, false), new EndpointAddress(this.URI));
			this.proxy = duplexChannelFactory.CreateChannel();
			IClientChannel clientChannel = this.proxy as IClientChannel;
			clientChannel.Closed += delegate(object s, EventArgs e)
			{
				if (this.Disconnected != null)
				{
					this.Disconnected(this, EventArgs.Empty);
				}
				this.proxy = null;
			};
			try
			{
				this.proxy.SubscribeService(this.Name);
			}
			catch (Exception ex)
			{
				if (this.Logger != null)
				{
					this.Logger.Error("Fail to connect chat relay server", ex);
				}
				return false;
			}
			return true;
		}

		private void ExceptionOccured(string message, Exception e)
		{
			if (this.Logger != null)
			{
				this.Logger.Error(message, e);
			}
			IClientChannel clientChannel = this.proxy as IClientChannel;
			if (clientChannel.State == CommunicationState.Faulted)
			{
				clientChannel.Abort();
			}
		}

		public void Whisper(string from, long fromCID, string to, string message)
		{
			if (this.proxy != null)
			{
				try
				{
					this.proxy.Whisper(from, fromCID, to, message);
				}
				catch (Exception e)
				{
					this.ExceptionOccured("Fail to SendChat", e);
				}
			}
		}

		public IAsyncResult BeginWhisper(string from, long fromCID, string to, string message, AsyncCallback callback, object asyncState)
		{
			if (this.proxy != null)
			{
				try
				{
					return this.proxy.BeginAsyncWhisper(from, fromCID, to, message, callback, asyncState);
				}
				catch (Exception ex)
				{
					if (this.Logger != null)
					{
						this.Logger.Fatal("Fail to BeginWhisper", ex);
					}
					IClientChannel clientChannel = this.proxy as IClientChannel;
					if (clientChannel.State == CommunicationState.Faulted)
					{
						clientChannel.Abort();
					}
					return null;
				}
			}
			throw new Exception("No Service is connected");
		}

		public bool EndWhisper(IAsyncResult result)
		{
			if (this.proxy != null)
			{
				try
				{
					return this.proxy.EndAsyncWhisper(result);
				}
				catch (Exception ex)
				{
					if (this.Logger != null)
					{
						this.Logger.Fatal("Fail to EndWhisper", ex);
					}
					IClientChannel clientChannel = this.proxy as IClientChannel;
					if (clientChannel != null)
					{
						CommunicationState state = clientChannel.State;
						if (clientChannel.State == CommunicationState.Faulted)
						{
							clientChannel.Abort();
						}
						return false;
					}
					return false;
				}
			}
			if (this.Logger != null)
			{
				this.Logger.Fatal("No Service is connected");
			}
			return false;
		}

		public void Ping()
		{
			if (this.proxy != null)
			{
				try
				{
					this.proxy.Ping();
				}
				catch (Exception e)
				{
					this.ExceptionOccured("Fail to Ping", e);
				}
			}
		}

		public void Close()
		{
			if (this.proxy != null)
			{
				this.GracefullyClosed = true;
				try
				{
					IClientChannel clientChannel = this.proxy as IClientChannel;
					clientChannel.Close();
				}
				catch (Exception ex)
				{
					if (this.Logger != null)
					{
						this.Logger.Error("Fail to close connection", ex);
					}
				}
				this.proxy = null;
			}
		}

		private ITalkService proxy;
	}
}
