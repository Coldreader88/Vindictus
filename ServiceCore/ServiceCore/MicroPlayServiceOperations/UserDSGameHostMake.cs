using System;
using System.Collections.Generic;
using System.Net;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.MicroPlayServiceOperations
{
	[Serializable]
	public sealed class UserDSGameHostMake : Operation
	{
		public long UserDSEntityID { get; set; }

		public IPEndPoint EndPoint
		{
			get
			{
				return this.endpoint;
			}
		}

		public int SN
		{
			get
			{
				return this.sn;
			}
		}

		public int Key
		{
			get
			{
				return this.key;
			}
		}

		public override OperationProcessor RequestProcessor()
		{
			return new UserDSGameHostMake.Request(this);
		}

		[NonSerialized]
		private IPEndPoint endpoint;

		[NonSerialized]
		private int sn;

		[NonSerialized]
		private int key;

		private class Request : OperationProcessor<UserDSGameHostMake>
		{
			public Request(UserDSGameHostMake op) : base(op)
			{
			}

			public override IEnumerable<object> Run()
			{
				yield return null;
				if (base.Feedback is IPEndPoint)
				{
					base.Operation.endpoint = (base.Feedback as IPEndPoint);
					yield return null;
					base.Operation.sn = (int)base.Feedback;
					yield return null;
					base.Operation.key = (int)base.Feedback;
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
