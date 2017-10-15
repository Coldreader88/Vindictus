using System;
using System.Collections.Generic;
using System.Linq;
using ServiceCore;
using ServiceCore.DSServiceOperations;
using ServiceCore.PvpServiceOperations;
using UnifiedNetwork.Cooperation;
using Utility;

namespace DSService.DSEntityMaker
{
	public class DSEntityMakerSystem : IDSEntityMakerSystem
	{
		public DSEntityMakerSystem(DSService parent)
		{
			this.NormalRaidResorcePoint = FeatureMatrix.GetInteger("DSNormalRaidResorcePoint");
			this.GaiantRaidResorcePoint = FeatureMatrix.GetInteger("DSGiantRaidResorcePoint");
			this.PvpResorcePoint = FeatureMatrix.GetInteger("DSPvpResorcePoint");
			this.DSServiceInfoDic = new Dictionary<int, DSServiceInfo>();
			this.DSEntityMakerList = new LinkedList<DSEntityMakerQueue>();
			this.Parent = parent;
			this.startID = 0;
			DSServiceInfo.SetDSResorcePoint(this.NormalRaidResorcePoint, this.GaiantRaidResorcePoint, this.PvpResorcePoint);
		}

		public void AddServiceInfo(int serviceID, int coreCount)
		{
			DSServiceInfo dsserviceInfo = new DSServiceInfo(serviceID, coreCount);
			this.startID = dsserviceInfo.Initialize(this.startID);
			this.DSServiceInfoDic.Add(serviceID, dsserviceInfo);
		}

		public void Enqueue(long id, DSType dsType)
		{
			Log<DSEntityMakerSystem>.Logger.InfoFormat("Enqueue Make DS Entity. DSType : [{0}]", dsType.ToString());
			this.DSEntityMakerList.AddLast(new DSEntityMakerQueue(id, dsType));
			this.Process();
		}

		public void Enqueue(long id, DSType dsType, int pvpServiceID)
		{
			Log<DSEntityMakerSystem>.Logger.InfoFormat("Enqueue Make PVP Entity. DSType : [{0}]  ServiceID : [{1}]", dsType.ToString(), pvpServiceID);
			this.DSEntityMakerList.AddLast(new DSEntityMakerQueue(id, dsType, pvpServiceID));
			this.Process();
		}

		public void Dequeue(long id, DSType dsType)
		{
			DSEntityMakerQueue dsentityMakerQueue = (from x in this.DSEntityMakerList
			where x.ID == id && x.DSType == dsType
			select x).FirstOrDefault<DSEntityMakerQueue>();
			if (dsentityMakerQueue != null)
			{
				this.DSEntityMakerList.Remove(dsentityMakerQueue);
				Log<DSEntityMakerSystem>.Logger.InfoFormat("Remove Entity Maker Queue : {0}", dsType);
			}
		}

