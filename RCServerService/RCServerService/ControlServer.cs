using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Runtime.Serialization;
using System.Threading;
using Devcat.Core;
using Devcat.Core.Net;
using Devcat.Core.Net.Message;
using Devcat.Core.Threading;
using RemoteControlSystem.ClientMessage;
using RemoteControlSystem.ControlMessage;
using RemoteControlSystem.ServerMessage;

namespace RemoteControlSystem.Server
{
	internal class ControlServer
	{
		public ControlServer()
		{
			this._userList = new UserCollection();
			this.log = new List<NotifyMessage>(100);
			this.tcpServer = new TcpServer2();
			this.jobProcessor = new JobProcessor();
			this.tcpServer.ClientAccept += this.ClientAccepted;
			this.MF = new MessageHandlerFactory();
			this.MF.Register<ControlServer>(ControlMessages.TypeConverters, "ProcessMessage");
			this.MF.Register<ControlServer>(OpToolMessages.TypeConverters, "ProcessMessage");
			this.logManager = new LogManager();
		}

		public void Start(int port)
		{
			Base.ClientServer.OnChildProcessLogAdded += this.logManager.ChildProcessLogAdded;
			this.logManager.OnAddLIstenGroup += delegate(object s, ListenerGroupEventArgs e)
			{
				Base.ClientServer.SendMessage<ChildProcessLogConnectMessage>(e.ClientID, new ChildProcessLogConnectMessage(e.ClientID, e.ProcessName, e.PID));
			};
			this.logManager.OnDeleteLIstenGroup += delegate(object s, ListenerGroupEventArgs e)
			{
				Base.ClientServer.SendMessage<ChildProcessLogDisconnectMessage>(e.ClientID, new ChildProcessLogDisconnectMessage(e.ClientID, e.ProcessName, e.PID));
			};
			this.logManager.OnChildProcessLogAdded += delegate(object s, ChildProcessLogEventArgs e)
			{
				RCClient rcclient = s as RCClient;
				e.Message.ClientID = rcclient.ID;
				foreach (int userID in e.Clients)
				{
					Base.ControlServer.SendToUser<ChildProcessLogMessage>(userID, e.Message);
				}
			};
			this.jobProcessor.Start();
			this.tcpServer.Start(this.jobProcessor, port);
			FileWatcher.Start();
			FileDistributor.Start();
		}

		public void Stop()
		{
			this.tcpServer.Stop();
			this.jobProcessor.Stop();
			FileDistributor.Exit();
			FileWatcher.Exit();
			if (Base.ClientServer != null)
			{
				Base.ClientServer.OnChildProcessLogAdded -= this.logManager.ChildProcessLogAdded;
			}
		}

		private int NewUserID()
		{
			return Interlocked.Increment(ref this.userIDProvider);
		}

		private void ClientAccepted(object sender, AcceptEventArgs e)
		{
			User user = new User(this.NewUserID(), e.Client);
			user.Disconnected += this.ClientDisconnected;
			user.PacketReceived += this.PacketReceived;
			this._userList.Add(user);
			e.PacketAnalyzer = new MessageAnalyzer();
		}

		private void ClientDisconnected(object sender, EventArgs arg)
		{
			User user = sender as User;
			if (user != null)
			{
				this.UnregisterUser(user);
				this.logManager.RemoveListener(user);
				Log<RCServerService>.Logger.DebugFormat("User {0} disconnected", user.IsValid ? user.AccountId : user.Connection.RemoteEndPoint.Address.ToString());
			}
		}

		private void PacketReceived(object sender, EventArgs<ArraySegment<byte>> e)
		{
			try
			{
				User user = sender as User;
				if (user != null)
				{
					Packet packet = new Packet(e.Value);
					try
					{
						this.MF.Handle(packet, user);
					}
					catch (Exception ex)
					{
						Log<RCServerService>.Logger.DebugFormat("Packet error - {0}\n{1}", ex.Message, ex.StackTrace);
						Base.ControlServer.NotifyMessage(MessageType.Message, "[{0}/{1}] - {3}", new object[]
						{
							user.ClientId,
							user.AccountId,
							ex.ToString()
						});
						user.Connection.Disconnect();
					}
				}
			}
			catch (Exception ex2)
			{
				Log<RCServerService>.Logger.Error(ex2);
			}
		}

		private static void ProcessMessage(object rawMessage, object tag)
		{
			Base.ControlServer._ProcessMessage(rawMessage, tag);
		}

