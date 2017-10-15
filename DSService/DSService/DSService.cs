using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Management;
using System.Net;
using System.Net.Sockets;
using System.Reflection;
using Devcat.Core;
using Devcat.Core.Net;
using Devcat.Core.Net.Message;
using Devcat.Core.Threading;
using DSService.DSEntityMaker;
using DSService.Message;
using DSService.Properties;
using DSService.WaitingQueue;
using ServiceCore;
using ServiceCore.CommonOperations;
using ServiceCore.Configuration;
using ServiceCore.DSServiceOperations;
using ServiceCore.EndPointNetwork;
using ServiceCore.PvpServiceOperations;
using UnifiedNetwork.Cooperation;
using UnifiedNetwork.Entity;
using UnifiedNetwork.Entity.Operations;
using UnifiedNetwork.OperationService;
using Utility;

namespace DSService
{
	public class DSService : UnifiedNetwork.Entity.Service
	{
		public TcpServer TcpServer { get; set; }

		public int Port { get; set; }

		public MessageHandlerFactory MessageHandlerFactory { get; set; }

		public CactiCsvFileDropper CactiCsvFileDropper { get; set; }

		public override void Initialize(JobProcessor thread)
		{
			ConnectionStringLoader.LoadFromServiceCore(Settings.Default);
			DSContents.Initialize();
			base.Initialize(thread, DSServiceOperations.TypeConverters);
			base.RegisterMessage(OperationMessages.TypeConverters);
			base.RegisterAllProcessors(Assembly.GetExecutingAssembly());
			this.MessageHandlerFactory = new MessageHandlerFactory(true);
			this.MessageHandlerFactory.Register<DSEntity>(Messages.TypeConverters, "ProcessMessage");
			this.InitializeTcpClientHolder();
			base.Disposed += delegate(object _, EventArgs __)
			{
				this.KillDSProcess();
			};
			this.KillDSProcess();
			DSService.MyIP = this.GetMyIP();
			this.InitializeTcpServer(ServiceCoreSettings.Default.DSServicePort);
			base.OnSetupID += delegate(int id)
			{
				Scheduler.Schedule(base.Thread, Job.Create(new Action(this.SetupDSController)), 0);
			};
			int integer = FeatureMatrix.GetInteger("DSCactiFrequency");
			if (integer > 0)
			{
				this.CactiCsvFileDropper = new CactiCsvFileDropper("Cacti", "DSService", integer, new List<string>
				{
					"ProcessCount",
					"FPS_siglint",
					"FPS_bugeikloth",
					"FPS_elculous"
				});
				this.CactiCsvFileDropper.FileDropStarted += this.GetCactiInformation;
			}
			FeatureMatrix.onUpdated += this.OnUpdatedFetureMatrix;
		}

		private void OnUpdatedFetureMatrix(UnifiedNetwork.OperationService.Service service, UpdateFeatureMatrix operation)
		{
			if (base.ID != service.ID)
			{
				return;
			}
			Log<DSService>.Logger.WarnFormat("Updating FeatureMatrix", new object[0]);
			SyncFeatureMatrixMessage message = new SyncFeatureMatrixMessage(FeatureMatrix.GetFeatureMatrixDic());
			foreach (DSEntity dsentity in this.DSEntities.Values)
			{
				dsentity.SendMessage<SyncFeatureMatrixMessage>(message);
			}
		}

		private int GetAvg(List<int> list)
		{
			if (list.Count == 0)
			{
				return 0;
			}
			return (int)list.Average();
		}

		private void GetCactiInformation()
		{
			Dictionary<string, List<int>> dictionary = this.CactiTargetQuestIDs.ToDictionary((string x) => x, (string x) => new List<int>());
			int num = 0;
			foreach (DSEntity dsentity in this.DSEntities.Values)
			{
				if (dsentity.Process != null)
				{
					num++;
					if (dsentity.QuestID != null && dictionary.ContainsKey(dsentity.QuestID) && dsentity.FPS != 0)
					{
						dictionary[dsentity.QuestID].Add(dsentity.FPS);
					}
				}
			}
			this.CactiCsvFileDropper.SetValue("ProcessCount", num);
			this.CactiCsvFileDropper.SetValue("FPS_siglint", this.GetAvg(dictionary["quest_dragon_ancient_siglint"]));
			this.CactiCsvFileDropper.SetValue("FPS_bugeikloth", this.GetAvg(dictionary["quest_dragon_ancient_bugeikloth"]));
			this.CactiCsvFileDropper.SetValue("FPS_elculous", this.GetAvg(dictionary["quest_dragon_ancient_elculous"]));
		}

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

