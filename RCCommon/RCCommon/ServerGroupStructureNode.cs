using System;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace RemoteControlSystem
{
	public class ServerGroupStructureNode : IWorkGroupStructureNode
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
				return this._servers;
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

		public ServerGroupStructureNode(string name, Authority authority, ServerGroupStructureNode[] childNodes, ServerGroupCondition[] servers)
		{
			if (name == null)
			{
				throw new ArgumentNullException("Name", "Name need not to be null!");
			}
			this.Name = name;
			this._childNodes = childNodes;
			this._servers = servers;
			this.Authority = authority;
		}

		public static ServerGroupStructureNode[] GetServerGroup(string xmlString)
		{
			XmlTextReader xmlTextReader = new XmlTextReader(xmlString, XmlNodeType.Element, new XmlParserContext(null, null, "", XmlSpace.Default, Encoding.Unicode));
			ServerGroupStructureNode[] result;
			try
			{
				XmlSerializer xmlSerializer = new XmlSerializer(typeof(XmlSerializationServerGroups));
				XmlSerializationServerGroups target = xmlSerializer.Deserialize(xmlTextReader) as XmlSerializationServerGroups;
				result = target;
			}
			finally
			{
				xmlTextReader.Close();
			}
			return result;
		}

		public ServerGroupStructureNode GetParent(IWorkGroupCondition condition)
		{
			ServerGroupStructureNode serverGroupStructureNode = null;
			if (this.Childs != null)
			{
				foreach (IWorkGroupCondition workGroupCondition in this.Childs)
				{
					if (workGroupCondition.CompareTo(condition) == 0)
					{
						return this;
					}
				}
			}
			if (this._childNodes != null)
			{
				foreach (ServerGroupStructureNode serverGroupStructureNode2 in this._childNodes)
				{
					serverGroupStructureNode = serverGroupStructureNode2.GetParent(condition);
					if (serverGroupStructureNode != null)
					{
						break;
					}
				}
			}
			return serverGroupStructureNode;
		}

		public const Authority DefaultAuthority = Authority.Supervisor;

		private ServerGroupStructureNode[] _childNodes;

		private ServerGroupCondition[] _servers;
	}
}
