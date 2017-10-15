using System;
using System.Collections.Generic;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.ExecutionServiceOperations
{
	[Serializable]
	public sealed class StartService : Operation
	{
		public string ServiceName
		{
			get
			{
				return this.serviceName;
			}
		}

		public StartService(string sname)
		{
			this.serviceName = sname;
		}

		public override OperationProcessor RequestProcessor()
		{
			return new StartService.Request(this);
		}

		private string serviceName;

		private class Request : OperationProcessor<StartService>
		{
			public Request(StartService op) : base(op)
			{
			}

			public override IEnumerable<object> Run()
			{
				if (!(base.Feedback is OkMessage))
				{
					base.Result = false;
				}
				yield break;
			}
		}
	}
}
