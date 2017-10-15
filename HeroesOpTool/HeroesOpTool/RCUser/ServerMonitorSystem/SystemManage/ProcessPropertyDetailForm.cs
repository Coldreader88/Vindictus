using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using RemoteControlSystem;

namespace HeroesOpTool.RCUser.ServerMonitorSystem.SystemManage
{
	public partial class ProcessPropertyDetailForm : Form
	{
		public string ShutdownString
		{
			get
			{
				return this.textBoxShutdownString.Text;
			}
			set
			{
				this.textBoxShutdownString.Text = value;
			}
		}

		public string BootedString
		{
			get
			{
				return this.textBoxBootedString.Text;
			}
			set
			{
				this.textBoxBootedString.Text = value;
			}
		}

		public string PerformanceString
		{
			get
			{
				return this.textBoxPerformanceString.Text;
			}
			set
			{
				this.textBoxPerformanceString.Text = value;
			}
		}

		public bool TraceChildProcess
		{
			get
			{
				return this.checkBoxTraceChildProcess.Checked;
			}
			set
			{
				this.checkBoxTraceChildProcess.Checked = value;
			}
		}

		public int MaxChildProcessCount
		{
			get
			{
				if (!this.TraceChildProcess)
				{
					return 0;
				}
				if (string.IsNullOrEmpty(this.textBoxMaxChildProcessCount.Text))
				{
					return 0;
				}
				return int.Parse(this.textBoxMaxChildProcessCount.Text);
			}
			set
			{
				this.textBoxMaxChildProcessCount.Text = value.ToString();
			}
		}

		public string ChildProcessLogStr
		{
			get
			{
				return this.textBoxChildProcessLogStr.Text;
			}
			set
			{
				this.textBoxChildProcessLogStr.Text = value;
			}
		}

		public ProcessPropertyDetailForm()
		{
			this.InitializeComponent();
			this.toolTipProperty.SetToolTip(this.textBoxPerformanceString, LocalizeText.Get(257));
			this.toolTipProperty.SetToolTip(this.textBoxShutdownString, LocalizeText.Get(258));
			this.toolTipProperty.SetToolTip(this.textBoxBootedString, LocalizeText.Get(259));
			this.labelBootedString.Text = LocalizeText.Get(271);
			this.labelPerformanceString.Text = LocalizeText.Get(272);
			this.groupBoxStandardOutProperty.Text = LocalizeText.Get(277);
			this.toolTipProperty.SetToolTip(this.groupBoxStandardOutProperty, LocalizeText.Get(278));
			this.columnHeader3.Text = LocalizeText.Get(279);
			this.columnHeader4.Text = LocalizeText.Get(280);
			this.labelShutdownString.Text = LocalizeText.Get(281);
			this.labelPerformanceDescription.Text = LocalizeText.Get(282);
			this.groupBoxStandardInProperty.Text = LocalizeText.Get(287);
			this.toolTipProperty.SetToolTip(this.groupBoxStandardInProperty, LocalizeText.Get(288));
			this.columnHeader1.Text = LocalizeText.Get(289);
			this.columnHeader2.Text = LocalizeText.Get(290);
			this.label1.Text = LocalizeText.Get(291);
			this.Text = LocalizeText.Get(430);
		}

		public ProcessPropertyDetailForm(RCProcess process, bool editable) : this()
		{
			this.textBoxShutdownString.Text = process.ShutdownString;
			this.textBoxBootedString.Text = process.BootedString;
			this.textBoxPerformanceString.Text = process.PerformanceString;
			this.checkBoxTraceChildProcess.Checked = process.TraceChildProcess;
			this.textBoxMaxChildProcessCount.Text = process.MaxChildProcessCount.ToString();
			this.textBoxChildProcessLogStr.Text = process.ChildProcessLogStr;
			this.FillCustomCommand(process.CustomCommandString);
			this.FillPerformanceDescription(process.PerformanceDescription);
			this.FillProperty(process.StaticProperties, process.Properties);
			if (!editable)
			{
				this._editable = false;
				this.textBoxShutdownString.Enabled = false;
				this.buttonCustomCommandAdd.Enabled = false;
				this.buttonCustomCommandSub.Enabled = false;
				this.buttonCustomCommandUp.Enabled = false;
				this.buttonCustomCommandDown.Enabled = false;
				this.textBoxBootedString.Enabled = false;
				this.textBoxPerformanceString.Enabled = false;
				this.buttonPerformanceDescAdd.Enabled = false;
				this.buttonPerformanceDescSub.Enabled = false;
				this.buttonPerformanceDescUp.Enabled = false;
				this.buttonPerformanceDescDown.Enabled = false;
				this.buttonPropertyAdd.Enabled = false;
				this.buttonPropertyDel.Enabled = false;
				this.checkBoxTraceChildProcess.Enabled = false;
				this.textBoxMaxChildProcessCount.Enabled = false;
				this.textBoxChildProcessLogStr.Enabled = false;
			}
		}

