using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using RemoteControlSystem;

namespace HeroesOpTool.RCUser.GeneralManage
{
	public partial class AuthUserManage : Form
	{
		public AuthUserManage(string modifyID, Authority maxAuthority)
		{
			this.InitializeComponent();
			this.label1.Text = LocalizeText.Get(158);
			this.label2.Text = LocalizeText.Get(159);
			this.label3.Text = LocalizeText.Get(160);
			this.label4.Text = LocalizeText.Get(161);
			this.BtnOK.Text = LocalizeText.Get(162);
			this.BtnCancel.Text = LocalizeText.Get(163);
			this.Text = LocalizeText.Get(164);
			if (modifyID != null)
			{
				this.Text = LocalizeText.Get(157);
				this.TBoxID.Text = modifyID;
				this.TBoxID.Enabled = false;
			}
			foreach (string text in Enum.GetNames(typeof(Authority)))
			{
				if (maxAuthority >= (Authority)Enum.Parse(typeof(Authority), text) && text != Authority.None.ToString() && text != Authority.Root.ToString())
				{
					this.CBoxAuthLevel.Items.Add(text);
				}
			}
		}
	}
}
