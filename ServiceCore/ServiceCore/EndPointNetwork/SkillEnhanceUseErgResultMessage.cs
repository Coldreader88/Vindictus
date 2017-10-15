using System;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class SkillEnhanceUseErgResultMessage : IMessage
	{
		public bool Result { get; set; }

		public int CurrentPercentage { get; set; }

		public int MaxPercentage { get; set; }

		public SkillEnhanceUseErgResultMessage(bool result, int currentRatio, int maxRatio)
		{
			this.Result = result;
			this.CurrentPercentage = currentRatio;
			this.MaxPercentage = maxRatio;
		}
	}
}
