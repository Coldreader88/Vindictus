using System;
using System.Collections.Generic;
using ServiceCore.EndPointNetwork.GuildService;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.GuildServiceOperations
{
	[Serializable]
	public sealed class QueryGuildList : Operation
	{
		public int QueryType { get; set; }

		public string SearchKey { get; set; }

		public int Page { get; set; }

		public byte PageSize { get; set; }

		public GuildListMessage Message
		{
			get
			{
				return this.message;
			}
			set
			{
				this.message = value;
			}
		}

		public QueryGuildList(int queryType, string searchKey, int page, byte pageSize)
		{
			this.QueryType = queryType;
			this.SearchKey = searchKey;
			this.Page = page;
			this.PageSize = pageSize;
		}

		public override OperationProcessor RequestProcessor()
		{
			return new QueryGuildList.Request(this);
		}

		[NonSerialized]
		private GuildListMessage message;

		private class Request : OperationProcessor<QueryGuildList>
		{
			public Request(QueryGuildList op) : base(op)
			{
			}

			public override IEnumerable<object> Run()
			{
				yield return null;
				if (base.Feedback is List<InGameGuildInfo>)
				{
					List<InGameGuildInfo> list = (List<InGameGuildInfo>)base.Feedback;
					yield return null;
					int page = (int)base.Feedback;
					yield return null;
					int totalPage = (int)base.Feedback;
					base.Operation.Message = new GuildListMessage(list, page, totalPage);
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
