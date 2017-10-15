using System;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.StoryServiceOperations
{
	[Serializable]
	public sealed class ModifyStoryVariable : Operation
	{
		public string StoryLine { get; set; }

		public string Key { get; set; }

		public int Value { get; set; }

		public ModifyStoryVariable(string StoryLine, string Key, int Value)
		{
			this.StoryLine = StoryLine;
			this.Key = Key;
			this.Value = Value;
		}

		public override OperationProcessor RequestProcessor()
		{
			return new OkOrFailProcessor(this);
		}
	}
}
