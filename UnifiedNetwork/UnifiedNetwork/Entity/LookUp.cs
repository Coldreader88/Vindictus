using System;
using System.Net;
using Devcat.Core.Threading;
using UnifiedNetwork.Cooperation;
using UnifiedNetwork.Entity.Operations;
using UnifiedNetwork.LocationFree;
using UnifiedNetwork.PipedNetwork;
using Utility;

namespace UnifiedNetwork.Entity
{
	public class LookUp : IAsyncLookUp<Location, IPEndPoint>
	{
		public Service Service { get; set; }

		public IAsyncLookUp<string, IPEndPoint> CategoryLookUp { get; set; }

		public IAsyncLookUp<int, IPEndPoint> IDLookUp { get; set; }

		public IAsyncLookUp<Location, IPEndPoint> BaseLookUp
		{
			get
			{
				return null;
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		public void FindLocation(Location key, Action<IPEndPoint> callback)
		{
			JobProcessor currentProcessor = JobProcessor.Current;
			this.CategoryLookUp.FindLocation(key.Category, delegate(IPEndPoint endpoint)
			{
				if (endpoint == null)
				{
					Log<LookUp>.Logger.ErrorFormat("Cannot find category {0}", key.Category);
					currentProcessor.Enqueue(Job.Create<IPEndPoint>(callback, null));
					return;
				}
				this.Service.ConnectToIP(endpoint, delegate(Peer peer)
				{
					SelectEntity selectop = new SelectEntity
					{
						ID = key.ID,
						Category = key.Category
					};
					selectop.OnComplete += delegate(Operation op)
					{
						switch (selectop.Result)
						{
						case SelectEntity.ResultCode.Ok:
							currentProcessor.Enqueue(Job.Create<IPEndPoint>(callback, endpoint));
							return;
						case SelectEntity.ResultCode.Redirect:
							this.IDLookUp.FindLocation(selectop.RedirectServiceID, callback);
							return;
						default:
							currentProcessor.Enqueue(Job.Create<IPEndPoint>(callback, null));
							return;
						}
					};
					selectop.OnFail += delegate(Operation op)
					{
						Log<LookUp>.Logger.ErrorFormat("SelectEntity failed : [{0}], [{1}], {2}, {3}", new object[]
						{
							selectop.Result,
							key,
							key.ID,
							key.Category
						});
						currentProcessor.Enqueue(Job.Create<IPEndPoint>(callback, null));
					};
					this.Service.RequestOperation(peer, selectop);
				});
			});
		}
	}
}
