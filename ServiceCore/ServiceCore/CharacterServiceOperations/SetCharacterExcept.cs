using System;
using System.Collections.Generic;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.CharacterServiceOperations
{
	[Serializable]
	public class SetCharacterExcept : Operation
	{
		public long CID { get; set; }

		public byte? LastStatus { get; set; }

		public SetCharacterExcept(long cid, byte? lastStatus)
		{
			this.CID = cid;
			this.LastStatus = lastStatus;
		}

		public override OperationProcessor RequestProcessor()
		{
			return new SetCharacterExcept.Request(this);
		}

		private class Request : OperationProcessor
		{
			public Request(SetCharacterExcept op) : base(op)
			{
			}

			public override IEnumerable<object> Run()
			{
				yield return null;
				base.Finished = true;
				if (base.Feedback is OkMessage)
				{
					base.Result = true;
				}
				else
				{
					base.Result = false;
				}
				yield break;
			}
		}
	}
}
