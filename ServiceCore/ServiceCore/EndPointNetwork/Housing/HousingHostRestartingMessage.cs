using System;

namespace ServiceCore.EndPointNetwork.Housing
{
	[Serializable]
	public sealed class HousingHostRestartingMessage : IMessage
	{
		public override string ToString()
		{
			return "HousingHostRestartingMessage[]";
		}
	}
}
