using System;
using System.Data.Linq.Mapping;

namespace GuildService
{
	public class GetInGameGuildInfo
	{
		[Column(Storage = "_codeGame", DbType = "Int NOT NULL")]
		public int codeGame
		{
			get
			{
				return this._codeGame;
			}
			set
			{
				if (this._codeGame != value)
				{
					this._codeGame = value;
				}
			}
		}

		[Column(Storage = "_codeServer", DbType = "Int NOT NULL")]
		public int codeServer
		{
			get
			{
				return this._codeServer;
			}
			set
			{
				if (this._codeServer != value)
				{
					this._codeServer = value;
				}
			}
		}

		[Column(Storage = "_oidGuild", DbType = "Int NOT NULL")]
		public int oidGuild
		{
			get
			{
				return this._oidGuild;
			}
			set
			{
				if (this._oidGuild != value)
				{
					this._oidGuild = value;
				}
			}
		}

		[Column(Storage = "_MaxMemberLimit", DbType = "Int NOT NULL")]
		public int MaxMemberLimit
		{
			get
			{
				return this._MaxMemberLimit;
			}
			set
			{
				if (this._MaxMemberLimit != value)
				{
					this._MaxMemberLimit = value;
				}
			}
		}

		[Column(Storage = "_NewbieRecommend", DbType = "Bit NOT NULL")]
		public bool NewbieRecommend
		{
			get
			{
				return this._NewbieRecommend;
			}
			set
			{
				if (this._NewbieRecommend != value)
				{
					this._NewbieRecommend = value;
				}
			}
		}

		[Column(Name = "[Level]", Storage = "_Level", DbType = "Int NOT NULL")]
		public int Level
		{
			get
			{
				return this._Level;
			}
			set
			{
				if (this._Level != value)
				{
					this._Level = value;
				}
			}
		}

		[Column(Storage = "_Exp", DbType = "BigInt NOT NULL")]
		public long Exp
		{
			get
			{
				return this._Exp;
			}
			set
			{
				if (this._Exp != value)
				{
					this._Exp = value;
				}
			}
		}

		[Column(Storage = "_Notice", DbType = "NVarChar(256)")]
		public string Notice
		{
			get
			{
				return this._Notice;
			}
			set
			{
				if (this._Notice != value)
				{
					this._Notice = value;
				}
			}
		}

		private int _codeGame;

		private int _codeServer;

		private int _oidGuild;

		private int _MaxMemberLimit;

		private bool _NewbieRecommend;

		private int _Level;

		private long _Exp;

		private string _Notice;
	}
}
