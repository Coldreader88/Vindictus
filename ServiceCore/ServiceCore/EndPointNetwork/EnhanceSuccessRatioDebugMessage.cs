using System;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class EnhanceSuccessRatioDebugMessage : IMessage
	{
		public float SuccessRatio { get; private set; }

		public float BonusRatio { get; private set; }

		public float Featurebonusratio { get; private set; }

		public EnhanceSuccessRatioDebugMessage(double successRatio, double bonusRatio, double featurebonusRatio)
		{
			this.SuccessRatio = (float)successRatio;
			this.BonusRatio = (float)bonusRatio;
			this.Featurebonusratio = (float)featurebonusRatio;
		}

		public override string ToString()
		{
			return string.Format("EnhanceSuccessRatioDebugMessage[ SuccessRatio = {0}, BonusRatio = {1}, Featurebonusratio = {2} ]", this.SuccessRatio, this.BonusRatio, this.Featurebonusratio);
		}
	}
}
