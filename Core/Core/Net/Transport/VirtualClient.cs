using System;
using System.Collections.Generic;
using System.Net;
using Devcat.Core.Net.Message;

namespace Devcat.Core.Net.Transport
{
	public class VirtualClient : IPacketTransmitter, ITransmitter<Packet>, ITransmitter<IEnumerable<Packet>>
	{
		public int ID
		{
			get
			{
				return this.id;
			}
		}

		public IPEndPoint RemoteEndPoint
		{
			get
			{
				return this.remoteEndPoint;
			}
		}

		public event EventHandler<EventArgs<ArraySegment<byte>>> PacketReceive;

		public event EventHandler<EventArgs> Disconnected;

		internal VirtualServer Server
		{
			get
			{
				return this.virtualServer;
			}
		}

		internal VirtualClient(VirtualServer virtualServer, int id, int ip, int port)
		{
			this.virtualServer = virtualServer;
			this.id = id;
			this.remoteEndPoint = new IPEndPoint((long)((ulong)ip), port);
		}

		public void Transmit(Packet packet)
		{
			packet.InstanceId = (long)VirtualPacketFlag.GenerateFlag(TransrouterMessage.SingleTransferMessage, this.id);
			this.virtualServer.Transmit(packet);
		}

		public void Transmit(IEnumerable<Packet> packets)
		{
			this.virtualServer.Transmit(EnumerablePacketFlagModifier.Convert(packets, VirtualPacketFlag.GenerateFlag(TransrouterMessage.SingleTransferMessage, this.id)));
		}

		public void Disconnect()
		{
			Packet packet = new Packet(0);
			packet.InstanceId = (long)VirtualPacketFlag.GenerateFlag(TransrouterMessage.DisconnectClient, this.id);
			this.virtualServer.Transmit(packet);
		}

		internal void ProcessReceive(EventArgs<ArraySegment<byte>> e)
		{
			EventHandler<EventArgs<ArraySegment<byte>>> packetReceive = this.PacketReceive;
			if (packetReceive != null)
			{
				packetReceive(this, e);
			}
		}

		internal void ProcessDisconnect()
		{
			EventHandler<EventArgs> disconnected = this.Disconnected;
			if (disconnected != null)
			{
				disconnected(this, EventArgs.Empty);
			}
		}

		private VirtualServer virtualServer;

		private int id;

		private IPEndPoint remoteEndPoint;
	}
}
