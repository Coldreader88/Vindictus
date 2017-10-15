using System;
using System.Collections.Generic;
using Devcat.Core.Threading;
using Nexon.CafeAuth;
using ServiceCore.CafeAuthServiceOperations;
using ServiceCore.EndPointNetwork;
using ServiceCore.FrontendServiceOperations;
using UnifiedNetwork.Cooperation;
using UnifiedNetwork.Entity;
using Utility;

namespace CafeAuthServiceCore.Processor
{
	internal class LoginProcessor : EntityProcessor<Login, CafeAuth>
	{
		public LoginProcessor(CafeAuthService service, Login op) : base(op)
		{
			this.Service = service;
		}

		private CafeAuthService Service { get; set; }

		public override IEnumerable<object> Run()
		{
			base.Entity.SetLoginInformation(base.Operation.NexonSN, base.Operation.NexonID, base.Operation.CharacterID, base.Operation.LocalAddress, base.Operation.RemoteAddress, base.Operation.CanTry, base.Operation.IsTrial, base.Operation.MachineID, base.Operation.GameRoomClient, base.Operation.ChannelCode);
			if (!this.Service.Valid)
			{
				this.Service.PCRoomManager.AddUser(255, base.Entity);
				Log<LoginProcessor>.Logger.Info("Invalid CafeAuth Session Server - Passed");
				base.Finished = true;
				yield return true;
				yield return false;
				yield return 0;
				yield return false;
				yield return 1;
			}
			else if (!this.Service.Running)
			{
				Log<LoginProcessor>.Logger.Info("Session Server is shut down : add to queue");
				base.Finished = true;
				this.Service.AddToWaitQueue(base.Entity);
				yield return false;
				yield return false;
				yield return 0;
				yield return false;
				yield return 0;
			}
			else
			{
				base.Entity.BeginLogin();
				yield return base.Entity.LoginSync;
				if (base.Entity.LoginSync.Result)
				{
					CafeAuthResult result = base.Entity.EndLogin();
					if (result == null)
					{
						Log<LoginProcessor>.Logger.ErrorFormat("Timeout for user [{{{0}}}] : Disconnected", base.Operation.NexonID);
						base.Entity.ReportCafeAuthMessage("Timeout");
						base.Finished = true;
						yield return new FailMessage("Entity.LoginSync.Result")
						{
							Reason = FailMessage.ReasonCode.LogicalFail
						};
					}
					else
					{
						base.Entity.SessionNo = result.SessionNo;
						if (this.Service.SessionDic.ContainsKey(result.SessionNo))
						{
							Log<CafeAuthService>.Logger.WarnFormat("Duplicate Session : [{0}]", result.SessionNo);
						}
						else
						{
							this.Service.SessionDic.Add(result.SessionNo, base.Entity);
						}
						if (result.ReloginRequired)
						{
							Scheduler.Schedule(this.Service.Thread, Job.Create(delegate
							{
								SendPacket op = SendPacket.Create<SystemMessage>(new SystemMessage(SystemMessageCategory.Dialog, "GameUI_Heroes_Policy_Shutdown_Relogin_Required"));
								base.Entity.FrontendConn.RequestOperation(op);
							}), 3000);
						}
						base.Entity.ReportCafeAuthLogin();
						if (result.PCRoomNo > 0)
						{
							this.Service.PCRoomManager.AddUser(result.PCRoomNo, base.Entity);
						}
						base.Finished = true;
						yield return result.Result != AuthorizeResult.Trial;
						if (result.Option == Option.NoOption && result.Result != AuthorizeResult.Trial)
						{
							yield return true;
						}
						else
						{
							yield return result.Result == AuthorizeResult.Forbidden || result.Result == AuthorizeResult.Terminate;
						}
						yield return (int)result.Option;
						yield return result.IsShutDownEnabled;
						yield return (result.Result != AuthorizeResult.Trial) ? 1 : 0;
					}
				}
				else
				{
					yield return "";
				}
			}
			yield break;
		}
	}
}
