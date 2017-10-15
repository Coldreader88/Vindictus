using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using RemoteControlSystem;

namespace HeroesOpTool.RCUser.ServerMonitorSystem
{
    internal class WorkGroupTreeNode : TreeNode, IComparable
    {
        public string Title
        {
            get;
            private set;
        }

        public bool IsLeafNode
        {
            get
            {
                return base.Nodes.Count == 0;
            }
        }

        public CheckState CheckState
        {
            get
            {
                return this.checkState;
            }
            set
            {
                this.ChangeCheckState(value, true, true);
            }
        }

        public int Priority
        {
            get
            {
                return this.priority;
            }
            set
            {
                if (ClientProcessItem.ShowItem.Count > 0)
                {
                    throw new InvalidOperationException("Cannot set priority after any client-process had been bound with any workgroup node.");
                }
                this.priority = value;
            }
        }

        public int Connected
        {
            get
            {
                return this.items.Count;
            }
        }

        public int Total
        {
            get
            {
                return this.Connected + this.naConditions.Count;
            }
        }

        public WorkGroupTreeNode(IWorkGroupStructureNode node, WorkGroup _parent) : base(node.Name)
        {
            this.Title = node.Name;
            this.items = new List<ClientProcessItem>();
            this.parent = _parent;
            this.priority = this.parent.GetNextPriority();
            this.naConditions = new HashSet<string>();
            if (node.IsLeafNode)
            {
                if (node.Childs != null)
                {
                    IWorkGroupCondition[] childs = node.Childs;
                    for (int i = 0; i < childs.Length; i++)
                    {
                        IWorkGroupCondition condition = childs[i];
                        string key = condition.ToString();
                        this.parent[key] = this;
                        this.naConditions.Add(key);
                    }
                    return;
                }
            }
            else
            {
                IWorkGroupStructureNode[] childNodes = node.ChildNodes;
                for (int j = 0; j < childNodes.Length; j++)
                {
                    IWorkGroupStructureNode childNode = childNodes[j];
                    base.Nodes.Add(new WorkGroupTreeNode(childNode, this.parent));
                }
            }
        }

        public void AddClientProcessItem(string key, ClientProcessItem target)
        {
            Log<WorkGroupTreeNode>.Logger.DebugFormat("=================================================", new object[0]);
            Log<WorkGroupTreeNode>.Logger.DebugFormat("AddClientProcessItem", new object[0]);
            if (target.Process == null)
            {
                Log<WorkGroupTreeNode>.Logger.DebugFormat("Key [{0}], Target [{1}]", key, "NULL");
            }
            else
            {
                Log<WorkGroupTreeNode>.Logger.DebugFormat("Key [{0}], Target [{1}]", key, target.Process.Description);
            }
            this.naConditions.Remove(key);
            this.items.Add(target);
            if (this.showing || target.ShowCount > 0)
            {
                this.AddToListView(target);
            }
            else
            {
                Log<WorkGroupTreeNode>.Logger.DebugFormat("\"showing\" is false and \"target.ShowCount\" <= 0.", new object[0]);
            }
            Log<WorkGroupTreeNode>.Logger.DebugFormat("=================================================", new object[0]);
        }

