using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using Devcat.Core.Net.Message;
using RemoteControlSystem;
using RemoteControlSystem.ClientMessage;
using RemoteControlSystem.ServerMessage;

namespace HeroesOpTool.RCUser.ServerMonitorSystem.SystemManage
{
	public class RCClientControl : UserControl
	{
		public RCClientControl()
		{
			this.InitializeComponent();
			this.listViewRCClient.ColumnClick += this.ListViewRCClient_ColumnClick;
			this.messageList = new List<ControlRequestMessage>();
		}

		public ICollection<ControlRequestMessage> MessageList
		{
			get
			{
				return this.messageList;
			}
		}

		public void AddClientRequest(RCClient client, bool forceRun)
		{
			if (!forceRun && this.listViewRCClient.InvokeRequired)
			{
				RCClientControl.Callback_AddClient method = new RCClientControl.Callback_AddClient(this.AddClientRequest);
				base.Invoke(method, new object[]
				{
					client,
					true
				});
				return;
			}
			int i = 0;
			int num = this.listViewRCClient.Items.Count;
			while (i < num)
			{
				int num2 = (i + num) / 2;
				int num3 = ((RCClientControl.ClientItem)this.listViewRCClient.Items[num2]).CompareTo(client.Name, client.ClientIP, client.ID);
				if (num3 < 0)
				{
					i = num2 + 1;
				}
				else
				{
					if (num3 <= 0)
					{
						return;
					}
					num = num2;
				}
			}
			this.listViewRCClient.Items.Insert(i, new RCClientControl.ClientItem(client));
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
			this.AddClientRequest(client, false);
		}

		private void RemoveClientRequest(string name, string ip, int id, bool bForceRun)
		{
			if (!bForceRun && this.listViewRCClient.InvokeRequired)
			{
				RCClientControl.CallBack_RemoveClientRequest method = new RCClientControl.CallBack_RemoveClientRequest(this.RemoveClientRequest);
				base.Invoke(method, new object[]
				{
					name,
					ip,
					id,
					true
				});
				return;
			}
			int i = 0;
			int num = this.listViewRCClient.Items.Count;
			while (i < num)
			{
				int num2 = (i + num) / 2;
				int num3 = ((RCClientControl.ClientItem)this.listViewRCClient.Items[num2]).CompareTo(name, ip, id);
				if (num3 < 0)
				{
					i = num2 + 1;
				}
				else
				{
					if (num3 <= 0)
					{
						this.listViewRCClient.Items.RemoveAt(num2);
						return;
					}
					num = num2;
				}
			}
		}

		public void RemoveClient(RCClient client)
		{
			this.RemoveClient(client.Name, client.ClientIP, client.ID);
		}

		private void RemoveClient(string name, string ip, int id)
		{
			this.RemoveClientRequest(name, ip, id, false);
		}

		public void SetTemplateList(RCProcessCollection _processTemplateCollection)
		{
			this.processTemplateCollection = _processTemplateCollection;
		}

		private void CalculateCommonProcess()
		{
			this.listViewProcess.Items.Clear();
			if (this.listViewRCClient.SelectedItems.Count > 0)
			{
				SortedList<RCProcess, int> sortedList = new SortedList<RCProcess, int>();
				foreach (object obj in this.listViewRCClient.SelectedItems)
				{
					RCClientControl.ClientItem clientItem = (RCClientControl.ClientItem)obj;
					foreach (RCProcess key in clientItem.ProcessList)
					{
						int num = 0;
						if (!sortedList.ContainsKey(key))
						{
							sortedList.Add(key, 0);
						}
						else
						{
							num = sortedList[key];
						}
						sortedList[key] = num + 1;
					}
				}
				int count = this.listViewRCClient.SelectedItems.Count;
				foreach (KeyValuePair<RCProcess, int> keyValuePair in sortedList)
				{
					this.listViewProcess.Items.Add(new RCClientControl.ProcessItem(keyValuePair.Key, keyValuePair.Value != count));
				}
			}
		}

		private void CalculateCommonProcessThread()
		{
			for (;;)
			{
				long num = (this.sleepTime - DateTime.Now.Ticks) / 10000L;
				if (num <= 0L)
				{
					lock (this)
					{
						this.calculateThread = null;
						this.UIThread(delegate
						{
							this.CalculateCommonProcess();
						});
						break;
					}
				}
				Thread.Sleep((int)num);
			}
		}

