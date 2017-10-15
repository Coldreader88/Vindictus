using System;
using System.Collections.Generic;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public class QueryCharacterNameByCIDListMessage : IMessage
	{
		public List<long> CIDList { get; set; }

		public override string ToString()
		{
			return string.Format("CIDByNameMessage[{0}]", this.CIDList);
		}
	}
}
