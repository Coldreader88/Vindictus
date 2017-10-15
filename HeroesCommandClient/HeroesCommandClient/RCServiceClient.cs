using System;
using System.Collections.Generic;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using Devcat.Core;
using Devcat.Core.Net;
using Devcat.Core.Net.Message;
using Devcat.Core.Threading;
using HeroesCommandClient.Properties;
using RemoteControlSystem;
using RemoteControlSystem.ClientMessage;
using RemoteControlSystem.ServerMessage;

namespace HeroesCommandClient
{
	internal class RCServiceClient
	{
		public event EventHandler Closed;

		public event EventHandler LoginSuccess;

		public event EventHandler<EventArgs<Exception>> LoginFail;

		public event EventHandler<EventArgs<string>> ServerGroupAdded;

		public event EventHandler<EventArgs<string>> ServerGroupActivated;

		public event EventHandler<EventArgs<string>> ServerGroupRemoved;

		public event EventHandler<EventArgs<string>> ProcessLogged;

		public RCServiceClient()
		{
			this.MF = new MessageHandlerFactory();
			this.MF.Register<RCServiceClient>(OpToolMessages.TypeConverters, "ProcessMessage");
			this.MF.Register<RCServiceClient>(RCClientMessages.TypeConverters, "ProcessMessage");
			this.tcpClient = new TcpClient2();
			this.tcpClient.ConnectionSucceed += this.OnConnect;
			this.tcpClient.ConnectionFail += delegate(object sender, EventArgs<Exception> e)
			{
				if (this.LoginFail != null)
				{
					this.LoginFail(this, e);
				}
			};
			this.tcpClient.PacketReceive += delegate(object sender, EventArgs<ArraySegment<byte>> e)
			{
				this.MF.Handle(new Packet(e.Value), this);
			};
			this.tcpClient.Disconnected += this.OnClose;
			this.clientList = new SortedList<int, RCClient>();
			this.clientNames = new Dictionary<string, RCClient>();
			this.serverGroups = new Dictionary<int, Dictionary<string, string>>();
			this.serverAddresses = new Dictionary<string, IPEndPoint>();
		}

		public void Start()
		{
			this.Connect();
			this.active = true;
		}

		private void Connect()
		{
			if (!this.tcpClient.Connected)
			{
				this.tcpClient.Connect(HeroesCommandBridge.Thread, Settings.Default.RCServerIP, Settings.Default.RCServerPort, new MessageAnalyzer());
			}
		}

		public void Reconnect()
		{
			Thread.Sleep(1000);
			this.Connect();
		}

		public void Stop()
		{
			if (this.active)
			{
				this.cleaning = true;
				this.tcpClient.Disconnect();
				this.active = false;
				return;
			}
			throw new InvalidOperationException("Already stopped");
		}

		private void OnConnect(object sender, EventArgs arg)
		{
			this.SendMessage<LoginMessage>(new LoginMessage(Settings.Default.ID, new MD5CryptoServiceProvider().ComputeHash(Encoding.Unicode.GetBytes(Settings.Default.Password))));
		}

		private void OnClose(object sender, EventArgs arg)
		{
			if (!this.cleaning)
			{
				if (this.Closed != null)
				{
					this.Closed(this, EventArgs.Empty);
				}
				HeroesCommandBridge.Thread.Enqueue(Job.Create(new Action(this.Reconnect)));
				return;
			}
			this.cleaning = false;
		}

		public void SendMessage<T>(T message)
		{
			this.tcpClient.Transmit(SerializeWriter.ToBinary<T>(message));
		}

		private void AddClient(RCClient client)
		{
			if (!this.clientList.ContainsKey(client.ID))
			{
				this.clientList.Add(client.ID, client);
			}
			if (!this.clientNames.ContainsKey(client.Name))
			{
				this.clientNames.Add(client.Name, client);
			}
		}

		private void RemoveClient(int clientid)
		{
			if (this.clientList.ContainsKey(clientid))
			{
				string name = this.clientList[clientid].Name;
				if (this.clientNames.ContainsKey(name))
				{
					this.clientNames.Remove(name);
				}
				this.clientList.Remove(clientid);
			}
		}

