using System;
using System.Collections.Generic;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.CharacterServiceOperations
{
	[Serializable]
	public sealed class ResetSkill : Operation
	{
		public string SkillID { get; set; }

		public int Reset { get; set; }

		public string ResultSkillID { get; set; }

		public int ResultCode { get; set; }

		public int ResultRank { get; set; }

		public int ReturnAP { get; set; }

		public override OperationProcessor RequestProcessor()
		{
			return new ResetSkill.Request(this);
		}

		private class Request : OperationProcessor<ResetSkill>
		{
			public Request(ResetSkill op) : base(op)
			{
			}

			public override IEnumerable<object> Run()
			{
				yield return null;
				base.Operation.ResultCode = (int)base.Feedback;
				if (base.Operation.ResultCode < 0)
				{
					base.Result = false;
				}
				else
				{
					switch (base.Operation.ResultCode)
					{
					case 0:
						yield return null;
						base.Operation.ResultRank = (int)base.Feedback;
						yield return null;
						base.Operation.ReturnAP = (int)base.Feedback;
						base.Operation.ResultSkillID = base.Operation.SkillID;
						break;
					case 2:
						yield return null;
						base.Operation.ResultSkillID = (string)base.Feedback;
						yield return null;
						base.Operation.ResultRank = (int)base.Feedback;
						break;
					}
				}
				yield break;
			}
		}
	}
}
