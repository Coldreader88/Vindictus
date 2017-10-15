using System;
using System.Data.Linq.Mapping;

namespace ServiceCore.HeroesContents
{
	[Table(Name = "dbo.ItemTradeLimitInfo")]
	public class ItemTradeLimitInfo
	{
		[Column(Storage = "_ItemClass", DbType = "NVarChar(256) NOT NULL", CanBeNull = false)]
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

		[Column(Storage = "_MaxLimitValue", DbType = "Int")]
		public int? MaxLimitValue
		{
			get
			{
				return this._MaxLimitValue;
			}
			set
			{
				if (this._MaxLimitValue != value)
				{
					this._MaxLimitValue = value;
				}
			}
		}

		private string _ItemClass;

		private int? _MaxLimitValue;
	}
}
