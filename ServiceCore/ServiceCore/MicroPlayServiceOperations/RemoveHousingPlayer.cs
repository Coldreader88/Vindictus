using System;
using System.Collections.Generic;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.MicroPlayServiceOperations
{
	[Serializable]
	public sealed class RemoveHousingPlayer : Operation
	{
		public long CharacterID { get; set; }

		public RemoveHousingPlayer(long characterID)
		{
			this.CharacterID = characterID;
		}

		public override OperationProcessor RequestProcessor()
		{
			return new RemoveHousingPlayer.Request(this);
		}

		private class Request : OperationProcessor
		{
			public Request(RemoveHousingPlayer op) : base(op)
			{
			}

			public override IEnumerable<object> Run()
			{
				yield return null;
				base.Result = (base.Feedback is OkMessage);
				yield break;
			}
		}
	}
}
