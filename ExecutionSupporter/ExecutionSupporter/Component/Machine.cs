using System;
using System.Collections.Generic;
using System.Drawing;
using System.Net;
using System.Text;
using System.Windows.Forms;
using Devcat.Core;
using Devcat.Core.Net;
using Devcat.Core.Net.Message;
using ExecutionSupporter.Properties;
using ExecutionSupporterMessages;
using Utility;

namespace ExecutionSupporter.Component
{
	public class Machine : ListViewItem, IComparable
	{
		public MachineManager MachineManager { get; set; }

		public ExecutionSupportCore Core
		{
			get
			{
				return this.MachineManager.Core;
			}
		}

		public TcpClient TcpClient { get; set; }

		public string HostName { get; set; }

		public IPAddress Address { get; set; }

		public List<string> Operations { get; set; }

		public ClientReportMessage Report { get; set; }

		public DateTime LastReportTime { get; set; }

		public MachineInfo MachineInfo { get; set; }

		public NetworkStatus NetworkStatus { get; set; }

		public int ProcessCount
		{
			get
			{
				if (this.Report == null)
				{
					return 0;
				}
				return this.Report.ProcessList.Count;
			}
		}

		public bool IsDSMachine
		{
			get
			{
				foreach (string text in this.Operations)
				{
					if (text.ToLower() == "DSService".ToLower())
					{
						return true;
					}
				}
				return false;
			}
		}

		public bool IsStarted
		{
			get
			{
				return this.Report != null && this.Report.ProcessList.Count >= this.Operations.Count;
			}
		}

		public bool IsUpdated
		{
			get
			{
				return this.Report != null && !(this.Report.LatestServerFileTime != this.MachineManager.LatestServerFileTime) && (!this.IsDSMachine || !(this.Report.LatestClientFileTime != this.MachineManager.LatestClientFileTime));
			}
		}

		public bool IsKilled
		{
			get
			{
				return this.Report != null && this.Report.ProcessList.Count == 0;
			}
		}

		public bool IsConnected
		{
			get
			{
				return this.NetworkStatus == NetworkStatus.Connected;
			}
		}

		public Machine(MachineManager serverManager, string hostName, List<string> operations, MachineInfo machineInfo)
		{
			this.MachineManager = serverManager;
			this.HostName = hostName;
			this.Operations = operations;
			this.MachineInfo = machineInfo;
			this.LastReportTime = DateTime.MinValue;
			this.TcpClient = new TcpClient();
			this.TcpClient.ConnectionFail += new EventHandler<EventArgs<Exception>>(this.OnConnectionFail);
			this.TcpClient.ConnectionSucceed += this.OnConnectionSucceed;
			this.TcpClient.PacketReceive += this.OnPacketReceive;
			this.TcpClient.Disconnected += this.OnDisconnected;
			this.NetworkStatus = NetworkStatus.Disconnected;
			this.UpdateListItem();
			this.UpdateDBRow();
		}

		public void TryConnect()
		{
			if (this.NetworkStatus != NetworkStatus.Disconnected)
			{
				return;
			}
			try
			{
				IPHostEntry hostEntry = Dns.GetHostEntry(this.HostName);
				this.Address = null;
				foreach (IPAddress ipaddress in hostEntry.AddressList)
				{
					if (this.Address == null)
					{
						this.Address = ipaddress;
					}
					if (ipaddress.IsPrivateNetwork())
					{
						this.Address = ipaddress;
						break;
					}
				}
			}
			catch
			{
				this.Address = null;
				return;
			}
			if (this.Address != null)
			{
				this.TcpClient.Connect(this.Core.JobProcessor, new IPEndPoint(this.Address, Settings.Default.SupporterClientPort), new MessageAnalyzer());
				this.NetworkStatus = NetworkStatus.Connecting;
			}
		}

		private void OnConnectionFail(object sender, EventArgs e)
		{
			this.NetworkStatus = NetworkStatus.Disconnected;
			this.UpdateDBRow();
			this.UpdateListItem();
		}

		private void OnConnectionSucceed(object sender, EventArgs e)
		{
			this.Core.LogManager.AddLog(LogType.ERROR, "Connected to {0} ({1} : {2})", new object[]
			{
				this.HostName,
				this.Address,
				Settings.Default.SupporterClientPort
			});
			this.NetworkStatus = NetworkStatus.Connected;
			this.UpdateDBRow();
			this.UpdateListItem();
			this.QueryClientReport();
		}

		private void OnDisconnected(object sender, EventArgs e)
		{
			this.Core.LogManager.AddLog(LogType.ERROR, "Disconnected from {0} ({1} : {2})", new object[]
			{
				this.HostName,
				this.Address,
				Settings.Default.SupporterClientPort
			});
			this.NetworkStatus = NetworkStatus.Disconnected;
			this.UpdateDBRow();
			this.UpdateListItem();
		}

		private void OnPacketReceive(object sender, EventArgs<ArraySegment<byte>> e)
		{
			Packet packet = new Packet(e.Value);
			this.MachineManager.MF.Handle(packet, this);
		}

