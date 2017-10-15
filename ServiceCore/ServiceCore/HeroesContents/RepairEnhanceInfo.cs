using System;
using System.Data.Linq.Mapping;

namespace ServiceCore.HeroesContents
{
	[Table(Name = "dbo.RepairEnhanceInfo")]
	public class RepairEnhanceInfo
	{
		[Column(Storage = "_EnhanceLevel", DbType = "Int NOT NULL")]
		public int EnhanceLevel
		{
			get
			{
				return this._EnhanceLevel;
			}
			set
			{
				if (this._EnhanceLevel != value)
				{
					this._EnhanceLevel = value;
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

		private int _EnhanceLevel;

		private int _Point;

		private int _MaxDurabilityRecoveryPoint;
	}
}
