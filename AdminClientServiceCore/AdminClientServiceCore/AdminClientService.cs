using System;
using System.Collections.Generic;
using System.Linq;
using AdminClientServiceCore.Messages;
using AdminClientServiceCore.Properties;
using Devcat.Core.Net;
using Devcat.Core.Net.Message;
using Devcat.Core.Threading;
using ServiceCore;
using ServiceCore.CommonOperations;
using ServiceCore.Configuration;
using ServiceCore.DSServiceOperations;
using ServiceCore.MicroPlayServiceOperations;
using UnifiedNetwork.OperationService;
using Utility;

namespace AdminClientServiceCore
{
	public class AdminClientService : Service
	{
		public List<string> StoppedServices
		{
			get
			{
				return this.stoppedservices;
			}
			set
			{
				this.stoppedservices = value;
			}
		}

		public MessageHandlerFactory MF
		{
			get
			{
				return this.mf;
			}
		}

		public int[] FrontendServiceIDs()
		{
			return base.LookUp.ReportLookUpInfo()["FrontendServiceCore.FrontendService"].ToArray<int>();
		}

		public int[] ItemServiceIDs()
		{
			return this.PlayerServiceIDs();
		}

		public int[] PartyServiceIDs()
		{
			return base.LookUp.ReportLookUpInfo()["MicroPlayServiceCore.MicroPlayService"].ToArray<int>();
		}

		public int[] CashShopServiceIDs()
		{
			return base.LookUp.ReportLookUpInfo()["CashShopService.CashShopService"].ToArray<int>();
		}

		public int[] StoryServiceIDs()
		{
			return this.PlayerServiceIDs();
		}

		public int[] CharacterServiceIDs()
		{
			return this.PlayerServiceIDs();
		}

		public int[] GuildServiceIDs()
		{
			return base.LookUp.ReportLookUpInfo()["GuildService.GuildService"].ToArray<int>();
		}

		public int[] MicroPlayServiceIDs()
		{
			return base.LookUp.ReportLookUpInfo()["MicroPlayServiceCore.MicroPlayService"].ToArray<int>();
		}

		public int[] QuestOwnershipServiceIDs()
		{
			return this.PlayerServiceIDs();
		}

		public int[] DSServiceIDs()
		{
			return base.LookUp.ReportLookUpInfo()["DSService.DSService"].ToArray<int>();
		}

		public int[] HackShieldIDs()
		{
			return base.LookUp.ReportLookUpInfo()["HackShieldServiceCore.HackShieldService"].ToArray<int>();
		}

		public int[] LoginServiceIDs()
		{
			return base.LookUp.ReportLookUpInfo()["LoginServiceCore.LoginService"].ToArray<int>();
		}

		public int[] PVPServiceIDs()
		{
			return base.LookUp.ReportLookUpInfo()["PvpService.PvpService"].ToArray<int>();
		}

		public int[] PlayerServiceIDs()
		{
			return base.LookUp.ReportLookUpInfo()["PlayerService.PlayerService"].ToArray<int>();
		}

		public int[] UserDSHostServiceIDs()
		{
			return base.LookUp.ReportLookUpInfo()["UserDSHostService.UserDSHostService"].ToArray<int>();
		}

		public int[] TradeServiceIDs()
		{
			return base.LookUp.ReportLookUpInfo()["TradeService.TradeService"].ToArray<int>();
		}

		public int[] MMOChannelServiceIDs()
		{
			return base.LookUp.ReportLookUpInfo()["MMOChannelService.MMOChannelService"].ToArray<int>();
		}

		protected override void OnServiceAdded(string category, int serviceID)
		{
			base.OnServiceAdded(category, serviceID);
			if (AdminClientService.isFirstMicroPlay && category == "MicroPlayServiceCore.MicroPlayService")
			{
				this.RequestResetAllShipList();
				AdminClientService.isFirstMicroPlay = false;
			}
			if (FeatureMatrix.IsEnable("FeatureMatrixSyncService"))
			{
				EventDataContext.AddFeatureMatrixUpdate(serviceID, category);
			}
		}

		public override void Initialize(JobProcessor thread)
		{
			ConnectionStringLoader.LoadFromServiceCore(Settings.Default);
			AdminContents.Initialize();
			this.logger = new HeroesLogDataContext();
			base.Initialize(thread, null);
			base.RegisterMessage(AdminClientServiceOperationMessages.TypeConverters);
			this.acceptor.ClientAccept += this.server_ClientAccept;
			this.acceptor.Start(thread, (int)ServiceCoreSettings.Default.AdminClientServicePort);
			Log<AdminClientService>.Logger.Debug("Starts AdminClientService");
			this.mf.Register<AdminClientServicePeer>(AdminClientServiceOperationMessages.TypeConverters, "ProcessMessage");
			this.controller = new AdminClientServicePeer(this, null);
			EventDataContext.Initialize(thread);
		}

