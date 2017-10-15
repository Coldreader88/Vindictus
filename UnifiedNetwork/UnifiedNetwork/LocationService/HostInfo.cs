using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using UnifiedNetwork.PipedNetwork;
using UnifiedNetwork.Properties;
using Utility;

namespace UnifiedNetwork.LocationService
{
	internal class HostInfo
	{
		public IPAddress Address { get; private set; }

		private ICollection<int> Ports { get; set; }

		public IEnumerable<ServiceInfo> Services
		{
			get
			{
				return this.services.Values;
			}
		}

		public HostInfo(IPAddress address)
		{
			this.Address = address;
			List<int> list = new List<int>();
			list.AddRange(Enumerable.Range(14415, 521));
			list.AddRange(Enumerable.Range(21001, 553));
			list.AddRange(Enumerable.Range(23458, 542));
			list.AddRange(Enumerable.Range(25010, 783));
			list.AddRange(Enumerable.Range(28241, 877));
			list.AddRange(Enumerable.Range(29169, 832));
			list.AddRange(Enumerable.Range(30261, 738));
			list.AddRange(Enumerable.Range(33657, 592));
			list.AddRange(Enumerable.Range(34380, 582));
			list.AddRange(Enumerable.Range(34981, 1020));
			list.AddRange(Enumerable.Range(36866, 609));
			list.AddRange(Enumerable.Range(37655, 546));
			list.AddRange(Enumerable.Range(40001, 840));
			list.AddRange(Enumerable.Range(41122, 672));
			list.AddRange(Enumerable.Range(41796, 707));
			list.AddRange(Enumerable.Range(42511, 677));
			list.AddRange(Enumerable.Range(43442, 879));
			list.AddRange(Enumerable.Range(45055, 623));
			list.AddRange(Enumerable.Range(45967, 1032));
			list.AddRange(Enumerable.Range(47002, 555));
			list.AddRange(Enumerable.Range(48620, 531));
			this.Ports = new HashSet<int>(list);
			this.services = new Dictionary<int, ServiceInfo>();
			this.services_by_fullname = new Dictionary<string, SortedList<int, ServiceInfo>>();
		}

		public ServiceInfo Register(string fullName, Guid guid, Guid moduleVersionId, int gameCode, int serverCode)
		{
			int num = PortScanner.ScanOne(this.Address, this.Ports, Settings.Default.LocationPortMin);
			if (num == 0)
			{
				return null;
			}
			ServiceInfo serviceInfo = new ServiceInfo
			{
				ID = Peer.CurrentPeer.Handle.ToInt32(),
				EndPoint = new IPEndPoint(this.Address, num),
				FullName = fullName,
				GUID = guid,
				ModuleVersionId = moduleVersionId,
				GameCode = gameCode,
				ServerCode = serverCode
			};
			foreach (byte b in from service in this.services.Values
			where service.FullName == fullName
			orderby service.Suffix
			select service.Suffix)
			{
				if (serviceInfo.Suffix != b)
				{
					break;
				}
				ServiceInfo serviceInfo2 = serviceInfo;
				serviceInfo2.Suffix += 1;
			}
			int localServiceOrder = 0;
			this.InsertServiceInfo(serviceInfo, out localServiceOrder);
			serviceInfo.LocalServiceOrder = localServiceOrder;
			this.Ports.Remove(serviceInfo.EndPoint.Port);
			return serviceInfo;
		}

		public bool Unregister(int port, out ServiceInfo info)
		{
			if (!this.RemoveServiceInfo(port, out info))
			{
				return false;
			}
			if (!this.Ports.Contains(port))
			{
				this.Ports.Add(port);
			}
			else
			{
				Log<HostInfo>.Logger.Error("포트 무결성 깨짐");
			}
			return true;
		}

		private int GetEmptySlot(string fullname)
		{
			int num = 1;
			if (this.services_by_fullname.ContainsKey(fullname))
			{
				SortedList<int, ServiceInfo> sortedList = this.services_by_fullname[fullname];
				while (sortedList.ContainsKey(num) && HostInfo.MAX_SPIN_INDEX >= num)
				{
					num++;
				}
			}
			return num;
		}

		private void InsertServiceInfo(ServiceInfo service, out int localServiceOrder)
		{
			this.services[service.EndPoint.Port] = service;
			if (!this.services_by_fullname.ContainsKey(service.FullName))
			{
				this.services_by_fullname[service.FullName] = new SortedList<int, ServiceInfo>();
			}
			SortedList<int, ServiceInfo> sortedList = this.services_by_fullname[service.FullName];
			localServiceOrder = this.GetEmptySlot(service.FullName);
			if (sortedList.ContainsKey(localServiceOrder))
			{
				Log<HostInfo>.Logger.ErrorFormat("cannot execute indexed_services.Add() function. duplicated key [ {0} {1} ]", service.FullName, localServiceOrder);
				return;
			}
			sortedList.Add(localServiceOrder, service);
		}

		private bool RemoveServiceInfo(int port, out ServiceInfo service)
		{
			if (!this.services.TryGetValue(port, out service))
			{
				return false;
			}
			this.services.Remove(port);
			if (this.services_by_fullname.ContainsKey(service.FullName))
			{
				SortedList<int, ServiceInfo> sortedList = this.services_by_fullname[service.FullName];
				sortedList.Remove(service.LocalServiceOrder);
			}
			return true;
		}

		private static readonly int MAX_SPIN_INDEX = 100;

		private Dictionary<int, ServiceInfo> services;

		private Dictionary<string, SortedList<int, ServiceInfo>> services_by_fullname;
	}
}
