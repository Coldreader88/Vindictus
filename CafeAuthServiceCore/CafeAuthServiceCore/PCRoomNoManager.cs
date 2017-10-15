using System;
using System.Collections.Generic;
using ServiceCore.FrontendServiceOperations;
using Utility;

namespace CafeAuthServiceCore
{
	public class PCRoomNoManager
	{
		public int TotalEntityCount
		{
			get
			{
				return this.PCRoomNoDic.Count;
			}
		}

		public bool AddUser(int pcRoomNo, CafeAuth user)
		{
			if (pcRoomNo <= 0 || user == null)
			{
				return false;
			}
			if (this.PCRoomNoDic.ContainsValue(user))
			{
				return false;
			}
			if (this.PCRoomNoDic.Add(pcRoomNo, user))
			{
				Log<CafeAuthService>.Logger.InfoFormat("[PCRoom] AddUser is complete. Total: {0}, After {1}'s Count: {2}", this.TotalEntityCount, pcRoomNo, this.PCRoomNoDic[pcRoomNo].Count);
				this.BroadCastUpdate(pcRoomNo, this.PCRoomNoDic[pcRoomNo].Count);
				return true;
			}
			Log<CafeAuthService>.Logger.InfoFormat("[PCRoom] AddUser is failed. Total: {0}, RoomNo: {1}", this.TotalEntityCount, pcRoomNo);
			return false;
		}

		public bool RemoveUser(CafeAuth targetUser)
		{
			if (targetUser == null)
			{
				return false;
			}
			if (!this.PCRoomNoDic.ContainsValue(targetUser))
			{
				return false;
			}
			foreach (KeyValuePair<int, CafeAuth> keyValuePair in this.PCRoomNoDic)
			{
				if (keyValuePair.Value == null)
				{
					return false;
				}
				if (keyValuePair.Value.NexonID == targetUser.NexonID)
				{
					this.PCRoomNoDic.Remove(keyValuePair.Key, targetUser);
					this.BroadCastUpdate(keyValuePair.Key, this.PCRoomNoDic[keyValuePair.Key].Count);
					return true;
				}
			}
			return false;
		}

		public bool RemoveUser(int pcRoomNo, CafeAuth user)
		{
			if (pcRoomNo <= 0 || user == null)
			{
				return false;
			}
			if (!this.PCRoomNoDic.ContainsValue(user))
			{
				return false;
			}
			if (this.PCRoomNoDic.Remove(pcRoomNo, user))
			{
				Log<CafeAuthService>.Logger.InfoFormat("[PCRoom] AddUser is complete. Total: {0}, After {1}'s Count: {2}", this.TotalEntityCount, pcRoomNo, this.PCRoomNoDic[pcRoomNo].Count);
				this.BroadCastUpdate(pcRoomNo, this.PCRoomNoDic[pcRoomNo].Count);
				return true;
			}
			Log<CafeAuthService>.Logger.InfoFormat("[PCRoom] RemoveUser is failed. Total: {0}, RoomNo: {1}", this.TotalEntityCount, pcRoomNo);
			return false;
		}

		public void BroadCastUpdate(int pcRoomNo, int count)
		{
			if (this.PCRoomNoDic.ContainsKey(pcRoomNo) && this.PCRoomNoDic[pcRoomNo] != null)
			{
				foreach (CafeAuth cafeAuth in this.PCRoomNoDic[pcRoomNo])
				{
					if (cafeAuth != null && cafeAuth.FrontendConn != null)
					{
						UpdatePCRoomCount op = new UpdatePCRoomCount(pcRoomNo, count);
						cafeAuth.FrontendConn.RequestOperation(op);
					}
				}
			}
		}

		private MultiDictionary<int, CafeAuth> PCRoomNoDic = new MultiDictionary<int, CafeAuth>();
	}
}
