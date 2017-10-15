using System;
using System.Collections.Generic;
using System.Text;
using AhnLab.HackShield;
using Devcat.Core.Threading;
using ServiceCore.EndPointNetwork;
using ServiceCore.FrontendServiceOperations;
using ServiceCore.HackShieldServiceOperations;
using UnifiedNetwork.Entity;
using Utility;

namespace HackShieldServiceCore.Processor
{
	internal class RespondProcessor : EntityProcessor<Respond, HackShieldClient>
	{
		private HackShieldService Service { get; set; }

		public RespondProcessor(HackShieldService service, Respond op) : base(op)
		{
			this.Service = service;
		}

		public override IEnumerable<object> Run()
		{
			ArraySegment<byte> response = new ArraySegment<byte>(base.Operation.Buffer);
			string errorType = "";
			string errorValue = "";
			bool toClose = false;
			if (response.Array.Length >= 4 && response.Array[0] == 204 && response.Array[1] == 20 && response.Array[2] == 234 && response.Array[3] == 30)
			{
				errorType = "ClientCheat";
				errorValue = Encoding.ASCII.GetString(response.Array, 4, response.Array.Length - 4);
				if (Log<RespondProcessor>.Logger.IsWarnEnabled)
				{
					string text = "";
					for (int i = 0; i < response.Array.Length; i++)
					{
						text += string.Format("{0:X2} ", response.Array[i]);
					}
					Log<RespondProcessor>.Logger.WarnFormat("Client Hack/Cheat Detected {{ Entity.ID = {0:o} }} Msg = {1}", base.Entity.ID, text);
				}
			}
			else
			{
				AntiCpXSvr.Recommend recommend;
				AntiCpXSvr.Error error = AntiCpXSvr.VerifyResponse(base.Entity.Handle, response, out recommend);
				if (recommend == AntiCpXSvr.Recommend.KeepSession)
				{
					if (Log<RespondProcessor>.Logger.IsInfoEnabled)
					{
						Log<RespondProcessor>.Logger.InfoFormat("AntiCpXSvr.VerifyResponse(clientHandle, responseBuffer, out recommend = {0}) = {1} {{ Entity.ID = {2:o} }}", recommend, error, base.Entity.ID);
					}
				}
				else
				{
					if (Log<RespondProcessor>.Logger.IsWarnEnabled)
					{
						Log<RespondProcessor>.Logger.WarnFormat("AntiCpXSvr.VerifyResponse(clientHandle, responseBuffer, out recommend = {0}) = {1} {{ Entity.ID = {2:o} }}", recommend, error, base.Entity.ID);
					}
					errorType = error.ToString();
					toClose = true;
				}
			}
			if (errorType.Length > 0)
			{
				using (HackShieldErrorLogDataContext hackShieldErrorLogDataContext = new HackShieldErrorLogDataContext())
				{
					try
					{
						hackShieldErrorLogDataContext.AddHackShieldError(new long?(base.Operation.CharacterID), errorType, errorValue);
					}
					catch (Exception ex)
					{
						Log<RespondProcessor>.Logger.Error(string.Format("Error while making log : [CharacterID = {0}]", base.Operation.CharacterID), ex);
					}
				}
				if (toClose)
				{
					SendPacket packet;
					if (!base.Operation.IsCheat)
					{
						packet = SendPacket.Create<SystemMessage>(new SystemMessage(SystemMessageCategory.Dialog, "GameUI_HackSheild_Error"));
					}
					else
					{
						packet = SendPacket.Create<SystemMessage>(new SystemMessage(SystemMessageCategory.Notice, "GameUI_HackSheild_Contact"));
					}
					OperationSync sync = new OperationSync
					{
						Connection = base.Entity.FrontendConn,
						Operation = packet
					};
					yield return sync;
					Scheduler.Schedule(this.Service.Thread, Job.Create(new Action(base.Entity.Entity.Close)), 3000);
				}
			}
			base.Finished = true;
			yield break;
		}
	}
}
