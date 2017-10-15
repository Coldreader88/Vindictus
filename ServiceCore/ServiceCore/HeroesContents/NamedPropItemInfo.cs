using System;
using System.Data.Linq.Mapping;

namespace ServiceCore.HeroesContents
{
	[Table(Name = "dbo.NamedPropItemInfo")]
	public class NamedPropItemInfo
	{
		[Column(Storage = "_EntityName", DbType = "NVarChar(50) NOT NULL", CanBeNull = false)]
		public string EntityName
		{
			get
			{
				return this._EntityName;
			}
			set
			{
				if (this._EntityName != value)
				{
					this._EntityName = value;
				}
			}
		}

		[Column(Storage = "_Difficulty", DbType = "Int NOT NULL")]
		public int Difficulty
		{
			get
			{
				return this._Difficulty;
			}
			set
			{
				if (this._Difficulty != value)
				{
					this._Difficulty = value;
				}
			}
		}

		[Column(Storage = "_Class", DbType = "NVarChar(50) NOT NULL", CanBeNull = false)]
		public string Class
		{
			get
			{
				return this._Class;
			}
			set
			{
				if (this._Class != value)
				{
					this._Class = value;
				}
			}
		}

		[Column(Storage = "_Type", DbType = "NVarChar(50) NOT NULL", CanBeNull = false)]
		public string Type
		{
			get
			{
				return this._Type;
			}
			set
			{
				if (this._Type != value)
				{
					this._Type = value;
				}
			}
		}

		[Column(Storage = "_Probability", DbType = "BigInt NOT NULL")]
		public long Probability
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

		[Column(Storage = "_Feature", DbType = "VarChar(50)")]
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

		private string _EntityName;

		private int _Difficulty;

		private string _Class;

		private string _Type;

		private long _Probability;

		private string _Feature;
	}
}
