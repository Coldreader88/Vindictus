using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using Devcat.Core.Threading;
using UnifiedNetwork.LocationFree;
using UnifiedNetwork.LocationService;
using Utility;

namespace UnifiedNetwork.OperationService
{
	public class LookUp : IAsyncLookUp<string, IPEndPoint>, IAsyncLookUp<int, IPEndPoint>
	{
		public event Action<string, int> ServiceAdded;

		public event Action<string, int> ServiceRemoved;

		public IPEndPoint GetLocation(int id)
		{
			ServiceInfo serviceInfo;
			this.serviceIndex.TryGetValue(id, out serviceInfo);
			return serviceInfo.EndPoint;
		}

		public IPEndPoint GetRandomLocation(string category)
		{
			List<int> list = this.categoryIndex.ToList(category);
			if (list == null || list.Count == 0)
			{
				return null;
			}
			return this.GetLocation(list[this.locationRandom.Next(list.Count)]);
		}

		public int GetLocationCount(string category)
		{
			return this.categoryIndex.CountValues(category);
		}

		public ICollection<IPEndPoint> GetLocations(string category)
		{
			return (from x in this.categoryIndex[category].AsQueryable<int>()
			join y in this.serviceIndex on x equals y.Key
			select y.Value.EndPoint).ToList<IPEndPoint>();
		}

		public void AddLocation(ServiceInfo info)
		{
			this.serviceIndex[info.ID] = info;
			List<int> list = this.categoryIndex.ToList(info.FullName);
			if (list == null || !list.Contains(info.ID))
			{
				this.categoryIndex.Add(info.FullName, info.ID);
			}
			if (this.ServiceAdded != null)
			{
				this.ServiceAdded(info.FullName, info.ID);
			}
		}

		public bool RemoveLocation(int id, string category)
		{
			List<int> list = this.categoryIndex.ToList(category);
			if (list.Remove(id))
			{
				this.serviceIndex.Remove(id);
				this.categoryIndex.Remove(category, id);
				if (this.ServiceRemoved != null)
				{
					this.ServiceRemoved(category, id);
				}
				return true;
			}
			return false;
		}

		public IAsyncLookUp<string, IPEndPoint> BaseLookUp { get; set; }

		public void FindLocation(string category, Action<IPEndPoint> callback)
		{
			IPEndPoint randomLocation = this.GetRandomLocation(category);
			if (randomLocation != null)
			{
				JobProcessor.Current.Enqueue(Job.Create<IPEndPoint>(callback, randomLocation));
				return;
			}
			if (this.BaseLookUp != null)
			{
				this.BaseLookUp.FindLocation(category, callback);
				return;
			}
			JobProcessor.Current.Enqueue(Job.Create<IPEndPoint>(callback, null));
		}

		public ICollection<int> FindIndex(string category)
		{
			return this.categoryIndex.ToList(category);
		}

		public int GetFirstRegisteredServiceID(string category)
		{
			ServiceInfo serviceInfo = null;
			foreach (int key in this.categoryIndex.ToList(category))
			{
				ServiceInfo serviceInfo2;
				if (this.serviceIndex.TryGetValue(key, out serviceInfo2) && (serviceInfo == null || serviceInfo2.ServiceOrder < serviceInfo.ServiceOrder))
				{
					serviceInfo = serviceInfo2;
				}
			}
			return serviceInfo.ID;
		}

		public IPEndPoint FindByIndex(int index)
		{
			ServiceInfo serviceInfo;
			this.serviceIndex.TryGetValue(index, out serviceInfo);
			return serviceInfo.EndPoint;
		}

		IAsyncLookUp<int, IPEndPoint> IAsyncLookUp<int, IPEndPoint>.BaseLookUp
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

		public void FindLocation(int key, Action<IPEndPoint> callback)
		{
			ServiceInfo serviceInfo;
			if (this.serviceIndex.TryGetValue(key, out serviceInfo))
			{
				JobProcessor.Current.Enqueue(Job.Create<IPEndPoint>(callback, serviceInfo.EndPoint));
				return;
			}
			JobProcessor.Current.Enqueue(Job.Create<IPEndPoint>(callback, null));
		}

		public MultiDictionary<string, int> ReportLookUpInfo()
		{
			return this.categoryIndex;
		}

		public Dictionary<int, KeyValuePair<string, IPEndPoint>> ReportExtendedLookUpInfo()
		{
			Dictionary<int, KeyValuePair<string, IPEndPoint>> dictionary = new Dictionary<int, KeyValuePair<string, IPEndPoint>>();
			foreach (KeyValuePair<string, int> keyValuePair in this.categoryIndex)
			{
				ServiceInfo serviceInfo;
				if (this.serviceIndex.TryGetValue(keyValuePair.Value, out serviceInfo))
				{
					dictionary[keyValuePair.Value] = new KeyValuePair<string, IPEndPoint>(keyValuePair.Key, serviceInfo.EndPoint);
				}
			}
			return dictionary;
		}

		private Dictionary<int, ServiceInfo> serviceIndex = new Dictionary<int, ServiceInfo>();

		private MultiDictionary<string, int> categoryIndex = new MultiDictionary<string, int>();

		private Random locationRandom = new Random();
	}
}