		private void RecalculateCommonProcess()
		{
			lock (this)
			{
				this.sleepTime = DateTime.Now.Ticks + 1000000L;
				if (this.calculateThread == null)
				{
					this.calculateThread = new Thread(new ThreadStart(this.CalculateCommonProcessThread));
					this.calculateThread.Start();
				}
			}
		}

		private void ListViewRCClient_ColumnClick(object o, ColumnClickEventArgs e)
		{
			if (e.Column == 1)
			{
				this.listViewRCClient.ListViewItemSorter = new Utility.ListViewItemIPComparer(e.Column);
				return;
			}
			this.listViewRCClient.ListViewItemSorter = new Utility.ListViewItemComparer(e.Column);
		}

		private void ListViewRCClient_SelectedIndexChanged(object sender, EventArgs e)
		{
			this.RecalculateCommonProcess();
		}

		private void ListViewProcess_DoubleClick(object sender, EventArgs e)
		{
			this.menuItemProcessProperty_Click(sender, e);
		}

		private void menuItemProcessAdd_Click(object sender, EventArgs e)
		{
			if (this.calculateThread != null)
			{
				return;
			}
			ProcessPropertyForm processPropertyForm = new ProcessPropertyForm(this.processTemplateCollection);
			if (processPropertyForm.ShowDialog(this) == DialogResult.OK)
			{
				ControlRequestMessage controlRequestMessage = new ControlRequestMessage(SerializeWriter.ToBinary<AddProcessMessage>(new AddProcessMessage(processPropertyForm.RCProcess)).Bytes);
				bool flag = false;
				foreach (object obj in this.listViewRCClient.SelectedItems)
				{
					RCClientControl.ClientItem clientItem = (RCClientControl.ClientItem)obj;
					try
					{
						clientItem.AddProcess(processPropertyForm.RCProcess);
						controlRequestMessage.AddClientID(clientItem.ID);
					}
					catch (ArgumentException)
					{
						flag = true;
					}
				}
				this.messageList.Add(controlRequestMessage);
				if (flag)
				{
					Utility.ShowErrorMessage(LocalizeText.Get(343));
				}
				this.RecalculateCommonProcess();
			}
		}

		private void menuItemProcessRemove_Click(object sender, EventArgs e)
		{
			if (this.listViewProcess.SelectedItems.Count > 0 && this.calculateThread == null)
			{
				string text = ((RCClientControl.ProcessItem)this.listViewProcess.SelectedItems[0]).Text;
				ControlRequestMessage controlRequestMessage = new ControlRequestMessage(SerializeWriter.ToBinary<RemoveProcessMessage>(new RemoveProcessMessage(text)).Bytes);
				foreach (object obj in this.listViewRCClient.SelectedItems)
				{
					RCClientControl.ClientItem clientItem = (RCClientControl.ClientItem)obj;
					clientItem.RemoveProcess(text);
					controlRequestMessage.AddClientID(clientItem.ID);
				}
				this.messageList.Add(controlRequestMessage);
				this.RecalculateCommonProcess();
			}
		}

		private void menuItemProcessProperty_Click(object sender, EventArgs e)
		{
			if (this.listViewProcess.SelectedItems.Count == 1 && this.calculateThread == null)
			{
				bool multEdit = this.listViewRCClient.SelectedItems.Count > 1;
				int num = 8;
				foreach (object obj in this.listViewRCClient.SelectedItems)
				{
					RCClientControl.ClientItem clientItem = (RCClientControl.ClientItem)obj;
					if (clientItem.Version < num)
					{
						num = clientItem.Version;
					}
				}
				ProcessPropertyForm processPropertyForm = new ProcessPropertyForm(((RCClientControl.ProcessItem)this.listViewProcess.SelectedItems[0]).Process, num, true, multEdit);
				if (processPropertyForm.ShowDialog() == DialogResult.OK)
				{
					bool flag = this.listViewRCClient.SelectedItems.Count == 1;
					foreach (object obj2 in this.listViewRCClient.SelectedItems)
					{
						RCClientControl.ClientItem clientItem2 = (RCClientControl.ClientItem)obj2;
						ModifyProcessMessage value;
						if (flag)
						{
							value = new ModifyProcessMessage(processPropertyForm.RCProcess);
							clientItem2.ModifyProcess(processPropertyForm.RCProcess);
						}
						else
						{
							RCProcess process = clientItem2.ModifyProcessWithoutPrimaryInfo(processPropertyForm.RCProcess);
							value = new ModifyProcessMessage(process);
						}
						ControlRequestMessage controlRequestMessage = new ControlRequestMessage(SerializeWriter.ToBinary<ModifyProcessMessage>(value).Bytes);
						controlRequestMessage.AddClientID(clientItem2.ID);
						this.messageList.Add(controlRequestMessage);
					}
				}
				this.RecalculateCommonProcess();
			}
		}

