using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Devcat.Core;
using Devcat.Core.Net.Message;
using Devcat.Core.Threading;
using Devcat.Core.Threading.Profiler;
using HeroesChannelServer;
using HeroesChannelServer.Message;
using MMOChannelService.Properties;
using MMOServer;
using ServiceCore;
using ServiceCore.Configuration;
using ServiceCore.EndPointNetwork;
using ServiceCore.FrontendServiceOperations;
using ServiceCore.HeroesContents;
using ServiceCore.MMOChannelServiceOperations;
using UnifiedNetwork.Entity;
using UnifiedNetwork.Entity.Operations;
using UnifiedNetwork.PerfMon;
using Utility;

namespace MMOChannelService
{
	internal class MMOChannelService : Service
	{
		public int Capacity
		{
			get
			{
				return ServiceCore.FeatureMatrix.GetInteger("MMOChannelCapacity");
			}
		}

		public int ThreadCount { get; private set; }

		public Server Server
		{
			get
			{
				return this.server;
			}
		}

		internal ICollection<int> ChannelServiceIDs
		{
			get
			{
				return base.LookUp.FindIndex("MMOChannelService.MMOChannelService");
			}
		}

		public PerformanceMonitor PerfMon
		{
			get
			{
				return this.perfMon;
			}
		}

		public LoadManager LoadManager
		{
			get
			{
				return this.loadMananger;
			}
		}

		public long CurrentLoad { get; private set; }

		public override int CompareAndSwapServiceID(long id, string category, int beforeID)
		{
			if (category != base.Category)
			{
				return -1;
			}
			int result;
			using (EntityDataContext entityDataContext = new EntityDataContext())
			{
				result = entityDataContext.AcquireService(new long?(id), base.Category, new int?(base.ID), new int?(beforeID));
			}
			return result;
		}

		private void InitializeThreads()
		{
			foreach (JobProcessor jobProcessor in this.threads)
			{
				jobProcessor.Stop();
			}
			foreach (JobProfiler jobProfiler in this.profilers)
			{
				jobProfiler.Unbind();
			}
			this.threads = new List<JobProcessor>();
			this.profilers = new List<JobProfiler>();
			if (this.ThreadCount <= 0)
			{
				return;
			}
			int i;
			for (i = 0; i < this.ThreadCount; i++)
			{
				JobProcessor jobProcessor2 = base.AcquireNewThread();
				this.threads.Add(jobProcessor2);
				jobProcessor2.ExceptionOccur += delegate(object sender, EventArgs<Exception> arg)
				{
					Log<MMOChannelService>.Logger.Error(string.Format("Unhandled Exception from Thread{0} : ", i), arg.Value);
				};
				this.isProfileThread = ServiceCore.FeatureMatrix.IsEnable("MMOThreadProfile");
				if (this.isProfileThread)
				{
					JobProfiler jobProfiler2 = new JobProfiler(new AverageProfilePolicy())
					{
						CalculateIntervalMilliSecs = 30000
					};
					jobProfiler2.ExceptionOccur += delegate(object sender, EventArgs<Exception> arg)
					{
						Log<MMOChannelService>.Logger.Error(string.Format("Unhandled Exception from Thread{0} profiler : ", i), arg.Value);
					};
					jobProfiler2.Bind(jobProcessor2);
					this.profilers.Add(jobProfiler2);
				}
				jobProcessor2.Start();
			}
		}

		public bool Possess(long channelID)
		{
			bool result;
			using (EntityDataContext entityDataContext = new EntityDataContext())
			{
				int num = entityDataContext.AcquireService(new long?(channelID), base.Category, new int?(base.ID), new int?(-1));
				if (this.ChannelServiceIDs.Contains(num))
				{
					if (num == base.ID)
					{
						base.BeginGetEntity(channelID, base.Category, delegate(IEntity _, BindEntityResult __)
						{
						});
						result = true;
					}
					else
					{
						result = false;
					}
				}
				else
				{
					entityDataContext.AcquireService(new long?(channelID), base.Category, new int?(base.ID), new int?(num));
					base.BeginGetEntity(channelID, base.Category, delegate(IEntity _, BindEntityResult __)
					{
					});
					result = true;
				}
			}
			return result;
		}

