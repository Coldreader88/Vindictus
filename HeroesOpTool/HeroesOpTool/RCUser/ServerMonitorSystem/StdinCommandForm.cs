using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using RemoteControlSystem;

namespace HeroesOpTool.RCUser.ServerMonitorSystem
{
	public partial class StdinCommandForm : CommandForm
	{
		public string Command { get; private set; }

		public StdinCommandForm(string customCommandString, IEnumerable<string> serverGroup)
		{
			this.InitializeComponent();
			this.groupBoxCommand.Text = LocalizeText.Get(242);
			this.groupBoxArgList.Text = LocalizeText.Get(243);
			this.buttonOK.Text = LocalizeText.Get(244);
			this.buttonCancel.Text = LocalizeText.Get(245);
			this.tabPage1.Text = LocalizeText.Get(523);
			this.Text = LocalizeText.Get(246);
			foreach (RCProcess.CustomCommandParser command in RCProcess.CustomCommandParser.GetFromRawString(customCommandString))
			{
				this.listBoxCommand.Items.Add(new CommandForm.CommandItem(command));
			}
			this.controlArgs = new List<Control>();
			this.serverList = new List<string>(serverGroup);
		}

		private void Control_GetFocus(object sender, EventArgs e)
		{
			Control control = sender as Control;
			if (control != null)
			{
				ToolTip toolTip = new ToolTip();
				this.labelComment.Text = toolTip.GetToolTip(control);
			}
		}

		private void Control_LostFocus(object sender, EventArgs e)
		{
			this.labelComment.Text = "";
		}

		private void ListBoxCommand_SelectedIndexChanged(object sender, EventArgs e)
		{
			CommandForm.CommandItem commandItem = this.listBoxCommand.SelectedItem as CommandForm.CommandItem;
			if (commandItem == null)
			{
				return;
			}
			foreach (CommandForm.ArgumentControl argumentControl in base.SetArgPanel(this.panelArgList, commandItem.Command, this.serverList))
			{
				argumentControl.Control.GotFocus += this.Control_GetFocus;
				argumentControl.Control.LostFocus += this.Control_LostFocus;
			}
		}

		private void buttonOK_Click(object sender, EventArgs e)
		{
			CommandForm.CommandItem commandItem = this.listBoxCommand.SelectedItem as CommandForm.CommandItem;
			if (commandItem == null)
			{
				Utility.ShowErrorMessage(LocalizeText.Get(247));
				return;
			}
			this.Command = base.GetFromArgPanel(commandItem.Command);
			if (this.Command != null)
			{
				base.DialogResult = DialogResult.OK;
				base.Close();
			}
		}

		private List<string> serverList;
	}
}
