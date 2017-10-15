using System;
using System.Collections.Generic;
using System.Windows.Forms;
using RemoteControlSystem;

namespace HeroesOpTool.RCUser.ServerMonitorSystem
{
	internal abstract class WorkGroup
	{
		public static WorkGroupSort Sort { get; private set; } = new WorkGroupSort();

		public TreeView View { get; private set; }

		public PriorityComparer Comparer { get; private set; }

		public WorkGroupTreeNode this[string key]
		{
			get
			{
				if (this.conditionList.ContainsKey(key))
				{
					return this.conditionList[key];
				}
				return null;
			}
			set
			{
				if (this.conditionList.ContainsKey(key))
				{
					this.conditionList[key] = value;
					return;
				}
				this.conditionList.Add(key, value);
			}
		}

		public IWorkGroupStructureNode[] Node { get; set; }

		public WorkGroup(TreeView view)
		{
			this.View = view;
			this.Comparer = new PriorityComparer(this);
		}

		public int GetNextPriority()
		{
			return this.instanceCount++;
		}

		public void Clear()
		{
			this.instanceCount = 0;
		}

		public void SelectNodeFromListView()
		{
			this.View.BeginUpdate();
			HashSet<WorkGroupTreeNode> hashSet = new HashSet<WorkGroupTreeNode>();
			foreach (object obj in WorkGroup.Sort.ShowListView.Items)
			{
				ClientProcessItem clientProcessItem = (ClientProcessItem)obj;
				WorkGroupTreeNode workGroupTreeNode = this[this.GetConditionKey(clientProcessItem.Client, clientProcessItem.Process)];
				if (workGroupTreeNode != null && !hashSet.Contains(workGroupTreeNode))
				{
					hashSet.Add(workGroupTreeNode);
				}
			}
			foreach (object obj2 in this.View.Nodes)
			{
				WorkGroupTreeNode workGroupTreeNode2 = (WorkGroupTreeNode)obj2;
				workGroupTreeNode2.ChangeCheckState(CheckState.Unchecked, hashSet.Count == 0, false);
			}
			foreach (WorkGroupTreeNode workGroupTreeNode3 in hashSet)
			{
				workGroupTreeNode3.ChangeCheckState(CheckState.Checked, true, false);
			}
			this.View.EndUpdate();
		}

		public void AddToWorkGroup(RCClient client)
		{
			foreach (RCProcess process in client.ProcessList)
			{
				this.AddToWorkGroup(client, process);
			}
		}

		public void AddToWorkGroup(RCClient client, RCProcess process)
		{
			string conditionKey = this.GetConditionKey(client, process);
			WorkGroupTreeNode workGroupTreeNode = this[conditionKey];
			if (workGroupTreeNode != null)
			{
				ClientProcessItem key = new ClientProcessItem(client, process);
				ClientProcessItem.CreateListItem(client, process);
				ClientProcessItem clientProcessItem;
				if (!ClientProcessItem.ShowItem.ContainsKey(key))
				{
					clientProcessItem = ClientProcessItem.CreateListItem(client, process);
					ClientProcessItem.ShowItem[key] = clientProcessItem;
				}
				else
				{
					clientProcessItem = ClientProcessItem.ShowItem[key];
				}
				workGroupTreeNode.AddClientProcessItem(conditionKey, clientProcessItem);
			}
		}

		public void UpdateProcessState(RCClient client, RCProcess process)
		{
			string conditionKey = this.GetConditionKey(client, process);
			WorkGroupTreeNode workGroupTreeNode = this[conditionKey];
			if (workGroupTreeNode != null)
			{
				workGroupTreeNode.RecalculateTreeColor();
			}
		}

		public void RemoveFromWorkGroup(RCClient client)
		{
			foreach (RCProcess process in client.ProcessList)
			{
				this.RemoveFromWorkGroup(client, process);
			}
		}

		public void RemoveFromWorkGroup(RCClient client, RCProcess process)
		{
			string conditionKey = this.GetConditionKey(client, process);
			WorkGroupTreeNode workGroupTreeNode = this[conditionKey];
			if (workGroupTreeNode != null)
			{
				ClientProcessItem showItem = ClientProcessItem.GetShowItem(client, process);
				if (showItem != null)
				{
					ClientProcessItem.ShowItem.Remove(showItem);
					workGroupTreeNode.RemoveClientProcessItem(conditionKey, showItem);
				}
			}
		}

		public abstract string GetConditionKey(RCClient client, RCProcess process);

		private SortedList<string, WorkGroupTreeNode> conditionList = new SortedList<string, WorkGroupTreeNode>();

		private int instanceCount;
	}
}
