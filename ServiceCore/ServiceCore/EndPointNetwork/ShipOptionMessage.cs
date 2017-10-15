using System;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class ShipOptionMessage : IMessage
	{
		public ShipOptionInfo ShipOption { get; set; }

		public override string ToString()
		{
			return string.Format("PartyOptionMessage[ MaxMemberCount = {0} UntilForceStart = {1} ]", this.ShipOption.MaxMemberCount, this.ShipOption.UntilForceStart);
		}
	}
}
