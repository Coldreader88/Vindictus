using System;
using System.ComponentModel;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;
using Devcat.Core;
using HeroesOpTool.RCUser.ServerMonitorSystem;
using RemoteControlSystem;

namespace HeroesOpTool.RCUser
{
	public partial class Login : Form
	{
		public ServerMonitorControl ServerMonitorControl { get; private set; }

		public RCUserHandler RCUserHandler { get; private set; }

		public Login(Configuration _config, RCUserHandler rcUser, ServerMonitorControl control)
		{
			this.InitializeComponent();
			this.config = _config;
			this.RCUserHandler = rcUser;
			this.ServerMonitorControl = control;
			this.TBoxID.GotFocus += this.OnFocusIdBox;
			this.TBoxPasswd.GotFocus += this.OnFocusPasswdBox;
			this.LbVersion.Text = string.Format("Build: {0}", this.BuildDate().ToShortDateString());
		}

		private DateTime BuildDate()
		{
			DateTime result = new DateTime(2000, 1, 1);
			Version version = Assembly.GetExecutingAssembly().GetName().Version;
			result = result.AddDays((double)version.Build);
			result = result.AddSeconds((double)(version.Revision * 2));
			return result;
		}

		private void OnFocusIdBox(object sender, EventArgs e)
		{
			this.TBoxID.SelectAll();
		}

		private void OnFocusPasswdBox(object sender, EventArgs e)
		{
			this.TBoxPasswd.SelectAll();
		}

		private void ProcessLogin()
		{
			this.config.ID = this.TBoxID.Text;
			this.config.Password = Utility.GetHashedPassword(this.TBoxPasswd.Text);
			this.RCUserHandler.ConnectionResulted += this.OnConnectionResult;
			this.RCUserHandler.Start();
			base.Enabled = false;
		}

		private void BtnLogin_Click(object sender, EventArgs e)
		{
			this.ProcessLogin();
		}

		private void OnConnectionResult(object sender, EventArgs<RCUserHandler.ConnectionResult> args)
		{
			this.RCUserHandler.ConnectionResulted -= this.OnConnectionResult;
			this.UIThread(delegate
			{
				this.Enabled = true;
				switch (args.Value)
				{
				case RCUserHandler.ConnectionResult.Success:
					if (this.RCUserHandler.Authority > Authority.None)
					{
						this.ServerMonitorControl.EnableConnectionEvents();
						Utility.ShowInformationMessage(string.Format(LocalizeText.Get(185), this.RCUserHandler.Authority.ToString()));
						this.DialogResult = DialogResult.OK;
						this.Close();
						return;
					}
					this.RCUserHandler.Stop();
					Utility.ShowErrorMessage(LocalizeText.Get(186));
					return;
				case RCUserHandler.ConnectionResult.VersionMismatch:
					Utility.ShowErrorMessage(LocalizeText.Get(187));
					this.DialogResult = DialogResult.Cancel;
					this.Close();
					return;
				}
				this.RCUserHandler.Stop();
				Utility.ShowErrorMessage(LocalizeText.Get(188));
			});
		}

		private Configuration config;
	}
}
