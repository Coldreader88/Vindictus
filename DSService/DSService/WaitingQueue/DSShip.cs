using System;
using System.Collections.Generic;
using System.Text;
using ServiceCore.DSServiceOperations;
using ServiceCore.EndPointNetwork.DS;
using Utility;

namespace DSService.WaitingQueue
{
	public class DSShip
	{
		public DSWaitingQueue DSWaitingQueue { get; set; }

		public DSWaitingSystem DSWaitingSystem
		{
			get
			{
				return this.DSWaitingQueue.Parent;
			}
		}

		public Dictionary<long, DSPlayer> Players { get; private set; }

		public Func<DSShip, Dictionary<long, DSPlayer>, bool> IsEnterable { get; private set; }

		public DSInfo DSInfo { get; set; }

		public DSShipState ShipState { get; set; }

		public DSGameState GameState { get; set; }

		public LinkedListNode<DSShip> Node { get; set; }

		public long PartyID { get; set; }

		public DateTime? StartTime { get; set; }

		public string QuestID { get; set; }

		public long MicroPlayID { get; set; }

		public bool IsAdultMode { get; set; }

		public DSShip(DSWaitingQueue queue, DSInfo ds)
		{
			this.DSWaitingQueue = queue;
			this.Players = new Dictionary<long, DSPlayer>();
			this.ShipState = DSShipState.Initial;
			this.GameState = DSGameState.Initial;
			this.DSInfo = ds;
			ds.SetDSShip(this);
			this.IsEnterable = DSShip_EnterFunc.GetEnterFunc(queue.QuestID);
			this.StartTime = null;
			this.IsAdultMode = false;
		}

		public bool HasEmptySlot
		{
			get
			{
				return this.DSWaitingQueue.ShipSize > this.Players.Count && this.GameState != DSGameState.BlockEntering && !this.IsTimePassed && (this.DSWaitingQueue.IsGiantRaid || (this.Players.Count == 0 && this.ShipState == DSShipState.Initial));
			}
		}

		public bool IsTimePassed
		{
			get
			{
				return (DateTime.Now - (this.StartTime ?? DateTime.Now)).TotalMinutes >= 15.0;
			}
		}

		public void SyncAllDSMemberStatus()
		{
			bool flag = this.DSWaitingQueue.IsGiantRaid && this.Players.Count < 16;
			foreach (DSPlayer dsplayer in this.Players.Values)
			{
				if (dsplayer.Status != DSPlayerStatus.InShip)
				{
					if (flag && this.GameState != DSGameState.GameStarted)
					{
						dsplayer.Status = DSPlayerStatus.ShipWaitingMember;
					}
					else
					{
						dsplayer.Status = DSPlayerStatus.ShipReady;
					}
					dsplayer.SyncDSStatus();
				}
			}
		}

		public bool TryEnterShip(DSWaitingParty party)
		{
			Dictionary<long, DSPlayer> members = party.Members;
			if (this.DSWaitingQueue.ShipSize < this.Players.Count + members.Count)
			{
				return false;
			}
			if (this.IsEnterable(this, members) && !this.IsTimePassed)
			{
				this.QuestID = party.QuestID;
				if (!this.DSWaitingQueue.IsGiantRaid)
				{
					this.MicroPlayID = party.MicroPlayID;
					this.PartyID = party.PartyID;
					this.IsAdultMode = party.IsAdultMode;
				}
				foreach (DSPlayer dsplayer in members.Values)
				{
					this.Players[dsplayer.CID] = dsplayer;
					dsplayer.RegisterShip(this);
				}
				if (this.ShipState == DSShipState.Launched)
				{
					this.SyncAllDSMemberStatus();
				}
				return true;
			}
			return false;
		}

		public void RemovePlayer(DSPlayer player)
		{
			this.Players.Remove(player.CID);
			player.Ship = null;
			this.SyncAllDSMemberStatus();
		}

		public void Clear()
		{
			foreach (DSPlayer dsplayer in this.Players.Values)
			{
				dsplayer.Ship = null;
			}
			this.Players.Clear();
		}

		public void Launch()
		{
			if (this.ShipState == DSShipState.Initial)
			{
				long frontendID = -1L;
				if (!this.DSWaitingQueue.IsGiantRaid)
				{
					using (Dictionary<long, DSPlayer>.ValueCollection.Enumerator enumerator = this.Players.Values.GetEnumerator())
					{
						if (enumerator.MoveNext())
						{
							DSPlayer dsplayer = enumerator.Current;
							frontendID = dsplayer.FID;
						}
					}
				}
				this.ShipState = DSShipState.Launching;
				this.DSWaitingSystem.ExternalOperation.LaunchShip(this.DSInfo.ServiceID, this.DSInfo.DSID, this.QuestID, this.MicroPlayID, this.PartyID, frontendID, this.DSWaitingQueue.IsGiantRaid, this.IsAdultMode, delegate(bool result)
				{
					if (!result)
					{
						this.ShipState = DSShipState.LaunchFail;
						Log<DSShip>.Logger.ErrorFormat("launch fail : {0}", this.DSInfo.DSID);
					}
				});
				return;
			}
			Log<DSShip>.Logger.ErrorFormat("launch fail : {0}", this.DSInfo.DSID);
		}

		public void Launched(long pid)
		{
			this.ShipState = DSShipState.Launched;
			this.PartyID = pid;
			this.SyncAllDSMemberStatus();
		}

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			Dictionary<DSPlayerStatus, int> dictionary = new Dictionary<DSPlayerStatus, int>();
			int num = 0;
			int num2 = 0;
			foreach (DSPlayer dsplayer in this.Players.Values)
			{
				dictionary.AddOrIncrease(dsplayer.Status, 1);
				if (dsplayer.Level >= 60)
				{
					num++;
				}
				else
				{
					num2++;
				}
			}
			stringBuilder.AppendFormat("<No.{0}> [high{1}/low{2}/{3}/{4}] ", new object[]
			{
				this.DSInfo.DSID,
				num,
				num2,
				this.ShipState,
				this.GameState
			});
			foreach (KeyValuePair<DSPlayerStatus, int> keyValuePair in dictionary)
			{
				stringBuilder.AppendFormat("{0}({1}) ", keyValuePair.Key, keyValuePair.Value);
			}
			return stringBuilder.ToString();
		}

		public string ToDetailString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendFormat("=============================", new object[0]).AppendLine();
			stringBuilder.AppendFormat("<{0}> ShipState.{1} GameState.{2}", this.DSInfo.DSID, this.ShipState, this.GameState).AppendLine();
			stringBuilder.AppendFormat("=============================", new object[0]).AppendLine();
			new Dictionary<DSPlayerStatus, int>();
			int num = 0;
			int num2 = 0;
			foreach (DSPlayer dsplayer in this.Players.Values)
			{
				stringBuilder.AppendFormat("{0}", dsplayer).AppendLine();
				if (dsplayer.Level >= 60)
				{
					num++;
				}
				else
				{
					num2++;
				}
			}
			stringBuilder.AppendFormat("=============================", new object[0]).AppendLine();
			stringBuilder.AppendFormat("{0}/{1}", num, num2).AppendLine();
			return stringBuilder.ToString();
		}
	}
}
