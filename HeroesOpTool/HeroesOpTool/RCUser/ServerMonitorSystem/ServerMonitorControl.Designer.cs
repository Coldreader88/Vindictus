using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using Devcat.Core;
using Devcat.Core.Net.Message;
using HeroesOpTool.Properties;
using HeroesOpTool.RCUser.ServerMonitorSystem.SystemManage;
using HeroesOpTool.UserMonitorSystem;
using RemoteControlSystem;
using RemoteControlSystem.ClientMessage;
using RemoteControlSystem.ControlMessage;
using RemoteControlSystem.ServerMessage;

namespace HeroesOpTool.RCUser.ServerMonitorSystem
{
	public class ServerMonitorControl : UserControl
	{
		public event EventHandler OnSplitGeneralLog;

		public event EventHandler<EventArgs<LogGenerator>> OnSplitProcessLog;

		public event EventHandler<EventArgs<LogGenerator>> OnSplitAllProcessLog;

		public event RCClient.ProcessEventHandler OnProcessCrash;

		public event EventHandler<EventArgs<ChildProcessLogListReplyMessage>> OnChildProcessList;

		public event EventHandler<EventArgs<ExeInfoReplyMessage>> OnExeInfo;

		public LogGenerator LogGenerator { get; private set; }

		public string GeneralLogs
		{
			get
			{
				return this.TBoxLogGeneral.Text;
			}
		}

		public UserAlarmForm alarmForm { get; private set; }

		public ServerMonitorControl(RCUserHandler rcUser)
		{
			this.InitializeComponent();
			this.GroupClientList.Text = LocalizeText.Get(189);
			this.ToolTipServerControl.SetToolTip(this.GroupClientList, LocalizeText.Get(190));
			this.ToolTipServerControl.SetToolTip(this.LViewClientList, LocalizeText.Get(191));
			this.ColumnProcessName.Text = LocalizeText.Get(192);
			this.ColumnClientName.Text = LocalizeText.Get(193);
			this.ColumnProcessState.Text = LocalizeText.Get(194);
			this.ColumnPerformance.Text = LocalizeText.Get(534);
			this.ColumnServerIP.Text = LocalizeText.Get(195);
			this.tabPageWorkGroup.Text = LocalizeText.Get(196);
			this.ToolTipServerControl.SetToolTip(this.tabPageWorkGroup, LocalizeText.Get(197));
			this.ToolTipServerControl.SetToolTip(this.TViewWorkGroup, LocalizeText.Get(198));
			this.menuItemSelectOnly.Text = LocalizeText.Get(199);
			this.menuItemSelectSameName.Text = LocalizeText.Get(200);
			this.menuItemSelectAll.Text = LocalizeText.Get(201);
			this.menuItemDeselectAll.Text = LocalizeText.Get(202);
			this.TabPageLogGeneral.Text = LocalizeText.Get(203);
			this.ToolTipServerControl.SetToolTip(this.TBoxLogGeneral, LocalizeText.Get(204));
			this.TabPageLogProcess.Text = LocalizeText.Get(205);
			this.ToolTipServerControl.SetToolTip(this.TBoxLogProcess, LocalizeText.Get(206));
			this.ToolTipServerControl.SetToolTip(this.TBoxLogProcess, LocalizeText.Get(206));
			this.ToolTipServerControl.SetToolTip(this.TStrpControl, LocalizeText.Get(207));
			this.TStrpBtnStart.Text = LocalizeText.Get(208);
			this.TStrpBtnStart.ToolTipText = LocalizeText.Get(209);
			this.TStrpBtnCommand.ToolTipText = LocalizeText.Get(211);
			this.TStrpBtnUpdate.Text = LocalizeText.Get(212);
			this.TStrpBtnUpdate.ToolTipText = LocalizeText.Get(213);
			this.TStrpBtnStop.Text = LocalizeText.Get(214);
			this.TStrpBtnStop.ToolTipText = LocalizeText.Get(215);
			this.TStrpBtnControl.Text = LocalizeText.Get(216);
			this.TStrpBtnSvrCommand.Text = LocalizeText.Get(512);
			this.TStrpBtnCommand.Text = LocalizeText.Get(513);
			this.tabPageServerGroup.Text = LocalizeText.Get(514);
			this.TSBGeneralLog.Text = LocalizeText.Get(524);
			this.TSBProcessLog.Text = LocalizeText.Get(524);
			this.TSBProcessLog.ToolTipText = LocalizeText.Get(525);
			this.TSBAllProcessLog.Text = LocalizeText.Get(526);
			this.TSBAllProcessLog.ToolTipText = LocalizeText.Get(527);
			this.menuItemSort.Text = LocalizeText.Get(528);
			this.TSMISelectAll.Text = LocalizeText.Get(529);
			this.TSMISplitLog.Text = LocalizeText.Get(530);
			this.TSMIChildProcess.Text = LocalizeText.Get(531);
			this.TSMIExeInfo.Text = LocalizeText.Get(532);
			this.Dock = DockStyle.Fill;
			this.client = rcUser;
			this.client.ClientAdd += this.RC_ClientAdd;
			this.client.ClientRemove += this.RC_ClientRemove;
			this.client.WorkGroupStructureChange += this.RC_WorkGroupStructureChange;
			this.client.ServerGroupStructureChange += this.RC_ServerGroupStructureChange;
			this.client.ControlReply += this.RC_ControlReply;
			this.client.Notify += this.RC_Notify;
			this.client.ChildProcessListed += this.RC_ChildProcessListed;
			this.client.ExeInfo += this.RC_ExeInfo;
			this.client.EmergencyCallInfo += this.RC_EmergenyCallInfo;
			this.workGroup = new WorkGroup<WorkGroupCondition>(this.TViewWorkGroup);
			this.serverGroup = new WorkGroup<ServerGroupCondition>(this.TViewServerGroup);
			WorkGroup.Sort.Current = this.workGroup;
			WorkGroup.Sort.SortType = SortType.Tree;
			WorkGroup.Sort.ShowListView = this.LViewClientList;
			this.LViewClientList.ListViewItemSorter = this.workGroup.Comparer;
			this.LViewClientList.Sorting = SortOrder.Ascending;
			this.processNameComparer = new Utility.ListViewItemComparer(1);
			this.serverNameComparer = new Utility.ListViewItemComparer(2);
			this.stateComparer = new Utility.ListViewItemImageIndexComparer();
			this.performaceComparer = new Utility.ListViewItemComparer(4);
			this.clientIPComparer = new Utility.ListViewItemIPComparer(5);
			this.LogGenerator = new LogGenerator("전체 로그", "전체 로그");
			this.processLogGenerators = new ProcessLogGeneratorCollection();
			this.TBoxLogProcess.DisabledText = LocalizeText.Get(228);
			this.TBoxLogProcess.Enabled = false;
			this.alarmForm = new UserAlarmForm();
			this.alarmForm.OnClose += this.UpdateAlarmTime;
		}

		private void ChangeSort(SortType type, WorkGroup workGroup)
		{
			if (WorkGroup.Sort.SortType == type && WorkGroup.Sort.Current == workGroup)
			{
				Utility.ILViewComparer ilviewComparer = this.LViewClientList.ListViewItemSorter as Utility.ILViewComparer;
				if (ilviewComparer != null)
				{
					ilviewComparer.IsAscending = !ilviewComparer.IsAscending;
					this.LViewClientList.Sort();
					return;
				}
			}
			else
			{
				WorkGroup.Sort.SortType = type;
				WorkGroup.Sort.Current = workGroup;
				IComparer listViewItemSorter = null;
				switch (type)
				{
				case SortType.Tree:
					listViewItemSorter = workGroup.Comparer;
					break;
				case SortType.State:
					listViewItemSorter = this.stateComparer;
					break;
				case SortType.ProcessName:
					listViewItemSorter = this.processNameComparer;
					break;
				case SortType.ServerName:
					listViewItemSorter = this.serverNameComparer;
					break;
				case SortType.Performance:
					listViewItemSorter = this.performaceComparer;
					break;
				case SortType.ClientIP:
					listViewItemSorter = this.clientIPComparer;
					break;
				}
				this.LViewClientList.ListViewItemSorter = listViewItemSorter;
				this.LViewClientList.Sort();
			}
		}

		private void GeneralLog(string message, params object[] args)
		{
			this.GeneralLog(DateTime.Now, message, args);
		}

		private void GeneralLog(DateTime time, string message, params object[] args)
		{
			if (args.Length > 0)
			{
				message = string.Format(message, args);
			}
			if (this.TBoxLogGeneral.Text.Length > 30000)
			{
				int num = this.TBoxLogGeneral.Text.IndexOf("\r\n", 15000);
				this.TBoxLogGeneral.Text = this.TBoxLogGeneral.Text.Substring(num + 2);
			}
			string text = time.ToString("MM-dd HH:mm:ss") + " > " + message + "\r\n";
			this.TBoxLogGeneral.AppendText(text);
			this.LogGenerator.LogGenerated(null, text);
		}

		private void AllNameChange(WorkGroupTreeNode node, string text, CheckState newState)
		{
			if (node.Title == text)
			{
				node.CheckState = newState;
			}
			foreach (object obj in node.Nodes)
			{
				WorkGroupTreeNode node2 = (WorkGroupTreeNode)obj;
				this.AllNameChange(node2, text, newState);
			}
		}

		private void AllNameChange(WorkGroupTreeNode node, TreeNodeCollection topNode)
		{
			foreach (object obj in topNode)
			{
				WorkGroupTreeNode node2 = (WorkGroupTreeNode)obj;
				this.AllNameChange(node2, node.Title, (node.CheckState == CheckState.Unchecked) ? CheckState.Checked : CheckState.Unchecked);
			}
		}