		private IWorkGroupStructureNode FindSubGroup(string groupName, IEnumerable<IWorkGroupStructureNode> groups, List<IWorkGroupStructureNode> subGroups)
		{
			subGroups.Clear();
			foreach (IWorkGroupStructureNode workGroupStructureNode in groups)
			{
				if (workGroupStructureNode.Name == groupName)
				{
					return workGroupStructureNode;
				}
				if (!workGroupStructureNode.IsLeafNode)
				{
					subGroups.AddRange(workGroupStructureNode.ChildNodes);
				}
			}
			return null;
		}

		private IEnumerable<KeyValuePair<int, string>> GetAllSubProcess(IWorkGroupStructureNode node)
		{
			if (node != null)
			{
				if (node.IsLeafNode)
				{
					foreach (IWorkGroupCondition i in node.Childs)
					{
						WorkGroupCondition c = i as WorkGroupCondition;
						if (c != null && this.clientNames.ContainsKey(c.ClientName))
						{
							yield return new KeyValuePair<int, string>(this.clientNames[c.ClientName].ID, c.ProcessName);
						}
					}
				}
				else
				{
					foreach (IWorkGroupStructureNode j in node.ChildNodes)
					{
						foreach (KeyValuePair<int, string> k in this.GetAllSubProcess(j))
						{
							yield return k;
						}
					}
				}
			}
			yield break;
		}

		public IEnumerable<KeyValuePair<int, string>> FindAllSubProcess(string groupName)
		{
			IWorkGroupStructureNode found = null;
			List<IWorkGroupStructureNode> subGroups = new List<IWorkGroupStructureNode>();
			found = this.FindSubGroup(groupName, this.workGroups, subGroups);
			if (found == null)
			{
				List<IWorkGroupStructureNode> list = new List<IWorkGroupStructureNode>();
				while (found == null && subGroups.Count > 0)
				{
					List<IWorkGroupStructureNode> list2 = list;
					list = subGroups;
					subGroups = list2;
					found = this.FindSubGroup(groupName, list, subGroups);
				}
			}
			if (found != null)
			{
				foreach (KeyValuePair<int, string> i in this.GetAllSubProcess(found))
				{
					yield return i;
				}
			}
			yield break;
		}

		public IPEndPoint GetIPFromServerGroup(string name)
		{
			if (this.serverAddresses.ContainsKey(name))
			{
				return this.serverAddresses[name];
			}
			return null;
		}

		private string GetProcessKey(int clientID, string processName)
		{
			return string.Format("{0}:{1}", clientID, processName);
		}

		private void CheckAndAddHeroesAdmin(int id, string clientIP, RCProcess process)
		{
			if (process.Properties.ContainsKey("HeroesAdmin"))
			{
				string[] array = process.Properties["HeroesAdmin"].Split(new char[]
				{
					':'
				}, StringSplitOptions.RemoveEmptyEntries);
				if (array.Length < 2)
				{
					return;
				}
				try
				{
					IPEndPoint ipendPoint = new IPEndPoint(IPAddress.Parse(clientIP), int.Parse(array[1]));
					string processKey = this.GetProcessKey(id, process.Name);
					Dictionary<string, string> dictionary;
					if (this.serverGroups.ContainsKey(id))
					{
						dictionary = this.serverGroups[id];
					}
					else
					{
						dictionary = new Dictionary<string, string>();
						this.serverGroups.Add(id, dictionary);
					}
					if (dictionary.ContainsKey(processKey))
					{
						if (dictionary[processKey] != array[0] || !this.serverAddresses[array[0]].Equals(ipendPoint))
						{
							this.serverAddresses.Remove(dictionary[processKey]);
							if (this.ServerGroupRemoved != null)
							{
								this.ServerGroupRemoved(this, new EventArgs<string>(dictionary[processKey]));
							}
							dictionary[processKey] = array[0];
							this.serverAddresses.Add(array[0], ipendPoint);
							if (this.ServerGroupAdded != null)
							{
								this.ServerGroupAdded(this, new EventArgs<string>(array[0]));
							}
							if (process.State == RCProcess.ProcessState.On && this.ServerGroupActivated != null)
							{
								this.ServerGroupActivated(this, new EventArgs<string>(array[0]));
							}
						}
					}
					else
					{
						dictionary.Add(processKey, array[0]);
						this.serverAddresses.Add(array[0], ipendPoint);
						if (this.ServerGroupAdded != null)
						{
							this.ServerGroupAdded(this, new EventArgs<string>(array[0]));
						}
						if (process.State == RCProcess.ProcessState.On && this.ServerGroupActivated != null)
						{
							this.ServerGroupActivated(this, new EventArgs<string>(array[0]));
						}
					}
					return;
				}
				catch
				{
					return;
				}
			}
			this.CheckAndRemoveHeroesAdmin(id, process.Name);
		}

