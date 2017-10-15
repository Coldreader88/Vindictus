using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Management;
using System.Threading;
using Devcat.Core;
using Devcat.Core.Net;
using Devcat.Core.Net.Message;
using Devcat.Core.Threading;
using ExecutionSupporterClient.Properties;
using ExecutionSupporterMessages;
using Utility;

namespace ExecutionSupporterClient
{
	public class ExecutionSupporterClient
	{
		public JobProcessor JobProcessor { get; set; }

		public TcpServer TcpServer { get; set; }

		public TcpClient TcpClient { get; set; }

		private int CoreCount { get; set; }

		private FileList FileList { get; set; }

		public ExecutionSupporterClient()
		{
			this.JobProcessor = new JobProcessor();
			this.JobProcessor.Start();
			this.TcpServer = new TcpServer();
			this.TcpServer.Start(this.JobProcessor, Settings.Default.Port);
			this.TcpServer.ClientAccept += this.OnClientAccepted;
			this.CPUCounter = new PerformanceCounter();
			this.CPUCounter.CategoryName = "Processor";
			this.CPUCounter.CounterName = "% Processor Time";
			this.CPUCounter.InstanceName = "_Total";
			this.MemUsedCounter = new PerformanceCounter("Memory", "Committed Bytes");
			this.MemLimitCounter = new PerformanceCounter("Memory", "Commit Limit");
			this.CoreCount = ExecutionSupporterClient.GetCoreCount();
			this.FileList = new FileList(Settings.Default.ServerBin, Settings.Default.DSBin);
			this.FileList.ExceptionOccurred += delegate(Exception ex)
			{
				Console.WriteLine("exception occurred in FileList : {0}", ex);
			};
			this.MF.Register<ExecutionSupporterClient>(Messages.TypeConverters, "ProcessMessage");
		}

		private static int GetCoreCount()
		{
			int num = 0;
			try
			{
				foreach (ManagementBaseObject managementBaseObject in new ManagementClass("Win32_Processor").GetInstances())
				{
					try
					{
						num += int.Parse(managementBaseObject.Properties["NumberOfCores"].Value.ToString());
					}
					catch (Exception arg)
					{
						Console.WriteLine("Cannot get core count : {0}", arg);
						num++;
					}
				}
			}
			catch (Exception arg2)
			{
				Console.WriteLine("Cannot get processor instance : {0}", arg2);
				num = 1;
			}
			return num;
		}

		private void OnClientAccepted(object sender, AcceptEventArgs e)
		{
			e.PacketAnalyzer = new MessageAnalyzer();
			e.Client.ConnectionSucceed += delegate(object caller, EventArgs ev)
			{
				if (this.TcpClient == null)
				{
					this.TcpClient = e.Client;
					this.TcpClient.Disconnected += this.OnClientDisconnected;
					this.TcpClient.PacketReceive += this.OnPacketReceive;
					Console.WriteLine("Execution Supporter Connected from {0}", e.Client.RemoteEndPoint.Address);
					return;
				}
				e.Client.Disconnect();
				Console.WriteLine("Duplicated connection from {0} Disconnect.", e.Client.RemoteEndPoint.Address);
			};
		}

		private void OnClientDisconnected(object sender, EventArgs e)
		{
			if (this.TcpClient == sender)
			{
				this.TcpClient = null;
				Console.WriteLine("Execution Supporter Disconnected");
				return;
			}
			Console.WriteLine("Unknown Tcp Disconnection detected.");
		}

		private void OnPacketReceive(object sender, EventArgs<ArraySegment<byte>> e)
		{
			Packet packet = new Packet(e.Value);
			this.MF.Handle(packet, this);
		}

		private void SendMessage<T>(T message)
		{
			if (this.TcpClient != null)
			{
				this.TcpClient.Transmit(SerializeWriter.ToBinary<T>(message));
			}
		}

		private static void Main(string[] args)
		{
			new ExecutionSupporterClient();
			Console.Clear();
			Console.WriteLine("Execution Supporter Client Initialized : {0}", Settings.Default.Port);
		}

		public void ProcessMessage(object message)
		{
			if (message is QueryClientReportMessage)
			{
				QueryClientReportMessage queryClientReportMessage = message as QueryClientReportMessage;
				this.SendServerInfo(queryClientReportMessage.IsDS);
				return;
			}
			if (message is ExecuteMessage)
			{
				ExecuteMessage executeMessage = message as ExecuteMessage;
				this.ExecuteCommand(executeMessage.TargetFile, executeMessage.Args);
			}
		}

		private void SendServerInfo(bool includeDS)
		{
			this.lasttime = DateTime.Now;
			List<ProcessReport> list = new List<ProcessReport>();
			Dictionary<int, TimeSpan> dictionary = new Dictionary<int, TimeSpan>();
			foreach (Process process in Process.GetProcesses())
			{
				if (process.MainWindowTitle.Length > 0 && process.ProcessName == "Executer")
				{
					dictionary.Add(process.Id, process.TotalProcessorTime);
				}
			}
			Thread.Sleep(300);
			foreach (Process process2 in Process.GetProcesses())
			{
				if (process2.MainWindowTitle.Length > 0 && process2.ProcessName == "Executer")
				{
					float memory = (float)process2.PrivateMemorySize64 / 1048576f;
					TimeSpan t = dictionary.TryGetValue(process2.Id);
					float cpu = (float)(process2.TotalProcessorTime - t).TotalMilliseconds / 300f * 100f;
					list.Add(new ProcessReport(process2.MainWindowTitle, memory, cpu, process2.Responding));
				}
			}
			ClientReportMessage message = new ClientReportMessage
			{
				CoreCount = this.CoreCount,
				ProcessList = list,
				cpuUsage = this.CPUCounter.NextValue(),
				memUsage = this.MemUsedCounter.NextValue(),
				memLimit = this.MemLimitCounter.NextValue(),
				LatestServerFile = this.FileList.GetLatestServerFile(),
				LatestServerFileTime = this.FileList.GetLatestServerFileTime(),
				LatestClientFile = this.FileList.GetLatestDSFile(),
				LatestClientFileTime = this.FileList.GetLatestDSFileTime()
			};
			this.SendMessage<ClientReportMessage>(message);
		}

		public void ExecuteCommand(string file, string arg)
		{
			Console.WriteLine("{0} Executing {1} {2}....", DateTime.Now.ToString("[hh:mm:ss]"), file, arg);
			ProcessStartInfo processStartInfo = new ProcessStartInfo(file);
			processStartInfo.WorkingDirectory = Settings.Default.ServerBin;
			processStartInfo.WindowStyle = ProcessWindowStyle.Minimized;
			processStartInfo.Arguments = arg;
			try
			{
				Process.Start(processStartInfo);
			}
			catch (Exception ex)
			{
				Console.WriteLine(string.Format("{0} {1} {2} {3}", new object[]
				{
					Settings.Default.ServerBin,
					file,
					arg,
					ex.Message
				}));
			}
		}

		private MessageHandlerFactory MF = new MessageHandlerFactory();

		private PerformanceCounter CPUCounter;

		private PerformanceCounter MemUsedCounter;

		private PerformanceCounter MemLimitCounter;

		private DateTime lasttime;
	}
}
