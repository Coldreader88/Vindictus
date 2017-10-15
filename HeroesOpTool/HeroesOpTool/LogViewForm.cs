using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Devcat.Core;
using RemoteControlSystem;

namespace HeroesOpTool
{
	public partial class LogViewForm : Form
	{
		public bool ShowProcessName { get; set; }

		public override string Text
		{
			get
			{
				return base.Text;
			}
			set
			{
				base.Text = string.Format("로그보기 - {0}", value);
			}
		}

		public bool EnableInput
		{
			set
			{
				this.buttonInput.Enabled = value;
				this.TBoxInput.Enabled = value;
			}
		}

		public new Color ForeColor
		{
			get
			{
				return this.TBoxLog.EnabledForeColor;
			}
			set
			{
				this.TBoxLog.EnabledForeColor = value;
			}
		}

		public new Color BackColor
		{
			get
			{
				return this.TBoxLog.EnabledBackColor;
			}
			set
			{
				this.TBoxLog.EnabledBackColor = value;
			}
		}

		public LogGenerator LogGenerator
		{
			set
			{
				if (this.logGenerator != null)
				{
					this.logGenerator.OnLog -= this.OnLog;
					this.logGenerator.Close();
				}
				this.logGenerator = value;
				this.logGenerator.OnLog += this.OnLog;
				this.logGenerator.Open();
			}
		}

		public LogViewForm()
		{
			this.InitializeComponent();
		}

		private void OnLog(object sender, EventArgs<string> args)
		{
			if (this.ShowProcessName)
			{
				RCProcess rcprocess = sender as RCProcess;
				if (rcprocess != null)
				{
					string text = string.Format("[{0}]:\t", rcprocess.Description);
					this.TBoxLog.AddPrefix(text, Color.White, Color.Gray);
				}
			}
			this.TBoxLog.AddLog(args.Value);
			this.TBoxLog.ScrollToEnd();
		}

		protected override void OnClosed(EventArgs e)
		{
			if (this.logGenerator != null)
			{
				this.logGenerator.OnLog -= this.OnLog;
				this.logGenerator.Close();
			}
			base.OnClosed(e);
		}

		public void AddLog<T>(IEnumerable<T> value)
		{
			this.TBoxLog.Clear();
			foreach (T t in value)
			{
				string message = t.ToString();
				this.TBoxLog.AddLog(message);
			}
			this.TBoxLog.ScrollToEnd();
		}

		private LogGenerator logGenerator;
	}
}
