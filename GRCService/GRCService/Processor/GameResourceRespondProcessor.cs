using System;
using System.Collections.Generic;
using GRCService;
using ServiceCore.GRCServiceOperations;
using UnifiedNetwork.Entity;
using Utility;

namespace GRCServiceCore.Processor
{
	internal class GameResourceRespondProcessor : EntityProcessor<GameResourceRespond, GRCClient>
	{
		private GRCService Service { get; set; }

		public GameResourceRespondProcessor(GRCService service, GameResourceRespond op) : base(op)
		{
			this.Service = service;
		}

		public override IEnumerable<object> Run()
		{
			string lastAnswer = base.Entity.LastAnswer;
			if (lastAnswer.ToLower() == base.Operation.RespondParam.ToLower())
			{
				Log<GameResourceRespondProcessor>.Logger.InfoFormat("GameResourceRespond: Correct answer, [LastAnswer={0}, LastQuestion={1}]", base.Entity.LastAnswer, base.Entity.LastQuestion);
			}
			else
			{
				Log<GameResourceRespondProcessor>.Logger.InfoFormat("GameResourceRespond: Wrong answer, LastAnswer={0}, LastQuestion={1}, LastReply={2}", base.Entity.LastAnswer, base.Entity.LastQuestion, base.Operation.RespondParam);
				using (GRCServiceErrorLogDataContext grcserviceErrorLogDataContext = new GRCServiceErrorLogDataContext())
				{
					try
					{
						grcserviceErrorLogDataContext.AddGRCServiceError(new long?(base.Operation.CharacterID), new int?(1), "WrongAnswer");
					}
					catch (Exception ex)
					{
						Log<GameResourceRespondProcessor>.Logger.Error(string.Format("GameResourceRespondProcessor Error while making log : [CharacterID = {0}]", base.Operation.CharacterID), ex);
					}
				}
			}
			base.Entity.State = GRCClient.GRCClientState.RespondReceived;
			base.Finished = true;
			yield break;
		}
	}
}
