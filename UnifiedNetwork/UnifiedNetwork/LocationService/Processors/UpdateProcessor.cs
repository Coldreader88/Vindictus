using System;
using System.Collections.Generic;
using System.Net;
using UnifiedNetwork.Cooperation;
using UnifiedNetwork.LocationService.Operations;
using UnifiedNetwork.OperationService;
using Utility;

namespace UnifiedNetwork.LocationService.Processors
{
	internal class UpdateProcessor : OperationProcessor<Update>
	{
		public UpdateProcessor(Service service, Update op) : base(op)
		{
			this.service = service;
		}

		public override IEnumerable<object> Run()
		{
			switch (base.Operation.Type)
			{
			case 0:
			{
				IPAddress address = base.Operation.Info.EndPoint.Address;
				if (IPAddress.IsLoopback(address))
				{
					IPEndPoint location = this.service.LookUp.GetLocation(0);
					if (location != null)
					{
						address = location.Address;
					}
				}
				Log<UpdateProcessor>.Logger.DebugFormat("{3} : up {0} {1} {2}", new object[]
				{
					base.Operation.Info.ID,
					base.Operation.Info.FullName,
					base.Operation.Info.EndPoint,
					this.service.ID
				});
				this.service.LookUp.AddLocation(base.Operation.Info);
				break;
			}
			case 1:
				Log<UpdateProcessor>.Logger.DebugFormat("{2} : down {0} {1}", base.Operation.Info.ID, base.Operation.Info.FullName, this.service.ID);
				this.service.LookUp.RemoveLocation(base.Operation.Info.ID, base.Operation.Info.FullName);
				break;
			default:
				Log<UpdateProcessor>.Logger.ErrorFormat("Invalid update type : {0}", base.Operation.Type);
				base.Result = false;
				break;
			}
			yield break;
		}

		private Service service;
	}
}
