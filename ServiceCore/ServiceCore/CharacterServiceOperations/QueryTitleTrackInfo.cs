using System;
using System.Collections.Generic;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.CharacterServiceOperations
{
	[Serializable]
	public sealed class QueryTitleTrackInfo : Operation
	{
		public long CID
		{
			get
			{
				return this.cid;
			}
		}

		public ICollection<int> TitleGoalIDs
		{
			get
			{
				return this.titleGoalIDs;
			}
			set
			{
				this.titleGoalIDs = value;
			}
		}

		public QueryTitleTrackInfo(long cid)
		{
			this.cid = cid;
		}

		public override OperationProcessor RequestProcessor()
		{
			return new QueryTitleTrackInfo.Request(this);
		}

		private long cid;

		[NonSerialized]
		private ICollection<int> titleGoalIDs;

		private class Request : OperationProcessor<QueryTitleTrackInfo>
		{
			public Request(QueryTitleTrackInfo op) : base(op)
			{
			}

			public override IEnumerable<object> Run()
			{
				yield return null;
				base.Finished = true;
				if (base.Feedback is IList<int>)
				{
					base.Operation.TitleGoalIDs = (base.Feedback as IList<int>);
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
