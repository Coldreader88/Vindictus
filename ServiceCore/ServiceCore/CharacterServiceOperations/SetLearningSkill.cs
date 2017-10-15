using System;
using System.Collections.Generic;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.CharacterServiceOperations
{
	[Serializable]
	public sealed class SetLearningSkill : Operation
	{
		public string SkillID
		{
			get
			{
				return this.skillID;
			}
			set
			{
				this.skillID = value;
			}
		}

		public int AP
		{
			get
			{
				return this.ap;
			}
			set
			{
				this.ap = value;
			}
		}

		public SetLearningSkill(string skillID)
		{
			this.skillID = skillID;
		}

		public override OperationProcessor RequestProcessor()
		{
			return new SetLearningSkill.Request(this);
		}

		private string skillID;

		[NonSerialized]
		private int ap;

		private class Request : OperationProcessor<SetLearningSkill>
		{
			public Request(SetLearningSkill op) : base(op)
			{
			}

			public override IEnumerable<object> Run()
			{
				yield return null;
				if (base.Feedback is string)
				{
					base.Result = true;
					base.Operation.SkillID = (base.Feedback as string);
					yield return null;
					base.Operation.AP = (int)base.Feedback;
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
