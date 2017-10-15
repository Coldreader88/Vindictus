using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using RemoteControlSystem;

namespace HeroesOpTool.RCUser.ServerMonitorSystem.SystemManage
{
	public class WorkGroupControl : UserControl
	{
		public WorkGroupControl()
		{
			this.InitializeComponent();
			this._conditionCountList = new SortedList();
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
			this.treeViewWorkGroup = new TreeView();
			this.contextMenuWorkGroup = new ContextMenu();
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
			this.columnHeader2 = new ColumnHeader();
			this.contextMenuProcess = new ContextMenu();
			this.menuItemClientProcessAdd = new MenuItem();
			this.menuItemClientProcessRemove = new MenuItem();
			this.labelWorkGroup = new Label();
			this.labelClientProcess = new Label();
			this.listViewUnbound = new ListView();
			this.columnHeader3 = new ColumnHeader();
			this.columnHeader4 = new ColumnHeader();
			this.labelUnboundProcess = new Label();
			this.panelLeft = new Panel();
			this.splitterV = new Splitter();
			this.panelRightUp = new Panel();
			this.splitterH = new Splitter();
			this.panelRightDown = new Panel();
			this.toolTipWorkGroupControl = new ToolTip(this.components);
			this.panelLeft.SuspendLayout();
			this.panelRightUp.SuspendLayout();
			this.panelRightDown.SuspendLayout();
			base.SuspendLayout();
			this.treeViewWorkGroup.AllowDrop = true;
			this.treeViewWorkGroup.Anchor = (AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right);
			this.treeViewWorkGroup.ContextMenu = this.contextMenuWorkGroup;
			this.treeViewWorkGroup.HideSelection = false;
			this.treeViewWorkGroup.LabelEdit = true;
			this.treeViewWorkGroup.Location = new Point(8, 32);
			this.treeViewWorkGroup.Name = "treeViewWorkGroup";
			this.treeViewWorkGroup.Size = new Size(192, 360);
			this.treeViewWorkGroup.TabIndex = 0;
			this.toolTipWorkGroupControl.SetToolTip(this.treeViewWorkGroup, "현재 작업 그룹의 구조를 나타냅니다.\n마우스 오른쪽 버튼을 눌러 원하는 작업의 내용을 선택하세요.");
			this.treeViewWorkGroup.AfterLabelEdit += this.TreeViewWorkGroup_AfterLabelEdit;
			this.treeViewWorkGroup.DragDrop += this.TreeViewWorkGroup_DragDrop;
			this.treeViewWorkGroup.AfterSelect += this.TreeViewWorkGroup_AfterSelect;
			this.treeViewWorkGroup.MouseDown += this.TreeViewWorkGroup_MouseDown;
			this.treeViewWorkGroup.DragEnter += this.TreeViewWorkGroup_DragEnter;
			this.treeViewWorkGroup.DragOver += this.TreeViewWorkGroup_DragOver;
			this.contextMenuWorkGroup.MenuItems.AddRange(new MenuItem[]
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
			this.menuItemWorkGroupRename.Click += this.menuItemWorkGroupRename_Click;
			this.menuItemWorkGroupNew.Index = 1;
			this.menuItemWorkGroupNew.Shortcut = Shortcut.Ins;
			this.menuItemWorkGroupNew.Text = "자식 작업 그룹 추가";
			this.menuItemWorkGroupNew.Click += this.menuItemWorkGroupNew_Click;
			this.menuItemWorkGroupDelete.Index = 2;
			this.menuItemWorkGroupDelete.Shortcut = Shortcut.Del;
			this.menuItemWorkGroupDelete.Text = "작업 그룹 삭제";
			this.menuItemWorkGroupDelete.Click += this.menuItemWorkGroupDelete_Click;
			this.menuItem1.Index = 3;
			this.menuItem1.Text = "-";
			this.menuItemWorkGroupMoveUp.Index = 4;
			this.menuItemWorkGroupMoveUp.Shortcut = Shortcut.CtrlA;
			this.menuItemWorkGroupMoveUp.Text = "작업 그룹 위로 이동";
			this.menuItemWorkGroupMoveUp.Click += this.menuItemWorkGroupMoveUp_Click;
			this.menuItemWorkGroupMoveDown.Index = 5;
			this.menuItemWorkGroupMoveDown.Shortcut = Shortcut.CtrlZ;
			this.menuItemWorkGroupMoveDown.Text = "작업 그룹 아래로 이동";
			this.menuItemWorkGroupMoveDown.Click += this.menuItemWorkGroupMoveDown_Click;
			this.menuItem2.Index = 6;
			this.menuItem2.Text = "-";
			this.menuItemWorkGroupNewRoot.Index = 7;
			this.menuItemWorkGroupNewRoot.Shortcut = Shortcut.CtrlIns;
			this.menuItemWorkGroupNewRoot.Text = "새 루트 작업 그룹 추가";
			this.menuItemWorkGroupNewRoot.Click += this.menuItemWorkGroupNewRoot_Click;
			this.listViewBound.AllowDrop = true;
			this.listViewBound.Anchor = (AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right);
			this.listViewBound.Columns.AddRange(new ColumnHeader[]
			{
				this.columnHeader1,
				this.columnHeader2
			});
			this.listViewBound.ContextMenu = this.contextMenuProcess;
			this.listViewBound.FullRowSelect = true;
			this.listViewBound.HideSelection = false;
			this.listViewBound.Location = new Point(0, 32);
			this.listViewBound.Name = "listViewBound";
			this.listViewBound.Size = new Size(384, 136);
			this.listViewBound.TabIndex = 1;
			this.toolTipWorkGroupControl.SetToolTip(this.listViewBound, "현재 선택된 작업그룹에 속한 프로그램의 리스트입니다.");
			this.listViewBound.UseCompatibleStateImageBehavior = false;
			this.listViewBound.View = View.Details;
			this.listViewBound.DragDrop += this.ListViewBoundProcess_DragDrop;
			this.listViewBound.DragEnter += this.ListViewBoundProcess_DragEnter;
			this.listViewBound.ItemDrag += this.ListViewBoundProcess_ItemDrag;
			this.columnHeader1.Text = "컴퓨터 이름";
			this.columnHeader1.Width = 180;
			this.columnHeader2.Text = "프로그램 이름";
			this.columnHeader2.Width = 180;
			this.contextMenuProcess.MenuItems.AddRange(new MenuItem[]
			{
				this.menuItemClientProcessAdd,
				this.menuItemClientProcessRemove
			});
			this.menuItemClientProcessAdd.Index = 0;
			this.menuItemClientProcessAdd.Shortcut = Shortcut.Ins;
			this.menuItemClientProcessAdd.Text = "새 프로그램 추가";
			this.menuItemClientProcessAdd.Click += this.menuItemClientProcessAdd_Click;
			this.menuItemClientProcessRemove.Index = 1;
			this.menuItemClientProcessRemove.Shortcut = Shortcut.Del;
			this.menuItemClientProcessRemove.Text = "연결된 프로그램 삭제";
			this.menuItemClientProcessRemove.Click += this.menuItemClientProcessRemove_Click;
			this.labelWorkGroup.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
			this.labelWorkGroup.Location = new Point(8, 8);
			this.labelWorkGroup.Name = "labelWorkGroup";
			this.labelWorkGroup.Size = new Size(192, 24);
			this.labelWorkGroup.TabIndex = 2;
			this.labelWorkGroup.Text = "작업 그룹의 구조";
			this.labelWorkGroup.TextAlign = ContentAlignment.MiddleLeft;
			this.labelClientProcess.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
			this.labelClientProcess.Location = new Point(0, 8);
			this.labelClientProcess.Name = "labelClientProcess";
			this.labelClientProcess.Size = new Size(384, 24);
			this.labelClientProcess.TabIndex = 2;
			this.labelClientProcess.Text = "작업 그룹에 연결된 프로그램 리스트";
			this.labelClientProcess.TextAlign = ContentAlignment.MiddleLeft;
			this.listViewUnbound.AllowDrop = true;
			this.listViewUnbound.Anchor = (AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right);
			this.listViewUnbound.Columns.AddRange(new ColumnHeader[]
			{
				this.columnHeader3,
				this.columnHeader4
			});
			this.listViewUnbound.FullRowSelect = true;
			this.listViewUnbound.HideSelection = false;
			this.listViewUnbound.Location = new Point(0, 24);
			this.listViewUnbound.Name = "listViewUnbound";
			this.listViewUnbound.Size = new Size(384, 192);
			this.listViewUnbound.TabIndex = 1;
			this.toolTipWorkGroupControl.SetToolTip(this.listViewUnbound, "현재 접속되어 있는 원격 제어 서버의 프로그램 중, 어떤 작업그룹에도 속하지 않은 프로그램의 리스트입니다. 드래그하여 작업그룹으로 넣을 수 있습니다.");
			this.listViewUnbound.UseCompatibleStateImageBehavior = false;
			this.listViewUnbound.View = View.Details;
			this.listViewUnbound.DragDrop += this.ListViewUnboundProcess_DragDrop;
			this.listViewUnbound.ColumnClick += this.listViewUnbound_ColumnClick;
			this.listViewUnbound.DragEnter += this.ListViewUnboundProcess_DragEnter;
			this.listViewUnbound.ItemDrag += this.ListViewUnboundProcess_ItemDrag;
			this.columnHeader3.Text = "컴퓨터 이름";
			this.columnHeader3.Width = 180;
			this.columnHeader4.Text = "프로그램 이름";
			this.columnHeader4.Width = 180;
			this.labelUnboundProcess.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
			this.labelUnboundProcess.Location = new Point(0, 0);
			this.labelUnboundProcess.Name = "labelUnboundProcess";
			this.labelUnboundProcess.Size = new Size(384, 24);
			this.labelUnboundProcess.TabIndex = 2;
			this.labelUnboundProcess.Text = "어디에도 연결되지 않은 프로그램 리스트";
			this.labelUnboundProcess.TextAlign = ContentAlignment.MiddleLeft;
			this.panelLeft.Controls.Add(this.treeViewWorkGroup);
			this.panelLeft.Controls.Add(this.labelWorkGroup);
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
			this.panelRightUp.Controls.Add(this.labelClientProcess);
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
			this.panelRightDown.Controls.Add(this.labelUnboundProcess);
			this.panelRightDown.Controls.Add(this.listViewUnbound);
			this.panelRightDown.Dock = DockStyle.Fill;
			this.panelRightDown.Location = new Point(208, 176);
			this.panelRightDown.Name = "panelRightDown";
			this.panelRightDown.Size = new Size(392, 224);
			this.panelRightDown.TabIndex = 7;
			base.Controls.Add(this.panelRightDown);
			base.Controls.Add(this.splitterH);
			base.Controls.Add(this.panelRightUp);
			base.Controls.Add(this.splitterV);
			base.Controls.Add(this.panelLeft);
			base.Name = "WorkGroupControl";
			base.Size = new Size(600, 400);
			base.Load += this.WorkGroupControl_Load;
			this.panelLeft.ResumeLayout(false);
			this.panelRightUp.ResumeLayout(false);
			this.panelRightDown.ResumeLayout(false);
			base.ResumeLayout(false);
		}

		public bool Modified
		{
			get
			{
				return this._modified;
			}
		}

		public string SerializedText
		{
			get
			{
				WorkGroupStructureNode[] array = new WorkGroupStructureNode[this.treeViewWorkGroup.Nodes.Count];
				for (int i = 0; i < array.Length; i++)
				{
					array[i] = (WorkGroupControl.SimpleWorkGroupTreeNode)this.treeViewWorkGroup.Nodes[i];
				}
				XmlSerializationWorkGroups xmlSerializationWorkGroups = array;
				return xmlSerializationWorkGroups.GetString();
			}
		}

		public void AddWorkGroup(IWorkGroupStructureNode[] workGroupRootNodes)
		{
			if (workGroupRootNodes != null)
			{
				foreach (WorkGroupStructureNode node in workGroupRootNodes)
				{
					this.AddTo(this.treeViewWorkGroup.Nodes, node);
				}
				this.treeViewWorkGroup.ExpandAll();
			}
		}

		private void AddTo(TreeNodeCollection nodeList, WorkGroupStructureNode node)
		{
			WorkGroupControl.SimpleWorkGroupTreeNode simpleWorkGroupTreeNode = new WorkGroupControl.SimpleWorkGroupTreeNode(node.Name, node.Authority);
			nodeList.Add(simpleWorkGroupTreeNode);
			if (node.ChildNodes != null)
			{
				foreach (WorkGroupStructureNode node2 in node.ChildNodes)
				{
					this.AddTo(simpleWorkGroupTreeNode.Nodes, node2);
				}
			}
			if (node.Childs != null)
			{
				foreach (WorkGroupCondition condition in node.Childs)
				{
					simpleWorkGroupTreeNode.AddCondition(condition);
					this.IncreaseConditionCountWorkGroup(condition);
				}
			}
		}

		public void AddClients(ICollection clientList)
		{
			foreach (object obj in clientList)
			{
				RCClient client = (RCClient)obj;
				this.AddClient(client);
			}
		}

		public void AddClient(RCClient client)
		{
			foreach (RCProcess rcprocess in client.ProcessList)
			{
				this.IncreaseConditionCountItem(new WorkGroupCondition(client.Name, rcprocess.Name));
			}
		}

		private void AddUnbound(WorkGroupCondition condition)
		{
			int i = 0;
			int num = this.listViewUnbound.Items.Count;
			while (i < num)
			{
				int num2 = (i + num) / 2;
				WorkGroupControl.ConditionItem conditionItem = (WorkGroupControl.ConditionItem)this.listViewUnbound.Items[num2];
				int num3 = condition.CompareTo(conditionItem.Condition);
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
			this.listViewUnbound.Items.Insert(i, new WorkGroupControl.ConditionItem(condition));
		}

		private void RemoveUnbound(WorkGroupCondition condition)
		{
			int i = 0;
			int num = this.listViewUnbound.Items.Count;
			while (i < num)
			{
				int num2 = (i + num) / 2;
				WorkGroupControl.ConditionItem conditionItem = (WorkGroupControl.ConditionItem)this.listViewUnbound.Items[num2];
				int num3 = condition.CompareTo(conditionItem.Condition);
				if (num3 < 0)
				{
					num = num2;
				}
				else
				{
					if (num3 <= 0)
					{
						this.listViewUnbound.Items.RemoveAt(num2);
						return;
					}
					i = num2 + 1;
				}
			}
		}

		private void IncreaseConditionCountWorkGroup(WorkGroupCondition condition)
		{
			WorkGroupControl.ConditionCount conditionCount = this._conditionCountList[condition] as WorkGroupControl.ConditionCount;
			if (conditionCount == null)
			{
				conditionCount = new WorkGroupControl.ConditionCount();
				this._conditionCountList.Add(condition, conditionCount);
			}
			conditionCount.WorkGroupCount++;
			if (conditionCount.WorkGroupCount == 1)
			{
				this.RemoveUnbound(condition);
			}
		}

		private void DecreaseConditionCountWorkGroup(WorkGroupCondition condition)
		{
			WorkGroupControl.ConditionCount conditionCount = this._conditionCountList[condition] as WorkGroupControl.ConditionCount;
			conditionCount.WorkGroupCount--;
			if (conditionCount.WorkGroupCount == 0)
			{
				this.AddUnbound(condition);
			}
		}

		private void IncreaseConditionCountItem(WorkGroupCondition condition)
		{
			if (!(this._conditionCountList[condition] is WorkGroupControl.ConditionCount))
			{
				this._conditionCountList.Add(condition, new WorkGroupControl.ConditionCount());
				this.AddUnbound(condition);
			}
		}

		private void RemoveCondition(WorkGroupControl.SimpleWorkGroupTreeNode node)
		{
			foreach (object obj in node.Nodes)
			{
				WorkGroupControl.SimpleWorkGroupTreeNode node2 = (WorkGroupControl.SimpleWorkGroupTreeNode)obj;
				this.RemoveCondition(node2);
			}
			foreach (object obj2 in node.Conditions)
			{
				WorkGroupCondition condition = (WorkGroupCondition)obj2;
				this.DecreaseConditionCountWorkGroup(condition);
			}
			node.ClearCondition();
		}

		private void RemoveSpecifiedCondition(WorkGroupControl.SimpleWorkGroupTreeNode node, WorkGroupCondition targetCondition)
		{
			foreach (object obj in node.Nodes)
			{
				WorkGroupControl.SimpleWorkGroupTreeNode node2 = (WorkGroupControl.SimpleWorkGroupTreeNode)obj;
				this.RemoveSpecifiedCondition(node2, targetCondition);
			}
			if (node.RemoveCondition(targetCondition))
			{
				this.DecreaseConditionCountWorkGroup(targetCondition);
			}
		}

		private void RemoveWorkGroup(WorkGroupControl.SimpleWorkGroupTreeNode node)
		{
			node.Remove();
			this.RemoveCondition(node);
		}

		private void TreeViewWorkGroup_MouseDown(object sender, MouseEventArgs args)
		{
			this._selectedNode = this.treeViewWorkGroup.GetNodeAt(args.X, args.Y);
		}

		private void TreeViewWorkGroup_AfterSelect(object sender, TreeViewEventArgs args)
		{
			this.listViewBound.Items.Clear();
			if (args.Node != null)
			{
				this._selectedNode = args.Node;
				foreach (object obj in ((WorkGroupControl.SimpleWorkGroupTreeNode)this._selectedNode).Conditions)
				{
					WorkGroupCondition condition = (WorkGroupCondition)obj;
					this.listViewBound.Items.Add(new WorkGroupControl.ConditionItem(condition));
				}
			}
		}

		private void TreeViewWorkGroup_AfterLabelEdit(object sender, NodeLabelEditEventArgs args)
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
				treeNode = this.treeViewWorkGroup.Nodes[0];
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
			this._modified = true;
			args.Node.EndEdit(false);
		}

		private void menuItemWorkGroupNew_Click(object sender, EventArgs e)
		{
			if (this._selectedNode != null)
			{
				this.treeViewWorkGroup.SelectedNode = this._selectedNode;
				if (!this._selectedNode.IsEditing)
				{
					if (((WorkGroupControl.SimpleWorkGroupTreeNode)this._selectedNode).Conditions.Count > 0)
					{
						if (!Utility.InputYesNoFromWarning(LocalizeText.Get(380)))
						{
							return;
						}
						foreach (object obj in ((WorkGroupControl.SimpleWorkGroupTreeNode)this._selectedNode).Conditions)
						{
							WorkGroupCondition condition = (WorkGroupCondition)obj;
							this.DecreaseConditionCountWorkGroup(condition);
						}
						((WorkGroupControl.SimpleWorkGroupTreeNode)this._selectedNode).ClearCondition();
					}
					WorkGroupControl.SimpleWorkGroupTreeNode simpleWorkGroupTreeNode = new WorkGroupControl.SimpleWorkGroupTreeNode("", ((WorkGroupControl.SimpleWorkGroupTreeNode)this._selectedNode).Authority);
					this._selectedNode.Nodes.Add(simpleWorkGroupTreeNode);
					this._selectedNode.Expand();
					this.treeViewWorkGroup.SelectedNode = simpleWorkGroupTreeNode;
					this._modified = true;
					simpleWorkGroupTreeNode.BeginEdit();
				}
			}
		}

		private void menuItemWorkGroupRename_Click(object sender, EventArgs e)
		{
			if (this._selectedNode != null)
			{
				this.treeViewWorkGroup.SelectedNode = this._selectedNode;
				if (!this._selectedNode.IsEditing)
				{
					this._selectedNode.BeginEdit();
				}
			}
		}

		private void menuItemWorkGroupDelete_Click(object sender, EventArgs e)
		{
			if (this._selectedNode != null)
			{
				TreeNode prevVisibleNode = this._selectedNode.PrevVisibleNode;
				this.treeViewWorkGroup.SelectedNode = this._selectedNode;
				this.RemoveWorkGroup((WorkGroupControl.SimpleWorkGroupTreeNode)this._selectedNode);
				if (this.treeViewWorkGroup.SelectedNode == null)
				{
					this.listViewBound.Items.Clear();
				}
				this._modified = true;
				if (prevVisibleNode != null)
				{
					this.treeViewWorkGroup.SelectedNode = prevVisibleNode;
				}
			}
		}

		private void menuItemWorkGroupMoveUp_Click(object sender, EventArgs e)
		{
			if (this._selectedNode != null)
			{
				this.treeViewWorkGroup.SelectedNode = this._selectedNode;
				TreeNodeCollection nodes;
				if (this._selectedNode.Parent == null)
				{
					nodes = this.treeViewWorkGroup.Nodes;
				}
				else
				{
					nodes = this._selectedNode.Parent.Nodes;
				}
				if (nodes[0] != this._selectedNode)
				{
					int index = this._selectedNode.Index;
					TreeNode selectedNode = this._selectedNode;
					this._modified = true;
					selectedNode.Remove();
					nodes.Insert(index - 1, selectedNode);
					this.treeViewWorkGroup.SelectedNode = selectedNode;
				}
			}
		}

		private void menuItemWorkGroupMoveDown_Click(object sender, EventArgs e)
		{
			if (this._selectedNode != null)
			{
				this.treeViewWorkGroup.SelectedNode = this._selectedNode;
				TreeNodeCollection nodes;
				if (this._selectedNode.Parent == null)
				{
					nodes = this.treeViewWorkGroup.Nodes;
				}
				else
				{
					nodes = this._selectedNode.Parent.Nodes;
				}
				if (nodes[nodes.Count - 1] != this._selectedNode)
				{
					int index = this._selectedNode.Index;
					TreeNode selectedNode = this._selectedNode;
					this._modified = true;
					selectedNode.Remove();
					nodes.Insert(index + 1, selectedNode);
					this.treeViewWorkGroup.SelectedNode = selectedNode;
				}
			}
		}

		private void menuItemWorkGroupNewRoot_Click(object sender, EventArgs e)
		{
			WorkGroupControl.SimpleWorkGroupTreeNode simpleWorkGroupTreeNode = new WorkGroupControl.SimpleWorkGroupTreeNode("", Authority.Supervisor);
			this.treeViewWorkGroup.Nodes.Add(simpleWorkGroupTreeNode);
			this.treeViewWorkGroup.SelectedNode = simpleWorkGroupTreeNode;
			this._modified = true;
			simpleWorkGroupTreeNode.BeginEdit();
		}

		private void menuItemClientProcessAdd_Click(object sender, EventArgs e)
		{
			if (this._selectedNode != null)
			{
				if (this._selectedNode.Nodes.Count > 0)
				{
					Utility.ShowErrorMessage(LocalizeText.Get(381));
					return;
				}
				TwoStringInputForm twoStringInputForm = new TwoStringInputForm(LocalizeText.Get(382), LocalizeText.Get(383), LocalizeText.Get(384), LocalizeText.Get(385));
				if (twoStringInputForm.ShowDialog() != DialogResult.OK)
				{
					return;
				}
				WorkGroupCondition condition = new WorkGroupCondition(twoStringInputForm.Value1, twoStringInputForm.Value2);
				try
				{
					((WorkGroupControl.SimpleWorkGroupTreeNode)this._selectedNode).AddCondition(condition);
					this.IncreaseConditionCountWorkGroup(condition);
					this._modified = true;
					this.TreeViewWorkGroup_AfterSelect(sender, new TreeViewEventArgs(this._selectedNode));
				}
				catch (ArgumentException)
				{
					Utility.ShowErrorMessage(LocalizeText.Get(386));
				}
			}
		}

		private void menuItemClientProcessRemove_Click(object sender, EventArgs e)
		{
			if (this._selectedNode != null)
			{
				foreach (object obj in this.listViewBound.SelectedItems)
				{
					WorkGroupControl.ConditionItem conditionItem = (WorkGroupControl.ConditionItem)obj;
					((WorkGroupControl.SimpleWorkGroupTreeNode)this._selectedNode).RemoveCondition(conditionItem.Condition);
					this.DecreaseConditionCountWorkGroup(conditionItem.Condition);
				}
				this._modified = true;
				this.TreeViewWorkGroup_AfterSelect(sender, new TreeViewEventArgs(this._selectedNode));
			}
		}

		private void ListViewBoundProcess_ItemDrag(object sender, ItemDragEventArgs e)
		{
			this._draggingConditionItem = true;
			this._boundSourceNode = this._selectedNode;
			WorkGroupControl.ConditionItem[] array = new WorkGroupControl.ConditionItem[this.listViewBound.SelectedItems.Count];
			this.listViewBound.SelectedItems.CopyTo(array, 0);
			base.DoDragDrop(array, DragDropEffects.Copy | DragDropEffects.Move);
		}

		private void ListViewBoundProcess_DragEnter(object sender, DragEventArgs args)
		{
			this._validDragToBoundList = (args.Data.GetData(typeof(WorkGroupControl.ConditionItem[])) != null);
			if (this._validDragToBoundList && this.treeViewWorkGroup.SelectedNode != null && this.treeViewWorkGroup.SelectedNode.Nodes.Count == 0)
			{
				args.Effect = DragDropEffects.Move;
			}
		}

		private void ListViewBoundProcess_DragDrop(object sender, DragEventArgs args)
		{
			if (this._validDragToBoundList)
			{
				if (this.treeViewWorkGroup.SelectedNode == null)
				{
					Utility.ShowErrorMessage(LocalizeText.Get(387));
					return;
				}
				TreeNode selectedNode = this.treeViewWorkGroup.SelectedNode;
				if (selectedNode.Nodes.Count > 0)
				{
					Utility.ShowErrorMessage(LocalizeText.Get(388));
					return;
				}
				WorkGroupControl.ConditionItem[] array = (WorkGroupControl.ConditionItem[])args.Data.GetData(typeof(WorkGroupControl.ConditionItem[]));
				if (array == null)
				{
					return;
				}
				foreach (WorkGroupControl.ConditionItem conditionItem in array)
				{
					try
					{
						((WorkGroupControl.SimpleWorkGroupTreeNode)selectedNode).AddCondition(conditionItem.Condition);
						this.IncreaseConditionCountWorkGroup(conditionItem.Condition);
					}
					catch (ArgumentException)
					{
					}
				}
				this._modified = true;
				this.TreeViewWorkGroup_AfterSelect(sender, new TreeViewEventArgs(selectedNode));
			}
		}

		private void ListViewUnboundProcess_ItemDrag(object sender, ItemDragEventArgs e)
		{
			this._draggingConditionItem = false;
			WorkGroupControl.ConditionItem[] array = new WorkGroupControl.ConditionItem[this.listViewUnbound.SelectedItems.Count];
			this.listViewUnbound.SelectedItems.CopyTo(array, 0);
			base.DoDragDrop(array, DragDropEffects.Move);
		}

		private void ListViewUnboundProcess_DragEnter(object sender, DragEventArgs args)
		{
			this._validDragToUnboundList = (args.Data.GetData(typeof(WorkGroupControl.ConditionItem[])) != null);
			if (this._validDragToUnboundList)
			{
				args.Effect = DragDropEffects.Move;
			}
		}

		private void ListViewUnboundProcess_DragDrop(object sender, DragEventArgs args)
		{
			if (this._validDragToUnboundList)
			{
				WorkGroupControl.ConditionItem[] array = (WorkGroupControl.ConditionItem[])args.Data.GetData(typeof(WorkGroupControl.ConditionItem[]));
				if (array == null)
				{
					return;
				}
				if (Utility.InputYesNoFromWarning(LocalizeText.Get(389)))
				{
					foreach (WorkGroupControl.ConditionItem conditionItem in array)
					{
						foreach (object obj in this.treeViewWorkGroup.Nodes)
						{
							WorkGroupControl.SimpleWorkGroupTreeNode node = (WorkGroupControl.SimpleWorkGroupTreeNode)obj;
							this.RemoveSpecifiedCondition(node, conditionItem.Condition);
						}
					}
				}
				this._modified = true;
				TreeNode selectedNode = this.treeViewWorkGroup.SelectedNode;
				if (selectedNode != null)
				{
					this.TreeViewWorkGroup_AfterSelect(sender, new TreeViewEventArgs(selectedNode));
				}
			}
		}

		private void TreeViewWorkGroup_DragEnter(object sender, DragEventArgs args)
		{
			this._validDragToWorkGroupTree = (args.Data.GetData(typeof(WorkGroupControl.ConditionItem[])) != null);
		}

		private void TreeViewWorkGroup_DragOver(object sender, DragEventArgs args)
		{
			if (this._validDragToWorkGroupTree)
			{
				Point point = this.treeViewWorkGroup.PointToClient(new Point(args.X, args.Y));
				TreeNode nodeAt = this.treeViewWorkGroup.GetNodeAt(point.X, point.Y);
				if (nodeAt != null)
				{
					this.treeViewWorkGroup.SelectedNode = nodeAt;
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

		private void TreeViewWorkGroup_DragDrop(object sender, DragEventArgs args)
		{
			if (this._validDragToWorkGroupTree)
			{
				this._validDragToBoundList = true;
				if (this._draggingConditionItem && this._selectedNode == this._boundSourceNode)
				{
					return;
				}
				this.ListViewBoundProcess_DragDrop(sender, args);
				WorkGroupControl.ConditionItem[] array = (WorkGroupControl.ConditionItem[])args.Data.GetData(typeof(WorkGroupControl.ConditionItem[]));
				if (array == null)
				{
					return;
				}
				if (this._draggingConditionItem && (args.KeyState & 8) == 0)
				{
					foreach (WorkGroupControl.ConditionItem conditionItem in array)
					{
						((WorkGroupControl.SimpleWorkGroupTreeNode)this._boundSourceNode).RemoveCondition(conditionItem.Condition);
						this.DecreaseConditionCountWorkGroup(conditionItem.Condition);
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

		private void WorkGroupControl_Load(object sender, EventArgs e)
		{
			this.toolTipWorkGroupControl.SetToolTip(this.treeViewWorkGroup, LocalizeText.Get(360));
			this.menuItemWorkGroupRename.Text = LocalizeText.Get(361);
			this.menuItemWorkGroupNew.Text = LocalizeText.Get(362);
			this.menuItemWorkGroupDelete.Text = LocalizeText.Get(363);
			this.menuItemWorkGroupMoveUp.Text = LocalizeText.Get(364);
			this.menuItemWorkGroupMoveDown.Text = LocalizeText.Get(365);
			this.menuItemWorkGroupNewRoot.Text = LocalizeText.Get(366);
			this.toolTipWorkGroupControl.SetToolTip(this.listViewBound, LocalizeText.Get(367));
			this.columnHeader1.Text = LocalizeText.Get(368);
			this.columnHeader2.Text = LocalizeText.Get(369);
			this.menuItemClientProcessAdd.Text = LocalizeText.Get(370);
			this.menuItemClientProcessRemove.Text = LocalizeText.Get(371);
			this.labelWorkGroup.Text = LocalizeText.Get(372);
			this.labelClientProcess.Text = LocalizeText.Get(373);
			this.toolTipWorkGroupControl.SetToolTip(this.listViewUnbound, LocalizeText.Get(374));
			this.columnHeader3.Text = LocalizeText.Get(375);
			this.columnHeader4.Text = LocalizeText.Get(376);
			this.labelUnboundProcess.Text = LocalizeText.Get(377);
		}

		private ColumnHeader columnHeader1;

		private ColumnHeader columnHeader2;

		private TreeView treeViewWorkGroup;

		private Label labelWorkGroup;

		private ColumnHeader columnHeader3;

		private ColumnHeader columnHeader4;

		private Panel panelLeft;

		private ContextMenu contextMenuWorkGroup;

		private ListView listViewBound;

		private Label labelClientProcess;

		private MenuItem menuItemWorkGroupNew;

		private MenuItem menuItemWorkGroupDelete;

		private ContextMenu contextMenuProcess;

		private MenuItem menuItemClientProcessAdd;

		private MenuItem menuItemClientProcessRemove;

		private ListView listViewUnbound;

		private Label labelUnboundProcess;

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

		private ToolTip toolTipWorkGroupControl;

		private IContainer components;

		private TreeNode _selectedNode;

		private SortedList _conditionCountList;

		private bool _draggingConditionItem;

		private TreeNode _boundSourceNode;

		private bool _validDragToBoundList;

		private bool _validDragToUnboundList;

		private bool _validDragToWorkGroupTree;

		private bool _modified;

		private class ConditionCount
		{
			public ConditionCount()
			{
				this.WorkGroupCount = 0;
			}

			public int WorkGroupCount;
		}

		private class ConditionItem : ListViewItem
		{
			public WorkGroupCondition Condition { get; private set; }

			public ConditionItem(WorkGroupCondition condition) : base(new string[]
			{
				condition.ClientName,
				condition.ProcessName
			})
			{
				this.Condition = condition;
			}
		}

		private class SimpleWorkGroupTreeNode : TreeNode
		{
			public ICollection Conditions
			{
				get
				{
					return this._conditions.Keys;
				}
			}

			public Authority Authority { get; private set; }

			public void AddCondition(WorkGroupCondition condition)
			{
				this._conditions.Add(condition, null);
			}

			public bool RemoveCondition(WorkGroupCondition condition)
			{
				if (this._conditions.ContainsKey(condition))
				{
					this._conditions.Remove(condition);
					return true;
				}
				return false;
			}

			public void ClearCondition()
			{
				this._conditions.Clear();
			}

			public SimpleWorkGroupTreeNode(string text, Authority authority) : base(text)
			{
				this._conditions = new SortedList();
				this.Authority = authority;
			}

			public static implicit operator WorkGroupStructureNode(WorkGroupControl.SimpleWorkGroupTreeNode node)
			{
				WorkGroupStructureNode[] array = new WorkGroupStructureNode[node.Nodes.Count];
				for (int i = 0; i < array.Length; i++)
				{
					array[i] = (WorkGroupControl.SimpleWorkGroupTreeNode)node.Nodes[i];
				}
				WorkGroupCondition[] array2 = new WorkGroupCondition[node.Conditions.Count];
				node.Conditions.CopyTo(array2, 0);
				return new WorkGroupStructureNode(node.Text, node.Authority, array, array2);
			}

			private SortedList _conditions;
		}
	}
}
