using System;
using System.Collections.Generic;
using Devcat.Core.Net.Message;

namespace Devcat.Core.Net.Transport
{
	internal class OuterClient
	{
		public int ID
		{
			get
			{
				return this.id;
			}
		}

		public int Index
		{
			get
			{
				return this.id & 65535;
			}
		}

		public int IndexSequence
		{
			get
			{
				return (int)((uint)this.id >> 16 & 255u);
			}
		}

		public int NextIndex
		{
			get
			{
				return this.nextIndex;
			}
			set
			{
				this.nextIndex = value;
			}
		}

		public OuterClient(Transrouter bridge, int id)
		{
			this.bridge = bridge;
			this.id = id;
			this.nextIndex = -1;
		}

		public void Activate(TcpClient client)
		{
			this.client = client;
			this.connectedServer = new bool[256];
			this.connectedServerIndexList = new List<int>();
			client.PacketReceive += this.Client_PacketReceive;
			client.Disconnected += this.Client_Disconnected;
		}

		public void Transmit(Packet data)
		{
			this.client.Transmit(data);
		}

		public void OpenServer(int serverID)
		{
			this.connectedServer[serverID] = true;
			this.connectedServerIndexList.Add(serverID);
			Packet packet = new Packet(8);
			packet.InstanceId = (long)VirtualPacketFlag.GenerateFlag(TransrouterMessage.ClientConnect, this.id);
			Buffer.BlockCopy(this.client.RemoteEndPoint.Address.GetAddressBytes(), 0, packet.Array, packet.Offset + packet.BodyOffset, 4);
			Buffer.BlockCopy(BitConverter.GetBytes(this.client.RemoteEndPoint.Port), 0, packet.Array, packet.Offset + packet.BodyOffset + 4, 4);
			this.bridge.SendToServer(serverID, packet);
			this.Transmit(new Packet
			{
				InstanceId = (long)(256 | VirtualPacketFlag.GenerateClientFlag(serverID))
			});
		}

		public void CloseServer(int serverID)
		{
			if (this.connectedServer == null)
			{
				return;
			}
			this.connectedServer[serverID] = false;
			int index = this.connectedServerIndexList.IndexOf(serverID);
			int index2 = this.connectedServerIndexList.Count - 1;
			this.connectedServerIndexList[index] = this.connectedServerIndexList[index2];
			this.connectedServerIndexList.RemoveAt(index2);
			Packet packet = new Packet(0);
			packet.InstanceId = (long)VirtualPacketFlag.GenerateFlag(TransrouterMessage.ClientDisconnect, this.id);
			this.bridge.SendToServer(serverID, packet);
			this.Transmit(new Packet(0)
			{
				InstanceId = (long)(512 | VirtualPacketFlag.GenerateClientFlag(serverID))
			});
		}

		private void Disable()
		{
			this.connectedServer = null;
			Packet packet = new Packet(0);
			packet.InstanceId = (long)VirtualPacketFlag.GenerateFlag(TransrouterMessage.ClientDisconnect, this.id);
			for (int i = 0; i < this.connectedServerIndexList.Count; i++)
			{
				this.bridge.SendToServer(this.connectedServerIndexList[i], packet);
			}
			this.connectedServerIndexList.Clear();
		}

		private void Client_PacketReceive(object sender, EventArgs<ArraySegment<byte>> e)
		{
			Packet packet = new Packet(e.Value);
			if ((packet.InstanceId & -65536L) != -1068630016L)
			{
				throw new ArgumentException("Invalid client packet header.");
			}
			int num = (int)((byte)packet.InstanceId);
			long instanceId = packet.InstanceId;
			if (this.connectedServer[num])
			{
				packet.InstanceId = (long)VirtualPacketFlag.GenerateFlag(TransrouterMessage.SingleTransferMessage, this.id);
				this.bridge.SendToServer(num, packet);
			}
		}

		private void Client_Disconnected(object sender, EventArgs e)
		{
			this.bridge.ProcessOuterClientDisconnect(this);
			this.Disable();
		}

		private Transrouter bridge;

		private int id;

		private bool[] connectedServer;

		private List<int> connectedServerIndexList;

		private int nextIndex;

		private TcpClient client;
	}
}
