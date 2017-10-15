using System;
using System.Data.Linq.Mapping;

namespace ServiceCore.HeroesContents
{
	[Table(Name = "dbo.AbilityClassGroupInfo")]
	public class AbilityClassGroupInfo
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

		[Column(Storage = "_AbilityClass", DbType = "NVarChar(100) NOT NULL", CanBeNull = false)]
		public string AbilityClass
		{
			get
			{
				return this._AbilityClass;
			}
			set
			{
				if (this._AbilityClass != value)
				{
					this._AbilityClass = value;
				}
			}
		}

		[Column(Storage = "_Probability", DbType = "Decimal(18,0) NOT NULL")]
		public decimal Probability
		{
			get
			{
				return this._Probability;
			}
			set
			{
				if (this._Probability != value)
				{
					this._Probability = value;
				}
			}
		}

		private int _ID;

		private string _AbilityClass;

		private decimal _Probability;
	}
}
