using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using RemoteControlSystem;

namespace HeroesOpTool.RCUser.ServerMonitorSystem.SystemManage
{
	public class ServerGroupControl : UserControl
	{
		public ServerGroupControl()
		{
			this.InitializeComponent();
			this.menuItemWorkGroupRename.Text = LocalizeText.Get(361);
			this.menuItemWorkGroupNew.Text = LocalizeText.Get(362);
			this.menuItemWorkGroupDelete.Text = LocalizeText.Get(363);
			this.menuItemWorkGroupMoveUp.Text = LocalizeText.Get(364);
			this.menuItemWorkGroupMoveDown.Text = LocalizeText.Get(365);
			this.menuItemWorkGroupNewRoot.Text = LocalizeText.Get(366);
			this.columnHeader1.Text = LocalizeText.Get(193);
			this.menuItemClientProcessRemove.Text = LocalizeText.Get(371);
			this.labelServerGroup.Text = LocalizeText.Get(516);
			this.labelBound.Text = LocalizeText.Get(517);
			this.columnHeader3.Text = LocalizeText.Get(193);
			this.labelUnbound.Text = LocalizeText.Get(377);
			this._serverRefCountList = new SortedList<string, int>();
		}

		public bool Modified { get; private set; }

		public string SerializedText
		{
			get
			{
				ServerGroupStructureNode[] array = new ServerGroupStructureNode[this.treeViewServerGroup.Nodes.Count];
				for (int i = 0; i < array.Length; i++)
				{
					array[i] = (ServerGroupControl.SimpleServerGroupTreeNode)this.treeViewServerGroup.Nodes[i];
				}
				XmlSerializationServerGroups xmlSerializationServerGroups = array;
				return xmlSerializationServerGroups.GetString();
			}
		}

		public void AddServerGroup(IWorkGroupStructureNode[] serverGroupRootNodes)
		{
			if (serverGroupRootNodes != null)
			{
				foreach (ServerGroupStructureNode node in serverGroupRootNodes)
				{
					this.AddTo(this.treeViewServerGroup.Nodes, node);
				}
				this.treeViewServerGroup.ExpandAll();
			}
		}

		private void AddTo(TreeNodeCollection nodeList, ServerGroupStructureNode node)
		{
			ServerGroupControl.SimpleServerGroupTreeNode simpleServerGroupTreeNode = new ServerGroupControl.SimpleServerGroupTreeNode(node.Name, node.Authority);
			nodeList.Add(simpleServerGroupTreeNode);
			if (node.ChildNodes != null)
			{
				foreach (ServerGroupStructureNode node2 in node.ChildNodes)
				{
					this.AddTo(simpleServerGroupTreeNode.Nodes, node2);
				}
			}
			if (node.Childs != null)
			{
				foreach (IWorkGroupCondition workGroupCondition in node.Childs)
				{
					string server = workGroupCondition as ServerGroupCondition;
					simpleServerGroupTreeNode.AddServer(server);
					this.IncreaseServerRefCount(server);
				}
			}
		}

		public void AddClients(ICollection<RCClient> clientList)
		{
			foreach (RCClient client in clientList)
			{
				this.AddClient(client);
			}
		}

		public void AddClient(RCClient client)
		{
			this.IncreaseServerRefCountItem(client.Name);
		}

		private void AddUnbound(string server)
		{
			int i = 0;
			int num = this.listViewUnbound.Items.Count;
			while (i < num)
			{
				int num2 = (i + num) / 2;
				ListViewItem listViewItem = this.listViewUnbound.Items[num2];
				int num3 = string.Compare(server, listViewItem.Tag as string);
				if (num3 < 0)
				{
					num = num2;
				}
				else
				{
					if (num3 <= 0)
					{
						return;
					}
					i = num2 + 1;
				}
			}
			this.listViewUnbound.Items.Insert(i, server, server, 0);
		}

		private void RemoveUnbound(string server)
		{
			this.listViewUnbound.Items.RemoveByKey(server);
		}

		private void IncreaseServerRefCount(string server)
		{
			int num = 0;
			if (this._serverRefCountList.ContainsKey(server))
			{
				num = this._serverRefCountList[server];
			}
			else
			{
				this._serverRefCountList.Add(server, 0);
			}
			num++;
			this._serverRefCountList[server] = num;
			if (num == 1)
			{
				this.RemoveUnbound(server);
			}
		}

