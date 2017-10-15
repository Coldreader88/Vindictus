using System;
using System.Data.Linq.Mapping;

namespace GuildService
{
	public class GetInGameGuildCharacterInfo
	{
		[Column(Storage = "_CID", DbType = "BigInt NOT NULL")]
		public long CID
		{
			get
			{
				return this._CID;
			}
			set
			{
				if (this._CID != value)
				{
					this._CID = value;
				}
			}
		}

		[Column(Storage = "_Point", DbType = "BigInt NOT NULL")]
		public long Point
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

		private long _CID;

		private long _Point;
	}
}
