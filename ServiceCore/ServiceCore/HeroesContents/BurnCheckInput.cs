using System;
using System.Data.Linq.Mapping;

namespace ServiceCore.HeroesContents
{
	[Table(Name = "dbo.BurnCheckInput")]
	public class BurnCheckInput
	{
		[Column(Storage = "_InputPriceMin", DbType = "Int")]
		public int? InputPriceMin
		{
			get
			{
				return this._InputPriceMin;
			}
			set
			{
				if (this._InputPriceMin != value)
				{
					this._InputPriceMin = value;
				}
			}
		}

		[Column(Storage = "_InputPriceMax", DbType = "Int")]
		public int? InputPriceMax
		{
			get
			{
				return this._InputPriceMax;
			}
			set
			{
				if (this._InputPriceMax != value)
				{
					this._InputPriceMax = value;
				}
			}
		}

		[Column(Storage = "_ResultItemClass", DbType = "NVarChar(64) NOT NULL", CanBeNull = false)]
		public string ResultItemClass
		{
			get
			{
				return this._ResultItemClass;
			}
			set
			{
				if (this._ResultItemClass != value)
				{
					this._ResultItemClass = value;
				}
			}
		}

		[Column(Storage = "_Probability", DbType = "Float NOT NULL")]
		public double Probability
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

		private int? _InputPriceMin;

		private int? _InputPriceMax;

		private string _ResultItemClass;

		private double _Probability;
	}
}
