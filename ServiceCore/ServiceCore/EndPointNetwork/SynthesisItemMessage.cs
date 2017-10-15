using System;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class SynthesisItemMessage : IMessage
	{
		public long BaseItemID { get; set; }

		public long LookItemID { get; set; }

		public string AdditionalItemClass { get; set; }

		public override string ToString()
		{
			return string.Format("SynthesisItemMessage[ BaseItemID = {0} LookItemID = {1} AdditionalItemClass = {2}]", this.BaseItemID, this.LookItemID, this.AdditionalItemClass);
		}
	}
}
