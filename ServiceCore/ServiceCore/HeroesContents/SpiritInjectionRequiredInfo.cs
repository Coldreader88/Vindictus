using System;
using System.Data.Linq.Mapping;

namespace ServiceCore.HeroesContents
{
	[Table(Name = "dbo.SpiritInjectionRequiredInfo")]
	public class SpiritInjectionRequiredInfo
	{
		[Column(Storage = "_ItemClass", DbType = "NVarChar(50) NOT NULL", CanBeNull = false)]
		public string ItemClass
		{
			get
			{
				return this._ItemClass;
			}
			set
			{
				if (this._ItemClass != value)
				{
					this._ItemClass = value;
				}
			}
		}

		[Column(Storage = "_LevelRange", DbType = "SmallInt NOT NULL")]
		public short LevelRange
		{
			get
			{
				return this._LevelRange;
			}
			set
			{
				if (this._LevelRange != value)
				{
					this._LevelRange = value;
				}
			}
		}

		[Column(Storage = "_AP", DbType = "Int NOT NULL")]
		public int AP
		{
			get
			{
				return this._AP;
			}
			set
			{
				if (this._AP != value)
				{
					this._AP = value;
				}
			}
		}

		[Column(Storage = "_Price", DbType = "Int NOT NULL")]
		public int Price
		{
			get
			{
				return this._Price;
			}
			set
			{
				if (this._Price != value)
				{
					this._Price = value;
				}
			}
		}

		private string _ItemClass;

		private short _LevelRange;

		private int _AP;

		private int _Price;
	}
}