		private void DecreaseServerRefCount(string server)
		{
			int num = 1;
			if (this._serverRefCountList.ContainsKey(server))
			{
				num = this._serverRefCountList[server];
			}
			else
			{
				this._serverRefCountList.Add(server, 1);
			}
			if (num - 1 == 0)
			{
				this.AddUnbound(server);
			}
		}

		private void IncreaseServerRefCountItem(string server)
		{
			if (!this._serverRefCountList.ContainsKey(server))
			{
				this._serverRefCountList.Add(server, 0);
				this.AddUnbound(server);
			}
		}

		private void RemoveServer(ServerGroupControl.SimpleServerGroupTreeNode node)
		{
			foreach (object obj in node.Nodes)
			{
				ServerGroupControl.SimpleServerGroupTreeNode node2 = (ServerGroupControl.SimpleServerGroupTreeNode)obj;
				this.RemoveServer(node2);
			}
			foreach (string server in node.Servers)
			{
				this.DecreaseServerRefCount(server);
			}
			node.Clear();
		}

		private void RemoveSpecifiedServer(ServerGroupControl.SimpleServerGroupTreeNode node, string server)
		{
			foreach (object obj in node.Nodes)
			{
				ServerGroupControl.SimpleServerGroupTreeNode node2 = (ServerGroupControl.SimpleServerGroupTreeNode)obj;
				this.RemoveSpecifiedServer(node2, server);
			}
			if (node.RemoveServer(server))
			{
				this.DecreaseServerRefCount(server);
			}
		}

		private void RemoveServerGroup(ServerGroupControl.SimpleServerGroupTreeNode node)
		{
			node.Remove();
			this.RemoveServer(node);
		}

		private void TreeViewServerGroup_MouseDown(object sender, MouseEventArgs args)
		{
			this._selectedNode = this.treeViewServerGroup.GetNodeAt(args.X, args.Y);
		}

		private void TreeViewServerGroup_AfterSelect(object sender, TreeViewEventArgs args)
		{
			this.listViewBound.Items.Clear();
			if (args.Node != null)
			{
				this._selectedNode = args.Node;
				foreach (string text in ((ServerGroupControl.SimpleServerGroupTreeNode)this._selectedNode).Servers)
				{
					this.listViewBound.Items.Add(text);
				}
			}
		}

		private void TreeViewServerGroup_AfterLabelEdit(object sender, NodeLabelEditEventArgs args)
		{
			if (args.Label == null)
			{
				if (args.Node.Text.Length == 0)
				{
					args.CancelEdit = true;
					args.Node.Remove();
				}
				return;
			}
			if (args.Label.Length == 0)
			{
				Utility.ShowErrorMessage(LocalizeText.Get(378));
				args.CancelEdit = true;
				args.Node.BeginEdit();
				return;
			}
			TreeNode treeNode;
			if (args.Node.Parent == null)
			{
				treeNode = this.treeViewServerGroup.Nodes[0];
			}
			else
			{
				treeNode = args.Node.Parent.FirstNode;
			}
			while (treeNode != null)
			{
				if (treeNode.Text == args.Label && treeNode != args.Node)
				{
					Utility.ShowErrorMessage(LocalizeText.Get(379));
					args.CancelEdit = true;
					args.Node.BeginEdit();
					return;
				}
				treeNode = treeNode.NextNode;
			}
			this.Modified = true;
			args.Node.EndEdit(false);
		}

		private void menuItemServerGroupNew_Click(object sender, EventArgs e)
		{
			if (this._selectedNode != null)
			{
				this.treeViewServerGroup.SelectedNode = this._selectedNode;
				if (!this._selectedNode.IsEditing)
				{
					if (((ServerGroupControl.SimpleServerGroupTreeNode)this._selectedNode).Servers.Count > 0)
					{
						if (!Utility.InputYesNoFromWarning(LocalizeText.Get(380)))
						{
							return;
						}
						foreach (string server in ((ServerGroupControl.SimpleServerGroupTreeNode)this._selectedNode).Servers)
						{
							this.DecreaseServerRefCount(server);
						}
						((ServerGroupControl.SimpleServerGroupTreeNode)this._selectedNode).Clear();
					}
					ServerGroupControl.SimpleServerGroupTreeNode simpleServerGroupTreeNode = new ServerGroupControl.SimpleServerGroupTreeNode("", ((ServerGroupControl.SimpleServerGroupTreeNode)this._selectedNode).Authority);
					this._selectedNode.Nodes.Add(simpleServerGroupTreeNode);
					this._selectedNode.Expand();
					this.treeViewServerGroup.SelectedNode = simpleServerGroupTreeNode;
					this.Modified = true;
					simpleServerGroupTreeNode.BeginEdit();
				}
			}
		}