		private void server_ClientAccept(object sender, AcceptEventArgs e)
		{
			e.PacketAnalyzer = new MessageAnalyzer();
			e.Client.Disconnected += this.Client_Disconnected;
			this.peerlist.Add(new AdminClientServicePeer(this, e.Client));
			Log<AdminClientService>.Logger.Debug("Admin Client Connected.");
		}

		private void Client_Disconnected(object sender, EventArgs e)
		{
			Log<AdminClientService>.Logger.Debug("Admin Client Disconnected.");
			this.peerlist.Remove(sender as AdminClientServicePeer);
		}

		public void NotifyClientCount(int sum, int total, int waiting, Dictionary<string, int> states)
		{
			foreach (AdminClientServicePeer adminClientServicePeer in this.peerlist)
			{
				adminClientServicePeer.NotifyClientCount(sum, total, waiting, states);
			}
		}

		public void NotifyDSProcessInfo(DSReportMessage message)
		{
			foreach (AdminClientServicePeer adminClientServicePeer in this.peerlist)
			{
				adminClientServicePeer.NotifyDSProcessInfo(message);
			}
		}

		public void NotifyStoppedServices(List<string> target)
		{
			foreach (AdminClientServicePeer adminClientServicePeer in this.peerlist)
			{
				adminClientServicePeer.NotifyStoppedServices(target);
			}
		}

		public static Service StartService(string ip, string portstr)
		{
			AdminClientService.Instance = new AdminClientService();
			ServiceInvoker.StartService(ip, portstr, AdminClientService.Instance, false);
			return AdminClientService.Instance;
		}

		public void RequestResetAllShipList()
		{
			int[] array = this.MicroPlayServiceIDs();
			if (array.Length > 0)
			{
				base.RequestOperation(array[0], new ResetShipList());
			}
		}

		public void SetFreeTokenMode(bool on)
		{
			this.SetFreeTokenMode(on, null);
		}

		public void SetFreeTokenMode(bool on, AdminClientServicePeer peer)
		{
			int[] array = this.MicroPlayServiceIDs();
			FreeToken[] array2 = new FreeToken[array.Length];
			for (int i = 0; i < array.Length; i++)
			{
				array2[i] = new FreeToken();
				array2[i].On = on;
				base.RequestOperation(array[i], array2[i]);
			}
			if (peer != null)
			{
				peer.Transmit(SerializeWriter.ToBinary<AdminReportNotifyMessage>(new AdminReportNotifyMessage(NotifyCode.SUCCESS, string.Format("Set FreeToken Mode [{0}]", on))));
			}
		}

		public void SetDSCheat(int on)
		{
			int[] array = this.DSServiceIDs();
			for (int i = 0; i < array.Length; i++)
			{
				base.RequestOperation(array[i], new DSCheatToggle(on));
			}
		}

		public static bool IsAllServiceReady()
		{
			bool flag = Settings.Default.ignoreDSServiceReady;
			if (!flag)
			{
				string text = FeatureMatrix.GetString("DSQuestSetting") ?? "";
				if (text.Length == 0 || AdminClientService.Instance.DSServiceIDs().Length > 0)
				{
					flag = true;
				}
			}
			return AdminClientService.Instance.FrontendServiceIDs().Length > 0 && AdminClientService.Instance.MicroPlayServiceIDs().Length > 0 && AdminClientService.Instance.CharacterServiceIDs().Length > 0 && AdminClientService.Instance.ItemServiceIDs().Length > 0 && AdminClientService.Instance.StoryServiceIDs().Length > 0 && AdminClientService.Instance.QuestOwnershipServiceIDs().Length > 0 && flag;
		}

