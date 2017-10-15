using System;
using System.Data.Linq.Mapping;

namespace ServiceCore.HeroesContents
{
	[Table(Name = "dbo.BurnJackpot")]
	public class BurnJackpot
	{
		[Column(Storage = "_Ratio", DbType = "Float NOT NULL")]
		public double Ratio
		{
			get
			{
				return this._Ratio;
			}
			set
			{
				if (this._Ratio != value)
				{
					this._Ratio = value;
				}
			}
		}

		[Column(Storage = "_MaxReward", DbType = "Int NOT NULL")]
		public int MaxReward
		{
			get
			{
				return this._MaxReward;
			}
			set
			{
				if (this._MaxReward != value)
				{
					this._MaxReward = value;
				}
			}
		}

		[Column(Storage = "_JackpotStart", DbType = "Int NOT NULL")]
		public int JackpotStart
		{
			get
			{
				return this._JackpotStart;
			}
			set
			{
				if (this._JackpotStart != value)
				{
					this._JackpotStart = value;
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

		[Column(Storage = "_StandardPrice", DbType = "Int NOT NULL")]
		public int StandardPrice
		{
			get
			{
				return this._StandardPrice;
			}
			set
			{
				if (this._StandardPrice != value)
				{
					this._StandardPrice = value;
				}
			}
		}

		[Column(Storage = "_Feature", DbType = "NVarChar(100)")]
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

		private double _Ratio;

		private int _MaxReward;

		private int _JackpotStart;

		private double _Probability;

		private int _StandardPrice;

		private string _Feature;
	}
}
