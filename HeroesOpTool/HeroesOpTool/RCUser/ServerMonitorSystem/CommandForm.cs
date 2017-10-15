using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using RemoteControlSystem;

namespace HeroesOpTool.RCUser.ServerMonitorSystem
{
	public partial class CommandForm : Form
	{
		public CommandForm()
		{
			this.InitializeComponent();
		}

		protected IEnumerable<CommandForm.ArgumentControl> SetArgPanel(Panel panel, RCProcess.CustomCommandParser command, IEnumerable<string> serverList)
		{
			panel.Controls.Clear();
			this.controlArgs.Clear();
			this.ctrlTotalSize = 0;
			foreach (RCProcess.CustomCommandParser.CommandArg arg in command.Arguments)
			{
				Label textLabel = new Label();
				textLabel.Text = arg.Name;
				textLabel.TextAlign = ContentAlignment.MiddleCenter;
				textLabel.Location = new Point(0, this.ctrlTotalSize);
				textLabel.Size = new Size(100, 24);
				Control ctrl = null;
				int width = panel.Size.Width - 120;
				int height = 24;
				int x = 100;
				int y = this.ctrlTotalSize;
				switch (arg.Type)
				{
				case RCProcess.CustomCommandParser.DataType.Numeric:
					ctrl = new Utility.NumericTextBox();
					break;
				case RCProcess.CustomCommandParser.DataType.Date:
					ctrl = new DateTimePicker();
					((DateTimePicker)ctrl).CustomFormat = "yyyy-MM-dd HH:mm:ss";
					((DateTimePicker)ctrl).Format = DateTimePickerFormat.Custom;
					break;
				case RCProcess.CustomCommandParser.DataType.Boolean:
					ctrl = new TwoRadioButton();
					break;
				case RCProcess.CustomCommandParser.DataType.ServerGroup:
				{
					ctrl = new ComboBox();
					ComboBox comboBox = ctrl as ComboBox;
					comboBox.DropDownStyle = ComboBoxStyle.DropDownList;
					if (serverList != null)
					{
						comboBox.Items.AddRange(serverList.ToArray<string>());
					}
					break;
				}
				case RCProcess.CustomCommandParser.DataType.LargeString:
				{
					ctrl = new RichTextBox();
					RichTextBox richTextBox = ctrl as RichTextBox;
					richTextBox.Multiline = true;
					x = 20;
					y = this.ctrlTotalSize + 24;
					height = 120;
					width = panel.Width - 40;
					break;
				}
				default:
					ctrl = new TextBox();
					break;
				}
				if (ctrl != null)
				{
					ctrl.Name = arg.Name;
					ctrl.Location = new Point(x, y);
					ctrl.Size = new Size(width, height);
					ctrl.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
					new ToolTip
					{
						AutoPopDelay = 5000,
						InitialDelay = 500,
						ReshowDelay = 250,
						ShowAlways = true
					}.SetToolTip(ctrl, arg.Comment);
					panel.Controls.Add(textLabel);
					panel.Controls.Add(ctrl);
					this.controlArgs.Add(ctrl);
					yield return new CommandForm.ArgumentControl(textLabel, ctrl, arg);
				}
				int sizeToIncrease = y - this.ctrlTotalSize + (ctrl.Visible ? height : 0);
				this.ctrlTotalSize += sizeToIncrease;
			}
			yield break;
		}

		protected virtual string GetArgument(int index, RCProcess.CustomCommandParser.CommandArg arg)
		{
			switch (arg.Type)
			{
			case RCProcess.CustomCommandParser.DataType.Date:
			{
				DateTimePicker dateTimePicker = this.controlArgs[index] as DateTimePicker;
				return dateTimePicker.Value.ToString("yyyy/MM/dd HH:mm:ss");
			}
			case RCProcess.CustomCommandParser.DataType.Boolean:
			{
				TwoRadioButton twoRadioButton = this.controlArgs[index] as TwoRadioButton;
				if (!twoRadioButton.Checked)
				{
					return "0";
				}
				return "1";
			}
			default:
				return this.controlArgs[index].Text;
			}
		}

		protected string GetFromArgPanel(RCProcess.CustomCommandParser command)
		{
			bool flag = false;
			int num = 0;
			List<string> list = new List<string>();
			foreach (RCProcess.CustomCommandParser.CommandArg arg in command.Arguments)
			{
				string text = this.GetArgument(num, arg);
				if (text.IndexOf('\'') != -1)
				{
					Utility.ShowErrorMessage(LocalizeText.Get(533));
					return null;
				}
				if (string.IsNullOrEmpty(text))
				{
					text = string.Empty;
					flag = true;
				}
				list.Add(string.Format("'{0}'", text));
				num++;
			}
			string finalCommand = command.GetFinalCommand(list.ToArray());
			if (flag && !Utility.InputYesNoFromWarning(LocalizeText.Get(405)))
			{
				return null;
			}
			return finalCommand;
		}

		protected const int ctrlSize = 24;

		protected int ctrlTotalSize;

		protected List<Control> controlArgs;

		protected class CommandItem
		{
			public RCProcess.CustomCommandParser Command { get; private set; }

			public CommandItem(RCProcess.CustomCommandParser command)
			{
				this.Command = command;
			}

			public override string ToString()
			{
				return this.Command.Name;
			}
		}

		protected struct ArgumentControl
		{
			public ArgumentControl(Label label, Control control, RCProcess.CustomCommandParser.CommandArg argument)
			{
				this.Label = label;
				this.Control = control;
				this.Argument = argument;
			}

			public Label Label;

			public Control Control;

			public RCProcess.CustomCommandParser.CommandArg Argument;
		}
	}
}
