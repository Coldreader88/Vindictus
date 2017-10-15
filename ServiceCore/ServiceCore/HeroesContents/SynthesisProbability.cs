using System;
using System.Data.Linq.Mapping;

namespace ServiceCore.HeroesContents
{
	[Table(Name = "dbo.SynthesisProbability")]
	public class SynthesisProbability
	{
		[Column(Storage = "_Grade", DbType = "NVarChar(20) NOT NULL", CanBeNull = false)]
		public string Grade
		{
			get
			{
				return this._Grade;
			}
			set
			{
				if (this._Grade != value)
				{
					this._Grade = value;
				}
			}
		}

		[Column(Storage = "_GradeValue", DbType = "Int NOT NULL")]
		public int GradeValue
		{
			get
			{
				return this._GradeValue;
			}
			set
			{
				if (this._GradeValue != value)
				{
					this._GradeValue = value;
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

		[Column(Storage = "_BonusCount", DbType = "Int NOT NULL")]
		public int BonusCount
		{
			get
			{
				return this._BonusCount;
			}
			set
			{
				if (this._BonusCount != value)
				{
					this._BonusCount = value;
				}
			}
		}

		[Column(Storage = "_Code", DbType = "NVarChar(20)")]
		public string Code
		{
			get
			{
				return this._Code;
			}
			set
			{
				if (this._Code != value)
				{
					this._Code = value;
				}
			}
		}

		[Column(Storage = "_Feature", DbType = "NVarChar(256)")]
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

		private string _Grade;

		private int _GradeValue;

		private double _Probability;

		private int _BonusCount;

		private string _Code;

		private string _Feature;
	}
}
