using System;
using System.Data.Linq.Mapping;

namespace ServiceCore.HeroesContents
{
	[Table(Name = "dbo.FeatureMatrix")]
	public class FeatureMatrix
	{
		[Column(Storage = "_Feature", DbType = "NVarChar(256) NOT NULL", CanBeNull = false)]
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

		[Column(Name = "[KO-KR]", Storage = "_KO_KR", DbType = "NVarChar(256)")]
		public string KO_KR
		{
			get
			{
				return this._KO_KR;
			}
			set
			{
				if (this._KO_KR != value)
				{
					this._KO_KR = value;
				}
			}
		}

		[Column(Name = "[KO-KR-X-TEST]", Storage = "_KO_KR_X_TEST", DbType = "NVarChar(256)")]
		public string KO_KR_X_TEST
		{
			get
			{
				return this._KO_KR_X_TEST;
			}
			set
			{
				if (this._KO_KR_X_TEST != value)
				{
					this._KO_KR_X_TEST = value;
				}
			}
		}

		[Column(Name = "[KO-KR-X-GM]", Storage = "_KO_KR_X_GM", DbType = "NVarChar(256)")]
		public string KO_KR_X_GM
		{
			get
			{
				return this._KO_KR_X_GM;
			}
			set
			{
				if (this._KO_KR_X_GM != value)
				{
					this._KO_KR_X_GM = value;
				}
			}
		}

		[Column(Name = "[KO-KR-X-DEV]", Storage = "_KO_KR_X_DEV", DbType = "NVarChar(256)")]
		public string KO_KR_X_DEV
		{
			get
			{
				return this._KO_KR_X_DEV;
			}
			set
			{
				if (this._KO_KR_X_DEV != value)
				{
					this._KO_KR_X_DEV = value;
				}
			}
		}

		[Column(Name = "[EN-US]", Storage = "_EN_US", DbType = "NVarChar(256)")]
		public string EN_US
		{
			get
			{
				return this._EN_US;
			}
			set
			{
				if (this._EN_US != value)
				{
					this._EN_US = value;
				}
			}
		}

		[Column(Name = "[EN-US-X-GM]", Storage = "_EN_US_X_GM", DbType = "NVarChar(256)")]
		public string EN_US_X_GM
		{
			get
			{
				return this._EN_US_X_GM;
			}
			set
			{
				if (this._EN_US_X_GM != value)
				{
					this._EN_US_X_GM = value;
				}
			}
		}

		[Column(Name = "[ZH-CN]", Storage = "_ZH_CN", DbType = "NVarChar(256)")]
		public string ZH_CN
		{
			get
			{
				return this._ZH_CN;
			}
			set
			{
				if (this._ZH_CN != value)
				{
					this._ZH_CN = value;
				}
			}
		}

		[Column(Name = "[ZH-CN-X-GM]", Storage = "_ZH_CN_X_GM", DbType = "NVarChar(256)")]
		public string ZH_CN_X_GM
		{
			get
			{
				return this._ZH_CN_X_GM;
			}
			set
			{
				if (this._ZH_CN_X_GM != value)
				{
					this._ZH_CN_X_GM = value;
				}
			}
		}

		[Column(Name = "[ZH-CN-X-TEST]", Storage = "_ZH_CN_X_TEST", DbType = "NVarChar(256)")]
		public string ZH_CN_X_TEST
		{
			get
			{
				return this._ZH_CN_X_TEST;
			}
			set
			{
				if (this._ZH_CN_X_TEST != value)
				{
					this._ZH_CN_X_TEST = value;
				}
			}
		}

		[Column(Name = "[JA-JP]", Storage = "_JA_JP", DbType = "NVarChar(256)")]
		public string JA_JP
		{
			get
			{
				return this._JA_JP;
			}
			set
			{
				if (this._JA_JP != value)
				{
					this._JA_JP = value;
				}
			}
		}

		[Column(Name = "[JA-JP-X-GM]", Storage = "_JA_JP_X_GM", DbType = "NVarChar(256)")]
		public string JA_JP_X_GM
		{
			get
			{
				return this._JA_JP_X_GM;
			}
			set
			{
				if (this._JA_JP_X_GM != value)
				{
					this._JA_JP_X_GM = value;
				}
			}
		}

		[Column(Name = "[EN-EU]", Storage = "_EN_EU", DbType = "NVarChar(256)")]
		public string EN_EU
		{
			get
			{
				return this._EN_EU;
			}
			set
			{
				if (this._EN_EU != value)
				{
					this._EN_EU = value;
				}
			}
		}

		[Column(Name = "[EN-EU-X-GM]", Storage = "_EN_EU_X_GM", DbType = "NVarChar(256)")]
		public string EN_EU_X_GM
		{
			get
			{
				return this._EN_EU_X_GM;
			}
			set
			{
				if (this._EN_EU_X_GM != value)
				{
					this._EN_EU_X_GM = value;
				}
			}
		}

		[Column(Name = "[ZH-TW]", Storage = "_ZH_TW", DbType = "NVarChar(256)")]
		public string ZH_TW
		{
			get
			{
				return this._ZH_TW;
			}
			set
			{
				if (this._ZH_TW != value)
				{
					this._ZH_TW = value;
				}
			}
		}

		[Column(Name = "[ZH-TW-X-GM]", Storage = "_ZH_TW_X_GM", DbType = "NVarChar(256)")]
		public string ZH_TW_X_GM
		{
			get
			{
				return this._ZH_TW_X_GM;
			}
			set
			{
				if (this._ZH_TW_X_GM != value)
				{
					this._ZH_TW_X_GM = value;
				}
			}
		}

		private string _Feature;

		private string _KO_KR;

		private string _KO_KR_X_TEST;

		private string _KO_KR_X_GM;

		private string _KO_KR_X_DEV;

		private string _EN_US;

		private string _EN_US_X_GM;

		private string _ZH_CN;

		private string _ZH_CN_X_GM;

		private string _ZH_CN_X_TEST;

		private string _JA_JP;

		private string _JA_JP_X_GM;

		private string _EN_EU;

		private string _EN_EU_X_GM;

		private string _ZH_TW;

		private string _ZH_TW_X_GM;
	}
}