		private void CheckAndAddHeroesAdmin(RCClient client)
		{
			foreach (RCProcess process in client.ProcessList)
			{
				this.CheckAndAddHeroesAdmin(client.ID, client.ClientIP, process);
			}
		}

		private void CheckAndRemoveHeroesAdmin(int id, string processName)
		{
			if (this.serverGroups.ContainsKey(id))
			{
				string processKey = this.GetProcessKey(id, processName);
				if (this.serverGroups[id].ContainsKey(processKey))
				{
					string text = this.serverGroups[id][processKey];
					this.serverAddresses.Remove(text);
					this.serverGroups[id].Remove(processKey);
					if (this.ServerGroupRemoved != null)
					{
						this.ServerGroupRemoved(this, new EventArgs<string>(text));
					}
				}
			}
		}

		private void CheckAndRemoveHeroesAdmin(int id)
		{
			if (this.serverGroups.ContainsKey(id))
			{
				foreach (string text in this.serverGroups[id].Values)
				{
					this.serverAddresses.Remove(text);
					if (this.ServerGroupRemoved != null)
					{
						this.ServerGroupRemoved(this, new EventArgs<string>(text));
					}
				}
				this.serverGroups.Remove(id);
			}
		}

		private void ProcessMessage(object rawMessage)
		{
			if (rawMessage is LoginReply)
			{
				LoginReply loginReply = rawMessage as LoginReply;
				if (loginReply.Authority == Authority.None)
				{
					if (this.LoginFail != null)
					{
						this.LoginFail(this, new EventArgs<Exception>(new Exception("Fail to login")));
					}
					this.Stop();
					return;
				}
				if (this.LoginSuccess != null)
				{
					this.LoginSuccess(this, EventArgs.Empty);
					return;
				}
			}
			else
			{
				if (rawMessage is ClientInfoMessage)
				{
					ClientInfoMessage clientInfoMessage = rawMessage as ClientInfoMessage;
					this.workGroups = WorkGroupStructureNode.GetWorkGroup(clientInfoMessage.WorkGroup);
					using (IEnumerator<KeyValuePair<int, RCClient>> enumerator = clientInfoMessage.Clients.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							KeyValuePair<int, RCClient> keyValuePair = enumerator.Current;
							RCClient rcclient = new RCClient(keyValuePair.Key, keyValuePair.Value.ClientIP);
							rcclient.AssignFrom(keyValuePair.Value);
							this.AddClient(rcclient);
							this.CheckAndAddHeroesAdmin(rcclient);
						}
						return;
					}
				}
				if (rawMessage is ClientAddedMessage)
				{
					ClientAddedMessage clientAddedMessage = rawMessage as ClientAddedMessage;
					RCClient rcclient2 = new RCClient(clientAddedMessage.ID, clientAddedMessage.Client.ClientIP);
					rcclient2.AssignFrom(clientAddedMessage.Client);
					this.AddClient(rcclient2);
					this.CheckAndAddHeroesAdmin(rcclient2);
					return;
				}
				if (rawMessage is ClientRemovedMessage)
				{
					ClientRemovedMessage clientRemovedMessage = rawMessage as ClientRemovedMessage;
					this.RemoveClient(clientRemovedMessage.ID);
					this.CheckAndRemoveHeroesAdmin(clientRemovedMessage.ID);
					return;
				}
				if (rawMessage is ControlReplyMessage)
				{
					ControlReplyMessage controlReplyMessage = rawMessage as ControlReplyMessage;
					if (this.clientList.ContainsKey(controlReplyMessage.ID))
					{
						this.ProcessClientMessage(new Packet(controlReplyMessage.Packet), this.clientList[controlReplyMessage.ID]);
					}
				}
			}
		}

