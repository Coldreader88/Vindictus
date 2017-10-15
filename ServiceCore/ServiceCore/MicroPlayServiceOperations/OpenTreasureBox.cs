using System;
using System.Collections.Generic;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.MicroPlayServiceOperations
{
	[Serializable]
	public sealed class OpenTreasureBox : Operation
	{
		public int GroupID { get; set; }

		public string TreasureBoxName { get; set; }

		public OpenTreasureBox(int groupID, string treasureBoxName)
		{
			this.GroupID = groupID;
			this.TreasureBoxName = treasureBoxName;
		}

		public override OperationProcessor RequestProcessor()
		{
			return new OpenTreasureBox.Request(this);
		}

		private class Request : OperationProcessor
		{
			public Request(OpenTreasureBox op) : base(op)
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
