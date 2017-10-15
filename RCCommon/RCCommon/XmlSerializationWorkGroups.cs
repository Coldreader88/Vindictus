using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace RemoteControlSystem
{
	[XmlRoot("WorkGroups")]
	public class XmlSerializationWorkGroups
	{
		public string GetString()
		{
			string result;
			using (StringWriter stringWriter = new StringWriter())
			{
				new XmlSerializer(typeof(XmlSerializationWorkGroups)).Serialize(stringWriter, this, new XmlSerializerNamespaces(new XmlQualifiedName[]
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

		public static void UpdateAuthority(XmlSerializationWorkGroups.XmlSerializationWorkGroup parent)
		{
			if (parent != null && parent.WorkGroup != null)
			{
				foreach (XmlSerializationWorkGroups.XmlSerializationWorkGroup xmlSerializationWorkGroup in parent.WorkGroup)
				{
					if (xmlSerializationWorkGroup.WorkGroupAuthority > parent.WorkGroupAuthority)
					{
						xmlSerializationWorkGroup.WorkGroupAuthority = parent.WorkGroupAuthority;
					}
					if (xmlSerializationWorkGroup.WorkGroup != null)
					{
						XmlSerializationWorkGroups.UpdateAuthority(xmlSerializationWorkGroup);
					}
				}
			}
		}

		public static implicit operator WorkGroupStructureNode[](XmlSerializationWorkGroups target)
		{
			if (target.WorkGroup == null)
			{
				return null;
			}
			WorkGroupStructureNode[] array = new WorkGroupStructureNode[target.WorkGroup.Length];
			for (int i = 0; i < array.Length; i++)
			{
				XmlSerializationWorkGroups.UpdateAuthority(target.WorkGroup[i]);
				array[i] = target.WorkGroup[i];
			}
			return array;
		}

		public static implicit operator XmlSerializationWorkGroups(WorkGroupStructureNode[] target)
		{
			XmlSerializationWorkGroups xmlSerializationWorkGroups = new XmlSerializationWorkGroups();
			if (target == null)
			{
				return xmlSerializationWorkGroups;
			}
			xmlSerializationWorkGroups.WorkGroup = new XmlSerializationWorkGroups.XmlSerializationWorkGroup[target.Length];
			for (int i = 0; i < xmlSerializationWorkGroups.WorkGroup.Length; i++)
			{
				xmlSerializationWorkGroups.WorkGroup[i] = target[i];
			}
			return xmlSerializationWorkGroups;
		}

		[XmlElement("WorkGroup")]
		public XmlSerializationWorkGroups.XmlSerializationWorkGroup[] WorkGroup;

		public class XmlSerializationWorkGroup
		{
			public static implicit operator WorkGroupStructureNode(XmlSerializationWorkGroups.XmlSerializationWorkGroup target)
			{
				WorkGroupStructureNode[] array = null;
				if (target.WorkGroup != null)
				{
					array = new WorkGroupStructureNode[target.WorkGroup.Length];
					for (int i = 0; i < array.Length; i++)
					{
						array[i] = target.WorkGroup[i];
					}
				}
				WorkGroupCondition[] array2 = null;
				if (target.Process != null)
				{
					array2 = new WorkGroupCondition[target.Process.Length];
					for (int j = 0; j < array2.Length; j++)
					{
						array2[j] = target.Process[j];
					}
				}
				return new WorkGroupStructureNode(target.Name, target.WorkGroupAuthority, array, array2);
			}

			public static implicit operator XmlSerializationWorkGroups.XmlSerializationWorkGroup(WorkGroupStructureNode target)
			{
				XmlSerializationWorkGroups.XmlSerializationWorkGroup xmlSerializationWorkGroup = new XmlSerializationWorkGroups.XmlSerializationWorkGroup();
				xmlSerializationWorkGroup.Name = target.Name;
				if (target.ChildNodes != null)
				{
					xmlSerializationWorkGroup.WorkGroup = new XmlSerializationWorkGroups.XmlSerializationWorkGroup[target.ChildNodes.Length];
					for (int i = 0; i < xmlSerializationWorkGroup.WorkGroup.Length; i++)
					{
						xmlSerializationWorkGroup.WorkGroup[i] = (target.ChildNodes[i] as WorkGroupStructureNode);
					}
				}
				if (target.Childs != null)
				{
					xmlSerializationWorkGroup.Process = new XmlSerializationWorkGroups.XmlSerializationWorkGroup.XmlSerializationWorkGroupCondition[target.Childs.Length];
					for (int j = 0; j < xmlSerializationWorkGroup.Process.Length; j++)
					{
						xmlSerializationWorkGroup.Process[j] = (target.Childs[j] as WorkGroupCondition);
					}
				}
				xmlSerializationWorkGroup.WorkGroupAuthority = target.Authority;
				return xmlSerializationWorkGroup;
			}

			[XmlAttribute("Name")]
			public string Name;

			[XmlAttribute("Authority")]
			public Authority WorkGroupAuthority = Authority.Supervisor;

			[XmlElement("WorkGroup")]
			public XmlSerializationWorkGroups.XmlSerializationWorkGroup[] WorkGroup;

			[XmlElement("Process")]
			public XmlSerializationWorkGroups.XmlSerializationWorkGroup.XmlSerializationWorkGroupCondition[] Process;

			public class XmlSerializationWorkGroupCondition
			{
				public static implicit operator WorkGroupCondition(XmlSerializationWorkGroups.XmlSerializationWorkGroup.XmlSerializationWorkGroupCondition target)
				{
					return new WorkGroupCondition(target.ClientName, target.ProcessName);
				}

				public static implicit operator XmlSerializationWorkGroups.XmlSerializationWorkGroup.XmlSerializationWorkGroupCondition(WorkGroupCondition target)
				{
					return new XmlSerializationWorkGroups.XmlSerializationWorkGroup.XmlSerializationWorkGroupCondition
					{
						ClientName = target.ClientName,
						ProcessName = target.ProcessName
					};
				}

				[XmlAttribute("ClientName")]
				public string ClientName;

				[XmlAttribute("ProcessName")]
				public string ProcessName;
			}
		}
	}
}
