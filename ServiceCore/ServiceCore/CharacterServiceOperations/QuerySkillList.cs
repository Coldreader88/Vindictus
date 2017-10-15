using System;
using System.Collections.Generic;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.CharacterServiceOperations
{
	[Serializable]
	public sealed class QuerySkillList : Operation
	{
		public string LearningSkillID
		{
			get
			{
				return this.learningSkillID;
			}
			set
			{
				this.learningSkillID = value;
			}
		}

		public int CurrentAP
		{
			get
			{
				return this.currentAP;
			}
			set
			{
				this.currentAP = value;
			}
		}

		public ICollection<BriefSkillInfo> SkillList
		{
			get
			{
				return this.skillList;
			}
			set
			{
				this.skillList = value;
			}
		}

		public override OperationProcessor RequestProcessor()
		{
			return new QuerySkillList.Request(this);
		}

		[NonSerialized]
		private string learningSkillID;

		[NonSerialized]
		private int currentAP;

		[NonSerialized]
		private ICollection<BriefSkillInfo> skillList;

		private class Request : OperationProcessor<QuerySkillList>
		{
			public Request(QuerySkillList op) : base(op)
			{
			}

			public override IEnumerable<object> Run()
			{
				yield return null;
				base.Finished = true;
				if (base.Feedback is SkillLearningInfo)
				{
					SkillLearningInfo skillLearningInfo = base.Feedback as SkillLearningInfo;
					base.Operation.LearningSkillID = skillLearningInfo.LearningSkillID;
					base.Operation.CurrentAP = skillLearningInfo.CurrentAp;
					base.Operation.SkillList = skillLearningInfo.SkillList;
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