        private void AddToListView(ClientProcessItem target)
        {
            Log<WorkGroupTreeNode>.Logger.DebugFormat("=================================================", new object[0]);
            Log<WorkGroupTreeNode>.Logger.DebugFormat("AddToListView", new object[0]);
            if (target.Process == null)
            {
                Log<WorkGroupTreeNode>.Logger.DebugFormat("Target [{0}]", "NULL");
            }
            else
            {
                Log<WorkGroupTreeNode>.Logger.DebugFormat("Target [{0}]", target.Process.Description);
            }
            Log<WorkGroupTreeNode>.Logger.DebugFormat("Start Add in ListView", new object[0]);
            if (target == null)
            {
                Log<WorkGroupTreeNode>.Logger.DebugFormat("Target is null.", new object[0]);
                return;
            }
            IComparer comparer = WorkGroup.Sort.ShowListView.ListViewItemSorter;
            int top = 0;
            int bottom = WorkGroup.Sort.ShowListView.Items.Count;
            int order = (WorkGroup.Sort.ShowListView.Sorting == SortOrder.Ascending) ? 1 : -1;
            while (top < bottom)
            {
                int middle = (top + bottom) / 2;
                ListViewItem item = WorkGroup.Sort.ShowListView.Items[middle];
                int result = comparer.Compare(target, item) * order;
                if (result > 0)
                {
                    top = middle + 1;
                }
                else
                {
                    if (result >= 0)
                    {
                        Log<WorkGroupTreeNode>.Logger.DebugFormat("!!! Target is already exist.", new object[0]);
                        Log<WorkGroupTreeNode>.Logger.DebugFormat("End Add in ListView", new object[0]);
                        Log<WorkGroupTreeNode>.Logger.DebugFormat("=================================================", new object[0]);
                        return;
                    }
                    bottom = middle;
                }
            }
            if (target.Process != null)
            {
                if (target.Process.DefaultSelect)
                {
                    target.Selected = true;
                }
            }
            else
            {
                Log<WorkGroupTreeNode>.Logger.DebugFormat("Target.Process is NULL!!!!!!!!!!!!!!", new object[0]);
            }
            target.ShowCount = 1;
            WorkGroup.Sort.ShowListView.Items.Insert(top, target);
            Log<WorkGroupTreeNode>.Logger.DebugFormat("End Add in ListView", new object[0]);
            Log<WorkGroupTreeNode>.Logger.DebugFormat("=================================================", new object[0]);
        }

        public void RemoveClientProcessItem(string key, ClientProcessItem target)
        {
            Log<WorkGroupTreeNode>.Logger.DebugFormat("=================================================", new object[0]);
            Log<WorkGroupTreeNode>.Logger.DebugFormat("RemoveClientProcessItem", new object[0]);
            if (target.Process == null)
            {
                Log<WorkGroupTreeNode>.Logger.DebugFormat("Key [{0}], Target [{1}]", key, "NULL");
            }
            else
            {
                Log<WorkGroupTreeNode>.Logger.DebugFormat("Key [{0}], Target [{1}]", key, target.Process.Description);
            }
            this.naConditions.Add(key);
            this.items.Remove(target);
            this.RemoveFromListView(target);
            Log<WorkGroupTreeNode>.Logger.DebugFormat("=================================================", new object[0]);
        }

        private void RemoveFromListView(ClientProcessItem target)
        {
            Log<WorkGroupTreeNode>.Logger.DebugFormat("=================================================", new object[0]);
            Log<WorkGroupTreeNode>.Logger.DebugFormat("RemoveFromListView", new object[0]);
            if (target.Process == null)
            {
                Log<WorkGroupTreeNode>.Logger.DebugFormat("Target [{0}]", "NULL");
            }
            else
            {
                Log<WorkGroupTreeNode>.Logger.DebugFormat("Target [{0}]", target.Process.Description);
            }
            Log<WorkGroupTreeNode>.Logger.DebugFormat("Start Remove in ListView", new object[0]);
            ClientProcessItem item = WorkGroup.Sort.ShowListView.Items[target.Name] as ClientProcessItem;
            if (item != null)
            {
                if (--item.ShowCount == 0)
                {
                    item.Selected = false;
                    WorkGroup.Sort.ShowListView.Items.RemoveAt(item.Index);
                }
            }
            else
            {
                Log<WorkGroupTreeNode>.Logger.DebugFormat("item is null", new object[0]);
            }
            Log<WorkGroupTreeNode>.Logger.DebugFormat("End Remove in ListView", new object[0]);
            Log<WorkGroupTreeNode>.Logger.DebugFormat("=================================================", new object[0]);
        }

