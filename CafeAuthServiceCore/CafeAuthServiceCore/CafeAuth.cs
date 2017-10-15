using System;
using System.Net;
using System.Text;
using Nexon.CafeAuth;
using ServiceCore;
using UnifiedNetwork.Cooperation;
using UnifiedNetwork.Entity;
using Utility;

namespace CafeAuthServiceCore
{
	public class CafeAuth
	{
		public CafeAuthService Service { get; set; }

		public IEntity Entity { get; set; }

		public IEntityProxy FrontendConn { get; set; }

		internal AsyncResultSync LoginSync { get; set; }

		internal int NexonSN { get; set; }

		internal string NexonID { get; set; }

		internal string CharacterID { get; set; }

		internal IPAddress LocalAddress { get; set; }

		internal bool CanTry { get; set; }

		internal bool IsTrial { get; set; }

		internal MachineID MID { get; set; }

		internal int GameRoomClient { get; set; }

		internal IPAddress RemoteAddress { get; set; }

		internal long SessionNo { get; set; }

		internal long JournalID { get; set; }

		public CafeAuth(CafeAuthService service, IEntity entity)
		{
			this.Service = service;
			this.Entity = entity;
			this.Entity.Closed += this.Entity_Closed;
		}

		private void Entity_Closed(object sender, EventArgs e)
		{
			if (this.Service.PCRoomManager != null)
			{
				this.Service.PCRoomManager.RemoveUser(this);
			}
			if (this.valid)
			{
				if (this.Service.NxIDToEntityDic.ContainsKey(this.NexonID))
				{
					this.Service.NxIDToEntityDic.Remove(this.NexonID);
				}
				if (this.Service.SessionDic.ContainsKey(this.SessionNo))
				{
					this.Service.SessionDic.Remove(this.SessionNo);
				}
				this.Service.Logout(this.SessionNo, this.NexonID, this.CharacterID, this.RemoteAddress, this.CanTry);
				this.ReportCafeAuthLogout();
			}
			try
			{
				EntityDataContext entityDataContext = new EntityDataContext();
				entityDataContext.AcquireService(new long?((sender as IEntity).ID), this.Service.Category, new int?(-1), new int?(this.Service.ID));
			}
			catch (Exception ex)
			{
				Log<CafeAuth>.Logger.ErrorFormat("Entity_Closed [EntityID : {0}] [ServiceID : {1}] [Category : {2}] - {3} ", new object[]
				{
					(sender as IEntity).ID,
					this.Service.ID,
					this.Service.Category,
					ex
				});
			}
		}

		public void ForceClose()
		{
			this.Entity_Closed(this.Entity, new EventArgs());
			this.Entity.Close();
		}

		internal void SetLoginInformation(int nexonSN, string nexonID, string characterID, IPAddress localAddress, IPAddress remoteAddress, bool canTry, bool isTrial, MachineID machineID, int gameRoomClient, byte channelCode)
		{
			if (FeatureMatrix.IsEnable("NaverChanneling"))
			{
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.AppendFormat("{0};{1}", channelCode, nexonID);
				this.NexonID = stringBuilder.ToString();
			}
			else
			{
				this.NexonID = nexonID;
			}
			this.NexonSN = nexonSN;
			this.CharacterID = characterID;
			this.LocalAddress = localAddress;
			this.RemoteAddress = remoteAddress;
			this.CanTry = canTry;
			this.IsTrial = isTrial;
			this.MID = machineID;
			this.GameRoomClient = gameRoomClient;
			this.SessionNo = -1L;
			this.NexonID_forLog = nexonID;
			try
			{
				using (LogDBDataContext logDBDataContext = new LogDBDataContext())
				{
					long? num = new long?(-1L);
					logDBDataContext.AddLoginJournal(ref num);
					this.JournalID = num.Value;
				}
			}
			catch (Exception ex)
			{
				Log<CafeAuth>.Logger.Error(ex);
			}
		}

		internal void BeginLogin()
		{
			this.valid = true;
			CafeAuth cafeAuth;
			if (this.Service.NxIDToEntityDic.TryGetValue(this.NexonID, out cafeAuth))
			{
				cafeAuth.ForceClose();
			}
			this.Service.NxIDToEntityDic.Add(this.NexonID, this);
			this.LoginSync = this.Service.BeginLogin(this.NexonID, this.CharacterID, this.LocalAddress, this.RemoteAddress, this.CanTry, this.IsTrial, this.MID, this.GameRoomClient, this);
		}

		internal CafeAuthResult EndLogin()
		{
			IAsyncResult asyncResult = this.LoginSync.AsyncResult;
			return this.Service.EndLogin(asyncResult);
		}

		internal void SetValid()
		{
			this.valid = true;
		}

		internal void ReportCafeAuthLogin()
		{
			try
			{
				using (LogDBDataContext logDBDataContext = new LogDBDataContext())
				{
					logDBDataContext.AddLoginLedger(new long?(this.JournalID), this.NexonID_forLog, this.SessionNo.ToString(), "CafeAuth/Login");
				}
			}
			catch (Exception ex)
			{
				Log<CafeAuth>.Logger.Error("Error while Making Logs.", ex);
			}
		}

		internal void ReportCafeAuthLogout()
		{
			try
			{
				using (LogDBDataContext logDBDataContext = new LogDBDataContext())
				{
					logDBDataContext.AddLoginLedger(new long?(this.JournalID), this.NexonID_forLog, this.SessionNo.ToString(), "CafeAuth/Logout");
				}
			}
			catch (Exception ex)
			{
				Log<CafeAuth>.Logger.Error("Error while Making Logs.", ex);
			}
		}

		internal void ReportCafeAuthMessage(string message)
		{
			try
			{
				using (LogDBDataContext logDBDataContext = new LogDBDataContext())
				{
					logDBDataContext.AddLoginLedger(new long?(this.JournalID), this.NexonID_forLog, this.SessionNo.ToString(), message);
				}
			}
			catch (Exception ex)
			{
				Log<CafeAuth>.Logger.Error("Error while Making Logs.", ex);
			}
		}

		private string NexonID_forLog;

		private bool valid;
	}
}
