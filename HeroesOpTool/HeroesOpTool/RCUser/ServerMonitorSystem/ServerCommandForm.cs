using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using RemoteControlSystem;
using RemoteControlSystem.ServerMessage;

namespace HeroesOpTool.RCUser.ServerMonitorSystem
{
	public partial class ServerCommandForm : CommandForm
	{
		public bool ScheduleModified { get; private set; }

		public List<KeyValuePair<int, StandardInProcessMessage>> Command { get; private set; }

		public IEnumerable<KeyValuePair<int, RCProcess>> ScheduledProcess
		{
			get
			{
				return this.commandBridges;
			}
		}

		public ServerCommandForm(Authority rcAuthority, IEnumerable<KeyValuePair<RCProcess, RCClient>> commandBridges)
		{
			this.InitializeComponent();
			this.groupBoxCommand.Text = LocalizeText.Get(519);
			this.groupBoxServer.Text = LocalizeText.Get(518);
			this.groupBoxArgList.Text = LocalizeText.Get(485);
			this.checkSchedule.Text = LocalizeText.Get(520);
			this.buttonCancel.Text = LocalizeText.Get(12);
			this.buttonOK.Text = LocalizeText.Get(244);
			this.textBox1.Text = LocalizeText.Get(522);
			this.Text = LocalizeText.Get(521);
			this.commandBridges = new Dictionary<int, RCProcess>();
			this.schedulesByTime = new Dictionary<string, Dictionary<string, RCProcessScheduler>>();
			this.schedulesOther = new Dictionary<string, Dictionary<string, RCProcessScheduler>>();
			this.commandDict = new Dictionary<string, HashSet<ServerCommandForm.ServerGroupItem>>();
			this.controlArgs = new List<Control>();
			this.clientAuthority = rcAuthority;
			foreach (KeyValuePair<RCProcess, RCClient> keyValuePair in commandBridges)
			{
				string key = this.MakeKey(keyValuePair.Value.ID, keyValuePair.Key.Name);
				this.commandBridges.Add(keyValuePair.Value.ID, keyValuePair.Key.Clone());
				this.schedulesOther.Add(key, new Dictionary<string, RCProcessScheduler>());
				this.schedulesByTime.Add(key, new Dictionary<string, RCProcessScheduler>());
				if (keyValuePair.Key.State == RCProcess.ProcessState.On)
				{
					IEnumerable<string> commandBridgeServer = RCUserHandler.GetCommandBridgeServer(keyValuePair.Key);
					foreach (RCProcess.CustomCommandParser customCommandParser in RCProcess.CustomCommandParser.GetFromRawString(keyValuePair.Key.CustomCommandString))
					{
						HashSet<ServerCommandForm.ServerGroupItem> hashSet;
						if (this.commandDict.ContainsKey(customCommandParser.RawCommand))
						{
							hashSet = this.commandDict[customCommandParser.RawCommand];
						}
						else
						{
							this.listBoxCommand.Items.Add(new CommandForm.CommandItem(customCommandParser));
							hashSet = new HashSet<ServerCommandForm.ServerGroupItem>();
							this.commandDict.Add(customCommandParser.RawCommand, hashSet);
						}
						foreach (string serverGroup in commandBridgeServer)
						{
							ServerCommandForm.ServerGroupItem item = new ServerCommandForm.ServerGroupItem(serverGroup, keyValuePair.Key.Name, keyValuePair.Value.ID);
							if (!hashSet.Contains(item))
							{
								hashSet.Add(item);
							}
						}
					}
				}
				foreach (RCProcessScheduler rcprocessScheduler in keyValuePair.Key.Schedules)
				{
					if (rcprocessScheduler.ExeType == RCProcessScheduler.EExeType.StdInput && rcprocessScheduler.ScheduleType == RCProcessScheduler.EScheduleType.Once)
					{
						this.AddSchedule(key, rcprocessScheduler.ScheduleTime, rcprocessScheduler.Name, rcprocessScheduler.Command);
					}
					else
					{
						this.schedulesOther[key].Add(rcprocessScheduler.Name, rcprocessScheduler);
					}
				}
			}
		}

		private string MakeKey(int id, string name)
		{
			return string.Format("{0}:{1}", id, name);
		}