		private void menuItemServerGroupRename_Click(object sender, EventArgs e)
		{
			if (this._selectedNode != null)
			{
				this.treeViewServerGroup.SelectedNode = this._selectedNode;
				if (!this._selectedNode.IsEditing)
				{
					this._selectedNode.BeginEdit();
				}
			}
		}

		private void menuItemServerGroupDelete_Click(object sender, EventArgs e)
		{
			if (this._selectedNode != null)
			{
				TreeNode prevVisibleNode = this._selectedNode.PrevVisibleNode;
				this.treeViewServerGroup.SelectedNode = this._selectedNode;
				this.RemoveServerGroup((ServerGroupControl.SimpleServerGroupTreeNode)this._selectedNode);
				if (this.treeViewServerGroup.SelectedNode == null)
				{
					this.listViewBound.Items.Clear();
				}
				this.Modified = true;
				if (prevVisibleNode != null)
				{
					this.treeViewServerGroup.SelectedNode = prevVisibleNode;
				}
			}
		}

		private void menuItemServerGroupMoveUp_Click(object sender, EventArgs e)
		{
			if (this._selectedNode != null)
			{
				this.treeViewServerGroup.SelectedNode = this._selectedNode;
				TreeNodeCollection nodes;
				if (this._selectedNode.Parent == null)
				{
					nodes = this.treeViewServerGroup.Nodes;
				}
				else
				{
					nodes = this._selectedNode.Parent.Nodes;
				}
				if (nodes[0] != this._selectedNode)
				{
					int index = this._selectedNode.Index;
					TreeNode selectedNode = this._selectedNode;
					this.Modified = true;
					selectedNode.Remove();
					nodes.Insert(index - 1, selectedNode);
					this.treeViewServerGroup.SelectedNode = selectedNode;
				}
			}
		}

		private void menuItemServerGroupMoveDown_Click(object sender, EventArgs e)
		{
			if (this._selectedNode != null)
			{
				this.treeViewServerGroup.SelectedNode = this._selectedNode;
				TreeNodeCollection nodes;
				if (this._selectedNode.Parent == null)
				{
					nodes = this.treeViewServerGroup.Nodes;
				}
				else
				{
					nodes = this._selectedNode.Parent.Nodes;
				}
				if (nodes[nodes.Count - 1] != this._selectedNode)
				{
					int index = this._selectedNode.Index;
					TreeNode selectedNode = this._selectedNode;
					this.Modified = true;
					selectedNode.Remove();
					nodes.Insert(index + 1, selectedNode);
					this.treeViewServerGroup.SelectedNode = selectedNode;
				}
			}
		}

		private void menuItemServerGroupNewRoot_Click(object sender, EventArgs e)
		{
			ServerGroupControl.SimpleServerGroupTreeNode simpleServerGroupTreeNode = new ServerGroupControl.SimpleServerGroupTreeNode("", Authority.Supervisor);
			this.treeViewServerGroup.Nodes.Add(simpleServerGroupTreeNode);
			this.treeViewServerGroup.SelectedNode = simpleServerGroupTreeNode;
			this.Modified = true;
			simpleServerGroupTreeNode.BeginEdit();
		}

		private void menuItemServerRemove_Click(object sender, EventArgs e)
		{
			if (this._selectedNode != null)
			{
				foreach (object obj in this.listViewBound.SelectedItems)
				{
					ListViewItem listViewItem = (ListViewItem)obj;
					((ServerGroupControl.SimpleServerGroupTreeNode)this._selectedNode).RemoveServer(listViewItem.Text);
					this.DecreaseServerRefCount(listViewItem.Text);
				}
				this.Modified = true;
				this.TreeViewServerGroup_AfterSelect(sender, new TreeViewEventArgs(this._selectedNode));
			}
		}