		private void SelectAll(TreeNodeCollection topNode)
		{
			foreach (object obj in topNode)
			{
				WorkGroupTreeNode workGroupTreeNode = (WorkGroupTreeNode)obj;
				workGroupTreeNode.CheckState = CheckState.Checked;
			}
		}

		private void DeselectAll(TreeNodeCollection topNode)
		{
			foreach (object obj in topNode)
			{
				WorkGroupTreeNode workGroupTreeNode = (WorkGroupTreeNode)obj;
				workGroupTreeNode.CheckState = CheckState.Unchecked;
			}
		}

		public void EnableConnectionEvents()
		{
			this.client.ConnectionResulted += this.RC_Connected;
			this.client.Closed += this.RC_Closed;
		}

		private void RC_ClientAdd(object sender, EventArgs<RCClient> args)
		{
			this.UIThread(delegate
			{
				RCClient value = args.Value;
				value.ProcessAdd += this.Client_ProcessAdd;
				value.ProcessModify += this.Client_ProcessModify;
				value.ProcessRemove += this.Client_ProcessRemove;
				value.ProcessLog += this.Client_ProcessLog;
				value.ProcessStateChange += this.Client_ProcessStateChange;
				value.ProcessPerformanceUpdate += this.Client_ProcessPerformanceUpdate;
				this.workGroup.AddToWorkGroup(value);
				this.serverGroup.AddToWorkGroup(value);
				foreach (object obj in this.TViewWorkGroup.Nodes)
				{
					WorkGroupTreeNode workGroupTreeNode = (WorkGroupTreeNode)obj;
					workGroupTreeNode.RecalculateItemCountFromRoot();
				}
				foreach (object obj2 in this.TViewServerGroup.Nodes)
				{
					WorkGroupTreeNode workGroupTreeNode2 = (WorkGroupTreeNode)obj2;
					workGroupTreeNode2.RecalculateItemCountFromRoot();
				}
				this.GeneralLog(LocalizeText.Get(218), new object[]
				{
					value.Name
				});
			});
		}

		private void RC_ClientRemove(object sender, EventArgs<RCClient> args)
		{
			this.UIThread(delegate
			{
				this.workGroup.RemoveFromWorkGroup(args.Value);
				this.serverGroup.RemoveFromWorkGroup(args.Value);
				foreach (object obj in this.TViewWorkGroup.Nodes)
				{
					WorkGroupTreeNode workGroupTreeNode = (WorkGroupTreeNode)obj;
					workGroupTreeNode.RecalculateItemCountFromRoot();
				}
				foreach (object obj2 in this.TViewServerGroup.Nodes)
				{
					WorkGroupTreeNode workGroupTreeNode2 = (WorkGroupTreeNode)obj2;
					workGroupTreeNode2.RecalculateItemCountFromRoot();
				}
				this.GeneralLog(LocalizeText.Get(219), new object[]
				{
					args.Value.Name
				});
			});
		}

		private void RC_WorkGroupStructureChange(object sender, RCUserHandler.WorkGroupStructureEventArgs args)
		{
			this.WorkGroupStructureChanaged(this.workGroup, args.RootNodes);
		}

		private void RC_ServerGroupStructureChange(object sender, RCUserHandler.WorkGroupStructureEventArgs args)
		{
			this.WorkGroupStructureChanaged(this.serverGroup, args.RootNodes);
		}

		private void WorkGroupStructureChanaged(WorkGroup parent, IWorkGroupStructureNode[] rootNodes)
		{
			this.UIThread(delegate
			{
				if (this.client.ClientList.Count > 0)
				{
					Utility.ShowInformationMessage(LocalizeText.Get(222));
					this.GeneralLog(LocalizeText.Get(223), new object[0]);
				}
				parent.Node = rootNodes;
				this.LViewClientList.Items.Clear();
				parent.Clear();
				parent.View.Nodes.Clear();
				if (parent.Node != null)
				{
					foreach (IWorkGroupStructureNode node2 in parent.Node)
					{
						parent.View.Nodes.Add(new WorkGroupTreeNode(node2, parent));
					}
					foreach (RCClient rcclient in this.client.ClientList)
					{
						parent.AddToWorkGroup(rcclient);
					}
					foreach (object obj in parent.View.Nodes)
					{
						WorkGroupTreeNode workGroupTreeNode = (WorkGroupTreeNode)obj;
						workGroupTreeNode.RecalculateItemCountFromRoot();
					}
					parent.View.ExpandAll();
				}
			});
		}

		private void RC_ControlReply(object sender, EventArgs<ControlEnterReply> args)
		{
			this.UIThread(delegate
			{
				if (args.Value.Account == this.client.ID)
				{
					foreach (Action<ControlEnterReply> action in this.OnControlReply)
					{
						action(args.Value);
					}
					this.OnControlReply.Clear();
					return;
				}
				Utility.ShowErrorMessage(string.Format(LocalizeText.Get(224), args.Value.Account));
			});
		}

		private void AcquireControlMutex(Action<ControlEnterReply> callback)
		{
			if (this.client.Authority < Authority.Developer)
			{
				Utility.ShowErrorMessage(LocalizeText.Get(241));
				return;
			}
			if (this.hasControl == null)
			{
				this.OnControlReply.Add(callback);
			}
			else
			{
				this.UIThread(delegate
				{
					callback(this.hasControl);
				});
			}
			if (Interlocked.Increment(ref this.ControlMutex) == 1)
			{
				this.client.SendMessage<ControlEnterMessage>(new ControlEnterMessage());
			}
		}

		private void ReleaseControlMutex()
		{
			if (Interlocked.Decrement(ref this.ControlMutex) == 0)
			{
				this.hasControl = null;
				ControlFinishMessage message = new ControlFinishMessage();
				this.client.SendMessage<ControlFinishMessage>(message);
			}
		}

		private void RC_Notify(object sender, EventArgs<NotifyMessage> args)
		{
			this.UIThread(delegate
			{
				switch (args.Value.MessageType)
				{
				case MessageType.Message:
					this.GeneralLog(args.Value.Time, args.Value.Message, new object[0]);
					return;
				case MessageType.Information:
					Utility.ShowInformationMessage(args.Value.Message);
					return;
				case MessageType.Warning:
					Utility.ShowWarningMessage(args.Value.Message);
					return;
				case MessageType.Error:
					Utility.ShowErrorMessage(args.Value.Message);
					return;
				default:
					return;
				}
			});
		}

		private void RC_Connected(object sender, EventArgs<RCUserHandler.ConnectionResult> args)
		{
			if (MainForm.Instance != null)
			{
				MainForm.Instance.UIThread(delegate
				{
					if (args.Value == RCUserHandler.ConnectionResult.Success)
					{
						Utility.ShowInformationMessage(MainForm.Instance, LocalizeText.Get(225));
						string text = MainForm.Instance.Text;
						if (text.EndsWith(" (Disconnected)"))
						{
							MainForm.Instance.Text = text.Substring(0, text.Length - 15);
						}
					}
				});
			}
			if (args.Value == RCUserHandler.ConnectionResult.Success)
			{
				this.UIThread(delegate
				{
					this.RC_WorkGroupStructureChange(sender, new RCUserHandler.WorkGroupStructureEventArgs(new WorkGroupStructureNode[0]));
					this.RC_ServerGroupStructureChange(sender, new RCUserHandler.WorkGroupStructureEventArgs(new ServerGroupStructureNode[0]));
				});
			}
		}

		private void RC_RCSVersionMismatch(object sender, EventArgs args)
		{
			Utility.ShowErrorMessage(Form.ActiveForm, LocalizeText.Get(226));
			MainForm.Instance.Close();
		}

		private void RC_Closed(object sender, EventArgs args)
		{
			if (!base.InvokeRequired && MainForm.Instance != null)
			{
				MainForm.Instance.UIThread(delegate
				{
					Utility.ShowErrorMessage(MainForm.Instance, LocalizeText.Get(227));
					MainForm instance = MainForm.Instance;
					instance.Text += " (Disconnected)";
				});
			}
		}

		private void RC_ChildProcessListed(object sender, EventArgs<ChildProcessLogListReplyMessage> args)
		{
			if (this.OnChildProcessList != null)
			{
				this.UIThread(delegate
				{
					this.OnChildProcessList(sender, args);
				});
			}
		}

		private void RC_ExeInfo(object sender, EventArgs<ExeInfoReplyMessage> args)
		{
			if (this.OnChildProcessList != null)
			{
				this.UIThread(delegate
				{
					this.OnExeInfo(sender, args);
				});
			}
		}

		private void RC_EmergenyCallInfo(object sender, EventArgs<List<string>> args)
		{
			if (this.alarmForm != null)
			{
				EmergencyInformationData emergencyInformationData = new EmergencyInformationData();
				emergencyInformationData.Department = args.Value[0];
				emergencyInformationData.ID = args.Value[1];
				emergencyInformationData.Name = args.Value[2];
				emergencyInformationData.PhoneNumber = args.Value[3];
				emergencyInformationData.Mail = args.Value[4];
				emergencyInformationData.Rank = args.Value[5];
				this.alarmForm.AddEmergencyCallInfo(emergencyInformationData);
			}
		}

		private void ItemRefresh(RCClient client)
		{
			foreach (RCProcess process in client.ProcessList)
			{
				this.ItemRefresh(client, process);
			}
		}

		private void ItemRefresh(RCClient client, RCProcess process)
		{
			ClientProcessItem showItem = ClientProcessItem.GetShowItem(client, process);
			if (showItem != null)
			{
				showItem.RefreshState();
			}
		}

