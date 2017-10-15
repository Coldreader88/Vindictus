using System;
using System.Data.Linq.Mapping;

namespace ServiceCore.HeroesContents
{
	[Table(Name = "dbo.Cashshop_PreviewInfo")]
	public class Cashshop_PreviewInfo
	{
		[Column(Storage = "_ItemType", DbType = "SmallInt NOT NULL")]
		public short ItemType
		{
			get
			{
				return this._ItemType;
			}
			set
			{
				if (this._ItemType != value)
				{
					this._ItemType = value;
				}
			}
		}

		[Column(Storage = "_ItemClass", DbType = "VarChar(50) NOT NULL", CanBeNull = false)]
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

		[Column(Storage = "_CostumeFile", DbType = "VarChar(255) NOT NULL", CanBeNull = false)]
		public string CostumeFile
		{
			get
			{
				return this._CostumeFile;
			}
			set
			{
				if (this._CostumeFile != value)
				{
					this._CostumeFile = value;
				}
			}
		}

		[Column(Storage = "_ClassRestriction", DbType = "Int NOT NULL")]
		public int ClassRestriction
		{
			get
			{
				return this._ClassRestriction;
			}
			set
			{
				if (this._ClassRestriction != value)
				{
					this._ClassRestriction = value;
				}
			}
		}

		private short _ItemType;

		private string _ItemClass;

		private string _CostumeFile;

		private int _ClassRestriction;
	}
}
