using System;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class SetStoryHintCategoryMessage : IMessage
	{
		public int Category { get; set; }

		public SetStoryHintCategoryMessage(int category)
		{
			this.Category = category;
		}

		public override string ToString()
		{
			return string.Format("SetStoryHintCategoryMessage[ Category = {0} ]", this.Category);
		}
	}
}