		public void UpdateFeatureMatrix(string feature, bool on, int targetServiceID)
		{
			if (feature != null && feature.Length > 0)
			{
				Dictionary<string, string> dictionary = new Dictionary<string, string>();
				char[] separator = new char[]
				{
					';'
				};
				string[] array = feature.Split(separator);
				for (int i = 0; i < array.Length; i++)
				{
					string text = array[i];
					string text2 = "0";
					int num = array[i].IndexOf('[');
					int num2 = array[i].IndexOf(']');
					if (num != -1 && num2 != -1 && num2 > num)
					{
						text = array[i].Substring(0, num);
						text2 = array[i].Substring(num + 1, num2 - num - 1);
					}
					if (text != null && text.Length > 0)
					{
						dictionary[text] = (on ? text2 : null);
					}
				}
				if (dictionary.Count > 0)
				{
					FeatureMatrix.OverrideFeature(dictionary);
					HashSet<string> exceptCategoryList = new HashSet<string>();
					exceptCategoryList.Add("UnifiedNetwork.LocationService.LocationService");
					exceptCategoryList.Add("AdminClientServiceCore.AdminClientService");
					IEnumerable<int> enumerable = from x in base.LookUp.ReportLookUpInfo()
					where !exceptCategoryList.Contains(x.Key)
					select x.Value;
					foreach (int num3 in enumerable)
					{
						if (targetServiceID == -1 || num3 == targetServiceID)
						{
							UpdateFeatureMatrix op = new UpdateFeatureMatrix(dictionary);
							base.RequestOperation(num3, op);
						}
					}
				}
			}
		}

		public void ProcessConsoleCommand(string cmd, string args, AdminClientServicePeer peer)
		{
			if (this._ProcessConsoleCommand(cmd, args, peer, -1))
			{
				peer.Transmit(SerializeWriter.ToBinary<AdminReportNotifyMessage>(new AdminReportNotifyMessage(NotifyCode.SUCCESS, string.Format("Console Command Executed ! - \"{0} {1}\"", cmd, args))));
				return;
			}
			if (peer != null)
			{
				peer.Transmit(SerializeWriter.ToBinary<AdminReportNotifyMessage>(new AdminReportNotifyMessage(NotifyCode.ERROR, string.Format("Console Command Failed ! - \"{0} {1}\"", cmd, args))));
			}
		}

		private bool _ProcessConsoleCommand(string cmd, string args, AdminClientServicePeer peer, int targetServiceID)
		{
			if (cmd == "event")
			{
				return this.ProcessEventCommand(args, peer, targetServiceID);
			}
			if (cmd == "freetoken")
			{
				if (args == "on")
				{
					this.SetFreeTokenMode(true, peer);
					return true;
				}
				if (args == "off")
				{
					this.SetFreeTokenMode(false, peer);
					return true;
				}
			}
			else
			{
				if (cmd.Contains("monster_") || cmd.Contains("droptable") || cmd.Contains("autofish") || cmd.Contains("sector_") || cmd.Contains("antisectorskip_"))
				{
					return this.ProcessMicroPlayCommand(cmd, args, targetServiceID);
				}
				if (cmd.Length >= 6 && cmd.Substring(0, 6) == "quest_")
				{
					return this.ProcessQuestCommand(cmd, args, targetServiceID);
				}
				if (cmd.Contains("manufacture_"))
				{
					return this.ProcessItemCommand(cmd, args, targetServiceID);
				}
				if (cmd.Contains("ds_"))
				{
					return this.ProcessDsCommand(cmd, args, peer);
				}
				if (cmd.Contains("store_fake_ingame_guild_info"))
				{
					return this.ProcessGuildCommand(cmd, args, peer);
				}
				if (cmd.Contains("guildwebchat_activate"))
				{
					return this.ProcessGuildCommand(cmd, args, peer);
				}
				if (cmd.Contains("goalevent_"))
				{
					bool flag = this.ProcessQuestCommand(cmd, args, targetServiceID);
					bool flag2 = this.ProcessMicroPlayCommand(cmd, args, targetServiceID);
					return flag && flag2;
				}
				if (cmd.ToLower().StartsWith("query_service_id"))
				{
					HashSet<int> hashSet = new HashSet<int>();
					hashSet.UnionWith(this.MicroPlayServiceIDs());
					hashSet.UnionWith(this.PlayerServiceIDs());
					hashSet.UnionWith(this.GuildServiceIDs());
					foreach (int serviceID in hashSet)
					{
						AdminCommand op = new AdminCommand(cmd, args);
						base.RequestOperation(serviceID, op);
					}
					return true;
				}
				if (cmd.ToLower().StartsWith("clr_profiler"))
				{
					string[] source = args.Split(new char[]
					{
						' '
					});
					if (source.Count<string>() == 2)
					{
						int num = 0;
						bool flag3 = int.TryParse(source.ElementAt(0), out num);
						if (flag3 && num > 0)
						{
							AdminCommand op2 = new AdminCommand(cmd, source.ElementAt(1).ToLower());
							base.RequestOperation(num, op2);
							return true;
						}
						Log<AdminClientService>.Logger.Error("clr_profiler cannot find serviceID");
					}
					else
					{
						Log<AdminClientService>.Logger.Error("clr_profiler needs two argments. clr_profiler <serviceId> <operation>");
					}
					return false;
				}
				if (cmd.ToLower().StartsWith("frauddetector_"))
				{
					string[] source2 = args.Split(new char[]
					{
						' '
					});
					AdminCommand op3 = new AdminCommand(cmd, (args.Length > 0) ? source2.ElementAt(1).ToLower() : "");
					HashSet<int> hashSet2 = new HashSet<int>();
					hashSet2.UnionWith(this.FrontendServiceIDs());
					foreach (int serviceID2 in hashSet2)
					{
						base.RequestOperation(serviceID2, op3);
					}
					return true;
				}
			}
			return false;
		}

