using System;
using System.ServiceModel;
using System.ServiceModel.Channels;

namespace Nexon.Enterprise.ServiceFacade.Channels.ChunkingChannel
{
	public class TcpChunkingBinding : Binding, IBindingRuntimePreferences
	{
		public TcpChunkingBinding()
		{
			this.Initialize();
		}

		public TcpChunkingBinding(string name, string ns) : base(name, ns)
		{
			this.Initialize();
		}

		public override BindingElementCollection CreateBindingElements()
		{
			return new BindingElementCollection
			{
				this.be,
				this.tcpbe
			};
		}

		public override string Scheme
		{
			get
			{
				return this.tcpbe.Scheme;
			}
		}

		public int MaxBufferedChunks
		{
			get
			{
				return this.be.MaxBufferedChunks;
			}
			set
			{
				this.be.MaxBufferedChunks = value;
			}
		}

		private void Initialize()
		{
			this.be = new ChunkingBindingElement();
			this.tcpbe = new TcpTransportBindingElement();
			this.tcpbe.TransferMode = TransferMode.Buffered;
			this.tcpbe.MaxReceivedMessageSize = 106496L;
			base.SendTimeout = new TimeSpan(0, 5, 0);
			base.ReceiveTimeout = base.SendTimeout;
		}

		public bool ReceiveSynchronously
		{
			get
			{
				return true;
			}
		}

		private TcpTransportBindingElement tcpbe;

		private ChunkingBindingElement be;
	}
}
