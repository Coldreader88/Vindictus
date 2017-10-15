using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using Devcat.Core.Threading;
using log4net;
using log4net.Appender;
using log4net.Core;
using log4net.Repository.Hierarchy;
using ServiceCore.Configuration;
using UnifiedNetwork.OperationService;
using Utility;

namespace ServiceCore
{
	public class ServiceReporter : AppenderSkeleton
	{
		public JobProcessor JobProc { get; private set; }

		public bool IsInitialized
		{
			get
			{
				return this.InitTime != DateTime.MaxValue;
			}
		}

		public string SessionID
		{
			get
			{
				return string.Format("{0}({1})", this.InitTime.ToString("MMdd_HHmmss"), this.ProcessID);
			}
		}

		private string Category
		{
			get
			{
				if (this.LocalServiceOrder == 0)
				{
					return this.ServiceName;
				}
				return string.Format("{0}_{1}", this.ServiceName, this.LocalServiceOrder.ToString("00"));
			}
		}

		private string SessionKey
		{
			get
			{
				return string.Format("{0}", this.RowNumber);
			}
		}

		private string CsvFileName
		{
			get
			{
				return string.Format("csv\\[{0}][{1}]{2}.csv", this.ServiceName, this.MachineIP, this.SessionID);
			}
		}

		private ServiceReporter()
		{
			this.FileWatcher = new FileSystemWatcher();
			this.LogCounter = new Dictionary<string, int>(StringComparer.InvariantCultureIgnoreCase)
			{
				{
					"Fatal",
					0
				},
				{
					"Error",
					0
				},
				{
					"Warn",
					0
				},
				{
					"Info",
					0
				},
				{
					"Debug",
					0
				}
			};
			this.LogEntries = new List<ServiceReporter.LogEntry>();
			this.Subjects = new List<ServiceReporter.SubjectEntry>();
			this.InitTime = DateTime.MaxValue;
			base.Threshold = Level.Error;
		}

		public void Release()
		{
		}

		public void Initialize(Service service)
		{
			this.LocalServiceOrder = service.LocalServiceOrder;
			string[] array = service.Category.Split(new char[]
			{
				'.'
			});
			if (array.Count<string>() > 1)
			{
				this.Initialize(array[0]);
				return;
			}
			this.Initialize(service.Category);
		}

		private string GetMachineIP()
		{
			IPAddress ipaddress = Network.GetPrivateIPAddress();
			if (ipaddress == null)
			{
				ipaddress = Network.GetIPAddress();
			}
			if (ipaddress == null)
			{
				Log<ServiceReporter>.Logger.ErrorFormat("cannot found MachineIP. category:{0}", this.Category);
				return string.Empty;
			}
			return ipaddress.ToString();
		}

		public void Initialize(string serviceName)
		{
			try
			{
				this.ServiceName = serviceName;
				this.GameCode = FeatureMatrix.GameCode;
				this.ServerCode = FeatureMatrix.ServerCode;
				this.RowNumber = 0;
				this.MachineIP = this.GetMachineIP();
				this.Proc = Process.GetCurrentProcess();
				this.ProcessID = this.Proc.Id;
				this.CpuUsage = new CpuUsage();
				this.FileWatcher.Path = Directory.GetCurrentDirectory();
				this.FileWatcher.Filter = "ServiceCore.dll.config";
				this.FileWatcher.NotifyFilter = (NotifyFilters.LastWrite | NotifyFilters.LastAccess);
				this.FileWatcher.Changed += this.SettingChanged;
				this.FileWatcher.EnableRaisingEvents = true;
				((Hierarchy)LogManager.GetRepository()).Root.AddAppender(this);
				int interval = ServiceReporterSettings.GetInterval(this.ServiceName + ".Perf", 60);
				int interval2 = ServiceReporterSettings.GetInterval(this.ServiceName + ".Log", 60);
				this.AddPerfSubject("Perf", interval * 1000, new ServiceReporter.GatheringDelegate<int>(this.OnGathringPerf));
				this.AddLogSubject("Log", interval2 * 1000, new ServiceReporter.GatheringDelegate<int>(this.OnGatheringLog));
				int interval3 = ServiceReporterSettings.GetInterval(this.ServiceName + ".Stat", 60);
				this.AddSubject("Stat", null, interval3 * 1000);
				int interval4 = ServiceReporterSettings.GetInterval("ProfileScope", 3600);
				this.AddProfileScopeSubject("ProfileScope", interval4 * 1000, new ServiceReporter.GatheringDelegate<double>(this.OnGatheringProfileScope));
				this.InitTime = DateTime.Now;
				if (!Directory.Exists("csv"))
				{
					Directory.CreateDirectory("csv");
				}
				this.WritePreds();
			}
			catch (Exception ex)
			{
				Log<ServiceReporter>.Logger.Error("ServiceReporter.Initialize() FAILED!", ex);
			}
		}

