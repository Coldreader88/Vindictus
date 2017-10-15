using System;
using System.Net;
using Devcat.Core;
using Devcat.Core.Threading;
using UnifiedNetwork.Cooperation;
using UnifiedNetwork.LocationService;
using UnifiedNetwork.LocationService.Operations;
using Utility;

namespace UnifiedNetwork.Entity.Test
{
	internal class AService : Service
	{
		public override void Initialize(JobProcessor thread)
		{
			base.Initialize(thread, null);
            UnifiedNetwork.LocationService.LookUp baseLookUp = new UnifiedNetwork.LocationService.LookUp(this);
			base.LookUp.BaseLookUp = baseLookUp;
			base.LookUp.AddLocation(new ServiceInfo
			{
				ID = 0,
				FullName = typeof(UnifiedNetwork.LocationService.LookUp).FullName,
                EndPoint = new IPEndPoint(IPAddress.Loopback, 3000)
			});
		}

		public override int CompareAndSwapServiceID(long id, string category, int beforeID)
		{
			return base.ID;
		}

		protected override IEntity MakeEntity(long id, string category)
		{
			if (category != "A")
			{
				return null;
			}
			return new Entity(id, category);
		}

		public static void StartService()
		{
			try
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
					Action<int> testRequest = null;
					Entity entity = new Entity(5L, "A");
					IEntityProxy[] connections = new IEntityProxy[5];
					for (int j = 0; j < 1; j++)
					{
						connections[j] = service.Connect(entity, new Location
						{
							ID = (long)j,
							Category = "B"
						});
					}
					testRequest = delegate(int i)
					{
						TestOp op2 = new TestOp
						{
							Param = i
						};
						op2.OnComplete += delegate(Operation op3)
						{
							Log<AService>.Logger.DebugFormat("result : {0}", op2.ResultMessage.Value);
							Scheduler.Schedule(thread, Job.Create<int>(testRequest, i + 1), 1000);
						};
						op2.OnFail += delegate(Operation op3)
						{
							Log<AService>.Logger.DebugFormat("TestOp Failed...", new object[0]);
						};
						connections[0].RequestOperation(op2);
					};
					Scheduler.Schedule(thread, Job.Create<int>(testRequest, 0), 1000);
				};
				registerop.OnFail += delegate(Operation op)
				{
					Log<AService>.Logger.ErrorFormat("cannot find LocationService or register failed : {0}", registerop.Category);
					service.Dispose();
				};
				thread.Enqueue(Job.Create<string, Register>(new Action<string, Register>(service.RequestOperation), typeof(UnifiedNetwork.LocationService.LookUp).FullName, registerop));
			}
			catch (Exception ex)
			{
				Log<AService>.Logger.Error("StartService", ex);
			}
		}
	}
}