		private void Client_ProcessAdd(RCClient sender, RCProcess process)
		{
			this.UIThread(delegate
			{
				this.workGroup.AddToWorkGroup(sender, process);
				this.serverGroup.AddToWorkGroup(sender, process);
			});
		}

		private void Client_ProcessModify(RCClient sender, RCProcess process)
		{
			this.UIThread(delegate
			{
				this.ItemRefresh(sender, process);
			});
		}

		private void Client_ProcessRemove(RCClient sender, RCProcess process)
		{
			this.UIThread(delegate
			{
				this.workGroup.RemoveFromWorkGroup(sender, process);
				this.serverGroup.RemoveFromWorkGroup(sender, process);
			});
		}

		private void Client_ProcessLog(RCClient sender, RCClient.ProcessLogEventArgs args)
		{
			this.UIThread(delegate
			{
				if (args.Process.PerformanceString.Length == 0 || !RCProcess.IsStandardOutputLog(args.Message) || !RCProcess.GetOriginalLog(args.Message).StartsWith(args.Process.PerformanceString))
				{
					if (this.LViewClientList.SelectedItems.Count == 1 && ((ClientProcessItem)this.LViewClientList.SelectedItems[0]).IsSameWith(sender, args.Process))
					{
						this.TBoxLogProcess.AddLog(args.Message);
						this.TBoxLogProcess.ScrollToEnd();
					}
					this.processLogGenerators.LogGenerated(sender, args.Process, args.Message);
				}
			});
		}

		private void Client_ProcessStateChange(RCClient sender, RCClient.ProcessStateEventArgs args)
		{
			this.UIThread(delegate
			{
				this.ItemRefresh(sender, args.TargetProcess);
				if (args.OldState == RCProcess.ProcessState.On && (args.TargetProcess.State == RCProcess.ProcessState.Freezing || args.TargetProcess.State == RCProcess.ProcessState.Crash) && this.OnProcessCrash != null)
				{
					this.OnProcessCrash(sender, args.TargetProcess);
				}
				this.workGroup.UpdateProcessState(sender, args.TargetProcess);
				this.serverGroup.UpdateProcessState(sender, args.TargetProcess);
			});
		}

		private void Client_ProcessPerformanceUpdate(RCClient sender, RCProcess process)
		{
			this.UIThread(delegate
			{
				this.ItemRefresh(sender, process);
			});
		}

		private void ControlForm_Load(object sender, EventArgs e)
		{
			this.LViewClientList_SelectedIndexChanged(null, null);
		}

		private void TViewWorkGroup_KeyDown(object sender, KeyEventArgs args)
		{
			TreeView treeView = sender as TreeView;
			Keys keyCode = args.KeyCode;
			switch (keyCode)
			{
			case Keys.ShiftKey:
				this._keyShift = true;
				return;
			case Keys.ControlKey:
				this._keyControl = true;
				return;
			default:
				if (keyCode != Keys.Space)
				{
					return;
				}
				if (treeView.SelectedNode != null)
				{
					this.TreeView_CheckBoxClicked(treeView, treeView.SelectedNode, !this._keyControl);
				}
				return;
			}
		}

		private void TViewWorkGroup_KeyUp(object sender, KeyEventArgs args)
		{
			switch (args.KeyCode)
			{
			case Keys.ShiftKey:
				this._keyShift = false;
				return;
			case Keys.ControlKey:
				this._keyControl = false;
				return;
			default:
				return;
			}
		}

		private void TreeView_MouseDown(object sender, MouseEventArgs args)
		{
			TreeView treeView = sender as TreeView;
			TreeNode nodeAt = treeView.GetNodeAt(args.X, args.Y);
			TreeNode treeNode = null;
			if (nodeAt != null)
			{
				int num = 1;
				foreach (char c in nodeAt.FullPath)
				{
					if (new string(c, 1) == treeView.PathSeparator)
					{
						num++;
					}
				}
				if (args.X >= num * treeView.Indent)
				{
					treeView.SelectedNode = nodeAt;
					if (args.X <= num * treeView.Indent + 16)
					{
						treeNode = nodeAt;
					}
				}
			}
			if (args.Button == MouseButtons.Left && treeNode != null)
			{
				this.TreeView_CheckBoxClicked(treeView, treeNode, this._keyControl);
			}
		}

		private void TreeView_CheckBoxClicked(TreeView tree, TreeNode selectedNode, bool selectOnly)
		{
			tree.BeginUpdate();
			if (selectOnly)
			{
				this.DeselectAll(selectedNode.Nodes);
			}
			if (this._keyShift)
			{
				this.AllNameChange(selectedNode as WorkGroupTreeNode, selectedNode.Nodes);
			}
			else
			{
				WorkGroupTreeNode workGroupTreeNode = selectedNode as WorkGroupTreeNode;
				workGroupTreeNode.CheckBoxClicked();
			}
			tree.EndUpdate();
			WorkGroup workGroup = (tree == this.workGroup.View) ? this.serverGroup : this.workGroup;
			workGroup.SelectNodeFromListView();
		}

		private TreeView FindOwnerTree(ToolStripMenuItem item)
		{
			ContextMenuStrip contextMenuStrip = item.Owner as ContextMenuStrip;
			return contextMenuStrip.SourceControl as TreeView;
		}

		private void menuItemSelectAll_Click(object sender, EventArgs e)
		{
			TreeView treeView = this.FindOwnerTree(sender as ToolStripMenuItem);
			treeView.BeginUpdate();
			this.SelectAll(treeView.Nodes);
			treeView.EndUpdate();
		}

		private void menuItemDeselectAll_Click(object sender, EventArgs e)
		{
			TreeView treeView = this.FindOwnerTree(sender as ToolStripMenuItem);
			treeView.BeginUpdate();
			this.DeselectAll(treeView.Nodes);
			treeView.EndUpdate();
		}

		private void menuItemSelectOnly_Click(object sender, EventArgs e)
		{
			TreeView treeView = this.FindOwnerTree(sender as ToolStripMenuItem);
			treeView.BeginUpdate();
			this.DeselectAll(treeView.Nodes);
			(treeView.SelectedNode as WorkGroupTreeNode).CheckBoxClicked();
			treeView.EndUpdate();
		}

		private void menuItemSelectSameName_Click(object sender, EventArgs e)
		{
			TreeView treeView = this.FindOwnerTree(sender as ToolStripMenuItem);
			treeView.BeginUpdate();
			this.AllNameChange(treeView.SelectedNode as WorkGroupTreeNode, treeView.Nodes);
			treeView.EndUpdate();
		}

		private void menuItemSort_Click(object sender, EventArgs e)
		{
			TreeView treeView = this.FindOwnerTree(sender as ToolStripMenuItem);
			WorkGroup workGroup = (treeView == this.workGroup.View) ? this.workGroup : this.serverGroup;
			if (WorkGroup.Sort.SortType == SortType.Tree && WorkGroup.Sort.Current == workGroup)
			{
				return;
			}
			this.LViewClientList.Sorting = SortOrder.Ascending;
			this.ChangeSort(SortType.Tree, workGroup);
		}

		private void LViewClientList_SelectedIndexChanged(object sender, EventArgs args)
		{
			bool enabled = false;
			if (this.LViewClientList.SelectedItems.Count == 1)
			{
				this.TBoxLogProcess.Enabled = true;
				this.TBoxLogProcess.AddLog(((ClientProcessItem)this.LViewClientList.SelectedItems[0]).Process.GetLog());
				this.TBoxLogProcess.ScrollToEnd();
				ClientProcessItem clientProcessItem = this.LViewClientList.SelectedItems[0] as ClientProcessItem;
				if (clientProcessItem != null)
				{
					enabled = clientProcessItem.Process.TraceChildProcess;
				}
				this.TSBProcessLog.Enabled = true;
				this.LViewClientList.ContextMenuStrip = this.contextMenuStripProcess;
			}
			else
			{
				this.TBoxLogProcess.Enabled = false;
				this.TSBProcessLog.Enabled = false;
				this.LViewClientList.ContextMenuStrip = null;
			}
			bool flag = true;
			if (flag)
			{
				this.TStrpControl.Enabled = true;
				foreach (object obj in this.TStrpControl.Items)
				{
					ToolStripItem toolStripItem = (ToolStripItem)obj;
					toolStripItem.Enabled = true;
				}
				this.TSMIChildProcess.Enabled = enabled;
				return;
			}
			this.TStrpControl.Enabled = false;
			foreach (object obj2 in this.TStrpControl.Items)
			{
				ToolStripItem toolStripItem2 = (ToolStripItem)obj2;
				toolStripItem2.Enabled = false;
			}
			foreach (object obj3 in MainForm.Instance.Menu.MenuItems)
			{
				MenuItem menuItem = (MenuItem)obj3;
				if (menuItem.Text == LocalizeText.Get(391))
				{
					menuItem.Enabled = false;
				}
				else if (menuItem.Text == LocalizeText.Get(392))
				{
					menuItem.Enabled = false;
				}
				else if (menuItem.Text == LocalizeText.Get(393))
				{
					menuItem.Enabled = false;
				}
				else if (menuItem.Text == LocalizeText.Get(394))
				{
					menuItem.Enabled = false;
				}
			}
		}

		private void LViewClientList_DoubleClick(object sender, EventArgs e)
		{
			if (this.LViewClientList.SelectedItems.Count == 1)
			{
				ClientProcessItem clientProcessItem = (ClientProcessItem)this.LViewClientList.SelectedItems[0];
				new ProcessPropertyForm(clientProcessItem.Process, 0, false, false).ShowDialog();
			}
		}

