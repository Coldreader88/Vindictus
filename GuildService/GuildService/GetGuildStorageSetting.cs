using System;
using System.Data.Linq.Mapping;

namespace GuildService
{
	public class GetGuildStorageSetting
	{
		[Column(Storage = "_AccessLimitTag", DbType = "BigInt NOT NULL")]
		public long AccessLimitTag
		{
			get
			{
				return this._AccessLimitTag;
			}
			set
			{
				if (this._AccessLimitTag != value)
				{
					this._AccessLimitTag = value;
				}
			}
		}

		[Column(Storage = "_GoldLimit", DbType = "Int NOT NULL")]
		public int GoldLimit
		{
			get
			{
				return this._GoldLimit;
			}
			set
			{
				if (this._GoldLimit != value)
				{
					this._GoldLimit = value;
				}
			}
		}

		private long _AccessLimitTag;

		private int _GoldLimit;
	}
}
