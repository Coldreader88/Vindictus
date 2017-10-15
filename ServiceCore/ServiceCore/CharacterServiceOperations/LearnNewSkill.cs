using System;
using System.Collections.Generic;
using ServiceCore.EndPointNetwork;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.CharacterServiceOperations
{
	[Serializable]
	public sealed class LearnNewSkill : Operation
	{
		public string SkillID
		{
			get
			{
				return this.skillID;
			}
		}

		public string FailReason
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

		public int ConvertedFailReason
		{
			get
			{
				return this.convertedFailReason;
			}
			set
			{
				this.convertedFailReason = value;
			}
		}

		public LearnNewSkill(string skillID)
		{
			this.skillID = skillID;
		}

		public override OperationProcessor RequestProcessor()
		{
			return new LearnNewSkill.Request(this);
		}

		public string skillID;

		[NonSerialized]
		private string failReason;

		[NonSerialized]
		private int convertedFailReason;

		private class Request : OperationProcessor<LearnNewSkill>
		{
			public Request(LearnNewSkill op) : base(op)
			{
			}

			public override IEnumerable<object> Run()
			{
				yield return null;
				base.Finished = true;
				if (base.Feedback is OkMessage)
				{
					base.Operation.FailReason = null;
				}
				else if (base.Feedback is string)
				{
					base.Result = false;
					base.Operation.FailReason = (base.Feedback as string);
					base.Operation.FailReason.ToLearnNewSkillResult();
				}
				else
				{
					base.Result = false;
					base.Operation.FailReason = "System";
					base.Operation.FailReason.ToLearnNewSkillResult();
				}
				yield break;
			}
		}
	}
}
