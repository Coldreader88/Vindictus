using System;
using System.Linq;
using Devcat.Core;
using Devcat.Core.Net.Message;
using Devcat.Core.Threading;
using MMOServer;
using ServiceCore.CharacterServiceOperations;
using ServiceCore.EndPointNetwork;

namespace HeroesChannelServer.PerformanceTest
{
	internal class MockClient
	{
		public long ID { get; private set; }

		public Player Player { get; private set; }

		public long PartitionID { get; private set; }

		public CharacterSummary RecentLook { get; private set; }

		public ActionSync RecentAction { get; private set; }

		public Packet RecentSentPacket { get; private set; }

		public MockClient(long id, long initialPartitionID, PerformanceTestRunner parent)
		{
			this.ID = id;
			this.PartitionID = initialPartitionID;
            this.moveRandom = new Random((int)(DateTime.Now.Ticks + this.ID & (long)uint.MaxValue));
            this.Parent = parent;
		}

		public void EnterChannel(Channel channel)
		{
			if (this.Player != null)
			{
				this.Leave(this.Player.Channel);
			}
			this.Player = new Player(this.ID, channel, channel.Tag as JobProcessor);
            CharacterSummary characterSummary = new CharacterSummary(this.ID, (int)(this.ID & (long)uint.MaxValue), string.Format("Mock{0}", this.ID), (int)(this.ID & (long)uint.MaxValue), (BaseCharacter)(this.ID % 5L), (int)(this.ID % 70L), 0, 0, 0, 0, new CostumeInfo(), 0, 0, 0, "", 0, "", 1, false, false, false, false, 0, false, 0, VocationEnum.None, 0, 0, 0, 0, 0, 0, null, false, "", 0, 0, 0, 0);
            ActionSync actionSync = default(ActionSync);
			this.Player.Enter(this.PartitionID, characterSummary, actionSync);
			this.RecentLook = characterSummary;
			this.RecentAction = actionSync;
			this.Player.SendMessage += delegate(object sender, EventArgs<Packet> args)
			{
				this.Parent.ReceivedPacketCount++;
				this.RecentSentPacket = args.Value;
			};
			this.Player.ActionUpdate += delegate(object sender, EventArgs<ActionSync> args)
			{
				this.RecentAction = args.Value;
			};
			this.Player.LookUpdate += delegate(object sender, EventArgs<CharacterSummary> args)
			{
				this.RecentLook = args.Value;
			};
		}

		public void UpdateAction()
		{
			if (this.Player == null)
			{
				return;
			}
			this.RecentAction = default(ActionSync);
			this.Player.Action(this.RecentAction);
		}

		public void UpdateLook()
		{
			if (this.Player == null)
			{
				return;
			}
            this.RecentLook = new CharacterSummary(this.ID, (int)(this.ID & (long)uint.MaxValue), string.Format("Mock{0}", this.ID), (int)(this.ID & (long)uint.MaxValue), (BaseCharacter)(this.ID % 5L), (int)(this.ID % 70L), 0, 0, 0, 0, new CostumeInfo(), 0, 0, 0, "", 0, "", 0, false, false, false, false, 0, false, 0, VocationEnum.None, 0, 0, 0, 0, 0, 0, null, false, "", 0, 0, 0, 0);
            this.Player.UpdateLook(this.RecentLook);
		}

		public void PartitionMove()
		{
			if (this.Player == null)
			{
				return;
			}
			long[] array = this.Player.Channel.GetNeighborPartitions(this.PartitionID).ToArray<long>();
			long pid = array[this.moveRandom.Next(array.Length)];
			this.Player.Move(pid);
		}

		public bool Leave(Channel channel)
		{
			if (this.Player == null)
			{
				return false;
			}
			if (this.Player.Channel != channel)
			{
				return false;
			}
			this.Player.LeaveChannel();
			this.Player = null;
			return true;
		}

		private PerformanceTestRunner Parent;

		private Random moveRandom;
	}
}
