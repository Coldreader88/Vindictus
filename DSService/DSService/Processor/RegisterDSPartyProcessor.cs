using System;
using System.Collections.Generic;
using ServiceCore.DSServiceOperations;
using UnifiedNetwork.Cooperation;
using Utility;

namespace DSService.Processor
{
	internal class RegisterDSPartyProcessor : OperationProcessor<RegisterDSParty>
	{
		public RegisterDSPartyProcessor(DSService sv, RegisterDSParty op) : base(op)
		{
			this.service = sv;
		}

		public override IEnumerable<object> Run()
		{
			if (this.service.DSWaitingSystem != null)
			{
				RegisterDSPartyResult result = this.service.DSWaitingSystem.Register(base.Operation.QuestID, base.Operation.PartyMembers, base.Operation.MicroPlayID, base.Operation.PartyID, base.Operation.IsGiantRaid, base.Operation.IsAdultMode);
				base.Finished = true;
				yield return result;
			}
			else
			{
				Log<RegisterDSPartyProcessor>.Logger.Fatal("DS WaitingSystem is null. invalid DSBossID");
				base.Finished = true;
				yield return RegisterDSPartyResult.InvalidBossID;
			}
			yield break;
		}

		private DSService service;
	}
}
