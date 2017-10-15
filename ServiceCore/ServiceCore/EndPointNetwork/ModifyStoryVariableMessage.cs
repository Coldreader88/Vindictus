using System;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class ModifyStoryVariableMessage : IMessage
	{
		public string StoryLine { get; set; }

		public string Key { get; set; }

		public int Value { get; set; }

		public ModifyStoryVariableMessage(string StoryLine, string Key, int Value)
		{
			this.StoryLine = StoryLine;
			this.Key = Key;
			this.Value = Value;
		}

		public override string ToString()
		{
			return string.Format("ModifyStoryVariableMessage [ {0}/{1} = {2} ]", this.StoryLine, this.Key, this.Value);
		}
	}
}
