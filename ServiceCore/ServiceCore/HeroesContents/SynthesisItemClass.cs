using System;
using System.Data.Linq.Mapping;

namespace ServiceCore.HeroesContents
{
	[Table(Name = "dbo.SynthesisItemClass")]
	public class SynthesisItemClass
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

		[Column(Storage = "_Min", DbType = "Int NOT NULL")]
		public int Min
		{
			get
			{
				return this._Min;
			}
			set
			{
				if (this._Min != value)
				{
					this._Min = value;
				}
			}
		}

		[Column(Storage = "_Max", DbType = "Int NOT NULL")]
		public int Max
		{
			get
			{
				return this._Max;
			}
			set
			{
				if (this._Max != value)
				{
					this._Max = value;
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

		[Column(Storage = "_Code", DbType = "NVarChar(20)")]
		public string Code
		{
			get
			{
				return this._Code;
			}
			set
			{
				if (this._Code != value)
				{
					this._Code = value;
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

		private string _Category;

		private int _Min;

		private int _Max;

		private int _ClassRestriction;

		private string _Code;

		private string _Feature;
	}
}
