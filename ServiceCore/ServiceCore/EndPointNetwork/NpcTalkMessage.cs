using System;
using System.Collections.Generic;
using System.Text;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class NpcTalkMessage : IMessage
	{
		public ICollection<NpcTalkEntity> Content { get; private set; }

		public NpcTalkMessage(ICollection<NpcTalkEntity> content)
		{
			this.Content = content;
		}

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder("NpcTalkMessage [ content = (");
			stringBuilder.Append(")]");
			return stringBuilder.ToString();
		}
	}
}