        public void Process()
        {
            List<DSEntityMakerQueue> dSEntityMakerQueues = new List<DSEntityMakerQueue>();
            Dictionary<int, int> nums = new Dictionary<int, int>();
            foreach (DSEntityMakerQueue dSEntityMakerList in this.DSEntityMakerList)
            {
                int serviceID = 0;
                int num = -1;
                DSType dSType = dSEntityMakerList.DSType;
                DSServiceInfo dSServiceInfo = null;
                bool flag = false;
                List<DSServiceInfo> list = (
                    from x in this.DSServiceInfoDic.Values
                    where x.HasEntity(dSType)
                    select x).ToList<DSServiceInfo>();
                List<DSServiceInfo> dSServiceInfos = (
                    from x in this.DSServiceInfoDic.Values
                    where x.CheckResource(dSType)
                    select x).ToList<DSServiceInfo>();
                List<DSServiceInfo> list1 = (
                    from x in this.DSServiceInfoDic.Values
                    where x.HasUnuseEntity(dSType)
                    select x).ToList<DSServiceInfo>();
                if ((
                    from x in this.DSServiceInfoDic
                    where x.Value.useFlag
                    select x).Count<KeyValuePair<int, DSServiceInfo>>() == this.DSServiceInfoDic.Count)
                {
                    foreach (KeyValuePair<int, DSServiceInfo> dSServiceInfoDic in this.DSServiceInfoDic)
                    {
                        dSServiceInfoDic.Value.useFlag = false;
                    }
                }
                if (list.Count > 0)
                {
                    dSServiceInfo = (
                        from x in list
                        orderby x.Resource descending
                        select x).First<DSServiceInfo>();
                    serviceID = dSServiceInfo.ServiceID;
                    Log<DSEntityMakerSystem>.Logger.InfoFormat("Find DS Entity!! ServiceID : [{0}]", serviceID);
                    num = dSServiceInfo.UseEntityAndGetDSID(dSType);
                }
                else if (dSServiceInfos.Count <= 0)
                {
                    if (list1.Count <= 0)
                    {
                        continue;
                    }
                    dSServiceInfo = (
                        from x in list1
                        orderby x.Resource descending
                        select x).First<DSServiceInfo>();
                    List<int>.Enumerator enumerator = dSServiceInfo.GetDestroyCadidateEntity(dSType).GetEnumerator();
                    try
                    {
                        while (enumerator.MoveNext())
                        {
                            int current = enumerator.Current;
                            nums.Add(current, dSServiceInfo.ServiceID);
                            Log<DSEntityMakerSystem>.Logger.InfoFormat("Delete Entity ServiceID : [{0}] EntityID : [{1}]", serviceID, current);
                        }
                        continue;
                    }
                    finally
                    {
                        ((IDisposable)enumerator).Dispose();
                    }
                }
                else
                {
                    dSServiceInfo = ((
                        from x in dSServiceInfos
                        where !x.useFlag
                        select x).Count<DSServiceInfo>() <= 0 ? (
                        from x in dSServiceInfos
                        orderby x.Resource descending
                        select x).First<DSServiceInfo>() : (
                        from x in dSServiceInfos
                        where !x.useFlag
                        orderby x.Resource descending
                        select x).First<DSServiceInfo>());
                    serviceID = dSServiceInfo.ServiceID;
                    Log<DSEntityMakerSystem>.Logger.InfoFormat("Find DS Service!! ServiceID : [{0}]", serviceID);
                    num = dSServiceInfo.UseResourceAndGetDSID(dSType);
                    flag = true;
                }
                this.Parent.DSWaitingSystem.DSStorage.DSMap.Add(num, new DSInfo(num, serviceID, dSType));
                dSEntityMakerQueues.Add(dSEntityMakerList);
                if (flag)
                {
                    MakeDSEntity makeDSEntity = new MakeDSEntity(num);
                    makeDSEntity.OnComplete += new Action<Operation>((Operation _) =>
                    {
                        if (dSEntityMakerList.DSType != DSType.Pvp)
                        {
                            this.Parent.DSWaitingSystem.ProcessAll();
                            return;
                        }
                        UpdatePvpDSInfo updatePvpDSInfo = new UpdatePvpDSInfo(num, new DSInfo(num, serviceID, dSType));
                        this.Parent.RequestOperation(dSEntityMakerList.PVPServiceID, updatePvpDSInfo);
                        this.DSServiceInfoDic[serviceID].EntityInfoDic[num].TargetPvpServiceID = dSEntityMakerList.PVPServiceID;
                    });
                    this.Parent.RequestOperation(serviceID, makeDSEntity);
                }
                else if (dSEntityMakerList.DSType != DSType.Pvp)
                {
                    this.Parent.DSWaitingSystem.ProcessAll();
                }
                else
                {
                    UpdatePvpDSInfo updatePvpDSInfo1 = new UpdatePvpDSInfo(num, new DSInfo(num, serviceID, dSType));
                    this.Parent.RequestOperation(dSEntityMakerList.PVPServiceID, updatePvpDSInfo1);
                    this.DSServiceInfoDic[serviceID].EntityInfoDic[num].TargetPvpServiceID = dSEntityMakerList.PVPServiceID;
                }
            }
            foreach (KeyValuePair<int, int> keyValuePair in nums)
            {
                DSLog.AddLog(keyValuePair.Key, "", (long)-1, -1, "RemoveDSEntity", "");
                RemoveDSEntity removeDSEntity = new RemoveDSEntity(keyValuePair.Key);
                this.Parent.RequestOperation(keyValuePair.Value, removeDSEntity);
            }
            foreach (DSEntityMakerQueue dSEntityMakerQueue in dSEntityMakerQueues)
            {
                this.DSEntityMakerList.Remove(dSEntityMakerQueue);
            }
        }

