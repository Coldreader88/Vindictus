namespace HeroesOpTool.RCUser.ServerMonitorSystem
{
	public partial class StdinCommandForm : global::HeroesOpTool.RCUser.ServerMonitorSystem.CommandForm
	{
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
			this.labelComment = new global::System.Windows.Forms.Label();
			this.panelButton = new global::System.Windows.Forms.Panel();
			this.buttonCancel = new global::System.Windows.Forms.Button();
			this.buttonOK = new global::System.Windows.Forms.Button();
			this.tabControl1 = new global::System.Windows.Forms.TabControl();
			this.tabPage1 = new global::System.Windows.Forms.TabPage();
			this.splitContainer = new global::System.Windows.Forms.SplitContainer();
			this.groupBoxCommand = new global::System.Windows.Forms.GroupBox();
			this.listBoxCommand = new global::System.Windows.Forms.ListBox();
			this.groupBoxArgList = new global::System.Windows.Forms.GroupBox();
			this.panelArgList = new global::System.Windows.Forms.Panel();
			this.panelButton.SuspendLayout();
			this.tabControl1.SuspendLayout();
			this.tabPage1.SuspendLayout();
			this.splitContainer.Panel1.SuspendLayout();
			this.splitContainer.Panel2.SuspendLayout();
			this.splitContainer.SuspendLayout();
			this.groupBoxCommand.SuspendLayout();
			this.groupBoxArgList.SuspendLayout();
			base.SuspendLayout();
			this.labelComment.Font = new global::System.Drawing.Font("굴림", 8.25f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 129);
			this.labelComment.Location = new global::System.Drawing.Point(103, 124);
			this.labelComment.Margin = new global::System.Windows.Forms.Padding(0);
			this.labelComment.Name = "labelComment";
			this.labelComment.Size = new global::System.Drawing.Size(246, 30);
			this.labelComment.TabIndex = 4;
			this.panelButton.Controls.Add(this.buttonCancel);
			this.panelButton.Controls.Add(this.buttonOK);
			this.panelButton.Dock = global::System.Windows.Forms.DockStyle.Bottom;
			this.panelButton.Location = new global::System.Drawing.Point(0, 236);
			this.panelButton.Name = "panelButton";
			this.panelButton.Size = new global::System.Drawing.Size(438, 45);
			this.panelButton.TabIndex = 5;
			this.buttonCancel.Anchor = (global::System.Windows.Forms.AnchorStyles.Bottom | global::System.Windows.Forms.AnchorStyles.Right);
			this.buttonCancel.DialogResult = global::System.Windows.Forms.DialogResult.Cancel;
			this.buttonCancel.Location = new global::System.Drawing.Point(330, 8);
			this.buttonCancel.Name = "buttonCancel";
			this.buttonCancel.Size = new global::System.Drawing.Size(96, 29);
			this.buttonCancel.TabIndex = 5;
			this.buttonCancel.Text = "취소";
			this.buttonOK.Anchor = (global::System.Windows.Forms.AnchorStyles.Bottom | global::System.Windows.Forms.AnchorStyles.Right);
			this.buttonOK.Location = new global::System.Drawing.Point(228, 8);
			this.buttonOK.Name = "buttonOK";
			this.buttonOK.Size = new global::System.Drawing.Size(96, 29);
			this.buttonOK.TabIndex = 4;
			this.buttonOK.Text = "실행";
			this.buttonOK.Click += new global::System.EventHandler(this.buttonOK_Click);
			this.tabControl1.Controls.Add(this.tabPage1);
			this.tabControl1.Dock = global::System.Windows.Forms.DockStyle.Fill;
			this.tabControl1.Location = new global::System.Drawing.Point(0, 0);
			this.tabControl1.Multiline = true;
			this.tabControl1.Name = "tabControl1";
			this.tabControl1.SelectedIndex = 0;
			this.tabControl1.Size = new global::System.Drawing.Size(438, 236);
			this.tabControl1.TabIndex = 9;
			this.tabPage1.BackColor = global::System.Drawing.Color.Transparent;
			this.tabPage1.Controls.Add(this.splitContainer);
			this.tabPage1.Location = new global::System.Drawing.Point(4, 22);
			this.tabPage1.Name = "tabPage1";
			this.tabPage1.Padding = new global::System.Windows.Forms.Padding(3);
			this.tabPage1.Size = new global::System.Drawing.Size(430, 210);
			this.tabPage1.TabIndex = 0;
			this.tabPage1.Text = "서버 명령";
			this.tabPage1.UseVisualStyleBackColor = true;
			this.splitContainer.Anchor = (global::System.Windows.Forms.AnchorStyles.Top | global::System.Windows.Forms.AnchorStyles.Bottom | global::System.Windows.Forms.AnchorStyles.Left | global::System.Windows.Forms.AnchorStyles.Right);
			this.splitContainer.Location = new global::System.Drawing.Point(8, 6);
			this.splitContainer.Name = "splitContainer";
			this.splitContainer.Panel1.Controls.Add(this.groupBoxCommand);
			this.splitContainer.Panel2.Controls.Add(this.groupBoxArgList);
			this.splitContainer.Size = new global::System.Drawing.Size(416, 204);
			this.splitContainer.SplitterDistance = 137;
			this.splitContainer.TabIndex = 9;
			this.groupBoxCommand.Controls.Add(this.listBoxCommand);
			this.groupBoxCommand.Dock = global::System.Windows.Forms.DockStyle.Fill;
			this.groupBoxCommand.Location = new global::System.Drawing.Point(0, 0);
			this.groupBoxCommand.Name = "groupBoxCommand";
			this.groupBoxCommand.Size = new global::System.Drawing.Size(137, 204);
			this.groupBoxCommand.TabIndex = 1;
			this.groupBoxCommand.TabStop = false;
			this.groupBoxCommand.Text = "가능한 명령들";
			this.listBoxCommand.Dock = global::System.Windows.Forms.DockStyle.Fill;
			this.listBoxCommand.ItemHeight = 12;
			this.listBoxCommand.Location = new global::System.Drawing.Point(3, 17);
			this.listBoxCommand.Name = "listBoxCommand";
			this.listBoxCommand.Size = new global::System.Drawing.Size(131, 184);
			this.listBoxCommand.TabIndex = 0;
			this.listBoxCommand.SelectedIndexChanged += new global::System.EventHandler(this.ListBoxCommand_SelectedIndexChanged);
			this.groupBoxArgList.Controls.Add(this.panelArgList);
			this.groupBoxArgList.Dock = global::System.Windows.Forms.DockStyle.Fill;
			this.groupBoxArgList.Location = new global::System.Drawing.Point(0, 0);
			this.groupBoxArgList.Name = "groupBoxArgList";
			this.groupBoxArgList.Size = new global::System.Drawing.Size(275, 204);
			this.groupBoxArgList.TabIndex = 2;
			this.groupBoxArgList.TabStop = false;
			this.groupBoxArgList.Text = "실행 옵션";
			this.panelArgList.AutoScroll = true;
			this.panelArgList.Dock = global::System.Windows.Forms.DockStyle.Fill;
			this.panelArgList.Location = new global::System.Drawing.Point(3, 17);
			this.panelArgList.Name = "panelArgList";
			this.panelArgList.Size = new global::System.Drawing.Size(269, 184);
			this.panelArgList.TabIndex = 0;
			this.AutoScaleBaseSize = new global::System.Drawing.Size(6, 14);
			base.ClientSize = new global::System.Drawing.Size(438, 281);
			base.Controls.Add(this.tabControl1);
			base.Controls.Add(this.panelButton);
			base.Controls.Add(this.labelComment);
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "StdinCommandForm";
			base.StartPosition = global::System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "사용자 정의 명령을 선택하세요.";
			this.panelButton.ResumeLayout(false);
			this.tabControl1.ResumeLayout(false);
			this.tabPage1.ResumeLayout(false);
			this.splitContainer.Panel1.ResumeLayout(false);
			this.splitContainer.Panel2.ResumeLayout(false);
			this.splitContainer.ResumeLayout(false);
			this.groupBoxCommand.ResumeLayout(false);
			this.groupBoxArgList.ResumeLayout(false);
			base.ResumeLayout(false);
		}

		private global::System.Windows.Forms.Label labelComment;

		private global::System.Windows.Forms.Panel panelButton;

		private global::System.Windows.Forms.Button buttonCancel;

		private global::System.Windows.Forms.Button buttonOK;

		private global::System.Windows.Forms.TabControl tabControl1;

		private global::System.Windows.Forms.TabPage tabPage1;

		private global::System.Windows.Forms.SplitContainer splitContainer;

		private global::System.Windows.Forms.GroupBox groupBoxCommand;

		private global::System.Windows.Forms.ListBox listBoxCommand;

		private global::System.Windows.Forms.GroupBox groupBoxArgList;

		private global::System.Windows.Forms.Panel panelArgList;

		//private global::System.ComponentModel.Container components;
	}
}