		private void menuItemSelfUpdate_Click(object sender, EventArgs e)
		{
			if (Utility.InputYesNoFromWarning(LocalizeText.Get(344)))
			{
				SelfUpdateForm selfUpdateForm = new SelfUpdateForm();
				if (selfUpdateForm.ShowDialog() == DialogResult.OK)
				{
					ControlRequestMessage controlRequestMessage = new ControlRequestMessage(SerializeWriter.ToBinary<ClientSelfUpdateMessage>(new ClientSelfUpdateMessage(selfUpdateForm.Argument)).Bytes);
					foreach (object obj in this.listViewRCClient.SelectedItems)
					{
						RCClientControl.ClientItem clientItem = (RCClientControl.ClientItem)obj;
						controlRequestMessage.AddClientID(clientItem.ID);
						this.RemoveClient(clientItem.Name, clientItem.IP, clientItem.ID);
					}
					this.messageList.Add(controlRequestMessage);
				}
			}
		}

		private void RCClientControl_Load(object sender, EventArgs e)
		{
			this.labelRCClient.Text = LocalizeText.Get(332);
			this.columnHeader1.Text = LocalizeText.Get(333);
			this.columnHeader2.Text = LocalizeText.Get(334);
			this.columnHeader4.Text = LocalizeText.Get(335);
			this.labelProcess.Text = LocalizeText.Get(336);
			this.columnHeader3.Text = LocalizeText.Get(337);
			this.menuItemProcessAdd.Text = LocalizeText.Get(338);
			this.menuItemProcessRemove.Text = LocalizeText.Get(339);
			this.menuItemProcessProperty.Text = LocalizeText.Get(340);
			this.menuItemClientProperty.Text = LocalizeText.Get(341);
			this.menuItemSelfUpdate.Text = LocalizeText.Get(342);
		}

		//protected override void Dispose(bool disposing)
		//{
		//	if (disposing && this.components != null)
		//	{
		//		this.components.Dispose();
		//	}
		//	base.Dispose(disposing);
		//}

