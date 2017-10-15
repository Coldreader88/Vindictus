using System;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class PvpRegisterResultMessage : IMessage
	{
		public string PvpMode { get; set; }

		public bool UsePendingDialog { get; set; }

		public bool StartWaitingTimer { get; set; }

		public int State { get; set; }

		public HeroesString Message { get; set; }

		public PvpRegisterResultMessage(string pvpMode, PvpRegisterResult state, bool usePendingDialog, bool startWaitingTimer, string format, params object[] args)
		{
			this.PvpMode = pvpMode;
			this.State = (int)state;
			this.UsePendingDialog = usePendingDialog;
			this.StartWaitingTimer = startWaitingTimer;
			this.Message = new HeroesString(format, args);
		}

		public PvpRegisterResultMessage(string pvpMode, PvpRegisterResult state, bool usePendingDialog, bool startWaitingTimer, HeroesString message)
		{
			this.PvpMode = pvpMode;
			this.State = (int)state;
			this.UsePendingDialog = usePendingDialog;
			this.StartWaitingTimer = startWaitingTimer;
			this.Message = message;
		}

		public override string ToString()
		{
			return string.Format("PvpRegisterResultMessage[ {0} : {1} ]", this.State, this.Message);
		}
	}
}