		private void ListViewBoundServer_ItemDrag(object sender, ItemDragEventArgs e)
		{
			this._draggingConditionItem = true;
			this._boundSourceNode = this._selectedNode;
			ListViewItem[] array = new ListViewItem[this.listViewBound.SelectedItems.Count];
			this.listViewBound.SelectedItems.CopyTo(array, 0);
			base.DoDragDrop(array, DragDropEffects.Copy | DragDropEffects.Move);
		}

		private void ListViewBoundServer_DragEnter(object sender, DragEventArgs args)
		{
			this._validDragToBoundList = (args.Data.GetData(typeof(ListViewItem[])) != null);
			if (this._validDragToBoundList && this.treeViewServerGroup.SelectedNode != null && this.treeViewServerGroup.SelectedNode.Nodes.Count == 0)
			{
				args.Effect = DragDropEffects.Move;
			}
		}

		private void ListViewBoundServer_DragDrop(object sender, DragEventArgs args)
		{
			if (this._validDragToBoundList)
			{
				if (this.treeViewServerGroup.SelectedNode == null)
				{
					Utility.ShowErrorMessage(LocalizeText.Get(387));
					return;
				}
				TreeNode selectedNode = this.treeViewServerGroup.SelectedNode;
				if (selectedNode.Nodes.Count > 0)
				{
					Utility.ShowErrorMessage(LocalizeText.Get(388));
					return;
				}
				ListViewItem[] array = (ListViewItem[])args.Data.GetData(typeof(ListViewItem[]));
				if (array == null)
				{
					return;
				}
				foreach (ListViewItem listViewItem in array)
				{
					try
					{
						((ServerGroupControl.SimpleServerGroupTreeNode)selectedNode).AddServer(listViewItem.Text);
						this.IncreaseServerRefCount(listViewItem.Text);
					}
					catch (ArgumentException)
					{
					}
				}
				this.Modified = true;
				this.TreeViewServerGroup_AfterSelect(sender, new TreeViewEventArgs(selectedNode));
			}
		}

		private void ListViewUnboundServer_ItemDrag(object sender, ItemDragEventArgs e)
		{
			this._draggingConditionItem = false;
			ListViewItem[] array = new ListViewItem[this.listViewUnbound.SelectedItems.Count];
			this.listViewUnbound.SelectedItems.CopyTo(array, 0);
			base.DoDragDrop(array, DragDropEffects.Move);
		}

		private void ListViewUnboundServer_DragEnter(object sender, DragEventArgs args)
		{
			this._validDragToUnboundList = (args.Data.GetData(typeof(ListViewItem[])) != null);
			if (this._validDragToUnboundList)
			{
				args.Effect = DragDropEffects.Move;
			}
		}

		private void ListViewUnboundServer_DragDrop(object sender, DragEventArgs args)
		{
			if (this._validDragToUnboundList)
			{
				string[] array = (string[])args.Data.GetData(typeof(ListViewItem[]));
				if (array == null)
				{
					return;
				}
				if (Utility.InputYesNoFromWarning(LocalizeText.Get(389)))
				{
					foreach (string server in array)
					{
						foreach (object obj in this.treeViewServerGroup.Nodes)
						{
							ServerGroupControl.SimpleServerGroupTreeNode node = (ServerGroupControl.SimpleServerGroupTreeNode)obj;
							this.RemoveSpecifiedServer(node, server);
						}
					}
				}
				this.Modified = true;
				TreeNode selectedNode = this.treeViewServerGroup.SelectedNode;
				if (selectedNode != null)
				{
					this.TreeViewServerGroup_AfterSelect(sender, new TreeViewEventArgs(selectedNode));
				}
			}
		}

		private void TreeViewServerGroup_DragEnter(object sender, DragEventArgs args)
		{
			this._validDragToWorkGroupTree = (args.Data.GetData(typeof(ListViewItem[])) != null);
		}