		public void BtnStart_Click(object sender, EventArgs args)
		{
			if (this.client.Authority < Authority.ChiefGM)
			{
				Utility.ShowErrorMessage(LocalizeText.Get(229));
				return;
			}
			if (this.LViewClientList.SelectedItems.Count == 0)
			{
				Utility.ShowErrorMessage(LocalizeText.Get(230));
				return;
			}
			foreach (object obj in this.LViewClientList.SelectedItems)
			{
				ClientProcessItem clientProcessItem = (ClientProcessItem)obj;
				if (clientProcessItem.Process.State == RCProcess.ProcessState.Off || clientProcessItem.Process.State == RCProcess.ProcessState.Crash)
				{
					ControlRequestMessage controlRequestMessage = new ControlRequestMessage(SerializeWriter.ToBinary<StartProcessMessage>(new StartProcessMessage(clientProcessItem.Process.Name)).Bytes);
					controlRequestMessage.AddClientID(clientProcessItem.Client.ID);
					this.client.SendMessage<ControlRequestMessage>(controlRequestMessage);
				}
			}
		}

		private void TStrpBtnSync_Click(object sender, EventArgs e)
		{
			if (this.client.Authority < Authority.ChiefGM)
			{
				Utility.ShowErrorMessage(LocalizeText.Get(238));
				return;
			}
			if (this.LViewClientList.SelectedItems.Count == 0)
			{
				Utility.ShowErrorMessage(LocalizeText.Get(239));
				return;
			}
			Dictionary<int, List<string>> dictionary = new Dictionary<int, List<string>>();
			foreach (object obj in this.LViewClientList.SelectedItems)
			{
				ClientProcessItem clientProcessItem = (ClientProcessItem)obj;
				if (!dictionary.ContainsKey(clientProcessItem.Client.ID))
				{
					dictionary.Add(clientProcessItem.Client.ID, new List<string>());
				}
				dictionary[clientProcessItem.Client.ID].Add(clientProcessItem.Process.Name);
			}
			foreach (KeyValuePair<int, List<string>> keyValuePair in dictionary)
			{
				ControlRequestMessage controlRequestMessage = new ControlRequestMessage(SerializeWriter.ToBinary<CheckPatchProcessMessage>(new CheckPatchProcessMessage(keyValuePair.Value)).Bytes);
				controlRequestMessage.AddClientID(keyValuePair.Key);
				this.client.SendMessage<ControlRequestMessage>(controlRequestMessage);
			}
		}

		public void BtnCommand_Click(object sender, EventArgs args)
		{
			if (this.client.Authority < Authority.ChiefGM)
			{
				Utility.ShowErrorMessage(LocalizeText.Get(231));
				return;
			}
			if (this.LViewClientList.SelectedItems.Count == 0)
			{
				Utility.ShowErrorMessage(LocalizeText.Get(232));
				return;
			}
			ClientProcessItem clientProcessItem = null;
			foreach (object obj in this.LViewClientList.SelectedItems)
			{
				ClientProcessItem clientProcessItem2 = (ClientProcessItem)obj;
				if (clientProcessItem != null)
				{
					if (((clientProcessItem2.Process.Type.Length == 0) ? clientProcessItem2.Process.Name : clientProcessItem2.Process.Type) != ((clientProcessItem.Process.Type.Length == 0) ? clientProcessItem.Process.Name : clientProcessItem.Process.Type))
					{
						Utility.ShowErrorMessage(LocalizeText.Get(233));
						return;
					}
					if (clientProcessItem2.Process.CustomCommandString != clientProcessItem.Process.CustomCommandString)
					{
						Utility.ShowErrorMessage(string.Format(LocalizeText.Get(234), new object[]
						{
							clientProcessItem.Client.Name,
							clientProcessItem.Process.Description,
							clientProcessItem2.Client.Name,
							clientProcessItem2.Process.Description
						}));
						return;
					}
				}
				clientProcessItem = clientProcessItem2;
			}
			StdinCommandForm stdinCommandForm = new StdinCommandForm(clientProcessItem.Process.CustomCommandString, this.client.ServerGroups);
			if (stdinCommandForm.ShowDialog() == DialogResult.OK)
			{
				SortedList<string, ControlRequestMessage> sortedList = new SortedList<string, ControlRequestMessage>();
				foreach (object obj2 in this.LViewClientList.SelectedItems)
				{
					ClientProcessItem clientProcessItem3 = (ClientProcessItem)obj2;
					ControlRequestMessage controlRequestMessage;
					if (!sortedList.ContainsKey(clientProcessItem3.Process.Name))
					{
						sortedList.Add(clientProcessItem3.Process.Name, new ControlRequestMessage(SerializeWriter.ToBinary<StandardInProcessMessage>(new StandardInProcessMessage(clientProcessItem3.Process.Name, stdinCommandForm.Command)).Bytes));
						controlRequestMessage = sortedList[clientProcessItem3.Process.Name];
					}
					else
					{
						controlRequestMessage = sortedList[clientProcessItem3.Process.Name];
					}
					controlRequestMessage.AddClientID(clientProcessItem3.Client.ID);
				}
				foreach (KeyValuePair<string, ControlRequestMessage> keyValuePair in sortedList)
				{
					this.client.SendMessage<ControlRequestMessage>(keyValuePair.Value);
				}
			}
		}

		public void BtnStop_Click(object sender, EventArgs args)
		{
			if (this.client.Authority < Authority.ChiefGM)
			{
				Utility.ShowErrorMessage(LocalizeText.Get(235));
				return;
			}
			if (this.LViewClientList.SelectedItems.Count == 0)
			{
				Utility.ShowErrorMessage(LocalizeText.Get(236));
				return;
			}
			if (!Utility.InputYesNoFromWarning(LocalizeText.Get(237)))
			{
				return;
			}
			foreach (object obj in this.LViewClientList.SelectedItems)
			{
				ClientProcessItem clientProcessItem = (ClientProcessItem)obj;
				Packet packet = default(Packet);
				switch (clientProcessItem.Process.State)
				{
				case RCProcess.ProcessState.Updating:
					packet = SerializeWriter.ToBinary<KillUpdateProcessMessage>(new KillUpdateProcessMessage(clientProcessItem.Process.Name));
					break;
				case RCProcess.ProcessState.Booting:
				case RCProcess.ProcessState.Closing:
					packet = SerializeWriter.ToBinary<KillProcessMessage>(new KillProcessMessage(clientProcessItem.Process.Name));
					break;
				case RCProcess.ProcessState.On:
				case RCProcess.ProcessState.Freezing:
					packet = SerializeWriter.ToBinary<StopProcessMessage>(new StopProcessMessage(clientProcessItem.Process.Name));
					break;
				}
				if (packet.Bytes.Array != null)
				{
					ControlRequestMessage controlRequestMessage = new ControlRequestMessage(packet.Bytes);
					controlRequestMessage.AddClientID(clientProcessItem.Client.ID);
					this.client.SendMessage<ControlRequestMessage>(controlRequestMessage);
				}
			}
		}

		public void BtnUpdate_Click(object sender, EventArgs args)
		{
			if (this.client.Authority < Authority.ChiefGM)
			{
				Utility.ShowErrorMessage(LocalizeText.Get(238));
				return;
			}
			if (this.LViewClientList.SelectedItems.Count == 0)
			{
				Utility.ShowErrorMessage(LocalizeText.Get(239));
				return;
			}
			if (!Utility.InputYesNoFromWarning(LocalizeText.Get(240)))
			{
				return;
			}
			foreach (object obj in this.LViewClientList.SelectedItems)
			{
				ClientProcessItem clientProcessItem = (ClientProcessItem)obj;
				Packet packet = default(Packet);
				RCProcess.ProcessState state = clientProcessItem.Process.State;
				if (state == RCProcess.ProcessState.Off)
				{
					packet = SerializeWriter.ToBinary<UpdateProcessMessage>(new UpdateProcessMessage(clientProcessItem.Process.Name));
				}
				if (packet.Bytes.Array != null)
				{
					ControlRequestMessage controlRequestMessage = new ControlRequestMessage(packet.Bytes);
					controlRequestMessage.AddClientID(clientProcessItem.Client.ID);
					this.client.SendMessage<ControlRequestMessage>(controlRequestMessage);
				}
			}
		}

		public void BtnControl_Click(object sender, EventArgs args)
		{
			this.AcquireControlMutex(delegate(ControlEnterReply c)
			{
				RemoteControlSystemManager remoteControlSystemManager = new RemoteControlSystemManager(this.client, this.client.ClientList, this.workGroup.Node, this.serverGroup.Node, c.Processes);
				remoteControlSystemManager.OnClose += delegate(object s, EventArgs e)
				{
					this.ReleaseControlMutex();
				};
				remoteControlSystemManager.ShowDialog();
			});
		}

		private void TStrpControl_ButtonClick(object sender, ToolStripItemClickedEventArgs args)
		{
			if (args.ClickedItem == this.TStrpBtnStart)
			{
				this.BtnStart_Click(sender, args);
				return;
			}
			if (args.ClickedItem == this.TStrpBtnCommand)
			{
				this.BtnCommand_Click(sender, args);
				return;
			}
			if (args.ClickedItem == this.TStrpBtnUpdate)
			{
				this.BtnUpdate_Click(sender, args);
				return;
			}
			if (args.ClickedItem == this.TStrpBtnStop)
			{
				this.BtnStop_Click(sender, args);
				return;
			}
			if (args.ClickedItem == this.TStrpBtnControl)
			{
				this.BtnControl_Click(sender, args);
			}
		}

