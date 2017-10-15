using System;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class EnhanceItemMessage : IMessage
	{
		public long ItemID { get; set; }

		public string Material1 { get; set; }

		public string Material2 { get; set; }

		public string AdditionalMaterial { get; set; }

		public bool IsEventEnhanceAShot { get; set; }

		public override string ToString()
		{
			return string.Format("EnhanceItemMessage[ ItemID = {0} ]", this.ItemID);
		}
	}
}