		private void Entity_Closed(object sender, EventArgs e)
		{
			try
			{
				EntityDataContext entityDataContext = new EntityDataContext();
				entityDataContext.AcquireService(new long?((sender as IEntity).ID), base.Category, new int?(-1), new int?(base.ID));
			}
			catch (Exception ex)
			{
				Log<DSService>.Logger.ErrorFormat("Entity_Closed [EntityID : {0}] [ServiceID : {1}] [Category : {2}] - {3} ", new object[]
				{
					(sender as IEntity).ID,
					base.ID,
					base.Category,
					ex
				});
			}
		}

		protected override IEntity MakeEntity(long id, string category)
		{
			IEntity entity = base.MakeEntity(id, category);
			DSEntity dsentity = new DSEntity();
			entity.Tag = dsentity;
			dsentity.Entity = entity;
			entity.Closed += delegate(object sender, EventArgs e)
			{
				Log<DSService>.Logger.FatalFormat("<GID : {0}> DSEntity closed...", entity.ID);
			};
			entity.Closed += this.Entity_Closed;
			return entity;
		}

		public static DSService Instance { get; set; }

		public static UnifiedNetwork.Entity.Service StartService(string ip, string portstr)
		{
			DSService.Instance = new DSService();
			ServiceInvoker.StartService(ip, portstr, DSService.Instance);
			if (FeatureMatrix.IsEnable("ServiceReporter"))
			{
				DSService.StartReporting(DSService.Instance);
			}
			return DSService.Instance;
		}

		public static void StartReporting(DSService serv)
		{
			if (!FeatureMatrix.IsEnable("ServiceReporter"))
			{
				return;
			}
			ServiceReporter.Instance.Initialize("DSService");
			int num = ServiceReporterSettings.Get("DSService.FPS.Interval", 60);
			ServiceReporter.Instance.AddSubject("FPS", null, num * 1000);
			ServiceReporter.Instance.AddGathering("Stat", new ServiceReporter.GatheringDelegate<int>(serv.OnGatheringStat));
			ServiceReporter.Instance.AddGathering("FPS", new ServiceReporter.GatheringDelegate<int>(serv.OnGatheringFPS));
			int num2 = ServiceReporterSettings.Get("DSService.Interval", 60);
			ServiceReporter.Instance.Start(num2 * 1000);
		}

		public void KillDSProcess()
		{
			foreach (Process process in Process.GetProcesses().ToList<Process>())
			{
				if (process.ProcessName.ToLower() == "srcds")
				{
					Log<DSService>.Logger.WarnFormat("kill Process : {0} {1}", process.Id, process.ProcessName);
					process.Kill();
				}
			}
		}

		public DSProcessInfo GetDSServiceState()
		{
			DSProcessInfo dsprocessInfo = new DSProcessInfo(base.Acceptor.EndPointAddress.Address.ToString());
			if (base.ID == this.DSBossID)
			{
				Dictionary<string, Dictionary<string, int>> dictionary = new Dictionary<string, Dictionary<string, int>>();
				foreach (KeyValuePair<string, DSWaitingQueue> keyValuePair in this.DSWaitingSystem.QueueDict)
				{
					Dictionary<string, int> dictionary2 = new Dictionary<string, int>();
					dictionary2.Add("Wait", keyValuePair.Value.WaitingParties.Count);
					dictionary2.Add("Play", keyValuePair.Value.Ships.Count);
					dictionary.Add(keyValuePair.Key, dictionary2);
				}
				dsprocessInfo.SetQueueInfo(dictionary);
			}
			foreach (KeyValuePair<int, DSEntity> keyValuePair2 in this.DSEntities)
			{
				Process process = keyValuePair2.Value.Process;
				if (process != null && !process.HasExited)
				{
					dsprocessInfo.AddProcessInfo(new Dictionary<string, string>
					{
						{
							"PID",
							process.Id.ToString()
						},
						{
							"QuestID",
							keyValuePair2.Value.QuestID
						},
						{
							"DSType",
							keyValuePair2.Value.DSType.ToString()
						},
						{
							"RunningTime",
							Math.Round((DateTime.Now - process.StartTime).TotalMinutes, 1) + "Min"
						},
						{
							"IsGiantRaid",
							keyValuePair2.Value.IsGiantRaid ? "GiantRaid" : "NormalRaid"
						},
						{
							"AllocatedMemory",
							Math.Round((double)process.PrivateMemorySize64 / 1024.0 / 1024.0, 1) + "MB"
						},
						{
							"UsingMemory",
							Math.Round((double)process.WorkingSet64 / 1024.0 / 1024.0, 1) + "MB"
						}
					});
				}
			}
			return dsprocessInfo;
		}

