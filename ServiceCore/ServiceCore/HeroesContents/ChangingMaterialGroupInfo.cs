using System;
using System.Data.Linq.Mapping;

namespace ServiceCore.HeroesContents
{
	[Table(Name = "dbo.ChangingMaterialGroupInfo")]
	public class ChangingMaterialGroupInfo
	{
		[Column(Storage = "_ID", DbType = "Int NOT NULL")]
		public int ID
		{
			get
			{
				return this._ID;
			}
			set
			{
				if (this._ID != value)
				{
					this._ID = value;
				}
			}
		}

		[Column(Storage = "_Material", DbType = "VarChar(50) NOT NULL", CanBeNull = false)]
		public string Material
		{
			get
			{
				return this._Material;
			}
			set
			{
				if (this._Material != value)
				{
					this._Material = value;
				}
			}
		}

		[Column(Storage = "_Count", DbType = "Int NOT NULL")]
		public int Count
		{
			get
			{
				return this._Count;
			}
			set
			{
				if (this._Count != value)
				{
					this._Count = value;
				}
			}
		}

		private int _ID;

		private string _Material;

		private int _Count;
	}
}
