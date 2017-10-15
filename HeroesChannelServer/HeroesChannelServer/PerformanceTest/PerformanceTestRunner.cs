using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using Devcat.Core;
using Devcat.Core.Threading;
using MMOServer;
using ServiceCore;
using Utility;

namespace HeroesChannelServer.PerformanceTest
{
	public class PerformanceTestRunner
	{
		public double ActionUpdateRatio { get; set; }

		public double LookUpdateRatio { get; set; }

		public double PartitionMoveRatio { get; set; }

		public double ChannelMoveRatio { get; set; }

		public int MaxChannels { get; set; }

		public int MaxClients { get; set; }

		public int ClientPerChannel { get; set; }

		public int ClientJoinInterval { get; set; }

		public int ThreadCount { get; set; }

		[Category("상태")]
		public int ReceivedPacketCount { get; internal set; }

		[Category("상태")]
		public int StartCount
		{
			get
			{
				return this.startCount;
			}
		}

		[Category("상태")]
		public int ClientCount
		{
			get
			{
				return this.clients.Count;
			}
		}

		[Category("성능")]
		public PerformanceTestRunner.ThreadPerformance MainThread_Perf { get; set; }

		[Category("성능")]
		public PerformanceTestRunner.ThreadPerformance[] Thread_Perfs { get; set; }

		public event EventHandler<EventArgs<Exception>> ExceptionOccur;

		private void InitializeMaps()
		{
			this.maps.Clear();
			string[] files = Directory.GetFiles(ServiceCoreSettings.Default.MMOChannelMapFilePath, "*.dat", SearchOption.AllDirectories);
			foreach (string text in files)
			{
				Log<PerformanceTestRunner>.Logger.InfoFormat("Loading {0}", text);
				string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(text);
				Map value = new Map(fileNameWithoutExtension, text);
				this.maps.Add(fileNameWithoutExtension, value);
			}
		}

		private void InitializeChannels(int channelCount)
		{
			this.channels.Clear();
			if (channelCount <= 0)
			{
				return;
			}
			for (int i = 0; i < channelCount; i++)
			{
				Channel channel = new Channel();
				foreach (Map map in this.maps.Values)
				{
					map.Build(channel);
				}
				channel.ConfirmRegions(3);
				this.channels[(long)i] = channel;
			}
			this.AssignThreads();
		}

		private void InitializeClients(int clientCount)
		{
			this.clients.Clear();
			this.ReceivedPacketCount = 0;
			int num = 0;
			int num2 = 0;
			for (int i = 0; i < clientCount; i++)
			{
				if (num2 < this.channels.Count)
				{
					num++;
					if (num >= this.ClientPerChannel)
					{
						num2++;
						num = 0;
					}
				}
				if (this.channels.ContainsKey((long)num2))
				{
					this.EnqueueMakeClient(i, this.channels[(long)num2], i * this.ClientJoinInterval);
				}
			}
		}

		private void InitializeThreads(int threadCount)
		{
			foreach (JobProcessor jobProcessor in this.threads)
			{
				jobProcessor.Stop();
			}
			this.threads.Clear();
			this.Thread_Perfs = null;
			if (threadCount <= 0)
			{
				return;
			}
			this.Thread_Perfs = new PerformanceTestRunner.ThreadPerformance[threadCount];
			for (int i = 0; i < threadCount; i++)
			{
				JobProcessor jobProcessor2 = new JobProcessor();
				jobProcessor2.ExceptionOccur += this.thread_ExceptionOccur;
				PerformanceTestRunner.ThreadPerformance threadPerformance = PerformanceTestRunner.ThreadPerformance.MakePerf(jobProcessor2);
				threadPerformance.ExceptionOccur += this.thread_ExceptionOccur;
				threadPerformance.Name = string.Format("Thread{0}", i);
				this.Thread_Perfs[i] = threadPerformance;
				jobProcessor2.Start();
				this.threads.Add(jobProcessor2);
			}
			this.AssignThreads();
		}

		private void AssignThreads()
		{
			if (this.channels.Count <= 0 || this.threads.Count <= 0)
			{
				return;
			}
			int count = this.threads.Count;
			int num = 0;
			foreach (Channel channel in this.channels.Values)
			{
				channel.Tag = this.threads[num];
				num++;
				num %= count;
			}
		}

