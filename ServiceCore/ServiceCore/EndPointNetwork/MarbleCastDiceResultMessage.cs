using System;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class MarbleCastDiceResultMessage : IMessage
	{
		public int FaceNumber { get; set; }

		public int NextNodeIndex { get; set; }

		public bool IsChance { get; set; }

		public MarbleCastDiceResultMessage(int faceNumber, int nextNodeIndex, bool isChance)
		{
			this.FaceNumber = faceNumber;
			this.NextNodeIndex = nextNodeIndex;
			this.IsChance = isChance;
		}
	}
}
