using System;
using System.Collections.Generic;
using UnifiedNetwork.Cooperation;
using Utility;

namespace ServiceCore.QuestOwnershipServiceOperations
{
	[Serializable]
	public sealed class QueryQuestDigest : Operation
	{
		public string QuestID { get; set; }

		public int SwearID { get; set; }

		public int CheckDifficulty { get; set; }

		public bool CheckDisableUserShip { get; set; }

		public bool IsPracticeMode { get; set; }

		public bool IsUserDSMode { get; set; }

		public bool IsDropTest { get; set; }

		public QuestDigest QuestDigest
		{
			get
			{
				return this.resultDigest;
			}
		}

		public int QuestStatus
		{
			get
			{
				return this.resultStatus;
			}
		}

		public QuestConstraintResult FailReason
		{
			get
			{
				return this.failReason;
			}
			set
			{
				this.failReason = value;
			}
		}

		public override OperationProcessor RequestProcessor()
		{
			return new QueryQuestDigest.Request(this);
		}

		[NonSerialized]
		private QuestDigest resultDigest;

		[NonSerialized]
		private int resultStatus;

		[NonSerialized]
		private QuestConstraintResult failReason;

		private class Request : OperationProcessor<QueryQuestDigest>
		{
			public Request(QueryQuestDigest op) : base(op)
			{
			}

			public override IEnumerable<object> Run()
			{
				yield return null;
				base.Operation.resultDigest = (base.Feedback as QuestDigest);
				if (base.Operation.resultDigest == null)
				{
					if (base.Feedback is QuestConstraintResult)
					{
						base.Operation.FailReason = (QuestConstraintResult)base.Feedback;
					}
					else
					{
						base.Operation.FailReason = QuestConstraintResult.Unknown;
						Log<QueryQuestDigest>.Logger.ErrorFormat("invalid message : [{0}]", base.Feedback);
					}
					base.Result = false;
				}
				else
				{
					yield return null;
					base.Operation.resultStatus = (int)base.Feedback;
				}
				yield break;
			}
		}
	}
}