		private void TStrpBtnSvrCommand_Click(object sender, EventArgs e)
		{
			ServerCommandForm form = new ServerCommandForm(this.client.Authority, this.client.CommandBridges);
			if (form.ShowDialog(this) == DialogResult.OK)
			{
				foreach (KeyValuePair<int, StandardInProcessMessage> keyValuePair in form.Command)
				{
					ControlRequestMessage controlRequestMessage = new ControlRequestMessage(SerializeWriter.ToBinary<StandardInProcessMessage>(keyValuePair.Value).Bytes);
					controlRequestMessage.AddClientID(keyValuePair.Key);
					this.client.SendMessage<ControlRequestMessage>(controlRequestMessage);
				}
				if (form.ScheduleModified)
				{
					this.AcquireControlMutex(delegate(ControlEnterReply c)
					{
						foreach (KeyValuePair<int, RCProcess> keyValuePair2 in form.ScheduledProcess)
						{
							ModifyProcessMessage value = new ModifyProcessMessage(keyValuePair2.Value);
							ControlRequestMessage controlRequestMessage2 = new ControlRequestMessage(SerializeWriter.ToBinary<ModifyProcessMessage>(value).Bytes);
							controlRequestMessage2.AddClientID(keyValuePair2.Key);
							this.client.SendMessage<ControlRequestMessage>(controlRequestMessage2);
						}
						this.ReleaseControlMutex();
					});
				}
			}
		}

		private void LViewClientList_ColumnClick(object sender, ColumnClickEventArgs e)
		{
			SortType type = WorkGroup.Sort.SortType;
			switch (e.Column)
			{
			case 0:
				type = SortType.State;
				break;
			case 1:
				type = SortType.ProcessName;
				break;
			case 2:
				type = SortType.ServerName;
				break;
			case 3:
				type = SortType.State;
				break;
			case 4:
				type = SortType.Performance;
				break;
			case 5:
				type = SortType.ClientIP;
				break;
			}
			this.ChangeSort(type, WorkGroup.Sort.Current);
		}

		private void TSBGeneralLog_Click(object sender, EventArgs e)
		{
			if (this.OnSplitGeneralLog != null)
			{
				this.OnSplitGeneralLog(this, EventArgs.Empty);
			}
		}

		private void TSBProcessLog_Click(object sender, EventArgs e)
		{
			this.SplitProcessLog();
		}

		private void TSMISplitLog_Click(object sender, EventArgs e)
		{
			this.SplitProcessLog();
		}

		private void TSMISelectAll_Click(object sender, EventArgs e)
		{
			foreach (object obj in this.LViewClientList.Items)
			{
				ListViewItem listViewItem = (ListViewItem)obj;
				listViewItem.Selected = true;
			}
		}

		private void TSBAllProcessLog_Click(object sender, EventArgs e)
		{
			this.SplitAllProcessLog();
		}

		private void TSMIChildProcess_Click(object sender, EventArgs e)
		{
			if (this.LViewClientList.SelectedItems.Count == 1)
			{
				ClientProcessItem clientProcessItem = this.LViewClientList.SelectedItems[0] as ClientProcessItem;
				ChildProcessLogListRequestMessage message = new ChildProcessLogListRequestMessage(clientProcessItem.Client.ID, clientProcessItem.Process.Name);
				this.client.SendMessage<ChildProcessLogListRequestMessage>(message);
			}
		}

		private void SplitProcessLog()
		{
			if (this.OnSplitProcessLog != null && this.LViewClientList.SelectedItems.Count == 1)
			{
				ClientProcessItem clientProcessItem = (ClientProcessItem)this.LViewClientList.SelectedItems[0];
				this.OnSplitProcessLog(clientProcessItem.Process, new EventArgs<LogGenerator>(this.processLogGenerators.GetGenerator(clientProcessItem.Client, clientProcessItem.Process)));
			}
		}

		private void SplitAllProcessLog()
		{
			if (this.OnSplitAllProcessLog != null)
			{
				this.OnSplitAllProcessLog(null, new EventArgs<LogGenerator>(this.processLogGenerators.AllProcessLog));
			}
		}

		private void TSMIExeInfo_Click(object sender, EventArgs e)
		{
			if (this.LViewClientList.SelectedItems.Count == 1)
			{
				ClientProcessItem clientProcessItem = this.LViewClientList.SelectedItems[0] as ClientProcessItem;
				ExeInfoRequestMessage message = new ExeInfoRequestMessage(clientProcessItem.Client.ID, clientProcessItem.Process.Name);
				this.client.SendMessage<ExeInfoRequestMessage>(message);
			}
		}

