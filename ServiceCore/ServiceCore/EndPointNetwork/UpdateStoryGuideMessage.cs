using System;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class UpdateStoryGuideMessage : IMessage
	{
		public string TargetLandMark { get; set; }

		public string GuideMessage { get; set; }

		public UpdateStoryGuideMessage(string TargetLandMark, string GuideMessage)
		{
			this.TargetLandMark = TargetLandMark;
			this.GuideMessage = GuideMessage;
		}

		public override string ToString()
		{
			return string.Format("UpdateStoryGuideMessage [ target = {0} message = {1} ]", this.TargetLandMark, this.GuideMessage);
		}
	}
}
