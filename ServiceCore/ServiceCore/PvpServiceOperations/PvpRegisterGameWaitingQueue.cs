using System;
using System.Collections.Generic;
using ServiceCore.EndPointNetwork;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.PvpServiceOperations
{
	[Serializable]
	public sealed class PvpRegisterGameWaitingQueue : Operation
	{
		public int GameIndex { get; set; }

		public long CID { get; set; }

		public int Level { get; set; }

		public PvpRegisterGameWaitingQueue(int gameIndex, long cid, int level)
		{
			this.GameIndex = gameIndex;
			this.CID = cid;
			this.Level = level;
		}

		public override OperationProcessor RequestProcessor()
		{
			return new PvpRegisterGameWaitingQueue.Request(this);
		}

		[NonSerialized]
		public HeroesString ErrorMessage;

		private class Request : OperationProcessor<PvpRegisterGameWaitingQueue>
		{
			public Request(PvpRegisterGameWaitingQueue op) : base(op)
			{
			}

			public override IEnumerable<object> Run()
			{
				yield return null;
				if (base.Feedback is bool)
				{
					base.Result = true;
					base.Operation.ErrorMessage = null;
				}
				else if (base.Feedback is HeroesString)
				{
					base.Result = false;
					base.Operation.ErrorMessage = (base.Feedback as HeroesString);
				}
				else
				{
					base.Result = false;
					base.Operation.ErrorMessage = new HeroesString("GameUI_Heroes_PvpJoinFail_Unknown");
				}
				yield break;
			}
		}
	}
}
