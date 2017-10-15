using System;
using System.Collections.Generic;
using ServiceCore.HackShieldServiceOperations;
using UnifiedNetwork.Cooperation;
using UnifiedNetwork.Entity;

namespace HackShieldServiceCore.Processor
{
	internal class TcProtectCheckProcessor : EntityProcessor<TcProtectCheck, HackShieldClient>
	{
		private HackShieldService Service { get; set; }

		public TcProtectCheckProcessor(HackShieldService service, TcProtectCheck op) : base(op)
		{
			this.Service = service;
		}

		public override IEnumerable<object> Run()
		{
			if (base.Entity == null)
			{
				base.Finished = true;
				yield return new FailMessage("[TcProtectCheckProcessor] Entity");
			}
			else
			{
				base.Entity.MakeTcProtectRequest();
				base.Finished = true;
			}
			yield break;
		}
	}
}
