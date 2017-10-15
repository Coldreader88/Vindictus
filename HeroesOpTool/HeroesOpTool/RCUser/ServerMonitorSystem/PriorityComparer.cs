using System;
using System.Collections;

namespace HeroesOpTool.RCUser.ServerMonitorSystem
{
	internal class PriorityComparer : IComparer
	{
		public PriorityComparer(WorkGroup _workGroup)
		{
			this.workGroup = _workGroup;
		}

		public int Compare(object x, object y)
		{
			ClientProcessItem clientProcessItem = x as ClientProcessItem;
			ClientProcessItem clientProcessItem2 = y as ClientProcessItem;
			if (clientProcessItem != null)
			{
				if (clientProcessItem2 == null)
				{
					return 1;
				}
				string conditionKey = this.workGroup.GetConditionKey(clientProcessItem.Client, clientProcessItem.Process);
				WorkGroupTreeNode workGroupTreeNode = this.workGroup[conditionKey];
				conditionKey = this.workGroup.GetConditionKey(clientProcessItem2.Client, clientProcessItem2.Process);
				WorkGroupTreeNode workGroupTreeNode2 = this.workGroup[conditionKey];
				if (workGroupTreeNode != null)
				{
					int num = workGroupTreeNode.CompareTo(workGroupTreeNode2);
					if (num != 0)
					{
						return num;
					}
					return clientProcessItem.CompareTo(clientProcessItem2);
				}
				else
				{
					if (workGroupTreeNode2 != null)
					{
						return -1;
					}
					return 0;
				}
			}
			else
			{
				if (clientProcessItem2 != null)
				{
					return -1;
				}
				return 0;
			}
		}

		private WorkGroup workGroup;
	}
}
