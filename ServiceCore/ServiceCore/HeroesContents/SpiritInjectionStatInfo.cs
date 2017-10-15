using System;
using System.Data.Linq.Mapping;

namespace ServiceCore.HeroesContents
{
	[Table(Name = "dbo.SpiritInjectionStatInfo")]
	public class SpiritInjectionStatInfo
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

		[Column(Storage = "_ATK_mean", DbType = "Float NOT NULL")]
		public double ATK_mean
		{
			get
			{
				return this._ATK_mean;
			}
			set
			{
				if (this._ATK_mean != value)
				{
					this._ATK_mean = value;
				}
			}
		}

		[Column(Storage = "_ATK_std", DbType = "Float NOT NULL")]
		public double ATK_std
		{
			get
			{
				return this._ATK_std;
			}
			set
			{
				if (this._ATK_std != value)
				{
					this._ATK_std = value;
				}
			}
		}

		[Column(Storage = "_ATK_Speed_mean", DbType = "Float NOT NULL")]
		public double ATK_Speed_mean
		{
			get
			{
				return this._ATK_Speed_mean;
			}
			set
			{
				if (this._ATK_Speed_mean != value)
				{
					this._ATK_Speed_mean = value;
				}
			}
		}

		[Column(Storage = "_ATK_Speed_std", DbType = "Float NOT NULL")]
		public double ATK_Speed_std
		{
			get
			{
				return this._ATK_Speed_std;
			}
			set
			{
				if (this._ATK_Speed_std != value)
				{
					this._ATK_Speed_std = value;
				}
			}
		}

		[Column(Storage = "_Critical_mean", DbType = "Float NOT NULL")]
		public double Critical_mean
		{
			get
			{
				return this._Critical_mean;
			}
			set
			{
				if (this._Critical_mean != value)
				{
					this._Critical_mean = value;
				}
			}
		}

		[Column(Storage = "_Critical_std", DbType = "Float NOT NULL")]
		public double Critical_std
		{
			get
			{
				return this._Critical_std;
			}
			set
			{
				if (this._Critical_std != value)
				{
					this._Critical_std = value;
				}
			}
		}

		[Column(Storage = "_Balance_mean", DbType = "Float NOT NULL")]
		public double Balance_mean
		{
			get
			{
				return this._Balance_mean;
			}
			set
			{
				if (this._Balance_mean != value)
				{
					this._Balance_mean = value;
				}
			}
		}

		[Column(Storage = "_Balance_std", DbType = "Float NOT NULL")]
		public double Balance_std
		{
			get
			{
				return this._Balance_std;
			}
			set
			{
				if (this._Balance_std != value)
				{
					this._Balance_std = value;
				}
			}
		}

		[Column(Storage = "_MATK_mean", DbType = "Float NOT NULL")]
		public double MATK_mean
		{
			get
			{
				return this._MATK_mean;
			}
			set
			{
				if (this._MATK_mean != value)
				{
					this._MATK_mean = value;
				}
			}
		}

		[Column(Storage = "_MATK_std", DbType = "Float NOT NULL")]
		public double MATK_std
		{
			get
			{
				return this._MATK_std;
			}
			set
			{
				if (this._MATK_std != value)
				{
					this._MATK_std = value;
				}
			}
		}

		[Column(Storage = "_DEF_mean", DbType = "Float NOT NULL")]
		public double DEF_mean
		{
			get
			{
				return this._DEF_mean;
			}
			set
			{
				if (this._DEF_mean != value)
				{
					this._DEF_mean = value;
				}
			}
		}

		[Column(Storage = "_DEF_std", DbType = "Float NOT NULL")]
		public double DEF_std
		{
			get
			{
				return this._DEF_std;
			}
			set
			{
				if (this._DEF_std != value)
				{
					this._DEF_std = value;
				}
			}
		}

		[Column(Storage = "_Res_Critical_mean", DbType = "Float NOT NULL")]
		public double Res_Critical_mean
		{
			get
			{
				return this._Res_Critical_mean;
			}
			set
			{
				if (this._Res_Critical_mean != value)
				{
					this._Res_Critical_mean = value;
				}
			}
		}

		[Column(Storage = "_Res_Critical_std", DbType = "Float NOT NULL")]
		public double Res_Critical_std
		{
			get
			{
				return this._Res_Critical_std;
			}
			set
			{
				if (this._Res_Critical_std != value)
				{
					this._Res_Critical_std = value;
				}
			}
		}