		private void _ProcessMessage(object rawMessage, object tag)
		{
			User user = tag as User;
			if (user == null)
			{
				return;
			}
			try
			{
				if (!(rawMessage is RemoteControlSystem.ClientMessage.PingMessage))
				{
					if (rawMessage is LoginMessage)
					{
						LoginMessage loginMessage = rawMessage as LoginMessage;
						if (user.IsValid)
						{
							throw new ArgumentException("Cannot login twice - " + loginMessage.Account);
						}
						this.Login(user, loginMessage.Account, loginMessage.Password);
					}
					else if (rawMessage is NotifyMessage)
					{
						this.CheckAuthority(user, Authority.Supervisor);
						this.NotifyMessage(user, rawMessage as NotifyMessage);
					}
					else if (rawMessage is ChangeMyPasswordMessage)
					{
						this.CheckAuthority(user, Authority.UserMonitor);
						ChangeMyPasswordMessage changeMyPasswordMessage = rawMessage as ChangeMyPasswordMessage;
						this.ChangeMyPassword(user, changeMyPasswordMessage.OldPassword, changeMyPasswordMessage.NewPassword);
					}
					else if (rawMessage is GetUserListMessage)
					{
						this.CheckAuthority(user, Authority.Supervisor);
						this.GetUserList(user);
					}
					else if (rawMessage is AddUserMessage)
					{
						this.CheckAuthority(user, Authority.Supervisor);
						AddUserMessage addUserMessage = rawMessage as AddUserMessage;
						this.AddUser(user, addUserMessage.Account, addUserMessage.Password, addUserMessage.Authority);
					}
					else if (rawMessage is RemoveUserMessage)
					{
						this.CheckAuthority(user, Authority.Supervisor);
						RemoveUserMessage removeUserMessage = rawMessage as RemoveUserMessage;
						this.RemoveUser(user, removeUserMessage.Account);
					}
					else if (rawMessage is ChangePasswordMessage)
					{
						this.CheckAuthority(user, Authority.Supervisor);
						ChangePasswordMessage changePasswordMessage = rawMessage as ChangePasswordMessage;
						this.ChangePassword(user, changePasswordMessage.Account, changePasswordMessage.NewPassword);
					}
					else if (rawMessage is ChangeAuthorityMessage)
					{
						this.CheckAuthority(user, Authority.Supervisor);
						ChangeAuthorityMessage changeAuthorityMessage = rawMessage as ChangeAuthorityMessage;
						this.ChangeAuthority(user, changeAuthorityMessage.Account, changeAuthorityMessage.Authority);
					}
					else if (rawMessage is GetUserAuthMesssage)
					{
						this.CheckAuthority(user, Authority.UserWatcher);
						this.GetUserAuthority(user, rawMessage as GetUserAuthMesssage);
					}
					else if (rawMessage is ControlEnterMessage)
					{
						this.CheckAuthority(user, Authority.ChiefGM);
						this.ControlEnter(user);
					}
					else if (rawMessage is ControlFinishMessage)
					{
						this.CheckMutex(user);
						this.ControlFinish(user);
					}
					else if (rawMessage is ControlRequestMessage)
					{
						this.CheckAuthority(user, Authority.UserKicker);
						ControlRequestMessage controlRequestMessage = rawMessage as ControlRequestMessage;
						this.SendControlProtocol(user, controlRequestMessage.Packet, controlRequestMessage.IDs);
					}
					else if (rawMessage is WorkGroupChangeMessage)
					{
						this.CheckMutex(user);
						WorkGroupChangeMessage workGroupChangeMessage = rawMessage as WorkGroupChangeMessage;
						this.WorkGroupChange(user, workGroupChangeMessage.WorkGroup);
					}
					else if (rawMessage is ServerGroupChangeMessage)
					{
						this.CheckMutex(user);
						ServerGroupChangeMessage serverGroupChangeMessage = rawMessage as ServerGroupChangeMessage;
						this.ServerGroupChange(user, serverGroupChangeMessage.ServerGroup);
					}
					else if (rawMessage is TemplateChangeMessage)
					{
						this.CheckMutex(user);
						TemplateChangeMessage templateChangeMessage = rawMessage as TemplateChangeMessage;
						this.TemplateChange(user, templateChangeMessage.Template);
					}
					else if (rawMessage is ChildProcessLogRequestMessage)
					{
						ChildProcessLogRequestMessage childProcessLogRequestMessage = rawMessage as ChildProcessLogRequestMessage;
						this.SendFunctionProtocol(user, SerializeWriter.ToBinary<ChildProcessLogRequestMessage>(childProcessLogRequestMessage).Bytes, childProcessLogRequestMessage.ClientID);
					}
					else if (rawMessage is ChildProcessLogListRequestMessage)
					{
						ChildProcessLogListRequestMessage childProcessLogListRequestMessage = rawMessage as ChildProcessLogListRequestMessage;
						this.SendFunctionProtocol(user, SerializeWriter.ToBinary<ChildProcessLogListRequestMessage>(childProcessLogListRequestMessage).Bytes, childProcessLogListRequestMessage.ClientID);
					}
					else if (rawMessage is ChildProcessLogConnectMessage)
					{
						ChildProcessLogConnectMessage childProcessLogConnectMessage = rawMessage as ChildProcessLogConnectMessage;
						this.logManager.AddListener(user, childProcessLogConnectMessage.ClientID, childProcessLogConnectMessage.ProcessName, childProcessLogConnectMessage.ProcessID);
					}
					else if (rawMessage is ChildProcessLogDisconnectMessage)
					{
						ChildProcessLogDisconnectMessage childProcessLogDisconnectMessage = rawMessage as ChildProcessLogDisconnectMessage;
						this.logManager.RemoveListener(user, childProcessLogDisconnectMessage.ClientID, childProcessLogDisconnectMessage.ProcessName, childProcessLogDisconnectMessage.ProcessID);
					}
					else
					{
						if (!(rawMessage is ExeInfoRequestMessage))
						{
							throw new RCServerException("Invalid packet! try to update latest version");
						}
						ExeInfoRequestMessage exeInfoRequestMessage = rawMessage as ExeInfoRequestMessage;
						this.SendFunctionProtocol(user, SerializeWriter.ToBinary<ExeInfoRequestMessage>(exeInfoRequestMessage).Bytes, exeInfoRequestMessage.ClientID);
					}
				}
			}
			catch (RCServerException ex)
			{
				this.NotifyMessage(user, MessageType.Error, ex.Message, new object[0]);
			}
			catch (Exception ex2)
			{
				if (rawMessage == null)
				{
					this.NotifyMessage(MessageType.Message, ex2.ToString(), new object[0]);
				}
				else
				{
					IPAddress address = user.Connection.RemoteEndPoint.Address;
					this.NotifyMessage(MessageType.Message, "[{0}/{1}] - {2}", new object[]
					{
						user.ClientId,
						address.ToString(),
						ex2.ToString()
					});
					user.Connection.Disconnect();
				}
			}
		}

