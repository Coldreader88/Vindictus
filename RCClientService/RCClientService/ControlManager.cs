using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using Devcat.Core;
using Devcat.Core.Net;
using Devcat.Core.Net.Message;
using Devcat.Core.Threading;
using RemoteControlSystem.ControlMessage;
using RemoteControlSystem.ServerMessage;

namespace RemoteControlSystem.Client
{
	internal class ControlManager
	{
		public JobProcessor Thread { get; private set; }

		public event EventHandler OnClientRegistered;

		public ControlManager()
		{
			this.Thread = new JobProcessor();
			this.clientNetwork = null;
			this.active = false;
			this.MF = new MessageHandlerFactory();
			this.MF.Register<ControlManager>(ControlMessages.TypeConverters, "ProcessMessage");
			this.MF.Register<RCClient>(RCClientMessages.TypeConverters, "ProcessMessage");
		}

		private void SetClient(TcpClient _clientNetwork)
		{
			lock (this)
			{
				if (this.clientNetwork != null)
				{
					throw new Exception("Client's already registered.");
				}
				this.clientNetwork = _clientNetwork;
				this.clientNetwork.PacketReceive += this.OnReceive;
			}
		}

		private void Close()
		{
			lock (this)
			{
				if (this.clientNetwork != null)
				{
					this.clientNetwork.Disconnect();
				}
			}
		}

		public void Start(RCClient client)
		{
			if (this.active)
			{
				return;
			}
			this.active = true;
			this.Thread.Start();
			client.ProcessCheckPatch = (RCClient.ProcessCheckPatchEventHandler)Delegate.Combine(client.ProcessCheckPatch, new RCClient.ProcessCheckPatchEventHandler(this.ClientCheckPatch));
			client.ProcessAdd += this.ClientProcessModified;
			client.ProcessRemove += this.ClientProcessModified;
			client.ProcessModify += this.ClientProcessModified;
			client.ProcessStateChange += this.ClientProcessStatusModified;
			this.ClientProcessModified(client);
		}

		public void Stop()
		{
			if (!this.active)
			{
				return;
			}
			this.active = false;
			this.Thread.Stop();
		}

        private void ClientCheckPatch(RCClient sender, List<RCProcess> processList)
        {
            Hashtable hashtable = new Hashtable();
            List<string> checkPatchLog = new List<string>();
            foreach (RCProcess process in processList)
            {
                if (!hashtable.ContainsKey(process.UpdateExecuteName + process.UpdateExecuteArgs))
                {
                    string key = process.UpdateExecuteName + process.UpdateExecuteArgs;
                    hashtable.Add(key, null);
                    string path = string.Format("{0}{1}", BaseConfiguration.WorkingDirectory, process.UpdateExecuteName + ".config");
                    if (File.Exists(path))
                    {
                        XmlTextReader reader = null;
                        try
                        {
                            try
                            {
                                reader = new XmlTextReader(path)
                                {
                                    WhitespaceHandling = WhitespaceHandling.None
                                };
                                string[] strArray = process.UpdateExecuteArgs.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                                string str3 = null;
                                string str4 = "PatchTime.config";
                                reader.ReadToFollowing("Address");
                                str3 = @"\\" + reader.ReadString() + @"\";
                                reader.ReadToFollowing("RootDirectory");
                                str3 = str3 + reader.ReadString() + @"\";
                                while (reader.ReadToFollowing("ServerGroup"))
                                {
                                    if (reader.GetAttribute("name") == strArray[1])
                                    {
                                        str3 = str3 + reader.GetAttribute("path") + @"\";
                                        break;
                                    }
                                }
                                while (reader.ReadToFollowing("ServiceType"))
                                {
                                    if (reader.GetAttribute("argument") == strArray[0])
                                    {
                                        str3 = str3 + reader.GetAttribute("path") + @"\";
                                        break;
                                    }
                                }
                                string str5 = str3 + str4;
                                string str6 = BaseConfiguration.WorkingDirectory + process.WorkingDirectory + @"\" + str4;
                                bool flag = true;
                                if (!File.Exists(str5))
                                {
                                    checkPatchLog.Add(string.Format("\"{0}\" is not exist. Please Packaging.", str5));
                                    flag = false;
                                }
                                if (!File.Exists(str6))
                                {
                                    checkPatchLog.Add(string.Format("\"{0}\" is not exist. Please Packaging.", str6));
                                    flag = false;
                                }
                                if (flag && !(File.GetLastWriteTime(str5) == File.GetLastWriteTime(str6)))
                                {
                                    checkPatchLog.Add(string.Format("\"{0}\" Process is not updated. Please Update.", process.Name));
                                }
                            }
                            catch (Exception exception)
                            {
                                checkPatchLog.Add(exception.ToString());
                            }
                            continue;
                        }
                        finally
                        {
                            if (reader != null)
                            {
                                reader.Close();
                            }
                        }
                    }
                    checkPatchLog.Add(string.Format("{0} file is not exist!", path));
                }
            }
            if (checkPatchLog.Count == 0)
            {
                checkPatchLog.Add("All Services are last version.");
            }
            CheckPatchProcessResultMessage message = new CheckPatchProcessResultMessage(checkPatchLog);
            this.Send<CheckPatchProcessResultMessage>(message);
        }

