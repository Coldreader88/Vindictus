using System;
using System.Collections.Generic;
using System.Net;
using Devcat.Core;
using Devcat.Core.Net;
using Devcat.Core.Net.Message;
using UnifiedNetwork.Cooperation;
using UnifiedNetwork.Entity;
using UnifiedNetwork.Entity.Operations;
using UnifiedNetwork.EntityGraph;
using UnifiedNetwork.ReportService.Operations;
using Utility;

namespace UnifiedNetwork.ReportService
{
	public class ReportAdminClient
	{
		public ReportAdminClient(ReportService p, TcpClient t)
		{
			this.parent = p;
			this.peer = t;
			t.ConnectionSucceed += this.OnConnectionSucceed;
			t.ConnectionFail += this.OnConnectionFail;
			t.Disconnected += this.OnDisconnected;
			t.ExceptionOccur += this.OnException;
			t.PacketReceive += this.OnPacketReceive;
		}

		private void OnConnectionFail(object sender, EventArgs<Exception> e)
		{
			Log<ReportAdminClient>.Logger.Error("Watch Admin Connection Failed! :");
			this.OnException(sender, e);
		}

		private void OnConnectionSucceed(object sender, EventArgs e)
		{
			Log<ReportAdminClient>.Logger.Debug("Watch Admin Connected");
		}

		private void OnPacketReceive(object sender, EventArgs<ArraySegment<byte>> e)
		{
			Packet packet = new Packet(e.Value);
			this.parent.MF.Handle(packet, this);
		}

		private void OnException(object sender, EventArgs<Exception> e)
		{
			Log<ReportAdminClient>.Logger.Error("[Exception] Watch admin exception", e.Value);
			this.Disconnect();
		}

		public void Transmit(Packet packet)
		{
			this.peer.Transmit(packet);
		}

		public void Disconnect()
		{
			this.peer.Disconnect();
		}

		private void OnDisconnected(object sender, EventArgs e)
		{
			Log<ReportAdminClient>.Logger.Debug("[Disconnect] Watch Admin disconnected");
		}

