using System;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class AvatarSynthesisItemMessage : IMessage
	{
		public long Material1ID { get; set; }

		public long Material2ID { get; set; }

		public long Material3ID { get; set; }

		public AvatarSynthesisItemMessage(long material1ID, long material2ID, long material3ID)
		{
			this.Material1ID = material1ID;
			this.Material2ID = material2ID;
			this.Material3ID = material3ID;
		}
	}
}
