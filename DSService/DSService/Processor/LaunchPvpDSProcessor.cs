using System;
using System.Collections.Generic;
using ServiceCore.DSServiceOperations;
using UnifiedNetwork.Cooperation;
using Utility;

namespace DSService.Processor
{
	internal class LaunchPvpDSProcessor : OperationProcessor<LaunchPvpDS>
	{
		public LaunchPvpDSProcessor(DSService service, LaunchPvpDS op) : base(op)
		{
			this.service = service;
		}

		public override IEnumerable<object> Run()
		{
			DSEntity ds = this.service.GetDSEntity(base.Operation.DSID);
			if (ds != null)
			{
				if (base.Operation.PvpID == -1L)
				{
					ds.StopDS();
				}
				else
				{
					ds.StartPvpDS(base.Operation.PvpID, base.Operation.PvpBSP, base.Operation.Config, base.Operation.GameInfo);
				}
				base.Finished = true;
				yield return ds.Entity.ID;
			}
			else
			{
				Log<LaunchDSProcessor>.Logger.ErrorFormat("GetDsEntity is null DSID {0}", base.Operation.DSID);
				base.Finished = true;
				yield return new FailMessage("[LaunchPvpDSProcessor] ds");
			}
			yield break;
		}

		private DSService service;
	}
}