		public void SendToUser<T>(T message)
		{
			foreach (User user in this._userList)
			{
				user.Send<T>(message);
			}
		}

		public void SendToUser<T>(int userID, T message)
		{
			if (this._userList.Contains(userID))
			{
				this._userList[userID].Send<T>(message);
			}
		}

		public void SendToUser(int userID, Packet packet)
		{
			if (this._userList.Contains(userID))
			{
				this._userList[userID].Send(packet);
			}
		}

		public void SendAuthorityUser<T>(Authority authority, T message, int exceptUserID)
		{
			foreach (User user in this._userList)
			{
				if (user.Authority >= authority && user.ClientId != exceptUserID)
				{
					user.Send<T>(message);
				}
			}
		}

		private void CheckAuthority(User user, Authority authority)
		{
			if (user.Authority < authority)
			{
				throw new RCServerException("No permission!");
			}
		}

		private void CheckMutex(User user)
		{
			if (user != User.MutexOwner)
			{
				throw new RCServerException("You are not in control mode!");
			}
		}

        private void Login(User user, string Id, byte[] Password)
        {
            Authority userAuthority = Base.Security.GetUserAuthority(Id, Password);
            LoginReply reply = new LoginReply(user.ClientId, userAuthority, BaseConfiguration.ServerVersion);
            if (userAuthority > Authority.None)
            {
                user.Login(Id);
                user.Send<LoginReply>(reply);
                ClientInfoMessage clientInfo = Base.ClientServer.GetClientInfo();
                lock (this.log)
                {
                    clientInfo.AddLog(this.log);
                }
                user.Send<ClientInfoMessage>(clientInfo);
                foreach (Emergency.EmergencyInformation information in Base.Emergency.Emergencies)
                {
                    EmergencyCallMessage message = new EmergencyCallMessage();
                    message.AddEmergencyCallInfo(information.Department, information.ID, information.Name, information.PhoneNumber, information.Mail, information.Rank);
                    user.Send<EmergencyCallMessage>(message);
                }
            }
            else
            {
                user.Send<LoginReply>(reply);
            }
        }

