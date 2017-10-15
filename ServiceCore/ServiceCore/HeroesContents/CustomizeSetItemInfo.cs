using System;
using System.Data.Linq.Mapping;

namespace ServiceCore.HeroesContents
{
	[Table(Name = "dbo.CustomizeSetItemInfo")]
	public class CustomizeSetItemInfo
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

		[Column(Storage = "_Icon", DbType = "NVarChar(50) NOT NULL", CanBeNull = false)]
		public string Icon
		{
			get
			{
				return this._Icon;
			}
			set
			{
				if (this._Icon != value)
				{
					this._Icon = value;
				}
			}
		}

		[Column(Storage = "_IconBG", DbType = "NVarChar(50)")]
		public string IconBG
		{
			get
			{
				return this._IconBG;
			}
			set
			{
				if (this._IconBG != value)
				{
					this._IconBG = value;
				}
			}
		}

		[Column(Storage = "_Item1", DbType = "NVarChar(50)")]
		public string Item1
		{
			get
			{
				return this._Item1;
			}
			set
			{
				if (this._Item1 != value)
				{
					this._Item1 = value;
				}
			}
		}

		[Column(Storage = "_Item2", DbType = "NVarChar(50)")]
		public string Item2
		{
			get
			{
				return this._Item2;
			}
			set
			{
				if (this._Item2 != value)
				{
					this._Item2 = value;
				}
			}
		}

		[Column(Storage = "_Item3", DbType = "NVarChar(50)")]
		public string Item3
		{
			get
			{
				return this._Item3;
			}
			set
			{
				if (this._Item3 != value)
				{
					this._Item3 = value;
				}
			}
		}

		[Column(Storage = "_Item4", DbType = "NVarChar(50)")]
		public string Item4
		{
			get
			{
				return this._Item4;
			}
			set
			{
				if (this._Item4 != value)
				{
					this._Item4 = value;
				}
			}
		}

		[Column(Storage = "_Item5", DbType = "NVarChar(50)")]
		public string Item5
		{
			get
			{
				return this._Item5;
			}
			set
			{
				if (this._Item5 != value)
				{
					this._Item5 = value;
				}
			}
		}

		[Column(Storage = "_Description", DbType = "NVarChar(100) NOT NULL", CanBeNull = false)]
		public string Description
		{
			get
			{
				return this._Description;
			}
			set
			{
				if (this._Description != value)
				{
					this._Description = value;
				}
			}
		}

		[Column(Storage = "_Feature", DbType = "NVarChar(256)")]
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

		private string _ItemClass;

		private string _Icon;

		private string _IconBG;

		private string _Item1;

		private string _Item2;

		private string _Item3;

		private string _Item4;

		private string _Item5;

		private string _Description;

		private string _Feature;
	}
}
