using System;
using System.Collections.Generic;
using System.Text;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class UpdateStoryStatusMessage : IMessage
	{
		public bool IsFullInformation { get; private set; }

		public IDictionary<string, int> StoryStatus { get; private set; }

		public UpdateStoryStatusMessage(IDictionary<string, int> storyStatus, bool isFullInformation)
		{
			this.StoryStatus = storyStatus;
			this.IsFullInformation = isFullInformation;
		}

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder("UpdateStoryStatusMessage [ storyStatus ");
			stringBuilder.AppendFormat("x {0}", this.StoryStatus.Count);
			stringBuilder.Append(") isFullInformation = ").Append(this.IsFullInformation).Append(" ]");
			return stringBuilder.ToString();
		}
	}
}
