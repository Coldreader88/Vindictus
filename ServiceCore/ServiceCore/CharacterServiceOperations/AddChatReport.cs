using System;
using System.Collections.Generic;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.CharacterServiceOperations
{
	[Serializable]
	public sealed class AddChatReport : Operation
	{
		public string m_Name { get; set; }

		public int m_Type { get; set; }

		public string m_Reason { get; set; }

		public string m_ChatLog { get; set; }

		public override OperationProcessor RequestProcessor()
		{
			return new AddChatReport.Request(this);
		}

		[NonSerialized]
		public bool IsCountOver;

		[NonSerialized]
		public bool IsSameName;

		[NonSerialized]
		public bool IsEmptyReason;

		private class Request : OperationProcessor<AddChatReport>
		{
			public Request(AddChatReport op) : base(op)
			{
			}

			public override IEnumerable<object> Run()
			{
				yield return null;
				if (base.Feedback is bool)
				{
					base.Result = true;
					base.Operation.IsCountOver = (bool)base.Feedback;
					yield return null;
					base.Operation.IsSameName = (bool)base.Feedback;
					yield return null;
					base.Operation.IsEmptyReason = (bool)base.Feedback;
				}
				else
				{
					base.Result = false;
					base.Operation.IsCountOver = false;
					base.Operation.IsSameName = false;
					base.Operation.IsEmptyReason = false;
				}
				yield break;
			}
		}
	}
}
