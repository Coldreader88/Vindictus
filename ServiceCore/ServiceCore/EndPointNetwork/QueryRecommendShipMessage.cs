using System;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class QueryRecommendShipMessage : IMessage
	{
		public RecommendShipRestriction Restriction
		{
			get
			{
				return this.restriction;
			}
		}

		public QueryRecommendShipMessage()
		{
			this.restriction = new RecommendShipRestriction();
		}

		public QueryRecommendShipMessage(RecommendShipRestriction option)
		{
			this.restriction = option;
		}

		public override string ToString()
		{
			return string.Format("QueryRecommendShipMessage [ {0} ]", this.restriction);
		}

		private RecommendShipRestriction restriction;
	}
}