		private void UpdateAlarmTime(object sender, EventArgs args)
		{
			this.lastAlarmUpdated = DateTime.Now;
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
			ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(ServerMonitorControl));
			this.toolStripSeparator1 = new ToolStripSeparator();
			this.toolStripSeparator2 = new ToolStripSeparator();
			this.toolStripSeparator3 = new ToolStripSeparator();
			this.toolStripSeparator4 = new ToolStripSeparator();
			this.PanelUpRight = new Panel();
			this.GroupClientList = new GroupBox();
			this.LViewClientList = new ListView();
			this.columnHeader1 = new ColumnHeader();
			this.ColumnProcessName = new ColumnHeader();
			this.ColumnClientName = new ColumnHeader();
			this.ColumnProcessState = new ColumnHeader();
			this.ColumnPerformance = new ColumnHeader();
			this.ColumnServerIP = new ColumnHeader();
			this.contextMenuStripProcess = new ContextMenuStrip(this.components);
			this.TSMISelectAll = new ToolStripMenuItem();
			this.toolStripSeparator6 = new ToolStripSeparator();
			this.TSMISplitLog = new ToolStripMenuItem();
			this.TSMIChildProcess = new ToolStripMenuItem();
			this.toolStripSeparator7 = new ToolStripSeparator();
			this.TSMIExeInfo = new ToolStripMenuItem();
			this.ImageProcessState = new ImageList(this.components);
			this.SplitterH = new Splitter();
			this.PanelUpLeft = new Panel();
			this.tabControlGroup = new TabControl();
			this.tabPageWorkGroup = new TabPage();
			this.TViewWorkGroup = new TreeView();
			this.ContextMenuWorkGroup = new ContextMenuStrip(this.components);
			this.menuItemSelectOnly = new ToolStripMenuItem();
			this.menuItemSelectSameName = new ToolStripMenuItem();
			this.toolStripSeparator5 = new ToolStripSeparator();
			this.menuItemSelectAll = new ToolStripMenuItem();
			this.menuItemDeselectAll = new ToolStripMenuItem();
			this.menuItemSort = new ToolStripMenuItem();
			this.ImageTreeViewIcons = new ImageList(this.components);
			this.tabPageServerGroup = new TabPage();
			this.TViewServerGroup = new TreeView();
			this.SplitterV = new Splitter();
			this.PanelDown = new Panel();
			this.TabControlLog = new TabControl();
			this.TabPageLogGeneral = new TabPage();
			this.TBoxLogGeneral = new TextBox();
			this.toolStripGeneralLog = new ToolStrip();
			this.TSBGeneralLog = new ToolStripButton();
			this.TabPageLogProcess = new TabPage();
			this.TBoxLogProcess = new LogTextBox();
			this.toolStripProcessLog = new ToolStrip();
			this.TSBProcessLog = new ToolStripButton();
			this.TSBAllProcessLog = new ToolStripButton();
			this.ToolTipServerControl = new ToolTip(this.components);
			this.TStrpControl = new ToolStrip();
			this.TStrpBtnStart = new ToolStripButton();
			this.TStrpBtnSvrCommand = new ToolStripButton();
			this.TStrpBtnCommand = new ToolStripButton();
			this.TStrpBtnUpdate = new ToolStripButton();
			this.TStrpBtnStop = new ToolStripButton();
			this.TStrpBtnControl = new ToolStripButton();
			this.toolStripSeparator8 = new ToolStripSeparator();
			this.TStrpBtnSync = new ToolStripButton();
			this.PanelUpRight.SuspendLayout();
			this.GroupClientList.SuspendLayout();
			this.contextMenuStripProcess.SuspendLayout();
			this.PanelUpLeft.SuspendLayout();
			this.tabControlGroup.SuspendLayout();
			this.tabPageWorkGroup.SuspendLayout();
			this.ContextMenuWorkGroup.SuspendLayout();
			this.tabPageServerGroup.SuspendLayout();
			this.PanelDown.SuspendLayout();
			this.TabControlLog.SuspendLayout();
			this.TabPageLogGeneral.SuspendLayout();
			this.toolStripGeneralLog.SuspendLayout();
			this.TabPageLogProcess.SuspendLayout();
			this.toolStripProcessLog.SuspendLayout();
			this.TStrpControl.SuspendLayout();
			base.SuspendLayout();
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new Size(6, 29);
			this.toolStripSeparator2.Name = "toolStripSeparator2";
			this.toolStripSeparator2.Size = new Size(6, 29);
			this.toolStripSeparator3.Name = "toolStripSeparator3";
			this.toolStripSeparator3.Size = new Size(6, 29);
			this.toolStripSeparator4.Name = "toolStripSeparator4";
			this.toolStripSeparator4.Size = new Size(6, 29);
			this.PanelUpRight.Controls.Add(this.GroupClientList);
			this.PanelUpRight.Dock = DockStyle.Fill;
			this.PanelUpRight.Location = new Point(260, 30);
			this.PanelUpRight.Name = "PanelUpRight";
			this.PanelUpRight.Padding = new Padding(0, 3, 0, 0);
			this.PanelUpRight.Size = new Size(540, 398);
			this.PanelUpRight.TabIndex = 13;
			this.GroupClientList.Controls.Add(this.LViewClientList);
			this.GroupClientList.Dock = DockStyle.Fill;
			this.GroupClientList.Location = new Point(0, 3);
			this.GroupClientList.Name = "GroupClientList";
			this.GroupClientList.Size = new Size(540, 395);
			this.GroupClientList.TabIndex = 1;
			this.GroupClientList.TabStop = false;
			this.GroupClientList.Text = "작업 그룹에 속해있는 프로그램 리스트 및 개별 상태";
			this.ToolTipServerControl.SetToolTip(this.GroupClientList, "제어할 프로그램들을 선택하세요.");
			this.LViewClientList.Anchor = (AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right);
			this.LViewClientList.Columns.AddRange(new ColumnHeader[]
			{
				this.columnHeader1,
				this.ColumnProcessName,
				this.ColumnClientName,
				this.ColumnProcessState,
				this.ColumnPerformance,
				this.ColumnServerIP
			});
			this.LViewClientList.ContextMenuStrip = this.contextMenuStripProcess;
			this.LViewClientList.FullRowSelect = true;
			this.LViewClientList.HideSelection = false;
			this.LViewClientList.Location = new Point(6, 25);
			this.LViewClientList.Name = "LViewClientList";
			this.LViewClientList.Size = new Size(528, 364);
			this.LViewClientList.SmallImageList = this.ImageProcessState;
			this.LViewClientList.TabIndex = 0;
			this.ToolTipServerControl.SetToolTip(this.LViewClientList, "제어할 프로그램들을 선택하세요.");
			this.LViewClientList.UseCompatibleStateImageBehavior = false;
			this.LViewClientList.View = View.Details;
			this.LViewClientList.SelectedIndexChanged += this.LViewClientList_SelectedIndexChanged;
			this.LViewClientList.DoubleClick += this.LViewClientList_DoubleClick;
			this.LViewClientList.ColumnClick += this.LViewClientList_ColumnClick;
			this.columnHeader1.Text = "∨";
			this.columnHeader1.Width = 24;
			this.ColumnProcessName.Text = "프로그램 이름";
			this.ColumnProcessName.Width = 180;
			this.ColumnClientName.Text = "컴퓨터 이름";
			this.ColumnClientName.Width = 180;
			this.ColumnProcessState.Text = "상태";
			this.ColumnPerformance.Text = "성능";
			this.ColumnPerformance.Width = 120;
			this.ColumnServerIP.Text = "컴퓨터 IP";
			this.ColumnServerIP.Width = 120;
			this.contextMenuStripProcess.Items.AddRange(new ToolStripItem[]
			{
				this.TSMISelectAll,
				this.toolStripSeparator6,
				this.TSMISplitLog,
				this.TSMIChildProcess,
				this.toolStripSeparator7,
				this.TSMIExeInfo
			});
			this.contextMenuStripProcess.Name = "contextMenuStripProcess";
			this.contextMenuStripProcess.Size = new Size(183, 104);
			this.TSMISelectAll.Name = "TSMISelectAll";
			this.TSMISelectAll.ShortcutKeys = (Keys)131137;
			this.TSMISelectAll.Size = new Size(182, 22);
			this.TSMISelectAll.Text = "전체 선택";
			this.TSMISelectAll.Click += this.TSMISelectAll_Click;
			this.toolStripSeparator6.Name = "toolStripSeparator6";
			this.toolStripSeparator6.Size = new Size(179, 6);
			this.TSMISplitLog.Name = "TSMISplitLog";
			this.TSMISplitLog.Size = new Size(182, 22);
			this.TSMISplitLog.Text = "새 창에서 로그 보기";
			this.TSMISplitLog.Click += this.TSMISplitLog_Click;
			this.TSMIChildProcess.Name = "TSMIChildProcess";
			this.TSMIChildProcess.Size = new Size(182, 22);
			this.TSMIChildProcess.Text = "자식 프로세스 로그";
			this.TSMIChildProcess.TextAlign = ContentAlignment.MiddleLeft;
			this.TSMIChildProcess.Click += this.TSMIChildProcess_Click;
			this.toolStripSeparator7.Name = "toolStripSeparator7";
			this.toolStripSeparator7.Size = new Size(179, 6);
			this.TSMIExeInfo.Name = "TSMIExeInfo";
			this.TSMIExeInfo.Size = new Size(182, 22);
			this.TSMIExeInfo.Text = "실행 파일 정보 보기";
			this.TSMIExeInfo.Click += this.TSMIExeInfo_Click;
			this.ImageProcessState.ImageStream = (ImageListStreamer)componentResourceManager.GetObject("ImageProcessState.ImageStream");
			this.ImageProcessState.TransparentColor = Color.White;
			this.ImageProcessState.Images.SetKeyName(0, "StateOff.bmp");
			this.ImageProcessState.Images.SetKeyName(1, "StateUpdating.bmp");
			this.ImageProcessState.Images.SetKeyName(2, "StateBooting.bmp");
			this.ImageProcessState.Images.SetKeyName(3, "StateOn.bmp");
			this.ImageProcessState.Images.SetKeyName(4, "StateClosing.bmp");
			this.ImageProcessState.Images.SetKeyName(5, "StateFreezing.bmp");
			this.SplitterH.Location = new Point(250, 30);
			this.SplitterH.Name = "SplitterH";
			this.SplitterH.Size = new Size(10, 398);
			this.SplitterH.TabIndex = 12;
			this.SplitterH.TabStop = false;
			this.PanelUpLeft.Controls.Add(this.tabControlGroup);
			this.PanelUpLeft.Dock = DockStyle.Left;
			this.PanelUpLeft.Location = new Point(0, 30);
			this.PanelUpLeft.Name = "PanelUpLeft";
			this.PanelUpLeft.Padding = new Padding(0, 3, 0, 0);
			this.PanelUpLeft.Size = new Size(250, 398);
			this.PanelUpLeft.TabIndex = 11;
			this.tabControlGroup.Controls.Add(this.tabPageWorkGroup);
			this.tabControlGroup.Controls.Add(this.tabPageServerGroup);
			this.tabControlGroup.Dock = DockStyle.Fill;
			this.tabControlGroup.ItemSize = new Size(100, 21);
			this.tabControlGroup.Location = new Point(0, 3);
			this.tabControlGroup.Name = "tabControlGroup";
			this.tabControlGroup.SelectedIndex = 0;
			this.tabControlGroup.Size = new Size(250, 395);
			this.tabControlGroup.SizeMode = TabSizeMode.Fixed;
			this.tabControlGroup.TabIndex = 0;
			this.tabPageWorkGroup.Controls.Add(this.TViewWorkGroup);
			this.tabPageWorkGroup.Location = new Point(4, 25);
			this.tabPageWorkGroup.Name = "tabPageWorkGroup";
			this.tabPageWorkGroup.Size = new Size(242, 366);
			this.tabPageWorkGroup.TabIndex = 0;
			this.tabPageWorkGroup.Text = "작업 그룹";
			this.tabPageWorkGroup.UseVisualStyleBackColor = true;
			this.TViewWorkGroup.ContextMenuStrip = this.ContextMenuWorkGroup;
			this.TViewWorkGroup.Dock = DockStyle.Fill;
			this.TViewWorkGroup.ImageIndex = 0;
			this.TViewWorkGroup.ImageList = this.ImageTreeViewIcons;
			this.TViewWorkGroup.Indent = 19;
			this.TViewWorkGroup.ItemHeight = 14;
			this.TViewWorkGroup.Location = new Point(0, 0);
			this.TViewWorkGroup.Name = "TViewWorkGroup";
			this.TViewWorkGroup.SelectedImageIndex = 0;
			this.TViewWorkGroup.ShowNodeToolTips = true;
			this.TViewWorkGroup.Size = new Size(242, 366);
			this.TViewWorkGroup.TabIndex = 2;
			this.TViewWorkGroup.MouseDown += this.TreeView_MouseDown;
			this.TViewWorkGroup.KeyUp += this.TViewWorkGroup_KeyUp;
			this.TViewWorkGroup.KeyDown += this.TViewWorkGroup_KeyDown;
			this.ContextMenuWorkGroup.Items.AddRange(new ToolStripItem[]
			{
				this.menuItemSelectOnly,
				this.menuItemSelectSameName,
				this.toolStripSeparator5,
				this.menuItemSelectAll,
				this.menuItemDeselectAll,
				this.menuItemSort
			});
			this.ContextMenuWorkGroup.Name = "ContextMenuWorkGroup";
			this.ContextMenuWorkGroup.Size = new Size(326, 120);
			this.menuItemSelectOnly.Name = "menuItemSelectOnly";
			this.menuItemSelectOnly.Size = new Size(325, 22);
			this.menuItemSelectOnly.Text = "이 항목만 선택(Ctrl + Click)";
			this.menuItemSelectOnly.Click += this.menuItemSelectOnly_Click;
			this.menuItemSelectSameName.Name = "menuItemSelectSameName";
			this.menuItemSelectSameName.Size = new Size(325, 22);
			this.menuItemSelectSameName.Text = "이 항목과 같은 이름을 전부 선택(Shift + Click)";
			this.menuItemSelectSameName.Click += this.menuItemSelectSameName_Click;
			this.toolStripSeparator5.Name = "toolStripSeparator5";
			this.toolStripSeparator5.Size = new Size(322, 6);
			this.menuItemSelectAll.Name = "menuItemSelectAll";
			this.menuItemSelectAll.ShortcutKeys = (Keys)131137;
			this.menuItemSelectAll.Size = new Size(325, 22);
			this.menuItemSelectAll.Text = "전체 선택";
			this.menuItemSelectAll.Click += this.menuItemSelectAll_Click;
			this.menuItemDeselectAll.Name = "menuItemDeselectAll";
			this.menuItemDeselectAll.ShortcutKeys = (Keys)131140;
			this.menuItemDeselectAll.Size = new Size(325, 22);
			this.menuItemDeselectAll.Text = "전체 해제";
			this.menuItemDeselectAll.Click += this.menuItemDeselectAll_Click;
			this.menuItemSort.Name = "menuItemSort";
			this.menuItemSort.Size = new Size(325, 22);
			this.menuItemSort.Text = "트리 구조로 리스트 정렬";
			this.menuItemSort.Click += this.menuItemSort_Click;
			this.ImageTreeViewIcons.ImageStream = (ImageListStreamer)componentResourceManager.GetObject("ImageTreeViewIcons.ImageStream");
			this.ImageTreeViewIcons.TransparentColor = Color.White;
			this.ImageTreeViewIcons.Images.SetKeyName(0, "Blank.bmp");
			this.ImageTreeViewIcons.Images.SetKeyName(1, "Gray.bmp");
			this.ImageTreeViewIcons.Images.SetKeyName(2, "Checked.bmp");
			this.tabPageServerGroup.Controls.Add(this.TViewServerGroup);
			this.tabPageServerGroup.Location = new Point(4, 25);
			this.tabPageServerGroup.Name = "tabPageServerGroup";
			this.tabPageServerGroup.Size = new Size(242, 366);
			this.tabPageServerGroup.TabIndex = 1;
			this.tabPageServerGroup.Text = "서버 그룹";
			this.tabPageServerGroup.UseVisualStyleBackColor = true;
			this.TViewServerGroup.ContextMenuStrip = this.ContextMenuWorkGroup;
			this.TViewServerGroup.Dock = DockStyle.Fill;
			this.TViewServerGroup.ImageIndex = 0;
			this.TViewServerGroup.ImageList = this.ImageTreeViewIcons;
			this.TViewServerGroup.Indent = 19;
			this.TViewServerGroup.ItemHeight = 14;
			this.TViewServerGroup.Location = new Point(0, 0);
			this.TViewServerGroup.Name = "TViewServerGroup";
			this.TViewServerGroup.SelectedImageIndex = 0;
			this.TViewServerGroup.ShowNodeToolTips = true;
			this.TViewServerGroup.Size = new Size(242, 366);
			this.TViewServerGroup.TabIndex = 3;
			this.TViewServerGroup.MouseDown += this.TreeView_MouseDown;
			this.TViewServerGroup.KeyUp += this.TViewWorkGroup_KeyUp;
			this.TViewServerGroup.KeyDown += this.TViewWorkGroup_KeyDown;
			this.SplitterV.Dock = DockStyle.Bottom;
			this.SplitterV.Location = new Point(0, 428);
			this.SplitterV.Name = "SplitterV";
			this.SplitterV.Size = new Size(800, 8);
			this.SplitterV.TabIndex = 10;
			this.SplitterV.TabStop = false;
			this.PanelDown.Controls.Add(this.TabControlLog);
			this.PanelDown.Dock = DockStyle.Bottom;
			this.PanelDown.Location = new Point(0, 436);
			this.PanelDown.Name = "PanelDown";
			this.PanelDown.Size = new Size(800, 160);
			this.PanelDown.TabIndex = 9;
			this.TabControlLog.Controls.Add(this.TabPageLogGeneral);
			this.TabControlLog.Controls.Add(this.TabPageLogProcess);
			this.TabControlLog.Dock = DockStyle.Fill;
			this.TabControlLog.Location = new Point(0, 0);
			this.TabControlLog.Name = "TabControlLog";
			this.TabControlLog.SelectedIndex = 0;
			this.TabControlLog.Size = new Size(800, 160);
			this.TabControlLog.TabIndex = 1;
			this.TabPageLogGeneral.Controls.Add(this.TBoxLogGeneral);
			this.TabPageLogGeneral.Controls.Add(this.toolStripGeneralLog);
			this.TabPageLogGeneral.Location = new Point(4, 22);
			this.TabPageLogGeneral.Name = "TabPageLogGeneral";
			this.TabPageLogGeneral.Size = new Size(792, 134);
			this.TabPageLogGeneral.TabIndex = 1;
			this.TabPageLogGeneral.Text = "전체 로그";
			this.TabPageLogGeneral.Visible = false;
			this.TBoxLogGeneral.BackColor = SystemColors.Window;
			this.TBoxLogGeneral.Dock = DockStyle.Fill;
			this.TBoxLogGeneral.Location = new Point(0, 0);
			this.TBoxLogGeneral.Multiline = true;
			this.TBoxLogGeneral.Name = "TBoxLogGeneral";
			this.TBoxLogGeneral.ReadOnly = true;
			this.TBoxLogGeneral.ScrollBars = ScrollBars.Vertical;
			this.TBoxLogGeneral.Size = new Size(764, 134);
			this.TBoxLogGeneral.TabIndex = 4;
			this.ToolTipServerControl.SetToolTip(this.TBoxLogGeneral, "전체적인 상황에 대한 일반 로그를 보여줍니다.");
			this.toolStripGeneralLog.AutoSize = false;
			this.toolStripGeneralLog.Dock = DockStyle.Right;
			this.toolStripGeneralLog.GripStyle = ToolStripGripStyle.Hidden;
			this.toolStripGeneralLog.Items.AddRange(new ToolStripItem[]
			{
				this.TSBGeneralLog
			});
			this.toolStripGeneralLog.Location = new Point(764, 0);
			this.toolStripGeneralLog.Name = "toolStripGeneralLog";
			this.toolStripGeneralLog.Size = new Size(28, 134);
			this.toolStripGeneralLog.TabIndex = 3;
			this.toolStripGeneralLog.Text = "toolStrip1";
			this.TSBGeneralLog.DisplayStyle = ToolStripItemDisplayStyle.Image;
			this.TSBGeneralLog.Image = (Image)componentResourceManager.GetObject("TSBGeneralLog.Image");
			this.TSBGeneralLog.ImageTransparentColor = Color.White;
			this.TSBGeneralLog.Name = "TSBGeneralLog";
			this.TSBGeneralLog.Size = new Size(26, 20);
			this.TSBGeneralLog.Text = "새 창에서 보기";
			this.TSBGeneralLog.Click += this.TSBGeneralLog_Click;
			this.TabPageLogProcess.Controls.Add(this.TBoxLogProcess);
			this.TabPageLogProcess.Controls.Add(this.toolStripProcessLog);
			this.TabPageLogProcess.Location = new Point(4, 22);
			this.TabPageLogProcess.Name = "TabPageLogProcess";
			this.TabPageLogProcess.Size = new Size(792, 134);
			this.TabPageLogProcess.TabIndex = 0;
			this.TabPageLogProcess.Text = "프로그램의 출력 로그";
			this.TBoxLogProcess.DisabledBackColor = SystemColors.Control;
			this.TBoxLogProcess.DisabledForeColor = SystemColors.ControlText;
			this.TBoxLogProcess.DisabledText = null;
			this.TBoxLogProcess.Dock = DockStyle.Fill;
			this.TBoxLogProcess.EnabledBackColor = Color.Black;
			this.TBoxLogProcess.EnabledForeColor = Color.White;
			this.TBoxLogProcess.Location = new Point(0, 0);
			this.TBoxLogProcess.Name = "TBoxLogProcess";
			this.TBoxLogProcess.Size = new Size(764, 134);
			this.TBoxLogProcess.TabIndex = 6;
			this.toolStripProcessLog.AutoSize = false;
			this.toolStripProcessLog.Dock = DockStyle.Right;
			this.toolStripProcessLog.GripStyle = ToolStripGripStyle.Hidden;
			this.toolStripProcessLog.Items.AddRange(new ToolStripItem[]
			{
				this.TSBProcessLog,
				this.TSBAllProcessLog
			});
			this.toolStripProcessLog.Location = new Point(764, 0);
			this.toolStripProcessLog.Name = "toolStripProcessLog";
			this.toolStripProcessLog.Size = new Size(28, 134);
			this.toolStripProcessLog.TabIndex = 4;
			this.toolStripProcessLog.Text = "toolStrip1";
			this.TSBProcessLog.DisplayStyle = ToolStripItemDisplayStyle.Image;
			this.TSBProcessLog.Image = (Image)componentResourceManager.GetObject("TSBProcessLog.Image");
			this.TSBProcessLog.ImageTransparentColor = Color.White;
			this.TSBProcessLog.Name = "TSBProcessLog";
			this.TSBProcessLog.Size = new Size(26, 20);
			this.TSBProcessLog.Text = "새 창에서 보기";
			this.TSBProcessLog.ToolTipText = "새 창의 띄워서 로그를 봅니다.";
			this.TSBProcessLog.Click += this.TSBProcessLog_Click;
			this.TSBAllProcessLog.DisplayStyle = ToolStripItemDisplayStyle.Image;
			this.TSBAllProcessLog.Image = (Image)componentResourceManager.GetObject("TSBAllProcessLog.Image");
			this.TSBAllProcessLog.ImageTransparentColor = Color.White;
			this.TSBAllProcessLog.Name = "TSBAllProcessLog";
			this.TSBAllProcessLog.Size = new Size(26, 20);
			this.TSBAllProcessLog.Text = "모든 프로세스 로그 보기";
			this.TSBAllProcessLog.ToolTipText = "새 창을 띄워서 모든 프로세스 로그를 봅니다.";
			this.TSBAllProcessLog.Click += this.TSBAllProcessLog_Click;
			this.TStrpControl.AutoSize = false;
			this.TStrpControl.Items.AddRange(new ToolStripItem[]
			{
				this.TStrpBtnStart,
				this.toolStripSeparator8,
				this.TStrpBtnSync,
				this.toolStripSeparator1,
				this.TStrpBtnSvrCommand,
				this.TStrpBtnCommand,
				this.toolStripSeparator2,
				this.TStrpBtnUpdate,
				this.toolStripSeparator3,
				this.TStrpBtnStop,
				this.toolStripSeparator4,
				this.TStrpBtnControl
			});
			this.TStrpControl.Location = new Point(0, 0);
			this.TStrpControl.Name = "TStrpControl";
			this.TStrpControl.Padding = new Padding(0, 1, 1, 0);
			this.TStrpControl.Size = new Size(800, 30);
			this.TStrpControl.Stretch = true;
			this.TStrpControl.TabIndex = 16;
			this.TStrpControl.Text = "toolStripMenu";
			this.TStrpControl.ItemClicked += this.TStrpControl_ButtonClick;
			this.TStrpBtnStart.Image = (Image)componentResourceManager.GetObject("TStrpBtnStart.Image");
			this.TStrpBtnStart.ImageTransparentColor = Color.White;
			this.TStrpBtnStart.Name = "TStrpBtnStart";
			this.TStrpBtnStart.Padding = new Padding(5, 0, 0, 0);
			this.TStrpBtnStart.Size = new Size(25, 26);
			this.TStrpBtnStart.TextAlign = ContentAlignment.MiddleRight;
			this.TStrpBtnSvrCommand.Image = (Image)componentResourceManager.GetObject("TStrpBtnSvrCommand.Image");
			this.TStrpBtnSvrCommand.ImageTransparentColor = Color.White;
			this.TStrpBtnSvrCommand.Name = "TStrpBtnSvrCommand";
			this.TStrpBtnSvrCommand.Size = new Size(79, 26);
			this.TStrpBtnSvrCommand.Text = "서버 작업";
			this.TStrpBtnSvrCommand.Click += this.TStrpBtnSvrCommand_Click;
			this.TStrpBtnCommand.Image = (Image)componentResourceManager.GetObject("TStrpBtnCommand.Image");
			this.TStrpBtnCommand.ImageTransparentColor = Color.White;
			this.TStrpBtnCommand.Name = "TStrpBtnCommand";
			this.TStrpBtnCommand.Padding = new Padding(5, 0, 0, 0);
			this.TStrpBtnCommand.Size = new Size(108, 26);
			this.TStrpBtnCommand.Text = "프로그램 작업";
			this.TStrpBtnCommand.TextAlign = ContentAlignment.MiddleRight;
			this.TStrpBtnUpdate.Image = (Image)componentResourceManager.GetObject("TStrpBtnUpdate.Image");
			this.TStrpBtnUpdate.ImageTransparentColor = Color.White;
			this.TStrpBtnUpdate.Name = "TStrpBtnUpdate";
			this.TStrpBtnUpdate.Padding = new Padding(5, 0, 0, 0);
			this.TStrpBtnUpdate.Size = new Size(25, 26);
			this.TStrpBtnUpdate.TextAlign = ContentAlignment.MiddleRight;
			this.TStrpBtnStop.Image = (Image)componentResourceManager.GetObject("TStrpBtnStop.Image");
			this.TStrpBtnStop.ImageTransparentColor = Color.White;
			this.TStrpBtnStop.Name = "TStrpBtnStop";
			this.TStrpBtnStop.Padding = new Padding(5, 0, 0, 0);
			this.TStrpBtnStop.Size = new Size(25, 26);
			this.TStrpBtnStop.TextAlign = ContentAlignment.MiddleRight;
			this.TStrpBtnControl.Image = (Image)componentResourceManager.GetObject("TStrpBtnControl.Image");
			this.TStrpBtnControl.ImageTransparentColor = Color.White;
			this.TStrpBtnControl.Name = "TStrpBtnControl";
			this.TStrpBtnControl.Padding = new Padding(5, 0, 0, 0);
			this.TStrpBtnControl.Size = new Size(25, 26);
			this.TStrpBtnControl.TextAlign = ContentAlignment.MiddleRight;
			this.toolStripSeparator8.Name = "toolStripSeparator8";
			this.toolStripSeparator8.Size = new Size(6, 29);
			this.TStrpBtnSync.Image = Resources.alarmon;
			this.TStrpBtnSync.ImageTransparentColor = Color.Magenta;
			this.TStrpBtnSync.Name = "TStrpBtnSync";
			this.TStrpBtnSync.Size = new Size(79, 26);
			this.TStrpBtnSync.Text = "패치 확인";
			this.TStrpBtnSync.Click += this.TStrpBtnSync_Click;
			base.Controls.Add(this.PanelUpRight);
			base.Controls.Add(this.SplitterH);
			base.Controls.Add(this.PanelUpLeft);
			base.Controls.Add(this.SplitterV);
			base.Controls.Add(this.PanelDown);
			base.Controls.Add(this.TStrpControl);
			base.Name = "ServerMonitorControl";
			base.Size = new Size(800, 596);
			base.Load += this.ControlForm_Load;
			this.PanelUpRight.ResumeLayout(false);
			this.GroupClientList.ResumeLayout(false);
			this.contextMenuStripProcess.ResumeLayout(false);
			this.PanelUpLeft.ResumeLayout(false);
			this.tabControlGroup.ResumeLayout(false);
			this.tabPageWorkGroup.ResumeLayout(false);
			this.ContextMenuWorkGroup.ResumeLayout(false);
			this.tabPageServerGroup.ResumeLayout(false);
			this.PanelDown.ResumeLayout(false);
			this.TabControlLog.ResumeLayout(false);
			this.TabPageLogGeneral.ResumeLayout(false);
			this.TabPageLogGeneral.PerformLayout();
			this.toolStripGeneralLog.ResumeLayout(false);
			this.toolStripGeneralLog.PerformLayout();
			this.TabPageLogProcess.ResumeLayout(false);
			this.toolStripProcessLog.ResumeLayout(false);
			this.toolStripProcessLog.PerformLayout();
			this.TStrpControl.ResumeLayout(false);
			this.TStrpControl.PerformLayout();
			base.ResumeLayout(false);
		}

