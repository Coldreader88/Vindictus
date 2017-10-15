using System;
using System.Collections.Generic;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public class QueryCharacterNameByCIDListResultMessage : IMessage
	{
		public List<string> NameList { get; set; }

		public QueryCharacterNameByCIDListResultMessage(List<string> list)
		{
			this.NameList = list;
		}

		public override string ToString()
		{
			return string.Format("CIDByNameMessage[{0}]", this.NameList);
		}
	}
}
