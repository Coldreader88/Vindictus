using System;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class UserLoginMessage : IMessage
	{
		public string Passport { get; private set; }

		public uint LocalAddress { get; private set; }

		public string hwID { get; private set; }

		public byte[] MachineID { get; private set; }

		public int GameRoomClient { get; private set; }

		public bool IsCharacterSelectSkipped { get; private set; }

		public string NexonID { get; private set; }

		public string UpToDateInfo { get; private set; }

		public long CheckSum { get; private set; }

		public UserLoginMessage(string passport)
		{
			this.Passport = passport;
			this.MachineID = new byte[16];
			this.NexonID = "";
		}

		public override string ToString()
		{
			return string.Format("UserLoginMessage[ ]", new object[0]);
		}
	}
}