		public bool ProcessScript(string text, int targetServiceID)
		{
			if (text != null && text.Length > 0)
			{
				char[] separator = new char[]
				{
					';'
				};
				string[] array = text.Split(separator);
				foreach (string text2 in array)
				{
					text2.Trim();
					string args = "";
					int num = text2.IndexOf(' ');
					string cmd;
					if (num != -1)
					{
						cmd = text2.Substring(0, num);
						args = text2.Substring(num).Trim();
					}
					else
					{
						cmd = text2;
					}
					this._ProcessConsoleCommand(cmd, args, null, targetServiceID);
				}
				return true;
			}
			return false;
		}

		private bool ProcessEventCommand(string args, AdminClientServicePeer peer, int targetServiceID)
		{
			if (args == "goal")
			{
				this.ProcessMicroPlayCommand("monster_kill", "", targetServiceID);
				this.ProcessMicroPlayCommand("autofish_item", "capsule_ap100", targetServiceID);
				return true;
			}
			CmdTokenizer cmdTokenizer = new CmdTokenizer(args);
			string next = cmdTokenizer.GetNext();
			string next2 = cmdTokenizer.GetNext();
			string template = null;
			string msgstart = null;
			string msgnotify = null;
			string msgend = null;
			string feature = null;
			string scriptstart = null;
			string scriptend = null;
			int msginterval = 1800;
			int num = 0;
			string username = "unknown";
			DateTime? startTime = null;
			DateTime? endTime = null;
			TimeSpan? periodBegin = null;
			TimeSpan? periodEnd = null;
			string text;
			while (cmdTokenizer.GetNext(out text))
			{
				if (text == "template")
				{
					template = cmdTokenizer.GetNext();
				}
				else if (text == "feature")
				{
					feature = cmdTokenizer.GetNext();
				}
				else if (text == "scriptstart")
				{
					scriptstart = cmdTokenizer.GetNext();
				}
				else
				{
					if (!(text == "scriptend"))
					{
						if (text == "starttime")
						{
							if (num > 0)
							{
								Log<AdminClientService>.Logger.Error("starttime은 startcount와 함께 사용될 수 없습니다.");
								return false;
							}
							try
							{
								string next3 = cmdTokenizer.GetNext();
								if (next3.Length > 0)
								{
									startTime = new DateTime?(DateTime.Parse(next3));
								}
								continue;
							}
							catch (Exception ex)
							{
								Log<AdminClientService>.Logger.Error("starttime 파싱 도중 에러가 발생했습니다", ex);
								return false;
							}
						}
						if (text == "endtime")
						{
							try
							{
								string next4 = cmdTokenizer.GetNext();
								if (next4.Length > 0)
								{
									endTime = new DateTime?(DateTime.Parse(next4));
								}
								continue;
							}
							catch (Exception ex2)
							{
								Log<AdminClientService>.Logger.Error("endtime 파싱 도중 에러가 발생했습니다", ex2);
								return false;
							}
						}
						if (text == "periodbegin")
						{
							try
							{
								string next5 = cmdTokenizer.GetNext();
								if (next5.Length > 0)
								{
									periodBegin = new TimeSpan?(TimeSpan.Parse(next5));
								}
								continue;
							}
							catch (Exception ex3)
							{
								Log<AdminClientService>.Logger.Error("periodbegin 파싱 도중 에러가 발생했습니다", ex3);
								return false;
							}
						}
						if (text == "periodend")
						{
							try
							{
								string next6 = cmdTokenizer.GetNext();
								if (next6.Length > 0)
								{
									periodEnd = new TimeSpan?(TimeSpan.Parse(next6));
								}
								continue;
							}
							catch (Exception ex4)
							{
								Log<AdminClientService>.Logger.Error("periodend 파싱 도중 에러가 발생했습니다", ex4);
								return false;
							}
						}
						if (text == "msgstart")
						{
							msgstart = cmdTokenizer.GetNext();
							continue;
						}
						if (text == "msgnotify")
						{
							msgnotify = cmdTokenizer.GetNext();
							continue;
						}
						if (text == "msgend")
						{
							msgend = cmdTokenizer.GetNext();
							continue;
						}
						if (text == "msginterval")
						{
							try
							{
								string next7 = cmdTokenizer.GetNext();
								if (next7.Length > 0)
								{
									msginterval = int.Parse(next7);
								}
								continue;
							}
							catch (Exception ex5)
							{
								Log<AdminClientService>.Logger.Error("msginterval 파싱 도중 에러가 발생했습니다", ex5);
								return false;
							}
						}
						if (text == "startcount")
						{
							if (startTime != null)
							{
								Log<AdminClientService>.Logger.Error("startcount는 starttime과 함께 사용될 수 없습니다.");
								return false;
							}
							try
							{
								string next8 = cmdTokenizer.GetNext();
								if (next8.Length > 0)
								{
									num = int.Parse(next8);
								}
								continue;
							}
							catch (Exception ex6)
							{
								Log<AdminClientService>.Logger.Error("startcount 파싱 도중 에러가 발생했습니다", ex6);
								return false;
							}
						}
						if (text == "username")
						{
							username = cmdTokenizer.GetNext();
							continue;
						}
						Log<AdminClientService>.Logger.ErrorFormat("알수 없는 옵션입니다. [{0}]", text);
						return false;
					}
					scriptend = cmdTokenizer.GetNext();
				}
			}
			if (next == "reg")
			{
				if (next2.Length > 0)
				{
					return EventDataContext.RegisterEvent(next2, template, feature, scriptstart, scriptend, startTime, endTime, periodBegin, periodEnd, msgstart, msgnotify, msgend, msginterval, num, username, peer);
				}
			}
			else if (next == "end")
			{
				if (next2.Length > 0)
				{
					return EventDataContext.EndEvent(next2, scriptend, msgend, peer);
				}
			}
			else
			{
				if (next == "list")
				{
					return EventDataContext.ListEvent(peer);
				}
				if (next == "show")
				{
					if (next2.Length > 0)
					{
						return EventDataContext.ShowEvent(next2, peer);
					}
				}
				else if (next == "resume")
				{
					EventDataContext.StartProcessing();
				}
			}
			return false;
		}

