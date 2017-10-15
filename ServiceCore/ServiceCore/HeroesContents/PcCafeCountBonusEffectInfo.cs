using System;
using System.Data.Linq.Mapping;

namespace ServiceCore.HeroesContents
{
	[Table(Name = "dbo.PcCafeCountBonusEffectInfo")]
	public class PcCafeCountBonusEffectInfo
	{
		[Column(Storage = "_StatusEffectLevel", DbType = "Int NOT NULL")]
		public int StatusEffectLevel
		{
			get
			{
				return this._StatusEffectLevel;
			}
			set
			{
				if (this._StatusEffectLevel != value)
				{
					this._StatusEffectLevel = value;
				}
			}
		}

		[Column(Storage = "_MinUserCount", DbType = "Int NOT NULL")]
		public int MinUserCount
		{
			get
			{
				return this._MinUserCount;
			}
			set
			{
				if (this._MinUserCount != value)
				{
					this._MinUserCount = value;
				}
			}
		}

		[Column(Storage = "_MaxUserCount", DbType = "Int NOT NULL")]
		public int MaxUserCount
		{
			get
			{
				return this._MaxUserCount;
			}
			set
			{
				if (this._MaxUserCount != value)
				{
					this._MaxUserCount = value;
				}
			}
		}

		private int _StatusEffectLevel;

		private int _MinUserCount;

		private int _MaxUserCount;
	}
}
