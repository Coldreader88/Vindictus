using System;
using System.Collections.Generic;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.MicroPlayServiceOperations
{
	[Serializable]
	public sealed class OpenRandomBox : Operation
	{
		public int GroupID { get; set; }

		public string RandomBoxName { get; set; }

		public OpenRandomBox(int groupID, string randomBoxName)
		{
			this.GroupID = groupID;
			this.RandomBoxName = randomBoxName;
		}

		public override OperationProcessor RequestProcessor()
		{
			return new OpenRandomBox.Request(this);
		}

		private class Request : OperationProcessor
		{
			public Request(OpenRandomBox op) : base(op)
			{
			}

			public override IEnumerable<object> Run()
			{
				yield return null;
				base.Result = (base.Feedback is OkMessage);
				yield break;
			}
		}
	}
}
