using System;
using System.Collections.Generic;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class StoryVariablesMessage : IMessage
	{
		public ICollection<StoryVariableInfo> StoryVariables { get; set; }

		public StoryVariablesMessage(ICollection<StoryVariableInfo> storyVariables)
		{
			this.StoryVariables = storyVariables;
		}

		public override string ToString()
		{
			return string.Format("StoryVariablesMessage[ var x {0}]", this.StoryVariables.Count);
		}
	}
}