		public void Start(int reportInterval)
		{
			if (reportInterval <= 1000)
			{
				return;
			}
			Log<ServiceReporter>.Logger.InfoFormat("ServiceReporter.Start() : Interval={0}", reportInterval);
			this.JobProc = new JobProcessor();
			this.JobProc.Start();
			ScheduleRepeater.Schedule(this.JobProc, Job.Create(new Action(this.ReportData)), reportInterval, false);
			foreach (ServiceReporter.SubjectEntry subjectEntry in this.Subjects)
			{
				if (subjectEntry.GatheringInterval >= 1000)
				{
					ScheduleRepeater.Schedule(this.JobProc, Job.Create<ServiceReporter.SubjectEntry>(new Action<ServiceReporter.SubjectEntry>(this.DoGathering), subjectEntry), subjectEntry.GatheringInterval, false);
				}
			}
		}

		private void Stop()
		{
			if (this.JobProc != null)
			{
				this.JobProc.Stop();
				this.JobProc = null;
			}
		}

		private void SettingChanged(object sender, FileSystemEventArgs e)
		{
			try
			{
				Log<ServiceReporter>.Logger.Info("ServiceReporter.SettingChanged()");
				ConfigurationManager.RefreshSection("ServiceReporterSettings");
				foreach (ServiceReporter.SubjectEntry subjectEntry in this.Subjects)
				{
					int num = ServiceReporterSettings.GetInterval(this.ServiceName + subjectEntry.SubjName, subjectEntry.GatheringInterval) * 1000;
					num = Math.Max(num, 1000);
					subjectEntry.GatheringInterval = num;
				}
				this.Stop();
				int num2 = ServiceReporterSettings.GetInterval(this.ServiceName, 60) * 1000;
				if (num2 > 1)
				{
					this.Start(num2);
				}
			}
			catch (Exception)
			{
			}
		}

		protected override void Append(LoggingEvent loggingEvent)
		{
			this.LogCounter.InsertOrIncrease(loggingEvent.Level.Name, 1);
			if (loggingEvent.Level >= Level.Error)
			{
				this.AddLog(loggingEvent.Domain, loggingEvent.Level.Name, loggingEvent.LocationInformation.FullInfo, loggingEvent.RenderedMessage);
			}
		}

		public void DoGathering(ServiceReporter.SubjectEntry subj)
		{
			try
			{
				if (this.IsInitialized)
				{
					Log<ServiceReporter>.Logger.InfoFormat("DoGathering() : {0}", subj.SubjName);
					subj.DoGathering();
				}
			}
			catch (Exception ex)
			{
				Log<ServiceReporter>.Logger.Error(ex.Message);
			}
		}

		public void AddLog(string strDomain, string strLevel, string strLocation, string strMsg)
		{
			if (strMsg.Length > 7168)
			{
				strMsg = strMsg.Substring(0, 7168);
			}
			ServiceReporter.LogEntry logEntry = new ServiceReporter.LogEntry();
			logEntry.domain = strDomain;
			logEntry.level = strLevel;
			logEntry.location = strLocation;
			logEntry.msg = strMsg;
			logEntry.tm = DateTime.Now;
			this.LogEntries.Add(logEntry);
		}

