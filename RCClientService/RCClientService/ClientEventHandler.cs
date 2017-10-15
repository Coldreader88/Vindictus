using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using RemoteControlSystem.ServerMessage;

namespace RemoteControlSystem.Client
{
	public class ClientEventHandler
	{
		public ClientEventHandler(RCClient client)
		{
			client.ServerInfoChange += this.Client_ServerInfoChange;
			client.ProcessAdd += this.Client_ProcessAdd;
			client.ProcessModify += this.Client_ProcessModify;
			client.ProcessRemove += this.Client_ProcessRemove;
			client.ProcessStateChange += this.Client_ProcessStateChange;
			client.ProcessLog += this.Client_ProcessLog;
			client.FileDistributed += this.Client_FileDistributed;
		}

		private void Client_ServerInfoChange(RCClient sender, object args)
		{
			Base.SaveConfig();
			this.SendMessageToControlServer<ServerInfoMessage>(new ServerInfoMessage(sender.ServerIP, sender.ServerPort));
		}

		private void Client_ProcessAdd(RCClient sender, RCProcess process)
		{
			Base.SaveConfig();
			this.SendMessageToControlServer<AddProcessMessage>(new AddProcessMessage(process));
		}

		private void Client_ProcessModify(RCClient sender, RCProcess process)
		{
			Base.SaveConfig();
			process.RunScheduler();
			this.SendMessageToControlServer<ModifyProcessMessage>(new ModifyProcessMessage(process));
		}

		private void Client_ProcessRemove(RCClient sender, RCProcess process)
		{
			Base.SaveConfig();
			process.RunScheduler();
			this.SendMessageToControlServer<RemoveProcessMessage>(new RemoveProcessMessage(process.Name));
		}

		private void Client_ProcessStateChange(RCClient sender, RCClient.ProcessStateEventArgs args)
		{
			this.SendMessageToControlServer<StateChangeProcessMessage>(new StateChangeProcessMessage(args.TargetProcess.Name, args.TargetProcess.State, args.ChangedTime));
		}

		private void Client_ProcessLog(RCClient sender, RCClient.ProcessLogEventArgs args)
		{
			this.SendMessageToControlServer<LogProcessMessage>(new LogProcessMessage(args.Process.Name, args.Message));
			if (args.Process.PerformanceString.Length > 0 && RCProcess.IsStandardOutputLog(args.Message) && RCProcess.GetOriginalLog(args.Message).StartsWith(args.Process.PerformanceString))
			{
				string categoryName = "RCClient." + args.Process.Name;
				RCProcess.PerformanceDescriptionParser[] fromRawString = RCProcess.PerformanceDescriptionParser.GetFromRawString(args.Process.PerformanceDescription);
				string[] array = RCProcess.GetOriginalLog(args.Message).Substring(args.Process.PerformanceString.Length).Trim().Split(new char[]
				{
					' '
				});
				int num = Math.Min(array.Length, fromRawString.Length);
				long[] array2 = new long[num];
				for (int i = 0; i < num; i++)
				{
					try
					{
						array2[i] = long.Parse(array[i]);
					}
					catch (Exception)
					{
						array2[i] = 0L;
					}
				}
				bool flag = false;
				do
				{
					try
					{
						for (int j = 0; j < num; j++)
						{
							new PerformanceCounter(categoryName, fromRawString[j].Name, false).RawValue = array2[j];
						}
						flag = false;
					}
					catch (Exception ex)
					{
						Log<RCClientService>.Logger.ErrorFormat("ExceptionRcClient: {0}", ex.ToString());
						if (flag)
						{
							if (ex is Win32Exception)
							{
								Log<RCClientService>.Logger.ErrorFormat("Performance monitor unexcepted exception : {0}\n{1}", ((Win32Exception)ex).NativeErrorCode, ex.Message);
							}
							else
							{
								Log<RCClientService>.Logger.Error("Performance monitor unexcepted exception :", ex);
							}
							break;
						}
						flag = true;
						if (PerformanceCounterCategory.Exists(categoryName))
						{
							PerformanceCounterCategory.Delete(categoryName);
						}
						CounterCreationDataCollection counterCreationDataCollection = new CounterCreationDataCollection();
						foreach (RCProcess.PerformanceDescriptionParser performanceDescriptionParser in fromRawString)
						{
							counterCreationDataCollection.Add(new CounterCreationData(performanceDescriptionParser.Name, performanceDescriptionParser.HelpMessage, PerformanceCounterType.NumberOfItems64));
						}
						PerformanceCounterCategory.Create(categoryName, "", PerformanceCounterCategoryType.SingleInstance, counterCreationDataCollection);
					}
				}
				while (flag);
			}
		}

