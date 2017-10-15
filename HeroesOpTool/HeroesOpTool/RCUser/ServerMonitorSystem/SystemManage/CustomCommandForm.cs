using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using RemoteControlSystem;

namespace HeroesOpTool.RCUser.ServerMonitorSystem.SystemManage
{
	public partial class CustomCommandForm : Form
	{
		public new string Name { get; private set; }

		public string Command { get; private set; }

		public CustomCommandForm()
		{
			this.InitializeComponent();
			this.groupBoxName.Text = LocalizeText.Get(289);
			this.groupBoxCommand.Text = LocalizeText.Get(434);
			this.groupBoxArg.Text = LocalizeText.Get(509);
			this.labelArgType.Text = LocalizeText.Get(442);
			this.labelArgName.Text = LocalizeText.Get(437);
			this.groupBoxArgList.Text = LocalizeText.Get(510);
			this.labelDesc.Text = LocalizeText.Get(493);
			this.buttonOK.Text = LocalizeText.Get(5);
			this.buttonCancel.Text = LocalizeText.Get(6);
			this.Text = LocalizeText.Get(511);
			foreach (string item in RCProcess.CustomCommandParser.DataTypes)
			{
				this.comboBoxType.Items.Add(item);
			}
		}

		public CustomCommandForm(string name, string command) : this()
		{
			RCProcess.CustomCommandParser customCommandParser = new RCProcess.CustomCommandParser(name, command);
			this.UpdateUI(customCommandParser);
			this.textBoxRawCommand.Text = string.Format("{0}|{1}", customCommandParser.Name, customCommandParser.RawCommand);
		}

		private void UpdateUI(RCProcess.CustomCommandParser cmdParser)
		{
			this.textBoxName.Text = cmdParser.Name;
			this.textBoxCommand.Text = cmdParser.Command;
			this.listViewArg.Items.Clear();
			foreach (RCProcess.CustomCommandParser.CommandArg commandArg in cmdParser.Arguments)
			{
				ListViewItem listViewItem = this.listViewArg.Items.Add(new ListViewItem(new string[]
				{
					commandArg.Name,
					(string)this.comboBoxType.Items[(int)commandArg.Type]
				}));
				listViewItem.Tag = commandArg.Comment;
			}
		}

		private void UpdateRawCommand()
		{
			if (!this.textBoxRawCommand.Focused)
			{
				string text = string.Format("{0}|{1}", this.textBoxName.Text, this.textBoxCommand.Text);
				if (this.listViewArg.Items.Count > 0)
				{
					StringBuilder stringBuilder = new StringBuilder();
					foreach (object obj in this.listViewArg.Items)
					{
						ListViewItem listViewItem = (ListViewItem)obj;
						int type = this.comboBoxType.FindString(listViewItem.SubItems[1].Text);
						string rawString = RCProcess.CustomCommandParser.CommandArg.GetRawString(listViewItem.SubItems[0].Text, (RCProcess.CustomCommandParser.DataType)type, (string)listViewItem.Tag);
						stringBuilder.AppendFormat(" {0}", rawString);
					}
					this.textBoxRawCommand.Text = text + stringBuilder.ToString();
					return;
				}
				this.textBoxRawCommand.Text = text;
			}
		}

		private void ClearArgmentInput()
		{
			this.textBoxArgName.Text = "";
			this.comboBoxType.SelectedIndex = -1;
			this.textBoxDesc.Text = "";
		}

		private void textBoxName_TextChanged(object sender, EventArgs e)
		{
			this.UpdateRawCommand();
		}

		private void textBoxCommand_TextChanged(object sender, EventArgs e)
		{
			this.UpdateRawCommand();
		}

		private void comboBoxType_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (this.listViewArg.SelectedItems.Count > 0)
			{
				this.listViewArg.SelectedItems[0].SubItems[1].Text = this.comboBoxType.SelectedItem.ToString();
				this.UpdateRawCommand();
			}
		}

		private void textBoxArgName_TextChanged(object sender, EventArgs e)
		{
			if (this.listViewArg.SelectedItems.Count > 0)
			{
				this.listViewArg.SelectedItems[0].SubItems[0].Text = this.textBoxArgName.Text;
				this.UpdateRawCommand();
			}
		}

		private void textBoxDesc_TextChanged(object sender, EventArgs e)
		{
			if (this.listViewArg.SelectedItems.Count > 0)
			{
				this.listViewArg.SelectedItems[0].Tag = this.textBoxDesc.Text;
				this.UpdateRawCommand();
			}
		}

		private void buttonArgAdd_Click(object sender, EventArgs e)
		{
			if (this.textBoxArgName.Text == string.Empty)
			{
				Utility.ShowErrorMessage(this, "인자 이름을 입력하세요.");
				this.textBoxArgName.Focus();
				return;
			}
			this.listViewArg.Items.Add(new ListViewItem(new string[]
			{
				this.textBoxArgName.Text,
				this.comboBoxType.SelectedItem.ToString()
			}));
			this.ClearArgmentInput();
			this.UpdateRawCommand();
			this.textBoxArgName.Focus();
		}

		private void buttoArgDel_Click(object sender, EventArgs e)
		{
			if (this.listViewArg.SelectedItems.Count > 0)
			{
				this.listViewArg.Items.RemoveAt(this.listViewArg.SelectedIndices[0]);
			}
			this.ClearArgmentInput();
			this.UpdateRawCommand();
		}

		private void listViewArg_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (this.listViewArg.SelectedItems.Count > 0)
			{
				ListViewItem listViewItem = this.listViewArg.SelectedItems[0];
				this.textBoxArgName.Text = listViewItem.SubItems[0].Text;
				this.comboBoxType.SelectedItem = listViewItem.SubItems[1].Text;
				this.textBoxDesc.Text = (listViewItem.Tag as string);
				return;
			}
			this.ClearArgmentInput();
		}

		private void CustomCommandForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (base.DialogResult == DialogResult.OK)
			{
				try
				{
					List<RCProcess.CustomCommandParser> fromRawString = RCProcess.CustomCommandParser.GetFromRawString(this.textBoxRawCommand.Text);
					if (fromRawString.Count <= 0)
					{
						throw new Exception("명령을 파싱하는데 실패하였습니다.");
					}
					this.Name = fromRawString[0].Name;
					this.Command = fromRawString[0].RawCommand;
				}
				catch (Exception ex)
				{
					Utility.ShowErrorMessage(this, ex.ToString());
					e.Cancel = true;
				}
			}
		}

		private void textBoxRawCommand_TextChanged(object sender, EventArgs e)
		{
			if (this.textBoxRawCommand.Focused)
			{
				try
				{
					List<RCProcess.CustomCommandParser> fromRawString = RCProcess.CustomCommandParser.GetFromRawString(this.textBoxRawCommand.Text);
					if (fromRawString.Count > 0)
					{
						this.UpdateUI(fromRawString[0]);
					}
				}
				catch
				{
				}
			}
		}
	}
}
