using System;
using System.Collections.Generic;
using Devcat.Core.Net.Message;

namespace Devcat.Core.Net.Transport
{
	internal class InnerServer
	{
		public int ID
		{
			get
			{
				return this.id;
			}
		}

		public InnerServer(Transrouter bridge, TcpClient tcpClient, int id)
		{
			this.bridge = bridge;
			this.tcpClient = tcpClient;
			this.id = id;
			this.clientGroup = new SortedDictionary<int, SortedDictionary<int, int>>();
			this.removeList = new List<int>();
		}

		public void Activate()
		{
			this.tcpClient.PacketReceive += this.TcpClient_PacketReceive;
			this.tcpClient.Disconnected += this.TcpClient_Disconnected;
		}

		private void TcpClient_PacketReceive(object sender, EventArgs<ArraySegment<byte>> e)
		{
			Packet packet = new Packet(e.Value);
			switch (VirtualPacketFlag.GetMessage(packet.InstanceId))
			{
			case TransrouterMessage.SingleTransferMessage:
			{
				int targetID = VirtualPacketFlag.GetTargetID(packet.InstanceId);
				packet.InstanceId = (long)VirtualPacketFlag.GenerateClientFlag(this.id);
				this.bridge.SendToClient(targetID, packet);
				return;
			}
			case TransrouterMessage.GroupTransferMessage:
			{
				int secondaryTargetID = VirtualPacketFlag.GetSecondaryTargetID(packet);
				packet.InstanceId = (long)VirtualPacketFlag.GenerateClientFlag(this.id);
				foreach (int num in this.clientGroup[secondaryTargetID].Keys)
				{
					if (!this.bridge.SendToClient(num, packet))
					{
						this.removeList.Add(num);
					}
				}
				for (int i = 0; i < this.removeList.Count; i++)
				{
					this.clientGroup[secondaryTargetID].Remove(this.removeList[i]);
				}
				this.removeList.Clear();
				return;
			}
			case TransrouterMessage.CreateClientGroup:
			{
				int secondaryTargetID = VirtualPacketFlag.GetSecondaryTargetID(packet);
				this.clientGroup.Add(secondaryTargetID, new SortedDictionary<int, int>());
				return;
			}
			case TransrouterMessage.DestroyClientGroup:
			{
				int secondaryTargetID = VirtualPacketFlag.GetSecondaryTargetID(packet);
				this.clientGroup.Remove(secondaryTargetID);
				return;
			}
			case TransrouterMessage.AddClientGroupMember:
			{
				int targetID = VirtualPacketFlag.GetTargetID(packet.InstanceId);
				int secondaryTargetID = VirtualPacketFlag.GetSecondaryTargetID(packet);
				this.clientGroup[secondaryTargetID][targetID] = 0;
				return;
			}
			case TransrouterMessage.RemoveClientGroupMember:
			{
				int targetID = VirtualPacketFlag.GetTargetID(packet.InstanceId);
				int secondaryTargetID = VirtualPacketFlag.GetSecondaryTargetID(packet);
				this.clientGroup[secondaryTargetID].Remove(targetID);
				return;
			}
			case TransrouterMessage.ConnectClient:
			{
				int targetID = VirtualPacketFlag.GetTargetID(packet.InstanceId);
				this.bridge.Open(this.id, targetID);
				return;
			}
			case TransrouterMessage.DisconnectClient:
			{
				int targetID = VirtualPacketFlag.GetTargetID(packet.InstanceId);
				this.bridge.Close(this.id, targetID);
				return;
			}
			default:
				return;
			}
		}

		private void TcpClient_Disconnected(object sender, EventArgs e)
		{
			this.bridge.ProcessInnerServerDisconnect(this);
		}

		public void Transmit(Packet packet)
		{
			this.tcpClient.Transmit(packet);
		}

		private Transrouter bridge;

		private TcpClient tcpClient;

		private int id;

		private SortedDictionary<int, SortedDictionary<int, int>> clientGroup;

		private List<int> removeList;
	}
}