		private void InitializeComponent()
		{
			this.labelRCClient = new Label();
			this.listViewRCClient = new ListView();
			this.columnHeader1 = new ColumnHeader();
			this.columnHeader2 = new ColumnHeader();
			this.columnHeader4 = new ColumnHeader();
			this.contextMenuClient = new ContextMenu();
			this.menuItemClientProperty = new MenuItem();
			this.menuItem2 = new MenuItem();
			this.menuItemSelfUpdate = new MenuItem();
			this.labelProcess = new Label();
			this.listViewProcess = new ListView();
			this.columnHeader3 = new ColumnHeader();
			this.contextMenuProcess = new ContextMenu();
			this.menuItemProcessAdd = new MenuItem();
			this.menuItemProcessRemove = new MenuItem();
			this.menuItemS1 = new MenuItem();
			this.menuItemProcessProperty = new MenuItem();
			this.panelLeft = new Panel();
			this.splitterV = new Splitter();
			this.panelRight = new Panel();
			this.panelLeft.SuspendLayout();
			this.panelRight.SuspendLayout();
			base.SuspendLayout();
			this.labelRCClient.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
			this.labelRCClient.Location = new Point(8, 8);
			this.labelRCClient.Name = "labelRCClient";
			this.labelRCClient.Size = new Size(368, 24);
			this.labelRCClient.TabIndex = 0;
			this.labelRCClient.Text = "현재 접속된 컴퓨터 리스트";
			this.labelRCClient.TextAlign = ContentAlignment.MiddleLeft;
			this.listViewRCClient.Anchor = (AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right);
			this.listViewRCClient.Columns.AddRange(new ColumnHeader[]
			{
				this.columnHeader1,
				this.columnHeader2,
				this.columnHeader4
			});
			this.listViewRCClient.ContextMenu = this.contextMenuClient;
			this.listViewRCClient.FullRowSelect = true;
			this.listViewRCClient.HideSelection = false;
			this.listViewRCClient.Location = new Point(8, 32);
			this.listViewRCClient.Name = "listViewRCClient";
			this.listViewRCClient.Size = new Size(368, 360);
			this.listViewRCClient.TabIndex = 1;
			this.listViewRCClient.UseCompatibleStateImageBehavior = false;
			this.listViewRCClient.View = View.Details;
			this.listViewRCClient.SelectedIndexChanged += this.ListViewRCClient_SelectedIndexChanged;
			this.columnHeader1.Text = "컴퓨터 이름";
			this.columnHeader1.Width = 180;
			this.columnHeader2.Text = "컴퓨터 IP";
			this.columnHeader2.Width = 120;
			this.columnHeader4.Text = "버전";
			this.columnHeader4.Width = 40;
			this.contextMenuClient.MenuItems.AddRange(new MenuItem[]
			{
				this.menuItemClientProperty,
				this.menuItem2,
				this.menuItemSelfUpdate
			});
			this.menuItemClientProperty.Index = 0;
			this.menuItemClientProperty.Text = "등록 정보";
			this.menuItem2.Index = 1;
			this.menuItem2.Text = "-";
			this.menuItemSelfUpdate.Index = 2;
			this.menuItemSelfUpdate.Text = "자가 업데이트 실행";
			this.menuItemSelfUpdate.Click += this.menuItemSelfUpdate_Click;
			this.labelProcess.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
			this.labelProcess.Location = new Point(0, 8);
			this.labelProcess.Name = "labelProcess";
			this.labelProcess.Size = new Size(208, 24);
			this.labelProcess.TabIndex = 1;
			this.labelProcess.Text = "공통으로 설치된 프로그램 리스트";
			this.labelProcess.TextAlign = ContentAlignment.MiddleLeft;
			this.listViewProcess.Anchor = (AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right);
			this.listViewProcess.Columns.AddRange(new ColumnHeader[]
			{
				this.columnHeader3
			});
			this.listViewProcess.ContextMenu = this.contextMenuProcess;
			this.listViewProcess.FullRowSelect = true;
			this.listViewProcess.HeaderStyle = ColumnHeaderStyle.Nonclickable;
			this.listViewProcess.HideSelection = false;
			this.listViewProcess.Location = new Point(0, 32);
			this.listViewProcess.MultiSelect = false;
			this.listViewProcess.Name = "listViewProcess";
			this.listViewProcess.Size = new Size(208, 360);
			this.listViewProcess.TabIndex = 2;
			this.listViewProcess.UseCompatibleStateImageBehavior = false;
			this.listViewProcess.View = View.Details;
			this.listViewProcess.DoubleClick += this.ListViewProcess_DoubleClick;
			this.columnHeader3.Text = "프로그램 이름";
			this.columnHeader3.Width = 180;
			this.contextMenuProcess.MenuItems.AddRange(new MenuItem[]
			{
				this.menuItemProcessAdd,
				this.menuItemProcessRemove,
				this.menuItemS1,
				this.menuItemProcessProperty
			});
			this.menuItemProcessAdd.Index = 0;
			this.menuItemProcessAdd.Shortcut = Shortcut.Ins;
			this.menuItemProcessAdd.Text = "새 프로그램 추가";
			this.menuItemProcessAdd.Click += this.menuItemProcessAdd_Click;
			this.menuItemProcessRemove.Index = 1;
			this.menuItemProcessRemove.Shortcut = Shortcut.Del;
			this.menuItemProcessRemove.Text = "선택된 프로그램 삭제";
			this.menuItemProcessRemove.Click += this.menuItemProcessRemove_Click;
			this.menuItemS1.Index = 2;
			this.menuItemS1.Text = "-";
			this.menuItemProcessProperty.Index = 3;
			this.menuItemProcessProperty.Text = "등록 정보";
			this.menuItemProcessProperty.Click += this.menuItemProcessProperty_Click;
			this.panelLeft.Controls.Add(this.labelRCClient);
			this.panelLeft.Controls.Add(this.listViewRCClient);
			this.panelLeft.Dock = DockStyle.Left;
			this.panelLeft.Location = new Point(0, 0);
			this.panelLeft.Name = "panelLeft";
			this.panelLeft.Size = new Size(376, 400);
			this.panelLeft.TabIndex = 3;
			this.splitterV.Location = new Point(376, 0);
			this.splitterV.Name = "splitterV";
			this.splitterV.Size = new Size(8, 400);
			this.splitterV.TabIndex = 4;
			this.splitterV.TabStop = false;
			this.panelRight.Controls.Add(this.listViewProcess);
			this.panelRight.Controls.Add(this.labelProcess);
			this.panelRight.Dock = DockStyle.Fill;
			this.panelRight.Location = new Point(384, 0);
			this.panelRight.Name = "panelRight";
			this.panelRight.Size = new Size(216, 400);
			this.panelRight.TabIndex = 5;
			base.Controls.Add(this.panelRight);
			base.Controls.Add(this.splitterV);
			base.Controls.Add(this.panelLeft);
			base.Name = "RCClientControl";
			base.Size = new Size(600, 400);
			base.Load += this.RCClientControl_Load;
			this.panelLeft.ResumeLayout(false);
			this.panelRight.ResumeLayout(false);
			base.ResumeLayout(false);
		}

