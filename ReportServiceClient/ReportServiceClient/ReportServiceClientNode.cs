using System;
using System.Text;
using Devcat.Core;
using Devcat.Core.Net;
using Devcat.Core.Net.Message;
using Devcat.Core.Threading;
using ReportServiceClient.Properties;
using UnifiedNetwork.EntityGraph;
using UnifiedNetwork.ReportService.Operations;

namespace ReportServiceClient
{
	public class ReportServiceClientNode
	{
		public event EventHandler<EventArgs> ConnectionSucceed;

		public bool Valid
		{
			get
			{
				return this.valid;
			}
		}

		public MessageHandlerFactory MF
		{
			get
			{
				return this.mf;
			}
		}

		public ReportServiceClientNode(ReportServiceClientForm p)
		{
			this.parent = p;
			this.valid = false;
			this.thread.Start();
			this.mf.Register<ReportServiceClientNode>(ReportServiceOperationMessages.TypeConverters, "ProcessMessage");
			this.peer.Connect(this.thread, "127.0.0.1", (int)Settings.Default.ListenPort, new MessageAnalyzer());
			this.peer.ConnectionSucceed += this.AtConnectionSucceed;
			this.peer.ConnectionFail += new EventHandler<EventArgs<Exception>>(this.AtConnectionFailed);
			this.peer.PacketReceive += this.OnPacketReceive;
		}

		private void AtConnectionFailed(object sender, EventArgs e)
		{
			this.valid = false;
		}

		private void AtConnectionSucceed(object sender, EventArgs e)
		{
			this.valid = true;
			this.ConnectionSucceed(sender, e);
		}

		private void OnPacketReceive(object sender, EventArgs<ArraySegment<byte>> e)
		{
			Packet packet = new Packet(e.Value);
			this.mf.Handle(packet, this);
		}

		public void QueryUnderingList(string Text)
		{
			if (Text == this.parent.ServicesString)
			{
				this.currentTarget = new EntityGraphIdentifier
				{
					category = "ReportService",
					entityID = (long)EntityGraphNode.ServiceEntityID
				};
				this.peer.Transmit(SerializeWriter.ToBinary<RequestLookUpInfoMessage>(new RequestLookUpInfoMessage("ReportService")));
				return;
			}
			string[] array = Text.Split(this.parent.delim);
			if (array.Length == 3)
			{
				this.currentTarget = new EntityGraphIdentifier
				{
					serviceID = int.Parse(array[1]),
					entityID = (long)EntityGraphNode.ServiceEntityID
				};
			}
			else
			{
				this.currentTarget = new EntityGraphIdentifier
				{
					serviceID = int.Parse(array[1]),
					entityID = (long.Parse(array[2]) << 32) + long.Parse(array[3])
				};
			}
			this.peer.Transmit(SerializeWriter.ToBinary<RequestUnderingListMessage>(new RequestUnderingListMessage(this.currentTarget)));
		}

		public void QueryUnderingList(string Text, long selectedEID)
		{
			if (Text == this.parent.ServicesString)
			{
				this.currentTarget = new EntityGraphIdentifier
				{
					category = "ReportService",
					entityID = (long)EntityGraphNode.ServiceEntityID
				};
				this.peer.Transmit(SerializeWriter.ToBinary<RequestLookUpInfoMessage>(new RequestLookUpInfoMessage("ReportService")));
				return;
			}
			string[] array = Text.Split(this.parent.delim);
			if (array.Length == 3)
			{
				this.currentTarget = new EntityGraphIdentifier
				{
					serviceID = int.Parse(array[1]),
					entityID = (long)EntityGraphNode.ServiceEntityID
				};
			}
			else
			{
				this.currentTarget = new EntityGraphIdentifier
				{
					serviceID = int.Parse(array[1]),
					entityID = (long.Parse(array[2]) << 32) + long.Parse(array[3])
				};
			}
			this.peer.Transmit(SerializeWriter.ToBinary<RequestUnderingListMessage>(new RequestUnderingListMessage(this.currentTarget, selectedEID)));
		}

		public void QuerySelect(string category, long entityID)
		{
			this.peer.Transmit(SerializeWriter.ToBinary<RequestSelectMessage>(new RequestSelectMessage(category, entityID)));
		}

		public void QueryLookUpInfo(int targetServiceID)
		{
			this.peer.Transmit(SerializeWriter.ToBinary<RequestLookUpInfoMessage>(new RequestLookUpInfoMessage(targetServiceID)));
		}

