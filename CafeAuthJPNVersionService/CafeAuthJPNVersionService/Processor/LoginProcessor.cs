using System;
using System.Collections.Generic;
using Devcat.Core.Threading;
using Nexon.CafeAuthJPN;
using ServiceCore.CafeAuthServiceOperations;
using UnifiedNetwork.Cooperation;
using UnifiedNetwork.Entity;
using Utility;

namespace CafeAuthJPNVersionServiceCore.Processor
{
	internal class LoginProcessor : EntityProcessor<Login, CafeAuth>
	{
		public LoginProcessor(CafeAuthJPNVersionService service, Login op) : base(op)
		{
			this.Service = service;
		}

		private CafeAuthJPNVersionService Service { get; set; }

		public override IEnumerable<object> Run()
		{
			if (!this.Service.Valid)
			{
				base.Finished = true;
				yield return true;
				yield return false;
				yield return 0;
				yield return false;
				yield return 0;
			}
			else if (!this.Service.Running)
			{
				base.Finished = true;
				yield return false;
				yield return false;
				yield return 0;
				yield return false;
				yield return 0;
			}
			else
			{
				AsyncResultSync sync = base.Entity.BeginLogin(base.Operation.NexonID, base.Operation.CharacterID, base.Operation.LocalAddress, base.Operation.RemoteAddress, base.Operation.CanTry, base.Operation.IsTrial, new MachineID(base.Operation.MachineID.ToByteArray()), base.Operation.GameRoomClient, base.Entity);
				if (sync == null)
				{
					Log<CafeAuthJPNVersionService>.Logger.InfoFormat("sync is NULL from CafeAuth service : {0} / {1}", base.Operation.NexonID, base.Operation.CharacterID);
					base.Finished = true;
					yield return false;
					yield return false;
					yield return 0;
					yield return 0;
				}
				else
				{
					IAsyncResult ar = sync.AsyncResult;
					if (ar == null)
					{
						Log<CafeAuthJPNVersionService>.Logger.InfoFormat("ar is NULL from CafeAuth service : {0} / {1}", base.Operation.NexonID, base.Operation.CharacterID);
					}
					Scheduler.Schedule(this.Service.Thread, Job.Create<AsyncResultSync, IAsyncResult>(delegate(AsyncResultSync async, IAsyncResult asyncResult)
					{
						if (sync.AsyncResult == ar && sync.AsyncResult.AsyncState == this.Entity)
						{
							Log<CafeAuthJPNVersionService>.Logger.InfoFormat("No response from CafeAuth service : {0} / {1}", this.Operation.NexonID, this.Operation.CharacterID);
							sync.AsyncCallback(asyncResult);
						}
					}, sync, ar), this.ackCompleteTimeout);
					yield return sync;
					base.Finished = true;
					if (sync.Result)
					{
						Log<CafeAuthJPNVersionService>.Logger.InfoFormat("sync.Result is true from CafeAuth service : {0} / {1}", base.Operation.NexonID, base.Operation.CharacterID);
						CafeAuthResult result = this.Service.EndLogin(sync.AsyncResult);
						if (result == null)
						{
							Log<CafeAuthJPNVersionService>.Logger.InfoFormat("CafeAuthResult Is NULL : {0} / {1}", base.Operation.NexonID, base.Operation.CharacterID);
							yield return "";
						}
						else
						{
							yield return result.Result != Nexon.CafeAuthJPN.Result.Trial;
							if (result.Option == Option.NoOption && result.Result != Nexon.CafeAuthJPN.Result.Trial)
							{
								Log<CafeAuthJPNVersionService>.Logger.InfoFormat("CafeAuthResult, Result={0}, Option={1} #1", result.Result, result.Option);
								yield return true;
							}
							else
							{
								Log<CafeAuthJPNVersionService>.Logger.InfoFormat("CafeAuthResult, Result={0}, Option={1} #2", result.Result, result.Option);
								yield return result.Result == Nexon.CafeAuthJPN.Result.Forbidden || result.Result == Nexon.CafeAuthJPN.Result.Terminate;
							}
							Log<CafeAuthJPNVersionService>.Logger.InfoFormat("CafeAuthResult, Result={0}, Option={1} #3", result.Result, result.Option);
							yield return (int)result.Option;
							yield return false;
							if (result.Result == Nexon.CafeAuthJPN.Result.Trial)
							{
								Log<CafeAuthJPNVersionService>.Logger.InfoFormat("CafeAuthResult, Result={0}, Option={1} #4", result.Result, result.Option);
								yield return 0;
							}
							else
							{
								Log<CafeAuthJPNVersionService>.Logger.InfoFormat("CafeAuthResult, Result={0}, Option={1} #5", result.Result, result.Option);
								yield return 1;
							}
						}
					}
					else
					{
						yield return "";
					}
				}
			}
			yield break;
		}

		private readonly int ackCompleteTimeout = 5000;
	}
}
