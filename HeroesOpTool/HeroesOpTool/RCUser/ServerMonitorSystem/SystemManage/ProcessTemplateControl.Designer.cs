using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using RemoteControlSystem;

namespace HeroesOpTool.RCUser.ServerMonitorSystem.SystemManage
{
	public class ProcessTemplateControl : UserControl
	{
		public ProcessTemplateControl()
		{
			this.InitializeComponent();
			this.processTemplateCollection = new RCProcessCollection();
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
			this.listViewProcessTemplate = new ListView();
			this.columnHeaderName = new ColumnHeader();
			this.columnHeaderDisplayName = new ColumnHeader();
			this.columnHeaderExecuteFile = new ColumnHeader();
			this.columnHeaderUpdateFile = new ColumnHeader();
			this.columnHeaderBootedString = new ColumnHeader();
			this.columnHeaderShutdownString = new ColumnHeader();
			this.columnHeaderPerformanceString = new ColumnHeader();
			this.columnHeaderPerformanceDescription = new ColumnHeader();
			this.columnHeaderStdOutLines = new ColumnHeader();
			this.columnHeaderUse = new ColumnHeader();
			this.columnHeaderAutoStart = new ColumnHeader();
			this.columnHeaderAutoRestart = new ColumnHeader();
			this.contextMenuProcessTemplate = new ContextMenu();
			this.menuItemTemplateNew = new MenuItem();
			this.menuItemTemplateModify = new MenuItem();
			this.menuItemTemplateDelete = new MenuItem();
			this.labelProcessTemplate = new Label();
			base.SuspendLayout();
			this.listViewProcessTemplate.Anchor = (AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right);
			this.listViewProcessTemplate.Columns.AddRange(new ColumnHeader[]
			{
				this.columnHeaderName,
				this.columnHeaderDisplayName,
				this.columnHeaderExecuteFile,
				this.columnHeaderUpdateFile,
				this.columnHeaderBootedString,
				this.columnHeaderShutdownString,
				this.columnHeaderPerformanceString,
				this.columnHeaderPerformanceDescription,
				this.columnHeaderStdOutLines,
				this.columnHeaderUse,
				this.columnHeaderAutoStart,
				this.columnHeaderAutoRestart
			});
			this.listViewProcessTemplate.ContextMenu = this.contextMenuProcessTemplate;
			this.listViewProcessTemplate.FullRowSelect = true;
			this.listViewProcessTemplate.HeaderStyle = ColumnHeaderStyle.Nonclickable;
			this.listViewProcessTemplate.Location = new Point(8, 32);
			this.listViewProcessTemplate.MultiSelect = false;
			this.listViewProcessTemplate.Name = "listViewProcessTemplate";
			this.listViewProcessTemplate.Size = new Size(584, 360);
			this.listViewProcessTemplate.TabIndex = 0;
			this.listViewProcessTemplate.UseCompatibleStateImageBehavior = false;
			this.listViewProcessTemplate.View = View.Details;
			this.listViewProcessTemplate.DoubleClick += this.listViewProcessTemplate_DoubleClick;
			this.columnHeaderName.Text = "이름";
			this.columnHeaderName.Width = 90;
			this.columnHeaderDisplayName.Text = "표시할 이름";
			this.columnHeaderDisplayName.Width = 180;
			this.columnHeaderExecuteFile.Text = "작업 폴더, 실행 파일  및 인자";
			this.columnHeaderExecuteFile.Width = 180;
			this.columnHeaderUpdateFile.Text = "업데이트 파일 및 인자";
			this.columnHeaderUpdateFile.Width = 120;
			this.columnHeaderBootedString.Text = "부팅 인식 문자열";
			this.columnHeaderShutdownString.Text = "종료 문자열";
			this.columnHeaderPerformanceString.Text = "상태 모니터 문자열";
			this.columnHeaderPerformanceDescription.Text = "상태 모니터 설명";
			this.columnHeaderPerformanceDescription.Width = 180;
			this.columnHeaderStdOutLines.Text = "기억할 표준 출력 라인";
			this.columnHeaderUse.Text = "사용중";
			this.columnHeaderUse.Width = 30;
			this.columnHeaderAutoStart.Text = "컴퓨터 시작시 자동 실행";
			this.columnHeaderAutoStart.Width = 30;
			this.columnHeaderAutoRestart.Text = "자가 종료시 자동 재시작";
			this.columnHeaderAutoRestart.Width = 30;
			this.contextMenuProcessTemplate.MenuItems.AddRange(new MenuItem[]
			{
				this.menuItemTemplateNew,
				this.menuItemTemplateModify,
				this.menuItemTemplateDelete
			});
			this.menuItemTemplateNew.Index = 0;
			this.menuItemTemplateNew.Shortcut = Shortcut.Ins;
			this.menuItemTemplateNew.Text = "새 템플릿 추가";
			this.menuItemTemplateNew.Click += this.menuItemTemplateNew_Click;
			this.menuItemTemplateModify.Index = 1;
			this.menuItemTemplateModify.Text = "선택된 템플릿 변경";
			this.menuItemTemplateModify.Click += this.menuItemTemplateModify_Click;
			this.menuItemTemplateDelete.Index = 2;
			this.menuItemTemplateDelete.Shortcut = Shortcut.Del;
			this.menuItemTemplateDelete.Text = "선택된 템플릿 삭제";
			this.menuItemTemplateDelete.Click += this.menuItemTemplateDelete_Click;
			this.labelProcessTemplate.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
			this.labelProcessTemplate.Location = new Point(8, 8);
			this.labelProcessTemplate.Name = "labelProcessTemplate";
			this.labelProcessTemplate.Size = new Size(584, 24);
			this.labelProcessTemplate.TabIndex = 1;
			this.labelProcessTemplate.Text = "프로세스 템플릿 리스트";
			this.labelProcessTemplate.TextAlign = ContentAlignment.MiddleLeft;
			base.Controls.Add(this.labelProcessTemplate);
			base.Controls.Add(this.listViewProcessTemplate);
			base.Name = "ProcessTemplateControl";
			base.Size = new Size(600, 400);
			base.Load += this.ProcessTemplateControl_Load;
			base.ResumeLayout(false);
		}

