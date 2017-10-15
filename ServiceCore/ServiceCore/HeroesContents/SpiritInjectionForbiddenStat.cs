using System;
using System.Data.Linq.Mapping;

namespace ServiceCore.HeroesContents
{
	[Table(Name = "dbo.SpiritInjectionForbiddenStat")]
	public class SpiritInjectionForbiddenStat
	{
		[Column(Storage = "_TradeCategory", DbType = "NVarChar(50) NOT NULL", CanBeNull = false)]
		public string TradeCategory
		{
			get
			{
				return this._TradeCategory;
			}
			set
			{
				if (this._TradeCategory != value)
				{
					this._TradeCategory = value;
				}
			}
		}

		[Column(Storage = "_ForbiddenStat", DbType = "NVarChar(50) NOT NULL", CanBeNull = false)]
		public string ForbiddenStat
		{
			get
			{
				return this._ForbiddenStat;
			}
			set
			{
				if (this._ForbiddenStat != value)
				{
					this._ForbiddenStat = value;
				}
			}
		}

		private string _TradeCategory;

		private string _ForbiddenStat;
	}
}
