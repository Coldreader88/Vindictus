using System;
using System.Data.Linq.Mapping;

namespace ServiceCore.HeroesContents
{
	[Table(Name = "dbo.EnchantInfo")]
	public class EnchantInfo
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

		[Column(Storage = "_EnchantClass", DbType = "NVarChar(50) NOT NULL", CanBeNull = false)]
		public string EnchantClass
		{
			get
			{
				return this._EnchantClass;
			}
			set
			{
				if (this._EnchantClass != value)
				{
					this._EnchantClass = value;
				}
			}
		}

		[Column(Storage = "_IsPrefix", DbType = "Bit NOT NULL")]
		public bool IsPrefix
		{
			get
			{
				return this._IsPrefix;
			}
			set
			{
				if (this._IsPrefix != value)
				{
					this._IsPrefix = value;
				}
			}
		}

		[Column(Storage = "_EnchantType", DbType = "NVarChar(50) NOT NULL", CanBeNull = false)]
		public string EnchantType
		{
			get
			{
				return this._EnchantType;
			}
			set
			{
				if (this._EnchantType != value)
				{
					this._EnchantType = value;
				}
			}
		}

		[Column(Storage = "_EnchantLevel", DbType = "Int NOT NULL")]
		public int EnchantLevel
		{
			get
			{
				return this._EnchantLevel;
			}
			set
			{
				if (this._EnchantLevel != value)
				{
					this._EnchantLevel = value;
				}
			}
		}

		[Column(Storage = "_MinArgValue", DbType = "Int NOT NULL")]
		public int MinArgValue
		{
			get
			{
				return this._MinArgValue;
			}
			set
			{
				if (this._MinArgValue != value)
				{
					this._MinArgValue = value;
				}
			}
		}

		[Column(Storage = "_MaxArgValue", DbType = "Int NOT NULL")]
		public int MaxArgValue
		{
			get
			{
				return this._MaxArgValue;
			}
			set
			{
				if (this._MaxArgValue != value)
				{
					this._MaxArgValue = value;
				}
			}
		}

		[Column(Storage = "_ItemConstraint", DbType = "NVarChar(1024)")]
		public string ItemConstraint
		{
			get
			{
				return this._ItemConstraint;
			}
			set
			{
				if (this._ItemConstraint != value)
				{
					this._ItemConstraint = value;
				}
			}
		}

		[Column(Storage = "_ItemConstraintDesc", DbType = "NVarChar(1024)")]
		public string ItemConstraintDesc
		{
			get
			{
				return this._ItemConstraintDesc;
			}
			set
			{
				if (this._ItemConstraintDesc != value)
				{
					this._ItemConstraintDesc = value;
				}
			}
		}

		[Column(Storage = "_MinSuccessRatio", DbType = "Int NOT NULL")]
		public int MinSuccessRatio
		{
			get
			{
				return this._MinSuccessRatio;
			}
			set
			{
				if (this._MinSuccessRatio != value)
				{
					this._MinSuccessRatio = value;
				}
			}
		}

		[Column(Storage = "_MaxSuccessRatio", DbType = "Int NOT NULL")]
		public int MaxSuccessRatio
		{
			get
			{
				return this._MaxSuccessRatio;
			}
			set
			{
				if (this._MaxSuccessRatio != value)
				{
					this._MaxSuccessRatio = value;
				}
			}
		}

		[Column(Storage = "_DestructionRatio", DbType = "Int NOT NULL")]
		public int DestructionRatio
		{
			get
			{
				return this._DestructionRatio;
			}
			set
			{
				if (this._DestructionRatio != value)
				{
					this._DestructionRatio = value;
				}
			}
		}

		[Column(Storage = "_MiniGameSize", DbType = "Int NOT NULL")]
		public int MiniGameSize
		{
			get
			{
				return this._MiniGameSize;
			}
			set
			{
				if (this._MiniGameSize != value)
				{
					this._MiniGameSize = value;
				}
			}
		}

		[Column(Storage = "_Duration", DbType = "Int NOT NULL")]
		public int Duration
		{
			get
			{
				return this._Duration;
			}
			set
			{
				if (this._Duration != value)
				{
					this._Duration = value;
				}
			}
		}

		[Column(Storage = "_IgnoreMaxArmor", DbType = "Bit NOT NULL")]
		public bool IgnoreMaxArmor
		{
			get
			{
				return this._IgnoreMaxArmor;
			}
			set
			{
				if (this._IgnoreMaxArmor != value)
				{
					this._IgnoreMaxArmor = value;
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

		private int _ID;

		private string _EnchantClass;

		private bool _IsPrefix;

		private string _EnchantType;

		private int _EnchantLevel;

		private int _MinArgValue;

		private int _MaxArgValue;

		private string _ItemConstraint;

		private string _ItemConstraintDesc;

		private int _MinSuccessRatio;

		private int _MaxSuccessRatio;

		private int _DestructionRatio;

		private int _MiniGameSize;

		private int _Duration;

		private bool _IgnoreMaxArmor;

		private string _Feature;
	}
}
