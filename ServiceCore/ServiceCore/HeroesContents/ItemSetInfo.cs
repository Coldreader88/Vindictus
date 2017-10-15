using System;
using System.Data.Linq.Mapping;

namespace ServiceCore.HeroesContents
{
	[Table(Name = "dbo.ItemSetInfo")]
	public class ItemSetInfo
	{
		[Column(Storage = "_Identifier", DbType = "Int NOT NULL")]
		public int Identifier
		{
			get
			{
				return this._Identifier;
			}
			set
			{
				if (this._Identifier != value)
				{
					this._Identifier = value;
				}
			}
		}

		[Column(Storage = "_SetBonusCoeff1", DbType = "Int NOT NULL")]
		public int SetBonusCoeff1
		{
			get
			{
				return this._SetBonusCoeff1;
			}
			set
			{
				if (this._SetBonusCoeff1 != value)
				{
					this._SetBonusCoeff1 = value;
				}
			}
		}

		[Column(Storage = "_SetBonusCoeff2", DbType = "Int NOT NULL")]
		public int SetBonusCoeff2
		{
			get
			{
				return this._SetBonusCoeff2;
			}
			set
			{
				if (this._SetBonusCoeff2 != value)
				{
					this._SetBonusCoeff2 = value;
				}
			}
		}

		private int _Identifier;

		private int _SetBonusCoeff1;

		private int _SetBonusCoeff2;
	}
}