        private void UnregisterUser(User user)
		{
			if (user != null)
			{
				this._userList.Remove(user.ClientId);
				User.ReleaseMutexAll(user);
			}
		}

		private void ChangeMyPassword(User user, byte[] oldPassword, byte[] newPassword)
		{
			if (Base.Security.IsPassword(user.AccountId, oldPassword))
			{
				Base.Security.ChangePassword(user.AccountId, newPassword);
				Base.SaveConfig();
				this.NotifyMessage(user, MessageType.Information, "Password changed successfully!", new object[0]);
				return;
			}
			throw new RCServerException("Wrong current password!");
		}

		private void GetUserAuthority(User user, GetUserAuthMesssage message)
		{
			GetUserAuthReply message2 = new GetUserAuthReply(message.SessionKey, Base.Security.GetUserAuthority(message.Account, message.Password));
			user.Send<GetUserAuthReply>(message2);
		}

		private void GetUserList(User user)
		{
			GetUserListReply getUserListReply = new GetUserListReply();
			foreach (object obj in Base.Security.Users)
			{
				Security.UserInformation userInformation = (Security.UserInformation)obj;
				getUserListReply.AddUser(userInformation.ID, userInformation.Authority);
			}
			user.Send<GetUserListReply>(getUserListReply);
		}

		private void AddUser(User user, string id, byte[] password, Authority authority)
		{
			if (user.Authority < authority)
			{
				throw new RCServerException("Cannot create user who has more authority than you");
			}
			try
			{
				Base.Security.AddUser(id, password, authority);
				Base.SaveConfig();
			}
			catch (ArgumentException)
			{
				throw new RCServerException("Account already exist or Invalid account information");
			}
			Log<RCServerService>.Logger.InfoFormat("User {0} created account {1}", user.AccountId, id);
		}

		private void RemoveUser(User user, string id)
		{
			if (user.Authority > Base.Security.GetUserAuthority(id))
			{
				Base.Security.RemoveUser(id);
				Base.SaveConfig();
				Log<RCServerService>.Logger.InfoFormat("User {0} deleted account {1}", user.AccountId, id);
				return;
			}
			throw new RCServerException("Cannot remove user who has more authority than you");
		}

		private void ChangePassword(User user, string id, byte[] password)
		{
			if (user.Authority <= Base.Security.GetUserAuthority(id))
			{
				throw new RCServerException("Cannot change password of user who has more authority than you");
			}
			Base.Security.ChangePassword(id, password);
			Base.SaveConfig();
			Log<RCServerService>.Logger.InfoFormat("User {0} changed password of {1}", user.AccountId, id);
		}

		private void ChangeAuthority(User user, string id, Authority authority)
		{
			if (user.Authority <= Base.Security.GetUserAuthority(id))
			{
				throw new RCServerException("Cannot change authority of user who has higher or same than yours");
			}
			if (user.Authority < authority)
			{
				throw new RCServerException("Cannot change user's authority to which is higher than yours");
			}
			Base.Security.ChangeAuthority(id, authority);
			Base.SaveConfig();
			Log<RCServerService>.Logger.InfoFormat("User {0} changed authority of {1}", user.AccountId, id);
		}

		public void NotifyMessage(MessageType messageType, string message, params object[] args)
		{
			if (args.Length > 0)
			{
				message = string.Format(message, args);
			}
			lock (this.log)
			{
				while (this.log.Count >= 100)
				{
					this.log.RemoveAt(0);
				}
				this.log.Add(new NotifyMessage(messageType, message));
			}
			NotifyMessage message2 = new NotifyMessage(messageType, message);
			this.SendToUser<NotifyMessage>(message2);
		}

		private void NotifyMessage(User user, MessageType messageType, string message, params object[] args)
		{
			if (args.Length > 0)
			{
				message = string.Format(message, args);
			}
			NotifyMessage message2 = new NotifyMessage(messageType, message);
			user.Send<NotifyMessage>(message2);
		}

		private void NotifyMessage(User user, NotifyMessage message)
		{
			this.NotifyMessage(message.MessageType, "Message From {0} : \n->{1}", new object[]
			{
				user.AccountId,
				message.Message
			});
		}