		private void listBoxCommand_SelectedIndexChanged(object sender, EventArgs e)
		{
			this.checkedListBoxServer.Items.Clear();
			CommandForm.CommandItem commandItem = this.listBoxCommand.SelectedItem as CommandForm.CommandItem;
			if (commandItem != null && this.commandDict.ContainsKey(commandItem.Command.RawCommand))
			{
				HashSet<ServerCommandForm.ServerGroupItem> hashSet = this.commandDict[commandItem.Command.RawCommand];
				foreach (ServerCommandForm.ServerGroupItem item in hashSet)
				{
					this.checkedListBoxServer.Items.Add(item);
				}
				foreach (CommandForm.ArgumentControl argumentControl in base.SetArgPanel(this.panelArgList, commandItem.Command, null))
				{
					if (argumentControl.Argument.Type == RCProcess.CustomCommandParser.DataType.ServerGroup)
					{
						argumentControl.Control.Visible = false;
						argumentControl.Label.Visible = false;
					}
				}
			}
		}

		protected override string GetArgument(int index, RCProcess.CustomCommandParser.CommandArg arg)
		{
			if (arg.Type == RCProcess.CustomCommandParser.DataType.ServerGroup)
			{
				return this.currentServer;
			}
			return base.GetArgument(index, arg);
		}

		private void buttonOK_Click(object sender, EventArgs e)
		{
			CommandForm.CommandItem commandItem = this.listBoxCommand.SelectedItem as CommandForm.CommandItem;
			if (!this.ScheduleModified || this.checkSchedule.Checked)
			{
				if (commandItem == null)
				{
					Utility.ShowErrorMessage(LocalizeText.Get(247));
					return;
				}
				if (this.checkedListBoxServer.CheckedItems.Count == 0)
				{
					Utility.ShowErrorMessage(LocalizeText.Get(535));
					return;
				}
				if (this.clientAuthority == Authority.UserKicker)
				{
					string text = commandItem.ToString();
					if (!text.Equals("Kick Character") && !text.Equals("Kick Account"))
					{
						Utility.ShowErrorMessage(LocalizeText.Get(229));
						return;
					}
				}
			}
			this.Command = new List<KeyValuePair<int, StandardInProcessMessage>>();
			if (commandItem.Command.Command.Equals("announce", StringComparison.CurrentCultureIgnoreCase) && MessageBox.Show(string.Format("\"{0}\"\r\n{1}", this.GetArgument(1, commandItem.Command.Arguments.ElementAt(1)), LocalizeText.Get(546)), "Announce to client Check", MessageBoxButtons.YesNo) != DialogResult.Yes)
			{
				return;
			}
			foreach (object obj in this.checkedListBoxServer.CheckedItems)
			{
				ServerCommandForm.ServerGroupItem serverGroupItem = (ServerCommandForm.ServerGroupItem)obj;
				this.currentServer = serverGroupItem.ServerGroup;
				string text2 = base.GetFromArgPanel(commandItem.Command);
				if (text2 == null)
				{
					return;
				}
				if (commandItem.Command.Command.Equals("console", StringComparison.CurrentCultureIgnoreCase) && text2.StartsWith("event reg ", StringComparison.CurrentCultureIgnoreCase))
				{
					text2 = text2.Substring(0, text2.Length - 1);
					text2 = text2 + " username " + MainForm.Instance.GetCurrentUserID() + "'";
				}
				if (this.checkSchedule.Checked)
				{
					string name = string.Format("{0} {1}", serverGroupItem.ServerGroup, commandItem.ToString());
					string key = this.MakeKey(serverGroupItem.ID, serverGroupItem.Process);
					this.AddSchedule(key, this.dateTimePicker.Value, name, text2);
					this.ScheduleModified = true;
				}
				else
				{
					this.Command.Add(new KeyValuePair<int, StandardInProcessMessage>(serverGroupItem.ID, new StandardInProcessMessage(serverGroupItem.Process, text2)));
				}
			}
			if (!this.checkSchedule.Checked)
			{
				if (this.ScheduleModified)
				{
					foreach (KeyValuePair<int, RCProcess> keyValuePair in this.commandBridges)
					{
						string key2 = this.MakeKey(keyValuePair.Key, keyValuePair.Value.Name);
						keyValuePair.Value.Schedules.Clear();
						foreach (RCProcessScheduler item in this.schedulesByTime[key2].Values)
						{
							keyValuePair.Value.Schedules.Add(item);
						}
						foreach (RCProcessScheduler item2 in this.schedulesOther[key2].Values)
						{
							keyValuePair.Value.Schedules.Add(item2);
						}
					}
				}
				base.DialogResult = DialogResult.OK;
				base.Close();
				return;
			}
			this.checkSchedule.Checked = false;
			this.checkedListBoxServer.ClearSelected();
			this.listBoxCommand.ClearSelected();
		}

