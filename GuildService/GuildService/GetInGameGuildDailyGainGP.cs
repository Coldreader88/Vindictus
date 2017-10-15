using System;
using System.Data.Linq.Mapping;

namespace GuildService
{
	public class GetInGameGuildDailyGainGP
	{
		[Column(Storage = "_GPGainType", DbType = "TinyInt NOT NULL")]
		public byte GPGainType
		{
			get
			{
				return this._GPGainType;
			}
			set
			{
				if (this._GPGainType != value)
				{
					this._GPGainType = value;
				}
			}
		}

		[Column(Storage = "_Point", DbType = "Int NOT NULL")]
		public int Point
		{
			get
			{
				return this._Point;
			}
			set
			{
				if (this._Point != value)
				{
					this._Point = value;
				}
			}
		}

		private byte _GPGainType;

		private int _Point;
	}
}
