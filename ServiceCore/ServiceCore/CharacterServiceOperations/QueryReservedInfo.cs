using System;
using System.Collections.Generic;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.CharacterServiceOperations
{
	[Serializable]
	public sealed class QueryReservedInfo : Operation
	{
		public string NexonID { get; set; }

		public QueryReservedInfo(string NexonID)
		{
			this.NexonID = NexonID;
		}

		public ICollection<string> ReservedNames
		{
			get
			{
				return this.reservedNames;
			}
			set
			{
				this.reservedNames = value;
			}
		}

		public ICollection<int> ReservedTitles
		{
			get
			{
				return this.reservedTitles;
			}
			set
			{
				this.reservedTitles = value;
			}
		}

		public override OperationProcessor RequestProcessor()
		{
			return new QueryReservedInfo.Request(this);
		}

		[NonSerialized]
		private ICollection<string> reservedNames;

		[NonSerialized]
		private ICollection<int> reservedTitles;

		private class Request : OperationProcessor<QueryReservedInfo>
		{
			public Request(QueryReservedInfo op) : base(op)
			{
			}

			public override IEnumerable<object> Run()
			{
				yield return null;
				if (base.Feedback is IList<string>)
				{
					base.Operation.ReservedNames = (base.Feedback as IList<string>);
					yield return null;
				}
				else if (base.Feedback is IList<int>)
				{
					base.Operation.ReservedTitles = (base.Feedback as IList<int>);
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