		private void MakeMockClient(int i, Channel channel)
		{
			long initialPartitionID;
			if (i % 2 == 0)
			{
				initialPartitionID = this.colhenStartPartition;
			}
			else
			{
				initialPartitionID = this.rochestStartPartition;
			}
			MockClient mockClient = new MockClient((long)i, initialPartitionID, this);
			this.clients[i] = mockClient;
			if (channel != null)
			{
				mockClient.EnterChannel(channel);
			}
		}

		private void EnqueueMakeClient(int i, Channel channel, int delay)
		{
			Scheduler.Schedule(this.thread, Job.Create<int, Channel>(new Action<int, Channel>(this.MakeMockClient), i, channel), delay);
		}

		public PerformanceTestRunner()
		{
			this.ActionUpdateRatio = 1.0;
			this.LookUpdateRatio = 60.0;
			this.PartitionMoveRatio = 10.0;
			this.ChannelMoveRatio = 300.0;
			this.ClientPerChannel = 500;
			this.MaxChannels = 200;
			this.MaxClients = 2000;
			this.startCount = 0;
			this.ClientJoinInterval = 10;
			this.ThreadCount = 4;
		}

		private void StartActionUpdate(int curCount)
		{
			if (this.startCount != curCount)
			{
				return;
			}
			foreach (MockClient mockClient in this.clients.Values)
			{
				JobProcessor boundThread = this.thread;
				if (mockClient.Player.BoundThread != null)
				{
					boundThread = mockClient.Player.BoundThread;
				}
				boundThread.Enqueue(Job.Create(new Action(mockClient.UpdateAction)));
			}
			Scheduler.Schedule(this.thread, Job.Create<int>(new Action<int>(this.StartActionUpdate), curCount), (int)(this.ActionUpdateRatio * 1000.0));
		}

		private void StartLookUpdate(int curCount)
		{
			if (this.startCount != curCount)
			{
				return;
			}
			foreach (MockClient mockClient in this.clients.Values)
			{
				JobProcessor boundThread = this.thread;
				if (mockClient.Player.BoundThread != null)
				{
					boundThread = mockClient.Player.BoundThread;
				}
				boundThread.Enqueue(Job.Create(new Action(mockClient.UpdateLook)));
			}
			Scheduler.Schedule(this.thread, Job.Create<int>(new Action<int>(this.StartLookUpdate), curCount), (int)(this.LookUpdateRatio * 1000.0));
		}

		private void StartPartitionMove(int curCount)
		{
			if (this.startCount != curCount)
			{
				return;
			}
			foreach (MockClient mockClient in this.clients.Values)
			{
				JobProcessor boundThread = this.thread;
				if (mockClient.Player.BoundThread != null)
				{
					boundThread = mockClient.Player.BoundThread;
				}
				boundThread.Enqueue(Job.Create(new Action(mockClient.PartitionMove)));
			}
			Scheduler.Schedule(this.thread, Job.Create<int>(new Action<int>(this.StartPartitionMove), curCount), (int)(this.PartitionMoveRatio * 1000.0));
		}

		private void StartChannelMove(int curCount)
		{
		}

		private void StartTimers(int curCount)
		{
			this.StartActionUpdate(curCount);
			this.StartLookUpdate(curCount);
			this.StartPartitionMove(curCount);
			this.StartChannelMove(curCount);
		}

		public void Start()
		{
			if (this.thread != null)
			{
				this.thread.Stop();
			}
			this.startCount++;
			int arg = this.startCount;
			this.thread = new JobProcessor();
			this.MainThread_Perf = PerformanceTestRunner.ThreadPerformance.MakePerf(this.thread);
			this.MainThread_Perf.Name = "Main Thread";
			this.thread.ExceptionOccur += this.thread_ExceptionOccur;
			this.thread.Enqueue(Job.Create(new Action(this.InitializeMaps)));
			this.thread.Enqueue(Job.Create<int>(new Action<int>(this.InitializeChannels), this.MaxChannels));
			this.thread.Enqueue(Job.Create<int>(new Action<int>(this.InitializeClients), this.MaxClients));
			this.thread.Enqueue(Job.Create<int>(new Action<int>(this.InitializeThreads), this.ThreadCount));
			this.thread.Enqueue(Job.Create<int>(new Action<int>(this.StartTimers), arg));
			this.thread.Start();
		}

