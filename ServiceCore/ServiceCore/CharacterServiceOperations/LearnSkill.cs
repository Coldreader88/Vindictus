using System;
using System.Collections.Generic;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.CharacterServiceOperations
{
	[Serializable]
	public sealed class LearnSkill : Operation
	{
		public string SkillID
		{
			get
			{
				return this.skillID;
			}
		}

		public int AP
		{
			get
			{
				return this.ap;
			}
		}

		public LearnSkill(string skillID, int ap)
		{
			this.skillID = skillID;
			this.ap = ap;
		}

		public override OperationProcessor RequestProcessor()
		{
			return new LearnSkill.Request(this);
		}

		private string skillID;

		private int ap;

		private class Request : OperationProcessor
		{
			public Request(LearnSkill op) : base(op)
			{
			}

			public override IEnumerable<object> Run()
			{
				yield return null;
				base.Finished = true;
				if (!(base.Feedback is OkMessage))
				{
					base.Result = false;
				}
				yield break;
			}
		}
	}
}
