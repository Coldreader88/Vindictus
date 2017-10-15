using System;
using System.Data.Linq.Mapping;

namespace ServiceCore.HeroesContents
{
	[Table(Name = "dbo.TownCampfireStatusInfo")]
	public class TownCampfireStatusInfo
	{
		[Column(Storage = "_Day", DbType = "NVarChar(50) NOT NULL", CanBeNull = false)]
		public string Day
		{
			get
			{
				return this._Day;
			}
			set
			{
				if (this._Day != value)
				{
					this._Day = value;
				}
			}
		}

		[Column(Storage = "_StatusEffect", DbType = "NVarChar(50)")]
		public string StatusEffect
		{
			get
			{
				return this._StatusEffect;
			}
			set
			{
				if (this._StatusEffect != value)
				{
					this._StatusEffect = value;
				}
			}
		}

		private string _Day;

		private string _StatusEffect;
	}
}
