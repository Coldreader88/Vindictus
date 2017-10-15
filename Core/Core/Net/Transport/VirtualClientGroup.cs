using System;
using System.Collections.Generic;
using Devcat.Core.Net.Message;

namespace Devcat.Core.Net.Transport
{
	public class VirtualClientGroup : IPacketTransmitter, ITransmitter<Packet>, ITransmitter<IEnumerable<Packet>>
	{
		public int ID
		{
			get
			{
				return this.id;
			}
		}

		internal VirtualClientGroup(VirtualServer server, int id)
		{
			this.server = server;
			this.id = id;
		}

		public void Destroy()
		{
			this.server.DestroyVirtualClientGroup(this);
		}

		public void Transmit(Packet packet)
		{
			packet.InstanceId = (long)VirtualPacketFlag.GenerateFlag(TransrouterMessage.GroupTransferMessage, this.id);
			this.server.Transmit(packet);
		}

		public void Transmit(IEnumerable<Packet> packets)
		{
			this.server.Transmit(EnumerablePacketFlagModifier.Convert(packets, VirtualPacketFlag.GenerateFlag(TransrouterMessage.GroupTransferMessage, this.id)));
		}

		public void Add(VirtualClient virtualClient)
		{
			if (virtualClient.Server != this.server)
			{
				throw new ArgumentException("Different virtual server.");
			}
			Packet packet = new Packet(0)
			{
				InstanceId = (long)VirtualPacketFlag.GenerateFlag(TransrouterMessage.AddClientGroupMember, virtualClient.ID)
			};
			VirtualPacketFlag.SetSecondaryTargetID(packet, this.id);
			this.server.Transmit(packet);
		}

		public void Remove(VirtualClient virtualClient)
		{
			if (virtualClient.Server != this.server)
			{
				throw new ArgumentException("Different virtual server.");
			}
			Packet packet = new Packet(0)
			{
				InstanceId = (long)VirtualPacketFlag.GenerateFlag(TransrouterMessage.RemoveClientGroupMember, virtualClient.ID)
			};
			VirtualPacketFlag.SetSecondaryTargetID(packet, this.id);
			this.server.Transmit(packet);
		}

		private VirtualServer server;

		private int id;
	}
}
