using System;
using System.Collections.Generic;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.ItemServiceOperations
{
	[Serializable]
	public sealed class ResetBingoBoard : Operation
	{
		public List<int> BingoBoard { get; set; }

		public override OperationProcessor RequestProcessor()
		{
			return new ResetBingoBoard.Request(this);
		}

		public bool DestoryNumbers;

		private class Request : OperationProcessor<ResetBingoBoard>
		{
			public Request(ResetBingoBoard op) : base(op)
			{
			}

			public override IEnumerable<object> Run()
			{
				yield return null;
				if (base.Feedback is List<int>)
				{
					base.Operation.BingoBoard = (List<int>)base.Feedback;
					base.Result = true;
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
