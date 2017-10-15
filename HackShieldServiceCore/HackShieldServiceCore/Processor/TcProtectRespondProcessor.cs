using System;
using System.Collections.Generic;
using Devcat.Core.Threading;
using ServiceCore.HackShieldServiceOperations;
using UnifiedNetwork.Entity;
using Utility;

namespace HackShieldServiceCore.Processor
{
	internal class TcProtectRespondProcessor : EntityProcessor<TcProtectRespond, HackShieldClient>
	{
		private HackShieldService Service { get; set; }

		public TcProtectRespondProcessor(HackShieldService service, TcProtectRespond op) : base(op)
		{
			this.Service = service;
		}

		public override IEnumerable<object> Run()
		{
			int md5Check = base.Operation.Md5Check;
			int impressCheck = base.Operation.ImpressCheck;
			if (1 != md5Check)
			{
				string type = "MD5";
				using (HackShieldErrorLogDataContext hackShieldErrorLogDataContext = new HackShieldErrorLogDataContext())
				{
					try
					{
						string value = Convert.ToString(md5Check);
						hackShieldErrorLogDataContext.AddTcProtectError(new long?(base.Operation.CharacterID), type, value);
					}
					catch (Exception ex)
					{
						Log<TcProtectRespondProcessor>.Logger.Error(string.Format("Error while making log : [CharacterID = {0}]", base.Operation.CharacterID), ex);
					}
				}
				if (Log<TcProtectRespondProcessor>.Logger.IsWarnEnabled)
				{
					Log<TcProtectRespondProcessor>.Logger.WarnFormat("(TcProtect) mdt check failed {{ Entity.ID = {0:o} }} Md5 = {1}", base.Entity.ID, this.Service.TcProtectMd5);
				}
				Scheduler.Schedule(this.Service.Thread, Job.Create(new Action(base.Entity.Entity.Close)), 3000);
			}
			if (-1 != impressCheck)
			{
				string type2 = "Impress";
				using (HackShieldErrorLogDataContext hackShieldErrorLogDataContext2 = new HackShieldErrorLogDataContext())
				{
					try
					{
						string value2 = Convert.ToString(impressCheck);
						hackShieldErrorLogDataContext2.AddTcProtectError(new long?(base.Operation.CharacterID), type2, value2);
					}
					catch (Exception ex2)
					{
						Log<TcProtectRespondProcessor>.Logger.Error(string.Format("Error while making log : [CharacterID = {0}]", base.Operation.CharacterID), ex2);
					}
				}
				Scheduler.Schedule(this.Service.Thread, Job.Create(new Action(base.Entity.Entity.Close)), 3000);
			}
			base.Finished = true;
			yield break;
		}
	}
}