		public void QueryOperationTime(int tSID, long tEID, string uCategory, long uEID)
		{
			this.peer.Transmit(SerializeWriter.ToBinary<RequestOperationTimeReportMessage>(new RequestOperationTimeReportMessage(tSID, tEID, uCategory, uEID)));
		}

		public void QueryShutDownEntity(int tSID, long tEID)
		{
			this.peer.Transmit(SerializeWriter.ToBinary<RequestShutDownEntityMessage>(new RequestShutDownEntityMessage(tSID, tEID)));
		}

		public void EnableOperationReport(int tSID, bool enable)
		{
			this.peer.Transmit(SerializeWriter.ToBinary<EnableOperationTimeReportMessage>(new EnableOperationTimeReportMessage(tSID, enable)));
		}

		public void ProcessMessage(object message)
		{
			if (message is ReportLookUpInfoMessage)
			{
				ReportLookUpInfoMessage reportLookUpInfoMessage = message as ReportLookUpInfoMessage;
				if (this.parent.OnWaiting)
				{
					if (this.parent.CurrentNodeTier == 1)
					{
						this.parent.Invoke(new Action(delegate
						{
							this.parent.ReadyInsertion();
						}));
						ServerPair[] serverlist = reportLookUpInfoMessage.serverlist;
						for (int i = 0; i < serverlist.Length; i++)
						{
							ServerPair v = serverlist[i];
							this.parent.Invoke(new Action(delegate
							{
								this.parent.Insertion(string.Format("[{0}]{1}", v.code, v.category));
							}));
						}
						this.parent.Invoke(new Action(delegate
						{
							this.parent.FinishInsertion();
						}));
					}
					else
					{
						StringBuilder buffer = new StringBuilder();
						foreach (ServerPair serverPair in reportLookUpInfoMessage.serverlist)
						{
						}
						buffer.AppendLine();
						buffer.AppendLine(string.Format("{0} entites under this service.", reportLookUpInfoMessage.EntityCount));
						this.parent.Invoke(new Action(delegate
						{
							this.parent.InvokeMessageBox(buffer.ToString());
						}));
					}
				}
			}
			if (message is ReportUnderingListMessage)
			{
				ReportUnderingListMessage reportUnderingListMessage = message as ReportUnderingListMessage;
				this.parent.Invoke(new Action(delegate
				{
					this.parent.ReadyInsertion();
				}));
				EntityGraphIdentifier[] serverlist3 = reportUnderingListMessage.serverlist;
				for (int k = 0; k < serverlist3.Length; k++)
				{
					EntityGraphIdentifier entityGraphIdentifier = serverlist3[k];
					string output = "";
					string arg;
					if (entityGraphIdentifier.entityID == (long)EntityGraphNode.ServiceEntityID)
					{
						arg = string.Format("{0},{1}", 0, EntityGraphNode.ServiceEntityID);
					}
					else
					{
						arg = string.Format("{0},{1}", entityGraphIdentifier.entityID >> 32, entityGraphIdentifier.entityID & (long)0xffffffffL);
					}
					if (entityGraphIdentifier.isCategoric)
					{
						output = string.Format("[{0}/{1}] {2}", entityGraphIdentifier.category, arg, entityGraphIdentifier.states);
					}
					if (entityGraphIdentifier.isNumeric)
					{
						output = string.Format("[{0}/{1}] {2}", entityGraphIdentifier.serviceID, arg, entityGraphIdentifier.states);
					}
					this.parent.Invoke(new Action(delegate
					{
						this.parent.Insertion(output, "");
					}));
				}
				this.parent.Invoke(new Action(delegate
				{
					this.parent.FinishInsertion();
				}));
			}
			if (message is ReportSelectMessage)
			{
				ReportSelectMessage m = message as ReportSelectMessage;
				if (m.serviceID == EntityGraphNode.BadServiceID)
				{
					this.parent.Invoke(new Action(delegate
					{
						this.parent.SelectException();
					}));
				}
				this.parent.Invoke(new Action(delegate
				{
					this.parent.AdjustCurrentNode(m.serviceID, m.entityID);
				}));
			}
			if (message is ReportOperationTimeReportMessage)
			{
				ReportOperationTimeReportMessage m = message as ReportOperationTimeReportMessage;
				this.parent.Invoke(new Action(delegate
				{
					this.parent.InvokeGridBox(m.ReportString);
				}));
			}
		}

		private TcpClient peer = new TcpClient();

		private ReportServiceClientForm parent;

		private JobProcessor thread = new JobProcessor();

		private bool valid;

		private EntityGraphIdentifier currentTarget;

		private MessageHandlerFactory mf = new MessageHandlerFactory();
	}
}
