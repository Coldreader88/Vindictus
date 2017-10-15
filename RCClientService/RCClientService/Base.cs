using System;
using System.IO;
using System.Text;
using System.Threading;
using System.Xml;

namespace RemoteControlSystem.Client
{
	internal class Base
	{
		private Base()
		{
		}

		public static void StartUp()
		{
			Base.ClientDirectory = Path.GetDirectoryName(Environment.GetCommandLineArgs()[0]);
			Base.ClientControlManager = new ControlManager();
			Base.ClientControlClient = new ControlClient();
			Base.PingSender = new PingSender(1);
			string text = BaseConfiguration.ClientConfigFile;
			if (!Path.IsPathRooted(text))
			{
				text = BaseConfiguration.WorkingDirectory + "\\" + BaseConfiguration.ClientConfigFile;
			}
			Base.LoadConfig(text);
		}

		public static void CleanUp()
		{
			Base.ServerIPAddress = BaseConfiguration.ServerAddress;
			Base.ServerPort = BaseConfiguration.ServerClientPort;
			if (Base.Client != null)
			{
				Base.Client.KillAll();
			}
			Base.Client = null;
			if (Base.ClientControlClient != null)
			{
				Base.ClientControlClient.Stop();
			}
			Base.ClientControlClient = null;
			if (Base.ClientControlManager != null)
			{
				Base.ClientControlManager.Stop();
			}
			Base.ClientControlManager = null;
			if (Base.PingSender != null)
			{
				Base.PingSender.Stop();
			}
			Base.PingSender = null;
		}

		private static void LoadConfig(string fileName)
		{
			if (File.Exists(fileName))
			{
				XmlTextReader xmlTextReader = null;
				try
				{
					try
					{
						xmlTextReader = new XmlTextReader(fileName);
						xmlTextReader.WhitespaceHandling = WhitespaceHandling.None;
						xmlTextReader.MoveToContent();
						RCProcessCollection rcprocessCollection = new RCProcessCollection();
						while (xmlTextReader.Read())
						{
							string name;
							if (xmlTextReader.NodeType == XmlNodeType.Element && (name = xmlTextReader.Name) != null)
							{
								if (!(name == "Server"))
								{
									if (name == "Process")
									{
										rcprocessCollection.Add(new RCProcess(xmlTextReader));
									}
								}
								else
								{
									Base.ReadServerInfo(xmlTextReader);
								}
							}
						}
						Base.Client = new RCClient(Base.Version, Base.ServerIPAddress, Base.ServerPort, Base.ClientName, rcprocessCollection);
						foreach (RCProcess rcprocess in Base.Client.ProcessList)
						{
							rcprocess.RunScheduler();
						}
						new ClientEventHandler(Base.Client);
					}
					catch (Exception ex)
					{
						Log<RCClientService>.Logger.Error("Exception occured while loading configuration", ex);
						throw new ApplicationException("Cannot start service because configuration is invalid!");
					}
					return;
				}
				finally
				{
					if (xmlTextReader != null)
					{
						xmlTextReader.Close();
					}
				}
			}
			Log<RCClientService>.Logger.Warn("Cannot find configuration file!");
		}

		private static void ReadServerInfo(XmlTextReader reader)
		{
			string attribute = reader.GetAttribute("IP");
			if (attribute != null)
			{
				Base.ServerIPAddress = attribute;
			}
			string attribute2 = reader.GetAttribute("Port");
			if (attribute2 != null)
			{
				Base.ServerPort = int.Parse(attribute2);
			}
			string attribute3 = reader.GetAttribute("Name");
			if (attribute3 != null)
			{
				Base.ClientName = attribute3;
				return;
			}
			Base.ClientName = Environment.MachineName;
		}

		public static void SaveConfig()
		{
			Base.SaveConfig(BaseConfiguration.WorkingDirectory + "\\" + BaseConfiguration.ClientConfigFile);
		}

		public static void SaveConfig(string fileName)
		{
			XmlTextWriter xmlTextWriter = null;
			Base._saveMutex.WaitOne();
			try
			{
				xmlTextWriter = new XmlTextWriter(fileName, Encoding.Unicode);
				xmlTextWriter.Indentation = 1;
				xmlTextWriter.IndentChar = '\t';
				xmlTextWriter.Formatting = Formatting.Indented;
				xmlTextWriter.WriteStartDocument();
				xmlTextWriter.WriteComment(" Remote Control Client Service Configuration file ");
				xmlTextWriter.WriteWhitespace("\r\n");
				xmlTextWriter.WriteStartElement("RCCS_Config");
				xmlTextWriter.WriteComment("\r\n\tServer node must have IP and Port attributes that of Remote Control Server.\r\n\t");
				xmlTextWriter.WriteStartElement("Server");
				xmlTextWriter.WriteAttributeString("IP", Base.ServerIPAddress);
				xmlTextWriter.WriteAttributeString("Port", Base.ServerPort.ToString());
				if (Base.ClientName != Environment.MachineName)
				{
					xmlTextWriter.WriteAttributeString("Name", Base.ClientName);
				}
				xmlTextWriter.WriteEndElement();
				xmlTextWriter.WriteComment("\r\n\tProcess node must have Name, WorkingDirectory and ExecuteName which are not empty string.\r\n\t\r\n\tName\r\n\t\tIdentifier. Display name\r\n\tDescription\r\n\t\tProcess Description\r\n\t\r\n\tWorkingDirectory\r\n\t\tMain Process working directory\r\n\tExecuteName/Args\r\n\t\tExecute file name/arguments of main process (under WorkingDirectory)\r\n\t\r\n\tBootedString\r\n\t\tSpecial output string of main process that RCCS will realize process was booted completely\r\n\tShutdownString\r\n\t\tSpecial input string of main process which will stop process gracefully\r\n\tPerformanceString\r\n\t\tStarting string from Standard Output which contains process' private performance information\r\n\tPerformanceDescription\r\n\t\tEach meaning of Performance numbers\r\n\t\r\n\tStdOutLogLines (default : 100)\r\n\t\tRemember lines of Standard Output\r\n\tRunOnce (default : true)\r\n\t\tBoolean for run only once\r\n\t\r\n\tUpdateExecuteName/Args\r\n\t\tExecute file name/arguments of process which updates main process (under RCCS directory)\r\n\t\r\n\tDefaultSelect (default : true)\r\n\t\tCurrently usage - for u/i\r\n\tAutomaticStart (defalut : false)\r\n\t\tIndicates process starts when client service just launched\r\n\t");
				foreach (RCProcess rcprocess in Base.Client.ProcessList)
				{
					rcprocess.WriteXml(xmlTextWriter);
				}
				xmlTextWriter.WriteEndElement();
			}
			catch (Exception ex)
			{
				Log<RCClientService>.Logger.Error("Exception occured while saving configuration : ", ex);
			}
			finally
			{
				Base._saveMutex.ReleaseMutex();
				if (xmlTextWriter != null)
				{
					xmlTextWriter.Close();
				}
			}
		}

		public static int Version = 8;

		public static string ClientDirectory;

		public static string ServerIPAddress = BaseConfiguration.ServerAddress;

		public static int ServerPort = BaseConfiguration.ServerClientPort;

		public static string ClientName = "Unnamed";

		public static RCClient Client = null;

		public static ControlManager ClientControlManager = null;

		public static ControlClient ClientControlClient = null;

		public static PingSender PingSender = null;

		private static Mutex _saveMutex = new Mutex(false);
	}
}
