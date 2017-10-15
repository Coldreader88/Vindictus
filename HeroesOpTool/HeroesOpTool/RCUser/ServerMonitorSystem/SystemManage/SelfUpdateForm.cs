using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace HeroesOpTool.RCUser.ServerMonitorSystem.SystemManage
{
	public partial class SelfUpdateForm : Form
	{
		public SelfUpdateForm()
		{
			this.InitializeComponent();
			this.label2.Text = LocalizeText.Get(351);
			this.label3.Text = LocalizeText.Get(352);
			this.label4.Text = LocalizeText.Get(353);
			this.label5.Text = LocalizeText.Get(354);
			this.buttonOK.Text = LocalizeText.Get(355);
			this.buttonCancel.Text = LocalizeText.Get(356);
			this.Text = LocalizeText.Get(357);
		}

		public string Argument
		{
			get
			{
				return this._arg;
			}
		}

		private void buttonOK_Click(object sender, EventArgs e)
		{
			this._arg = string.Join(" ", new string[]
			{
				this.textBoxAddress.Text,
				this.textBoxPort.Text,
				this.textBoxAccount.Text,
				this.textBoxPassword.Text,
				this.textBoxSourceFolder.Text,
				this.textBoxSourceFiles.Text
			});
		}

		private string _arg;
	}
}