		private void ProcessClientMessage(Packet packet, RCClient client)
		{
			Type type = this.MF.GetType(packet);
			if (type == typeof(AddProcessMessage))
			{
				AddProcessMessage addProcessMessage;
				SerializeReader.FromBinary<AddProcessMessage>(packet, out addProcessMessage);
				client.ProcessMessage(addProcessMessage);
				this.CheckAndAddHeroesAdmin(client.ID, client.ClientIP, addProcessMessage.Process);
				return;
			}
			if (type == typeof(ModifyProcessMessage))
			{
				ModifyProcessMessage modifyProcessMessage;
				SerializeReader.FromBinary<ModifyProcessMessage>(packet, out modifyProcessMessage);
				client.ProcessMessage(modifyProcessMessage);
				this.CheckAndAddHeroesAdmin(client.ID, client.ClientIP, modifyProcessMessage.Process);
				return;
			}
			if (type == typeof(RemoveProcessMessage))
			{
				RemoveProcessMessage removeProcessMessage;
				SerializeReader.FromBinary<RemoveProcessMessage>(packet, out removeProcessMessage);
				client.ProcessMessage(removeProcessMessage);
				this.CheckAndRemoveHeroesAdmin(client.ID, removeProcessMessage.Name);
				return;
			}
			if (type == typeof(StateChangeProcessMessage))
			{
				StateChangeProcessMessage stateChangeProcessMessage;
				SerializeReader.FromBinary<StateChangeProcessMessage>(packet, out stateChangeProcessMessage);
				if (this.serverGroups.ContainsKey(client.ID) && stateChangeProcessMessage.State == RCProcess.ProcessState.On)
				{
					string processKey = this.GetProcessKey(client.ID, stateChangeProcessMessage.Name);
					if (this.serverGroups[client.ID].ContainsKey(processKey))
					{
						string value = this.serverGroups[client.ID][processKey];
						if (this.ServerGroupActivated != null)
						{
							this.ServerGroupActivated(this, new EventArgs<string>(value));
							return;
						}
					}
				}
			}
			else if (type == typeof(LogProcessMessage))
			{
				LogProcessMessage logProcessMessage;
				SerializeReader.FromBinary<LogProcessMessage>(packet, out logProcessMessage);
				RCProcess rcprocess = client[logProcessMessage.Name];
				if ((rcprocess.PerformanceString.Length == 0 || !RCProcess.IsStandardOutputLog(logProcessMessage.Message) || !RCProcess.GetOriginalLog(logProcessMessage.Message).StartsWith(rcprocess.PerformanceString)) && this.ProcessLogged != null)
				{
					this.ProcessLogged(new KeyValuePair<RCClient, RCProcess>(client, rcprocess), new EventArgs<string>(RCProcess.GetOriginalLog(logProcessMessage.Message)));
				}
			}
		}

		private const string HeroesAdminProperty = "HeroesAdmin";

		private TcpClient tcpClient;

		private MessageHandlerFactory MF;

		private bool cleaning;

		private bool active;

		private Dictionary<int, Dictionary<string, string>> serverGroups;

		private Dictionary<string, IPEndPoint> serverAddresses;

		private SortedList<int, RCClient> clientList;

		private Dictionary<string, RCClient> clientNames;

		private WorkGroupStructureNode[] workGroups;
	}
}
