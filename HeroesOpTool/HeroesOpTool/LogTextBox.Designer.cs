using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace HeroesOpTool
{
	public class LogTextBox : UserControl
	{
		[DllImport("user32.dll")]
		public static extern bool LockWindowUpdate(IntPtr hWnd);

		public new bool Enabled
		{
			get
			{
				return this.enabled;
			}
			set
			{
				if (this.enabled != value)
				{
					this.enabled = value;
					if (!this.enabled)
					{
						this.TextBox.BackColor = this.DisabledBackColor;
						this.TextBox.ForeColor = this.DisabledForeColor;
						if (!string.IsNullOrEmpty(this.DisabledText))
						{
							this.TextBox.Text = this.DisabledText;
							this.TextBox.Select(0, this.DisabledText.Length);
							this.TextBox.SelectionColor = this.DisabledForeColor;
							this.TextBox.SelectionBackColor = this.DisabledBackColor;
							return;
						}
					}
					else
					{
						this.TextBox.Clear();
						this.TextBox.BackColor = this.EnabledBackColor;
						this.TextBox.ForeColor = this.EnabledForeColor;
					}
				}
			}
		}

		public string DisabledText { get; set; }

		public Color DisabledBackColor { get; set; }

		public Color DisabledForeColor { get; set; }

		public Color EnabledBackColor
		{
			get
			{
				return this.enabledBackColor;
			}
			set
			{
				this.enabledBackColor = value;
				if (this.Enabled)
				{
					this.TextBox.BackColor = this.enabledBackColor;
				}
			}
		}

		public Color EnabledForeColor
		{
			get
			{
				return this.enabledForeColor;
			}
			set
			{
				this.enabledForeColor = value;
				if (this.Enabled)
				{
					this.TextBox.ForeColor = this.enabledForeColor;
				}
			}
		}

		public LogTextBox()
		{
			this.InitializeComponent();
			this.DisabledBackColor = Color.FromKnownColor(KnownColor.Control);
			this.DisabledForeColor = Color.FromKnownColor(KnownColor.ControlText);
			this.EnabledBackColor = Color.Black;
			this.EnabledForeColor = Color.White;
			this.ParseDate = new Func<string, int>(this.DefaultDateParser);
			this.tags = new List<LogTextBox.TagSchem>();
			this.AddTag("FATAL", Color.White, Color.Red);
			this.AddTag("ERROR", Color.Red);
			this.AddTag("WARN", Color.Yellow);
			this.AddTag("INFO", Color.Green);
		}

		private int DefaultDateParser(string text)
		{
			return 17;
		}

		public void AddTag(string tagName, Color foreColor)
		{
			this.AddTag(tagName, foreColor, Color.Empty);
		}

		public void AddTag(string tagName, Color foreColor, Color backColor)
		{
			this.tags.Add(new LogTextBox.TagSchem
			{
				BeginTag = string.Format(this.TagBeginFormat, tagName),
				EndTag = string.Format(this.TagEndFormat, tagName),
				ForeColor = foreColor,
				BackColor = backColor
			});
		}

		public void Clear()
		{
			this.TextBox.Clear();
		}

		public void AddPrefix(string text, Color foreColor, Color backColor)
		{
			this.TextBox.AppendText(text);
			this.TextBox.SelectionStart = Math.Max(this.TextBox.Text.Length - text.Length, 0);
			this.TextBox.SelectionLength = text.Length;
			this.TextBox.SelectionColor = foreColor;
			this.TextBox.SelectionBackColor = backColor;
		}

		public void AddLog(string[] message)
		{
			try
			{
				LogTextBox.LockWindowUpdate(this.TextBox.Handle);
				foreach (string message2 in message)
				{
					this.AddLog(message2);
				}
			}
			finally
			{
				LogTextBox.LockWindowUpdate(IntPtr.Zero);
			}
		}

		public void AddLog(string message)
		{
			int num = this.ParseDate(message);
			string text = message.Substring(num);
			string text2 = message.Substring(0, num);
			bool flag = false;
			bool flag2 = false;
			if (this.inColor.Length == 0 && text.StartsWith("["))
			{
				foreach (LogTextBox.TagSchem tagSchem in this.tags)
				{
					if (text.StartsWith(tagSchem.BeginTag))
					{
						this.CurrentForeColor = tagSchem.ForeColor;
						this.CurrentBackColor = tagSchem.BackColor;
						text = text.Substring(tagSchem.BeginTag.Length);
						flag2 = true;
						this.inColor = tagSchem.EndTag;
						break;
					}
				}
			}
			if (this.inColor.Length > 0 && text.EndsWith(this.inColor))
			{
				flag = true;
				text = text.Substring(0, text.Length - this.inColor.Length);
				flag2 = true;
				this.inColor = string.Empty;
			}
			if (flag2)
			{
				message = message.Substring(0, num) + text;
			}
			if (this.TextBox.Text.Length > 0)
			{
				this.TextBox.Select(this.TextBox.Text.Length, 0);
			}
			this.TextBox.SelectionColor = Color.Gray;
			this.TextBox.SelectionBackColor = this.EnabledBackColor;
			this.TextBox.AppendText(text2);
			this.TextBox.SelectionColor = ((this.CurrentForeColor == Color.Empty) ? this.TextBox.ForeColor : this.CurrentForeColor);
			this.TextBox.SelectionBackColor = ((this.CurrentBackColor == Color.Empty) ? this.TextBox.BackColor : this.CurrentBackColor);
			this.TextBox.AppendText(text + Environment.NewLine);
			if (flag)
			{
				this.CurrentForeColor = Color.Empty;
				this.CurrentBackColor = Color.Empty;
			}
		}

		public void ScrollToEnd()
		{
			if (this.TextBox.Text.Length > 0)
			{
				this.TextBox.Select(this.TextBox.Text.Length - 1, 0);
				this.TextBox.ScrollToCaret();
			}
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
			this.TextBox = new RichTextBox();
			base.SuspendLayout();
			this.TextBox.BackColor = Color.Black;
			this.TextBox.Dock = DockStyle.Fill;
			this.TextBox.ForeColor = Color.White;
			this.TextBox.Location = new Point(0, 0);
			this.TextBox.Name = "TextBox";
			this.TextBox.ReadOnly = true;
			this.TextBox.ScrollBars = RichTextBoxScrollBars.ForcedVertical;
			this.TextBox.Size = new Size(150, 150);
			this.TextBox.TabIndex = 0;
			this.TextBox.Text = "";
			base.AutoScaleDimensions = new SizeF(7f, 12f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.Controls.Add(this.TextBox);
			base.Name = "LogTextBox";
			base.ResumeLayout(false);
		}

		public Func<string, int> ParseDate;

		private readonly string TagBeginFormat = "[{0}]";

		private readonly string TagEndFormat = "[/{0}]";

		private string inColor = string.Empty;

		private bool enabled = true;

		private Color enabledBackColor;

		private Color enabledForeColor;

		private Color CurrentForeColor = Color.Empty;

		private Color CurrentBackColor = Color.Empty;

		private List<LogTextBox.TagSchem> tags;

		private IContainer components = null;

		private RichTextBox TextBox;

		private struct TagSchem
		{
			public string BeginTag;

			public string EndTag;

			public Color ForeColor;

			public Color BackColor;
		}
	}
}
