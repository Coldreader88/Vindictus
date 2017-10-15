using System;
using System.Collections.Generic;
using ServiceCore;
using Utility;

namespace MMOChannelService
{
	internal class LoadManager
	{
		public void UpdateLoad(int serviceID, long load)
		{
			this.loads[serviceID] = new LoadManager.LoadData
			{
				ServiceID = serviceID,
				LoadValue = load,
				SyncTime = DateTime.Now
			};
		}

		public int GetRecommendServiceID(long curLoad)
		{
			if (FeatureMatrix.IsEnable("MMOForceRecommend"))
			{
				return -1;
			}
			DateTime t = DateTime.Now.Subtract(new TimeSpan(0, 1, 0));
			if (curLoad < (long)LoadManager.RecommendLoad)
			{
				return -1;
			}
			if (curLoad < (long)LoadManager.MaxLoad)
			{
				foreach (KeyValuePair<int, LoadManager.LoadData> keyValuePair in this.loads)
				{
					if (!(keyValuePair.Value.SyncTime < t) && keyValuePair.Value.LoadValue < (long)LoadManager.RecommendLoad)
					{
						Log<LoadManager>.Logger.WarnFormat("recommend other service load {0} < {1}, current load {2}", keyValuePair.Value.LoadValue, LoadManager.RecommendLoad, curLoad);
						return keyValuePair.Key;
					}
				}
				return -1;
			}
			int result = -2;
			long num = 100L;
			foreach (KeyValuePair<int, LoadManager.LoadData> keyValuePair2 in this.loads)
			{
				if (!(keyValuePair2.Value.SyncTime < t) && keyValuePair2.Value.LoadValue < num)
				{
					result = keyValuePair2.Value.ServiceID;
					num = keyValuePair2.Value.LoadValue;
				}
			}
			if (num < (long)LoadManager.MaxLoad)
			{
				Log<LoadManager>.Logger.WarnFormat("recommend other service load {0}, current load {1}", num, curLoad);
				return result;
			}
			Log<LoadManager>.Logger.Warn("cannot find recommendable service");
			return -2;
		}

		public static readonly int RecommendLoad = 50;

		public static readonly int MaxLoad = 80;

		private Dictionary<int, LoadManager.LoadData> loads = new Dictionary<int, LoadManager.LoadData>();

		public struct LoadData
		{
			public int ServiceID;

			public long LoadValue;

			public DateTime SyncTime;
		}
	}
}
