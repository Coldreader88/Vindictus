using System;
using System.Collections.Generic;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.CharacterServiceOperations
{
	[Serializable]
	public sealed class QueryUserAchievement : Operation
	{
		public override OperationProcessor RequestProcessor()
		{
			return new QueryUserAchievement.Request(this);
		}

		[NonSerialized]
		public ICollection<string> UserAchievementList;

		private class Request : OperationProcessor<QueryUserAchievement>
		{
			public Request(QueryUserAchievement op) : base(op)
			{
			}

			public override IEnumerable<object> Run()
			{
				yield return null;
				base.Finished = true;
				if (base.Feedback is IList<string>)
				{
					base.Operation.UserAchievementList = (base.Feedback as IList<string>);
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
