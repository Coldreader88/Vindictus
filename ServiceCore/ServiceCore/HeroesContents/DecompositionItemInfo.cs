using System;
using System.Data.Linq.Mapping;

namespace ServiceCore.HeroesContents
{
	[Table(Name = "dbo.DecompositionItemInfo")]
	public class DecompositionItemInfo
	{
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

		[Column(Storage = "_LevelMin", DbType = "Int NOT NULL")]
		public int LevelMin
		{
			get
			{
				return this._LevelMin;
			}
			set
			{
				if (this._LevelMin != value)
				{
					this._LevelMin = value;
				}
			}
		}

		[Column(Storage = "_LevelMax", DbType = "Int NOT NULL")]
		public int LevelMax
		{
			get
			{
				return this._LevelMax;
			}
			set
			{
				if (this._LevelMax != value)
				{
					this._LevelMax = value;
				}
			}
		}

		[Column(Storage = "_ItemClass1", DbType = "NVarChar(50)")]
		public string ItemClass1
		{
			get
			{
				return this._ItemClass1;
			}
			set
			{
				if (this._ItemClass1 != value)
				{
					this._ItemClass1 = value;
				}
			}
		}

		[Column(Storage = "_ItemClass2", DbType = "NVarChar(50)")]
		public string ItemClass2
		{
			get
			{
				return this._ItemClass2;
			}
			set
			{
				if (this._ItemClass2 != value)
				{
					this._ItemClass2 = value;
				}
			}
		}

		[Column(Storage = "_ItemClass3", DbType = "NVarChar(50)")]
		public string ItemClass3
		{
			get
			{
				return this._ItemClass3;
			}
			set
			{
				if (this._ItemClass3 != value)
				{
					this._ItemClass3 = value;
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

		[Column(Storage = "_Experience", DbType = "Int NOT NULL")]
		public int Experience
		{
			get
			{
				return this._Experience;
			}
			set
			{
				if (this._Experience != value)
				{
					this._Experience = value;
				}
			}
		}

		[Column(Storage = "_Feature", DbType = "NVarChar(128)", CanBeNull = false)]
		public string Feature
		{
			get
			{
				return this._Feature;
			}
			set
			{
				if (this._Feature != value)
				{
					this._Feature = value;
				}
			}
		}

		private string _ManufactureID;

		private int _LevelMin;

		private int _LevelMax;

		private string _ItemClass1;

		private string _ItemClass2;

		private string _ItemClass3;

		private int _Price;

		private int _Experience;

		private string _Feature;
	}
}
