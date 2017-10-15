using System;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.StoryServiceOperations
{
	[Serializable]
	public sealed class SetStoryHintCategory : Operation
	{
		public int Category { get; set; }

		public SetStoryHintCategory(int category)
		{
			this.Category = category;
		}

		public override OperationProcessor RequestProcessor()
		{
			return new OkOrFailProcessor(this);
		}
	}
}