		private List<ControlRequestMessage> messageList;

		private Thread calculateThread;

		private long sleepTime;

		private RCProcessCollection processTemplateCollection;

		//private IContainer components;

		private ColumnHeader columnHeader1;

		private ColumnHeader columnHeader2;

		private ColumnHeader columnHeader3;

		private Panel panelLeft;

		private Splitter splitterV;

		private Panel panelRight;

		private Label labelRCClient;

		private ListView listViewRCClient;

		private Label labelProcess;

		private ListView listViewProcess;

		private ContextMenu contextMenuProcess;

		private MenuItem menuItemProcessAdd;

		private MenuItem menuItemProcessRemove;

		private MenuItem menuItemS1;

		private MenuItem menuItemProcessProperty;

		private ColumnHeader columnHeader4;

		private ContextMenu contextMenuClient;

		private MenuItem menuItem2;

		private MenuItem menuItemSelfUpdate;

		private MenuItem menuItemClientProperty;

		private class ClientItem : ListViewItem
		{
			public ICollection<RCProcess> ProcessList
			{
				get
				{
					return this._processCollection;
				}
			}

			public new string Name { get; private set; }

			public string IP { get; private set; }

			public int ID { get; private set; }

			public int Version { get; private set; }

			public ClientItem(RCClient client) : base(new string[]
			{
				client.Name,
				client.ClientIP,
				client.Version.ToString()
			})
			{
				this.Name = client.Name;
				this.IP = client.ClientIP;
				this.ID = client.ID;
				this.Version = client.Version;
				this._processCollection = client.ProcessClone();
			}

			public void AddProcess(RCProcess newProcess)
			{
				if (newProcess != null)
				{
					this._processCollection.Add(newProcess);
				}
			}

			public void ModifyProcess(RCProcess newProcess)
			{
				if (newProcess != null && this._processCollection.Contains(newProcess.Name))
				{
					this._processCollection[newProcess.Name].Modify(newProcess);
				}
			}

			public RCProcess ModifyProcessWithoutPrimaryInfo(RCProcess newProcess)
			{
				if (newProcess != null)
				{
					if (this._processCollection.Contains(newProcess.Name))
					{
						this._processCollection[newProcess.Name].ModifyEx(newProcess);
					}
					return this._processCollection[newProcess.Name];
				}
				return null;
			}

			public void RemoveProcess(string processName)
			{
				this._processCollection.Remove(processName);
			}

			private int CompareIP(string ip1, string ip2)
			{
				string[] array = ip1.Split(new char[]
				{
					'.'
				});
				string[] array2 = ip2.Split(new char[]
				{
					'.'
				});
				try
				{
					for (int i = 0; i < array.Length; i++)
					{
						int num = int.Parse(array[i]).CompareTo(int.Parse(array2[i]));
						if (num != 0)
						{
							return num;
						}
					}
				}
				catch (Exception)
				{
					return ip1.CompareTo(ip2);
				}
				return 0;
			}

			public int CompareTo(string name, string ip, int id)
			{
				int num = this.Name.CompareTo(name);
				if (num != 0)
				{
					return num;
				}
				num = this.CompareIP(this.IP, ip);
				if (num != 0)
				{
					return num;
				}
				return this.ID.CompareTo(id);
			}

			private RCProcessCollection _processCollection;
		}

		private class ProcessItem : ListViewItem
		{
			public RCProcess Process { get; private set; }

			public ProcessItem(RCProcess process, bool disabled) : base(process.Name)
			{
				this.Process = process;
				if (disabled)
				{
					base.ForeColor = Color.FromKnownColor(KnownColor.GrayText);
				}
			}
		}

		private delegate void Callback_AddClient(RCClient client, bool forceRun);

		private delegate void CallBack_RemoveClientRequest(string name, string ip, int id, bool bForceRun);
	}
}
