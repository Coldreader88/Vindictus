using System;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class QueryMileagePointMessage : IMessage
	{
		public override string ToString()
		{
			return string.Format("QueryMileagePointMessage[ ]", new object[0]);
		}
	}
}
