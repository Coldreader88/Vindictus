using System;
using System.Collections.Generic;
using System.Text;

namespace ServiceCore.EndPointNetwork.Housing
{
	[Serializable]
	public sealed class HousingListMessage : IMessage
	{
		public ICollection<long> HousingList { get; set; }

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder("HousingListMessage [ (\n");
			stringBuilder.Append(")]");
			return stringBuilder.ToString();
		}
	}
}
