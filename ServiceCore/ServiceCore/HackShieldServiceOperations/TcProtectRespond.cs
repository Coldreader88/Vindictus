using System;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.HackShieldServiceOperations
{
	[Serializable]
	public sealed class TcProtectRespond : Operation
	{
		public int Md5Check { get; set; }

		public int ImpressCheck { get; set; }

		public long CharacterID { get; set; }

		public TcProtectRespond(int md5Check, int impressCheck, long characterID)
		{
			this.Md5Check = md5Check;
			this.ImpressCheck = impressCheck;
			this.CharacterID = characterID;
		}

		public override OperationProcessor RequestProcessor()
		{
			return new NullProcessor(this);
		}
	}
}
