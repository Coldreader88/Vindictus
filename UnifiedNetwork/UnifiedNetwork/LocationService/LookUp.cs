using System;
using System.Collections.Generic;
using System.Net;
using Devcat.Core.Net.Message;
using UnifiedNetwork.Cooperation;
using UnifiedNetwork.LocationFree;
using UnifiedNetwork.LocationService.Operations;
using UnifiedNetwork.LocationService.Processors;
using UnifiedNetwork.OperationService;
using Utility;

namespace UnifiedNetwork.LocationService
{
	public class LookUp : IAsyncLookUp<string, IPEndPoint>
	{
		private Service Service { get; set; }

		public IAsyncLookUp<string, IPEndPoint> BaseLookUp { get; set; }

		static LookUp()
		{
			MessageHandlerFactory.Register(UnifiedNetwork.LocationService.Operations.Operations.TypeConverters);
		}

		public LookUp(Service service)
		{
			this.Service = service;
			this.Service.RegisterProcessor(typeof(Update), (Operation op) => new UpdateProcessor(this.Service, op as Update));
			this.Service.RegisterMessage(ServiceOperations.TypeConverters);
			this.Service.LookUp.BaseLookUp = this;
		}

		public void StartService(IPEndPoint locationServiceLocation)
		{
			this.Service.LookUp.AddLocation(new ServiceInfo
			{
				ID = 0,
				FullName = typeof(LocationService).FullName,
				EndPoint = locationServiceLocation
			});
			Register registerop = new Register(this.Service.GetType());
			registerop.OnComplete += delegate(Operation x)
			{
				this.Service.Suffix = registerop.Suffix;
				this.Service.ServiceOrder = registerop.ServiceOrder;
				this.Service.LocalServiceOrder = registerop.LocalOrder;
				this.Service.ID = registerop.ID;
				this.Service.Start(registerop.Port);
				this.Service.BootStep();
			};
			registerop.OnFail += delegate(Operation x)
			{
				Log<LookUp>.Logger.Error("Error in register location");
				this.Service.BootFail();
			};
			this.Service.RequestOperation(typeof(LocationService).FullName, registerop);
		}

		public void FindLocation(string key, Action<IPEndPoint> callback)
		{
			if (this.Service == null)
			{
				Log<LookUp>.Logger.Error("LocationServiceLookUp.Service 가 잘못된 참조입니다.");
				callback(null);
				return;
			}
			if (key == typeof(LocationService).FullName)
			{
				callback(null);
				return;
			}
			Query queryop = new Query(key);
			Log<LookUp>.Logger.DebugFormat("{0} query {1}", this.Service.ID, key);
			queryop.OnComplete += delegate(Operation op)
			{
				List<IPEndPoint> list = new List<IPEndPoint>();
				foreach (ServiceInfo serviceInfo in queryop.ServiceList)
				{
					list.Add(serviceInfo.EndPoint);
					this.Service.LookUp.AddLocation(serviceInfo);
				}
				if (list.Count != 0)
				{
					callback(list[this.random.Next(list.Count)]);
					return;
				}
				if (this.BaseLookUp != null)
				{
					this.BaseLookUp.FindLocation(key, callback);
					return;
				}
				callback(null);
			};
			queryop.OnFail += delegate(Operation op)
			{
				if (this.BaseLookUp != null)
				{
					this.BaseLookUp.FindLocation(key, callback);
					return;
				}
				callback(null);
			};
			this.Service.RequestOperation(typeof(LocationService).FullName, queryop);
		}

		private Random random = new Random();
	}
}