		private void thread_ExceptionOccur(object sender, EventArgs<Exception> e)
		{
			if (this.ExceptionOccur != null)
			{
				this.ExceptionOccur(sender, e);
			}
		}

		public void Stop()
		{
			if (this.thread != null)
			{
				this.thread.Stop();
			}
			this.thread = null;
			foreach (JobProcessor jobProcessor in this.threads)
			{
				jobProcessor.Stop();
			}
		}

		private Dictionary<long, Channel> channels = new Dictionary<long, Channel>();

		private Dictionary<string, Map> maps = new Dictionary<string, Map>();

		private Dictionary<int, MockClient> clients = new Dictionary<int, MockClient>();

		private List<JobProcessor> threads = new List<JobProcessor>();

		private long colhenStartPartition = 6479215318592913498L;

		private long rochestStartPartition = 6479215318592913497L;

		private JobProcessor thread;

		private int startCount;

		[TypeConverter(typeof(ExpandableObjectConverter))]
		public class ThreadPerformance
		{
			public static PerformanceTestRunner.ThreadPerformance MakePerf(JobProcessor thread)
			{
				PerformanceTestRunner.ThreadPerformance threadPerformance = new PerformanceTestRunner.ThreadPerformance();
				threadPerformance.BindThread(thread);
				return threadPerformance;
			}

			public string Name { get; set; }

			[Description("Wait_Average가 최근 몇 개 작업의 평균인지를 나타냅니다.")]
			public long Wait_JobCount { get; private set; }

			[Description("Proc_Average가 최근 몇 개 작업의 평균인지를 나타냅니다.")]
			public long Proc_JobCount { get; private set; }

			[Description("작업이 큐에 들어간 뒤 실제로 시작될 때 까지 대기 시간입니다.")]
			public double Wait_Average
			{
				get
				{
					return (double)this.totalWait / (double)this.Wait_JobCount / (double)Stopwatch.Frequency;
				}
			}

			[Description("실제로 작업이 완수되는 데 걸린 시간입니다.")]
			public double Proc_Average
			{
				get
				{
					return (double)this.totalProc / (double)this.Proc_JobCount / (double)Stopwatch.Frequency;
				}
			}

			[Description("스레드에 현재 몇 개의 작업이 대기중인지 나타냅니다.")]
			public int QueueLength
			{
				get
				{
					return this.thread.JobCount;
				}
			}

			public long JobCount { get; private set; }

			public event EventHandler<EventArgs<Exception>> ExceptionOccur;

			public void BindThread(JobProcessor thread)
			{
				this.thread = thread;
				thread.ExceptionOccur += this.thread_ExceptionOccur;
				thread.Dequeued += this.thread_Dequeued;
				thread.Done += this.thread_Done;
			}

			private void thread_Done(object sender, EventArgs<IJob> e)
			{
				IJob value = e.Value;
				long num = value.EndTick - value.StartTick;
				if (value.EndTick <= 0L || value.StartTick <= 0L || num < 0L)
				{
					this.thread_ExceptionOccur(this, new EventArgs<Exception>(new Exception(string.Format("끝도 안난 잡이... {0} ~ {1}", value.StartTick, value.EndTick))));
					return;
				}
				if (this.Proc_JobCount >= 100L)
				{
					this.Proc_JobCount = 0L;
					this.totalProc = 0L;
				}
				this.Proc_JobCount += 1L;
				this.totalProc += num;
				this.JobCount += 1L;
			}

			private void thread_Dequeued(object sender, EventArgs<IJob> e)
			{
				IJob value = e.Value;
				long num = value.StartTick - value.EnqueueTick;
				if (value.EnqueueTick <= 0L || value.StartTick <= 0L || num < 0L)
				{
					this.thread_ExceptionOccur(this, new EventArgs<Exception>(new Exception(string.Format("시작도 안된 잡이... {0} ~ {1}", value.EnqueueTick, value.StartTick))));
					return;
				}
				if (this.Wait_JobCount >= 100L)
				{
					this.Wait_JobCount = 0L;
					this.totalWait = 0L;
				}
				this.Wait_JobCount += 1L;
				this.totalWait += num;
			}

			private void thread_ExceptionOccur(object sender, EventArgs<Exception> e)
			{
				if (this.ExceptionOccur != null)
				{
					this.ExceptionOccur(sender, e);
				}
			}

			private JobProcessor thread;

			private long totalWait;

			private long totalProc;
		}
	}
}
