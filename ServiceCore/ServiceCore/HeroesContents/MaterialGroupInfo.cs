using System;
using System.Data.Linq.Mapping;

namespace ServiceCore.HeroesContents
{
	[Table(Name = "dbo.MaterialGroupInfo")]
	public class MaterialGroupInfo
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

		[Column(Storage = "_MaterialGroup", DbType = "NVarChar(50) NOT NULL", CanBeNull = false)]
		public string MaterialGroup
		{
			get
			{
				return this._MaterialGroup;
			}
			set
			{
				if (this._MaterialGroup != value)
				{
					this._MaterialGroup = value;
				}
			}
		}

		[Column(Storage = "_Material", DbType = "NVarChar(50) NOT NULL", CanBeNull = false)]
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

		private int _ID;

		private string _MaterialGroup;

		private string _Material;
	}
}
