using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Devcat.Core;
using RemoteControlSystem;
using RemoteControlSystem.ClientMessage;

namespace HeroesOpTool.RCUser.ServerMonitorSystem.SystemManage
{
	public partial class RemoteControlSystemManager : Form
	{
		public event EventHandler OnClose;

		public RemoteControlSystemManager(RCUserHandler _userClient, ICollection<RCClient> _clientList, IWorkGroupStructureNode[] workGroupStructure, IWorkGroupStructureNode[] serverGroupStructure, IEnumerable<RCProcess> _processTemplateInfo)
		{
			this.InitializeComponent();
			this.tabPageRCCControl.Text = LocalizeText.Get(345);
			this.tabPageWorkGroupControl.Text = LocalizeText.Get(346);
			this.tabPageProcessTemplate.Text = LocalizeText.Get(347);
			this.buttonOK.Text = LocalizeText.Get(348);
			this.buttonCancel.Text = LocalizeText.Get(349);
			this.tabPageServerGroupControl.Text = LocalizeText.Get(515);
			this.Text = LocalizeText.Get(350);
			if (_userClient.Authority < Authority.Supervisor)
			{
				this.tabPageWorkGroupControl.Enabled = false;
				this.tabPageProcessTemplate.Enabled = false;
				this.tabControl.Controls.Remove(this.tabPageWorkGroupControl);
				this.tabControl.Controls.Remove(this.tabPageServerGroupControl);
				this.tabControl.Controls.Remove(this.tabPageProcessTemplate);
			}
			this.userClient = _userClient;
			this.clientList = new SortedList();
			this.userClient.ClientAdd += this.RC_ClientAdd;
			this.userClient.ClientRemove += this.RC_ClientRemove;
			Dictionary<int, RCClient> dictionary = new Dictionary<int, RCClient>();
			foreach (RCClient rcclient in _clientList)
			{
				this.clientList.Add(rcclient.ID, rcclient);
				RCClient rcclient2 = rcclient.Clone();
				foreach (RCProcess rcprocess in rcclient.ProcessList)
				{
					WorkGroupStructureNode workGroupStructureNode = null;
					if (workGroupStructure != null)
					{
						foreach (WorkGroupStructureNode workGroupStructureNode2 in workGroupStructure)
						{
							workGroupStructureNode = workGroupStructureNode2.GetParent(new WorkGroupCondition(rcclient.Name, rcprocess.Name));
							if (workGroupStructureNode != null)
							{
								break;
							}
						}
					}
					if (workGroupStructureNode != null)
					{
						if (workGroupStructureNode.Authority > this.userClient.Authority)
						{
							((RCProcessCollection)rcclient2.ProcessList).Remove(rcprocess.Name);
						}
					}
					else if (this.userClient.Authority < Authority.Supervisor)
					{
						((RCProcessCollection)rcclient2.ProcessList).Remove(rcprocess.Name);
					}
				}
				if (rcclient.ProcessList.Count == 0 | rcclient2.ProcessList.Count > 0)
				{
					dictionary.Add(rcclient2.ID, rcclient2);
				}
			}
			RCProcessCollection rcprocessCollection = new RCProcessCollection();
			foreach (RCProcess item in _processTemplateInfo)
			{
				rcprocessCollection.Add(item);
			}
			this.rcClientControl.AddClients(dictionary.Values);
			this.rcClientControl.SetTemplateList(rcprocessCollection);
			this.workGroupControl.AddWorkGroup(workGroupStructure);
			this.workGroupControl.AddClients(dictionary.Values);
			this.serverGroupControl.AddServerGroup(serverGroupStructure);
			this.serverGroupControl.AddClients(dictionary.Values);
			this.processTemplateControl.SetTemplateList(rcprocessCollection);
		}

		private void RC_ClientAdd(object sender, EventArgs<RCClient> args)
		{
			this.clientList.Add(args.Value.ID, args.Value);
			this.workGroupControl.AddClient(args.Value);
			this.rcClientControl.AddClient(args.Value);
		}

		private void RC_ClientRemove(object sender, EventArgs<RCClient> args)
		{
			RCClient rcclient = this.clientList[args.Value.ID] as RCClient;
			if (rcclient == null)
			{
				return;
			}
			this.clientList.Remove(args.Value.ID);
			this.rcClientControl.RemoveClient(rcclient);
		}

		private void RemoteControlSystemManager_Closing(object sender, CancelEventArgs args)
		{
			this.userClient.ClientAdd -= this.RC_ClientAdd;
			this.userClient.ClientRemove -= this.RC_ClientRemove;
			if (this.OnClose != null)
			{
				this.OnClose(this, EventArgs.Empty);
			}
		}

		private void buttonOK_Click(object sender, EventArgs args)
		{
			if (this.workGroupControl.Modified)
			{
				WorkGroupChangeMessage message = new WorkGroupChangeMessage(this.workGroupControl.SerializedText);
				this.userClient.SendMessage<WorkGroupChangeMessage>(message);
			}
			if (this.serverGroupControl.Modified)
			{
				ServerGroupChangeMessage message2 = new ServerGroupChangeMessage(this.serverGroupControl.SerializedText);
				this.userClient.SendMessage<ServerGroupChangeMessage>(message2);
			}
			if (this.processTemplateControl.Modified)
			{
				TemplateChangeMessage message3 = new TemplateChangeMessage(this.processTemplateControl.ProcessTemplateCollection);
				this.userClient.SendMessage<TemplateChangeMessage>(message3);
			}
			foreach (ControlRequestMessage message4 in this.rcClientControl.MessageList)
			{
				this.userClient.SendMessage<ControlRequestMessage>(message4);
			}
			base.Close();
		}

		private RCUserHandler userClient;

		private SortedList clientList;
	}
}
