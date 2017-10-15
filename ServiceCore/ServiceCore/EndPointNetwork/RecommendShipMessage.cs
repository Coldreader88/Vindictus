using System;
using System.Collections.Generic;
using System.Text;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class RecommendShipMessage : IMessage
	{
		public ICollection<ShipInfo> RecommendedShip { get; set; }

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			foreach (ShipInfo shipInfo in this.RecommendedShip)
			{
				stringBuilder.Append(stringBuilder.ToString());
			}
			return string.Format("RecommendShipMessage[ {0} ]", stringBuilder.ToString());
		}
	}
}
