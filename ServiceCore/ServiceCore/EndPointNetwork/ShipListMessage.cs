using System;
using System.Collections.Generic;
using System.Text;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class ShipListMessage : IMessage
	{
		public bool Ignored { get; set; }

		public ICollection<ShipInfo> ShipList { get; set; }

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder("ShipListMessage [ (\n");
			stringBuilder.Append(")]");
			return stringBuilder.ToString();
		}
	}
}
