using System;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class ExecuteNpcServerCommandMessage : IMessage
	{
		public override string ToString()
		{
			return string.Format("ExcuteNpcServerCommandMessage[ ]", new object[0]);
		}
	}
}
