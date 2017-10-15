using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DSService.WaitingQueue.Connection;
using ServiceCore;
using ServiceCore.DSServiceOperations;
using ServiceCore.EndPointNetwork.DS;
using ServiceCore.FrontendServiceOperations;
using ServiceCore.HeroesContents;
using Utility;

namespace DSService.WaitingQueue
{
	public class DSWaitingSystem
	{
		public Dictionary<string, DSWaitingQueue> QueueDict { get; set; }

		public Dictionary<long, DSPlayer> DSPlayerDict { get; set; }

		public DSStorage DSStorage { get; set; }

		public IExternalOperation ExternalOperation { get; set; }

		public DSWaitingSystem()
		{
			this.QueueDict = new Dictionary<string, DSWaitingQueue>();
			this.DSPlayerDict = new Dictionary<long, DSPlayer>();
			this.DSStorage = new DSStorage();
			this.ExternalOperation = new ExternalService();
			try
			{
				string @string = ServiceCore.FeatureMatrix.GetString("DSQuestSetting");
				foreach (string text in from q in @string.Split(new char[]
				{
					','
				})
				where q != ""
				select q)
				{
					this.AddWaitingQueue(text.Trim(), -1, true);
				}
				if (ServiceCore.FeatureMatrix.IsEnable("DSNormalRaid"))
				{
					this.AddWaitingQueue("normal", -1, false);
					this.AddWaitingQueue("isolate", -1, false);
				}
			}
			catch (Exception ex)
			{
				Log<DSWaitingSystem>.Logger.Error("exception occurred while registering quest", ex);
			}
		}

		public void AddWaitingQueue(string quest, int shipSize, bool isGiantRaid)
		{
			if (this.QueueDict.ContainsKey(quest))
			{
				return;
			}
			QuestInfo questInfo = DSContents.GetQuestInfo(quest);
			int levelConstraint = 0;
			if (isGiantRaid)
			{
				levelConstraint = DSContents.GetLevelConstraint(quest);
			}
			else
			{
				questInfo = new QuestInfo();
				questInfo.ID = quest;
				questInfo.MaxMemberCount = 10;
				isGiantRaid = false;
			}
			if (questInfo == null)
			{
				Log<DSWaitingSystem>.Logger.ErrorFormat("{0} is invalid quest", quest);
				return;
			}
			if (shipSize <= 0)
			{
				shipSize = questInfo.MaxMemberCount;
			}
			this.QueueDict.Add(questInfo.ID, new DSWaitingQueue(this, questInfo.ID, shipSize, levelConstraint, isGiantRaid));
			Log<DSWaitingSystem>.Logger.InfoFormat("{0} registered to DS", quest);
		}

		public void RemoveWaitingQueue(string quest)
		{
			DSWaitingQueue dswaitingQueue = this.QueueDict.TryGetValue(quest);
			if (dswaitingQueue != null)
			{
				dswaitingQueue.Clear();
				this.QueueDict.Remove(quest);
			}
		}

		public RegisterDSPartyResult Register(string quest, List<DSPlayerInfo> partyInfo, long microPlayID, long partyID, bool isGiantRaid, bool isAdultMode)
		{
			DSLog.AddLog(-1, quest, -1L, -1, "Register", partyInfo.Count.ToString());
			string key = isGiantRaid ? quest : (this.DSStorage.CheckIsolateNormalQuest(quest) ? "isolate" : "normal");
			DSWaitingQueue dswaitingQueue = this.QueueDict.TryGetValue(key);
			if (dswaitingQueue == null)
			{
				Log<DSWaitingSystem>.Logger.InfoFormat("[RegisterFail NoSuchQuest] quest:{0} microPlayID:{1} partyID:{2} partyCount:{3}", new object[]
				{
					quest,
					microPlayID,
					partyID,
					partyInfo.Count
				});
				return RegisterDSPartyResult.NoSuchQuest;
			}
			foreach (DSPlayerInfo dsplayerInfo in partyInfo)
			{
				if (this.DSPlayerDict.ContainsKey(dsplayerInfo.CID))
				{
					Log<DSWaitingSystem>.Logger.InfoFormat("[RegisterFail AlreadyInQueue] quest:{0} microPlayID:{1} partyID:{2} partyCount:{3} cid:{4}", new object[]
					{
						quest,
						microPlayID,
						partyID,
						partyInfo.Count,
						dsplayerInfo.CID
					});
					return RegisterDSPartyResult.AlreadyInQueue;
				}
			}
			DSWaitingParty dswaitingParty = new DSWaitingParty(dswaitingQueue, partyInfo, quest, microPlayID, partyID, isGiantRaid, isAdultMode);
			foreach (DSPlayer dsplayer in dswaitingParty.Members.Values)
			{
				this.DSPlayerDict.Add(dsplayer.CID, dsplayer);
			}
			if (!dswaitingQueue.RegisterParty(dswaitingParty))
			{
				foreach (DSPlayer dsplayer2 in dswaitingParty.Members.Values)
				{
					this.DSPlayerDict.Remove(dsplayer2.CID);
					Log<DSWaitingSystem>.Logger.InfoFormat("[RegisterFail CannotJoinQuest] quest:{0} microPlayID:{1} partyID:{2} partyCount:{3} cid:{4}", new object[]
					{
						quest,
						microPlayID,
						partyID,
						partyInfo.Count,
						dsplayer2.CID
					});
				}
				dswaitingParty.Clear();
				return RegisterDSPartyResult.CannotJoinQuest;
			}
			return RegisterDSPartyResult.Success;
		}

