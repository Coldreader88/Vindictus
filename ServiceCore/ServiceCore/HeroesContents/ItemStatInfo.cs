using System;
using System.Data.Linq.Mapping;

namespace ServiceCore.HeroesContents
{
	[Table(Name = "dbo.ItemStatInfo")]
	public class ItemStatInfo
	{
		[Column(Name = "[Order]", Storage = "_Order", DbType = "Int NOT NULL")]
		public int Order
		{
			get
			{
				return this._Order;
			}
			set
			{
				if (this._Order != value)
				{
					this._Order = value;
				}
			}
		}

		[Column(Storage = "_StatName", DbType = "VarChar(50) NOT NULL", CanBeNull = false)]
		public string StatName
		{
			get
			{
				return this._StatName;
			}
			set
			{
				if (this._StatName != value)
				{
					this._StatName = value;
				}
			}
		}

		[Column(Name = "[Desc]", Storage = "_Desc", DbType = "VarChar(255) NOT NULL", CanBeNull = false)]
		public string Desc
		{
			get
			{
				return this._Desc;
			}
			set
			{
				if (this._Desc != value)
				{
					this._Desc = value;
				}
			}
		}

		private int _Order;

		private string _StatName;

		private string _Desc;
	}
}
