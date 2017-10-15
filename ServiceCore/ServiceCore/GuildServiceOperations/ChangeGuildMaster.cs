using System;
using System.Collections.Generic;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.GuildServiceOperations
{
	[Serializable]
	public sealed class ChangeGuildMaster : Operation
	{
		public string OldMasterName { get; set; }

		public string NewMasterName { get; set; }

		public ChangeGuildMaster.ResultCodeEnum ResultCode
		{
			get
			{
				return this.resultCode;
			}
			set
			{
				this.resultCode = value;
			}
		}

		public ChangeGuildMaster(string oldMasterName, string newMasterName)
		{
			this.OldMasterName = oldMasterName;
			this.NewMasterName = newMasterName;
		}

		public override OperationProcessor RequestProcessor()
		{
			return new ChangeGuildMaster.Request(this);
		}

		[NonSerialized]
		private ChangeGuildMaster.ResultCodeEnum resultCode;

		public enum ResultCodeEnum
		{
			ChangeMaster_Unknown,
			ChangeMaster_Success,
			ChangeMaster_NotExistGuild,
			ChangeMaster_NotMatchOldMaster,
			ChangeMaster_NotFoundNewMaster,
			ChangeMaster_LowRankOldMaster,
			ChangeMaster_EqualNewAndOldID,
			ChangeMaster_SystemError,
			ChangeMaster_SyncFailed
		}

		private class Request : OperationProcessor<ChangeGuildMaster>
		{
			public Request(ChangeGuildMaster op) : base(op)
			{
			}

			public override IEnumerable<object> Run()
			{
				yield return null;
				if (base.Feedback is ChangeGuildMaster.ResultCodeEnum)
				{
					base.Operation.ResultCode = (ChangeGuildMaster.ResultCodeEnum)base.Feedback;
				}
				else
				{
					base.Operation.ResultCode = ChangeGuildMaster.ResultCodeEnum.ChangeMaster_Unknown;
				}
				yield break;
			}
		}
	}
}
