using System;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.StoryServiceOperations
{
	[Serializable]
	public sealed class SelectButton : Operation
	{
		public int ButtonIndex { get; set; }

		public SelectButton(int ButtonIndex)
		{
			this.ButtonIndex = ButtonIndex;
		}

		public override OperationProcessor RequestProcessor()
		{
			return new OkOrFailProcessor(this);
		}
	}
}