		private void ClientProcessStatusModified(RCClient sender, RCClient.ProcessStateEventArgs e)
		{
			this.ClientProcessModified(sender);
		}

		private void ClientProcessModified(RCClient sender, RCProcess process)
		{
			this.ClientProcessModified(sender);
		}

		private void ClientProcessModified(RCClient sender)
		{
			bool flag = false;
			foreach (RCProcess rcprocess in sender.ProcessList)
			{
				if (rcprocess.CanQueryPerformance())
				{
					flag = true;
					break;
				}
			}
			if (flag && !this.performanceScheduled)
			{
				Scheduler.Schedule(this.Thread, Job.Create<RCClient>(new Action<RCClient>(this.ReportPerformance), sender), 15000);
			}
			this.performanceScheduled = flag;
		}

		private void ClientProcessChildProcessLog(object sender, EventArgs<RCProcess.ChildProcessLog> log)
		{
			if (!this.Thread.IsInThread())
			{
				this.Thread.Enqueue(Job.Create<object, EventArgs<RCProcess.ChildProcessLog>>(new Action<object, EventArgs<RCProcess.ChildProcessLog>>(this.ClientProcessChildProcessLog), sender, log));
				return;
			}
			RCProcess.ChildProcessLogs childProcessLogs = sender as RCProcess.ChildProcessLogs;
			ChildProcessLogMessage message = new ChildProcessLogMessage(childProcessLogs.Parent, childProcessLogs.PID, log.Value);
			this.Send<ChildProcessLogMessage>(message);
		}

		private void ReportPerformance(RCClient client)
		{
			if (this.performanceScheduled)
			{
				PerformanceUpdateMessage performanceUpdateMessage = null;
				foreach (RCProcess rcprocess in client.ProcessList)
				{
					if (rcprocess.CanQueryPerformance() && rcprocess.QueryPerformance())
					{
						if (performanceUpdateMessage == null)
						{
							performanceUpdateMessage = new PerformanceUpdateMessage();
						}
						performanceUpdateMessage.Add(rcprocess.Name, rcprocess.Performance);
					}
				}
				if (performanceUpdateMessage != null)
				{
					this.Send<PerformanceUpdateMessage>(performanceUpdateMessage);
				}
				Scheduler.Schedule(this.Thread, Job.Create<RCClient>(new Action<RCClient>(this.ReportPerformance), client), 15000);
			}
		}

		public void Send<T>(T message)
		{
			lock (this)
			{
				if (this.clientNetwork != null)
				{
					Packet packet = SerializeWriter.ToBinary<T>(message);
					this.clientNetwork.Transmit(packet);
				}
			}
		}

		public void RegisterClient(TcpClient tcpClient)
		{
			lock (this)
			{
				this.SetClient(tcpClient);
				this.Send<ClientInitMessage>(new ClientInitMessage(Base.Client));
				if (this.OnClientRegistered != null)
				{
					this.OnClientRegistered(this, EventArgs.Empty);
				}
			}
		}

		public void UnregisterClient(TcpClient _tcpClient)
		{
			lock (this)
			{
				if (this.clientNetwork == _tcpClient)
				{
					this.clientNetwork.PacketReceive -= this.OnReceive;
					this.clientNetwork = null;
				}
			}
		}

		public void OnReceive(object sender, EventArgs<ArraySegment<byte>> args)
		{
			try
			{
				this.MF.Handle(new Packet(args.Value), Base.Client);
			}
			catch (Exception ex)
			{
				Log<RCClientService>.Logger.Error("Exception in OnReceive()", ex);
			}
		}