		private void PossessChannels()
		{
			List<int> list = new List<int>();
			list.AddRange(this.ChannelServiceIDs);
			list.Sort();
			int num = list.IndexOf(base.ID);
			if (num < 0)
			{
				Log<MMOChannelService>.Logger.FatalFormat("Service[ID: {0}] is not in the Lookup Service List", base.ID);
				return;
			}
			for (int i = num + 1; i <= ServiceCore.FeatureMatrix.GetInteger("ActiveChannelMax"); i += list.Count)
			{
				if (!this.Possess((long)i))
				{
					Log<MMOChannelService>.Logger.ErrorFormat("Channel [{0}] is already possessed by other ", i);
				}
			}
			Log<MMOChannelService>.Logger.WarnFormat("ChannelService {0} : Channel Possession Complete", base.ID);
		}

		public override void Initialize(JobProcessor thread)
		{
			ConnectionStringLoader.LoadFromServiceCore(Settings.Default);
			this.ThreadCount = ServiceCore.FeatureMatrix.GetInteger("MMOChannelThreadCount");
			base.Initialize(thread, MMOChannelServiceOperations.TypeConverters);
			base.RegisterMessage(OperationMessages.TypeConverters);
			base.RegisterAllProcessors(Assembly.GetExecutingAssembly());
			this.InitializeThreads();
			Log<MMOChannelService>.Logger.InfoFormat("Starting server with port {0}", HeroesSection.Instance.ChannelRelay.TcpPort);
			this.server = new Server(thread, HeroesSection.Instance.ChannelRelay.TcpPort);
			this.server.ClientIdentified += this.server_ClientIdentified;
			string[] files = Directory.GetFiles(ServiceCoreSettings.Default.MMOChannelMapFilePath, "*.dat", SearchOption.AllDirectories);
			foreach (string text in files)
			{
				Log<MMOChannelService>.Logger.InfoFormat("Loading {0}", text);
				string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(text);
				Map item = new Map(fileNameWithoutExtension, text);
				this.maps.Add(item);
			}
			MMOChannelContents.Initialize();
			if (!ServiceCore.FeatureMatrix.IsEnable("MMOForceRecommend"))
			{
				this.perfMon = new PerformanceMonitor(thread, 30000);
				this.perfMon.ValueUpdated += this.perfMon_ValueUpdated;
			}
			Scheduler.Schedule(thread, Job.Create(new Action(this.PossessChannels)), 60000);
			if (ServiceCore.FeatureMatrix.IsEnable("Burn"))
			{
				Scheduler.Schedule(base.Thread, Job.Create(new Action(this.BurnGaugeBroadcastRepeat)), 60000);
			}
		}

		private void perfMon_ValueUpdated(object sender, EventArgs e)
		{
			long num = -1L;
			DateTime now = DateTime.Now;
			if (this.isProfileThread)
			{
				for (int i = 0; i < this.profilers.Count; i++)
				{
					JobProfiler jobProfiler = this.profilers[i];
					if (jobProfiler != null && now.Subtract(jobProfiler.CalculatedTime).TotalMinutes <= 1.0)
					{
						double num2 = ((ProfileIndex<double>)jobProfiler.CalculatedValue).WaitTimeIndex * 100.0;
						Log<MMOChannelService>.Logger.DebugFormat("{0} thread index {1}", i, num2);
						if ((long)num2 > num)
						{
							num = (long)num2;
						}
					}
				}
				if (num < 0L)
				{
					if (this.ThreadCount > 0)
					{
						Log<MMOChannelService>.Logger.Error("no profiler index - all thread dead?");
						num = 100L;
					}
					else
					{
						num = (long)((int)this.perfMon.CpuPercentTotal);
					}
				}
			}
			else
			{
				num = (long)((int)this.perfMon.CpuPercentTotal);
			}
			this.CurrentLoad = num;
			foreach (int serviceID in base.LookUp.FindIndex(base.Category))
			{
				SyncLoad op = new SyncLoad
				{
					ServiceID = base.ID,
					Load = num
				};
				base.RequestOperation(serviceID, op);
			}
		}

