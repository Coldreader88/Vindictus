using System;
using System.Data.Linq.Mapping;

namespace ServiceCore.HeroesContents
{
	[Table(Name = "dbo.PeriodicEventInfo")]
	public class PeriodicEventInfo
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

		[Column(Storage = "_RewardType", DbType = "NVarChar(32) NOT NULL", CanBeNull = false)]
		public string RewardType
		{
			get
			{
				return this._RewardType;
			}
			set
			{
				if (this._RewardType != value)
				{
					this._RewardType = value;
				}
			}
		}

		[Column(Storage = "_RewardArg", DbType = "NVarChar(128)")]
		public string RewardArg
		{
			get
			{
				return this._RewardArg;
			}
			set
			{
				if (this._RewardArg != value)
				{
					this._RewardArg = value;
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

		[Column(Storage = "_Possibility", DbType = "Int NOT NULL")]
		public int Possibility
		{
			get
			{
				return this._Possibility;
			}
			set
			{
				if (this._Possibility != value)
				{
					this._Possibility = value;
				}
			}
		}

		[Column(Storage = "_AnnounceLocalizedKey", DbType = "NVarChar(128)")]
		public string AnnounceLocalizedKey
		{
			get
			{
				return this._AnnounceLocalizedKey;
			}
			set
			{
				if (this._AnnounceLocalizedKey != value)
				{
					this._AnnounceLocalizedKey = value;
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

		private string _RewardType;

		private string _RewardArg;

		private int _Amount;

		private int _Possibility;

		private string _AnnounceLocalizedKey;

		private string _Feature;
	}
}
