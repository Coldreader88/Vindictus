using System;
using System.Data.Linq.Mapping;

namespace ServiceCore.HeroesContents
{
	[Table(Name = "dbo.HousingPropClassInfo")]
	public class HousingPropClassInfo
	{
		[Column(Storage = "_PropClass", DbType = "Int NOT NULL")]
		public int PropClass
		{
			get
			{
				return this._PropClass;
			}
			set
			{
				if (this._PropClass != value)
				{
					this._PropClass = value;
				}
			}
		}

		[Column(Storage = "_Model", DbType = "NVarChar(256) NOT NULL", CanBeNull = false)]
		public string Model
		{
			get
			{
				return this._Model;
			}
			set
			{
				if (this._Model != value)
				{
					this._Model = value;
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

		[Column(Storage = "_Category", DbType = "NVarChar(50) NOT NULL", CanBeNull = false)]
		public string Category
		{
			get
			{
				return this._Category;
			}
			set
			{
				if (this._Category != value)
				{
					this._Category = value;
				}
			}
		}

		[Column(Storage = "_Gimmic", DbType = "NVarChar(50)")]
		public string Gimmic
		{
			get
			{
				return this._Gimmic;
			}
			set
			{
				if (this._Gimmic != value)
				{
					this._Gimmic = value;
				}
			}
		}

		[Column(Storage = "_Feature", DbType = "NVarChar(50)")]
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

		[Column(Storage = "_Name", DbType = "NVarChar(256) NOT NULL", CanBeNull = false)]
		public string Name
		{
			get
			{
				return this._Name;
			}
			set
			{
				if (this._Name != value)
				{
					this._Name = value;
				}
			}
		}

		[Column(Name = "[Desc]", Storage = "_Desc", DbType = "NVarChar(256) NOT NULL", CanBeNull = false)]
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

		private int _PropClass;

		private string _Model;

		private string _Icon;

		private string _Category;

		private string _Gimmic;

		private string _Feature;

		private string _Name;

		private string _Desc;
	}
}
