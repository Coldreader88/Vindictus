using System;
using System.Collections.Generic;
using System.Linq;
using Devcat.Core;
using Devcat.Core.Net.Message;
using Devcat.Core.Threading;
using HeroesChannelServer.Message;
using MMOServer;
using ServiceCore.CharacterServiceOperations;
using ServiceCore.EndPointNetwork;

namespace HeroesChannelServer
{
	public class Player : ICamera
	{
		public long ID { get; set; }

		public long WatchingPartition { get; set; }

		public Channel Channel { get; set; }

		internal ActionSync RecentAction { get; private set; }

		internal CharacterSummary RecentLook { get; private set; }

		public JobProcessor BoundThread
		{
			get
			{
				return this.boundThread;
			}
		}

		public event EventHandler<EventArgs<Packet>> SendMessage;

		public event EventHandler<EventArgs<ActionSync>> ActionUpdate;

		public event EventHandler<EventArgs<CharacterSummary>> LookUpdate;

		public Player(long id, Channel channel, JobProcessor thread)
		{
			this.ID = id;
			this.Channel = channel;
			this.boundThread = thread;
		}

		public void Enter(long pid, CharacterSummary look, ActionSync action)
		{
			if (this.boundThread != null && JobProcessor.Current != this.boundThread)
			{
				this.boundThread.Enqueue(Job.Create<long, CharacterSummary, ActionSync>(new Action<long, CharacterSummary, ActionSync>(this.Enter), pid, look, action));
				return;
			}
			this.RecentLook = look;
			this.Channel.AddComponent(this.Channel.MakeLocation(this.ID, 1, pid));
			this.Channel.AddComponent(new Look(this.ID, look));
			this.Channel.AddComponent(new Kinetics(this.ID, action));
			this.Channel.AddObserver(pid, this);
		}

		public void Move(long pid)
		{
			if (this.boundThread != null && JobProcessor.Current != this.boundThread)
			{
				this.boundThread.Enqueue(Job.Create<long>(new Action<long>(this.Move), pid));
				return;
			}
			this.Channel.ApplyModifier(this.Channel.MakeMove(this.ID, pid));
			this.Channel.MoveObserver(pid, this);
		}

		public void Action(ActionSync action)
		{
			if (this.boundThread != null && JobProcessor.Current != this.boundThread)
			{
				this.boundThread.Enqueue(Job.Create<ActionSync>(new Action<ActionSync>(this.Action), action));
				return;
			}
			this.Channel.ApplyModifier(new KineticsChanged(this.ID, action));
			this.RecentAction = action;
			if (this.ActionUpdate != null)
			{
				this.ActionUpdate(this, new EventArgs<ActionSync>(action));
			}
		}

		public void UpdateLook(CharacterSummary summary)
		{
			if (this.boundThread != null && JobProcessor.Current != this.boundThread)
			{
				this.boundThread.Enqueue(Job.Create<CharacterSummary>(new Action<CharacterSummary>(this.UpdateLook), summary));
				return;
			}
			this.Channel.ApplyModifier(new UpdateLook(this.ID, summary));
			this.RecentLook = summary;
			if (this.LookUpdate != null)
			{
				this.LookUpdate(this, new EventArgs<CharacterSummary>(summary));
			}
		}

		public void Chat(string msg)
		{
			if (this.boundThread != null && JobProcessor.Current != this.boundThread)
			{
				this.boundThread.Enqueue(Job.Create<string>(new Action<string>(this.Chat), msg));
				return;
			}
			string chatter = "";
			if (this.RecentLook != null)
			{
				chatter = this.RecentLook.CharacterID;
			}
			this.Channel.AddComponent(new Chat(this.ID, chatter, msg));
		}

		private void IssueSendMessage(Packet msg)
		{
			if (this.SendMessage != null)
			{
				this.SendMessage(this, new EventArgs<Packet>(msg));
			}
		}

		public void BroadCastChat(string message)
		{
			if (this.boundThread != null && JobProcessor.Current != this.boundThread)
			{
				this.boundThread.Enqueue(Job.Create<string>(new Action<string>(this.BroadCastChat), message));
				return;
			}
			string chatter = "";
			if (this.RecentLook != null)
			{
				chatter = this.RecentLook.CharacterID;
			}
			this.Channel.Broadcast(new Megaphone(this.ID, chatter, message).AppearMessage());
		}

		public void BroadCastHotSpringInfo(ICollection<HotSpringPotionEffectInfo> hotSpringPotionEffectInfos, int townID)
		{
			if (this.boundThread != null && JobProcessor.Current != this.boundThread)
			{
				this.boundThread.Enqueue(Job.Create<ICollection<HotSpringPotionEffectInfo>, int>(new Action<ICollection<HotSpringPotionEffectInfo>, int>(this.BroadCastHotSpringInfo), hotSpringPotionEffectInfos, townID));
				return;
			}
			this.Channel.Broadcast(new HotSpringInfo(hotSpringPotionEffectInfos, townID).AppearMessage());
		}

		public void Update(MMOServer.IMessage message)
		{
			if (message == null)
			{
				return;
			}
			if (message is PacketMessage)
			{
				PacketMessage packetMessage = message as PacketMessage;
				this.IssueSendMessage(packetMessage.Data);
				return;
			}
			if (message is MMOServer.Disappeared)
			{
				MMOServer.Disappeared disappeared = message as MMOServer.Disappeared;
				this.IssueSendMessage(SerializeWriter.ToBinary<ServiceCore.EndPointNetwork.Disappeared>(new ServiceCore.EndPointNetwork.Disappeared
				{
					ID = disappeared.ID
				}));
			}
		}

		public void ProcessMessage(ServiceCore.EndPointNetwork.IMessage message)
		{
			if (message == null)
			{
				return;
			}
			if (message is MovePartition)
			{
				MovePartition movePartition = message as MovePartition;
				this.Move(movePartition.TargetPartitionID);
				return;
			}
			if (message is UpdateAction)
			{
				UpdateAction updateAction = message as UpdateAction;
				this.Action(updateAction.Data);
				return;
			}
			if (message is Chat)
			{
				Chat chat = message as Chat;
				this.Chat(chat.Message);
			}
		}

		public void Update(IEnumerable<MMOServer.IMessage> messages)
		{
			foreach (MMOServer.IMessage message in messages)
			{
				this.Update(message);
			}
		}

		public void LeaveChannel()
		{
			if (this.boundThread != null && JobProcessor.Current != this.boundThread)
			{
				this.boundThread.Enqueue(Job.Create(new Action(this.LeaveChannel)));
				return;
			}
			this.Channel.RemoveObserver(this);
			List<IComponent> list = this.Channel.Space.FindByID(this.ID).ToList<IComponent>();
			foreach (IComponent component in list)
			{
				this.Channel.RemoveComponent(component);
			}
		}

		public void Flush()
		{
		}

		private JobProcessor boundThread;
	}
}
