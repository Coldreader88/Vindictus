using System;
using System.IO;
using System.Text;
using System.Threading;
using System.Xml;

namespace RemoteControlSystem.Server
{
	internal class Base
	{
		private Base()
		{
		}

		public static void StartUp()
		{
			Base.ClientServer = new ClientServer();
			Base.ControlServer = new ControlServer();
			Base.ProcessTemplate = new RCProcessCollection();
			Base.GarbageCollector = new GarbageCollector(30);
			Base.PingSender = new PingSender(1);
			Base.Security = new Security();
			Base.Emergency = new Emergency();
			Base.LoadConfig(BaseConfiguration.WorkingDirectory + "\\" + BaseConfiguration.ServerConfigFile);
		}

		public static void CleanUp()
		{
			Base.ServerClientPort = BaseConfiguration.ServerClientPort;
			Base.ServerControlPort = BaseConfiguration.ServerControlPort;
			if (Base.ClientServer != null)
			{
				Base.ClientServer.Stop();
			}
			Base.ClientServer = null;
			if (Base.ControlServer != null)
			{
				Base.ControlServer.Stop();
			}
			Base.ControlServer = null;
			if (Base.GarbageCollector != null)
			{
				Base.GarbageCollector.Stop();
			}
			Base.GarbageCollector = null;
			if (Base.PingSender != null)
			{
				Base.PingSender.Stop();
			}
			Base.PingSender = null;
			Base.ProcessTemplate = null;
			Base.WorkGroupString = null;
			Base.Security = null;
		}

        private static void LoadConfig(string fileName)
        {
            if (!File.Exists(fileName))
            {
                Log<RCServerService>.Logger.Warn("Cannot find configuration file!");
            }
            else
            {
                XmlTextReader xmlTextReader = null;
                try
                {
                    try
                    {
                        xmlTextReader = new XmlTextReader(fileName)
                        {
                            WhitespaceHandling = WhitespaceHandling.None
                        };
                        xmlTextReader.MoveToContent();
                        bool flag = true;
                        while (true)
                        {
                            if (!flag)
                            {
                                flag = true;
                            }
                            else if (!xmlTextReader.Read())
                            {
                                if (Base.WorkGroupString == null)
                                {
                                    Base.WorkGroupString = "<WorkGroups/>";
                                }
                                if (Base.ServerGroupString == null)
                                {
                                    Base.ServerGroupString = "<ServerGroups/>";
                                }
                                return;
                            }
                            if (xmlTextReader.NodeType == XmlNodeType.Element)
                            {
                                string name = xmlTextReader.Name;
                                string str = name;
                                if (name != null)
                                {
                                    if (str == "Server")
                                    {
                                        Base.ReadServerInfo(xmlTextReader);
                                    }
                                    else if (str == "ProcessTemplates")
                                    {
                                        if (!xmlTextReader.IsEmptyElement)
                                        {
                                            Base.ReadProcessTemplateInfo(xmlTextReader);
                                        }
                                    }
                                    else if (str == "WorkGroups")
                                    {
                                        if (Base.WorkGroupString != null)
                                        {
                                            throw new FormatException("<WorkGroups> tag cannot be appeared before");
                                        }
                                        Base.WorkGroupString = xmlTextReader.ReadOuterXml();
                                        flag = false;
                                        WorkGroupStructureNode.GetWorkGroup(Base.WorkGroupString);
                                    }
                                    else if (str == "ServerGroups")
                                    {
                                        if (Base.ServerGroupString != null)
                                        {
                                            break;
                                        }
                                        Base.ServerGroupString = xmlTextReader.ReadOuterXml();
                                        flag = false;
                                        ServerGroupStructureNode.GetServerGroup(Base.ServerGroupString);
                                    }
                                    else if (str == "Authority")
                                    {
                                        Base.Security.LoadSecurity(xmlTextReader);
                                        flag = false;
                                    }
                                    else if (str == "EmergencyCall")
                                    {
                                        Base.Emergency.LoadEmergencyInfo(xmlTextReader);
                                        flag = false;
                                    }
                                }
                            }
                        }
                        throw new FormatException("<ServerGroups> tag cannot be appeared before");
                    }
                    catch (Exception exception)
                    {
                        Log<RCServerService>.Logger.Error("Exception occured while loading configuration", exception);
                        throw new ApplicationException("Cannot start service because configuration is invalid!");
                    }
                }
                finally
                {
                    if (xmlTextReader != null)
                    {
                        xmlTextReader.Close();
                    }
                }
            }
        }

        private static void ReadServerInfo(XmlTextReader reader)
		{
			string attribute = reader.GetAttribute("ClientPort");
			if (attribute != null)
			{
				Base.ServerClientPort = int.Parse(attribute);
			}
			string attribute2 = reader.GetAttribute("ControlPort");
			if (attribute2 != null)
			{
				Base.ServerControlPort = int.Parse(attribute2);
			}
		}

