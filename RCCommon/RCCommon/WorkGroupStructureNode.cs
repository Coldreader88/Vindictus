using System;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace RemoteControlSystem
{
	public class WorkGroupStructureNode : IWorkGroupStructureNode
	{
		public string Name { get; private set; }

		public IWorkGroupStructureNode[] ChildNodes
		{
			get
			{
				return this._childNodes;
			}
		}

		public IWorkGroupCondition[] Childs
		{
			get
			{
				return this._childProcesses;
			}
		}

		public Authority Authority { get; private set; }

		public bool IsLeafNode
		{
			get
			{
				return this._childNodes == null || this._childNodes.Length == 0;
			}
		}

		public WorkGroupStructureNode(string name, Authority authority, WorkGroupStructureNode[] childNodes, WorkGroupCondition[] childProcesses)
		{
			if (name == null)
			{
				throw new ArgumentNullException("Name", "Name need not to be null!");
			}
			this.Name = name;
			this._childNodes = childNodes;
			this._childProcesses = childProcesses;
			this.Authority = authority;
		}

		public static WorkGroupStructureNode[] GetWorkGroup(string xmlString)
		{
			XmlTextReader xmlTextReader = new XmlTextReader(xmlString, XmlNodeType.Element, new XmlParserContext(null, null, "", XmlSpace.Default, Encoding.Unicode));
			WorkGroupStructureNode[] result;
			try
			{
				result = (XmlSerializationWorkGroups)new XmlSerializer(typeof(XmlSerializationWorkGroups)).Deserialize(xmlTextReader);
			}
			finally
			{
				xmlTextReader.Close();
			}
			return result;
		}

		public WorkGroupStructureNode GetParent(IWorkGroupCondition condition)
		{
			WorkGroupStructureNode workGroupStructureNode = null;
			if (this._childProcesses != null)
			{
				foreach (WorkGroupCondition workGroupCondition in this._childProcesses)
				{
					if (workGroupCondition.CompareTo(condition) == 0)
					{
						return this;
					}
				}
			}
			if (this._childNodes != null)
			{
				foreach (WorkGroupStructureNode workGroupStructureNode2 in this._childNodes)
				{
					workGroupStructureNode = workGroupStructureNode2.GetParent(condition);
					if (workGroupStructureNode != null)
					{
						break;
					}
				}
			}
			return workGroupStructureNode;
		}

		public const Authority DefaultAuthority = Authority.Supervisor;

		private WorkGroupStructureNode[] _childNodes;

		private WorkGroupCondition[] _childProcesses;
	}
}
