using System;
using System.Data.Linq.Mapping;

namespace ServiceCore.HeroesContents
{
	[Table(Name = "dbo.HairTypeInfo")]
	public class HairTypeInfo
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

		[Column(Storage = "_Name", DbType = "NVarChar(100) NOT NULL", CanBeNull = false)]
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

		[Column(Storage = "_ClassConstraint", DbType = "Int NOT NULL")]
		public int ClassConstraint
		{
			get
			{
				return this._ClassConstraint;
			}
			set
			{
				if (this._ClassConstraint != value)
				{
					this._ClassConstraint = value;
				}
			}
		}

		[Column(Storage = "_SN", DbType = "Int NOT NULL")]
		public int SN
		{
			get
			{
				return this._SN;
			}
			set
			{
				if (this._SN != value)
				{
					this._SN = value;
				}
			}
		}

		[Column(Storage = "_Weight", DbType = "Int NOT NULL")]
		public int Weight
		{
			get
			{
				return this._Weight;
			}
			set
			{
				if (this._Weight != value)
				{
					this._Weight = value;
				}
			}
		}

		[Column(Storage = "_Icon", DbType = "NVarChar(100) NOT NULL", CanBeNull = false)]
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

		[Column(Storage = "_RequiredColor", DbType = "Int NOT NULL")]
		public int RequiredColor
		{
			get
			{
				return this._RequiredColor;
			}
			set
			{
				if (this._RequiredColor != value)
				{
					this._RequiredColor = value;
				}
			}
		}

		private int _ID;

		private string _Name;

		private int _ClassConstraint;

		private int _SN;

		private int _Weight;

		private string _Icon;

		private int _RequiredColor;
	}
}
