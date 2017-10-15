using System;
using System.Data.Linq.Mapping;

namespace ServiceCore.HeroesContents
{
	[Table(Name = "dbo.DecompositionCategory")]
	public class DecompositionCategory
	{
		[Column(Storage = "_TradeCategorySub", DbType = "NVarChar(50) NOT NULL", CanBeNull = false)]
		public string TradeCategorySub
		{
			get
			{
				return this._TradeCategorySub;
			}
			set
			{
				if (this._TradeCategorySub != value)
				{
					this._TradeCategorySub = value;
				}
			}
		}

		[Column(Storage = "_ManufactureID", DbType = "NVarChar(16) NOT NULL", CanBeNull = false)]
		public string ManufactureID
		{
			get
			{
				return this._ManufactureID;
			}
			set
			{
				if (this._ManufactureID != value)
				{
					this._ManufactureID = value;
				}
			}
		}

		private string _TradeCategorySub;

		private string _ManufactureID;
	}
}
