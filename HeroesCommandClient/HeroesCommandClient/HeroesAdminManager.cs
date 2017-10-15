using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using AdminClientServiceCore.Messages;
using Devcat.Core;

namespace HeroesCommandClient
{
	internal class HeroesAdminManager
	{
		public event EventHandler<EventArgs<string>> ServerGroupConnected;

		public event EventHandler<EventArgs<string>> ServerGroupDisconnected;

		public event EventHandler<EventArgs<string>> ServerGroupUserCounted;

		public event EventHandler<EventArgs<string>> ServerGroupNotified;

		public HeroesAdminPeer this[string name]
		{
			get
			{
				if (this.peers.ContainsKey(name))
				{
					return this.peers[name];
				}
				return this.dummy;
			}
		}

		public string ServerString
		{
			get
			{
				string text = string.Empty;
				foreach (HeroesAdminPeer heroesAdminPeer in this.peers.Values)
				{
					if (heroesAdminPeer.IsConnected)
					{
						text = text + heroesAdminPeer.Name + ";";
					}
				}
				return text;
			}
		}

		public HeroesAdminManager(RCServiceClient _client)
		{
			this.peers = new Dictionary<string, HeroesAdminPeer>();
			this.client = _client;
			this.client.ServerGroupAdded += this.ServerGroupAdded;
			this.client.ServerGroupActivated += this.ServerGroupActivated;
			this.client.ServerGroupRemoved += this.ServerGroupRemoved;
		}

		public bool SendMessage<T>(string server, T message)
		{
			if (this.peers.ContainsKey(server))
			{
				HeroesAdminPeer heroesAdminPeer = this.peers[server];
				if (heroesAdminPeer.IsConnected)
				{
					this.peers[server].SendMessage<T>(message);
					return true;
				}
			}
			return false;
		}

		private void ServerGroupAdded(object sender, EventArgs<string> arg)
		{
			IPEndPoint ipfromServerGroup = this.client.GetIPFromServerGroup(arg.Value);
			if (ipfromServerGroup != null)
			{
				HeroesAdminPeer heroesAdminPeer = new HeroesAdminPeer(arg.Value, ipfromServerGroup);
				heroesAdminPeer.ConnectionSucceed += this.OnServerGroupConnected;
				heroesAdminPeer.ConnectionFailed += new EventHandler<EventArgs<Exception>>(this.OnServerGroupConnectionFailed);
				heroesAdminPeer.Disconnected += this.OnServerGroupDisconnected;
				heroesAdminPeer.UserCounted += this.OnServerGroupUserCounted;
				heroesAdminPeer.Notified += this.OnServerGroupNotified;
				heroesAdminPeer.AutoReconnect = true;
				this.peers.Add(arg.Value, heroesAdminPeer);
				return;
			}
			throw new Exception(string.Format("ServerGroup {0} IP's not found", arg.Value));
		}

		private void ServerGroupRemoved(object sender, EventArgs<string> arg)
		{
			if (this.peers.ContainsKey(arg.Value))
			{
				HeroesAdminPeer heroesAdminPeer = this.peers[arg.Value];
				heroesAdminPeer.ConnectionSucceed -= this.OnServerGroupConnected;
				heroesAdminPeer.ConnectionFailed -= new EventHandler<EventArgs<Exception>>(this.OnServerGroupConnectionFailed);
				heroesAdminPeer.Disconnected -= this.OnServerGroupDisconnected;
				heroesAdminPeer.UserCounted -= this.OnServerGroupUserCounted;
				heroesAdminPeer.Notified -= this.OnServerGroupNotified;
				this.peers.Remove(arg.Value);
				heroesAdminPeer.Stop();
			}
		}

		private void ServerGroupActivated(object sender, EventArgs<string> arg)
		{
			if (this.peers.ContainsKey(arg.Value))
			{
				HeroesAdminPeer heroesAdminPeer = this.peers[arg.Value];
				if (!heroesAdminPeer.IsConnected)
				{
					heroesAdminPeer.Start();
				}
			}
		}

		private void OnServerGroupConnected(object sender, EventArgs arg)
		{
			HeroesAdminPeer heroesAdminPeer = sender as HeroesAdminPeer;
			if (this.ServerGroupConnected != null)
			{
				this.ServerGroupConnected(sender, new EventArgs<string>(heroesAdminPeer.Name));
			}
		}

		private void OnServerGroupConnectionFailed(object sender, EventArgs arg)
		{
			HeroesAdminPeer heroesAdminPeer = sender as HeroesAdminPeer;
			if (this.ServerGroupDisconnected != null)
			{
				this.ServerGroupDisconnected(sender, new EventArgs<string>(heroesAdminPeer.Name));
			}
		}

		private void OnServerGroupDisconnected(object sender, EventArgs arg)
		{
			HeroesAdminPeer heroesAdminPeer = sender as HeroesAdminPeer;
			if (this.ServerGroupDisconnected != null)
			{
				this.ServerGroupDisconnected(sender, new EventArgs<string>(heroesAdminPeer.Name));
			}
		}

		private void OnServerGroupUserCounted(object sender, EventArgs<AdminReportClientCountMessage2> args)
		{
			HeroesAdminPeer heroesAdminPeer = sender as HeroesAdminPeer;
			StringBuilder stringBuilder = new StringBuilder("uc_stat ");
			foreach (KeyValuePair<string, Dictionary<string, int>> keyValuePair in args.Value.UserCount)
			{
				stringBuilder.AppendFormat("{0}-{1}:", heroesAdminPeer.Name, keyValuePair.Key);
				foreach (KeyValuePair<string, int> keyValuePair2 in keyValuePair.Value)
				{
					stringBuilder.AppendFormat("{0}({1});", keyValuePair2.Key, keyValuePair2.Value);
				}
				stringBuilder.Append("/");
			}
			if (this.ServerGroupUserCounted != null)
			{
				this.ServerGroupUserCounted(sender, new EventArgs<string>(stringBuilder.ToString()));
			}
		}

        private void OnServerGroupNotified(object sender, EventArgs<AdminReportNotifyMessage> args)
        {
            HeroesAdminPeer peer = sender as HeroesAdminPeer;
            StringBuilder builder = new StringBuilder();
            builder.AppendFormat("{0} {1} ", peer.Name, args.Value.Message);
            switch (args.Value.Code)
            {
                case NotifyCode.ERROR:
                    builder.Insert(0, "[Error] ");
                    builder.AppendLine();
                    break;

                case NotifyCode.SUCCESS:
                    builder.Append("Success ");
                    builder.AppendLine();
                    break;

                default:
                    builder.AppendLine();
                    break;
            }
            if (this.ServerGroupNotified != null)
            {
                this.ServerGroupNotified(sender, new EventArgs<string>(builder.ToString()));
            }
        }

        private RCServiceClient client;

		private Dictionary<string, HeroesAdminPeer> peers;

		private HeroesAdminPeer dummy = new HeroesAdminPeer("dummy", new IPEndPoint(IPAddress.Parse("127.0.0.1"), 0));
	}
}
