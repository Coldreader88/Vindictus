using System;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.CharacterServiceOperations
{
	[Serializable]
	public sealed class SelectTitle : Operation
	{
		public int TitleID
		{
			get
			{
				return this.titleID;
			}
		}

		public SelectTitle(int titleID)
		{
			this.titleID = titleID;
		}

		public override OperationProcessor RequestProcessor()
		{
			return new OkOrFailProcessor(this);
		}

		private int titleID;
	}
}