		internal void SendMessageToControlServer<T>(T message)
		{
			if (Base.ClientControlManager != null)
			{
				Base.ClientControlManager.Send<T>(message);
			}
		}

		private void Client_FileDistributed(RCClient sender, WatcherChangeTypes changeType, string pathName, string fileName, string oldFileName, bool isDirectory, byte[] fileData)
		{
			foreach (RCProcess rcprocess in sender.ProcessList)
			{
				if (rcprocess.WorkingDirectory.IndexOf(pathName) >= 0)
				{
					string text = null;
					string text2 = rcprocess.WorkingDirectory;
					if (fileName.Length > 0)
					{
						text2 = text2 + "\\" + fileName;
					}
					string text3 = text2;
					string text4 = text2 + ".tmp";
					if (changeType == WatcherChangeTypes.Renamed)
					{
						text3 = rcprocess.WorkingDirectory;
						if (oldFileName.Length > 0)
						{
							text3 = text3 + "\\" + oldFileName;
						}
					}
					try
					{
						if (changeType == WatcherChangeTypes.Deleted)
						{
							if (Directory.Exists(text2))
							{
								Directory.Delete(text2, true);
								Log<RCClientService>.Logger.InfoFormat("FileDistributor >> [{0}] Dir {1}.", changeType, text2);
							}
							else if (File.Exists(text2))
							{
								File.Delete(text2);
								Log<RCClientService>.Logger.InfoFormat("FileDistributor >> [{0}] File {1}.", changeType, text2);
							}
						}
						if (isDirectory)
						{
							if (changeType == WatcherChangeTypes.Renamed)
							{
								if (Directory.Exists(text3))
								{
									Directory.Move(text3, text2);
									Log<RCClientService>.Logger.InfoFormat("FileDistributor >> [{0}] Dir {1}.", changeType, text2);
								}
							}
							else if (changeType != WatcherChangeTypes.Deleted && !Directory.Exists(text2))
							{
								Directory.CreateDirectory(text2);
								Log<RCClientService>.Logger.InfoFormat("FileDistributor >> [{0}] Dir {1}.", changeType, text2);
							}
						}
						else
						{
							if (changeType == WatcherChangeTypes.Renamed)
							{
								if (File.Exists(text3))
								{
									File.Move(text3, text2);
									Log<RCClientService>.Logger.InfoFormat("FileDistributor >> [{0}] File {1}.", changeType, text2);
								}
								break;
							}
							if (File.Exists(text2))
							{
								File.Delete(text2);
							}
							if (changeType != WatcherChangeTypes.Deleted)
							{
								string path = text2.Substring(0, text2.LastIndexOf('\\'));
								if (!Directory.Exists(path))
								{
									Directory.CreateDirectory(path);
								}
								using (FileStream fileStream = File.Open(text4, FileMode.Create))
								{
									fileStream.Write(fileData, 0, fileData.Length);
									fileStream.Close();
									File.Move(text4, text2);
									Log<RCClientService>.Logger.InfoFormat("FileDistributor >> [{0}] File {1}.", changeType, text2);
								}
							}
						}
					}
					catch (ArgumentException ex)
					{
						text = ex.Message;
					}
					catch (FileNotFoundException ex2)
					{
						text = ex2.Message;
					}
					catch (DirectoryNotFoundException ex3)
					{
						text = ex3.Message;
					}
					catch (IOException ex4)
					{
						text = ex4.Message;
					}
					catch (NotSupportedException ex5)
					{
						text = ex5.Message;
					}
					catch (UnauthorizedAccessException ex6)
					{
						text = ex6.Message;
					}
					finally
					{
						if (File.Exists(text4))
						{
							try
							{
								File.Delete(text4);
							}
							catch
							{
							}
						}
						if (text != null)
						{
							Log<RCClientService>.Logger.ErrorFormat("FileDistributor >> {0} : {1}", text2, text);
						}
					}
				}
			}
		}
	}
}
