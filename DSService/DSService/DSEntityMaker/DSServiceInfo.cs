using System;
using System.Collections.Generic;
using System.Linq;
using ServiceCore.DSServiceOperations;
using Utility;

namespace DSService.DSEntityMaker
{
	public class DSServiceInfo
	{
		public static int DSNormalRaidResorcePoint { get; set; }

		public static int DSGiantRaidResorcePoint { get; set; }

		public static int DSPvpResorcePoint { get; set; }

		public int Resource { get; set; }

		public int ServiceID { get; set; }

		public Dictionary<int, DSEntityInfo> EntityInfoDic { get; set; }

		public bool useFlag { get; set; }

		public DSServiceInfo(int serviceID, int coreCount)
		{
			this.useFlag = false;
			this.ServiceID = serviceID;
			this.Resource = coreCount * 2 - 1;
			this.EntityInfoDic = new Dictionary<int, DSEntityInfo>();
		}

		public static void SetDSResorcePoint(int dsNormal, int dsGiant, int pvp)
		{
			DSServiceInfo.DSNormalRaidResorcePoint = dsNormal;
			DSServiceInfo.DSGiantRaidResorcePoint = dsGiant;
			DSServiceInfo.DSPvpResorcePoint = pvp;
		}

		public int Initialize(int startID)
		{
			for (int i = 0; i < this.Resource; i++)
			{
				this.EntityInfoDic.Add(startID + i, new DSEntityInfo());
				Log<DSServiceInfo>.Logger.WarnFormat("DSServiceInfo Initialize. Entity[{0}]", startID + i);
			}
			return startID + this.Resource;
		}

		private void UseResource(DSType dsType)
		{
			Log<DSServiceInfo>.Logger.WarnFormat("UseResource ServiceID : [{0}], DSType : [{1}]", this.ServiceID, dsType.ToString());
			this.Resource -= this.GetResorcePoint(dsType);
			this.useFlag = true;
			Log<DSServiceInfo>.Logger.WarnFormat("RemainResource : [{0}]", this.Resource);
		}

		private void UnuseResource(DSType dsType)
		{
			Log<DSServiceInfo>.Logger.WarnFormat("UnUseResource ServiceID : [{0}], DSType : [{1}]", this.ServiceID, dsType.ToString());
			this.Resource += this.GetResorcePoint(dsType);
			Log<DSServiceInfo>.Logger.WarnFormat("RemainResource : [{0}]", this.Resource);
		}

		public bool HasEntity(DSType dsType)
		{
			return (from x in this.EntityInfoDic
			where x.Value.State == DSEntityState.UnUsed && x.Value.DSType == dsType
			select x).Count<KeyValuePair<int, DSEntityInfo>>() > 0;
		}

		public int UseEntityAndGetDSID(DSType dsType)
		{
			KeyValuePair<int, DSEntityInfo> keyValuePair = (from x in this.EntityInfoDic
			where x.Value.State == DSEntityState.UnUsed && x.Value.DSType == dsType
			select x).First<KeyValuePair<int, DSEntityInfo>>();
			keyValuePair.Value.State = DSEntityState.Used;
			return keyValuePair.Key;
		}

		public void UnuseEntity(int dsID)
		{
			DSEntityInfo dsentityInfo = this.EntityInfoDic[dsID];
			this.EntityInfoDic[dsID].State = DSEntityState.UnUsed;
		}

		public bool CheckResource(DSType dsType)
		{
			return this.Resource - this.GetResorcePoint(dsType) >= 0;
		}

		public int UseResourceAndGetDSID(DSType dsType)
		{
			this.UseResource(dsType);
			KeyValuePair<int, DSEntityInfo> keyValuePair = (from x in this.EntityInfoDic
			where x.Value.State == DSEntityState.UnUsed && x.Value.DSType == DSType.None
			select x).First<KeyValuePair<int, DSEntityInfo>>();
			keyValuePair.Value.Update(DSEntityState.Used, dsType);
			Log<DSEntityMakerSystem>.Logger.InfoFormat("Update DS Entity [{0}] [{1}] [Used]", keyValuePair.Key, dsType);
			return keyValuePair.Key;
		}

		public bool HasUnuseEntity(DSType dsType)
		{
			int num = this.GetResorcePoint(dsType) - this.Resource;
			foreach (KeyValuePair<int, DSEntityInfo> keyValuePair in this.EntityInfoDic)
			{
				if (keyValuePair.Value.State == DSEntityState.UnUsed)
				{
					num -= this.GetResorcePoint(keyValuePair.Value.DSType);
					if (num <= 0)
					{
						return true;
					}
				}
			}
			return false;
		}

		public List<int> GetDestroyCadidateEntity(DSType dsType)
		{
			List<int> list = new List<int>();
			int num = this.GetResorcePoint(dsType);
			foreach (KeyValuePair<int, DSEntityInfo> keyValuePair in this.EntityInfoDic)
			{
				if (keyValuePair.Value.State == DSEntityState.UnUsed)
				{
					num -= this.GetResorcePoint(keyValuePair.Value.DSType);
					list.Add(keyValuePair.Key);
					keyValuePair.Value.State = DSEntityState.Reserved;
					if (num <= 0)
					{
						return list;
					}
				}
			}
			return null;
		}

		public void UnuseResource(int dsID)
		{
			DSEntityInfo dsentityInfo = this.EntityInfoDic[dsID];
			this.UnuseResource(dsentityInfo.DSType);
			if (dsentityInfo.DSType != DSType.None)
			{
				Log<DSEntityMakerSystem>.Logger.WarnFormat("Reset DS Entity [{0}]", dsID);
				dsentityInfo.Reset();
			}
		}

		private int GetResorcePoint(DSType dsType)
		{
			switch (dsType)
			{
			case DSType.GiantRaid:
				return DSServiceInfo.DSGiantRaidResorcePoint;
			case DSType.NormalRaid:
			case DSType.IsolateNormalRaid:
				return DSServiceInfo.DSNormalRaidResorcePoint;
			case DSType.Pvp:
				return DSServiceInfo.DSPvpResorcePoint;
			}
			return 0;
		}

		public override string ToString()
		{
			string text = string.Concat(new object[]
			{
				"Service ID : ",
				this.ServiceID,
				"\tResource : ",
				this.Resource,
				"\n"
			});
			foreach (KeyValuePair<int, DSEntityInfo> keyValuePair in this.EntityInfoDic)
			{
				text = text + keyValuePair.ToString() + "\n";
			}
			return text;
		}
	}
}
