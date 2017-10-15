using System;
using System.Collections.Generic;
using UnifiedNetwork.Cooperation;
using Utility;

namespace ServiceCore.ExecutionServiceOperations
{
	[Serializable]
	public sealed class ExecAppDomain : Operation
	{
		public IEnumerable<string> LoadedAppDomains
		{
			get
			{
				return this.loadedAppDomains;
			}
			set
			{
				this.loadedAppDomains = value;
			}
		}

		public string AppDomainName
		{
			get
			{
				return this.appdomainName;
			}
		}

		public int ExecOption
		{
			get
			{
				return this.execOption;
			}
		}

		public ExecAppDomain(string domain, int option)
		{
			this.appdomainName = domain;
			this.execOption = option;
		}

		public override OperationProcessor RequestProcessor()
		{
			return new ExecAppDomain.Request(this);
		}

		[NonSerialized]
		private IEnumerable<string> loadedAppDomains;

		private string appdomainName;

		private int execOption;

		private class Request : OperationProcessor<ExecAppDomain>
		{
			public Request(ExecAppDomain op) : base(op)
			{
			}

			public override IEnumerable<object> Run()
			{
				yield return null;
				base.Operation.loadedAppDomains = (base.Feedback as IList<string>);
				if (base.Operation.loadedAppDomains == null)
				{
					if (base.Feedback is FailMessage)
					{
						base.Result = false;
					}
					else
					{
						Log<ExecAppDomain>.Logger.ErrorFormat("쿼리했는데 이상한게 날아왔습니다.", new object[0]);
						base.Result = false;
					}
				}
				yield break;
			}
		}
	}
}
