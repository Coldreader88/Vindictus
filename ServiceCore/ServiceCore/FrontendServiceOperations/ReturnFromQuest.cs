using System;
using ServiceCore.EndPointNetwork;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.FrontendServiceOperations
{
	[Serializable]
	public sealed class ReturnFromQuest : Operation
	{
		public bool Restart { get; set; }

		public bool ShipLaunchFail { get; set; }

		public ReturnFromQuestType ReturnType
		{
			get
			{
				if (this.Restart)
				{
					return ReturnFromQuestType.ToShip;
				}
				return ReturnFromQuestType.ToTown;
			}
		}

		public ReturnFromQuest(bool restart, bool shipLaunchFail)
		{
			this.Restart = restart;
			this.ShipLaunchFail = shipLaunchFail;
		}

		public override OperationProcessor RequestProcessor()
		{
			return new OkOrFailProcessor(this);
		}
	}
}