		private RCUserHandler client;

		private bool _keyControl;

		private bool _keyShift;

		private WorkGroup workGroup;

		private WorkGroup serverGroup;

		private Utility.ILViewComparer processNameComparer;

		private Utility.ILViewComparer serverNameComparer;

		private Utility.ILViewComparer performaceComparer;

		private Utility.ILViewComparer stateComparer;

		private Utility.ILViewComparer clientIPComparer;

		private ProcessLogGeneratorCollection processLogGenerators;

		private List<Action<ControlEnterReply>> OnControlReply = new List<Action<ControlEnterReply>>();

		private int ControlMutex;

		private ControlEnterReply hasControl;

		private DateTime lastAlarmUpdated;

		private IContainer components;

		private ColumnHeader ColumnClientName;

		private ColumnHeader ColumnProcessState;

		private ColumnHeader ColumnProcessName;

		private ColumnHeader ColumnServerIP;

		private TabControl TabControlLog;

		private TabPage TabPageLogProcess;

		private TabPage TabPageLogGeneral;

		private ToolTip ToolTipServerControl;

		private Panel PanelUpRight;

		private GroupBox GroupClientList;

		private ListView LViewClientList;

		private Splitter SplitterH;

		private Panel PanelUpLeft;

		private Splitter SplitterV;