		private void server_ClientIdentified(object sender, EventArgs<Client> e)
		{
			Client value = e.Value;
			ChannelMember waitingMember;
			if (!this.waitingMembers.TryGetValue(value.ID, out waitingMember))
			{
				value.Close();
				return;
			}
			this.waitingMembers.Remove(value.ID);
			waitingMember.Client = value;
			waitingMember.Client.Disconnected += delegate(object _, EventArgs __)
			{
				waitingMember.Close();
			};
			IEntityProxy conn = base.Connect(waitingMember.ChannelJoined.Entity, new UnifiedNetwork.Entity.Location(value.ID, "PlayerService.PlayerService"));
			waitingMember.BindCharacterConn(conn);
			waitingMember.CharacterConn.Closed += delegate(object _, EventArgs<IEntityProxy> __)
			{
				waitingMember.Close();
			};
			waitingMember.FrontendConn = base.Connect(waitingMember.ChannelJoined.Entity, new UnifiedNetwork.Entity.Location(waitingMember.FID, "FrontendServiceCore.FrontendService"));
			waitingMember.FrontendConn.Closed += delegate(object _, EventArgs<IEntityProxy> __)
			{
				waitingMember.Close();
			};
			waitingMember.ChannelJoined.Enter(waitingMember);
			waitingMember.IsInChannel = true;
			waitingMember.FrontendConn.RequestOperation(new NotifyEnterChannelResult
			{
				ResultEnum = EnterChannelResult.Success
			});
		}

		protected override void BeginMakeEntity(long id, string category, Action<IEntity> callback)
		{
			Channel channel = this.channels.TryGetValue(id);
			if (channel == null)
			{
				Log<MMOChannelService>.Logger.InfoFormat("Channel {0} initialize...", id);
				channel = new Channel();
				foreach (Map map in this.maps)
				{
					map.Build(channel);
				}
				channel.ConfirmRegions(1);
				if (this.threads.Count > 0)
				{
					if (this.nextThread >= this.threads.Count)
					{
						this.nextThread %= this.threads.Count;
					}
					channel.Tag = this.threads[this.nextThread];
					this.nextThread = (this.nextThread + 1) % this.threads.Count;
				}
				else
				{
					channel.Tag = null;
				}
			}
			IEntity entity = this.MakeEntity(id, category);
			entity.Tag = new ChannelEntity(channel, entity);
			this.channels[id] = channel;
			channel.Entity = entity.Tag;
			entity.Used += this.newEntity_Used;
			if (callback != null)
			{
				callback(entity);
			}
		}

		private void newEntity_Used(object sender, EventArgs<IEntityAdapter> e)
		{
			if (e.Value.RemoteCategory == "FrontendServiceCore.FrontendService")
			{
				ChannelMember channelMember = e.Value.Tag as ChannelMember;
				if (channelMember != null && channelMember.ChannelJoined.Entity == e.Value.LocalEntity)
				{
					channelMember.Close();
				}
			}
		}

		public bool AddWaitingMember(long cid, ChannelMember member)
		{
			if (this.waitingMembers.ContainsKey(cid))
			{
				return false;
			}
			this.waitingMembers[cid] = member;
			member.Closed += delegate(object _, EventArgs __)
			{
				this.RemoveWaitingMember(cid, member);
			};
			return true;
		}

		public bool RemoveWaitingMember(long cid, ChannelMember member)
		{
			ChannelMember channelMember;
			if (this.waitingMembers.TryGetValue(cid, out channelMember) && channelMember == member)
			{
				this.waitingMembers.Remove(cid);
				return true;
			}
			return false;
		}

		public long RecommendChannel()
		{
			long num = -1L;
			double num2 = 1.0;
			List<JobProcessor> list = new List<JobProcessor>();
			bool flag = false;
			if (this.isProfileThread && !ServiceCore.FeatureMatrix.IsEnable("MMOForceRecommend"))
			{
				for (int i = 0; i < this.profilers.Count; i++)
				{
					JobProfiler jobProfiler = this.profilers[i];
					if (jobProfiler != null)
					{
						if (num2 > ((ProfileIndex<double>)jobProfiler.CalculatedValue).WaitTimeIndex)
						{
							flag = true;
						}
						else
						{
							list.Add(this.threads[i]);
						}
					}
				}
				if (!flag && this.ThreadCount > 0)
				{
					Log<MMOChannelService>.Logger.Warn("all threads are busy");
					return -1L;
				}
			}
			foreach (KeyValuePair<long, Channel> keyValuePair in this.channels)
			{
				Channel value = keyValuePair.Value;
				if (!list.Contains(value.Tag as JobProcessor))
				{
					ChannelEntity channelEntity = value.Entity as ChannelEntity;
					int num3 = (channelEntity == null) ? 0 : channelEntity.Count;
					if (num3 < (int)((double)this.Capacity * 0.8) && (num < 0L || keyValuePair.Key < num))
					{
						num = keyValuePair.Key;
					}
				}
			}
			if (num == -1L)
			{
				Log<MMOChannelService>.Logger.Warn("no channel to recommend");
				if (Log<MMOChannelService>.Logger.IsWarnEnabled)
				{
					foreach (KeyValuePair<long, Channel> keyValuePair2 in this.channels)
					{
						Channel value2 = keyValuePair2.Value;
						if (!list.Contains(value2.Tag as JobProcessor))
						{
							ChannelEntity channelEntity2 = value2.Entity as ChannelEntity;
							int num4 = (channelEntity2 == null) ? 0 : channelEntity2.Count;
							Log<MMOChannelService>.Logger.WarnFormat("{0} channel count : {1}", keyValuePair2.Key, num4);
						}
					}
				}
			}
			return num;
		}

