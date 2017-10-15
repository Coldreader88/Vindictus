using System;
using System.Collections.Generic;
using System.Net;
using Devcat.Core.Threading;
using ServiceCore;
using UnifiedNetwork.Cooperation;
using UnifiedNetwork.Entity;

namespace AdminService
{
	public class AdaptorService : Service
	{
		public override void Initialize(JobProcessor thread)
		{
			base.Initialize(thread, null);
			base.RegisterMessage(OperationMessages.TypeConverters);
		}

		public new void RequestOperation(IPEndPoint location, Operation op)
		{
			base.Thread.Enqueue(Job.Create<IPEndPoint, Operation>(new Action<IPEndPoint, Operation>(base.RequestOperation), location, op));
		}

		public void QueryService(string category, Action<IEnumerable<IPEndPoint>> callback)
		{
			base.Thread.Enqueue(Job.Create<string, Action<IPEndPoint>>(new Action<string, Action<IPEndPoint>>(base.LookUp.FindLocation), category, delegate(IPEndPoint x)
			{
				callback(this.LookUp.GetLocations(category));
			}));
		}

		public override int CompareAndSwapServiceID(long id, string category, int beforeID)
		{
			return base.ID;
		}
	}
}