		private void OnGatheringStat(Dictionary<string, int> dict)
		{
			int num = 0;
			int num2 = 0;
			int num3 = 0;
			foreach (DSEntity dsentity in this.DSEntities.Values)
			{
				num += dsentity.QuestStarted;
				num2 += dsentity.QuestClosed;
				num3 += dsentity.QuestCrashed;
			}
			dict.InsertOrIncrease("QuestStarted", num);
			dict.InsertOrIncrease("QuestClosed", num2);
			dict.InsertOrIncrease("QuestCrashed", num3);
			dict.InsertOrIncrease("Entity", (int)this.GetEntityCount());
			dict.InsertOrIncrease("Queue", (int)this.GetQueueLength());
		}

		private void OnGatheringFPS(Dictionary<string, int> dict)
		{
			Dictionary<string, List<int>> dictionary = new Dictionary<string, List<int>>();
			int num = 0;
			foreach (DSEntity dsentity in this.DSEntities.Values)
			{
				if (dsentity.Process != null)
				{
					num++;
					if (dsentity.QuestID != null && dsentity.FPS != 0)
					{
						if (!dictionary.ContainsKey(dsentity.QuestID))
						{
							dictionary.Add(dsentity.QuestID, new List<int>());
						}
						dictionary[dsentity.QuestID].Add(dsentity.FPS);
					}
				}
			}
			dict.Add("QuestWorking", num);
			foreach (KeyValuePair<string, List<int>> keyValuePair in dictionary)
			{
				int avg = this.GetAvg(keyValuePair.Value);
				dict.InsertOrUpdate(keyValuePair.Key, avg);
			}
		}

		public IEntity DSServiceEntity { get; set; }

		public void InitializeEntity()
		{
			base.BeginGetEntity(base.MakeEntityID().Ticks, base.Category, delegate(IEntity resultEntity, BindEntityResult result)
			{
				if (resultEntity != null)
				{
					resultEntity.Tag = null;
					this.DSServiceEntity = resultEntity;
					Log<DSService>.Logger.Info("DS ServiceEntity Initialized");
					return;
				}
				Log<DSService>.Logger.Fatal("DS ServiceEntity Initialize Failed");
			});
		}

		public void InitializeEntity(int idStart, int count)
		{
			using (IEnumerator<int> enumerator = Enumerable.Range(idStart, count).GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					int key = enumerator.Current;
					base.BeginGetEntity(base.MakeEntityID().Ticks, base.Category, delegate(IEntity resultEntity, BindEntityResult result)
					{
						if (resultEntity != null)
						{
							DSEntity dsentity = resultEntity.Tag as DSEntity;
							if (dsentity != null)
							{
								this.DSEntities.Add(key, dsentity);
								dsentity.DSID = key;
								dsentity.PortIndex = key - idStart;
								Log<DSService>.Logger.InfoFormat("DS Entity Initialized : {0}", key);
							}
						}
					});
				}
			}
			base.BeginGetEntity(base.MakeEntityID().Ticks, base.Category, delegate(IEntity resultEntity, BindEntityResult result)
			{
				if (resultEntity != null)
				{
					resultEntity.Tag = null;
					this.DSServiceEntity = resultEntity;
					Log<DSService>.Logger.Info("DS ServiceEntity Initialized");
					return;
				}
				Log<DSService>.Logger.Fatal("DS ServiceEntity Initialize Failed");
			});
		}

