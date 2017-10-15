namespace HeroesOpTool.UserMonitorSystem
{
	public partial class UserAlarmForm : global::System.Windows.Forms.Form
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
			global::System.ComponentModel.ComponentResourceManager componentResourceManager = new global::System.ComponentModel.ComponentResourceManager(typeof(global::HeroesOpTool.UserMonitorSystem.UserAlarmForm));
			this.ListLog = new global::System.Windows.Forms.ListView();
			this.clmnTime = new global::System.Windows.Forms.ColumnHeader();
			this.clmnDesc = new global::System.Windows.Forms.ColumnHeader();
			this.BtnOK = new global::System.Windows.Forms.Button();
			this.ChkSupress = new global::System.Windows.Forms.CheckBox();
			this.ComboMinute = new global::System.Windows.Forms.ComboBox();
			this.LabelDesc = new global::System.Windows.Forms.Label();
			this.EmergencyView = new global::System.Windows.Forms.ListView();
			this.Department = new global::System.Windows.Forms.ColumnHeader();
			this.Id = new global::System.Windows.Forms.ColumnHeader();
			this.CallName = new global::System.Windows.Forms.ColumnHeader();
			this.PhoneNumber = new global::System.Windows.Forms.ColumnHeader();
			this.Mail = new global::System.Windows.Forms.ColumnHeader();
			this.Rank = new global::System.Windows.Forms.ColumnHeader();
			this.EmergencyCallLabel = new global::System.Windows.Forms.Label();
			base.SuspendLayout();
			this.ListLog.Anchor = (global::System.Windows.Forms.AnchorStyles.Top | global::System.Windows.Forms.AnchorStyles.Bottom | global::System.Windows.Forms.AnchorStyles.Left | global::System.Windows.Forms.AnchorStyles.Right);
			this.ListLog.Columns.AddRange(new global::System.Windows.Forms.ColumnHeader[]
			{
				this.clmnTime,
				this.clmnDesc
			});
			this.ListLog.Location = new global::System.Drawing.Point(33, 37);
			this.ListLog.Name = "ListLog";
			this.ListLog.Size = new global::System.Drawing.Size(612, 282);
			this.ListLog.TabIndex = 0;
			this.ListLog.UseCompatibleStateImageBehavior = false;
			this.ListLog.View = global::System.Windows.Forms.View.Details;
			this.clmnTime.Width = 103;
			this.clmnDesc.Width = 505;
			this.BtnOK.Anchor = (global::System.Windows.Forms.AnchorStyles.Bottom | global::System.Windows.Forms.AnchorStyles.Right);
			this.BtnOK.Location = new global::System.Drawing.Point(602, 598);
			this.BtnOK.Name = "BtnOK";
			this.BtnOK.Size = new global::System.Drawing.Size(75, 23);
			this.BtnOK.TabIndex = 1;
			this.BtnOK.Text = "button1";
			this.BtnOK.UseVisualStyleBackColor = true;
			this.BtnOK.Click += new global::System.EventHandler(this.BtnOK_Click);
			this.ChkSupress.Anchor = (global::System.Windows.Forms.AnchorStyles.Bottom | global::System.Windows.Forms.AnchorStyles.Left);
			this.ChkSupress.AutoSize = true;
			this.ChkSupress.Location = new global::System.Drawing.Point(12, 605);
			this.ChkSupress.Name = "ChkSupress";
			this.ChkSupress.Size = new global::System.Drawing.Size(15, 14);
			this.ChkSupress.TabIndex = 2;
			this.ChkSupress.UseVisualStyleBackColor = true;
			this.ComboMinute.Anchor = (global::System.Windows.Forms.AnchorStyles.Bottom | global::System.Windows.Forms.AnchorStyles.Left);
			this.ComboMinute.DropDownStyle = global::System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.ComboMinute.FormattingEnabled = true;
			this.ComboMinute.Location = new global::System.Drawing.Point(33, 600);
			this.ComboMinute.Name = "ComboMinute";
			this.ComboMinute.Size = new global::System.Drawing.Size(55, 20);
			this.ComboMinute.TabIndex = 3;
			this.LabelDesc.Anchor = (global::System.Windows.Forms.AnchorStyles.Bottom | global::System.Windows.Forms.AnchorStyles.Left);
			this.LabelDesc.AutoSize = true;
			this.LabelDesc.Location = new global::System.Drawing.Point(94, 603);
			this.LabelDesc.Name = "LabelDesc";
			this.LabelDesc.Size = new global::System.Drawing.Size(53, 12);
			this.LabelDesc.TabIndex = 4;
			this.LabelDesc.Text = "알람끄기";
			this.EmergencyView.Columns.AddRange(new global::System.Windows.Forms.ColumnHeader[]
			{
				this.Department,
				this.Id,
				this.CallName,
				this.PhoneNumber,
				this.Mail,
				this.Rank
			});
			this.EmergencyView.Location = new global::System.Drawing.Point(33, 397);
			this.EmergencyView.Name = "EmergencyView";
			this.EmergencyView.Size = new global::System.Drawing.Size(612, 197);
			this.EmergencyView.TabIndex = 5;
			this.EmergencyView.UseCompatibleStateImageBehavior = false;
			this.EmergencyView.View = global::System.Windows.Forms.View.Details;
			this.Department.Width = 91;
			this.Id.Width = 74;
			this.CallName.Width = 71;
			this.PhoneNumber.Width = 137;
			this.Mail.Width = 148;
			this.Rank.Width = 85;
			this.EmergencyCallLabel.AutoSize = true;
			this.EmergencyCallLabel.Font = new global::System.Drawing.Font("굴림", 18f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 129);
			this.EmergencyCallLabel.Location = new global::System.Drawing.Point(36, 361);
			this.EmergencyCallLabel.Name = "EmergencyCallLabel";
			this.EmergencyCallLabel.Size = new global::System.Drawing.Size(130, 24);
			this.EmergencyCallLabel.TabIndex = 6;
			this.EmergencyCallLabel.Text = "비상연락망";
			base.AcceptButton = this.BtnOK;
			base.AutoScaleDimensions = new global::System.Drawing.SizeF(7f, 12f);
			base.AutoScaleMode = global::System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = global::System.Drawing.Color.DarkRed;
			base.ClientSize = new global::System.Drawing.Size(689, 630);
			base.Controls.Add(this.EmergencyCallLabel);
			base.Controls.Add(this.EmergencyView);
			base.Controls.Add(this.LabelDesc);
			base.Controls.Add(this.ComboMinute);
			base.Controls.Add(this.ChkSupress);
			base.Controls.Add(this.BtnOK);
			base.Controls.Add(this.ListLog);
			base.FormBorderStyle = global::System.Windows.Forms.FormBorderStyle.FixedSingle;
			base.Icon = (global::System.Drawing.Icon)componentResourceManager.GetObject("$this.Icon");
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "UserAlarmForm";
			base.SizeGripStyle = global::System.Windows.Forms.SizeGripStyle.Show;
			this.Text = "경고 창";
			base.TopMost = true;
			base.FormClosing += new global::System.Windows.Forms.FormClosingEventHandler(this.UserAlarmForm_FormClosing);
			base.VisibleChanged += new global::System.EventHandler(this.UserAlarmForm_VisibleChanged);
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		//private global::System.ComponentModel.IContainer components;

		private global::System.Windows.Forms.ListView ListLog;

		private global::System.Windows.Forms.Button BtnOK;

		private global::System.Windows.Forms.CheckBox ChkSupress;

		private global::System.Windows.Forms.ComboBox ComboMinute;

		private global::System.Windows.Forms.Label LabelDesc;

		private global::System.Windows.Forms.ColumnHeader clmnTime;

		private global::System.Windows.Forms.ColumnHeader clmnDesc;

		private global::System.Windows.Forms.ListView EmergencyView;

		private global::System.Windows.Forms.ColumnHeader Department;

		private global::System.Windows.Forms.ColumnHeader Id;

		private global::System.Windows.Forms.ColumnHeader CallName;

		private global::System.Windows.Forms.ColumnHeader PhoneNumber;

		private global::System.Windows.Forms.ColumnHeader Mail;

		private global::System.Windows.Forms.ColumnHeader Rank;

		private global::System.Windows.Forms.Label EmergencyCallLabel;
	}
}
