using System;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class QuerySkillListMessage : IMessage
	{
		public override string ToString()
		{
			return string.Format("QuerySkillListMessage[]", new object[0]);
		}
	}
}