        public void UnuseEntity(int serviceID, int dsID)
		{
			Log<DSEntityMakerSystem>.Logger.InfoFormat("Entity Unused - ServiceID : [{0}]  DSID : [{1}]", serviceID, dsID);
			if (this.DSServiceInfoDic[serviceID].EntityInfoDic[dsID].DSType == DSType.Pvp)
			{
				UpdatePvpDSInfo updatePvpDSInfo = new UpdatePvpDSInfo(dsID, null);
				updatePvpDSInfo.OnComplete += delegate(Operation _)
				{
					this.DSServiceInfoDic[serviceID].UnuseEntity(dsID);
					this.Parent.DSWaitingSystem.DSStorage.DSMap.Remove(dsID);
					this.Process();
				};
				this.Parent.RequestOperation(this.DSServiceInfoDic[serviceID].EntityInfoDic[dsID].TargetPvpServiceID, updatePvpDSInfo);
				return;
			}
			this.DSServiceInfoDic[serviceID].UnuseEntity(dsID);
			this.Parent.DSWaitingSystem.DSStorage.DSMap.Remove(dsID);
			this.Process();
		}

		public void CloseEntity(int serviceID, int dsID)
		{
			Log<DSEntityMakerSystem>.Logger.InfoFormat("Entity Close - ServiceID : [{0}]  DSID : [{1}]", serviceID, dsID);
			if (this.DSServiceInfoDic[serviceID].EntityInfoDic[dsID].DSType == DSType.Pvp)
			{
				UpdatePvpDSInfo updatePvpDSInfo = new UpdatePvpDSInfo(dsID, null);
				updatePvpDSInfo.OnComplete += delegate(Operation _)
				{
					this.DSServiceInfoDic[serviceID].UnuseResource(dsID);
					if (this.Parent.DSWaitingSystem.DSStorage.DSMap.TryGetValue(dsID) != null)
					{
						this.Parent.DSWaitingSystem.DSStorage.DSMap.Remove(dsID);
					}
					this.Process();
				};
				this.Parent.RequestOperation(this.DSServiceInfoDic[serviceID].EntityInfoDic[dsID].TargetPvpServiceID, updatePvpDSInfo);
				return;
			}
			this.DSServiceInfoDic[serviceID].UnuseResource(dsID);
			if (this.Parent.DSWaitingSystem.DSStorage.DSMap.TryGetValue(dsID) != null)
			{
				this.Parent.DSWaitingSystem.DSStorage.DSMap.Remove(dsID);
			}
			this.Process();
		}

		public override string ToString()
		{
			string text = "";
			foreach (KeyValuePair<int, DSServiceInfo> keyValuePair in this.DSServiceInfoDic)
			{
				text = text + "\n" + keyValuePair.ToString() + "\n";
			}
			text += "\n\nENTITY MAKER QUEUE \n";
			foreach (DSEntityMakerQueue dsentityMakerQueue in this.DSEntityMakerList)
			{
				object obj = text;
				text = string.Concat(new object[]
				{
					obj,
					"\t[",
					dsentityMakerQueue.ID,
					"] ",
					dsentityMakerQueue.DSType,
					"\n"
				});
			}
			return text;
		}

		private int NormalRaidResorcePoint;

		private int GaiantRaidResorcePoint;

		private int PvpResorcePoint;

		private Dictionary<int, DSServiceInfo> DSServiceInfoDic;

		private LinkedList<DSEntityMakerQueue> DSEntityMakerList;

		private DSService Parent;

		private int startID;
	}
}
