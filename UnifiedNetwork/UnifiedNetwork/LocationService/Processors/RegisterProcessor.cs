using System;
using System.Collections.Generic;
using UnifiedNetwork.Cooperation;
using UnifiedNetwork.LocationService.Operations;
using UnifiedNetwork.PipedNetwork;

namespace UnifiedNetwork.LocationService.Processors
{
	internal class RegisterProcessor : OperationProcessor<Register>
	{
		public RegisterProcessor(LocationService service, Register op) : base(op)
		{
			this.service = service;
		}

		public override IEnumerable<object> Run()
		{
			ServiceInfo result = this.service.Register(base.Operation.FullName, base.Operation.GUID, base.Operation.ModuleVersionId, base.Operation.GameCode, base.Operation.ServerCode);
			if (result == null)
			{
				base.Finished = true;
				yield return new FailMessage("[RegisterProcessor] result is null")
				{
					Reason = FailMessage.ReasonCode.LogicalFail
				};
			}
			else
			{
				Peer.CurrentPeer.Disconnected += delegate(Peer x)
				{
					this.service.Unregister(result.EndPoint.Address, result.EndPoint.Port);
				};
				base.Finished = true;
				yield return (ushort)result.ID;
				yield return (ushort)result.EndPoint.Port;
				yield return result.Suffix;
				yield return result.ServiceOrder;
				yield return result.LocalServiceOrder;
				using (IEnumerator<ServiceInfo> enumerator = this.service.Services.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						ServiceInfo info = enumerator.Current;
						Update op = new Update
						{
							Info = info,
							Type = 0
						};
						this.service.RequestOperation(Peer.CurrentPeer, op);
					}
					yield break;
				}
			}
			yield break;
		}

		private LocationService service;
	}
}
