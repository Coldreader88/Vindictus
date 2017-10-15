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
	internal class BService : Service
	{
		public override void Initialize(JobProcessor thread)
		{
			base.Initialize(thread, BOP.TypeConverters);
            UnifiedNetwork.LocationService.LookUp baseLookUp = new UnifiedNetwork.LocationService.LookUp(this);
			base.LookUp.BaseLookUp = baseLookUp;
			base.LookUp.AddLocation(new ServiceInfo
			{
				ID = 0,
				FullName = typeof(UnifiedNetwork.LocationService.LookUp).FullName,
				EndPoint = new IPEndPoint(IPAddress.Loopback, 3000)
			});
			base.ProcessorBuilder.Add(typeof(TestOp), (Operation op) => new TestOpProcessor(this, op as TestOp));
		}

		public override int CompareAndSwapServiceID(long id, string category, int beforeID)
		{
			return base.ID;
		}

		protected override IEntity MakeEntity(long id, string category)
		{
			if (category != "B")
			{
				return null;
			}
			return new Entity(id, category);
		}

		public static void StartService()
		{
			try
			{
				BService service = new BService();
				JobProcessor jobProcessor = new JobProcessor();
				jobProcessor.ExceptionOccur += delegate(object sender, EventArgs<Exception> e)
				{
					Log<BService>.Logger.Error("StartService", e.Value);
				};
				service.Initialize(jobProcessor);
				jobProcessor.Start();
				Register registerop = new Register(typeof(BService));
				registerop.OnComplete += delegate(Operation op)
				{
					Log<BService>.Logger.DebugFormat("{0} registered on {1} : port {2}", registerop.Category, registerop.ID, registerop.Port);
					service.ID = registerop.ID;
					service.Suffix = registerop.Suffix;
					service.Start(registerop.Port);
				};
				registerop.OnFail += delegate(Operation op)
				{
					Log<BService>.Logger.ErrorFormat("cannot find LocationService or register failed : {0}", registerop.Category);
					service.Dispose();
				};
				jobProcessor.Enqueue(Job.Create<string, Register>(new Action<string, Register>(service.RequestOperation), typeof(UnifiedNetwork.LocationService.LookUp).FullName, registerop));
			}
			catch (Exception ex)
			{
				Log<BService>.Logger.Error("StartService", ex);
			}
		}
	}
}