		private void WritePreds()
		{
			if (FeatureMatrix.IsEnable("ServiceReporter_ToDB"))
			{
				this.WritePredsToDB();
			}
			if (FeatureMatrix.IsEnable("ServiceReporter_ToCSV"))
			{
				this.WritePredsToCsv();
			}
		}

		private void WritePredsToDB()
		{
			try
			{
				using (HeroesWebStatDataContext heroesWebStatDataContext = new HeroesWebStatDataContext
				{
					ObjectTrackingEnabled = false
				})
				{
					heroesWebStatDataContext.AddServiceReporterSession(new int?(this.GameCode), new int?(this.ServerCode), this.MachineIP, this.Category, this.SessionID);
				}
			}
			catch (Exception ex)
			{
				Log<ServiceReporter>.Logger.Error(ex);
			}
		}

		private void WritePredsToCsv()
		{
			using (StreamWriter streamWriter = new StreamWriter(this.CsvFileName, true, Encoding.Default))
			{
				streamWriter.WriteLine("@GameCode={0}", this.GameCode);
				streamWriter.WriteLine("@ServerCode={0}", this.ServerCode);
				streamWriter.WriteLine("@MachineIP={0}", this.MachineIP);
				streamWriter.WriteLine("@Category={0}", this.Category);
				streamWriter.WriteLine("@SessionID={0}", this.SessionID);
			}
		}

		private IEnumerable<object[]> GetReportData()
		{
			int i = 0;
			foreach (ServiceReporter.LogEntry log in this.LogEntries)
			{
				i++;
				yield return new object[]
				{
					this.RowNumber + i,
					log.tm,
					"Logging",
					"Level",
					log.level
				};
				yield return new object[]
				{
					this.RowNumber + i,
					log.tm,
					"Logging",
					"Location",
					log.location
				};
				yield return new object[]
				{
					this.RowNumber + i,
					log.tm,
					"Logging",
					"Message",
					log.msg
				};
			}
			foreach (ServiceReporter.SubjectEntry subject in this.Subjects)
			{
				foreach (ServiceReporter.SubjectStat stat in subject.StatHistory)
				{
					i++;
					foreach (KeyValuePair<string, object> kv in stat)
					{
						object[] array = new object[5];
						array[0] = this.RowNumber + i;
						array[1] = stat.tm;
						array[2] = subject.SubjName;
						object[] array2 = array;
						int num = 3;
						KeyValuePair<string, object> keyValuePair = kv;
						array2[num] = keyValuePair.Key;
						object[] array3 = array;
						int num2 = 4;
						KeyValuePair<string, object> keyValuePair2 = kv;
						array3[num2] = keyValuePair2.Value;
						yield return array;
					}
				}
			}
			yield break;
		}

		private void ReportData()
		{
			if (!this.IsInitialized)
			{
				return;
			}
			List<object[]> list = this.GetReportData().ToList<object[]>();
			if (FeatureMatrix.IsEnable("ServiceReporter_ToDB"))
			{
				this.ReportDataToDB(list);
			}
			if (FeatureMatrix.IsEnable("ServiceReporter_ToCSV"))
			{
				this.ReportDataToCsv(list);
			}
			this.RowNumber += list.Count<object[]>();
			this.LogEntries.Clear();
			foreach (ServiceReporter.SubjectEntry subjectEntry in this.Subjects)
			{
				subjectEntry.StatHistory.Clear();
			}
		}

