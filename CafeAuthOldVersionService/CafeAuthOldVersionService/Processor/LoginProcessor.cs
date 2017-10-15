using System;
using System.Collections.Generic;
using Devcat.Core.Threading;
using Nexon.CafeAuthOld;
using ServiceCore.CafeAuthServiceOperations;
using UnifiedNetwork.Cooperation;
using UnifiedNetwork.Entity;
using Utility;

namespace CafeAuthOldVersionServiceCore.Processor
{
	internal class LoginProcessor : EntityProcessor<Login, CafeAuth>
	{
		public LoginProcessor(CafeAuthOldVersionService service, Login op) : base(op)
		{
			this.Service = service;
		}

		private CafeAuthOldVersionService Service { get; set; }

		public override IEnumerable<object> Run()
		{
			Log<CafeAuthOldVersionService>.Logger.InfoFormat("CafeAuthService sync starting : {0} / {1}", base.Operation.NexonID, base.Operation.CharacterID);
			if (!this.Service.Valid)
			{
				base.Finished = true;
				Log<CafeAuthOldVersionService>.Logger.InfoFormat("CafeAuthService is invalid : {0} / {1}", base.Operation.NexonID, base.Operation.CharacterID);
				yield return true;
				yield return false;
				yield return 0;
				yield return false;
				yield return 1;
			}
			else if (!this.Service.Running)
			{
				base.Finished = true;
				Log<CafeAuthOldVersionService>.Logger.InfoFormat("CafeAuthService is not working : {0} / {1}", base.Operation.NexonID, base.Operation.CharacterID);
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
					Log<CafeAuthOldVersionService>.Logger.InfoFormat("CafeAuthService sync null : {0} / {1}", base.Operation.NexonID, base.Operation.CharacterID);
					base.Finished = true;
					yield return false;
					yield return false;
					yield return 0;
					yield return false;
					yield return 0;
				}
				else
				{
					IAsyncResult ar = sync.AsyncResult;
					if (ar == null)
					{
						Log<CafeAuthOldVersionService>.Logger.InfoFormat("ar is NULL from CafeAuth service : {0} / {1}", base.Operation.NexonID, base.Operation.CharacterID);
					}
					Scheduler.Schedule(this.Service.Thread, Job.Create<AsyncResultSync, IAsyncResult>(delegate(AsyncResultSync async, IAsyncResult asyncResult)
					{
						if (sync.AsyncResult == ar && (sync.AsyncResult == null || sync.AsyncResult.AsyncState == this.Entity))
						{
							Log<CafeAuthOldVersionService>.Logger.InfoFormat("No response from CafeAuth service : {0} / {1}", this.Operation.NexonID, this.Operation.CharacterID);
							sync.AsyncCallback(asyncResult);
						}
					}, sync, ar), this.ackCompleteTimeout);
					yield return sync;
					Log<CafeAuthOldVersionService>.Logger.InfoFormat("CafeAuthService sync : {0} / {1}", base.Operation.NexonID, base.Operation.CharacterID);
					base.Finished = true;
					if (sync.Result)
					{
						CafeAuthResult result = this.Service.EndLogin(sync.AsyncResult);
						if (result == null)
						{
							Log<CafeAuthOldVersionService>.Logger.InfoFormat("CafeAuth fail : {0} / {1}", base.Operation.NexonID, base.Operation.CharacterID);
							yield return "";
						}
						else
						{
							Log<CafeAuthOldVersionService>.Logger.InfoFormat("CafeAuth : {0} / {1} = {2} {3} {4}", new object[]
							{
								base.Operation.NexonID,
								base.Operation.CharacterID,
								result.Result,
								result.Option,
								result.CafeLevel
							});
							yield return result.Result != Nexon.CafeAuthOld.Result.Trial;
							if (result.Option == Option.NoOption && result.Result != Nexon.CafeAuthOld.Result.Trial)
							{
								yield return true;
							}
							else
							{
								yield return result.Result == Nexon.CafeAuthOld.Result.Forbidden || result.Result == Nexon.CafeAuthOld.Result.Terminate;
							}
							yield return (int)result.Option;
							yield return false;
							yield return (int)result.CafeLevel;
						}
					}
					else
					{
						base.Finished = true;
						Log<CafeAuthOldVersionService>.Logger.InfoFormat("CafeAuth Auth Time Out  : {0} / {1}", base.Operation.NexonID, base.Operation.CharacterID);
						yield return false;
						yield return false;
						yield return 0;
						yield return false;
						yield return 0;
					}
				}
			}
			yield break;
		}

		private readonly int ackCompleteTimeout = 30000;
	}
}
