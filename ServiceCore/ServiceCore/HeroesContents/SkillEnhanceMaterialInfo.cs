using System;
using System.Data.Linq.Mapping;

namespace ServiceCore.HeroesContents
{
	[Table(Name = "dbo.SkillEnhanceMaterialInfo")]
	public class SkillEnhanceMaterialInfo
	{
		public bool IsFixedRatio
		{
			get
			{
				return this.MinIncreaseRatio == this.MaxIncreaseRatio;
			}
		}

		[Column(Storage = "_MaterialItemClass", DbType = "NVarChar(50) NOT NULL", CanBeNull = false)]
		public string MaterialItemClass
		{
			get
			{
				return this._MaterialItemClass;
			}
			set
			{
				if (this._MaterialItemClass != value)
				{
					this._MaterialItemClass = value;
				}
			}
		}

		[Column(Storage = "_Type", DbType = "TinyInt NOT NULL")]
		public byte Type
		{
			get
			{
				return this._Type;
			}
			set
			{
				if (this._Type != value)
				{
					this._Type = value;
				}
			}
		}

		[Column(Storage = "_MinIncreaseRatio", DbType = "Int NOT NULL")]
		public int MinIncreaseRatio
		{
			get
			{
				return this._MinIncreaseRatio;
			}
			set
			{
				if (this._MinIncreaseRatio != value)
				{
					this._MinIncreaseRatio = value;
				}
			}
		}

		[Column(Storage = "_MaxIncreaseRatio", DbType = "Int NOT NULL")]
		public int MaxIncreaseRatio
		{
			get
			{
				return this._MaxIncreaseRatio;
			}
			set
			{
				if (this._MaxIncreaseRatio != value)
				{
					this._MaxIncreaseRatio = value;
				}
			}
		}

		private string _MaterialItemClass;

		private byte _Type;

		private int _MinIncreaseRatio;

		private int _MaxIncreaseRatio;
	}
}