		private bool ProcessMicroPlayCommand(string cmd, string args, int targetServiceID)
		{
			foreach (int num in this.MicroPlayServiceIDs())
			{
				if (targetServiceID == -1 || num == targetServiceID)
				{
					AdminCommand op = new AdminCommand(cmd, args);
					base.RequestOperation(num, op);
				}
			}
			return true;
		}

		private bool ProcessQuestCommand(string cmd, string args, int targetServiceID)
		{
			foreach (int num in this.QuestOwnershipServiceIDs())
			{
				if (targetServiceID == -1 || num == targetServiceID)
				{
					AdminCommand op = new AdminCommand(cmd, args);
					base.RequestOperation(num, op);
				}
			}
			return true;
		}

		private bool ProcessItemCommand(string cmd, string args, int targetServiceID)
		{
			foreach (int num in this.ItemServiceIDs())
			{
				if (targetServiceID == -1 || num == targetServiceID)
				{
					AdminCommand op = new AdminCommand(cmd, args);
					base.RequestOperation(num, op);
				}
			}
			return true;
		}

		private bool ProcessDsCommand(string cmd, string args, AdminClientServicePeer peer)
		{
			AdminCommand op = new AdminCommand(cmd, args);
			base.RequestOperation("DSService.DSService", op);
			return true;
		}

		private bool ProcessGuildCommand(string cmd, string args, AdminClientServicePeer peer)
		{
			foreach (int num in this.GuildServiceIDs())
			{
				AdminCommand op = new AdminCommand(cmd, args);
				base.RequestOperation("GuildService.GuildService", op);
			}
			return true;
		}

		public static AdminClientService Instance;

		private TcpServer acceptor = new TcpServer();

		private AdminClientServicePeer controller;

		private List<AdminClientServicePeer> peerlist = new List<AdminClientServicePeer>();

		public HeroesLogDataContext logger;

		private List<string> stoppedservices = new List<string>();

		private MessageHandlerFactory mf = new MessageHandlerFactory();

		private static bool isFirstMicroPlay = true;
	}
}
