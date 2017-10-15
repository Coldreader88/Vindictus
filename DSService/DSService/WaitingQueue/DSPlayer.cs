using System;
using Devcat.Core;
using ServiceCore;
using ServiceCore.EndPointNetwork;
using ServiceCore.EndPointNetwork.DS;
using ServiceCore.FrontendServiceOperations;
using UnifiedNetwork.Entity;
using Utility;

namespace DSService.WaitingQueue
{
	public class DSPlayer
	{
		public DSWaitingQueue DSWaitingQueue { get; set; }

		public DSWaitingParty WaitingParty { get; set; }

		public DSShip Ship { get; set; }

		public long CID { get; set; }

		public long FID { get; set; }

		public int Level { get; set; }

		public DateTime RegisteredTime { get; set; }

		public IEntityProxy FrontendConn { get; set; }

		public DSPlayerStatus Status { get; set; }

		public bool IsGiantRaid { get; set; }

		public DSPlayer(DSWaitingParty parent, long cid, long fid, int level, bool isGiantRaid)
		{
			this.DSWaitingQueue = parent.DSWaitingQueue;
			this.WaitingParty = parent;
			this.Status = DSPlayerStatus.Waiting;
			this.CID = cid;
			this.FID = fid;
			this.Level = level;
			this.IsGiantRaid = isGiantRaid;
			if (this.FID > 0L)
			{
				this.FrontendConn = DSService.Instance.Connect(DSService.Instance.DSServiceEntity, new Location(this.FID, "FrontendServiceCore.FrontendService"));
				this.FrontendConn.Closed += delegate(object _, EventArgs<IEntityProxy> __)
				{
					this.DSWaitingQueue.Parent.Unregister(this.CID, false);
				};
				return;
			}
			this.FrontendConn = null;
		}

		public void SendMessage<T>(T message) where T : IMessage
		{
			if (this.FrontendConn == null)
			{
				Log<DSPlayer>.Logger.InfoFormat("[to {0}] {1}", this.CID, message);
				return;
			}
			this.FrontendConn.RequestOperation(SendPacket.Create<T>(message));
		}

		public void SyncDSStatus()
		{
			if (this.WaitingParty != null)
			{
				int orderCount = this.WaitingParty.Order ?? (FeatureMatrix.GetInteger("DSWaitingMax") + 1);
				this.SendMessage<DSPlayerStatusMessage>(new DSPlayerStatusMessage(this.WaitingParty.QuestID, this.Status, this.WaitingParty.RegisteredTime, orderCount, this.IsGiantRaid));
				return;
			}
			if (this.Ship == null)
			{
				this.SendMessage<DSPlayerStatusMessage>(new DSPlayerStatusMessage("", this.Status, "", this.IsGiantRaid));
				return;
			}
			if (this.Status == DSPlayerStatus.ShipWaitingMember)
			{
				this.SendMessage<DSPlayerStatusMessage>(new DSPlayerStatusMessage(this.Ship.QuestID, this.Status, this.Ship.Players.Count, this.Ship.PartyID, this.IsGiantRaid));
				return;
			}
			this.SendMessage<DSPlayerStatusMessage>(new DSPlayerStatusMessage(this.Ship.QuestID, this.Status, this.Ship.PartyID, this.IsGiantRaid));
		}

		public void RegisterShip(DSShip ship)
		{
			this.WaitingParty = null;
			this.Ship = ship;
			this.Status = ((ship.ShipState == DSShipState.Launched) ? ((this.IsGiantRaid && this.Ship.Players.Count < 16) ? DSPlayerStatus.ShipWaitingMember : DSPlayerStatus.ShipReady) : DSPlayerStatus.ShipLaunching);
			this.SyncDSStatus();
		}

		public void EnterShip()
		{
			this.Status = DSPlayerStatus.InShip;
			this.SyncDSStatus();
		}
	}
}
