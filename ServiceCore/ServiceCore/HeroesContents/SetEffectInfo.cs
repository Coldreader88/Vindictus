using System;
using System.Data.Linq.Mapping;

namespace ServiceCore.HeroesContents
{
	[Table(Name = "dbo.SetEffectInfo")]
	public class SetEffectInfo
	{
		[Column(Storage = "_SetID", DbType = "NVarChar(50) NOT NULL", CanBeNull = false)]
		public string SetID
		{
			get
			{
				return this._SetID;
			}
			set
			{
				if (this._SetID != value)
				{
					this._SetID = value;
				}
			}
		}

		[Column(Storage = "_SetCount", DbType = "Int NOT NULL")]
		public int SetCount
		{
			get
			{
				return this._SetCount;
			}
			set
			{
				if (this._SetCount != value)
				{
					this._SetCount = value;
				}
			}
		}

		[Column(Storage = "_EffectTarget", DbType = "NVarChar(50) NOT NULL", CanBeNull = false)]
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

		[Column(Storage = "_Amount", DbType = "Int NOT NULL")]
		public int Amount
		{
			get
			{
				return this._Amount;
			}
			set
			{
				if (this._Amount != value)
				{
					this._Amount = value;
				}
			}
		}

		private string _SetID;

		private int _SetCount;

		private string _EffectTarget;

		private int _Amount;
	}
}
