using System;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class UpdateFlexibleMapLoadingInfoMessage : IMessage
	{
		public UpdateFlexibleMapLoadingInfoMessage(byte mapState, string regionName)
		{
			this.mapState = mapState;
			this.regionName = regionName;
		}

		private byte mapState;

		private string regionName;
	}
}
