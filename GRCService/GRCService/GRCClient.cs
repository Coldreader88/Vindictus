using System;
using Devcat.Core.Threading;
using GRCService;
using ServiceCore;
using ServiceCore.EndPointNetwork;
using ServiceCore.FrontendServiceOperations;
using UnifiedNetwork.Entity;
using Utility;

namespace GRCServiceCore
{
	public class GRCClient
	{
		public GRCService Service { get; private set; }

		public IEntity Entity { get; private set; }

		public IEntityProxy FrontendConn { get; set; }

		public GRCClient(GRCService service, IEntity entity)
		{
			this.Service = service;
			this.Entity = entity;
			this.LastQuestion = "";
			this.LastAnswer = "";
			this.State = GRCClient.GRCClientState.RequestSending;
		}

		internal void MakeRequest()
		{
			if (!FeatureMatrix.IsEnable("GRCService"))
			{
				if (!this.Entity.IsClosed)
				{
					this.Entity.Close();
				}
				return;
			}
			if (this.Entity.IsClosed)
			{
				return;
			}
			if (this.FrontendConn == null)
			{
				this.Entity.Close();
				return;
			}
			if (this.State == GRCClient.GRCClientState.RespondWaiting)
			{
				Log<GRCService>.Logger.InfoFormat("GRCClient received no reply", new object[0]);
				using (GRCServiceErrorLogDataContext grcserviceErrorLogDataContext = new GRCServiceErrorLogDataContext())
				{
					try
					{
						grcserviceErrorLogDataContext.AddGRCServiceError(new long?(this.Entity.ID), new int?(1), "NoReply");
					}
					catch (Exception ex)
					{
						Log<GRCService>.Logger.Error(string.Format("GRCService Error while making log : [CharacterID = {0}]", this.Entity.ID), ex);
					}
				}
				this.Entity.Close();
				return;
			}
			this.Service.Content.MakeQuestionRandom(out this.LastQuestion, out this.LastAnswer);
			if (this.LastAnswer.Length > 0 || this.LastQuestion.Length > 0)
			{
				GameResourceRequestMessage serializeObject = new GameResourceRequestMessage(0, this.LastQuestion);
				this.FrontendConn.RequestOperation(SendPacket.Create<GameResourceRequestMessage>(serializeObject));
				this.State = GRCClient.GRCClientState.RespondWaiting;
			}
			else
			{
				this.State = GRCClient.GRCClientState.RequestSkipped;
			}
			Scheduler.Schedule(this.Service.Thread, Job.Create(new Action(this.MakeRequest)), this.Service.NextMilliseconds());
		}

		public string LastQuestion;

		public string LastAnswer;

		public GRCClient.GRCClientState State;

		public enum GRCClientState
		{
			RequestSending,
			RequestSkipped,
			RespondWaiting,
			RespondTimeout,
			RespondReceived
		}
	}
}
