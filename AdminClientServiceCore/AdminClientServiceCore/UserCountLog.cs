using System;
using System.ComponentModel;
using System.Data.Linq.Mapping;

namespace AdminClientServiceCore
{
	[Table(Name = "dbo.UserCountLog")]
	public class UserCountLog : INotifyPropertyChanging, INotifyPropertyChanged
	{
		[Column(Storage = "_ID", AutoSync = AutoSync.OnInsert, DbType = "BigInt NOT NULL IDENTITY", IsPrimaryKey = true, IsDbGenerated = true)]
		public long ID
		{
			get
			{
				return this._ID;
			}
			set
			{
				if (this._ID != value)
				{
					this.SendPropertyChanging();
					this._ID = value;
					this.SendPropertyChanged("ID");
				}
			}
		}

		[Column(Storage = "_TIMESTAMP", DbType = "DateTime")]
		public DateTime? TIMESTAMP
		{
			get
			{
				return this._TIMESTAMP;
			}
			set
			{
				if (this._TIMESTAMP != value)
				{
					this.SendPropertyChanging();
					this._TIMESTAMP = value;
					this.SendPropertyChanged("TIMESTAMP");
				}
			}
		}

		[Column(Storage = "_usercount", DbType = "Int")]
		public int? usercount
		{
			get
			{
				return this._usercount;
			}
			set
			{
				if (this._usercount != value)
				{
					this.SendPropertyChanging();
					this._usercount = value;
					this.SendPropertyChanged("usercount");
				}
			}
		}

		[Column(Storage = "_Quest", DbType = "Int NOT NULL")]
		public int Quest
		{
			get
			{
				return this._Quest;
			}
			set
			{
				if (this._Quest != value)
				{
					this.SendPropertyChanging();
					this._Quest = value;
					this.SendPropertyChanged("Quest");
				}
			}
		}

		[Column(Storage = "_PVP_PMatch", DbType = "Int NOT NULL")]
		public int PVP_PMatch
		{
			get
			{
				return this._PVP_PMatch;
			}
			set
			{
				if (this._PVP_PMatch != value)
				{
					this.SendPropertyChanging();
					this._PVP_PMatch = value;
					this.SendPropertyChanged("PVP_PMatch");
				}
			}
		}

		[Column(Storage = "_PVP_MMatch", DbType = "Int NOT NULL")]
		public int PVP_MMatch
		{
			get
			{
				return this._PVP_MMatch;
			}
			set
			{
				if (this._PVP_MMatch != value)
				{
					this.SendPropertyChanging();
					this._PVP_MMatch = value;
					this.SendPropertyChanged("PVP_MMatch");
				}
			}
		}

		[Column(Storage = "_DS", DbType = "Int NOT NULL")]
		public int DS
		{
			get
			{
				return this._DS;
			}
			set
			{
				if (this._DS != value)
				{
					this.SendPropertyChanging();
					this._DS = value;
					this.SendPropertyChanged("DS");
				}
			}
		}

		[Column(Storage = "_Fish", DbType = "Int NOT NULL")]
		public int Fish
		{
			get
			{
				return this._Fish;
			}
			set
			{
				if (this._Fish != value)
				{
					this.SendPropertyChanging();
					this._Fish = value;
					this.SendPropertyChanged("Fish");
				}
			}
		}

		[Column(Storage = "_Elite", DbType = "Int NOT NULL")]
		public int Elite
		{
			get
			{
				return this._Elite;
			}
			set
			{
				if (this._Elite != value)
				{
					this.SendPropertyChanging();
					this._Elite = value;
					this.SendPropertyChanged("Elite");
				}
			}
		}

		[Column(Storage = "_Tower", DbType = "Int NOT NULL")]
		public int Tower
		{
			get
			{
				return this._Tower;
			}
			set
			{
				if (this._Tower != value)
				{
					this.SendPropertyChanging();
					this._Tower = value;
					this.SendPropertyChanged("Tower");
				}
			}
		}

		[Column(Storage = "_QuestDS", DbType = "Int")]
		public int? QuestDS
		{
			get
			{
				return this._QuestDS;
			}
			set
			{
				if (this._QuestDS != value)
				{
					this.SendPropertyChanging();
					this._QuestDS = value;
					this.SendPropertyChanged("QuestDS");
				}
			}
		}

		[Column(Storage = "_Wait", DbType = "Int NOT NULL")]
		public int Wait
		{
			get
			{
				return this._Wait;
			}
			set
			{
				if (this._Wait != value)
				{
					this.SendPropertyChanging();
					this._Wait = value;
					this.SendPropertyChanged("Wait");
				}
			}
		}

		[Column(Storage = "_RegionCode", DbType = "Char(2)")]
		public string RegionCode
		{
			get
			{
				return this._RegionCode;
			}
			set
			{
				if (this._RegionCode != value)
				{
					this.SendPropertyChanging();
					this._RegionCode = value;
					this.SendPropertyChanged("RegionCode");
				}
			}
		}

		[Column(Storage = "_PVP_DMatch", DbType = "Int NOT NULL")]
		public int PVP_DMatch
		{
			get
			{
				return this._PVP_DMatch;
			}
			set
			{
				if (this._PVP_DMatch != value)
				{
					this.SendPropertyChanging();
					this._PVP_DMatch = value;
					this.SendPropertyChanged("PVP_DMatch");
				}
			}
		}

		[Column(Storage = "_PVP_GMatch", DbType = "Int NOT NULL")]
		public int PVP_GMatch
		{
			get
			{
				return this._PVP_GMatch;
			}
			set
			{
				if (this._PVP_GMatch != value)
				{
					this.SendPropertyChanging();
					this._PVP_GMatch = value;
					this.SendPropertyChanged("PVP_GMatch");
				}
			}
		}

		[Column(Storage = "_PVP_Arena", DbType = "Int NOT NULL")]
		public int PVP_Arena
		{
			get
			{
				return this._PVP_Arena;
			}
			set
			{
				if (this._PVP_Arena != value)
				{
					this.SendPropertyChanging();
					this._PVP_Arena = value;
					this.SendPropertyChanged("PVP_Arena");
				}
			}
		}

		[Column(Storage = "_PVP_FMatch", DbType = "Int NOT NULL")]
		public int PVP_FMatch
		{
			get
			{
				return this._PVP_FMatch;
			}
			set
			{
				if (this._PVP_FMatch != value)
				{
					this.SendPropertyChanging();
					this._PVP_FMatch = value;
					this.SendPropertyChanged("PVP_FMatch");
				}
			}
		}

		public event PropertyChangingEventHandler PropertyChanging;

		public event PropertyChangedEventHandler PropertyChanged;

		protected virtual void SendPropertyChanging()
		{
			if (this.PropertyChanging != null)
			{
				this.PropertyChanging(this, UserCountLog.emptyChangingEventArgs);
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

		private long _ID;

		private DateTime? _TIMESTAMP;

		private int? _usercount;

		private int _Quest;

		private int _PVP_PMatch;

		private int _PVP_MMatch;

		private int _DS;

		private int _Fish;

		private int _Elite;

		private int _Tower;

		private int? _QuestDS;

		private int _Wait;

		private string _RegionCode;

		private int _PVP_DMatch;

		private int _PVP_GMatch;

		private int _PVP_Arena;

		private int _PVP_FMatch;
	}
}
