using System;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.HackShieldServiceOperations
{
	[Serializable]
	public sealed class Respond : Operation
	{
		public byte[] Buffer { get; set; }

		public long CharacterID { get; set; }

		public bool IsCheat { get; set; }

		public Respond(byte[] buffer, long characterID, bool isCheat)
		{
			this.Buffer = buffer;
			this.CharacterID = characterID;
			this.IsCheat = isCheat;
		}

		public override OperationProcessor RequestProcessor()
		{
			return new NullProcessor(this);
		}
	}
}