		public void SendMessage<T>(T message)
		{
			if (this.TcpClient != null)
			{
				this.TcpClient.Transmit(SerializeWriter.ToBinary<T>(message));
			}
		}

		public void QueryClientReport()
		{
			this.SendMessage<QueryClientReportMessage>(new QueryClientReportMessage(this.IsDSMachine));
		}

		public void ExecuteFile(string file, params object[] args)
		{
			StringBuilder stringBuilder = new StringBuilder();
			foreach (object obj in args)
			{
				stringBuilder.Append(obj.ToString()).Append(' ');
			}
			this.SendMessage<ExecuteMessage>(new ExecuteMessage(file, stringBuilder.ToString()));
		}

		public void ExecuteServices()
		{
			foreach (string text in this.Operations)
			{
				this.ExecuteFile("Trigger_Service.bat", new object[]
				{
					text,
					Settings.Default.s,
					Settings.Default.LocationServicePort
				});
			}
		}

		public void ExecuteConsole(string command)
		{
			this.ExecuteFile("cmd.exe", new object[]
			{
				"/c",
				command,
				"> ConsoleResult.log"
			});
		}

		private void ProcessMessage(object message)
		{
			if (message is ClientReportMessage)
			{
				this.Report = (message as ClientReportMessage);
				this.LastReportTime = DateTime.Now;
				this.UpdateDBRow();
				this.UpdateListItem();
				this.MachineManager.RefreshMachineInfo();
				this.MachineManager.CheckRequestCompleted(this);
			}
		}

		public void UpdateListItem()
		{
			this.Core.Form.Invoke(new Action(delegate
			{
				base.Text = this.ToString();
				if (!this.IsConnected)
				{
					base.ForeColor = Color.Red;
					return;
				}
				if (this.ProcessCount == this.Operations.Count || this.ProcessCount == 0)
				{
					base.ForeColor = Color.Black;
					return;
				}
				base.ForeColor = Color.Red;
			}));
		}

		public void UpdateDBRow()
		{
			this.MachineInfo.Info = this.Info;
			this.MachineInfo.Status = this.NetworkStatus.ToString();
			this.MachineManager.SaveMachineStatus();
		}

		internal string Info
		{
			get
			{
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.AppendFormat("{0}({1})\r\n", this.HostName, this.Address);
				stringBuilder.AppendFormat("{0} ({1} / {2})\r\n", this.NetworkStatus, this.ProcessCount, this.Operations.Count);
				stringBuilder.AppendLine();
				if (this.Report == null)
				{
					stringBuilder.AppendFormat("[No Info]", new object[0]);
				}
				else
				{
					stringBuilder.Append("-- System Information\r\n");
					stringBuilder.AppendFormat("CPU : {0} Cores {1:0.0}%\r\n", this.Report.CoreCount, this.Report.cpuUsage);
					stringBuilder.AppendFormat("Memory : {0:0.0}MB / {1:0.0}MB\r\n", this.Report.memUsage / 1048576f, this.Report.memLimit / 1048576f);
					stringBuilder.AppendLine();
					foreach (ProcessReport processReport in this.Report.ProcessList)
					{
						stringBuilder.AppendFormat("[{0}]\t{1:00.0}% {2:00.0}MB\r\n", processReport.ProcessName, processReport.CPU, processReport.Memory);
					}
					stringBuilder.AppendLine();
					if (this.Report.LatestServerFileTime != this.MachineManager.LatestServerFileTime)
					{
						stringBuilder.AppendLine();
						stringBuilder.Append("-- Warning : server binary doesn't match").AppendLine();
						stringBuilder.AppendFormat("Source : {0}  ({1})", this.MachineManager.LatestServerFileTime, this.MachineManager.LatestServerFile).AppendLine();
						stringBuilder.AppendFormat("Machine : {0}  ({1})", this.Report.LatestServerFileTime, this.Report.LatestServerFile).AppendLine();
					}
					if (this.IsDSMachine && this.Report.LatestClientFileTime != this.MachineManager.LatestClientFileTime)
					{
						stringBuilder.AppendLine();
						stringBuilder.Append("-- Warning : ds binary doesn't match").AppendLine();
						stringBuilder.AppendFormat("Source : {0}\r\n - {1}", this.MachineManager.LatestClientFileTime, this.MachineManager.LatestClientFile).AppendLine();
						stringBuilder.AppendFormat("Machine : {0}\r\n - {1}", this.Report.LatestClientFileTime, this.Report.LatestClientFile).AppendLine();
					}
				}
				return stringBuilder.ToString();
			}
		}

		public override string ToString()
		{
			if (this.NetworkStatus != NetworkStatus.Connected)
			{
				return string.Format("{0} (X)", this.HostName);
			}
			return string.Format("{0} ({1}/{2})", this.HostName, this.ProcessCount, this.Operations.Count);
		}

		public int CompareTo(object obj)
		{
			Machine machine = obj as Machine;
			if (machine == null)
			{
				return -1;
			}
			if (this.IsConnected && !machine.IsConnected)
			{
				return -1;
			}
			if (!this.IsConnected && machine.IsConnected)
			{
				return 1;
			}
			return this.HostName.CompareTo(machine.HostName);
		}
	}
}