		public void ProcessMessage(object message)
		{
			if (message is RequestSelectMessage)
			{
				RequestSelectMessage m = message as RequestSelectMessage;
				Log<ReportAdminClient>.Logger.Debug(m.ToString());
				int[] array = this.parent.LookUp.ReportLookUpInfo().ToList(m.category).ToArray();
				if (array.Length == 0)
				{
					this.Transmit(SerializeWriter.ToBinary<ReportSelectMessage>(new ReportSelectMessage(EntityGraphNode.BadServiceID, m.entityID)));
					return;
				}
				int queryTargetID = array[0];
				Log<ReportAdminClient>.Logger.DebugFormat("Query select [{0},{1}] to service {2}", m.category, m.entityID, queryTargetID);
				new Location(m.entityID, m.category);
				SelectEntity selectop = new SelectEntity
				{
					ID = m.entityID,
					Category = m.category
				};
				selectop.OnComplete += delegate(Operation opq)
				{
					switch (selectop.Result)
					{
					case SelectEntity.ResultCode.Ok:
						this.Transmit(SerializeWriter.ToBinary<ReportSelectMessage>(new ReportSelectMessage(queryTargetID, m.entityID)));
						return;
					case SelectEntity.ResultCode.Redirect:
						this.Transmit(SerializeWriter.ToBinary<ReportSelectMessage>(new ReportSelectMessage(selectop.RedirectServiceID, m.entityID)));
						return;
					default:
						this.Transmit(SerializeWriter.ToBinary<ReportSelectMessage>(new ReportSelectMessage(EntityGraphNode.BadServiceID, m.entityID)));
						return;
					}
				};
				this.parent.RequestOperation(queryTargetID, selectop);
			}
			if (message is RequestLookUpInfoMessage)
			{
				RequestLookUpInfoMessage requestLookUpInfoMessage = message as RequestLookUpInfoMessage;
				Log<ReportAdminClient>.Logger.Debug(requestLookUpInfoMessage.ToString());
				if (requestLookUpInfoMessage.Target.category.Equals("ReportService"))
				{
					Dictionary<int, KeyValuePair<string, IPEndPoint>> dic = this.parent.ReportExtendedLookUpInfo();
					this.peer.Transmit(SerializeWriter.ToBinary<ReportLookUpInfoMessage>(new ReportLookUpInfoMessage(dic, this.parent.ReportUnderingCounts())));
				}
				else
				{
					if (requestLookUpInfoMessage.Target.code == 0)
					{
						RequestLookUpInfo op = new RequestLookUpInfo(new EntityGraphIdentifier
						{
							category = requestLookUpInfoMessage.Target.category,
							entityID = (long)EntityGraphNode.ServiceEntityID
						});
						op.OnComplete += delegate(Operation ops)
						{
							this.peer.Transmit(SerializeWriter.ToBinary<ReportLookUpInfoMessage>(op.Result));
						};
						this.parent.RequestOperation(requestLookUpInfoMessage.Target.category, op);
					}
					else
					{
						RequestLookUpInfo op = new RequestLookUpInfo(new EntityGraphIdentifier
						{
							serviceID = requestLookUpInfoMessage.Target.code,
							entityID = (long)EntityGraphNode.ServiceEntityID
						});
						op.OnComplete += delegate(Operation ops)
						{
							this.peer.Transmit(SerializeWriter.ToBinary<ReportLookUpInfoMessage>(op.Result));
						};
						this.parent.RequestOperation(requestLookUpInfoMessage.Target.code, op);
					}
				}
			}
			if (message is RequestUnderingListMessage)
			{
				RequestUnderingListMessage requestUnderingListMessage = message as RequestUnderingListMessage;
				Log<ReportAdminClient>.Logger.Debug(requestUnderingListMessage.ToString());
				if (requestUnderingListMessage.Target.isNumeric)
				{
					RequestUnderingList op;
					if (requestUnderingListMessage.isIncluded)
					{
						op = new RequestUnderingList(requestUnderingListMessage.Target, requestUnderingListMessage.includedEID);
					}
					else
					{
						op = new RequestUnderingList(requestUnderingListMessage.Target);
					}
					op.OnComplete += delegate(Operation ops)
					{
						if (op.Message != null)
						{
							this.Transmit(SerializeWriter.ToBinary<ReportUnderingListMessage>(op.Message));
							return;
						}
						Log<ReportAdminClient>.Logger.Error("Null Message returned");
						this.Transmit(SerializeWriter.ToBinary<ReportUnderingListMessage>(new ReportUnderingListMessage()));
					};
					this.parent.RequestOperation(requestUnderingListMessage.Target.serviceID, op);
				}
				if (requestUnderingListMessage.Target.isCategoric)
				{
					this.Transmit(SerializeWriter.ToBinary<ReportUnderingListMessage>(new ReportUnderingListMessage()));
				}
			}
			if (message is RequestOperationTimeReportMessage)
			{
				RequestOperationTimeReportMessage requestOperationTimeReportMessage = message as RequestOperationTimeReportMessage;
				Log<ReportAdminClient>.Logger.Debug(requestOperationTimeReportMessage.ToString());
				RequestOperationTimeReport op = new RequestOperationTimeReport(new EntityGraphIdentifier
				{
					serviceID = requestOperationTimeReportMessage.serviceID,
					entityID = requestOperationTimeReportMessage.entityID
				}, new EntityGraphIdentifier
				{
					category = requestOperationTimeReportMessage.targetCategory,
					entityID = requestOperationTimeReportMessage.targetEntityID
				});
				op.OnComplete += delegate(Operation ops)
				{
					if (op.Message != null)
					{
						this.Transmit(SerializeWriter.ToBinary<ReportOperationTimeReportMessage>(op.Message));
						return;
					}
					Log<ReportAdminClient>.Logger.Error("Null Message returned");
					this.Transmit(SerializeWriter.ToBinary<ReportOperationTimeReportMessage>(new ReportOperationTimeReportMessage()));
				};
				this.parent.RequestOperation(requestOperationTimeReportMessage.serviceID, op);
			}
			if (message is EnableOperationTimeReportMessage)
			{
				EnableOperationTimeReportMessage enableOperationTimeReportMessage = message as EnableOperationTimeReportMessage;
				Log<ReportAdminClient>.Logger.Debug(enableOperationTimeReportMessage.ToString());
				EnableOperationTimeReport op3 = new EnableOperationTimeReport(new EntityGraphIdentifier
				{
					serviceID = enableOperationTimeReportMessage.serviceID,
					entityID = (long)EntityGraphNode.ServiceEntityID
				}, enableOperationTimeReportMessage.enable);
				this.parent.RequestOperation(enableOperationTimeReportMessage.serviceID, op3);
			}
			if (message is RequestShutDownEntityMessage)
			{
				RequestShutDownEntityMessage requestShutDownEntityMessage = message as RequestShutDownEntityMessage;
				Log<ReportAdminClient>.Logger.Debug(requestShutDownEntityMessage.ToString());
				RequestShutDownEntity op2 = new RequestShutDownEntity(new EntityGraphIdentifier
				{
					serviceID = requestShutDownEntityMessage.serviceID,
					entityID = requestShutDownEntityMessage.entityID
				});
				this.parent.RequestOperation(requestShutDownEntityMessage.serviceID, op2);
			}
		}

		private TcpClient peer;

		private ReportService parent;
	}
}
