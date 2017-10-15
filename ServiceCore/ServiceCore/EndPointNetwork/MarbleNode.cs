using System;
using System.Collections.Generic;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class MarbleNode
	{
		public int NodeIndex { get; set; }

		public int NodeType { get; set; }

		public int NodeGrade { get; set; }

		public List<string> Arg { get; set; }

		public string Desc { get; set; }

		public MarbleNode(int nodeIndex, int nodeType, int nodeGrade, List<string> arg, string desc)
		{
			this.NodeIndex = nodeIndex;
			this.NodeType = nodeType;
			this.NodeGrade = nodeGrade;
			this.Arg = arg;
			this.Desc = desc;
		}
	}
}
