using System;
using System.Collections.Generic;
using System.Net;

namespace Nexon.CafeAuth.Packets
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
			return this.LoginType.CalculateStructureSize() + this.NexonID.CalculateStructureSize() + this.RemoteAddress.CalculateStructureSize() + this.PropertyCount.CalculateStructureSize() + this.ExtendCode_CID.CalculateStructureSize() + this.CharacterID.CalculateStructureSize() + this.ExtendCode_LocalAddress.CalculateStructureSize() + this.LocalAddress.CalculateStructureSize() + this.ExtendCode_IsCanTry.CalculateStructureSize() + this.CanTry.CalculateStructureSize() + this.ExtendCode_IsTrial.CalculateStructureSize() + this.IsTrial.CalculateStructureSize() + this.ExtendCode_MID.CalculateStructureSize() + this.MachineID.CalculateStructureSize() + ((this.GameRoomClients == null) ? 0 : this.ExtendCode_GRCs.CalculateStructureSize()) + ((this.GameRoomClients == null) ? 0 : 0.CalculateStructureSize()) + ((this.GameRoomClients == null) ? 0 : this.GameRoomClients.Count) * 0.CalculateStructureSize() + this.ExtendCode_IsReturnPCBangNo.CalculateStructureSize() + this.IsReturnPCBangNo.CalculateStructureSize() + this.ExtendCode_Policies.CalculateStructureSize() + this.policyCount.CalculateStructureSize() + this.policyShutDown.CalculateStructureSize();
		}

		internal Packet Serialize()
		{
			Packet result = new Packet(this.CalculateStructureSize(), PacketType.Login);
			result.Write(this.LoginType);
			result.Write(this.NexonID);
			result.Write(this.RemoteAddress);
			result.Write(this.PropertyCount);
			result.Write(this.ExtendCode_CID);
			result.Write(this.CharacterID);
			result.Write(this.ExtendCode_LocalAddress);
			result.Write(this.LocalAddress);
			result.Write(this.ExtendCode_IsCanTry);
			result.Write(this.CanTry);
			result.Write(this.ExtendCode_IsTrial);
			result.Write(this.IsTrial);
			result.Write(this.ExtendCode_MID);
			result.Write(this.MachineID);
			if (this.GameRoomClients != null)
			{
				result.Write(this.ExtendCode_GRCs);
				result.Write((byte)this.GameRoomClients.Count);
				foreach (int value in this.GameRoomClients)
				{
					result.Write(value);
				}
			}
			result.Write(this.ExtendCode_IsReturnPCBangNo);
			result.Write(this.IsReturnPCBangNo);
			result.Write(this.ExtendCode_Policies);
			result.Write(this.policyCount);
			result.Write(this.policyShutDown);
			return result;
		}

		public readonly byte LoginType = 1;

		public readonly byte PropertyCount = 7;

		public readonly short ExtendCode_CID = 1;

		public readonly short ExtendCode_LocalAddress = 2;

		public readonly short ExtendCode_IsCanTry = 3;

		public readonly short ExtendCode_IsTrial = 4;

		public readonly short ExtendCode_MID = 5;

		public readonly short ExtendCode_GRCs = 6;

		public readonly short ExtendCode_IsReturnPCBangNo = 13;

		public readonly byte IsReturnPCBangNo = 1;

		public readonly short ExtendCode_Policies = 17;

		public readonly byte policyCount = 1;

		public readonly byte policyShutDown = 10;
	}
}
