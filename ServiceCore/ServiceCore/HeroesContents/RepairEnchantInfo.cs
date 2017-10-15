using System;
using System.Data.Linq.Mapping;

namespace ServiceCore.HeroesContents
{
	[Table(Name = "dbo.RepairEnchantInfo")]
	public class RepairEnchantInfo
	{
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

		[Column(Storage = "_PrefixPoint", DbType = "Int NOT NULL")]
		public int PrefixPoint
		{
			get
			{
				return this._PrefixPoint;
			}
			set
			{
				if (this._PrefixPoint != value)
				{
					this._PrefixPoint = value;
				}
			}
		}

		[Column(Storage = "_SuffixPoint", DbType = "Int NOT NULL")]
		public int SuffixPoint
		{
			get
			{
				return this._SuffixPoint;
			}
			set
			{
				if (this._SuffixPoint != value)
				{
					this._SuffixPoint = value;
				}
			}
		}

		[Column(Storage = "_PrefixMaxDurabilityRecoveryPoint", DbType = "Int NOT NULL")]
		public int PrefixMaxDurabilityRecoveryPoint
		{
			get
			{
				return this._PrefixMaxDurabilityRecoveryPoint;
			}
			set
			{
				if (this._PrefixMaxDurabilityRecoveryPoint != value)
				{
					this._PrefixMaxDurabilityRecoveryPoint = value;
				}
			}
		}

		[Column(Storage = "_SuffixMaxDurabilityRecoveryPoint", DbType = "Int NOT NULL")]
		public int SuffixMaxDurabilityRecoveryPoint
		{
			get
			{
				return this._SuffixMaxDurabilityRecoveryPoint;
			}
			set
			{
				if (this._SuffixMaxDurabilityRecoveryPoint != value)
				{
					this._SuffixMaxDurabilityRecoveryPoint = value;
				}
			}
		}

		private int _EnchantLevel;

		private int _PrefixPoint;

		private int _SuffixPoint;

		private int _PrefixMaxDurabilityRecoveryPoint;

		private int _SuffixMaxDurabilityRecoveryPoint;
	}
}
