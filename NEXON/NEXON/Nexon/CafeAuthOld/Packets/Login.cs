using System;
using System.Collections.Generic;
using System.Net;

namespace Nexon.CafeAuthOld.Packets
{
	internal class Login
	{
		public string NexonID { get; private set; }

		public string CharacterID { get; private set; }

		public IPAddress LocalAddress { get; private set; }

		public IPAddress RemoteAddress { get; private set; }

		public bool CanTry { get; private set; }

		public bool IsTrial { get; private set; }

		public MachineID MachineID { get; private set; }

		public ICollection<int> GameRoomClients { get; private set; }

		public Login(string nexonID, string characterID, IPAddress localAddress, IPAddress remoteAddress, bool canTry, bool isTrial, MachineID machineID, ICollection<int> gameRoomClients)
		{
			this.NexonID = nexonID;
			this.CharacterID = characterID;
			this.LocalAddress = localAddress;
			this.RemoteAddress = remoteAddress;
			this.CanTry = canTry;
			this.IsTrial = isTrial;
			this.MachineID = machineID;
			this.GameRoomClients = gameRoomClients;
		}

		private int CalculateStructureSize()
		{
			return 4.CalculateStructureSize() + this.NexonID.CalculateStructureSize() + this.CharacterID.CalculateStructureSize() + this.LocalAddress.CalculateStructureSize() + this.RemoteAddress.CalculateStructureSize() + this.CanTry.CalculateStructureSize() + this.IsTrial.CalculateStructureSize() + this.MachineID.CalculateStructureSize() + 0.CalculateStructureSize() + ((this.GameRoomClients == null) ? 0 : this.GameRoomClients.Count) * 0.CalculateStructureSize();
		}

		internal Packet Serialize()
		{
			Packet result = new Packet(this.CalculateStructureSize(), PacketType.LoginLogout);
			result.Write(4);
			result.Write(this.NexonID);
			result.Write(this.CharacterID);
			result.Write(this.LocalAddress);
			result.Write(this.RemoteAddress);
			result.Write(this.CanTry);
			result.Write(this.IsTrial);
			result.Write(this.MachineID);
			result.Write((byte)((this.GameRoomClients == null) ? 0 : this.GameRoomClients.Count));
			if (this.GameRoomClients != null)
			{
				foreach (int value in this.GameRoomClients)
				{
					result.Write(value);
				}
			}
			return result;
		}
	}
}
