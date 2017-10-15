using System;
using System.Data.Linq.Mapping;

namespace ServiceCore.HeroesContents
{
	[Table(Name = "dbo.EnchantDiceInfo")]
	public class EnchantDiceInfo
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

		[Column(Storage = "_MinValue", DbType = "Int NOT NULL")]
		public int MinValue
		{
			get
			{
				return this._MinValue;
			}
			set
			{
				if (this._MinValue != value)
				{
					this._MinValue = value;
				}
			}
		}

		[Column(Storage = "_MaxValue", DbType = "Int NOT NULL")]
		public int MaxValue
		{
			get
			{
				return this._MaxValue;
			}
			set
			{
				if (this._MaxValue != value)
				{
					this._MaxValue = value;
				}
			}
		}

		[Column(Storage = "_SkillRank", DbType = "Int NOT NULL")]
		public int SkillRank
		{
			get
			{
				return this._SkillRank;
			}
			set
			{
				if (this._SkillRank != value)
				{
					this._SkillRank = value;
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

		private string _ItemClass;

		private int _MinValue;

		private int _MaxValue;

		private int _SkillRank;

		private string _Feature;
	}
}
