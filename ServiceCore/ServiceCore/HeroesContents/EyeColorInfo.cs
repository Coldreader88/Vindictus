using System;
using System.Data.Linq.Mapping;

namespace ServiceCore.HeroesContents
{
	[Table(Name = "dbo.EyeColorInfo")]
	public class EyeColorInfo
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

		[Column(Storage = "_Name", DbType = "NVarChar(50)")]
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

		[Column(Storage = "_R", DbType = "Int NOT NULL")]
		public int R
		{
			get
			{
				return this._R;
			}
			set
			{
				if (this._R != value)
				{
					this._R = value;
				}
			}
		}

		[Column(Storage = "_G", DbType = "Int NOT NULL")]
		public int G
		{
			get
			{
				return this._G;
			}
			set
			{
				if (this._G != value)
				{
					this._G = value;
				}
			}
		}

		[Column(Storage = "_B", DbType = "Int NOT NULL")]
		public int B
		{
			get
			{
				return this._B;
			}
			set
			{
				if (this._B != value)
				{
					this._B = value;
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

		private int _ID;

		private string _Name;

		private int _R;

		private int _G;

		private int _B;

		private int _Weight;
	}
}