		private bool IsStaticProperty(ListViewItem item)
		{
			return item.BackColor != Color.Gray;
		}

		private void SetStaticProperty(ListViewItem item)
		{
			item.BackColor = Color.Gray;
		}

		public void FillCustomCommand(string customCommand)
		{
			this.listViewCustomCommand.Items.Clear();
			foreach (RCProcess.CustomCommandParser customCommandParser in RCProcess.CustomCommandParser.GetFromRawString(customCommand))
			{
				this.listViewCustomCommand.Items.Add(new ListViewItem(new string[]
				{
					customCommandParser.Name,
					customCommandParser.RawCommand
				}));
			}
		}

		public void FillPerformanceDescription(string performanceDescription)
		{
			this.listViewPerformanceDescription.Items.Clear();
			foreach (RCProcess.PerformanceDescriptionParser performanceDescriptionParser in RCProcess.PerformanceDescriptionParser.GetFromRawString(performanceDescription))
			{
				this.listViewPerformanceDescription.Items.Add(new ListViewItem(new string[]
				{
					performanceDescriptionParser.Name,
					performanceDescriptionParser.HelpMessage
				}));
			}
		}

		private void FillProperty(IDictionary<string, string> staticProperty, IDictionary<string, string> property)
		{
			this.listViewProperty.Items.Clear();
			foreach (KeyValuePair<string, string> keyValuePair in property)
			{
				ListViewItem listViewItem = new ListViewItem(new string[]
				{
					keyValuePair.Key,
					keyValuePair.Value
				});
				if (!staticProperty.ContainsKey(keyValuePair.Key))
				{
					this.SetStaticProperty(listViewItem);
				}
				this.listViewProperty.Items.Add(listViewItem);
			}
		}

		public string GetCustomCommand()
		{
			RCProcess.CustomCommandParser[] array = new RCProcess.CustomCommandParser[this.listViewCustomCommand.Items.Count];
			for (int i = 0; i < array.Length; i++)
			{
				ListViewItem listViewItem = this.listViewCustomCommand.Items[i];
				array[i] = new RCProcess.CustomCommandParser(listViewItem.SubItems[0].Text, listViewItem.SubItems[1].Text);
			}
			return RCProcess.CustomCommandParser.ToRawString(array);
		}

		public string GetPerformanceDescription()
		{
			RCProcess.PerformanceDescriptionParser[] array = new RCProcess.PerformanceDescriptionParser[this.listViewPerformanceDescription.Items.Count];
			for (int i = 0; i < array.Length; i++)
			{
				ListViewItem listViewItem = this.listViewPerformanceDescription.Items[i];
				array[i] = new RCProcess.PerformanceDescriptionParser(listViewItem.SubItems[0].Text, listViewItem.SubItems[1].Text);
			}
			return RCProcess.PerformanceDescriptionParser.ToRawString(array);
		}

		public IEnumerable<KeyValuePair<string, string>> GetProperty()
		{
			foreach (object obj in this.listViewProperty.Items)
			{
				ListViewItem item = (ListViewItem)obj;
				if (this.IsStaticProperty(item))
				{
					yield return new KeyValuePair<string, string>(item.SubItems[0].Text, item.SubItems[1].Text);
				}
			}
			yield break;
		}

		private void buttonCustomCommandAdd_Click(object sender, EventArgs e)
		{
			CustomCommandForm customCommandForm = new CustomCommandForm();
			if (customCommandForm.ShowDialog(this) == DialogResult.OK)
			{
				this.listViewCustomCommand.Items.Add(new ListViewItem(new string[]
				{
					customCommandForm.Name,
					customCommandForm.Command
				}));
				this.listViewCustomCommand.EnsureVisible(this.listViewCustomCommand.Items.Count - 1);
			}
		}