		public RCProcessCollection ProcessTemplateCollection
		{
			get
			{
				return this.processTemplateCollection;
			}
		}

		public bool Modified
		{
			get
			{
				return this._modified;
			}
			set
			{
				this._modified = value;
			}
		}

		public void SetTemplateList(RCProcessCollection _processTemplateCollection)
		{
			this.processTemplateCollection = _processTemplateCollection;
			foreach (RCProcess processTemplate in this.processTemplateCollection)
			{
				this.AddView(processTemplate);
			}
		}

		private void AddView(RCProcess processTemplate, int index)
		{
			ListViewItem listViewItem = new ListViewItem(new string[]
			{
				processTemplate.Name,
				processTemplate.Description,
				string.Concat(new string[]
				{
					processTemplate.WorkingDirectory,
					"\\",
					processTemplate.ExecuteName,
					" ",
					processTemplate.ExecuteArgs
				}),
				processTemplate.UpdateExecuteName + " " + processTemplate.UpdateExecuteArgs,
				processTemplate.BootedString,
				processTemplate.ShutdownString,
				processTemplate.PerformanceString,
				processTemplate.PerformanceDescription,
				processTemplate.LogLines.ToString(),
				processTemplate.DefaultSelect ? "O" : "X",
				processTemplate.AutomaticStart ? "O" : "X",
				processTemplate.AutomaticRestart ? "O" : "X"
			});
			if (index == -1)
			{
				this.listViewProcessTemplate.Items.Add(listViewItem);
				index = this.listViewProcessTemplate.Items.Count - 1;
			}
			else
			{
				this.listViewProcessTemplate.Items.Insert(index, listViewItem);
			}
			this.listViewProcessTemplate.Items[index].Focused = true;
			this.listViewProcessTemplate.Items[index].Selected = true;
		}

		private void AddView(RCProcess processTemplate)
		{
			this.AddView(processTemplate, -1);
		}

		private void AddTemplate(RCProcess process)
		{
			this.processTemplateCollection.Add(process);
			this.AddView(process);
		}

		private void ModifyTemplate(RCProcess process, int index)
		{
			if (this.processTemplateCollection.Contains(process))
			{
				this.processTemplateCollection[process.Name].Modify(process);
				this.listViewProcessTemplate.Items.RemoveAt(index);
				this.AddView(process, index);
			}
		}

		private void DeleteTemplate(int index)
		{
			string text = this.listViewProcessTemplate.Items[index].Text;
			foreach (RCProcess rcprocess in this.processTemplateCollection)
			{
				if (rcprocess.Name == text)
				{
					this.processTemplateCollection.Remove(text);
					break;
				}
			}
			this.listViewProcessTemplate.Items.RemoveAt(this.listViewProcessTemplate.SelectedIndices[0]);
		}

