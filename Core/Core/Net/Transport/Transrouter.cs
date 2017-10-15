using System;
using System.Collections.Generic;
using Devcat.Core.Net.Message;

namespace Devcat.Core.Net.Transport
{
	public class Transrouter
	{
		public int ClientCount
		{
			get
			{
				return this.clientCount;
			}
		}

		public Transrouter()
		{
			this.clientList = new OuterClient[16384];
			for (int i = 0; i < 16384; i++)
			{
				this.clientList[i] = new OuterClient(this, i);
				this.clientList[i].NextIndex = i + 1;
			}
			this.clientList[this.clientList.Length - 1].NextIndex = -1;
			this.clientHeadIndex = 0;
			this.clientTailIndex = this.clientList.Length - 1;
			this.serverList = new InnerServer[256];
			this.autoConnectServerList = new List<int>();
		}

		public void AddInnerServer(TcpClient tcpClient, bool autoConnect)
		{
			if (!tcpClient.Connected)
			{
				throw new ArgumentException("InnerServer not connected.");
			}
			InnerServer innerServer = null;
			for (int i = 0; i < this.serverList.Length; i++)
			{
				if (this.serverList[i] == null)
				{
					this.serverList[i] = new InnerServer(this, tcpClient, i);
					innerServer = this.serverList[i];
					break;
				}
			}
			if (innerServer == null)
			{
				throw new IndexOutOfRangeException("Server count limit exceed. Can't add more server.");
			}
			innerServer.Activate();
			if (autoConnect)
			{
				this.autoConnectServerList.Add(innerServer.ID);
			}
		}

		public void AddOuterClient(TcpClient tcpClient)
		{
			if (!tcpClient.Connected)
			{
				throw new ArgumentException("OuterClient not connected.");
			}
			if (this.clientHeadIndex == -1)
			{
				throw new IndexOutOfRangeException("Client count limit exceed. Can't add more client.");
			}
			OuterClient outerClient = this.clientList[this.clientHeadIndex];
			outerClient.Activate(tcpClient);
			this.clientCount++;
			for (int i = 0; i < this.autoConnectServerList.Count; i++)
			{
				outerClient.OpenServer(this.autoConnectServerList[i]);
			}
			this.clientHeadIndex = outerClient.NextIndex;
			if (this.clientHeadIndex == -1)
			{
				this.clientTailIndex = -1;
			}
		}

		internal void ProcessInnerServerDisconnect(InnerServer server)
		{
			for (int i = 0; i < 16384; i++)
			{
				this.clientList[i].CloseServer(server.ID);
			}
		}

		internal void ProcessOuterClientDisconnect(OuterClient client)
		{
			if (this.clientList[client.Index].ID == client.ID)
			{
				this.clientList[client.Index] = new OuterClient(this, this.GetNextClientID(client.ID));
				this.clientCount--;
				if (this.clientTailIndex != -1)
				{
					this.clientList[this.clientTailIndex].NextIndex = client.Index;
				}
				else
				{
					this.clientHeadIndex = client.Index;
				}
				this.clientTailIndex = client.Index;
			}
		}

		internal void SendToServer(int id, Packet packet)
		{
			InnerServer innerServer = this.serverList[id];
			innerServer.Transmit(packet);
		}

		internal bool SendToClient(int clientID, Packet packet)
		{
			OuterClient outerClient = this.clientList[this.GetClientIndex(clientID)];
			if (outerClient.ID == clientID)
			{
				outerClient.Transmit(packet);
				return true;
			}
			return false;
		}

		internal void Open(int serverID, int clientID)
		{
			OuterClient outerClient = this.clientList[this.GetClientIndex(clientID)];
			if (outerClient.ID == clientID)
			{
				outerClient.OpenServer(serverID);
			}
		}

		internal void Close(int serverID, int clientID)
		{
			OuterClient outerClient = this.clientList[this.GetClientIndex(clientID)];
			if (outerClient.ID == clientID)
			{
				outerClient.CloseServer(serverID);
			}
		}

		private int GetNextClientID(int id)
		{
			if ((id & 16711680) == 16711680)
			{
				return id & -16711681;
			}
			return id + 65536;
		}

		private int GetClientIndex(int id)
		{
			return id & 65535;
		}

		private const int MaxClient = 16384;

		private InnerServer[] serverList;

		private List<int> autoConnectServerList;

		private int clientHeadIndex;

		private int clientTailIndex;

		private OuterClient[] clientList;

		private int clientCount;
	}
}
