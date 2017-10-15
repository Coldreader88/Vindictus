using System;
using System.Data.Linq.Mapping;

namespace ServiceCore.HeroesContents
{
	[Table(Name = "dbo.ShipYardItemClassInfo")]
	public class ShipYardItemClassInfo
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

		[Column(Storage = "_ModelName", DbType = "NVarChar(50) NOT NULL", CanBeNull = false)]
		public string ModelName
		{
			get
			{
				return this._ModelName;
			}
			set
			{
				if (this._ModelName != value)
				{
					this._ModelName = value;
				}
			}
		}

		[Column(Storage = "_IsPhysics", DbType = "Int NOT NULL")]
		public int IsPhysics
		{
			get
			{
				return this._IsPhysics;
			}
			set
			{
				if (this._IsPhysics != value)
				{
					this._IsPhysics = value;
				}
			}
		}

		[Column(Storage = "_GimicName", DbType = "NVarChar(50)")]
		public string GimicName
		{
			get
			{
				return this._GimicName;
			}
			set
			{
				if (this._GimicName != value)
				{
					this._GimicName = value;
				}
			}
		}

		private int _ID;

		private string _ModelName;

		private int _IsPhysics;

		private string _GimicName;
	}
}
