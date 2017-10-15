using System;
using System.Net;
using Devcat.Core;
using Devcat.Core.Threading;
using UnifiedNetwork.Cooperation;
using UnifiedNetwork.LocationService;
using Utility;

namespace UnifiedNetwork.OperationService.Test
{
	public class Test
	{
		public Test()
		{
			Service1 service = new Service1();
			Service2 service2 = new Service2();
			JobProcessor jobProcessor = new JobProcessor();
			jobProcessor.ExceptionOccur += this.thread_ExceptionOccur;
			jobProcessor.Start();
			service.Initialize(jobProcessor, TestOperations.TypeConverters);
			service2.Initialize(jobProcessor, TestOperations.TypeConverters);
			service.LookUp.AddLocation(new ServiceInfo
			{
				ID = 1,
				FullName = typeof(Service2).FullName,
				EndPoint = new IPEndPoint(IPAddress.Loopback, 10001)
			});
			service2.LookUp.AddLocation(new ServiceInfo
			{
				ID = 2,
				FullName = typeof(Service1).FullName,
				EndPoint = new IPEndPoint(IPAddress.Loopback, 10000)
			});
			service.Start(10000);
			service2.Start(10001);
			TestOp t1 = new TestOp(100);
			t1.OnComplete += delegate(Operation op)
			{
				Console.WriteLine("result of t1 : {0}", t1.OutValue);
			};
			TestOp t2 = new TestOp(200);
			t2.OnComplete += delegate(Operation op)
			{
				Console.WriteLine("result of t2 : {0}", t2.OutValue);
			};
			jobProcessor.Enqueue(Job.Create<string, TestOp>(new Action<string, TestOp>(service.RequestOperation), typeof(Service2).FullName, t1));
			jobProcessor.Enqueue(Job.Create<string, TestOp>(new Action<string, TestOp>(service2.RequestOperation), typeof(Service1).FullName, t2));
		}

		private void thread_ExceptionOccur(object sender, EventArgs<Exception> e)
		{
			Log<Test>.Logger.Fatal("Unhandled exception", e.Value);
		}
	}
}
