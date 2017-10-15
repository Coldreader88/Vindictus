using System;
using System.Collections.Generic;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class MarbleInfoResultMessage : IMessage
	{
		public int MarbleID { get; set; }

		public List<MarbleNode> NodeList { get; set; }

		public int CurrentIndex { get; set; }

		public bool IsFirst { get; set; }

		public bool IsProcessed { get; set; }

		public MarbleInfoResultMessage(int marbleID, List<MarbleNode> nodeList, int currentIndex, bool isFirst, bool isProcessed)
		{
			this.MarbleID = marbleID;
			this.NodeList = nodeList;
			this.CurrentIndex = currentIndex;
			this.IsFirst = isFirst;
			this.IsProcessed = isProcessed;
		}
	}
}
