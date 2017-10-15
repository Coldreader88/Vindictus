using System;
using System.Collections.Generic;
using GuildService.API;
using ServiceCore.GuildServiceOperations;
using UnifiedNetwork.Cooperation;
using Utility;

namespace GuildService.Processor
{
	internal class UpdateGuildCharacterNameProcessor : OperationProcessor<UpdateGuildCharacterName>
	{
		public UpdateGuildCharacterNameProcessor(GuildService service, UpdateGuildCharacterName op) : base(op)
		{
			this.service = service;
		}

		public override IEnumerable<object> Run()
		{
			try
			{
				GuildAPI.GetAPI().UserNameModify(base.Operation.Key, base.Operation.NewName);
			}
			catch (Exception ex)
			{
				Log<GuildService>.Logger.Error("UserNameModify Error", ex);
			}
			base.Finished = true;
			yield return new OkMessage();
			yield break;
		}

		private GuildService service;
	}
}
