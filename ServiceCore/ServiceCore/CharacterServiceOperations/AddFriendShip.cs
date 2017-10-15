using System;
using System.Collections.Generic;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.CharacterServiceOperations
{
	[Serializable]
	public sealed class AddFriendShip : Operation
	{
		public string friendCharacterName { get; set; }

		public int myUID { get; set; }

		public long myCharacterID { get; set; }

		public int QueryResult { get; set; }

		public override OperationProcessor RequestProcessor()
		{
			return new AddFriendShip.Request(this);
		}

		private class Request : OperationProcessor<AddFriendShip>
		{
			public Request(AddFriendShip op) : base(op)
			{
			}

			public override IEnumerable<object> Run()
			{
				yield return null;
				base.Operation.QueryResult = (int)base.Feedback;
				yield break;
			}
		}
	}
}
