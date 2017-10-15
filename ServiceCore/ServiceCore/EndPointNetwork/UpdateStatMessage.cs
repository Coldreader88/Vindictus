using System;
using System.Collections.Generic;
using System.Text;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class UpdateStatMessage : IMessage
	{
		public IDictionary<string, int> Stat { get; private set; }

		public UpdateStatMessage(IDictionary<string, int> stat)
		{
			this.Stat = stat;
		}

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder("UpdateStatMessage [ ");
			stringBuilder.Append(" ]");
			return stringBuilder.ToString();
		}
	}
}