		[Column(Storage = "_STR_mean", DbType = "Float NOT NULL")]
		public double STR_mean
		{
			get
			{
				return this._STR_mean;
			}
			set
			{
				if (this._STR_mean != value)
				{
					this._STR_mean = value;
				}
			}
		}

		[Column(Storage = "_STR_std", DbType = "Float NOT NULL")]
		public double STR_std
		{
			get
			{
				return this._STR_std;
			}
			set
			{
				if (this._STR_std != value)
				{
					this._STR_std = value;
				}
			}
		}

		[Column(Storage = "_DEX_mean", DbType = "Float NOT NULL")]
		public double DEX_mean
		{
			get
			{
				return this._DEX_mean;
			}
			set
			{
				if (this._DEX_mean != value)
				{
					this._DEX_mean = value;
				}
			}
		}

		[Column(Storage = "_DEX_std", DbType = "Float NOT NULL")]
		public double DEX_std
		{
			get
			{
				return this._DEX_std;
			}
			set
			{
				if (this._DEX_std != value)
				{
					this._DEX_std = value;
				}
			}
		}

		[Column(Storage = "_INT_mean", DbType = "Float NOT NULL")]
		public double INT_mean
		{
			get
			{
				return this._INT_mean;
			}
			set
			{
				if (this._INT_mean != value)
				{
					this._INT_mean = value;
				}
			}
		}

		[Column(Storage = "_INT_std", DbType = "Float NOT NULL")]
		public double INT_std
		{
			get
			{
				return this._INT_std;
			}
			set
			{
				if (this._INT_std != value)
				{
					this._INT_std = value;
				}
			}
		}

		[Column(Storage = "_WILL_mean", DbType = "Float NOT NULL")]
		public double WILL_mean
		{
			get
			{
				return this._WILL_mean;
			}
			set
			{
				if (this._WILL_mean != value)
				{
					this._WILL_mean = value;
				}
			}
		}

		[Column(Storage = "_WILL_std", DbType = "Float NOT NULL")]
		public double WILL_std
		{
			get
			{
				return this._WILL_std;
			}
			set
			{
				if (this._WILL_std != value)
				{
					this._WILL_std = value;
				}
			}
		}

		[Column(Storage = "_HP_mean", DbType = "Float NOT NULL")]
		public double HP_mean
		{
			get
			{
				return this._HP_mean;
			}
			set
			{
				if (this._HP_mean != value)
				{
					this._HP_mean = value;
				}
			}
		}

		[Column(Storage = "_HP_std", DbType = "Float NOT NULL")]
		public double HP_std
		{
			get
			{
				return this._HP_std;
			}
			set
			{
				if (this._HP_std != value)
				{
					this._HP_std = value;
				}
			}
		}

		[Column(Storage = "_STAMINA_mean", DbType = "Float NOT NULL")]
		public double STAMINA_mean
		{
			get
			{
				return this._STAMINA_mean;
			}
			set
			{
				if (this._STAMINA_mean != value)
				{
					this._STAMINA_mean = value;
				}
			}
		}

		[Column(Storage = "_STAMINA_std", DbType = "Float NOT NULL")]
		public double STAMINA_std
		{
			get
			{
				return this._STAMINA_std;
			}
			set
			{
				if (this._STAMINA_std != value)
				{
					this._STAMINA_std = value;
				}
			}
		}

		private string _ItemClass;

		private double _ATK_mean;

		private double _ATK_std;

		private double _ATK_Speed_mean;

		private double _ATK_Speed_std;

		private double _Critical_mean;

		private double _Critical_std;

		private double _Balance_mean;

		private double _Balance_std;

		private double _MATK_mean;

		private double _MATK_std;

		private double _DEF_mean;

		private double _DEF_std;

		private double _Res_Critical_mean;

		private double _Res_Critical_std;

		private double _STR_mean;

		private double _STR_std;

		private double _DEX_mean;

		private double _DEX_std;

		private double _INT_mean;

		private double _INT_std;

		private double _WILL_mean;

		private double _WILL_std;

		private double _HP_mean;

		private double _HP_std;

		private double _STAMINA_mean;

		private double _STAMINA_std;
	}
}
