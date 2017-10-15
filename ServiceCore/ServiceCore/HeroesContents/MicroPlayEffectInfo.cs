using System;
using System.Data.Linq.Mapping;

namespace ServiceCore.HeroesContents
{
	[Table(Name = "dbo.MicroPlayEffectInfo")]
	public class MicroPlayEffectInfo
	{
		[Column(Storage = "_EffectSource", DbType = "NVarChar(128) NOT NULL", CanBeNull = false)]
		public string EffectSource
		{
			get
			{
				return this._EffectSource;
			}
			set
			{
				if (this._EffectSource != value)
				{
					this._EffectSource = value;
				}
			}
		}

		[Column(Storage = "_EffectOperation", DbType = "NVarChar(32) NOT NULL", CanBeNull = false)]
		public string EffectOperation
		{
			get
			{
				return this._EffectOperation;
			}
			set
			{
				if (this._EffectOperation != value)
				{
					this._EffectOperation = value;
				}
			}
		}

		[Column(Storage = "_Condition", DbType = "NVarChar(128)")]
		public string Condition
		{
			get
			{
				return this._Condition;
			}
			set
			{
				if (this._Condition != value)
				{
					this._Condition = value;
				}
			}
		}

		[Column(Storage = "_EffectTarget", DbType = "NVarChar(32) NOT NULL", CanBeNull = false)]
		public string EffectTarget
		{
			get
			{
				return this._EffectTarget;
			}
			set
			{
				if (this._EffectTarget != value)
				{
					this._EffectTarget = value;
				}
			}
		}

		[Column(Storage = "_EffectTargetAmount", DbType = "Int NOT NULL")]
		public int EffectTargetAmount
		{
			get
			{
				return this._EffectTargetAmount;
			}
			set
			{
				if (this._EffectTargetAmount != value)
				{
					this._EffectTargetAmount = value;
				}
			}
		}

		[Column(Storage = "_Effect1", DbType = "NVarChar(128)")]
		public string Effect1
		{
			get
			{
				return this._Effect1;
			}
			set
			{
				if (this._Effect1 != value)
				{
					this._Effect1 = value;
				}
			}
		}

		[Column(Storage = "_Effect2", DbType = "NVarChar(128)")]
		public string Effect2
		{
			get
			{
				return this._Effect2;
			}
			set
			{
				if (this._Effect2 != value)
				{
					this._Effect2 = value;
				}
			}
		}

		[Column(Storage = "_Effect3", DbType = "NVarChar(128)")]
		public string Effect3
		{
			get
			{
				return this._Effect3;
			}
			set
			{
				if (this._Effect3 != value)
				{
					this._Effect3 = value;
				}
			}
		}

		[Column(Storage = "_Effect4", DbType = "NVarChar(128)")]
		public string Effect4
		{
			get
			{
				return this._Effect4;
			}
			set
			{
				if (this._Effect4 != value)
				{
					this._Effect4 = value;
				}
			}
		}

		[Column(Storage = "_Effect5", DbType = "NVarChar(128)")]
		public string Effect5
		{
			get
			{
				return this._Effect5;
			}
			set
			{
				if (this._Effect5 != value)
				{
					this._Effect5 = value;
				}
			}
		}

		[Column(Storage = "_Feature", DbType = "NVarChar(128)")]
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

		private string _EffectSource;

		private string _EffectOperation;

		private string _Condition;

		private string _EffectTarget;

		private int _EffectTargetAmount;

		private string _Effect1;

		private string _Effect2;

		private string _Effect3;

		private string _Effect4;

		private string _Effect5;

		private string _Feature;
	}
}
