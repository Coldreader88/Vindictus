using System;
using System.Data.Linq.Mapping;

namespace ServiceCore.HeroesContents
{
	[Table(Name = "dbo.GoldenBallDropInfo")]
	public class GoldenBallDropInfo
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

		[Column(Storage = "_DropCount", DbType = "Int NOT NULL")]
		public int DropCount
		{
			get
			{
				return this._DropCount;
			}
			set
			{
				if (this._DropCount != value)
				{
					this._DropCount = value;
				}
			}
		}

		[Column(Storage = "_ItemClass", DbType = "NVarChar(128) NOT NULL", CanBeNull = false)]
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

		[Column(Storage = "_Probability", DbType = "Int NOT NULL")]
		public int Probability
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

		private int _Identifier;

		private int _DropCount;

		private string _ItemClass;

		private int _Probability;

		private string _Feature;
	}
}
