using System;
using System.Net;
using Devcat.Core;
using Devcat.Core.Threading;
using UnifiedNetwork.Cooperation;
using UnifiedNetwork.LocationService.Operations;
using UnifiedNetwork.OperationService;
using Utility;

namespace UnifiedNetwork.LocationService.Test
{
	public class AService : Service
	{
		public override void Initialize(JobProcessor thread)
		{
			base.Initialize(thread, null);
			LookUp baseLookUp = new LookUp(this);
			base.LookUp.BaseLookUp = baseLookUp;
			base.LookUp.AddLocation(new ServiceInfo
			{
				ID = 0,
				FullName = typeof(LocationService).FullName,
				EndPoint = new IPEndPoint(IPAddress.Loopback, 3000)
			});
		}

		public static void StartService()
		{
			AService service = new AService();
			JobProcessor thread = new JobProcessor();
			thread.ExceptionOccur += delegate(object sender, EventArgs<Exception> e)
			{
				Log<AService>.Logger.Error("StartService", e.Value);
			};
			service.Initialize(thread);
			thread.Start();
			Register registerop = new Register(typeof(AService));
			registerop.OnComplete += delegate(Operation op)
			{
				Log<AService>.Logger.DebugFormat("{0} registered on {1} : port {2}", registerop.Category, registerop.ID, registerop.Port);
				service.ID = registerop.ID;
				service.Suffix = registerop.Suffix;
				service.Start(registerop.Port);
				Plus1 op2 = new Plus1
				{
					Input = 5
				};
				op2.OnComplete += delegate(Operation x)
				{
					Log<AService>.Logger.DebugFormat("result : {0}", op2.Output);
				};
				op2.OnComplete += delegate(Operation x)
				{
					Scheduler.Schedule(thread, Job.Create(delegate
					{
						service.RequestOperation("B", op2);
					}), 5000);
				};
				op2.OnFail += delegate(Operation x)
				{
					Log<AService>.Logger.Debug("failed");
				};
				service.RequestOperation("B", op2);
			};
			registerop.OnFail += delegate(Operation op)
			{
				Log<AService>.Logger.ErrorFormat("cannot find LocationService or register failed : {0}", registerop.Category);
				service.Dispose();
			};
			thread.Enqueue(Job.Create<string, Register>(new Action<string, Register>(service.RequestOperation), typeof(LocationService).FullName, registerop));
		}
	}
}
