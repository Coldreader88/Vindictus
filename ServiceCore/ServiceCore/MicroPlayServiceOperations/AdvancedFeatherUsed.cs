using System;
using System.Collections.Generic;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.MicroPlayServiceOperations
{
	[Serializable]
	public sealed class AdvancedFeatherUsed : Operation
	{
		public long TargetCID { get; set; }

		public string SenderName { get; set; }

		public string SenderGuild { get; set; }

		public string ItemClass { get; set; }

		public AdvancedFeatherUsed(long targetCID, string senderName, string senderGuild, string itemclass)
		{
			this.TargetCID = targetCID;
			this.SenderName = senderName;
			this.SenderGuild = senderGuild;
			this.ItemClass = itemclass;
		}

		public override OperationProcessor RequestProcessor()
		{
			return new AdvancedFeatherUsed.Request(this);
		}

		[NonSerialized]
		public string ErrorCode;

		private class Request : OperationProcessor<AdvancedFeatherUsed>
		{
			public Request(AdvancedFeatherUsed op) : base(op)
			{
			}

			public override IEnumerable<object> Run()
			{
				yield return null;
				if (base.Feedback is OkMessage)
				{
					base.Result = true;
					base.Operation.ErrorCode = "";
				}
				else if (base.Feedback is string)
				{
					base.Result = false;
					base.Operation.ErrorCode = (base.Feedback as string);
				}
				else
				{
					base.Result = false;
					base.Operation.ErrorCode = "Unknown";
				}
				yield break;
			}
		}
	}
}
