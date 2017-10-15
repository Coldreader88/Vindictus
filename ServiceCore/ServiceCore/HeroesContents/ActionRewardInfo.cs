using System;
using System.Data.Linq.Mapping;

namespace ServiceCore.HeroesContents
{
	[Table(Name = "dbo.ActionRewardInfo")]
	public class ActionRewardInfo
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

		[Column(Storage = "_Action", DbType = "NVarChar(64) NOT NULL", CanBeNull = false)]
		public string Action
		{
			get
			{
				return this._Action;
			}
			set
			{
				if (this._Action != value)
				{
					this._Action = value;
				}
			}
		}

		[Column(Name = "foreach", Storage = "_foreach", DbType = "TinyInt NOT NULL")]
		public byte @foreach
		{
			get
			{
				return this._foreach;
			}
			set
			{
				if (this._foreach != value)
				{
					this._foreach = value;
				}
			}
		}

		[Column(Storage = "_Reward", DbType = "NVarChar(50) NOT NULL", CanBeNull = false)]
		public string Reward
		{
			get
			{
				return this._Reward;
			}
			set
			{
				if (this._Reward != value)
				{
					this._Reward = value;
				}
			}
		}

		[Column(Storage = "_RewardCount", DbType = "Int NOT NULL")]
		public int RewardCount
		{
			get
			{
				return this._RewardCount;
			}
			set
			{
				if (this._RewardCount != value)
				{
					this._RewardCount = value;
				}
			}
		}

		[Column(Storage = "_MailOnFailed", DbType = "Bit NOT NULL")]
		public bool MailOnFailed
		{
			get
			{
				return this._MailOnFailed;
			}
			set
			{
				if (this._MailOnFailed != value)
				{
					this._MailOnFailed = value;
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

		private int _ID;

		private string _Action;

		private byte _foreach;

		private string _Reward;

		private int _RewardCount;

		private bool _MailOnFailed;

		private string _Feature;
	}
}
