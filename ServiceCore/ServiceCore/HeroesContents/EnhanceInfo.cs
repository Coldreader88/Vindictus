using System;
using System.Data.Linq.Mapping;

namespace ServiceCore.HeroesContents
{
	[Table(Name = "dbo.EnhanceInfo")]
	public class EnhanceInfo
	{
		[Column(Storage = "_EnhanceType", DbType = "VarChar(50) NOT NULL", CanBeNull = false)]
		public string EnhanceType
		{
			get
			{
				return this._EnhanceType;
			}
			set
			{
				if (this._EnhanceType != value)
				{
					this._EnhanceType = value;
				}
			}
		}

		[Column(Storage = "_EnhanceLevel", DbType = "Int NOT NULL")]
		public int EnhanceLevel
		{
			get
			{
				return this._EnhanceLevel;
			}
			set
			{
				if (this._EnhanceLevel != value)
				{
					this._EnhanceLevel = value;
				}
			}
		}

		[Column(Storage = "_MaterialClass1", DbType = "VarChar(50) NOT NULL", CanBeNull = false)]
		public string MaterialClass1
		{
			get
			{
				return this._MaterialClass1;
			}
			set
			{
				if (this._MaterialClass1 != value)
				{
					this._MaterialClass1 = value;
				}
			}
		}

		[Column(Storage = "_MaterialNum1", DbType = "Int NOT NULL")]
		public int MaterialNum1
		{
			get
			{
				return this._MaterialNum1;
			}
			set
			{
				if (this._MaterialNum1 != value)
				{
					this._MaterialNum1 = value;
				}
			}
		}

		[Column(Storage = "_MaterialClass2", DbType = "VarChar(50) NOT NULL", CanBeNull = false)]
		public string MaterialClass2
		{
			get
			{
				return this._MaterialClass2;
			}
			set
			{
				if (this._MaterialClass2 != value)
				{
					this._MaterialClass2 = value;
				}
			}
		}

		[Column(Storage = "_MaterialNum2", DbType = "Int NOT NULL")]
		public int MaterialNum2
		{
			get
			{
				return this._MaterialNum2;
			}
			set
			{
				if (this._MaterialNum2 != value)
				{
					this._MaterialNum2 = value;
				}
			}
		}

		[Column(Storage = "_Gold", DbType = "Int NOT NULL")]
		public int Gold
		{
			get
			{
				return this._Gold;
			}
			set
			{
				if (this._Gold != value)
				{
					this._Gold = value;
				}
			}
		}

		[Column(Storage = "_SuccessRatio", DbType = "Real NOT NULL")]
		public float SuccessRatio
		{
			get
			{
				return this._SuccessRatio;
			}
			set
			{
				if (this._SuccessRatio != value)
				{
					this._SuccessRatio = value;
				}
			}
		}

		[Column(Storage = "_OnFail", DbType = "VarChar(50) NOT NULL", CanBeNull = false)]
		public string OnFail
		{
			get
			{
				return this._OnFail;
			}
			set
			{
				if (this._OnFail != value)
				{
					this._OnFail = value;
				}
			}
		}

		[Column(Storage = "_OnFailRecipeRatio", DbType = "Real NOT NULL")]
		public float OnFailRecipeRatio
		{
			get
			{
				return this._OnFailRecipeRatio;
			}
			set
			{
				if (this._OnFailRecipeRatio != value)
				{
					this._OnFailRecipeRatio = value;
				}
			}
		}

		[Column(Storage = "_OnFailMaterialNum1", DbType = "Int NOT NULL")]
		public int OnFailMaterialNum1
		{
			get
			{
				return this._OnFailMaterialNum1;
			}
			set
			{
				if (this._OnFailMaterialNum1 != value)
				{
					this._OnFailMaterialNum1 = value;
				}
			}
		}

		[Column(Storage = "_Effect", DbType = "Int NOT NULL")]
		public int Effect
		{
			get
			{
				return this._Effect;
			}
			set
			{
				if (this._Effect != value)
				{
					this._Effect = value;
				}
			}
		}

		[Column(Storage = "_Feature", DbType = "VarChar(50)")]
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

		private string _EnhanceType;

		private int _EnhanceLevel;

		private string _MaterialClass1;

		private int _MaterialNum1;

		private string _MaterialClass2;

		private int _MaterialNum2;

		private int _Gold;

		private float _SuccessRatio;

		private string _OnFail;

		private float _OnFailRecipeRatio;

		private int _OnFailMaterialNum1;

		private int _Effect;

		private string _Feature;
	}
}