		private void checkSchedule_CheckedChanged(object sender, EventArgs e)
		{
			this.dateTimePicker.Enabled = this.checkSchedule.Checked;
			this.buttonOK.Text = (this.checkSchedule.Checked ? "예약" : "실행");
		}

		private void AddSchedule(string key, DateTime datetime, string name, string command)
		{
			int num = 1;
			string text = name;
			while (this.schedulesByTime[key].ContainsKey(text) || this.schedulesOther[key].ContainsKey(text))
			{
				text = string.Format("{0} {1}", name, num++);
			}
			datetime = datetime.AddSeconds((double)(-(double)datetime.Second));
			RCProcessScheduler rcprocessScheduler = new RCProcessScheduler(text, RCProcessScheduler.EScheduleType.Once, datetime, RCProcessScheduler.EExeType.StdInput, command, true);
			int height = this.panelSchdule.Size.Height;
			ServerCommandSchedule serverCommandSchedule = new ServerCommandSchedule(key, rcprocessScheduler);
			serverCommandSchedule.OnDelete += this.OnDelete;
			serverCommandSchedule.Size = new Size(this.panelSchdule.Width - 2, 40);
			serverCommandSchedule.Location = new Point(2, this.allScheduleCount * 40);
			this.panelSchdule.Controls.Add(serverCommandSchedule);
			serverCommandSchedule.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
			int height2 = this.panelSchdule.Size.Height;
			AnchorStyles anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			AnchorStyles anchor2 = this.panelMid.Anchor;
			AnchorStyles anchor3 = this.panelBottom.Anchor;
			this.panelMid.Anchor = anchor;
			this.panelBottom.Anchor = anchor;
			base.Size = new Size(base.Size.Width, base.Size.Height + height2 - height);
			this.panelMid.Anchor = anchor2;
			this.panelBottom.Anchor = anchor3;
			this.schedulesByTime[key].Add(text, rcprocessScheduler);
			this.allScheduleCount++;
		}

		private void OnDelete(object sender, EventArgs e)
		{
			ServerCommandSchedule serverCommandSchedule = sender as ServerCommandSchedule;
			if (serverCommandSchedule != null)
			{
				this.schedulesByTime[serverCommandSchedule.Key].Remove(serverCommandSchedule.ScheduleName);
				int height = this.panelSchdule.Size.Height;
				this.panelSchdule.Controls.Remove(serverCommandSchedule);
				int num = 0;
				foreach (object obj in this.panelSchdule.Controls)
				{
					ServerCommandSchedule serverCommandSchedule2 = (ServerCommandSchedule)obj;
					serverCommandSchedule2.Location = new Point(2, num++ * 40);
				}
				int height2 = this.panelSchdule.Size.Height;
				AnchorStyles anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
				AnchorStyles anchor2 = this.panelMid.Anchor;
				AnchorStyles anchor3 = this.panelBottom.Anchor;
				this.panelMid.Anchor = anchor;
				this.panelBottom.Anchor = anchor;
				base.Size = new Size(base.Size.Width, base.Size.Height + height2 - height);
				this.panelMid.Anchor = anchor2;
				this.panelBottom.Anchor = anchor3;
				this.ScheduleModified = true;
				this.allScheduleCount--;
			}
		}

		private const int formSize = 40;

		private Dictionary<string, HashSet<ServerCommandForm.ServerGroupItem>> commandDict;

		private string currentServer;

		private Dictionary<int, RCProcess> commandBridges;

		private Dictionary<string, Dictionary<string, RCProcessScheduler>> schedulesByTime;

		private Dictionary<string, Dictionary<string, RCProcessScheduler>> schedulesOther;

		private int allScheduleCount;

		private Authority clientAuthority;

		private class ServerGroupItem
		{
			public string ServerGroup { get; private set; }

			public string Process { get; private set; }

			public int ID { get; private set; }

			public ServerGroupItem(string serverGroup, string processName, int clientID)
			{
				this.ServerGroup = serverGroup;
				this.Process = processName;
				this.ID = clientID;
			}

			public override string ToString()
			{
				return this.ServerGroup;
			}
		}
	}
}
