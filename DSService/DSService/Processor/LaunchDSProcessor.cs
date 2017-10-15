using System;
using System.Collections.Generic;
using ServiceCore.DSServiceOperations;
using UnifiedNetwork.Cooperation;
using Utility;

namespace DSService.Processor
{
	internal class LaunchDSProcessor : OperationProcessor<LaunchDS>
	{
		public LaunchDSProcessor(DSService service, LaunchDS op) : base(op)
		{
			this.service = service;
		}

		public override IEnumerable<object> Run()
		{
			DSEntity ds = this.service.GetDSEntity(base.Operation.DSID);
			if (ds != null)
			{
				ds.StartQuestDS(base.Operation.QuestID, base.Operation.MicroPlayID, base.Operation.PartyID, base.Operation.FrontendID, base.Operation.IsGiantRaid, base.Operation.IsAdultMode);
				base.Finished = true;
				yield return new OkMessage();
			}
			else
			{
				Log<LaunchDSProcessor>.Logger.ErrorFormat("GetDsEntity is null DSID {0}", base.Operation.DSID);
				base.Finished = true;
				yield return new FailMessage("[LaunchDSProcessor] ds");
			}
			yield break;
		}

		private DSService service;
	}
}
