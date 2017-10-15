using System;
using System.Data.Linq.Mapping;

namespace ServiceCore.HeroesContents
{
	[Table(Name = "dbo.RepairQualityInfo")]
	public class RepairQualityInfo
	{
		[Column(Storage = "_Quality", DbType = "Int NOT NULL")]
		public int Quality
		{
			get
			{
				return this._Quality;
			}
			set
			{
				if (this._Quality != value)
				{
					this._Quality = value;
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

		[Column(Storage = "_MaxDurabilityRecoveryPoint", DbType = "Int NOT NULL")]
		public int MaxDurabilityRecoveryPoint
		{
			get
			{
				return this._MaxDurabilityRecoveryPoint;
			}
			set
			{
				if (this._MaxDurabilityRecoveryPoint != value)
				{
					this._MaxDurabilityRecoveryPoint = value;
				}
			}
		}

		private int _Quality;

		private int _Point;

		private int _MaxDurabilityRecoveryPoint;
	}
}
