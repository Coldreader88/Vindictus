using System;
using System.Collections.Generic;
using DSService.WaitingQueue;
using ServiceCore;
using ServiceCore.DSServiceOperations;
using UnifiedNetwork.Cooperation;
using Utility;

namespace DSService.Processor
{
	internal class UpdateDSShipInfoProcessor : OperationProcessor<UpdateDSShipInfo>
	{
		public UpdateDSShipInfoProcessor(DSService sv, UpdateDSShipInfo op) : base(op)
		{
			this.service = sv;
		}

		public override IEnumerable<object> Run()
		{
			bool result = false;
			if (this.service.DSWaitingSystem != null)
			{
				try
				{
					switch (base.Operation.Command)
					{
					case UpdateDSShipInfo.CommandEnum.LaunchComplete:
						this.service.DSWaitingSystem.DSShipLaunched(base.Operation.DSID, base.Operation.PartyID);
						break;
					case UpdateDSShipInfo.CommandEnum.StartGame:
						this.service.DSWaitingSystem.DSShipUpdated(base.Operation.DSID, DSGameState.GameStarted);
						break;
					case UpdateDSShipInfo.CommandEnum.BlockEntering:
						this.service.DSWaitingSystem.DSShipUpdated(base.Operation.DSID, DSGameState.BlockEntering);
						break;
					case UpdateDSShipInfo.CommandEnum.DSFinished:
						this.service.DSWaitingSystem.DSShipSinked(base.Operation.DSID, base.Operation.Arg);
						if (FeatureMatrix.IsEnable("DSDynamicLoad"))
						{
							this.service.DSEntityMakerSystem.UnuseEntity(base.Operation.ServiceID, base.Operation.DSID);
						}
						break;
					case UpdateDSShipInfo.CommandEnum.PVPFinished:
						this.service.DSEntityMakerSystem.UnuseEntity(base.Operation.ServiceID, base.Operation.DSID);
						break;
					case UpdateDSShipInfo.CommandEnum.DSClosed:
						this.service.DSWaitingSystem.DSShipSinked(base.Operation.DSID, base.Operation.Arg);
						this.service.DSEntityMakerSystem.CloseEntity(base.Operation.ServiceID, base.Operation.DSID);
						break;
					case UpdateDSShipInfo.CommandEnum.PVPClosed:
						this.service.DSEntityMakerSystem.CloseEntity(base.Operation.ServiceID, base.Operation.DSID);
						break;
					}
					result = true;
				}
				catch (Exception ex)
				{
					Log<UpdateDSShipInfoProcessor>.Logger.Error("exception occurred!!!", ex);
				}
			}
			if (result)
			{
				base.Finished = true;
				yield return new OkMessage();
			}
			else
			{
				base.Finished = true;
				yield return new FailMessage("[UpdateDSShipInfoProcessor] result");
			}
			yield break;
		}

		private DSService service;
	}
}
