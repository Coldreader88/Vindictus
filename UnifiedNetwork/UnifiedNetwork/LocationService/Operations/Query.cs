using System;
using System.Collections.Generic;
using UnifiedNetwork.Cooperation;
using UnifiedNetwork.Properties;

namespace UnifiedNetwork.LocationService.Operations
{
	[Serializable]
	public sealed class Query : Operation
	{
		public int GameCode { get; set; }

		public int ServerCode { get; set; }

		public string FullName { get; set; }

		public Query()
		{
		}

		public Query(string category)
		{
			this.GameCode = Settings.Default.GameCode;
			this.ServerCode = Settings.Default.ServerCode;
			this.FullName = category;
		}

		public ICollection<ServiceInfo> ServiceList
		{
			get
			{
				return this.result;
			}
		}

		public override OperationProcessor RequestProcessor()
		{
			return new Query.Request(this);
		}

		[NonSerialized]
		private ICollection<ServiceInfo> result;

		private class Request : OperationProcessor<Query>
		{
			public Request(Query op) : base(op)
			{
			}

			public override IEnumerable<object> Run()
			{
				yield return null;
				base.Operation.result = (base.Feedback as ICollection<ServiceInfo>);
				yield break;
			}
		}
	}
}