		private void buttonCustomCommandSub_Click(object sender, EventArgs e)
		{
			if (this.listViewCustomCommand.SelectedIndices.Count > 0)
			{
				this.listViewCustomCommand.Items.RemoveAt(this.listViewCustomCommand.SelectedIndices[0]);
			}
		}

		private void buttonCustomCommandUp_Click(object sender, EventArgs e)
		{
			if (this.listViewCustomCommand.SelectedIndices.Count > 0)
			{
				int num = this.listViewCustomCommand.SelectedIndices[0];
				if (num > 0)
				{
					ListViewItem listViewItem = this.listViewCustomCommand.Items[num];
					this.listViewCustomCommand.Items.RemoveAt(num);
					this.listViewCustomCommand.Items.Insert(num - 1, listViewItem);
					listViewItem.Selected = true;
				}
			}
		}

		private void buttonCustomCommandDown_Click(object sender, EventArgs e)
		{
			if (this.listViewCustomCommand.SelectedIndices.Count > 0)
			{
				int num = this.listViewCustomCommand.SelectedIndices[0];
				if (num < this.listViewCustomCommand.Items.Count - 1)
				{
					ListViewItem listViewItem = this.listViewCustomCommand.Items[num];
					this.listViewCustomCommand.Items.RemoveAt(num);
					this.listViewCustomCommand.Items.Insert(num + 1, listViewItem);
					listViewItem.Selected = true;
				}
			}
		}

		private void ListViewCustomCommand_DoubleClick(object sender, EventArgs e)
		{
			if (!this._editable)
			{
				return;
			}
			if (this.listViewCustomCommand.SelectedItems.Count > 0)
			{
				ListViewItem listViewItem = this.listViewCustomCommand.SelectedItems[0];
				CustomCommandForm customCommandForm = new CustomCommandForm(listViewItem.SubItems[0].Text, listViewItem.SubItems[1].Text);
				if (customCommandForm.ShowDialog() == DialogResult.OK)
				{
					listViewItem.SubItems[0].Text = customCommandForm.Name;
					listViewItem.SubItems[1].Text = customCommandForm.Command;
				}
			}
		}

		private void buttonPerformanceDescAdd_Click(object sender, EventArgs e)
		{
			TwoStringInputForm twoStringInputForm = new TwoStringInputForm(LocalizeText.Get(304), LocalizeText.Get(305), LocalizeText.Get(306), LocalizeText.Get(307));
			while (twoStringInputForm.ShowDialog() == DialogResult.OK)
			{
				try
				{
					new RCProcess.PerformanceDescriptionParser(twoStringInputForm.Value1, twoStringInputForm.Value2);
					this.listViewPerformanceDescription.Items.Add(new ListViewItem(new string[]
					{
						twoStringInputForm.Value1,
						twoStringInputForm.Value2
					}));
					this.listViewPerformanceDescription.EnsureVisible(this.listViewPerformanceDescription.Items.Count - 1);
					break;
				}
				catch (Exception ex)
				{
					Utility.ShowErrorMessage(LocalizeText.Get(308) + ex.Message);
				}
			}
		}

		private void buttonPerformanceDescSub_Click(object sender, EventArgs e)
		{
			if (this.listViewPerformanceDescription.SelectedIndices.Count > 0)
			{
				this.listViewPerformanceDescription.Items.RemoveAt(this.listViewPerformanceDescription.SelectedIndices[0]);
			}
		}

		private void buttonPerformanceDescUp_Click(object sender, EventArgs e)
		{
			if (this.listViewPerformanceDescription.SelectedIndices.Count > 0)
			{
				int num = this.listViewPerformanceDescription.SelectedIndices[0];
				if (num > 0)
				{
					ListViewItem listViewItem = this.listViewPerformanceDescription.Items[num];
					this.listViewPerformanceDescription.Items.RemoveAt(num);
					this.listViewPerformanceDescription.Items.Insert(num - 1, listViewItem);
					listViewItem.Selected = true;
				}
			}
		}

		private void buttonPerformanceDescDown_Click(object sender, EventArgs e)
		{
			if (this.listViewPerformanceDescription.SelectedIndices.Count > 0)
			{
				int num = this.listViewPerformanceDescription.SelectedIndices[0];
				if (num < this.listViewPerformanceDescription.Items.Count - 1)
				{
					ListViewItem listViewItem = this.listViewPerformanceDescription.Items[num];
					this.listViewPerformanceDescription.Items.RemoveAt(num);
					this.listViewPerformanceDescription.Items.Insert(num + 1, listViewItem);
					listViewItem.Selected = true;
				}
			}
		}

