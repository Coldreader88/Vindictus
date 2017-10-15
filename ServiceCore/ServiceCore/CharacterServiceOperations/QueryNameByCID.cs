using System;
using System.Collections.Generic;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.CharacterServiceOperations
{
	[Serializable]
	public sealed class QueryNameByCID : Operation
	{
		public long CID
		{
			get
			{
				return this.cid;
			}
			set
			{
				this.cid = value;
			}
		}

		public string Name
		{
			get
			{
				return this.name;
			}
			set
			{
				this.name = value;
			}
		}

		public bool IsDeleted
		{
			get
			{
				return this.isDeleted;
			}
			set
			{
				this.isDeleted = value;
			}
		}

		public QueryNameByCID(long cid)
		{
			this.cid = cid;
		}

		public override OperationProcessor RequestProcessor()
		{
			return new QueryNameByCID.Request(this);
		}

		private long cid;

		[NonSerialized]
		private string name;

		[NonSerialized]
		private bool isDeleted;

		private class Request : OperationProcessor<QueryNameByCID>
		{
			public Request(QueryNameByCID op) : base(op)
			{
			}

			public override IEnumerable<object> Run()
			{
				yield return null;
				if (base.Feedback is string)
				{
					base.Operation.Name = (string)base.Feedback;
					yield return null;
					base.Operation.IsDeleted = (bool)base.Feedback;
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