        public void RecalculateTreeColor()
        {
            if (base.Nodes.Count == 0)
            {
                bool crashed = false;
                foreach (ClientProcessItem item in this.items)
                {
                    if (item.Process.State == RCProcess.ProcessState.Crash || item.Process.State == RCProcess.ProcessState.Freezing)
                    {
                        crashed = true;
                    }
                }
                if (crashed)
                {
                    base.ForeColor = Color.Red;
                    return;
                }
                if (this.naConditions.Count > 0)
                {
                    base.ForeColor = Color.SlateBlue;
                    return;
                }
                base.ForeColor = Color.Black;
            }
        }

        private WorkGroupTreeNode.SelectedCount RecalculateItemCount()
        {
            if (base.Nodes.Count == 0)
            {
                int selectedCount = 0;
                bool crashed = false;
                foreach (ClientProcessItem item in this.items)
                {
                    if (item.ShowCount > 0)
                    {
                        selectedCount++;
                    }
                    if (item.Process.State == RCProcess.ProcessState.Crash || item.Process.State == RCProcess.ProcessState.Freezing)
                    {
                        crashed = true;
                    }
                }
                string unconnected = string.Empty;
                if (crashed)
                {
                    base.ForeColor = Color.Red;
                }
                else if (this.naConditions.Count > 0)
                {
                    base.ForeColor = Color.SlateBlue;
                }
                else
                {
                    base.ForeColor = Color.Black;
                }
                if (this.naConditions.Count > 0)
                {
                    StringBuilder sb = new StringBuilder();
                    foreach (string s in this.naConditions)
                    {
                        sb.AppendLine(s);
                    }
                    unconnected = sb.ToString();
                    base.ToolTipText = string.Format("현재 접속되지 않은 클라이언트 :\n\n{0}\n합계: {1}", unconnected, this.naConditions.Count);
                    base.ForeColor = Color.SlateBlue;
                }
                else
                {
                    base.ToolTipText = string.Empty;
                }
                this.SetCount(selectedCount, this.Connected, this.Total);
                return new WorkGroupTreeNode.SelectedCount(selectedCount, this.Connected, this.Total, unconnected, this.showing);
            }
            WorkGroupTreeNode.SelectedCount selectedCount2 = new WorkGroupTreeNode.SelectedCount(0, 0, 0, string.Empty, false);
            foreach (WorkGroupTreeNode node in base.Nodes)
            {
                selectedCount2 += node.RecalculateItemCount();
            }
            this.showing = selectedCount2.Checked;
            if (selectedCount2.Connected != selectedCount2.Total)
            {
                base.ToolTipText = string.Format("현재 접속되지 않은 클라이언트 :\n\n{0}\n합계: {1}", selectedCount2.Unconnected, selectedCount2.Total - selectedCount2.Connected);
                base.ForeColor = Color.SlateBlue;
            }
            else
            {
                base.ToolTipText = string.Empty;
                base.ForeColor = Color.Black;
            }
            this.SetCount(selectedCount2.Selected, selectedCount2.Connected, selectedCount2.Total);
            return selectedCount2;
        }

        private void RecalculateItemCountStart()
        {
            this.RecalculateItemCount();
        }

        private void RecalculateItemCountThreadStart()
        {
            while (true)
            {
                int sleepTime;
                lock (this)
                {
                    sleepTime = (int)(this.recalculateTime - DateTime.Now.Ticks / 10000L);
                    if (sleepTime <= 0)
                    {
                        this.threadRecalculate = null;
                        base.TreeView.BeginInvoke(new WorkGroupTreeNode.VoidDelegate(this.RecalculateItemCountStart), null);
                        break;
                    }
                }
                Thread.Sleep(sleepTime);
            }
        }

        private void RecalculateItemCountThread()
        {
            this.recalculateTime = DateTime.Now.Ticks / 10000L + 100L;
            lock (this)
            {
                if (this.threadRecalculate == null)
                {
                    this.threadRecalculate = new Thread(new ThreadStart(this.RecalculateItemCountThreadStart));
                    this.threadRecalculate.Start();
                }
            }
        }

        public void RecalculateItemCountFromRoot()
        {
            WorkGroupTreeNode parentNode = this;
            while (parentNode.Parent != null)
            {
                parentNode = (parentNode.Parent as WorkGroupTreeNode);
            }
            parentNode.RecalculateItemCountThread();
        }

