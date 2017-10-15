using System;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class ReloadNpcScriptMessage : IMessage
	{
		public override string ToString()
		{
			return string.Format("ReloadNpcScriptMessage[ ]", new object[0]);
		}
	}
}
