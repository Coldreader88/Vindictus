using System;
using System.ComponentModel;
using System.Data.Linq.Mapping;

namespace GuildService
{
	[Table(Name = "dbo.Guild")]
	public class Guild : INotifyPropertyChanging, INotifyPropertyChanged
	{
		[Column(Storage = "_codeGame", DbType = "Int NOT NULL", IsPrimaryKey = true)]
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
					this.SendPropertyChanging();
					this._codeGame = value;
					this.SendPropertyChanged("codeGame");
				}
			}
		}

		[Column(Storage = "_codeServer", DbType = "Int NOT NULL", IsPrimaryKey = true)]
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
					this.SendPropertyChanging();
					this._codeServer = value;
					this.SendPropertyChanged("codeServer");
				}
			}
		}

		[Column(Storage = "_oidGuild", DbType = "Int NOT NULL", IsPrimaryKey = true)]
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
					this.SendPropertyChanging();
					this._oidGuild = value;
					this.SendPropertyChanged("oidGuild");
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
					this.SendPropertyChanging();
					this._MaxMemberLimit = value;
					this.SendPropertyChanged("MaxMemberLimit");
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
					this.SendPropertyChanging();
					this._NewbieRecommend = value;
					this.SendPropertyChanged("NewbieRecommend");
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
					this.SendPropertyChanging();
					this._Level = value;
					this.SendPropertyChanged("Level");
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
					this.SendPropertyChanging();
					this._Exp = value;
					this.SendPropertyChanged("Exp");
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
					this.SendPropertyChanging();
					this._Notice = value;
					this.SendPropertyChanged("Notice");
				}
			}
		}

		public event PropertyChangingEventHandler PropertyChanging;

		public event PropertyChangedEventHandler PropertyChanged;

		protected virtual void SendPropertyChanging()
		{
			if (this.PropertyChanging != null)
			{
				this.PropertyChanging(this, Guild.emptyChangingEventArgs);
			}
		}

		protected virtual void SendPropertyChanged(string propertyName)
		{
			if (this.PropertyChanged != null)
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}

		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(string.Empty);

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
