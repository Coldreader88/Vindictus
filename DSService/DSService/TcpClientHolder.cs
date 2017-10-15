using System;
using Devcat.Core;
using Devcat.Core.Net;
using Devcat.Core.Net.Message;
using DSService.Message;
using Utility;

namespace DSService
{
	public class TcpClientHolder
	{
		public long TimeoutShedID { get; set; }

		public TcpClient TcpClient { get; set; }

		public bool HasTransfered { get; set; }

		public TcpClientHolder()
		{
			this.TimeoutShedID = -1L;
			this.TcpClient = null;
			this.HasTransfered = false;
		}

		public void BindTcpClient(TcpClient tcpClient)
		{
			this.TcpClient = tcpClient;
			tcpClient.PacketReceive += this.TCPHost_PacketReceive;
		}

		public void UnBindTcpClient()
		{
			if (this.TcpClient != null)
			{
				this.TcpClient.PacketReceive -= this.TCPHost_PacketReceive;
				this.TcpClient = null;
			}
		}

		public void DisconnectTcpClient()
		{
			if (this.TcpClient != null)
			{
				this.TcpClient.PacketReceive -= this.TCPHost_PacketReceive;
				this.TcpClient.Disconnect();
				this.TcpClient = null;
			}
		}

		public void ProcessMessage(object message)
		{
			if (message is DSHostConnectionEstablish)
			{
				DSHostConnectionEstablish dshostConnectionEstablish = message as DSHostConnectionEstablish;
				Log<TcpClientHolder>.Logger.InfoFormat("Process DSHostConnectionEstablish Message", new object[0]);
				DSEntity dsentity = null;
				foreach (DSEntity dsentity2 in DSService.Instance.DSEntities.Values)
				{
					if (dsentity2.Process != null && dsentity2.DSID == dshostConnectionEstablish.DSID)
					{
						dsentity = dsentity2;
						break;
					}
				}
				if (dsentity != null)
				{
					if (dsentity.PvpConfig == null)
					{
						dsentity.RegisterConnection(this.TcpClient);
					}
					else
					{
						dsentity.RegisterConnectionPvp(this.TcpClient);
					}
					this.HasTransfered = true;
				}
				else
				{
					string text = string.Format("[Peer {0}:{1}]", this.TcpClient.LocalEndPoint.Address.ToString(), this.TcpClient.LocalEndPoint.Port.ToString());
					Log<DSService>.Logger.ErrorFormat("{0} has no target DS Entity!", text);
					DSLog.AddLog(-1, null, -1L, -1, "No DSEntity", string.Format("{0} dsid{1}", text, dshostConnectionEstablish.DSID));
					this.DisconnectTcpClient();
				}
				this.UnBindTcpClient();
			}
		}

		public void TCPHost_PacketReceive(object sender, EventArgs<ArraySegment<byte>> e)
		{
			if (this.TcpClient != null)
			{
				Packet packet = new Packet(e.Value);
				DSService.Instance.MessageHandlerFactory.Handle(packet, this);
			}
		}
	}
}
