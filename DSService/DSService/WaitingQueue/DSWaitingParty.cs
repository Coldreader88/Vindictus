using System;
using System.Collections.Generic;
using ServiceCore.DSServiceOperations;

namespace DSService.WaitingQueue
{
	public class DSWaitingParty
	{
		public DSWaitingQueue DSWaitingQueue { get; set; }

		public Dictionary<long, DSPlayer> Members { get; set; }

		public int? Order { get; set; }

		public DateTime RegisteredTime { get; set; }

		public LinkedListNode<DSWaitingParty> Node { get; set; }

		public string QuestID { get; set; }

		public long MicroPlayID { get; set; }

		public long PartyID { get; set; }

		public bool IsGiantRaid { get; set; }

		public bool IsAdultMode { get; set; }

		public DSWaitingParty(DSWaitingQueue parent, List<DSPlayerInfo> list, string questID, long microPlayID, long partyID, bool isGiantRaid, bool isAdultMode)
		{
			this.DSWaitingQueue = parent;
			this.Members = new Dictionary<long, DSPlayer>();
			this.Order = null;
			this.RegisteredTime = DateTime.UtcNow;
			this.QuestID = questID;
			this.MicroPlayID = microPlayID;
			this.PartyID = partyID;
			this.IsGiantRaid = isGiantRaid;
			this.IsAdultMode = isAdultMode;
			foreach (DSPlayerInfo dsplayerInfo in list)
			{
				this.Members.Add(dsplayerInfo.CID, new DSPlayer(this, dsplayerInfo.CID, dsplayerInfo.FID, dsplayerInfo.Level, this.IsGiantRaid));
			}
		}

		public void RemovePlayer(DSPlayer player)
		{
			this.Members.Remove(player.CID);
			player.WaitingParty = null;
		}

		public void Clear()
		{
			foreach (KeyValuePair<long, DSPlayer> keyValuePair in this.Members)
			{
				keyValuePair.Value.WaitingParty = null;
			}
			this.Members.Clear();
		}

		public void UpdateOrder(int order)
		{
			if (this.Order != order)
			{
				this.Order = new int?(order);
			}
		}

		public void Sync()
		{
			foreach (DSPlayer dsplayer in this.Members.Values)
			{
				dsplayer.SyncDSStatus();
			}
		}

		public override string ToString()
		{
			return new string('o', this.Members.Count);
		}
	}
}
