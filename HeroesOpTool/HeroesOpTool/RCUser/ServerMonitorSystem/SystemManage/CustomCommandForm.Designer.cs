namespace HeroesOpTool.RCUser.ServerMonitorSystem.SystemManage
{
	public partial class CustomCommandForm : global::System.Windows.Forms.Form
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
			this.groupBoxName = new global::System.Windows.Forms.GroupBox();
			this.textBoxName = new global::System.Windows.Forms.TextBox();
			this.textBoxCommand = new global::System.Windows.Forms.TextBox();
			this.groupBoxCommand = new global::System.Windows.Forms.GroupBox();
			this.groupBoxArg = new global::System.Windows.Forms.GroupBox();
			this.buttoArgDel = new global::System.Windows.Forms.Button();
			this.buttonArgAdd = new global::System.Windows.Forms.Button();
			this.comboBoxType = new global::System.Windows.Forms.ComboBox();
			this.labelArgType = new global::System.Windows.Forms.Label();
			this.labelArgName = new global::System.Windows.Forms.Label();
			this.textBoxArgName = new global::System.Windows.Forms.TextBox();
			this.groupBoxArgList = new global::System.Windows.Forms.GroupBox();
			this.textBoxDesc = new global::System.Windows.Forms.TextBox();
			this.listViewArg = new global::System.Windows.Forms.ListView();
			this.labelDesc = new global::System.Windows.Forms.Label();
			this.textBoxRawCommand = new global::System.Windows.Forms.TextBox();
			this.buttonOK = new global::System.Windows.Forms.Button();
			this.buttonCancel = new global::System.Windows.Forms.Button();
			global::System.Windows.Forms.ColumnHeader columnHeader = new global::System.Windows.Forms.ColumnHeader();
			global::System.Windows.Forms.ColumnHeader columnHeader2 = new global::System.Windows.Forms.ColumnHeader();
			this.groupBoxName.SuspendLayout();
			this.groupBoxCommand.SuspendLayout();
			this.groupBoxArg.SuspendLayout();
			this.groupBoxArgList.SuspendLayout();
			base.SuspendLayout();
			columnHeader.Width = 97;
			columnHeader2.Width = 67;
			this.groupBoxName.Controls.Add(this.textBoxName);
			this.groupBoxName.Location = new global::System.Drawing.Point(12, 12);
			this.groupBoxName.Name = "groupBoxName";
			this.groupBoxName.Size = new global::System.Drawing.Size(174, 45);
			this.groupBoxName.TabIndex = 1;
			this.groupBoxName.TabStop = false;
			this.groupBoxName.Text = "명령 이름";
			this.textBoxName.Anchor = (global::System.Windows.Forms.AnchorStyles.Top | global::System.Windows.Forms.AnchorStyles.Left | global::System.Windows.Forms.AnchorStyles.Right);
			this.textBoxName.Location = new global::System.Drawing.Point(6, 18);
			this.textBoxName.Name = "textBoxName";
			this.textBoxName.Size = new global::System.Drawing.Size(162, 21);
			this.textBoxName.TabIndex = 0;
			this.textBoxName.TextChanged += new global::System.EventHandler(this.textBoxName_TextChanged);
			this.textBoxCommand.Anchor = (global::System.Windows.Forms.AnchorStyles.Top | global::System.Windows.Forms.AnchorStyles.Left | global::System.Windows.Forms.AnchorStyles.Right);
			this.textBoxCommand.Location = new global::System.Drawing.Point(6, 18);
			this.textBoxCommand.Name = "textBoxCommand";
			this.textBoxCommand.Size = new global::System.Drawing.Size(162, 21);
			this.textBoxCommand.TabIndex = 0;
			this.textBoxCommand.TextChanged += new global::System.EventHandler(this.textBoxCommand_TextChanged);
			this.groupBoxCommand.Controls.Add(this.textBoxCommand);
			this.groupBoxCommand.Location = new global::System.Drawing.Point(12, 63);
			this.groupBoxCommand.Name = "groupBoxCommand";
			this.groupBoxCommand.Size = new global::System.Drawing.Size(174, 45);
			this.groupBoxCommand.TabIndex = 2;
			this.groupBoxCommand.TabStop = false;
			this.groupBoxCommand.Text = "명령";
			this.groupBoxArg.Controls.Add(this.buttoArgDel);
			this.groupBoxArg.Controls.Add(this.buttonArgAdd);
			this.groupBoxArg.Controls.Add(this.comboBoxType);
			this.groupBoxArg.Controls.Add(this.labelArgType);
			this.groupBoxArg.Controls.Add(this.labelArgName);
			this.groupBoxArg.Controls.Add(this.textBoxArgName);
			this.groupBoxArg.Location = new global::System.Drawing.Point(12, 114);
			this.groupBoxArg.Name = "groupBoxArg";
			this.groupBoxArg.Size = new global::System.Drawing.Size(174, 104);
			this.groupBoxArg.TabIndex = 3;
			this.groupBoxArg.TabStop = false;
			this.groupBoxArg.Text = "인자";
			this.buttoArgDel.Location = new global::System.Drawing.Point(144, 72);
			this.buttoArgDel.Name = "buttoArgDel";
			this.buttoArgDel.Size = new global::System.Drawing.Size(24, 24);
			this.buttoArgDel.TabIndex = 5;
			this.buttoArgDel.Text = "－";
			this.buttoArgDel.Click += new global::System.EventHandler(this.buttoArgDel_Click);
			this.buttonArgAdd.Location = new global::System.Drawing.Point(114, 72);
			this.buttonArgAdd.Name = "buttonArgAdd";
			this.buttonArgAdd.Size = new global::System.Drawing.Size(24, 24);
			this.buttonArgAdd.TabIndex = 4;
			this.buttonArgAdd.Text = "＋";
			this.buttonArgAdd.Click += new global::System.EventHandler(this.buttonArgAdd_Click);
			this.comboBoxType.DropDownStyle = global::System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBoxType.FormattingEnabled = true;
			this.comboBoxType.Location = new global::System.Drawing.Point(41, 46);
			this.comboBoxType.Name = "comboBoxType";
			this.comboBoxType.Size = new global::System.Drawing.Size(127, 20);
			this.comboBoxType.TabIndex = 3;
			this.comboBoxType.SelectedIndexChanged += new global::System.EventHandler(this.comboBoxType_SelectedIndexChanged);
			this.labelArgType.AutoSize = true;
			this.labelArgType.Location = new global::System.Drawing.Point(6, 48);
			this.labelArgType.Name = "labelArgType";
			this.labelArgType.Size = new global::System.Drawing.Size(29, 12);
			this.labelArgType.TabIndex = 2;
			this.labelArgType.Text = "타입";
			this.labelArgName.AutoSize = true;
			this.labelArgName.Location = new global::System.Drawing.Point(6, 21);
			this.labelArgName.Name = "labelArgName";
			this.labelArgName.Size = new global::System.Drawing.Size(29, 12);
			this.labelArgName.TabIndex = 0;
			this.labelArgName.Text = "이름";
			this.textBoxArgName.Anchor = (global::System.Windows.Forms.AnchorStyles.Top | global::System.Windows.Forms.AnchorStyles.Left | global::System.Windows.Forms.AnchorStyles.Right);
			this.textBoxArgName.Location = new global::System.Drawing.Point(41, 18);
			this.textBoxArgName.Name = "textBoxArgName";
			this.textBoxArgName.Size = new global::System.Drawing.Size(127, 21);
			this.textBoxArgName.TabIndex = 1;
			this.textBoxArgName.TextChanged += new global::System.EventHandler(this.textBoxArgName_TextChanged);
			this.groupBoxArgList.Anchor = (global::System.Windows.Forms.AnchorStyles.Top | global::System.Windows.Forms.AnchorStyles.Bottom | global::System.Windows.Forms.AnchorStyles.Left | global::System.Windows.Forms.AnchorStyles.Right);
			this.groupBoxArgList.Controls.Add(this.textBoxDesc);
			this.groupBoxArgList.Controls.Add(this.listViewArg);
			this.groupBoxArgList.Controls.Add(this.labelDesc);
			this.groupBoxArgList.Location = new global::System.Drawing.Point(193, 13);
			this.groupBoxArgList.Name = "groupBoxArgList";
			this.groupBoxArgList.Size = new global::System.Drawing.Size(187, 205);
			this.groupBoxArgList.TabIndex = 4;
			this.groupBoxArgList.TabStop = false;
			this.groupBoxArgList.Text = "인자 리스트";
			this.textBoxDesc.Anchor = (global::System.Windows.Forms.AnchorStyles.Bottom | global::System.Windows.Forms.AnchorStyles.Left | global::System.Windows.Forms.AnchorStyles.Right);
			this.textBoxDesc.Location = new global::System.Drawing.Point(7, 176);
			this.textBoxDesc.Name = "textBoxDesc";
			this.textBoxDesc.Size = new global::System.Drawing.Size(174, 21);
			this.textBoxDesc.TabIndex = 2;
			this.textBoxDesc.TextChanged += new global::System.EventHandler(this.textBoxDesc_TextChanged);
			this.listViewArg.Anchor = (global::System.Windows.Forms.AnchorStyles.Top | global::System.Windows.Forms.AnchorStyles.Bottom | global::System.Windows.Forms.AnchorStyles.Left | global::System.Windows.Forms.AnchorStyles.Right);
			this.listViewArg.Columns.AddRange(new global::System.Windows.Forms.ColumnHeader[]
			{
				columnHeader,
				columnHeader2
			});
			this.listViewArg.FullRowSelect = true;
			this.listViewArg.GridLines = true;
			this.listViewArg.HeaderStyle = global::System.Windows.Forms.ColumnHeaderStyle.None;
			this.listViewArg.HideSelection = false;
			this.listViewArg.Location = new global::System.Drawing.Point(7, 17);
			this.listViewArg.Name = "listViewArg";
			this.listViewArg.ShowGroups = false;
			this.listViewArg.Size = new global::System.Drawing.Size(174, 138);
			this.listViewArg.TabIndex = 0;
			this.listViewArg.UseCompatibleStateImageBehavior = false;
			this.listViewArg.View = global::System.Windows.Forms.View.Details;
			this.listViewArg.SelectedIndexChanged += new global::System.EventHandler(this.listViewArg_SelectedIndexChanged);
			this.labelDesc.Anchor = (global::System.Windows.Forms.AnchorStyles.Bottom | global::System.Windows.Forms.AnchorStyles.Left);
			this.labelDesc.AutoSize = true;
			this.labelDesc.Location = new global::System.Drawing.Point(6, 161);
			this.labelDesc.Name = "labelDesc";
			this.labelDesc.Size = new global::System.Drawing.Size(29, 12);
			this.labelDesc.TabIndex = 1;
			this.labelDesc.Text = "설명";
			this.textBoxRawCommand.Anchor = (global::System.Windows.Forms.AnchorStyles.Bottom | global::System.Windows.Forms.AnchorStyles.Left | global::System.Windows.Forms.AnchorStyles.Right);
			this.textBoxRawCommand.Location = new global::System.Drawing.Point(12, 224);
			this.textBoxRawCommand.Name = "textBoxRawCommand";
			this.textBoxRawCommand.Size = new global::System.Drawing.Size(368, 21);
			this.textBoxRawCommand.TabIndex = 0;
			this.textBoxRawCommand.TextChanged += new global::System.EventHandler(this.textBoxRawCommand_TextChanged);
			this.buttonOK.Anchor = (global::System.Windows.Forms.AnchorStyles.Bottom | global::System.Windows.Forms.AnchorStyles.Right);
			this.buttonOK.DialogResult = global::System.Windows.Forms.DialogResult.OK;
			this.buttonOK.Location = new global::System.Drawing.Point(166, 252);
			this.buttonOK.Name = "buttonOK";
			this.buttonOK.Size = new global::System.Drawing.Size(104, 24);
			this.buttonOK.TabIndex = 5;
			this.buttonOK.Text = "확인";
			this.buttonCancel.Anchor = (global::System.Windows.Forms.AnchorStyles.Bottom | global::System.Windows.Forms.AnchorStyles.Right);
			this.buttonCancel.DialogResult = global::System.Windows.Forms.DialogResult.Cancel;
			this.buttonCancel.Location = new global::System.Drawing.Point(276, 252);
			this.buttonCancel.Name = "buttonCancel";
			this.buttonCancel.Size = new global::System.Drawing.Size(104, 24);
			this.buttonCancel.TabIndex = 6;
			this.buttonCancel.Text = "취소";
			base.AcceptButton = this.buttonOK;
			base.AutoScaleDimensions = new global::System.Drawing.SizeF(7f, 12f);
			base.AutoScaleMode = global::System.Windows.Forms.AutoScaleMode.Font;
			base.CancelButton = this.buttonCancel;
			base.ClientSize = new global::System.Drawing.Size(392, 288);
			base.Controls.Add(this.buttonOK);
			base.Controls.Add(this.textBoxRawCommand);
			base.Controls.Add(this.buttonCancel);
			base.Controls.Add(this.groupBoxArgList);
			base.Controls.Add(this.groupBoxArg);
			base.Controls.Add(this.groupBoxCommand);
			base.Controls.Add(this.groupBoxName);
			this.Name = "CustomCommandForm";
			base.StartPosition = global::System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "사용자 정의 명령";
			base.FormClosing += new global::System.Windows.Forms.FormClosingEventHandler(this.CustomCommandForm_FormClosing);
			this.groupBoxName.ResumeLayout(false);
			this.groupBoxName.PerformLayout();
			this.groupBoxCommand.ResumeLayout(false);
			this.groupBoxCommand.PerformLayout();
			this.groupBoxArg.ResumeLayout(false);
			this.groupBoxArg.PerformLayout();
			this.groupBoxArgList.ResumeLayout(false);
			this.groupBoxArgList.PerformLayout();
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		//private global::System.ComponentModel.IContainer components;

		private global::System.Windows.Forms.GroupBox groupBoxName;

		private global::System.Windows.Forms.TextBox textBoxName;

		private global::System.Windows.Forms.TextBox textBoxCommand;

		private global::System.Windows.Forms.GroupBox groupBoxCommand;

		private global::System.Windows.Forms.GroupBox groupBoxArg;

		private global::System.Windows.Forms.TextBox textBoxArgName;

		private global::System.Windows.Forms.GroupBox groupBoxArgList;

		private global::System.Windows.Forms.TextBox textBoxRawCommand;

		private global::System.Windows.Forms.Button buttonOK;

		private global::System.Windows.Forms.Button buttonCancel;

		private global::System.Windows.Forms.ComboBox comboBoxType;

		private global::System.Windows.Forms.Label labelDesc;

		private global::System.Windows.Forms.Label labelArgType;

		private global::System.Windows.Forms.Label labelArgName;

		private global::System.Windows.Forms.Button buttoArgDel;

		private global::System.Windows.Forms.Button buttonArgAdd;

		private global::System.Windows.Forms.TextBox textBoxDesc;

		private global::System.Windows.Forms.ListView listViewArg;
	}
}