        private void ReportDataToDB(IEnumerable<object[]> data)
        {
            if (true)
            {
                try
                {
                    try
                    {
                        using (HeroesWebStatDataContext heroesWebStatDataContext = new HeroesWebStatDataContext())
                        {
                            foreach (object[] current in data)
                            {
                                int value = (int)current[0];
                                DateTime value2 = (DateTime)current[1];
                                string text = (string)current[2];
                                string key = (string)current[3];
                                string value3 = Convert.ToString(current[4]);
                                if (text == "Logging")
                                {
                                    heroesWebStatDataContext.AddServiceReporterLog(this.SessionID, new int?(value), new DateTime?(value2), text, key, value3);
                                }
                                else
                                {
                                    heroesWebStatDataContext.AddServiceReporterIndicator(this.SessionID, new int?(value), new DateTime?(value2), text, key, value3);
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Log<ServiceReporter>.Logger.Error(ex);
                    }
                }
                catch (Exception)
                {

                }
                finally
                {
                }
            }
        }

        private void ReportDataToCsv(IEnumerable<object[]> data)
        {
            if (true)
            {
                try
                {
                    try
                    {
                        if (!Directory.Exists("csv"))
                        {
                            Directory.CreateDirectory("csv");
                            this.WritePredsToCsv();
                        }
                        using (StreamWriter streamWriter = new StreamWriter(this.CsvFileName, true, Encoding.Default))
                        {
                            foreach (object[] current in data)
                            {
                                streamWriter.WriteLine(string.Format("{0}\t{1}\t{2}\t{3}\t{4}", new object[]
                                {
                                    current[0],
                                    ((DateTime)current[1]).ToString("yy-MM-dd HH:mm:ss"),
                                    current[2],
                                    current[3],
                                    current[4]
                                }));
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Log<ServiceReporter>.Logger.Error(ex);
                    }
                }
                catch (Exception exception)
                {
                    Log<ServiceReporter>.Logger.Error(exception);
                }
                finally
                {
                }
            }
        }

        public bool AddSubject(string subject, string[] cols, int interval)
		{
			Log<ServiceReporter>.Logger.InfoFormat("ServiceReporter.AddSubject() : Sub={0}, Interval={1}", subject, interval);
			foreach (ServiceReporter.SubjectEntry subjectEntry in this.Subjects)
			{
				if (subjectEntry.SubjName == subject)
				{
					return false;
				}
			}
			ServiceReporter.SubjectEntry subjectEntry2 = new ServiceReporter.SubjectEntry();
			subjectEntry2.SubjName = subject;
			subjectEntry2.Cols = cols;
			subjectEntry2.GatheringInterval = interval;
			subjectEntry2.StatHistory = new List<ServiceReporter.SubjectStat>();
			this.Subjects.Add(subjectEntry2);
			return true;
		}

		public void AddGathering<T>(string subjName, ServiceReporter.GatheringDelegate<T> onGathering)
		{
			foreach (ServiceReporter.SubjectEntry subjectEntry in this.Subjects)
			{
				if (subjectEntry.SubjName == subjName)
				{
					subjectEntry.OnGathering += delegate(ServiceReporter.SubjectStat stat)
					{
						Dictionary<string, T> dictionary = new Dictionary<string, T>();
						onGathering(dictionary);
						stat.AddStats<T>(dictionary);
					};
				}
			}
		}

		public void AddGathering(string subjName, ServiceReporter.GatheringDelegate<int> onGathering)
		{
			this.AddGathering<int>(subjName, onGathering);
		}

		public void AddLogSubject(string subjName, int interval, ServiceReporter.GatheringDelegate<int> onGathering)
		{
			if (this.AddSubject(subjName, null, interval))
			{
				this.AddGathering(subjName, onGathering);
			}
		}

		public void AddPerfSubject(string subjName, int interval, ServiceReporter.GatheringDelegate<int> onGathering)
		{
			string[] cols = new string[]
			{
				"GatheringElapsed",
				"ReportingElapsed",
				"CPU",
				"CPU.Kernel%",
				"CPU.User%",
				"Memory.WorkingSet",
				"Memory.Private",
				"Memory.Virtual",
				"Memory.Paged",
				"Memory.GC.Total",
				"Thread",
				"Thread.Running"
			};
			if (this.AddSubject(subjName, cols, interval))
			{
				this.AddGathering("Perf", onGathering);
			}
		}

		public void AddProfileScopeSubject(string subjName, int interval, ServiceReporter.GatheringDelegate<double> onGathering)
		{
			string[] cols = new string[]
			{
				"Min",
				"Max",
				"Avg",
				"Total",
				"Count"
			};
			if (this.AddSubject(subjName, cols, interval))
			{
				this.AddGathering<double>(subjName, onGathering);
			}
		}

		private void OnGatheringLog(Dictionary<string, int> dict)
		{
			dict["Fatal"] = this.LogCounter["Fatal"];
			dict["Error"] = this.LogCounter["Error"];
		}

		private void OnGathringPerf(Dictionary<string, int> dict)
		{
			if (this.Proc == null)
			{
				return;
			}
			this.Proc.Refresh();
			int value = (int)(this.Proc.WorkingSet64 / 1024L);
			int value2 = (int)(this.Proc.PrivateMemorySize64 / 1024L);
			int value3 = (int)(this.Proc.VirtualMemorySize64 / 1024L);
			int value4 = (int)(this.Proc.PagedMemorySize64 / 1024L);
			int value5 = (int)(GC.GetTotalMemory(false) / 1024L);
			dict["Memory.WorkingSet"] = value;
			dict["Memory.Private"] = value2;
			dict["Memory.Virtual"] = value3;
			dict["Memory.Paged"] = value4;
			dict["Memory.GC.Total"] = value5;
			float value7;
			float value8;
			float value6 = this.CpuUsage.CalcUsage(this.Proc, out value7, out value8);
			dict["CPU"] = Convert.ToInt32(value6);
			dict["CPU.Kernel%"] = Convert.ToInt32(value7);
			dict["CPU.User%"] = Convert.ToInt32(value8);
			int count = this.Proc.Threads.Count;
			int value9 = (from x in this.Proc.Threads.OfType<ProcessThread>()
			where x.ThreadState == ThreadState.Running
			select x).Count<ProcessThread>();
			dict["Thread"] = count;
			dict["Thread.Running"] = value9;
		}

		private void OnGatheringProfileScope(Dictionary<string, double> dict)
		{
			foreach (ProfileScopeData profileScopeData in ProfileScope.DataList)
			{
				dict[profileScopeData.Tag + ".Avg"] = profileScopeData.AvgDuration;
				dict[profileScopeData.Tag + ".Count"] = (double)profileScopeData.TotalCallCount;
			}
			ProfileScope.Clear();
		}

		static ServiceReporter()
		{
			ServiceReporter.Instance = new ServiceReporter();
		}

		public static ServiceReporter Instance;

		private Process Proc;

		private DateTime InitTime;

		private int GameCode;

		private int ServerCode;

		private string MachineIP;

		private string ServiceName;

		private int LocalServiceOrder;

		private int ProcessID;

		private int RowNumber;

		private List<ServiceReporter.LogEntry> LogEntries;

		private Dictionary<string, int> LogCounter;

		private List<ServiceReporter.SubjectEntry> Subjects;

		private FileSystemWatcher FileWatcher;

		private CpuUsage CpuUsage;

		private class LogEntry
		{
			public string domain;

			public string level;

			public string location;

			public string msg;

			public DateTime tm;
		}

		public class SubjectStat : Dictionary<string, object>
		{
			public SubjectStat() : base(StringComparer.InvariantCultureIgnoreCase)
			{
			}

			public void AddStats<T>(Dictionary<string, T> dic)
			{
				foreach (KeyValuePair<string, T> keyValuePair in dic)
				{
					base[keyValuePair.Key] = keyValuePair.Value;
				}
			}

			public DateTime tm;
		}

		public class SubjectEntry
		{
			public event Action<ServiceReporter.SubjectStat> OnGathering;

			public void DoGathering()
			{
				if (this.OnGathering != null)
				{
					ServiceReporter.SubjectStat subjectStat = new ServiceReporter.SubjectStat();
					this.OnGathering(subjectStat);
					subjectStat.tm = DateTime.Now;
					this.StatHistory.Add(subjectStat);
				}
			}

			public string SubjName;

			public string[] Cols;

			public List<ServiceReporter.SubjectStat> StatHistory;

			public int GatheringInterval;
		}

		public delegate void GatheringDelegate<T>(Dictionary<string, T> val);
	}
}
