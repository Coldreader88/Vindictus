using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using RemoteControlSystem;

namespace HeroesOpTool.RCUser.GeneralManage
{
	public partial class UserView : Form
	{
		public UserView(RCUserHandler rcUser, IEnumerable<Member> memberList)
		{
			this.InitializeComponent();
			this.UserId.Text = LocalizeText.Get(173);
			this.UserAuthority.Text = LocalizeText.Get(174);
			this.BtnAddUser.Text = LocalizeText.Get(175);
			this.BtnRemoveUser.Text = LocalizeText.Get(176);
			this.BtnChangeUser.Text = LocalizeText.Get(177);
			this.BtnOK.Text = LocalizeText.Get(178);
			this.Text = LocalizeText.Get(179);
			this._rcUser = rcUser;
			foreach (Member member in memberList)
			{
				this.AddMember(member.ID, member.Authority);
			}
		}

		private void AddMember(string id, Authority authority)
		{
			int i;
			for (i = 0; i < this.LViewUsers.Items.Count; i++)
			{
				Authority authority2 = (Authority)Enum.Parse(typeof(Authority), this.LViewUsers.Items[i].SubItems[1].Text);
				if (authority > authority2 || (authority == authority2 && id.CompareTo(this.LViewUsers.Items[i].SubItems[0].Text) < 0))
				{
					break;
				}
			}
			this.LViewUsers.Items.Insert(i, new ListViewItem(new string[]
			{
				id,
				authority.ToString()
			}));
		}

		private void BtnAddUser_Click(object sender, EventArgs e)
		{
			AuthUserManage authUserManage = new AuthUserManage(null, this._rcUser.Authority);
			while (authUserManage.ShowDialog() == DialogResult.OK)
			{
				if (!(authUserManage.TBoxPassword.Text != authUserManage.TBoxRePassword.Text))
				{
					string text = authUserManage.TBoxID.Text;
					Authority authority = (Authority)Enum.Parse(typeof(Authority), authUserManage.CBoxAuthLevel.Text);
					foreach (object obj in this.LViewUsers.Items)
					{
						ListViewItem listViewItem = (ListViewItem)obj;
						if (listViewItem.SubItems[0].Text == text)
						{
							Utility.ShowErrorMessage(LocalizeText.Get(181));
							return;
						}
					}
					this._rcUser.RegisterUser(text, Utility.GetHashedPassword(authUserManage.TBoxPassword.Text), authority);
					this.AddMember(text, authority);
					break;
				}
				Utility.ShowErrorMessage(LocalizeText.Get(180));
			}
		}

		private void BtnRemoveUser_Click(object sender, EventArgs e)
		{
			string text = this.LViewUsers.SelectedItems[0].SubItems[0].Text;
			if (Utility.InputYesNoFromWarning(string.Format(LocalizeText.Get(182), text)))
			{
				this._rcUser.RemoveUser(text);
				this.LViewUsers.Items.RemoveAt(this.LViewUsers.SelectedIndices[0]);
			}
		}

		private void BtnChangeUser_Click(object sender, EventArgs e)
		{
			string text = this.LViewUsers.SelectedItems[0].SubItems[0].Text;
			Authority authority = (Authority)Enum.Parse(typeof(Authority), this.LViewUsers.SelectedItems[0].SubItems[1].Text);
			if (text == this._rcUser.ID)
			{
				Utility.ShowErrorMessage(LocalizeText.Get(183));
				return;
			}
			AuthUserManage authUserManage = new AuthUserManage(text, this._rcUser.Authority);
			while (authUserManage.ShowDialog() == DialogResult.OK)
			{
				if (authUserManage.TBoxPassword.Text != authUserManage.TBoxRePassword.Text)
				{
					Utility.ShowErrorMessage(LocalizeText.Get(184));
				}
				else
				{
					if (authUserManage.TBoxPassword.Text.Length > 0)
					{
						this._rcUser.ChangeOtherPassword(text, Utility.GetHashedPassword(authUserManage.TBoxPassword.Text));
					}
					if (authUserManage.CBoxAuthLevel.SelectedIndex != -1)
					{
						Authority authority2 = (Authority)Enum.Parse(typeof(Authority), authUserManage.CBoxAuthLevel.Text);
						this._rcUser.ModifyUser(text, authority2);
						this.LViewUsers.Items.RemoveAt(this.LViewUsers.SelectedIndices[0]);
						this.AddMember(text, authority2);
						break;
					}
					break;
				}
			}
		}

		private void BtnOK_Click(object sender, EventArgs e)
		{
			base.Close();
		}

		private void LViewUsers_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (this.LViewUsers.SelectedItems.Count == 0)
			{
				this.BtnChangeUser.Enabled = false;
				this.BtnRemoveUser.Enabled = false;
				return;
			}
			Authority authority = (Authority)Enum.Parse(typeof(Authority), this.LViewUsers.SelectedItems[0].SubItems[1].Text);
			if (authority >= this._rcUser.Authority)
			{
				this.BtnChangeUser.Enabled = false;
				this.BtnRemoveUser.Enabled = false;
				return;
			}
			this.BtnChangeUser.Enabled = true;
			this.BtnRemoveUser.Enabled = true;
		}

		private void LViewUsers_DoubleClick(object sender, EventArgs e)
		{
			if (this.LViewUsers.SelectedItems.Count > 0)
			{
				this.BtnChangeUser_Click(sender, e);
			}
		}

		private RCUserHandler _rcUser;
	}
}