		private static void ReadProcessTemplateInfo(XmlTextReader reader)
		{
			while (reader.Read())
			{
				if (reader.NodeType == XmlNodeType.Element)
				{
					string name;
					if ((name = reader.Name) == null || !(name == "Process"))
					{
						throw new FormatException("<ProcessTemplates> has unknown child <" + reader.Name + ">.");
					}
					Base.ProcessTemplate.Add(new RCProcess(reader));
				}
				else if (reader.NodeType == XmlNodeType.EndElement && reader.Name == "ProcessTemplates")
				{
					return;
				}
			}
		}

		public static void SaveConfig()
		{
			Base.SaveConfig(BaseConfiguration.WorkingDirectory + "\\" + BaseConfiguration.ServerConfigFile);
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
				xmlTextWriter.WriteComment(" Remote Control Server Service Configuration file ");
				xmlTextWriter.WriteWhitespace("\r\n");
				xmlTextWriter.WriteStartElement("RCSS_Config");
				xmlTextWriter.WriteComment("\r\n\tServer node can have 'ClientPort', 'ControlPort' which default value is 10001, 10002\t");
				xmlTextWriter.WriteStartElement("Server");
				xmlTextWriter.WriteAttributeString("ClientPort", Base.ServerClientPort.ToString());
				xmlTextWriter.WriteAttributeString("ControlPort", Base.ServerControlPort.ToString());
				xmlTextWriter.WriteEndElement();
				xmlTextWriter.WriteComment("\r\n\tEmergency node can have Department, ID, Name, PhoneNumber, Mail, Rank\t");
				Base.Emergency.WriteEmergencyInfo(xmlTextWriter);
				xmlTextWriter.WriteComment("\r\n\tProcessTemplates node can have 'Process' and 'Environment' nodes.\r\n\t'Process' node can have same attributes which client's process has with environmental variables\r\n\twhich type is %variable_name% and will be replaced by user's input.\r\n\t'Environment' node can have attribute named 'Name' and 'Description'\r\n\t");
				xmlTextWriter.WriteStartElement("ProcessTemplates");
				foreach (RCProcess rcprocess in Base.ProcessTemplate)
				{
					rcprocess.WriteXml(xmlTextWriter);
				}
				xmlTextWriter.WriteEndElement();
				xmlTextWriter.WriteComment("\r\n\tWorkGroups node has structure for showing client&processes\r\n\tIt has childs named 'WorkGroup' and 'WorkGroup' can has childs named 'WorkGroup' or 'Process'\r\n\t'WorkGroup' node has attribute named 'Name' for screen name.\r\n\t'Process' node has attribute named 'ClientName' and 'ProcessName' which targetting client&process\r\n\t");
				XmlTextReader xmlTextReader = new XmlTextReader(Base.WorkGroupString, XmlNodeType.Element, new XmlParserContext(null, null, "", XmlSpace.Default, Encoding.Unicode));
				xmlTextWriter.WriteNode(xmlTextReader, false);
				xmlTextReader.Close();
				xmlTextWriter.WriteComment("\r\n\tServerGroups node has structure for showing client\r\n\tIt has childs named 'ServerGroup' and 'ServerGroup' can has childs named 'ServerGroup' or 'Client'\r\n\t'ServerGroup' node has attribute named 'Name' for screen name.\r\n\t'Process' node has attribute named 'ClientName' which targetting client\r\n\t");
				XmlTextReader xmlTextReader2 = new XmlTextReader(Base.ServerGroupString, XmlNodeType.Element, new XmlParserContext(null, null, "", XmlSpace.Default, Encoding.Unicode));
				xmlTextWriter.WriteNode(xmlTextReader2, false);
				xmlTextReader2.Close();
				xmlTextWriter.WriteComment("\r\n\tSecurity node has structure for user authority level\r\n\tIt has childs named 'UserInformation' and it can have attribute named 'ID', 'HashedPassword' and 'Authority'\r\n\t");
				Base.Security.WriteSecurity(xmlTextWriter);
			}
			catch (Exception ex)
			{
				Log<RCServerService>.Logger.Error("[Base] - Exception in SaveConfig()", ex);
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

		public static object SyncRoot = typeof(Base);

		public static int ServerClientPort = BaseConfiguration.ServerClientPort;

		public static int ServerControlPort = BaseConfiguration.ServerControlPort;

		public static ClientServer ClientServer = null;

		public static ControlServer ControlServer = null;

		public static RCProcessCollection ProcessTemplate = null;

		public static string WorkGroupString = null;

		public static string ServerGroupString = null;

		public static Emergency Emergency = null;

		public static Security Security = null;

		public static GarbageCollector GarbageCollector = null;

		public static PingSender PingSender = null;

		private static Mutex _saveMutex = new Mutex(false);
	}
}
