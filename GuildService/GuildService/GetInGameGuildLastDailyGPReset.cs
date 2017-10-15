using System;
using System.Data.Linq.Mapping;

namespace GuildService
{
	public class GetInGameGuildLastDailyGPReset
	{
		[Column(Storage = "_LastDailyGPReset", DbType = "DateTime NOT NULL")]
		public DateTime LastDailyGPReset
		{
			get
			{
				return this._LastDailyGPReset;
			}
			set
			{
				if (this._LastDailyGPReset != value)
				{
					this._LastDailyGPReset = value;
				}
			}
		}

		private DateTime _LastDailyGPReset;
	}
}