		private void ListViewPerformanceDescription_DoubleClick(object sender, EventArgs e)
		{
			if (!this._editable)
			{
				return;
			}
			if (this.listViewPerformanceDescription.SelectedItems.Count > 0)
			{
				ListViewItem listViewItem = this.listViewPerformanceDescription.SelectedItems[0];
				TwoStringInputForm twoStringInputForm = new TwoStringInputForm(LocalizeText.Get(309), LocalizeText.Get(310), LocalizeText.Get(311), LocalizeText.Get(312), listViewItem.SubItems[0].Text, listViewItem.SubItems[1].Text);
				while (twoStringInputForm.ShowDialog() == DialogResult.OK)
				{
					try
					{
						new RCProcess.PerformanceDescriptionParser(twoStringInputForm.Value1, twoStringInputForm.Value2);
						listViewItem.SubItems[0].Text = twoStringInputForm.Value1;
						listViewItem.SubItems[1].Text = twoStringInputForm.Value2;
						break;
					}
					catch (Exception ex)
					{
						Utility.ShowErrorMessage(LocalizeText.Get(313) + ex.Message);
					}
				}
			}
		}

		private void buttonPropertyAdd_Click(object sender, EventArgs e)
		{
			TwoStringInputForm twoStringInputForm = new TwoStringInputForm("프로세스 속성 추가", "프로세스 속성을 입력하세요", "Key", "Value");
			while (twoStringInputForm.ShowDialog() == DialogResult.OK)
			{
				try
				{
					this.CheckProperty(twoStringInputForm.Value1, -1);
					this.listViewProperty.Items.Add(new ListViewItem(new string[]
					{
						twoStringInputForm.Value1,
						twoStringInputForm.Value2
					}));
					this.listViewProperty.EnsureVisible(this.listViewProperty.Items.Count - 1);
					break;
				}
				catch (Exception ex)
				{
					Utility.ShowErrorMessage("속성을 추가할 수 없습니다. 원본 메세지: " + ex.Message);
				}
			}
		}

		private void buttonPropertyDel_Click(object sender, EventArgs e)
		{
			if (this.listViewProperty.SelectedItems.Count > 0)
			{
				ListViewItem item = this.listViewProperty.SelectedItems[0];
				if (this.IsStaticProperty(item))
				{
					this.listViewProperty.Items.RemoveAt(this.listViewProperty.SelectedIndices[0]);
					return;
				}
				Utility.ShowErrorMessage("동적 속성은 삭제할 수 없습니다.");
			}
		}

		private void listViewProperty_DoubleClick(object sender, EventArgs e)
		{
			if (!this._editable)
			{
				return;
			}
			if (this.listViewProperty.SelectedItems.Count > 0)
			{
				ListViewItem listViewItem = this.listViewProperty.SelectedItems[0];
				TwoStringInputForm twoStringInputForm = new TwoStringInputForm("프로세스 속성 편집", this.IsStaticProperty(listViewItem) ? "프로세스 속성을 입력하세요." : "동적 속성은 편집할 수 없습니다.", "Key", "Value", listViewItem.SubItems[0].Text, listViewItem.SubItems[1].Text);
				while (twoStringInputForm.ShowDialog() == DialogResult.OK)
				{
					try
					{
						if (this.IsStaticProperty(listViewItem))
						{
							this.CheckProperty(twoStringInputForm.Value1, listViewItem.Index);
							listViewItem.SubItems[0].Text = twoStringInputForm.Value1;
							listViewItem.SubItems[1].Text = twoStringInputForm.Value2;
						}
						break;
					}
					catch (Exception ex)
					{
						Utility.ShowErrorMessage(LocalizeText.Get(313) + ex.Message);
					}
				}
			}
		}

		private void CheckProperty(string key, int index)
		{
			int num = 0;
			if (this.listViewProperty.Items.Count > num)
			{
				for (;;)
				{
					ListViewItem listViewItem = this.listViewProperty.FindItemWithText(key, false, num);
					if (listViewItem == null)
					{
						return;
					}
					if (listViewItem.Index != index)
					{
						break;
					}
					num = index + 1;
					if (num >= this.listViewProperty.Items.Count)
					{
						return;
					}
				}
				throw new Exception(string.Format("속성 Key {0} 값이 중복되었습니다.", key));
			}
		}

		private bool _editable = true;
	}
}
