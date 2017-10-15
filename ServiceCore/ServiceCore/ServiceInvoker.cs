using System;
using System.Net;
using System.Net.Sockets;
using System.Reflection;
using Devcat.Core.Threading;
using ServiceCore.CommonOperations;
using ServiceCore.CommonProcessors;
using UnifiedNetwork.Cooperation;
using UnifiedNetwork.LocationService;
using UnifiedNetwork.OperationService;
using Utility;

namespace ServiceCore
{
	public class ServiceInvoker
	{
		public static void StartService(string ip, string portstr, Service service)
		{
			ServiceInvoker.StartService(ip, portstr, service, true);
		}

		public static void StartService(string ip, string portstr, Service service, bool bUsePerfLog)
		{
			int port = int.Parse(portstr);
			IPAddress ipaddress = null;
			foreach (IPAddress ipaddress2 in Dns.GetHostAddresses(ip))
			{
				if (ipaddress2.AddressFamily == AddressFamily.InterNetwork)
				{
					ipaddress = ipaddress2;
					break;
				}
			}
			if (ipaddress == null)
			{
				Log<ServiceInvoker>.Logger.ErrorFormat("cannot resolve IPv4 address for hostname [{0}]", ip);
				return;
			}
			IPEndPoint arg = new IPEndPoint(ipaddress, port);
			if (FeatureMatrix.IsEnable("FeatureMatrixSyncService"))
			{
				FeatureMatrix.OverrideFeature(EventLoader.GetStartEventList());
			}
			Type typeFromHandle = typeof(UpdateFeatureMatrix);
			Func<Operation, OperationProcessor> func = (Operation op) => new UpdateFeatureMatrixProcessor(service, op as UpdateFeatureMatrix);
			MethodInfo method = service.GetType().GetMethod("RegisterProcessor", BindingFlags.Instance | BindingFlags.NonPublic);
			method.Invoke(service, new object[]
			{
				typeFromHandle,
				func
			});
			JobProcessor jobProcessor = new JobProcessor();
			service.Initialize(jobProcessor);
			UnifiedNetwork.LocationService.LookUp @object = new UnifiedNetwork.LocationService.LookUp(service);
			jobProcessor.Start();
			service.AddBootStep();
			jobProcessor.Enqueue(Job.Create<IPEndPoint>(new Action<IPEndPoint>(@object.StartService), arg));
			if (bUsePerfLog)
			{
				PerformanceLogger performanceLogger = new PerformanceLogger(service, 300000);
				performanceLogger.Start();
			}
		}
	}
}
