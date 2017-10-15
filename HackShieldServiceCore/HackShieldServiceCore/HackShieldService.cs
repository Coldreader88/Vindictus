using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using AhnLab.HackShield;
using Devcat.Core;
using Devcat.Core.Threading;
using HackShieldServiceCore.Processor;
using HackShieldServiceCore.Properties;
using ServiceCore;
using ServiceCore.Configuration;
using ServiceCore.HackShieldServiceOperations;
using UnifiedNetwork.Cooperation;
using UnifiedNetwork.Entity;
using Utility;

namespace HackShieldServiceCore
{
	public class HackShieldService : Service
	{
		[DllImport("SeServ.dll", CallingConvention = CallingConvention.StdCall)]
		public static extern IntPtr TcProtect_GetXmlData(out IntPtr nSize);

		[DllImport("SeServ.dll")]
		public static extern int TcProtect_GetInterval();

		[DllImport("SeServ.dll")]
		public static extern IntPtr TcProtect_GetSeDataMD5();

		public int TcProtectInterval { get; private set; }

		public byte[] TcProtectMd5 { get; private set; }

		public byte[] TcProtectEncoded { get; private set; }

		private void InitializeFsw()
		{
			string fullPath = Path.GetFullPath("TcProtect.xml");
			FileInfo fileInfo = new FileInfo(fullPath);
			if (fileInfo.Exists)
			{
				this.fsw.Path = Path.GetDirectoryName(fullPath);
				this.fsw.Filter = Path.GetFileName(fullPath);
				this.fsw.NotifyFilter = (NotifyFilters.FileName | NotifyFilters.Attributes | NotifyFilters.Size | NotifyFilters.LastWrite | NotifyFilters.LastAccess | NotifyFilters.CreationTime | NotifyFilters.Security);
				this.fsw.Changed += this.fsw_Changed;
				this.fsw.EnableRaisingEvents = true;
			}
		}

		private void fsw_Changed(object sender, FileSystemEventArgs e)
		{
			try
			{
				Scheduler.Schedule(base.Thread, Job.Create(new Action(this.UpdateTcProtect)), 0);
			}
			catch (Exception ex)
			{
				Log<HackShieldService>.Logger.Error(ex);
			}
		}

		private HackShieldService()
		{
			if (this.serverHandle.IsInvalid)
			{
				Log<HackShieldService>.Logger.Warn("Error while Making Hackshield handler.");
			}
		}

		internal int NextMilliseconds()
		{
			return this.cycle[0] + this.random.Next(-this.cycle[1], this.cycle[1]);
		}

		internal void UpdateTcProtect()
		{
			try
			{
				this.TcProtectInterval = HackShieldService.TcProtect_GetInterval();
			}
			catch (Exception ex)
			{
				Log<HackShieldService>.Logger.Error(ex);
			}
			try
			{
				IntPtr ptr = HackShieldService.TcProtect_GetSeDataMD5();
				string s = Marshal.PtrToStringAnsi(ptr);
				this.TcProtectMd5 = Encoding.ASCII.GetBytes(s);
			}
			catch (Exception ex2)
			{
				Log<HackShieldService>.Logger.Error(ex2);
			}
			try
			{
				IntPtr intPtr;
				IntPtr source = HackShieldService.TcProtect_GetXmlData(out intPtr);
				int num = intPtr.ToInt32();
				this.TcProtectEncoded = new byte[num];
				Marshal.Copy(source, this.TcProtectEncoded, 0, num);
			}
			catch (Exception ex3)
			{
				Log<HackShieldService>.Logger.Error(ex3);
			}
		}

