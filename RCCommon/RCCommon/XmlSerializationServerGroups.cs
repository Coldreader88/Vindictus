using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace RemoteControlSystem
{
	[XmlRoot("ServerGroups")]
	public class XmlSerializationServerGroups
	{
		public string GetString()
		{
			string result;
			using (StringWriter stringWriter = new StringWriter())
			{
				new XmlSerializer(typeof(XmlSerializationServerGroups)).Serialize(stringWriter, this, new XmlSerializerNamespaces(new XmlQualifiedName[]
				{
					XmlQualifiedName.Empty
				}));
				XmlTextReader xmlTextReader = new XmlTextReader(stringWriter.GetStringBuilder().ToString(), XmlNodeType.Document, new XmlParserContext(null, null, "", XmlSpace.Default, Encoding.Unicode));
				try
				{
					xmlTextReader.MoveToContent();
					result = xmlTextReader.ReadOuterXml();
				}
				finally
				{
					xmlTextReader.Close();
				}
			}
			return result;
		}

		public static void UpdateAuthority(XmlSerializationServerGroups.XmlSerializationServerGroup parent)
		{
			if (parent != null && parent.ServerGroup != null)
			{
				foreach (XmlSerializationServerGroups.XmlSerializationServerGroup xmlSerializationServerGroup in parent.ServerGroup)
				{
					if (xmlSerializationServerGroup.ServerGroupAuthority > parent.ServerGroupAuthority)
					{
						xmlSerializationServerGroup.ServerGroupAuthority = parent.ServerGroupAuthority;
					}
					if (xmlSerializationServerGroup.ServerGroup != null)
					{
						XmlSerializationServerGroups.UpdateAuthority(xmlSerializationServerGroup);
					}
				}
			}
		}

		public static implicit operator ServerGroupStructureNode[](XmlSerializationServerGroups target)
		{
			if (target.ServerGroup == null)
			{
				return null;
			}
			ServerGroupStructureNode[] array = new ServerGroupStructureNode[target.ServerGroup.Length];
			for (int i = 0; i < array.Length; i++)
			{
				XmlSerializationServerGroups.UpdateAuthority(target.ServerGroup[i]);
				array[i] = target.ServerGroup[i];
			}
			return array;
		}

		public static implicit operator XmlSerializationServerGroups(ServerGroupStructureNode[] target)
		{
			XmlSerializationServerGroups xmlSerializationServerGroups = new XmlSerializationServerGroups();
			if (target == null)
			{
				return xmlSerializationServerGroups;
			}
			xmlSerializationServerGroups.ServerGroup = new XmlSerializationServerGroups.XmlSerializationServerGroup[target.Length];
			for (int i = 0; i < xmlSerializationServerGroups.ServerGroup.Length; i++)
			{
				xmlSerializationServerGroups.ServerGroup[i] = target[i];
			}
			return xmlSerializationServerGroups;
		}

		[XmlElement("ServerGroup")]
		public XmlSerializationServerGroups.XmlSerializationServerGroup[] ServerGroup;

		public class XmlSerializationServerGroup
		{
			public static implicit operator ServerGroupStructureNode(XmlSerializationServerGroups.XmlSerializationServerGroup target)
			{
				ServerGroupStructureNode[] array = null;
				if (target.ServerGroup != null)
				{
					array = new ServerGroupStructureNode[target.ServerGroup.Length];
					for (int i = 0; i < array.Length; i++)
					{
						array[i] = target.ServerGroup[i];
					}
				}
				ServerGroupCondition[] array2 = null;
				if (target.Server != null)
				{
					array2 = new ServerGroupCondition[target.Server.Length];
					for (int j = 0; j < array2.Length; j++)
					{
						array2[j] = target.Server[j];
					}
				}
				return new ServerGroupStructureNode(target.Name, target.ServerGroupAuthority, array, array2);
			}

			public static implicit operator XmlSerializationServerGroups.XmlSerializationServerGroup(ServerGroupStructureNode target)
			{
				XmlSerializationServerGroups.XmlSerializationServerGroup xmlSerializationServerGroup = new XmlSerializationServerGroups.XmlSerializationServerGroup();
				xmlSerializationServerGroup.Name = target.Name;
				if (target.ChildNodes != null)
				{
					xmlSerializationServerGroup.ServerGroup = new XmlSerializationServerGroups.XmlSerializationServerGroup[target.ChildNodes.Length];
					for (int i = 0; i < xmlSerializationServerGroup.ServerGroup.Length; i++)
					{
						xmlSerializationServerGroup.ServerGroup[i] = (target.ChildNodes[i] as ServerGroupStructureNode);
					}
				}
				if (target.Childs != null)
				{
					xmlSerializationServerGroup.Server = new string[target.Childs.Length];
					for (int j = 0; j < xmlSerializationServerGroup.Server.Length; j++)
					{
						xmlSerializationServerGroup.Server[j] = (target.Childs[j] as ServerGroupCondition);
					}
				}
				xmlSerializationServerGroup.ServerGroupAuthority = target.Authority;
				return xmlSerializationServerGroup;
			}

			[XmlAttribute("Name")]
			public string Name;

			[XmlAttribute("Authority")]
			public Authority ServerGroupAuthority = Authority.Supervisor;

			[XmlElement("ServerGroup")]
			public XmlSerializationServerGroups.XmlSerializationServerGroup[] ServerGroup;

			[XmlElement("Server")]
			public string[] Server;
		}
	}
}
