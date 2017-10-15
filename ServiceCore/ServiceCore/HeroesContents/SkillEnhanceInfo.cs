using System;
using System.Data.Linq.Mapping;

namespace ServiceCore.HeroesContents
{
	[Table(Name = "dbo.SkillEnhanceInfo")]
	public class SkillEnhanceInfo
	{
		[Column(Storage = "_SkillEnhanceGroup", DbType = "Int NOT NULL")]
		public int SkillEnhanceGroup
		{
			get
			{
				return this._SkillEnhanceGroup;
			}
			set
			{
				if (this._SkillEnhanceGroup != value)
				{
					this._SkillEnhanceGroup = value;
				}
			}
		}

		[Column(Storage = "_SkillEnhanceKey", DbType = "Int NOT NULL")]
		public int SkillEnhanceKey
		{
			get
			{
				return this._SkillEnhanceKey;
			}
			set
			{
				if (this._SkillEnhanceKey != value)
				{
					this._SkillEnhanceKey = value;
				}
			}
		}

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

		[Column(Storage = "_EffectType", DbType = "Int NOT NULL")]
		public int EffectType
		{
			get
			{
				return this._EffectType;
			}
			set
			{
				if (this._EffectType != value)
				{
					this._EffectType = value;
				}
			}
		}

		[Column(Storage = "_EffectValue", DbType = "Float NOT NULL")]
		public double EffectValue
		{
			get
			{
				return this._EffectValue;
			}
			set
			{
				if (this._EffectValue != value)
				{
					this._EffectValue = value;
				}
			}
		}

		[Column(Storage = "_EffectValueFailFeature", DbType = "NVarChar(256) NOT NULL", CanBeNull = false)]
		public string EffectValueFailFeature
		{
			get
			{
				return this._EffectValueFailFeature;
			}
			set
			{
				if (this._EffectValueFailFeature != value)
				{
					this._EffectValueFailFeature = value;
				}
			}
		}

		[Column(Storage = "_EffectValueFail", DbType = "Float NOT NULL")]
		public double EffectValueFail
		{
			get
			{
				return this._EffectValueFail;
			}
			set
			{
				if (this._EffectValueFail != value)
				{
					this._EffectValueFail = value;
				}
			}
		}

		[Column(Storage = "_MaxDurability", DbType = "Int NOT NULL")]
		public int MaxDurability
		{
			get
			{
				return this._MaxDurability;
			}
			set
			{
				if (this._MaxDurability != value)
				{
					this._MaxDurability = value;
				}
			}
		}

		[Column(Storage = "_RepairValue", DbType = "Int NOT NULL")]
		public int RepairValue
		{
			get
			{
				return this._RepairValue;
			}
			set
			{
				if (this._RepairValue != value)
				{
					this._RepairValue = value;
				}
			}
		}

		[Column(Storage = "_SubTitleDesc", DbType = "NVarChar(128) NOT NULL", CanBeNull = false)]
		public string SubTitleDesc
		{
			get
			{
				return this._SubTitleDesc;
			}
			set
			{
				if (this._SubTitleDesc != value)
				{
					this._SubTitleDesc = value;
				}
			}
		}

		[Column(Storage = "_EffectDesc", DbType = "NVarChar(128) NOT NULL", CanBeNull = false)]
		public string EffectDesc
		{
			get
			{
				return this._EffectDesc;
			}
			set
			{
				if (this._EffectDesc != value)
				{
					this._EffectDesc = value;
				}
			}
		}

		[Column(Storage = "_EffectDescFail", DbType = "NVarChar(128) NOT NULL", CanBeNull = false)]
		public string EffectDescFail
		{
			get
			{
				return this._EffectDescFail;
			}
			set
			{
				if (this._EffectDescFail != value)
				{
					this._EffectDescFail = value;
				}
			}
		}

		private int _SkillEnhanceGroup;

		private int _SkillEnhanceKey;

		private string _ItemClass;

		private int _MaxSuccessRatio;

		private int _EffectType;

		private double _EffectValue;

		private string _EffectValueFailFeature;

		private double _EffectValueFail;

		private int _MaxDurability;

		private int _RepairValue;

		private string _SubTitleDesc;

		private string _EffectDesc;

		private string _EffectDescFail;
	}
}