		internal int TcProtectNextMilliseconds()
		{
			return this.TcProtectInterval;
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

		public override void Initialize(JobProcessor thread)
		{
			ConnectionStringLoader.LoadFromServiceCore(Settings.Default);
			base.Initialize(thread, MessageID.TypeConverters);
			base.RegisterMessage(OperationMessages.TypeConverters);
			base.RegisterProcessor(typeof(Respond), (Operation op) => new RespondProcessor(this, op as Respond));
			if (FeatureMatrix.IsEnable("TcProtect"))
			{
				this.InitializeFsw();
				Scheduler.Schedule(base.Thread, Job.Create(new Action(this.UpdateTcProtect)), 0);
				base.RegisterProcessor(typeof(TcProtectRespond), (Operation op) => new TcProtectRespondProcessor(this, op as TcProtectRespond));
				base.RegisterProcessor(typeof(TcProtectCheck), (Operation op) => new TcProtectCheckProcessor(this, op as TcProtectCheck));
			}
		}

		protected override IEntity MakeEntity(long id, string category)
		{
			AntiCpXSvr.SafeClientHandle safeClientHandle = new AntiCpXSvr.SafeClientHandle(this.serverHandle);
			if (safeClientHandle.IsInvalid)
			{
				Log<HackShieldService>.Logger.WarnFormat("Error while making Hackshield handler : {0}", id);
				return null;
			}
			IEntity entity = base.MakeEntity(id, category);
			entity.Tag = new HackShieldClient(this, entity, safeClientHandle);
			entity.Using += delegate(object sender, EventArgs<IEntityAdapter> e)
			{
				IEntityAdapter value = e.Value;
				if (entity.Tag == null)
				{
					return;
				}
				if (value.RemoteCategory == "FrontendServiceCore.FrontendService")
				{
					HackShieldClient hackShieldClient = entity.Tag as HackShieldClient;
					if (hackShieldClient.FrontendConn != null)
					{
						hackShieldClient.FrontendConn.Close();
					}
					hackShieldClient.FrontendConn = this.Connect(entity, new Location(value.RemoteID, value.RemoteCategory));
					hackShieldClient.FrontendConn.Closed += delegate(object _, EventArgs<IEntityProxy> __)
					{
						entity.Close();
					};
					hackShieldClient.FrontendConn.OperationQueueOversized += delegate(object _, EventArgs<IEntityProxy> __)
					{
						entity.Close(true);
					};
				}
			};
			entity.Closed += delegate(object sender, EventArgs e)
			{
				try
				{
					EntityDataContext entityDataContext = new EntityDataContext();
					entityDataContext.AcquireService(new long?((sender as IEntity).ID), base.Category, new int?(-1), new int?(base.ID));
				}
				catch (Exception ex)
				{
					Log<HackShieldService>.Logger.ErrorFormat("Entity_Closed [EntityID : {0}] [ServiceID : {1}] [Category : {2}] - {3} ", new object[]
					{
						(sender as IEntity).ID,
						base.ID,
						base.Category,
						ex
					});
				}
			};
			entity.Used += delegate(object sender, EventArgs<IEntityAdapter> e)
			{
				if (entity.UseCount == 0)
				{
					entity.Close();
				}
			};
			if (!FeatureMatrix.IsEnable("HackShieldConnCheckOff"))
			{
				Scheduler.Schedule(base.Thread, Job.Create(new Action((entity.Tag as HackShieldClient).MakeRequest)), this.NextMilliseconds());
			}
			if (FeatureMatrix.IsEnable("TcProtect"))
			{
				Scheduler.Schedule(base.Thread, Job.Create(new Action((entity.Tag as HackShieldClient).MakeTcProtectRequest)), 0);
			}
			return entity;
		}

		public static Service StartService(string ip, string portstr)
		{
			HackShieldService hackShieldService = new HackShieldService();
			ServiceInvoker.StartService(ip, portstr, hackShieldService);
			HackShieldService.StartReporting(hackShieldService);
			return hackShieldService;
		}

		private static void StartReporting(HackShieldService serv)
		{
			if (!FeatureMatrix.IsEnable("ServiceReporter"))
			{
				return;
			}
			int interval = ServiceReporterSettings.GetInterval("HackShieldService", 60);
			ServiceReporter.Instance.Initialize("HackShieldService");
			ServiceReporter.Instance.AddGathering("Stat", new ServiceReporter.GatheringDelegate<int>(serv.OnGatheringStat));
			ServiceReporter.Instance.Start(interval * 1000);
		}

		private void OnGatheringStat(Dictionary<string, int> dict)
		{
			dict.InsertOrIncrease("Entity", (int)this.GetEntityCount());
			dict.InsertOrIncrease("Queue", (int)this.GetQueueLength());
		}

		private AntiCpXSvr.SafeServerHandle serverHandle = new AntiCpXSvr.SafeServerHandle(Path.GetFullPath("AntiCpX.hsb"));

		private readonly int[] cycle = new int[]
		{
			(int)(new TimeSpan(0, 1, 0).Ticks / 10000L),
			(int)(new TimeSpan(0, 0, 15).Ticks / 10000L)
		};

		private Random random = new Random();

		private FileSystemWatcher fsw = new FileSystemWatcher();
	}
}
