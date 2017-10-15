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
	public class BService : Service
	{
		public override void Initialize(JobProcessor thread)
		{
			base.Initialize(thread, ABOperation.TypeConverters);
			LookUp baseLookUp = new LookUp(this);
			base.LookUp.BaseLookUp = baseLookUp;
			base.LookUp.AddLocation(new ServiceInfo
			{
				ID = 0,
				FullName = typeof(LocationService).FullName,
				EndPoint = new IPEndPoint(IPAddress.Loopback, 3000)
			});
			base.ProcessorBuilder.Add(typeof(Plus1), (Operation op) => new Plus1Processor(this, op as Plus1));
		}

		public static void StartService()
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
			service.RequestOperation(typeof(LocationService).FullName, registerop);
		}
	}
}
