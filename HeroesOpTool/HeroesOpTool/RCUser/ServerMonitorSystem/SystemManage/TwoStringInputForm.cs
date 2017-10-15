using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace HeroesOpTool.RCUser.ServerMonitorSystem.SystemManage
{
	public partial class TwoStringInputForm : Form
	{
		private TwoStringInputForm()
		{
			this.InitializeComponent();
			this.buttonOK.Text = LocalizeText.Get(358);
			this.buttonCancel.Text = LocalizeText.Get(359);
		}

		public TwoStringInputForm(string title, string message, string text1, string text2) : this()
		{
			this.Text = title;
			this.labelInfo.Text = message;
			this.label1.Text = text1;
			this.label2.Text = text2;
		}

		public TwoStringInputForm(string title, string message, string text1, string text2, string default1, string default2) : this(title, message, text1, text2)
		{
			this.textBox1.Text = default1;
			this.textBox2.Text = default2;
		}

		public string Value1
		{
			get
			{
				return this._value1;
			}
		}

		public string Value2
		{
			get
			{
				return this._value2;
			}
		}

		private void buttonOK_Click(object sender, EventArgs e)
		{
			this._value1 = this.textBox1.Text;
			this._value2 = this.textBox2.Text;
		}

		private string _value1;

		private string _value2;
	}
}
