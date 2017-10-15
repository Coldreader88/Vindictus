using System;
using Devcat.Core;
using Devcat.Core.Threading;
using ServiceCore.EndPointNetwork;
using ServiceCore.EndPointNetwork.GuildService;
using ServiceCore.FrontendServiceOperations;
using ServiceCore.GuildServiceOperations;
using UnifiedNetwork.CacheSync;
using UnifiedNetwork.Cooperation;
using UnifiedNetwork.Entity;
using Utility;

namespace GuildService
{
	public class OnlineGuildMember : IGuildChatMember
	{
		public GuildEntity Parent { get; set; }

		public GuildMemberKey Key { get; set; }

		public long FID { get; set; }

		public string CharacterName
		{
			get
			{
				return this.Key.CharacterName;
			}
		}

		public string Sender
		{
			get
			{
				return this.Key.CharacterName;
			}
		}

		public IEntityProxy FrontendConn { get; set; }

		public IEntityProxy PlayerConn { get; set; }

		public IEntityProxy CashShopConn { get; set; }

		private ObservableCollection Observables { get; set; }

		public IObservable BriefGuildInfoObservable { get; set; }

		public bool IsGuildStorageListening { get; set; }

		public GuildMember GuildMember
		{
			get
			{
				return this.Parent.GetGuildMember(this.CharacterName);
			}
		}

		public long CID
		{
			get
			{
				return this.Key.CID;
			}
		}

		public long GuildID
		{
			get
			{
				return this.Parent.Entity.ID;
			}
		}

		public OnlineGuildMember(GuildEntity parent, GuildMemberKey key, long fid)
		{
			this.Parent = parent;
			this.Key = key;
			this.FID = fid;
			this.FrontendConn = GuildService.Instance.Connect(this.Parent.Entity, new Location
			{
				ID = fid,
				Category = "FrontendServiceCore.FrontendService"
			});
			this.PlayerConn = GuildService.Instance.Connect(this.Parent.Entity, new Location
			{
				ID = key.CID,
				Category = "PlayerService.PlayerService"
			});
			this.PlayerConn.Closed += delegate(object _, EventArgs<IEntityProxy> __)
			{
				Log<OnlineGuildMember>.Logger.WarnFormat("[{0}] Charater Logout Detected", this.Key.CharacterName);
				GuildService.Instance.LeaveChatRoom(this);
				this.Disconnect();
				this.Parent.Disconnect(this.Key);
				this.Parent.Sync();
			};
			this.CashShopConn = GuildService.Instance.Connect(this.Parent.Entity, new Location
			{
				ID = (long)key.NexonSN,
				Category = "CashShopService.CashShopService"
			});
			this.Observables = new ObservableCollection();
			this.BriefGuildInfoObservable = this.Observables.AddObservable(this.Key.CID, "BriefGuildInfo");
		}

		public void Disconnect()
		{
			if (!this.FrontendConn.IsClosed)
			{
				this.FrontendConn.Close();
			}
			if (!this.PlayerConn.IsClosed)
			{
				this.PlayerConn.Close();
			}
			Scheduler.Schedule(GuildService.Instance.Thread, Job.Create(delegate
			{
				if (!this.FrontendConn.IsClosed)
				{
					this.FrontendConn.Close(true);
				}
				if (!this.PlayerConn.IsClosed)
				{
					this.PlayerConn.Close(true);
				}
			}), 30000);
		}

		public void SendMessage<T>(T message) where T : IMessage
		{
			if (this.FrontendConn != null && !this.FrontendConn.IsClosed)
			{
				this.FrontendConn.RequestOperation(SendPacket.Create<T>(message));
			}
		}

		public void RequestFrontendOperation(Operation op)
		{
			if (this.FrontendConn != null && !this.FrontendConn.IsClosed)
			{
				this.FrontendConn.RequestOperation(op);
			}
		}

		public void RequestItemOperation(Operation op)
		{
			if (this.PlayerConn != null && !this.PlayerConn.IsClosed)
			{
				this.PlayerConn.RequestOperation(op);
			}
		}

		public void SendGuildStorageInfoMessage()
		{
			if (this.GuildMember.Rank > GuildMemberRank.Member)
			{
				return;
			}
			this.RequestFrontendOperation(SendPacket.Create<GuildInventoryInfoMessage>(this.Parent.Storage.InventoryMessage));
		}

		public void SendGuildStorageLogsMessage(bool sendTodayLog)
		{
			if (this.GuildMember.Rank > GuildMemberRank.Member)
			{
				return;
			}
			this.RequestFrontendOperation(SendPacket.Create<GuildStorageLogsMessage>(this.Parent.Storage.LogMessage(sendTodayLog)));
		}

		public void SendOperationFailedDialog(string errorCode)
		{
			this.RequestFrontendOperation(SendPacket.Create<SystemMessage>(new SystemMessage(SystemMessageCategory.Dialog, string.Format("GameUI_Heroes_{0}", errorCode))));
		}

		public void SendChatMessage(string sender, string chatMessage)
		{
			if (!this.GuildMember.Rank.IsRegularMember())
			{
				return;
			}
			GuildChatMessage message = new GuildChatMessage
			{
				Sender = sender,
				Message = chatMessage
			};
			this.SendMessage<GuildChatMessage>(message);
		}

		public void SendInfoMessage(string sender, bool isOnline)
		{
			if (!this.GuildMember.Rank.IsRegularMember())
			{
				return;
			}
			GuildChatMemberInfoMessage message = new GuildChatMemberInfoMessage
			{
				Sender = sender,
				IsOnline = isOnline
			};
			this.SendMessage<GuildChatMemberInfoMessage>(message);
		}
	}
}
