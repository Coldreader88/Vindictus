using System;
using System.Collections.Generic;
using System.ServiceModel.Channels;

namespace Nexon.Enterprise.ServiceFacade.Channels.ChunkingChannel
{
	public class ChunkingBindingElement : BindingElement
	{
		public ChunkingBindingElement()
		{
			this.maxBufferedChunks = 10;
		}

		public override bool CanBuildChannelFactory<TChannel>(BindingContext context)
		{
			return typeof(TChannel) == typeof(IDuplexSessionChannel) && context.CanBuildInnerChannelFactory<TChannel>();
		}

		public override bool CanBuildChannelListener<TChannel>(BindingContext context)
		{
			return typeof(TChannel) == typeof(IDuplexSessionChannel) && context.CanBuildInnerChannelListener<TChannel>();
		}

		public override IChannelFactory<TChannel> BuildChannelFactory<TChannel>(BindingContext context)
		{
			if (!this.CanBuildChannelFactory<TChannel>(context))
			{
				throw new InvalidOperationException("Unsupported channel type");
			}
			if (context == null)
			{
				throw new ArgumentNullException("context");
			}
			ICollection<string> operationParams = this.GetOperationParams(context);
			ChunkingChannelFactory chunkingChannelFactory = new ChunkingChannelFactory(context.BuildInnerChannelFactory<IDuplexSessionChannel>(), operationParams, this.maxBufferedChunks);
			return (IChannelFactory<TChannel>)chunkingChannelFactory;
		}

		public override IChannelListener<TChannel> BuildChannelListener<TChannel>(BindingContext context)
		{
			if (!this.CanBuildChannelListener<TChannel>(context))
			{
				throw new InvalidOperationException("Unsupported channel type");
			}
			if (context == null)
			{
				throw new ArgumentNullException("context");
			}
			ICollection<string> operationParams = this.GetOperationParams(context);
			ChunkingChannelListener chunkingChannelListener = new ChunkingChannelListener(context.BuildInnerChannelListener<IDuplexSessionChannel>(), operationParams, this.maxBufferedChunks);
			return (IChannelListener<TChannel>)chunkingChannelListener;
		}

		private ICollection<string> GetOperationParams(BindingContext context)
		{
			ChunkingBindingParameter chunkingBindingParameter = context.BindingParameters.Find<ChunkingBindingParameter>();
			if (chunkingBindingParameter != null)
			{
				return chunkingBindingParameter.OperationParams;
			}
			return new List<string>();
		}

		public override BindingElement Clone()
		{
			return new ChunkingBindingElement();
		}

		public int MaxBufferedChunks
		{
			get
			{
				return this.maxBufferedChunks;
			}
			set
			{
				this.maxBufferedChunks = value;
			}
		}

		public override T GetProperty<T>(BindingContext context)
		{
			if (context == null)
			{
				throw new ArgumentNullException("context");
			}
			return context.GetInnerProperty<T>();
		}

		private int maxBufferedChunks;
	}
}