		private void TreeViewServerGroup_DragOver(object sender, DragEventArgs args)
		{
			if (this._validDragToWorkGroupTree)
			{
				Point point = this.treeViewServerGroup.PointToClient(new Point(args.X, args.Y));
				TreeNode nodeAt = this.treeViewServerGroup.GetNodeAt(point.X, point.Y);
				if (nodeAt != null)
				{
					this.treeViewServerGroup.SelectedNode = nodeAt;
					if (this._draggingConditionItem && (args.KeyState & 8) != 0)
					{
						args.Effect = DragDropEffects.Copy;
						return;
					}
					args.Effect = DragDropEffects.Move;
					return;
				}
				else
				{
					args.Effect = DragDropEffects.None;
				}
			}
		}

		private void TreeViewServerGroup_DragDrop(object sender, DragEventArgs args)
		{
			if (this._validDragToWorkGroupTree)
			{
				this._validDragToBoundList = true;
				if (this._draggingConditionItem && this._selectedNode == this._boundSourceNode)
				{
					return;
				}
				this.ListViewBoundServer_DragDrop(sender, args);
				ListViewItem[] array = (ListViewItem[])args.Data.GetData(typeof(ListViewItem[]));
				if (array == null)
				{
					return;
				}
				if (this._draggingConditionItem && (args.KeyState & 8) == 0)
				{
					foreach (ListViewItem listViewItem in array)
					{
						((ServerGroupControl.SimpleServerGroupTreeNode)this._boundSourceNode).RemoveServer(listViewItem.Text);
						this.DecreaseServerRefCount(listViewItem.Text);
					}
				}
			}
		}

