using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using Devcat.Core;
using Devcat.Core.Net.Message;
using Devcat.Core.Threading;
using UnifiedNetwork.Cooperation;
using UnifiedNetwork.LocationService.Operations;
using UnifiedNetwork.LocationService.Processors;
using UnifiedNetwork.OperationService;
using UnifiedNetwork.PipedNetwork;
using Utility;

namespace UnifiedNetwork.LocationService
{
	public class LocationService : Service
	{
		private static IPAddress LocalhostAddress { get; set; }

		static LocationService()
		{
			MessageHandlerFactory.Register(ServiceOperations.TypeConverters);
			string hostName = Dns.GetHostName();
			IPHostEntry hostEntry = Dns.GetHostEntry(hostName);
			LocationService.LocalhostAddress = (from x in hostEntry.AddressList
			where x.AddressFamily == AddressFamily.InterNetwork
			select x).FirstOrDefault<IPAddress>();
		}

		public override void Initialize(JobProcessor thread)
		{
			base.ID = 0;
			base.Initialize(thread, UnifiedNetwork.LocationService.Operations.Operations.TypeConverters);
			base.ProcessorBuilder.Add(typeof(Register), (Operation op) => new RegisterProcessor(this, op as Register));
			base.ProcessorBuilder.Add(typeof(Query), (Operation op) => new QueryProcessor(this, op as Query));
		}

		private void BroadcastOperation(Operation op)
		{
			foreach (Peer peer in base.Peers)
			{
				base.RequestOperation(peer, op);
			}
		}

		public IEnumerable<ServiceInfo> Services
		{
			get
			{
				return (from host in this.hosts.Values
				select host.Services).Aggregate((IEnumerable<ServiceInfo> x, IEnumerable<ServiceInfo> y) => x.Union(y));
			}
		}

		internal ServiceInfo Register(string fullName, Guid guid, Guid moduleVersionId, int gameCode, int serverCode)
		{
			IPAddress ipaddress = Peer.CurrentPeer.RemoteEndPoint.Address;
			if (ipaddress.ToString() == "127.0.0.1" && LocationService.LocalhostAddress != null)
			{
				ipaddress = LocationService.LocalhostAddress;
			}
			HostInfo hostInfo;
			if (!this.hosts.TryGetValue(ipaddress, out hostInfo))
			{
				hostInfo = new HostInfo(ipaddress);
				this.hosts[ipaddress] = hostInfo;
			}
			ServiceInfo serviceInfo = hostInfo.Register(fullName, guid, moduleVersionId, gameCode, serverCode);
			Log<LocationService>.Logger.DebugFormat("{0} reserved int {1} - {2}", serviceInfo.FullName, serviceInfo.ID, serviceInfo.EndPoint);
			serviceInfo.ServiceOrder = this.categoryCount.TryGetValue(fullName);
			this.categoryCount[fullName] = serviceInfo.ServiceOrder + 1;
			if (serviceInfo != null)
			{
				Update op = new Update
				{
					Info = serviceInfo,
					Type = 0
				};
				this.BroadcastOperation(op);
			}
			return serviceInfo;
		}

		internal ICollection<ServiceInfo> Query(int gameCode, int serverCode, string fullName)
		{
			LinkedList<ServiceInfo> linkedList = new LinkedList<ServiceInfo>();
			foreach (HostInfo hostInfo in this.hosts.Values)
			{
				foreach (ServiceInfo serviceInfo in hostInfo.Services)
				{
					if (serviceInfo.GameCode == gameCode && serviceInfo.ServerCode == serverCode && serviceInfo.FullName == fullName)
					{
						linkedList.AddLast(serviceInfo);
					}
				}
			}
			return linkedList;
		}

		internal ServiceInfo Unregister(IPAddress address, int port)
		{
			HostInfo hostInfo;
			if (!this.hosts.TryGetValue(address, out hostInfo))
			{
				return null;
			}
			ServiceInfo serviceInfo;
			if (!hostInfo.Unregister(port, out serviceInfo))
			{
				return null;
			}
			Update op = new Update
			{
				Info = serviceInfo,
				Type = 1
			};
			this.BroadcastOperation(op);
			return serviceInfo;
		}

		public static void StartService(string portstr)
		{
			int port = int.Parse(portstr);
			LocationService locationService = new LocationService();
			JobProcessor jobProcessor = new JobProcessor();
			jobProcessor.ExceptionOccur += delegate(object sender, EventArgs<Exception> e)
			{
				Log<JobProcessor>.Logger.Error("Unexpected exception", e.Value);
			};
			locationService.Initialize(jobProcessor);
			jobProcessor.Start();
			locationService.Start(port);
		}

		private Dictionary<IPAddress, HostInfo> hosts = new Dictionary<IPAddress, HostInfo>();

		private Dictionary<string, int> categoryCount = new Dictionary<string, int>();
	}
}
