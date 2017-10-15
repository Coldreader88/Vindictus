using System;
using System.Collections.Generic;
using System.Text;
using ServiceCore;
using ServiceCore.DSServiceOperations;

namespace DSService.WaitingQueue
{
	public class DSWaitingQueue
	{
		public string QuestID { get; set; }

		public int ShipSize { get; set; }

		public int LevelConstraint { get; set; }

		public DSWaitingSystem Parent { get; set; }

		public LinkedList<DSWaitingParty> WaitingParties { get; set; }

		public LinkedList<DSShip> Ships { get; set; }

		public bool IsGiantRaid { get; set; }

		public DSType DSType
		{
			get
			{
				if (this.IsGiantRaid)
				{
					return DSType.GiantRaid;
				}
				if (!(this.QuestID == "isolate"))
				{
					return DSType.NormalRaid;
				}
				return DSType.IsolateNormalRaid;
			}
		}

		public DSWaitingQueue(DSWaitingSystem parent, string questID, int shipSize, int levelConstraint, bool isGiantRaid)
		{
			this.Parent = parent;
			this.QuestID = questID;
			this.ShipSize = shipSize;
			this.LevelConstraint = levelConstraint;
			this.WaitingParties = new LinkedList<DSWaitingParty>();
			this.Ships = new LinkedList<DSShip>();
			this.IsGiantRaid = isGiantRaid;
			this.ID = DateTime.Now.Ticks;
		}

		public void Clear()
		{
		}

		public bool RegisterParty(DSWaitingParty party)
		{
			if (this.IsGiantRaid)
			{
				if (party.Members.Count > this.ShipSize)
				{
					return false;
				}
				foreach (KeyValuePair<long, DSPlayer> keyValuePair in party.Members)
				{
					if (keyValuePair.Value.Level < this.LevelConstraint)
					{
						return false;
					}
				}
			}
			LinkedListNode<DSWaitingParty> linkedListNode = this.WaitingParties.AddLast(party);
			this.Process(linkedListNode);
			party.Node = linkedListNode;
			return true;
		}

		public void UnregisterPlayer(DSPlayer player)
		{
			if (player.WaitingParty != null)
			{
				DSWaitingParty waitingParty = player.WaitingParty;
				waitingParty.RemovePlayer(player);
				if (waitingParty.Members.Count == 0)
				{
					this.WaitingParties.Remove(waitingParty.Node);
					if (FeatureMatrix.IsEnable("DSDynamicLoad") && this.WaitingParties.Count == 0)
					{
						DSService.Instance.DSEntityMakerSystem.Dequeue(this.ID, this.DSType);
					}
				}
				this.Process(this.WaitingParties.First);
				return;
			}
			if (player.Ship != null)
			{
				DSShip ship = player.Ship;
				ship.RemovePlayer(player);
				if (ship.HasEmptySlot)
				{
					this.Process(this.WaitingParties.First);
				}
			}
		}

		public void SinkShip(DSShip ship)
		{
			ship.ShipState = DSShipState.Finished;
			this.Parent.DSStorage.DSShipMap.Remove(ship.DSInfo.DSID);
			ship.Clear();
			this.Ships.Remove(ship.Node);
		}

		public bool Step(ref LinkedListNode<DSWaitingParty> from)
		{
			if (this.WaitingParties.Count == 0 || from == null)
			{
				return false;
			}
			bool flag = true;
			foreach (DSShip dsship in this.Ships)
			{
				if (dsship.HasEmptySlot)
				{
					flag = false;
					break;
				}
			}
			if (flag)
			{
				DSInfo waitingDS = this.Parent.DSStorage.GetWaitingDS(this.DSType);
				if (waitingDS == null)
				{
					if (FeatureMatrix.IsEnable("DSDynamicLoad"))
					{
						DSService.Instance.DSEntityMakerSystem.Enqueue(this.ID, this.DSType);
					}
					return false;
				}
				DSShip dsship2 = new DSShip(this, waitingDS);
				dsship2.Node = this.Ships.AddLast(dsship2);
			}
			do
			{
				DSWaitingParty value = from.Value;
				from = from.Next;
				foreach (DSShip dsship3 in this.Ships)
				{
					if (dsship3.HasEmptySlot && dsship3.TryEnterShip(value))
					{
						value.Clear();
						this.WaitingParties.Remove(value);
						return true;
					}
				}
			}
			while (from != null);
			DSInfo waitingDS2 = this.Parent.DSStorage.GetWaitingDS(this.DSType);
			if (waitingDS2 == null)
			{
				return false;
			}
			DSShip dsship4 = new DSShip(this, waitingDS2);
			dsship4.Node = this.Ships.AddLast(dsship4);
			from = this.WaitingParties.First;
			return true;
		}

		public void Process(LinkedListNode<DSWaitingParty> from)
		{
			while (this.Step(ref from))
			{
			}
			foreach (DSShip dsship in this.Ships)
			{
				if (dsship.Players.Count != 0 && dsship.ShipState == DSShipState.Initial)
				{
					dsship.Launch();
				}
			}
			this.RefreshOrder();
		}

		public void RefreshOrder()
		{
			int integer = FeatureMatrix.GetInteger("DSWaitingMax");
			int num = 1;
			foreach (DSWaitingParty dswaitingParty in this.WaitingParties)
			{
				dswaitingParty.UpdateOrder(num);
				dswaitingParty.Sync();
				if (this.IsGiantRaid)
				{
					num += dswaitingParty.Members.Count;
				}
				else
				{
					num++;
				}
				if (num > integer)
				{
					break;
				}
			}
		}

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendFormat("<{0}>", this.QuestID).AppendLine();
			foreach (DSShip dsship in this.Ships)
			{
				stringBuilder.AppendFormat(dsship.ToString(), new object[0]).AppendLine();
			}
			stringBuilder.AppendFormat("Waiting : ", new object[0]);
			foreach (DSWaitingParty dswaitingParty in this.WaitingParties)
			{
				stringBuilder.AppendFormat(dswaitingParty.ToString(), new object[0]).Append(' ');
			}
			stringBuilder.AppendLine();
			return stringBuilder.ToString();
		}

		public long ID;
	}
}