		private void SendControlProtocol(User user, ArraySegment<byte> data, IEnumerable<int> clientIDs)
		{
			Packet packet = new Packet(data);
			if (packet.Bytes.Array == null)
			{
				throw new ArgumentNullException("data");
			}
			Type packetType = Base.ClientServer.GetPacketType(packet);
			this.AddCommandLogging(packet, user);
			object[] customAttributes = packetType.GetCustomAttributes(typeof(MutexCommandAttribute), true);
			if (customAttributes != null && customAttributes.Length > 0)
			{
				this.CheckMutex(user);
			}
			foreach (int num in clientIDs)
			{
				this.NotifyMessage(MessageType.Message, "user {0} using command to client [{1}]", new object[]
				{
					user.AccountId,
					num
				});
				Base.ClientServer.SendMessage(num, packet);
			}
			this.NotifyMessage(MessageType.Message, "user {0} using command [{1}]", new object[]
			{
				user.AccountId,
				packetType.Name
			});
		}

		private void AddCommandLogging(Packet packet, User user)
		{
			try
			{
				StandardInProcessMessage standardInProcessMessage = null;
				SerializeReader.FromBinary<StandardInProcessMessage>(packet, out standardInProcessMessage);
				if (standardInProcessMessage != null && standardInProcessMessage.Name.CompareTo("HeroesCommandBridge") == 0)
				{
					Log<ControlServer>.Logger.InfoFormat(standardInProcessMessage.Message + string.Format(" Written By [{0}].", user.AccountId), new object[0]);
				}
			}
			catch (SerializationException)
			{
			}
			catch (Exception ex)
			{
				Log<ControlServer>.Logger.Error(ex);
			}
		}

		private void SendFunctionProtocol(User user, ArraySegment<byte> data, int clientID)
		{
			FunctionRequestMessage value = new FunctionRequestMessage(data, user.ClientId);
			Base.ClientServer.SendMessage(clientID, SerializeWriter.ToBinary<FunctionRequestMessage>(value));
		}

		private void ControlEnter(User user)
		{
			User user2 = User.RequireMutex(user);
			ControlEnterReply controlEnterReply = new ControlEnterReply(user2.AccountId);
			if (user2 == user)
			{
				controlEnterReply.AddTemplate(Base.ProcessTemplate);
				foreach (object obj in Base.Security.Users)
				{
					Security.UserInformation userInformation = (Security.UserInformation)obj;
					controlEnterReply.AddUser(userInformation.ID, userInformation.Authority);
				}
			}
			user.Send<ControlEnterReply>(controlEnterReply);
			if (user2 == user)
			{
				this.NotifyMessage(MessageType.Message, "{0} entered control mode", new object[]
				{
					user.AccountId
				});
			}
		}

		private void ControlFinish(User user)
		{
			if (User.ReleaseMutex(user))
			{
				this.NotifyMessage(MessageType.Message, "{0} leaved control mode", new object[]
				{
					user.AccountId
				});
			}
		}

		private void WorkGroupChange(User user, string newWorkGroupString)
		{
			try
			{
				WorkGroupStructureNode.GetWorkGroup(newWorkGroupString);
				Base.WorkGroupString = newWorkGroupString;
				Base.SaveConfig();
				WorkGroupChangeMessage message = new WorkGroupChangeMessage(newWorkGroupString);
				this.SendToUser<WorkGroupChangeMessage>(message);
			}
			catch (Exception ex)
			{
				throw new RCServerException("Exception : Cannot parse workgroup string!\nSource Error Message : {0}", new object[]
				{
					ex.Message
				});
			}
		}

		private void ServerGroupChange(User user, string newServerGroupString)
		{
			try
			{
				ServerGroupStructureNode.GetServerGroup(newServerGroupString);
				Base.ServerGroupString = newServerGroupString;
				Base.SaveConfig();
				ServerGroupChangeMessage message = new ServerGroupChangeMessage(newServerGroupString);
				this.SendToUser<ServerGroupChangeMessage>(message);
			}
			catch (Exception ex)
			{
				throw new RCServerException("Exception : Cannot parse server group string!\nSource Error Message : {0}", new object[]
				{
					ex.Message
				});
			}
		}

		private void TemplateChange(User user, RCProcessCollection collection)
		{
			try
			{
				Base.ProcessTemplate = collection;
				Base.SaveConfig();
			}
			catch (Exception ex)
			{
				throw new RCServerException("Exception : Cannot parse process templates!\nSource Error Message : {0}", new object[]
				{
					ex.Message
				});
			}
		}

		public const int LogSize = 100;

		public static readonly Authority SCRIPT_MANAGER_AUTHORITY = Authority.Supervisor;

		private UserCollection _userList;

		private int userIDProvider;

		private List<NotifyMessage> log;

		private ITcpServer tcpServer;

		private JobProcessor jobProcessor;

		private MessageHandlerFactory MF;

		private LogManager logManager;
	}
}