		private Panel PanelDown;

		private ImageList ImageTreeViewIcons;

		private ImageList ImageProcessState;

		private ToolStrip TStrpControl;

		private ToolStripButton TStrpBtnCommand;

		private ToolStripButton TStrpBtnUpdate;

		private ToolStripButton TStrpBtnStop;

		private ToolStripButton TStrpBtnControl;

		private ToolStripButton TStrpBtnStart;

		private ToolStripSeparator toolStripSeparator1;

		private ToolStripSeparator toolStripSeparator2;

		private ToolStripSeparator toolStripSeparator3;

		private ToolStripSeparator toolStripSeparator4;

		private ContextMenuStrip ContextMenuWorkGroup;

		private ToolStripMenuItem menuItemSelectOnly;

		private ToolStripMenuItem menuItemSelectSameName;

		private ToolStripSeparator toolStripSeparator5;

		private ToolStripMenuItem menuItemSelectAll;

		private ToolStripMenuItem menuItemDeselectAll;

		private ColumnHeader ColumnPerformance;

		private TabControl tabControlGroup;

		private TabPage tabPageWorkGroup;

		private TreeView TViewWorkGroup;

		private TabPage tabPageServerGroup;

		private TreeView TViewServerGroup;

		private ColumnHeader columnHeader1;

		private ToolStripMenuItem menuItemSort;

		private ToolStripButton TStrpBtnSvrCommand;

		private ContextMenuStrip contextMenuStripProcess;

		private ToolStripMenuItem TSMIChildProcess;

		private TextBox TBoxLogGeneral;

		private ToolStrip toolStripGeneralLog;

		private ToolStripButton TSBGeneralLog;

		private ToolStripMenuItem TSMISplitLog;

		private ToolStrip toolStripProcessLog;

		private ToolStripButton TSBProcessLog;

		private ToolStripButton TSBAllProcessLog;

		private ToolStripMenuItem TSMISelectAll;

		private ToolStripSeparator toolStripSeparator6;

		private ToolStripSeparator toolStripSeparator7;

		private ToolStripMenuItem TSMIExeInfo;

		private LogTextBox TBoxLogProcess;

		private ToolStripSeparator toolStripSeparator8;

		private ToolStripButton TStrpBtnSync;
	}
}