		public void MakeDSEntity(int dsID)
		{
			if (this.DSEntities.TryGetValue(dsID) != null)
			{
				return;
			}
			base.BeginGetEntity(base.MakeEntityID().Ticks, base.Category, delegate(IEntity resultEntity, BindEntityResult result)
			{
				if (resultEntity != null)
				{
					DSEntity dsentity = resultEntity.Tag as DSEntity;
					if (dsentity != null)
					{
						this.DSEntities.Add(dsID, dsentity);
						dsentity.DSID = dsID;
						dsentity.PortIndex = dsID;
						Log<DSService>.Logger.InfoFormat("Make DS Entity : {0}", dsID);
					}
				}
			});
		}

		public void RemoveDSEntity(int dsID)
		{
			DSEntity dsentity;
			if (this.DSEntities.TryGetValue(dsID, out dsentity))
			{
				DSLog.AddLog(dsentity, "RemoveDSEntity", "");
				Log<DSService>.Logger.WarnFormat("{0} DS 제거", dsID);
				if (dsentity.Process != null)
				{
					try
					{
						dsentity.Process.Kill();
						DSLog.AddLog(dsentity, "KillProcess (RemoveDSEntity)", "");
					}
					catch (Exception ex)
					{
						Log<DSEntity>.Logger.FatalFormat("KillProcess Failed!!\n - {0}", ex);
						DSLog.AddLog(dsentity, "KillProcess Failed", "");
					}
					dsentity.Process = null;
				}
				else
				{
					UpdateDSShipInfo op = new UpdateDSShipInfo(DSService.Instance.ID, dsID, 0L, UpdateDSShipInfo.CommandEnum.PVPClosed, "");
					DSService.RequestDSBossOperation(op);
				}
				if (dsentity.TcpClient != null)
				{
					try
					{
						dsentity.TcpClient.Disconnect();
						DSLog.AddLog(dsentity, "DisconnectTcp", "");
					}
					catch (Exception ex2)
					{
						Log<DSEntity>.Logger.FatalFormat("DisconnectTcp Failed!!\n - {0}", ex2);
						DSLog.AddLog(dsentity, "DisconnectTcp Failed", "");
					}
					dsentity.TcpClient = null;
				}
			}
		}

		public DSEntity GetDSEntity(int key)
		{
			return this.DSEntities.TryGetValue(key);
		}

		public static List<string> MyPrivateIPs { get; set; }

		public static string MyIP { get; set; }

		public string GetMyIP()
		{
			DSService.MyPrivateIPs = new List<string>();
			string hostName = Dns.GetHostName();
			Log<DSService>.Logger.WarnFormat("myHostName : {0}", hostName);
			IPHostEntry hostEntry = Dns.GetHostEntry(hostName);
			string text = "";
			foreach (IPAddress ipaddress in hostEntry.AddressList)
			{
				if (ipaddress.AddressFamily == AddressFamily.InterNetwork)
				{
					if (ipaddress.IsPrivateNetwork())
					{
						DSService.MyPrivateIPs.Add(ipaddress.ToString());
					}
					else
					{
						Log<DSService>.Logger.WarnFormat("{0}", ipaddress);
						text = ipaddress.ToString();
					}
				}
			}
			if (text == "")
			{
				foreach (IPAddress ipaddress2 in hostEntry.AddressList)
				{
					if (ipaddress2.AddressFamily == AddressFamily.InterNetwork)
					{
						Log<DSService>.Logger.WarnFormat("{0}", ipaddress2);
						text = ipaddress2.ToString();
					}
				}
			}
			return text;
		}

		public void InitializeTcpServer(int port)
		{
			try
			{
				this.TcpServer = new TcpServer();
				this.Port = port;
				this.TcpServer.ClientAccept += delegate(object s, AcceptEventArgs e)
				{
					Log<DSService>.Logger.Info("<TCP Server> Connected");
					e.PacketAnalyzer = new MessageAnalyzer
					{
						CryptoTransform = new CryptoTransformHeroes()
					};
					e.JobProcessor = base.Thread;
					this.RegisterTcpClientHolder(e.Client);
				};
				this.TcpServer.ExceptionOccur += delegate(object s, EventArgs<Exception> e)
				{
					Log<DSService>.Logger.Error("<TCP Server> Exception!!!", e.Value);
				};
				this.TcpServer.Start(base.Thread, port);
				Log<DSService>.Logger.InfoFormat("Waiting in port {0}...", port);
			}
			catch
			{
				if (port - ServiceCoreSettings.Default.DSServicePort > 10)
				{
					Log<DSService>.Logger.ErrorFormat("Initialize TcpServer Failed...", new object[0]);
				}
				else
				{
					this.InitializeTcpServer(port + 1);
				}
			}
		}