		private static void ProcessMessage(object rawMessage, object tag)
		{
			if (rawMessage is FunctionRequestMessage)
			{
				FunctionRequestMessage functionRequestMessage = rawMessage as FunctionRequestMessage;
				Base.ClientControlManager.MF.Handle(new Packet(functionRequestMessage.Packet), functionRequestMessage.ClientID);
				return;
			}
			int id = (int)tag;
			Packet packet = default(Packet);
			if (rawMessage is ChildProcessLogListRequestMessage)
			{
				ChildProcessLogListRequestMessage childProcessLogListRequestMessage = rawMessage as ChildProcessLogListRequestMessage;
				RCProcess rcprocess = Base.Client[childProcessLogListRequestMessage.ProcessName];
				ChildProcessLogListReplyMessage value = new ChildProcessLogListReplyMessage(childProcessLogListRequestMessage.ClientID, rcprocess.Name, rcprocess.ChildProcesses);
				packet = SerializeWriter.ToBinary<ChildProcessLogListReplyMessage>(value);
			}
			else if (rawMessage is ChildProcessLogRequestMessage)
			{
				ChildProcessLogRequestMessage childProcessLogRequestMessage = rawMessage as ChildProcessLogRequestMessage;
				RCProcess rcprocess2 = Base.Client[childProcessLogRequestMessage.ProcessName];
				List<RCProcess.ChildProcessLog> log = null;
				RCProcess.ChildProcessLogs childLog = rcprocess2.GetChildLog(childProcessLogRequestMessage.ProcessID);
				if (childLog != null)
				{
					log = childLog.LogLiness;
				}
				ChildProcessLogReplyMessage value2 = new ChildProcessLogReplyMessage(childProcessLogRequestMessage.ClientID, childProcessLogRequestMessage.ProcessName, childProcessLogRequestMessage.ProcessID, log);
				packet = SerializeWriter.ToBinary<ChildProcessLogReplyMessage>(value2);
			}
			else if (rawMessage is ChildProcessLogConnectMessage)
			{
				ChildProcessLogConnectMessage childProcessLogConnectMessage = rawMessage as ChildProcessLogConnectMessage;
				RCProcess rcprocess3 = Base.Client[childProcessLogConnectMessage.ProcessName];
				RCProcess.ChildProcessLogs childLog2 = rcprocess3.GetChildLog(childProcessLogConnectMessage.ProcessID);
				if (childLog2 != null && childLog2.CanConnect)
				{
					childLog2.OnLog += Base.ClientControlManager.ClientProcessChildProcessLog;
				}
			}
			else if (rawMessage is ChildProcessLogDisconnectMessage)
			{
				ChildProcessLogDisconnectMessage childProcessLogDisconnectMessage = rawMessage as ChildProcessLogDisconnectMessage;
				RCProcess rcprocess4 = Base.Client[childProcessLogDisconnectMessage.ProcessName];
				RCProcess.ChildProcessLogs childLog3 = rcprocess4.GetChildLog(childProcessLogDisconnectMessage.ProcessID);
				if (childLog3 != null)
				{
					childLog3.OnLog -= Base.ClientControlManager.ClientProcessChildProcessLog;
				}
			}
			else if (rawMessage is ExeInfoRequestMessage)
			{
				ExeInfoRequestMessage exeInfoRequestMessage = rawMessage as ExeInfoRequestMessage;
				RCProcess rcprocess5 = Base.Client[exeInfoRequestMessage.ProcessName];
				ExeInfoReplyMessage exeInfoReplyMessage = new ExeInfoReplyMessage(exeInfoRequestMessage.ClientID, rcprocess5.Name);
				try
				{
					DirectoryInfo directoryInfo = new DirectoryInfo(Path.Combine(BaseConfiguration.WorkingDirectory, rcprocess5.WorkingDirectory));
					List<FileInfo> list = new List<FileInfo>();
					list.AddRange(directoryInfo.GetFiles("*.dll", SearchOption.AllDirectories));
					list.AddRange(directoryInfo.GetFiles("*.exe", SearchOption.AllDirectories));
					foreach (FileInfo fileInfo in list)
					{
						exeInfoReplyMessage.AddFile(fileInfo.FullName.Substring(directoryInfo.FullName.Length + 1), fileInfo.LastWriteTime);
					}
				}
				finally
				{
					packet = SerializeWriter.ToBinary<ExeInfoReplyMessage>(exeInfoReplyMessage);
				}
			}
			if (packet.Bytes.Array != null)
			{
				FunctionReplyMessage message = new FunctionReplyMessage(packet.Bytes, id);
				Base.ClientControlManager.Send<FunctionReplyMessage>(message);
			}
		}

		private const int performanceUpdateTick = 15000;

		private TcpClient clientNetwork;

		private bool active;

		private MessageHandlerFactory MF;

		private bool performanceScheduled;
	}
}
