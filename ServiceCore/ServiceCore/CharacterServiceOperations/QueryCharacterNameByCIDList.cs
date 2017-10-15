using System;
using System.Collections.Generic;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.CharacterServiceOperations
{
	[Serializable]
	public class QueryCharacterNameByCIDList : Operation
	{
		public List<long> CIDList
		{
			get
			{
				return this.cidList;
			}
		}

		public List<string> NameList
		{
			get
			{
				return this.nameList;
			}
		}

		public QueryCharacterNameByCIDList(List<long> cidList)
		{
			this.cidList = cidList;
		}

		public override OperationProcessor RequestProcessor()
		{
			return new QueryCharacterNameByCIDList.Request(this);
		}

		private List<long> cidList;

		private List<string> nameList;

		private class Request : OperationProcessor<QueryCharacterNameByCIDList>
		{
			public Request(QueryCharacterNameByCIDList op) : base(op)
			{
			}

			public override IEnumerable<object> Run()
			{
				yield return null;
				if (base.Feedback is List<string>)
				{
					base.Operation.nameList = (List<string>)base.Feedback;
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
