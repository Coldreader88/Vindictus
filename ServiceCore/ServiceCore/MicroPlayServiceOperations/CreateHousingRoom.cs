using System;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.MicroPlayServiceOperations
{
	[Serializable]
	public sealed class CreateHousingRoom : Operation
	{
		public string Desc { get; set; }

		public int OpenLevel { get; set; }

		public long CID { get; set; }

		public CreateHousingRoom(string desc, int openLevel, long cID)
		{
			this.Desc = desc;
			this.OpenLevel = openLevel;
			this.CID = cID;
		}

		public override OperationProcessor RequestProcessor()
		{
			return new OkOrFailProcessor(this);
		}
	}
}
