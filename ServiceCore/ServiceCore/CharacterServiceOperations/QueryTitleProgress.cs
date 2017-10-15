using System;
using System.Collections.Generic;
using ServiceCore.EndPointNetwork;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.CharacterServiceOperations
{
	[Serializable]
	public sealed class QueryTitleProgress : Operation
	{
		public ICollection<TitleSlotInfo> Titles
		{
			get
			{
				return this.titles;
			}
		}

		public ICollection<TitleSlotInfo> AccountTitles
		{
			get
			{
				return this.accountTitles;
			}
		}

		public override OperationProcessor RequestProcessor()
		{
			return new QueryTitleProgress.Request(this);
		}

		[NonSerialized]
		private ICollection<TitleSlotInfo> titles;

		[NonSerialized]
		private ICollection<TitleSlotInfo> accountTitles;

		private class Request : OperationProcessor<QueryTitleProgress>
		{
			public Request(QueryTitleProgress op) : base(op)
			{
			}

			public override IEnumerable<object> Run()
			{
				yield return null;
				base.Operation.accountTitles = (base.Feedback as IList<TitleSlotInfo>);
				yield return null;
				base.Operation.titles = (base.Feedback as IList<TitleSlotInfo>);
				yield break;
			}
		}
	}
}
