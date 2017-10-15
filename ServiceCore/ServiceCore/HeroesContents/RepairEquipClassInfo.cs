using System;
using System.Data.Linq.Mapping;

namespace ServiceCore.HeroesContents
{
	[Table(Name = "dbo.RepairEquipClassInfo")]
	public class RepairEquipClassInfo
	{
		[Column(Storage = "_EquipClass", DbType = "NVarChar(50) NOT NULL", CanBeNull = false)]
		public string EquipClass
		{
			get
			{
				return this._EquipClass;
			}
			set
			{
				if (this._EquipClass != value)
				{
					this._EquipClass = value;
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

		private string _EquipClass;

		private int _Point;

		private int _MaxDurabilityRecoveryPoint;
	}
}
