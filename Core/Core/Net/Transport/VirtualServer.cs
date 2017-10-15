using System;
using System.Collections.Generic;
using System.Threading;
using Devcat.Core.Net.Message;

namespace Devcat.Core.Net.Transport
{
	public class VirtualServer
	{
		public int ID
		{
			get
			{
				return this.id;
			}
		}

		public event EventHandler<EventArgs<VirtualClient>> ClientAccept;

		public event EventHandler<EventArgs<Exception>> ExceptionOccur;

		public event EventHandler<EventArgs> ServerClose;

		public VirtualServer(TcpClient tcpClient)
		{
			this.tcpClient = tcpClient;
			this.id = Interlocked.Increment(ref VirtualServer.globalID);
			tcpClient.PacketReceive += this.TcpClient_PacketReceive;
			tcpClient.ExceptionOccur += this.TcpClient_ExceptionOccur;
			tcpClient.Disconnected += this.TcpClient_Disconnected;
			this.virtualClientList = new VirtualClient[65536];
		}

		public VirtualClient LinkVirtualClient(int id, int ip, int port)
		{
			Packet packet = new Packet(0);
			packet.InstanceId = (long)VirtualPacketFlag.GenerateFlag(TransrouterMessage.ConnectClient, id);
			this.tcpClient.Transmit(packet);
			return this.AddVirtualClient(id, ip, port);
		}

		public VirtualClientGroup CreateVirtualClientGroup()
		{
			int num = Interlocked.Increment(ref this.nextGroupID);
			Packet packet = new Packet(0)
			{
				InstanceId = (long)VirtualPacketFlag.GenerateFlag(TransrouterMessage.CreateClientGroup, 0)
			};
			VirtualPacketFlag.SetSecondaryTargetID(packet, num);
			this.tcpClient.Transmit(packet);
			return new VirtualClientGroup(this, num);
		}

		internal void DestroyVirtualClientGroup(VirtualClientGroup vcGroup)
		{
			Packet packet = new Packet(0)
			{
				InstanceId = (long)VirtualPacketFlag.GenerateFlag(TransrouterMessage.DestroyClientGroup, 0)
			};
			VirtualPacketFlag.SetSecondaryTargetID(packet, vcGroup.ID);
			this.tcpClient.Transmit(packet);
		}

		internal void Transmit(Packet packet)
		{
			this.tcpClient.Transmit(packet);
		}

		internal void Transmit(IEnumerable<Packet> packets)
		{
			this.tcpClient.Transmit(packets);
		}

		private void TcpClient_PacketReceive(object sender, EventArgs<ArraySegment<byte>> e)
		{
			Packet packet = new Packet(e.Value);
			switch (VirtualPacketFlag.GetMessage(packet.InstanceId))
			{
			case TransrouterMessage.ClientConnect:
				this.AddVirtualClient(VirtualPacketFlag.GetTargetID(packet.InstanceId), BitConverter.ToInt32(packet.Array, packet.Offset + packet.BodyOffset), BitConverter.ToInt32(packet.Array, packet.Offset + packet.BodyOffset + 4));
				return;
			case TransrouterMessage.ClientDisconnect:
				this.RemoveVirtualClient(VirtualPacketFlag.GetTargetID(packet.InstanceId));
				return;
			case TransrouterMessage.SingleTransferMessage:
				this.TransferMessage(VirtualPacketFlag.GetTargetID(packet.InstanceId), e);
				return;
			default:
				return;
			}
		}

		private VirtualClient AddVirtualClient(int id, int ip, int port)
		{
			VirtualClient virtualClient = this.virtualClientList[id & 65535];
			if (virtualClient != null)
			{
				if (virtualClient.ID == id)
				{
					return virtualClient;
				}
				this.RemoveVirtualClient(virtualClient.ID);
			}
			virtualClient = new VirtualClient(this, id, ip, port);
			this.virtualClientList[id & 65535] = virtualClient;
			EventHandler<EventArgs<VirtualClient>> clientAccept = this.ClientAccept;
			if (clientAccept != null)
			{
				clientAccept(this, new EventArgs<VirtualClient>(virtualClient));
			}
			return virtualClient;
		}

		private void RemoveVirtualClient(int id)
		{
			VirtualClient virtualClient = this.virtualClientList[id & 65535];
			if (virtualClient != null && virtualClient.ID == id)
			{
				virtualClient.ProcessDisconnect();
				this.virtualClientList[id & 65535] = null;
			}
		}

		private void TransferMessage(int id, EventArgs<ArraySegment<byte>> e)
		{
			VirtualClient virtualClient = this.virtualClientList[id & 65535];
			if (virtualClient != null && virtualClient.ID == id)
			{
				virtualClient.ProcessReceive(e);
			}
		}

		private void TcpClient_ExceptionOccur(object sender, EventArgs<Exception> e)
		{
			EventHandler<EventArgs<Exception>> exceptionOccur = this.ExceptionOccur;
			if (exceptionOccur != null)
			{
				exceptionOccur(this, e);
			}
		}

		private void TcpClient_Disconnected(object sender, EventArgs e)
		{
			foreach (VirtualClient virtualClient in this.virtualClientList)
			{
				if (virtualClient != null)
				{
					virtualClient.ProcessDisconnect();
				}
			}
			EventHandler<EventArgs> serverClose = this.ServerClose;
			if (serverClose != null)
			{
				serverClose(this, EventArgs.Empty);
			}
		}

		private const int MaxClient = 65536;

		private static int globalID;

		private int id;

		private int nextGroupID;

		private TcpClient tcpClient;

		private VirtualClient[] virtualClientList;
	}
}
