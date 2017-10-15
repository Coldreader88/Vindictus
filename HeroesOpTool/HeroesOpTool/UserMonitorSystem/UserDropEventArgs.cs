using System;

namespace HeroesOpTool.UserMonitorSystem
{
	public class UserDropEventArgs : MainForm.AlarmEventArgsBase
	{
		public override string Message
		{
			get
			{
				return string.Format(LocalizeText.Get(423), this.server, this.channel, this.userDiff);
			}
		}

		public UserDropEventArgs(string _server, string _channel, int _userDiff)
		{
			this.server = _server;
			this.channel = _channel;
			this.userDiff = _userDiff;
		}

		private string server;

		private string channel;

		private int userDiff;
	}
}
