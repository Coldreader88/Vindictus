using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Channels;

namespace Nexon.Enterprise.ServiceFacade.Channels.ChunkingChannel
{
	internal class ChunkingChannelFactory : ChannelFactoryBase<IDuplexSessionChannel>
	{
		internal ChunkingChannelFactory(IChannelFactory<IDuplexSessionChannel> innerChannelFactory, ICollection<string> operationParams, int maxBufferedChunks)
		{
			this.innerChannelFactory = innerChannelFactory;
			this.operationParams = operationParams;
			this.maxBufferedChunks = maxBufferedChunks;
		}

		protected override IAsyncResult OnBeginOpen(TimeSpan timeout, AsyncCallback callback, object state)
		{
			return this.innerChannelFactory.BeginOpen(timeout, callback, state);
		}

		protected override void OnEndOpen(IAsyncResult result)
		{
			this.innerChannelFactory.EndOpen(result);
		}

		protected override IAsyncResult OnBeginClose(TimeSpan timeout, AsyncCallback callback, object state)
		{
			return this.innerChannelFactory.BeginClose(timeout, callback, state);
		}

		protected override void OnEndClose(IAsyncResult result)
		{
			this.innerChannelFactory.EndClose(result);
		}

		protected override void OnOpen(TimeSpan timeout)
		{
			this.innerChannelFactory.Open(timeout);
		}

		protected override void OnAbort()
		{
			this.innerChannelFactory.Abort();
		}

		protected override void OnClose(TimeSpan timeout)
		{
			this.innerChannelFactory.Close(timeout);
		}

		protected override IDuplexSessionChannel OnCreateChannel(EndpointAddress address, Uri via)
		{
			IDuplexSessionChannel innerChannel = this.innerChannelFactory.CreateChannel(address, via);
			return new ChunkingDuplexSessionChannel(this, innerChannel, this.operationParams, this.maxBufferedChunks);
		}

		public override T GetProperty<T>()
		{
			return this.innerChannelFactory.GetProperty<T>();
		}

		private IChannelFactory<IDuplexSessionChannel> innerChannelFactory;

		private ICollection<string> operationParams;

		private int maxBufferedChunks;
	}
}