		private void listViewUnbound_ColumnClick(object sender, ColumnClickEventArgs e)
		{
			if (this.listViewUnbound.ListViewItemSorter == null)
			{
				this.listViewUnbound.ListViewItemSorter = new Utility.ListViewItemComparer(e.Column);
			}
			Utility.ListViewItemComparer listViewItemComparer = this.listViewUnbound.ListViewItemSorter as Utility.ListViewItemComparer;
			listViewItemComparer.IsAscending = !listViewItemComparer.IsAscending;
			this.listViewUnbound.Sort();
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		private void InitializeComponent()
		{
			this.components = new Container();
			this.treeViewServerGroup = new TreeView();
			this.contextMenuServerGroup = new ContextMenu();
			this.menuItemWorkGroupRename = new MenuItem();
			this.menuItemWorkGroupNew = new MenuItem();
			this.menuItemWorkGroupDelete = new MenuItem();
			this.menuItem1 = new MenuItem();
			this.menuItemWorkGroupMoveUp = new MenuItem();
			this.menuItemWorkGroupMoveDown = new MenuItem();
			this.menuItem2 = new MenuItem();
			this.menuItemWorkGroupNewRoot = new MenuItem();
			this.listViewBound = new ListView();
			this.columnHeader1 = new ColumnHeader();
			this.contextMenuProcess = new ContextMenu();
			this.menuItemClientProcessRemove = new MenuItem();
			this.labelServerGroup = new Label();
			this.labelBound = new Label();
			this.listViewUnbound = new ListView();
			this.columnHeader3 = new ColumnHeader();
			this.labelUnbound = new Label();
			this.panelLeft = new Panel();
			this.splitterV = new Splitter();
			this.panelRightUp = new Panel();
			this.splitterH = new Splitter();
			this.panelRightDown = new Panel();
			this.toolTipServerGroupControl = new ToolTip(this.components);
			this.panelLeft.SuspendLayout();
			this.panelRightUp.SuspendLayout();
			this.panelRightDown.SuspendLayout();
			base.SuspendLayout();
			this.treeViewServerGroup.AllowDrop = true;
			this.treeViewServerGroup.Anchor = (AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right);
			this.treeViewServerGroup.ContextMenu = this.contextMenuServerGroup;
			this.treeViewServerGroup.HideSelection = false;
			this.treeViewServerGroup.LabelEdit = true;
			this.treeViewServerGroup.Location = new Point(8, 32);
			this.treeViewServerGroup.Name = "treeViewServerGroup";
			this.treeViewServerGroup.Size = new Size(192, 360);
			this.treeViewServerGroup.TabIndex = 0;
			this.toolTipServerGroupControl.SetToolTip(this.treeViewServerGroup, "현재 서버 그룹의 구조를 나타냅니다.\n마우스 오른쪽 버튼을 눌러 원하는 작업의 내용을 선택하세요.");
			this.treeViewServerGroup.AfterLabelEdit += this.TreeViewServerGroup_AfterLabelEdit;
			this.treeViewServerGroup.DragDrop += this.TreeViewServerGroup_DragDrop;
			this.treeViewServerGroup.AfterSelect += this.TreeViewServerGroup_AfterSelect;
			this.treeViewServerGroup.MouseDown += this.TreeViewServerGroup_MouseDown;
			this.treeViewServerGroup.DragEnter += this.TreeViewServerGroup_DragEnter;
			this.treeViewServerGroup.DragOver += this.TreeViewServerGroup_DragOver;
			this.contextMenuServerGroup.MenuItems.AddRange(new MenuItem[]
			{
				this.menuItemWorkGroupRename,
				this.menuItemWorkGroupNew,
				this.menuItemWorkGroupDelete,
				this.menuItem1,
				this.menuItemWorkGroupMoveUp,
				this.menuItemWorkGroupMoveDown,
				this.menuItem2,
				this.menuItemWorkGroupNewRoot
			});
			this.menuItemWorkGroupRename.Index = 0;
			this.menuItemWorkGroupRename.Shortcut = Shortcut.F2;
			this.menuItemWorkGroupRename.Text = "작업 그룹 이름 변경";
			this.menuItemWorkGroupRename.Click += this.menuItemServerGroupRename_Click;
			this.menuItemWorkGroupNew.Index = 1;
			this.menuItemWorkGroupNew.Shortcut = Shortcut.Ins;
			this.menuItemWorkGroupNew.Text = "자식 작업 그룹 추가";
			this.menuItemWorkGroupNew.Click += this.menuItemServerGroupNew_Click;
			this.menuItemWorkGroupDelete.Index = 2;
			this.menuItemWorkGroupDelete.Shortcut = Shortcut.Del;
			this.menuItemWorkGroupDelete.Text = "작업 그룹 삭제";
			this.menuItemWorkGroupDelete.Click += this.menuItemServerGroupDelete_Click;
			this.menuItem1.Index = 3;
			this.menuItem1.Text = "-";
			this.menuItemWorkGroupMoveUp.Index = 4;
			this.menuItemWorkGroupMoveUp.Shortcut = Shortcut.CtrlA;
			this.menuItemWorkGroupMoveUp.Text = "작업 그룹 위로 이동";
			this.menuItemWorkGroupMoveUp.Click += this.menuItemServerGroupMoveUp_Click;
			this.menuItemWorkGroupMoveDown.Index = 5;
			this.menuItemWorkGroupMoveDown.Shortcut = Shortcut.CtrlZ;
			this.menuItemWorkGroupMoveDown.Text = "작업 그룹 아래로 이동";
			this.menuItemWorkGroupMoveDown.Click += this.menuItemServerGroupMoveDown_Click;
			this.menuItem2.Index = 6;
			this.menuItem2.Text = "-";
			this.menuItemWorkGroupNewRoot.Index = 7;
			this.menuItemWorkGroupNewRoot.Shortcut = Shortcut.CtrlIns;
			this.menuItemWorkGroupNewRoot.Text = "새 루트 작업 그룹 추가";
			this.menuItemWorkGroupNewRoot.Click += this.menuItemServerGroupNewRoot_Click;
			this.listViewBound.AllowDrop = true;
			this.listViewBound.Anchor = (AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right);
			this.listViewBound.Columns.AddRange(new ColumnHeader[]
			{
				this.columnHeader1
			});
			this.listViewBound.ContextMenu = this.contextMenuProcess;
			this.listViewBound.FullRowSelect = true;
			this.listViewBound.HideSelection = false;
			this.listViewBound.Location = new Point(0, 32);
			this.listViewBound.Name = "listViewBound";
			this.listViewBound.Size = new Size(384, 136);
			this.listViewBound.TabIndex = 1;
			this.toolTipServerGroupControl.SetToolTip(this.listViewBound, "현재 선택된 서버그룹에 속한 프로그램의 리스트입니다.");
			this.listViewBound.UseCompatibleStateImageBehavior = false;
			this.listViewBound.View = View.Details;
			this.listViewBound.DragDrop += this.ListViewBoundServer_DragDrop;
			this.listViewBound.DragEnter += this.ListViewBoundServer_DragEnter;
			this.listViewBound.ItemDrag += this.ListViewBoundServer_ItemDrag;
			this.columnHeader1.Text = "컴퓨터 이름";
			this.columnHeader1.Width = 367;
			this.contextMenuProcess.MenuItems.AddRange(new MenuItem[]
			{
				this.menuItemClientProcessRemove
			});
			this.menuItemClientProcessRemove.Index = 0;
			this.menuItemClientProcessRemove.Shortcut = Shortcut.Del;
			this.menuItemClientProcessRemove.Text = "연결된 프로그램 삭제";
			this.menuItemClientProcessRemove.Click += this.menuItemServerRemove_Click;
			this.labelServerGroup.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
			this.labelServerGroup.Location = new Point(8, 8);
			this.labelServerGroup.Name = "labelServerGroup";
			this.labelServerGroup.Size = new Size(192, 24);
			this.labelServerGroup.TabIndex = 2;
			this.labelServerGroup.Text = "서버 그룹의 구조";
			this.labelServerGroup.TextAlign = ContentAlignment.MiddleLeft;
			this.labelBound.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
			this.labelBound.Location = new Point(0, 8);
			this.labelBound.Name = "labelBound";
			this.labelBound.Size = new Size(384, 24);
			this.labelBound.TabIndex = 2;
			this.labelBound.Text = "서버 그룹에 연결된 클라이언트 리스트";
			this.labelBound.TextAlign = ContentAlignment.MiddleLeft;
			this.listViewUnbound.AllowDrop = true;
			this.listViewUnbound.Anchor = (AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right);
			this.listViewUnbound.Columns.AddRange(new ColumnHeader[]
			{
				this.columnHeader3
			});
			this.listViewUnbound.FullRowSelect = true;
			this.listViewUnbound.HideSelection = false;
			this.listViewUnbound.Location = new Point(0, 24);
			this.listViewUnbound.Name = "listViewUnbound";
			this.listViewUnbound.Size = new Size(384, 192);
			this.listViewUnbound.TabIndex = 1;
			this.toolTipServerGroupControl.SetToolTip(this.listViewUnbound, "현재 접속되어 있는 원격 제어 서버의 프로그램 중, 어떤 작업그룹에도 속하지 않은 프로그램의 리스트입니다. 드래그하여 작업그룹으로 넣을 수 있습니다.");
			this.listViewUnbound.UseCompatibleStateImageBehavior = false;
			this.listViewUnbound.View = View.Details;
			this.listViewUnbound.DragDrop += this.ListViewUnboundServer_DragDrop;
			this.listViewUnbound.ColumnClick += this.listViewUnbound_ColumnClick;
			this.listViewUnbound.DragEnter += this.ListViewUnboundServer_DragEnter;
			this.listViewUnbound.ItemDrag += this.ListViewUnboundServer_ItemDrag;
			this.columnHeader3.Text = "컴퓨터 이름";
			this.columnHeader3.Width = 180;
			this.labelUnbound.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
			this.labelUnbound.Location = new Point(0, 0);
			this.labelUnbound.Name = "labelUnbound";
			this.labelUnbound.Size = new Size(384, 24);
			this.labelUnbound.TabIndex = 2;
			this.labelUnbound.Text = "어디에도 연결되지 않은 클라이언트 리스트";
			this.labelUnbound.TextAlign = ContentAlignment.MiddleLeft;
			this.panelLeft.Controls.Add(this.treeViewServerGroup);
			this.panelLeft.Controls.Add(this.labelServerGroup);
			this.panelLeft.Dock = DockStyle.Left;
			this.panelLeft.Location = new Point(0, 0);
			this.panelLeft.Name = "panelLeft";
			this.panelLeft.Size = new Size(200, 400);
			this.panelLeft.TabIndex = 3;
			this.splitterV.Location = new Point(200, 0);
			this.splitterV.Name = "splitterV";
			this.splitterV.Size = new Size(8, 400);
			this.splitterV.TabIndex = 4;
			this.splitterV.TabStop = false;
			this.panelRightUp.Controls.Add(this.listViewBound);
			this.panelRightUp.Controls.Add(this.labelBound);
			this.panelRightUp.Dock = DockStyle.Top;
			this.panelRightUp.Location = new Point(208, 0);
			this.panelRightUp.Name = "panelRightUp";
			this.panelRightUp.Size = new Size(392, 168);
			this.panelRightUp.TabIndex = 5;
			this.splitterH.Dock = DockStyle.Top;
			this.splitterH.Location = new Point(208, 168);
			this.splitterH.Name = "splitterH";
			this.splitterH.Size = new Size(392, 8);
			this.splitterH.TabIndex = 6;
			this.splitterH.TabStop = false;
			this.panelRightDown.Controls.Add(this.labelUnbound);
			this.panelRightDown.Controls.Add(this.listViewUnbound);
			this.panelRightDown.Dock = DockStyle.Fill;
			this.panelRightDown.Location = new Point(208, 176);
			this.panelRightDown.Name = "panelRightDown";
			this.panelRightDown.Size = new Size(392, 224);
			this.panelRightDown.TabIndex = 7;
			base.AutoScaleMode = AutoScaleMode.Inherit;
			base.Controls.Add(this.panelRightDown);
			base.Controls.Add(this.splitterH);
			base.Controls.Add(this.panelRightUp);
			base.Controls.Add(this.splitterV);
			base.Controls.Add(this.panelLeft);
			base.Name = "ServerGroupControl";
			base.Size = new Size(600, 400);
			this.panelLeft.ResumeLayout(false);
			this.panelRightUp.ResumeLayout(false);
			this.panelRightDown.ResumeLayout(false);
			base.ResumeLayout(false);
		}

		private TreeNode _selectedNode;

		private SortedList<string, int> _serverRefCountList;

		private bool _draggingConditionItem;

		private TreeNode _boundSourceNode;

		private bool _validDragToBoundList;

		private bool _validDragToUnboundList;

		private bool _validDragToWorkGroupTree;

		private IContainer components;

		private ColumnHeader columnHeader1;

		private TreeView treeViewServerGroup;

		private Label labelServerGroup;

		private ColumnHeader columnHeader3;

		private Panel panelLeft;

		private ContextMenu contextMenuServerGroup;

		private ListView listViewBound;

		private Label labelBound;

		private MenuItem menuItemWorkGroupNew;

		private MenuItem menuItemWorkGroupDelete;

		private ListView listViewUnbound;

		private Label labelUnbound;

		private MenuItem menuItemWorkGroupRename;

		private Splitter splitterV;

		private Panel panelRightUp;

		private Splitter splitterH;

		private Panel panelRightDown;

		private MenuItem menuItemWorkGroupNewRoot;

		private MenuItem menuItem1;

		private MenuItem menuItem2;

		private MenuItem menuItemWorkGroupMoveDown;

		private MenuItem menuItemWorkGroupMoveUp;

		private ToolTip toolTipServerGroupControl;

		private ContextMenu contextMenuProcess;

		private MenuItem menuItemClientProcessRemove;

		private class SimpleServerGroupTreeNode : TreeNode
		{
			public ICollection<string> Servers
			{
				get
				{
					return this._servers;
				}
			}

			public Authority Authority { get; private set; }

			public void AddServer(string server)
			{
				this._servers.Add(server);
			}

			public bool RemoveServer(string server)
			{
				if (this._servers.Contains(server))
				{
					this._servers.Remove(server);
					return true;
				}
				return false;
			}

			public void Clear()
			{
				this._servers.Clear();
			}

			public SimpleServerGroupTreeNode(string text, Authority authority) : base(text)
			{
				this._servers = new HashSet<string>();
				this.Authority = authority;
			}

			public static implicit operator ServerGroupStructureNode(ServerGroupControl.SimpleServerGroupTreeNode node)
			{
				ServerGroupStructureNode[] array = new ServerGroupStructureNode[node.Nodes.Count];
				for (int i = 0; i < array.Length; i++)
				{
					array[i] = (ServerGroupControl.SimpleServerGroupTreeNode)node.Nodes[i];
				}
				ServerGroupCondition[] array2 = new ServerGroupCondition[node.Servers.Count];
				int num = 0;
				foreach (string target in node.Servers)
				{
					array2[num++] = target;
				}
				return new ServerGroupStructureNode(node.Text, node.Authority, array, array2);
			}

			private HashSet<string> _servers;
		}
	}
}