        private void SetCount(int selectedItemCount, int connected, int total)
        {
            base.Text = string.Format("{0} [{1}/{2}/{3}]", new object[]
            {
                this.Title,
                selectedItemCount,
                connected,
                total
            });
            if (selectedItemCount > 0)
            {
                if (selectedItemCount == connected)
                {
                    this.checkState = CheckState.Checked;
                }
                else
                {
                    this.checkState = CheckState.Indeterminate;
                }
            }
            else if (this.showing)
            {
                this.checkState = CheckState.Indeterminate;
            }
            else
            {
                this.checkState = CheckState.Unchecked;
            }
            base.ImageIndex = (base.SelectedImageIndex = (int)this.checkState);
        }

        public void CheckBoxClicked()
        {
            switch (this.CheckState)
            {
                case CheckState.Unchecked:
                    this.CheckState = CheckState.Checked;
                    return;
                case CheckState.Indeterminate:
                case CheckState.Checked:
                    this.CheckState = CheckState.Unchecked;
                    return;
                default:
                    return;
            }
        }

        public void ChangeCheckState(CheckState newState, bool recalculate, bool updateListView)
        {
            if (newState == CheckState.Indeterminate)
            {
                throw new ArgumentException("It cannot be Indeterminate", "newState");
            }
            if (base.Nodes.Count != 0)
            {
                if (this.checkState == newState)
                {
                    return;
                }
                this.showing = newState == CheckState.Checked;
                this.checkState = newState;
                foreach (WorkGroupTreeNode node in base.Nodes)
                {
                    node.ChangeCheckState(newState, false, updateListView);
                }
            }
            else
            {
                this.checkState = newState;
                if (newState == CheckState.Checked & updateListView)
                {
                    this.showing = true;
                    foreach (ClientProcessItem item in this.items)
                    {
                        this.AddToListView(item);
                    }
                }
                else if (newState == CheckState.Unchecked)
                {
                    this.showing = false;
                    if (updateListView)
                    {
                        foreach (ClientProcessItem clientProcessItem in this.items)
                        {
                            this.RemoveFromListView(clientProcessItem);
                        }
                    }
                }
            }
            if (recalculate)
            {
                this.RecalculateItemCountFromRoot();
            }
        }

        public int CompareTo(object obj)
        {
            WorkGroupTreeNode node = obj as WorkGroupTreeNode;
            if (node != null)
            {
                return this.Priority.CompareTo(node.Priority);
            }
            return 1;
        }

        private bool showing;

        private CheckState checkState;

        private List<ClientProcessItem> items;

        private HashSet<string> naConditions;

        private long recalculateTime;

        private Thread threadRecalculate;

        private int priority;

        private WorkGroup parent;

        private delegate void VoidDelegate();

        private struct SelectedCount
        {
            public int Selected
            {
                get
                {
                    return this.selected;
                }
            }

            public int Connected
            {
                get
                {
                    return this.connected;
                }
            }

            public int Total
            {
                get
                {
                    return this.total;
                }
            }

            public bool Checked
            {
                get
                {
                    return this.checkedItem;
                }
            }

            public string Unconnected
            {
                get
                {
                    return this.unconnected;
                }
            }

            public SelectedCount(int _selected, int _connected, int _total, string _unconnected, bool _checkedItem)
            {
                this.selected = _selected;
                this.connected = _connected;
                this.total = _total;
                this.unconnected = _unconnected;
                this.checkedItem = _checkedItem;
            }

            public static WorkGroupTreeNode.SelectedCount operator +(WorkGroupTreeNode.SelectedCount item1, WorkGroupTreeNode.SelectedCount item2)
            {
                return new WorkGroupTreeNode.SelectedCount(item1.Selected + item2.Selected, item1.Connected + item2.Connected, item1.Total + item2.Total, item1.Unconnected + item2.Unconnected, item1.Checked | item2.Checked);
            }

            private int selected;

            private int connected;

            private int total;

            private string unconnected;

            private bool checkedItem;
        }
    }
}