		private static int ToInt(string str, int defaultValue)
		{
			int result;
			if (int.TryParse(str.Trim(), out result))
			{
				return result;
			}
			return defaultValue;
		}

		public static int GetDSProcessCount()
		{
			string text = FeatureMatrix.GetString("DSProcessCount").ToUpper();
			if (text == "" || text == null)
			{
				return 0;
			}
			if (text.Contains('C'))
			{
				int num = text.IndexOf('C');
				string str = text.Substring(0, num);
				string str2 = (text.Length > num) ? text.Substring(num + 1) : "";
				return DSService.ToInt(str, 1) * DSService.GetCoreCount() + DSService.ToInt(str2, 0);
			}
			return DSService.ToInt(text, 1);
		}

		private static int GetCoreCount()
		{
			int num = 0;
			foreach (ManagementBaseObject managementBaseObject in new ManagementClass("Win32_Processor").GetInstances())
			{
				try
				{
					num += int.Parse(managementBaseObject.Properties["NumberOfCores"].Value.ToString());
				}
				catch
				{
					num++;
				}
			}
			return num;
		}

		public static int ReCheckCount(int idStart, int count)
		{
			if (!FeatureMatrix.IsEnable("DSNormalRaid") || idStart < ServiceCoreSettings.Default.DSGiantCount || ServiceCoreSettings.Default.DSGiantCount < 0)
			{
				return count;
			}
			string text = FeatureMatrix.GetString("DSNormalCount").ToUpper();
			if (text == "" || text == null)
			{
				return 0;
			}
			if (text.Contains("P"))
			{
				int num = text.IndexOf('P');
				string str = text.Substring(0, num);
				string str2 = (text.Length > num) ? text.Substring(num + 1) : "";
				return DSService.ToInt(str, 1) * count + DSService.ToInt(str2, 0);
			}
			return DSService.ToInt(text, 1);
		}

		public int DSBossID { get; set; }

		public DSWaitingSystem DSWaitingSystem { get; set; }

		public IDSEntityMakerSystem DSEntityMakerSystem { get; set; }

		private bool GiantRaidMachinCandidate
		{
			get
			{
				if (FeatureMatrix.IsEnable("GiantRaidMachineCandidateByIP"))
				{
					string[] separator = new string[]
					{
						";"
					};
					string[] giantRaidMachineIPs = ServiceCoreSettings.Default.DSGiantRaidMachineIPs.Split(separator, StringSplitOptions.RemoveEmptyEntries);
					return DSService.MyPrivateIPs.Any((string ip) => giantRaidMachineIPs.Contains(ip));
				}
				return true;
			}
		}

		public void PrintDsState()
		{
			int num = FeatureMatrix.GetInteger("DSStatusLogPeriod");
			if (num > 0)
			{
				string log = string.Format("{0}\n{1}\n===========================================\n", DateTime.Now.ToString(), this.DSWaitingSystem.ToString());
				FileLog.Log("DS_Status.log", log);
			}
			else
			{
				num = 10;
			}
			Scheduler.Schedule(base.Thread, Job.Create(new Action(this.PrintDsState)), num * 1000);
		}

		public string GetDSState()
		{
			if (base.ID == this.DSBossID)
			{
				return this.DSEntityMakerSystem.ToString();
			}
			return "I'm not boss.";
		}

		public void SetupDSController()
		{
			this.DSBossID = base.LookUp.GetFirstRegisteredServiceID("DSService.DSService");
			if (base.ID == this.DSBossID)
			{
				Log<DSService>.Logger.WarnFormat("I am Boss!!!!!", new object[0]);
				this.DSWaitingSystem = new DSWaitingSystem();
				if (FeatureMatrix.IsEnable("DSDynamicLoad"))
				{
					this.DSEntityMakerSystem = new DSEntityMakerSystem(this);
				}
				this.PrintDsState();
			}
			Log<DSService>.Logger.WarnFormat("Boss is {0}", this.DSBossID);
			if (FeatureMatrix.IsEnable("DSDynamicLoad"))
			{
				this.InitializeEntity();
				RegisterDSServiceInfo registerDSServiceInfo = new RegisterDSServiceInfo(base.ID, DSService.GetCoreCount());
				registerDSServiceInfo.OnComplete += delegate(Operation _)
				{
					Log<DSService>.Logger.WarnFormat("Register DS Service ID : [{0}] / Core : [{1}]", base.ID, DSService.GetCoreCount());
				};
				base.RequestOperation(this.DSBossID, registerDSServiceInfo);
				return;
			}
			RegisterDSEntity op = new RegisterDSEntity(base.ID, DSService.GetCoreCount(), this.GiantRaidMachinCandidate);
			op.OnComplete += delegate(Operation _)
			{
				this.InitializeEntity(op.IDStart, op.ProcessCount);
				Log<DSService>.Logger.WarnFormat("Initialized for {0}", op.DSType);
			};
			base.RequestOperation(this.DSBossID, op);
		}

