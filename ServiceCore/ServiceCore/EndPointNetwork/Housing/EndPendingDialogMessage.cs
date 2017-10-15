using System;

namespace ServiceCore.EndPointNetwork.Housing
{
	[Serializable]
	public sealed class EndPendingDialogMessage : IMessage
	{
		public override string ToString()
		{
			return string.Format("EndPendingDialogMessage", new object[0]);
		}
	}
}