		private void menuItemTemplateNew_Click(object sender, EventArgs e)
		{
			ProcessPropertyForm processPropertyForm = new ProcessPropertyForm(this.processTemplateCollection);
			while (processPropertyForm.ShowDialog() == DialogResult.OK)
			{
				try
				{
					this.AddTemplate(processPropertyForm.RCProcess);
					this._modified = true;
					break;
				}
				catch (ArgumentException ex)
				{
					Utility.ShowErrorMessage(LocalizeText.Get(330) + ex.Message);
				}
			}
		}

		private void menuItemTemplateModify_Click(object sender, EventArgs e)
		{
			if (this.listViewProcessTemplate.SelectedItems.Count > 0)
			{
				string text = this.listViewProcessTemplate.SelectedItems[0].Text;
				foreach (RCProcess rcprocess in this.processTemplateCollection)
				{
					if (rcprocess.Name == text)
					{
						ProcessPropertyForm processPropertyForm = new ProcessPropertyForm(rcprocess, 0, true, false);
						while (processPropertyForm.ShowDialog() == DialogResult.OK)
						{
							try
							{
								this.ModifyTemplate(processPropertyForm.RCProcess, this.listViewProcessTemplate.SelectedIndices[0]);
								this._modified = true;
								break;
							}
							catch (ArgumentException ex)
							{
								Utility.ShowErrorMessage(LocalizeText.Get(331) + ex.Message);
							}
						}
						break;
					}
				}
			}
		}

		private void menuItemTemplateDelete_Click(object sender, EventArgs e)
		{
			if (this.listViewProcessTemplate.SelectedItems.Count > 0)
			{
				this.DeleteTemplate(this.listViewProcessTemplate.SelectedIndices[0]);
				this._modified = true;
			}
		}

		private void listViewProcessTemplate_DoubleClick(object sender, EventArgs e)
		{
			this.menuItemTemplateModify_Click(sender, e);
		}

		private void ProcessTemplateControl_Load(object sender, EventArgs e)
		{
			this.columnHeaderName.Text = LocalizeText.Get(314);
			this.columnHeaderDisplayName.Text = LocalizeText.Get(315);
			this.columnHeaderExecuteFile.Text = LocalizeText.Get(316);
			this.columnHeaderUpdateFile.Text = LocalizeText.Get(317);
			this.columnHeaderBootedString.Text = LocalizeText.Get(318);
			this.columnHeaderShutdownString.Text = LocalizeText.Get(319);
			this.columnHeaderPerformanceString.Text = LocalizeText.Get(320);
			this.columnHeaderPerformanceDescription.Text = LocalizeText.Get(321);
			this.columnHeaderStdOutLines.Text = LocalizeText.Get(322);
			this.columnHeaderAutoRestart.Text = LocalizeText.Get(323);
			this.columnHeaderUse.Text = LocalizeText.Get(324);
			this.columnHeaderAutoStart.Text = LocalizeText.Get(325);
			this.menuItemTemplateNew.Text = LocalizeText.Get(326);
			this.menuItemTemplateModify.Text = LocalizeText.Get(327);
			this.menuItemTemplateDelete.Text = LocalizeText.Get(328);
			this.labelProcessTemplate.Text = LocalizeText.Get(329);
		}

		private ColumnHeader columnHeaderName;

		private ColumnHeader columnHeaderDisplayName;

		private ColumnHeader columnHeaderExecuteFile;

		private ColumnHeader columnHeaderBootedString;

		private ColumnHeader columnHeaderShutdownString;

		private ColumnHeader columnHeaderPerformanceString;

		private ColumnHeader columnHeaderPerformanceDescription;

		private ColumnHeader columnHeaderStdOutLines;

		private ColumnHeader columnHeaderUpdateFile;

		private ColumnHeader columnHeaderUse;

		private ColumnHeader columnHeaderAutoStart;

		private ListView listViewProcessTemplate;

		private Label labelProcessTemplate;

		private ContextMenu contextMenuProcessTemplate;

		private MenuItem menuItemTemplateNew;

		private MenuItem menuItemTemplateDelete;

		private MenuItem menuItemTemplateModify;

		private ColumnHeader columnHeaderAutoRestart;

		//private Container components;

		private RCProcessCollection processTemplateCollection;

		private bool _modified;
	}
}