		public static void RequestDSBossOperation(Operation op)
		{
			DSService.Instance.RequestOperation(DSService.Instance.DSBossID, op);
		}

		public void UpdatePvpDS()
		{
			foreach (IPEndPoint location in base.LookUp.GetLocations("PvpService.PvpService"))
			{
				base.RequestOperation(location, new UpdatePvpDSInfo(this.DSWaitingSystem.DSStorage.PvpDSMap));
			}
		}

		public void InitializeTcpClientHolder()
		{
			this.TcpClientHolders = new List<TcpClientHolder>();
			base.RegisterMessage(DSHostConnectionMessage.TypeConverters);
			this.MessageHandlerFactory.Register<TcpClientHolder>(DSHostConnectionMessage.TypeConverters, "ProcessMessage");
		}

		public void RegisterTcpClientHolder(Devcat.Core.Net.TcpClient tcpClient)
		{
			tcpClient.ConnectionSucceed += delegate(object sender, EventArgs evt)
			{
				string text = string.Format("[Peer {0}:{1}]", tcpClient.LocalEndPoint.Address.ToString(), tcpClient.LocalEndPoint.Port.ToString());
				Log<DSService>.Logger.WarnFormat("{0} Connect ", text);
				object typeConverter = DSService.Instance.MessageHandlerFactory.GetTypeConverter();
				tcpClient.Transmit(SerializeWriter.ToBinary(typeConverter));
				DSHostConnectionQuery value = new DSHostConnectionQuery();
				tcpClient.Transmit(SerializeWriter.ToBinary<DSHostConnectionQuery>(value));
				TcpClientHolder tcpClientHolder = new TcpClientHolder();
				tcpClientHolder.BindTcpClient(tcpClient);
				tcpClientHolder.TimeoutShedID = Scheduler.Schedule(this.Thread, Job.Create<TcpClientHolder>(new Action<TcpClientHolder>(this.UnregisterTcpClientHolder), tcpClientHolder), 30000);
				this.TcpClientHolders.Add(tcpClientHolder);
			};
			tcpClient.ExceptionOccur += delegate(object sender, EventArgs<Exception> evt)
			{
				string text = string.Format("[Peer {0}:{1}]", tcpClient.LocalEndPoint.Address.ToString(), tcpClient.LocalEndPoint.Port.ToString());
				Log<DSService>.Logger.ErrorFormat("{0} ExceptionOccur", text);
				DSLog.AddLog(-1, null, -1L, -1, "ExceptionOccur", text);
			};
			tcpClient.ConnectionFail += delegate(object sender, EventArgs<Exception> evt)
			{
				string text = string.Format("[Peer {0}:{1}]", tcpClient.LocalEndPoint.Address.ToString(), tcpClient.LocalEndPoint.Port.ToString());
				Log<DSService>.Logger.ErrorFormat("{0} ConnectionFail", text);
				DSLog.AddLog(-1, null, -1L, -1, "ConnectionFail", text);
			};
		}

		public void UnregisterTcpClientHolder(TcpClientHolder clientHolder)
		{
			if (!clientHolder.HasTransfered)
			{
				clientHolder.DisconnectTcpClient();
			}
			this.TcpClientHolders.Remove(clientHolder);
		}

		private List<string> CactiTargetQuestIDs = new List<string>
		{
			"quest_dragon_ancient_siglint",
			"quest_dragon_ancient_bugeikloth",
			"quest_dragon_ancient_elculous"
		};

		public Dictionary<int, DSEntity> DSEntities = new Dictionary<int, DSEntity>();

		private List<TcpClientHolder> TcpClientHolders;
	}
}