		public void Unregister(long cid, bool byUser)
		{
			DSPlayer dsplayer = this.DSPlayerDict.TryGetValue(cid);
			if (dsplayer != null)
			{
				if (byUser && !dsplayer.IsGiantRaid)
				{
					if (dsplayer.Ship != null && dsplayer.Ship.ShipState >= DSShipState.Launching)
					{
						Log<DSWaitingSystem>.Logger.WarnFormat("Cannot unregister user after launching: - {0}", cid);
						return;
					}
					if (dsplayer.FrontendConn != null)
					{
						CancelStartGame op = new CancelStartGame();
						dsplayer.FrontendConn.RequestOperation(op);
					}
				}
				DSLog.AddLog(-1, dsplayer.DSWaitingQueue.QuestID, -1L, -1, "Unregister", cid.ToString());
				dsplayer.DSWaitingQueue.UnregisterPlayer(dsplayer);
				this.DSPlayerDict.Remove(cid);
			}
		}

		public void DSShipLaunched(int dsID, long partyID)
		{
			DSInfo dsinfo = this.DSStorage.DSMap.TryGetValue(dsID);
			DSLog.AddLog(dsinfo.DSID, dsinfo.GetDSShip().DSWaitingQueue.QuestID, partyID, -1, "DSShipLaunched", "");
			dsinfo.GetDSShip().Launched(partyID);
		}

		public void DSShipUpdated(int dsID, DSGameState state)
		{
			DSInfo dsinfo = this.DSStorage.DSMap.TryGetValue(dsID);
			if (dsinfo != null && dsinfo.GetDSShip() != null)
			{
				if (state == DSGameState.GameStarted)
				{
					dsinfo.GetDSShip().StartTime = new DateTime?(DateTime.Now);
				}
				DSLog.AddLog(dsinfo.DSID, dsinfo.GetDSShip().DSWaitingQueue.QuestID, dsinfo.GetDSShip().PartyID, -1, "DSShipUpdated", state.ToString());
				dsinfo.GetDSShip().GameState = state;
			}
		}

		public void DSShipSinked(int dsID, string failReason)
		{
			DSInfo dsinfo = this.DSStorage.DSMap.TryGetValue(dsID);
			if (dsinfo != null)
			{
				DSShip dsship = dsinfo.GetDSShip();
				if (dsship != null)
				{
					DSLog.AddLog(dsinfo.DSID, dsinfo.GetDSShip().DSWaitingQueue.QuestID, dsinfo.GetDSShip().PartyID, -1, "DSShipSinked", failReason);
					foreach (KeyValuePair<long, DSPlayer> keyValuePair in dsship.Players)
					{
						this.DSPlayerDict.Remove(keyValuePair.Key);
						keyValuePair.Value.SendMessage<DSPlayerStatusMessage>(new DSPlayerStatusMessage("", DSPlayerStatus.Unregistered, failReason, keyValuePair.Value.IsGiantRaid));
					}
					if (failReason != null)
					{
						Log<DSWaitingSystem>.Logger.ErrorFormat("DS Ship Sinked!!! : {0}", failReason);
					}
					dsship.DSWaitingQueue.SinkShip(dsship);
					if (ServiceCore.FeatureMatrix.IsEnable("DSDynamicLoad"))
					{
						return;
					}
					this.ProcessAll();
				}
			}
		}

		public void ProcessAll()
		{
			foreach (DSWaitingQueue dswaitingQueue in from q in this.QueueDict.Values
			orderby q.WaitingParties.Count descending
			select q)
			{
				dswaitingQueue.Process(dswaitingQueue.WaitingParties.First);
			}
		}

		public string GetDebugInfo()
		{
			StringBuilder stringBuilder = new StringBuilder();
			foreach (KeyValuePair<string, DSWaitingQueue> keyValuePair in this.QueueDict)
			{
				int num = keyValuePair.Value.WaitingParties.Count + keyValuePair.Value.Ships.Count;
				if (num > 0)
				{
					stringBuilder.AppendFormat(" {0}:{1}/{2}", keyValuePair.Value.DSType.ToString(), keyValuePair.Value.Ships.Count, keyValuePair.Value.WaitingParties.Count);
				}
			}
			return stringBuilder.ToString();
		}

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendLine();
			stringBuilder.AppendLine("=========players======");
			stringBuilder.AppendFormat(" {0} Players", this.DSPlayerDict.Count).AppendLine();
			stringBuilder.AppendLine("=========storage=======");
			stringBuilder.AppendLine(this.DSStorage.ToString());
			stringBuilder.AppendLine("=========queue========");
			foreach (KeyValuePair<string, DSWaitingQueue> keyValuePair in this.QueueDict)
			{
				stringBuilder.Append(keyValuePair.Key).AppendLine();
				stringBuilder.AppendLine(keyValuePair.Value.ToString());
			}
			return stringBuilder.ToString();
		}
	}
}
