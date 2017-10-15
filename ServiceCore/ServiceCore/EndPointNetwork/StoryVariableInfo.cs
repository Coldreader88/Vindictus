using System;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class StoryVariableInfo
	{
		public string StoryLine { get; set; }

		public string Key { get; set; }

		public int Value { get; set; }

		public StoryVariableInfo(string StoryLine, string Key, int Value)
		{
			this.StoryLine = StoryLine;
			this.Key = Key;
			this.Value = Value;
		}
	}
}