		public static Service StartService(string ip, string portstr)
		{
			MMOChannelService mmochannelService = new MMOChannelService();
			ServiceInvoker.StartService(ip, portstr, mmochannelService);
			MMOChannelService.StartReporting(mmochannelService);
			return mmochannelService;
		}

		private static void StartReporting(MMOChannelService serv)
		{
			if (!ServiceCore.FeatureMatrix.IsEnable("ServiceReporter"))
			{
				return;
			}
			int num = ServiceReporterSettings.Get("MMOChannelService.Interval", 60);
			ServiceReporter.Instance.Initialize("MMOChannelService");
			ServiceReporter.Instance.AddGathering("Stat", new ServiceReporter.GatheringDelegate<int>(serv.OnGatheringStat));
			ServiceReporter.Instance.Start(num * 1000);
		}

		private void OnGatheringStat(Dictionary<string, int> dict)
		{
			dict.InsertOrIncrease("Entity", (int)this.GetEntityCount());
			dict.InsertOrIncrease("Queue", (int)this.GetQueueLength());
		}

		public void BurnGaugeBroadcastRepeat()
		{
			if (ServiceCore.FeatureMatrix.IsEnable("Burn"))
			{
				this.BurnGaugeBroadcast();
				Scheduler.Schedule(base.Thread, Job.Create(new Action(this.BurnGaugeBroadcastRepeat)), ServiceCore.FeatureMatrix.GetInteger("BurnGaugeInterval"));
			}
		}

		public void BurnGaugeBroadcast()
		{
			using (BurnDataContext burnDataContext = new BurnDataContext())
			{
				try
				{
					int num = burnDataContext.AddAllUserEventCount(new int?(-99), new int?(0), new DateTime?(DateTime.MaxValue));
					BurnJackpot burnJackpot = MMOChannelContents.GetBurnJackpot();
					if (burnJackpot != null)
					{
						if (this.LatestBurnGauge != num)
						{
							this.LatestBurnGauge = num;
							foreach (KeyValuePair<long, Channel> keyValuePair in this.channels)
							{
								Channel value = keyValuePair.Value;
								PacketMessage message = new PacketMessage
								{
									Data = SerializeWriter.ToBinary<ServiceCore.EndPointNetwork.BurnGauge>(new ServiceCore.EndPointNetwork.BurnGauge(num, burnJackpot.JackpotStart, burnJackpot.MaxReward))
								};
								value.BroadcastToColhenLobby(message);
							}
						}
					}
				}
				catch (Exception ex)
				{
					Log<MMOChannelService>.Logger.Error("burn gauge broadcasting error", ex);
				}
			}
		}

		public void BurnJackpotBroadcast(long cid)
		{
			foreach (KeyValuePair<long, Channel> keyValuePair in this.channels)
			{
				Channel value = keyValuePair.Value;
				PacketMessage message = new PacketMessage
				{
					Data = SerializeWriter.ToBinary<BurnJackpotMessage>(new BurnJackpotMessage(cid))
				};
				value.BroadcastToColhenLobby(message);
			}
		}

		private List<JobProcessor> threads = new List<JobProcessor>();

		private bool isProfileThread;

		private List<JobProfiler> profilers = new List<JobProfiler>();

		private Dictionary<long, Channel> channels = new Dictionary<long, Channel>();

		private List<Map> maps = new List<Map>();

		private Dictionary<long, long> fids = new Dictionary<long, long>();

		private Dictionary<long, ChannelMember> waitingMembers = new Dictionary<long, ChannelMember>();

		private PerformanceMonitor perfMon;

		private LoadManager loadMananger = new LoadManager();

		private int LatestBurnGauge = -1;

		private Server server;

		private int nextThread;
	}
}
