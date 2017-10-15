using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace HeroesOpTool.RCUser.GeneralManage
{
	public partial class PersonalInfo : Form
	{
		public PersonalInfo(string ID, string Authority)
		{
			this.InitializeComponent();
			this.label1.Text = LocalizeText.Get(165);
			this.label2.Text = LocalizeText.Get(166);
			this.label3.Text = LocalizeText.Get(167);
			this.label4.Text = LocalizeText.Get(168);
			this.label5.Text = LocalizeText.Get(169);
			this.BtnOK.Text = LocalizeText.Get(170);
			this.BtnCancel.Text = LocalizeText.Get(171);
			this.Text = LocalizeText.Get(172);
			this.LabelID.Text = ID;
			this.LabelAuth.Text = Authority;
		}

		private void TBoxOldPassword_TextChanged(object sender, EventArgs e)
		{
			if (this.TBoxOldPassword.Text.Length > 0)
			{
				this.TBoxNewPassword.Enabled = true;
				this.TBoxRePassword.Enabled = true;
				return;
			}
			this.TBoxNewPassword.Enabled = false;
			this.TBoxRePassword.Enabled = false;
		}
	}
}
