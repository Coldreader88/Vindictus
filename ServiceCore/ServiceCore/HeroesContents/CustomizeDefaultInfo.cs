using System;
using System.Data.Linq.Mapping;

namespace ServiceCore.HeroesContents
{
	[Table(Name = "dbo.CustomizeDefaultInfo")]
	public class CustomizeDefaultInfo
	{
		[Column(Storage = "_ItemClass", DbType = "VarChar(100) NOT NULL", CanBeNull = false)]
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

		[Column(Storage = "_Color1", DbType = "Int NOT NULL")]
		public int Color1
		{
			get
			{
				return this._Color1;
			}
			set
			{
				if (this._Color1 != value)
				{
					this._Color1 = value;
				}
			}
		}

		[Column(Storage = "_Color2", DbType = "Int NOT NULL")]
		public int Color2
		{
			get
			{
				return this._Color2;
			}
			set
			{
				if (this._Color2 != value)
				{
					this._Color2 = value;
				}
			}
		}

		[Column(Storage = "_Color3", DbType = "Int NOT NULL")]
		public int Color3
		{
			get
			{
				return this._Color3;
			}
			set
			{
				if (this._Color3 != value)
				{
					this._Color3 = value;
				}
			}
		}

		[Column(Storage = "_X", DbType = "Int NOT NULL")]
		public int X
		{
			get
			{
				return this._X;
			}
			set
			{
				if (this._X != value)
				{
					this._X = value;
				}
			}
		}

		[Column(Storage = "_Y", DbType = "Int NOT NULL")]
		public int Y
		{
			get
			{
				return this._Y;
			}
			set
			{
				if (this._Y != value)
				{
					this._Y = value;
				}
			}
		}

		[Column(Storage = "_Rotation", DbType = "Int NOT NULL")]
		public int Rotation
		{
			get
			{
				return this._Rotation;
			}
			set
			{
				if (this._Rotation != value)
				{
					this._Rotation = value;
				}
			}
		}

		[Column(Storage = "_Scale", DbType = "Int NOT NULL")]
		public int Scale
		{
			get
			{
				return this._Scale;
			}
			set
			{
				if (this._Scale != value)
				{
					this._Scale = value;
				}
			}
		}

		[Column(Storage = "_Surface", DbType = "Int NOT NULL")]
		public int Surface
		{
			get
			{
				return this._Surface;
			}
			set
			{
				if (this._Surface != value)
				{
					this._Surface = value;
				}
			}
		}

		[Column(Storage = "_Clip", DbType = "Int NOT NULL")]
		public int Clip
		{
			get
			{
				return this._Clip;
			}
			set
			{
				if (this._Clip != value)
				{
					this._Clip = value;
				}
			}
		}

		[Column(Storage = "_Blend", DbType = "Int NOT NULL")]
		public int Blend
		{
			get
			{
				return this._Blend;
			}
			set
			{
				if (this._Blend != value)
				{
					this._Blend = value;
				}
			}
		}

		[Column(Storage = "_CashShopType", DbType = "VarChar(100)")]
		public string CashShopType
		{
			get
			{
				return this._CashShopType;
			}
			set
			{
				if (this._CashShopType != value)
				{
					this._CashShopType = value;
				}
			}
		}

		[Column(Storage = "_Feature", DbType = "VarChar(100)")]
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

		private int _Color1;

		private int _Color2;

		private int _Color3;

		private int _X;

		private int _Y;

		private int _Rotation;

		private int _Scale;

		private int _Surface;

		private int _Clip;

		private int _Blend;

		private string _CashShopType;

		private string _Feature;
	}
}
