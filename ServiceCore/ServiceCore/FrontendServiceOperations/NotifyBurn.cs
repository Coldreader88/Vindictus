using System;
using ServiceCore.EndPointNetwork;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.FrontendServiceOperations
{
	[Serializable]
	public class NotifyBurn : Operation
	{
		public HeroesString Message { get; set; }

		public bool IsTelepathyEnable { get; set; }

		public bool IsUIEnable { get; set; }

		public bool IsTownOnly { get; set; }

		public int AnnounceLevel { get; set; }

		public NotifyBurn()
		{
			this.IsTelepathyEnable = true;
			this.IsUIEnable = true;
			this.IsTownOnly = FeatureMatrix.IsEnable("RandomItemBroadcastMode");
			this.AnnounceLevel = FeatureMatrix.GetInteger("RandomItem_AnnounceLevel");
		}

		public override OperationProcessor RequestProcessor()
		{
			return new NullProcessor(this);
		}
	}
}
