using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using RemoteControlSystem;

namespace HeroesOpTool.RCUser.ServerMonitorSystem
{
	public class ServerCommandSchedule : UserControl
	{
		public event EventHandler OnDelete;

		public RCProcessScheduler Schedule
		{
			get
			{
				return this.schedule;
			}
			private set
			{
				this.schedule = value;
				this.OnSetSchedule();
			}
		}

		public string Key { get; private set; }

		public string ScheduleName
		{
			get
			{
				return this.Schedule.Name;
			}
		}

		public ServerCommandSchedule(string key, RCProcessScheduler schedule)
		{
			this.InitializeComponent();
			this.Key = key;
			this.Schedule = schedule;
		}

		private void buttonDelete_Click(object sender, EventArgs e)
		{
			if (this.OnDelete != null)
			{
				this.OnDelete(this, e);
			}
		}

		private void OnSetSchedule()
		{
			this.label.Text = string.Format("{0}\r\n{1}", this.Schedule.ScheduleTime.ToString("yyyy-MM-dd ddd HH:mm"), this.ScheduleName);
			if (!this.Schedule.Enabled || this.Schedule.ScheduleTime < DateTime.Now)
			{
				this.label.ForeColor = Color.Gray;
			}
			this.textBox.Text = this.Schedule.Command;
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
			this.buttonDelete = new Button();
			this.label = new Label();
			this.textBox = new TextBox();
			base.SuspendLayout();
			this.buttonDelete.Anchor = AnchorStyles.Left;
			this.buttonDelete.AutoSizeMode = AutoSizeMode.GrowAndShrink;
			this.buttonDelete.FlatAppearance.BorderSize = 0;
			this.buttonDelete.FlatAppearance.MouseOverBackColor = Color.FromArgb(192, 0, 0);
			this.buttonDelete.FlatStyle = FlatStyle.Flat;
			this.buttonDelete.Location = new Point(1, 10);
			this.buttonDelete.Name = "buttonDelete";
			this.buttonDelete.Size = new Size(19, 20);
			this.buttonDelete.TabIndex = 0;
			this.buttonDelete.Text = "x";
			this.buttonDelete.TextAlign = ContentAlignment.TopCenter;
			this.buttonDelete.UseVisualStyleBackColor = true;
			this.buttonDelete.Click += this.buttonDelete_Click;
			this.label.Anchor = AnchorStyles.Left;
			this.label.AutoSize = true;
			this.label.Location = new Point(26, 7);
			this.label.Name = "label";
			this.label.Size = new Size(113, 24);
			this.label.TabIndex = 1;
			this.label.Text = "2012-07-09 금 10:10\r\nAnnouce\r\n";
			this.textBox.Anchor = (AnchorStyles.Left | AnchorStyles.Right);
			this.textBox.BackColor = Color.White;
			this.textBox.Location = new Point(271, 1);
			this.textBox.Multiline = true;
			this.textBox.Name = "textBox";
			this.textBox.ReadOnly = true;
			this.textBox.Size = new Size(248, 37);
			this.textBox.TabIndex = 2;
			base.AutoScaleDimensions = new SizeF(7f, 12f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.Controls.Add(this.textBox);
			base.Controls.Add(this.label);
			base.Controls.Add(this.buttonDelete);
			base.Name = "ServerCommandSchedule";
			base.Size = new Size(522, 38);
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		private RCProcessScheduler schedule;

		//private IContainer components;

		private Button buttonDelete;

		private Label label;

		private TextBox textBox;
	}
}
