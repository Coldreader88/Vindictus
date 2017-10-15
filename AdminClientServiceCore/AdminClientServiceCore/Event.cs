using System;
using System.ComponentModel;
using System.Data.Linq.Mapping;

namespace AdminClientServiceCore
{
	[Table(Name = "dbo.Event")]
	public class Event : INotifyPropertyChanging, INotifyPropertyChanged
	{
		public string ToStringX()
		{
			return string.Format("Name [{0}]\r\nFeature [{1}]\r\nStartScript [{2}]\r\nEndScript [{3}]\r\nStartTime [{4}]\r\nEndTime [{5}]\r\nPeriodBegin [{6}]\r\nPeriodEnd [{7}]\r\nStartMessage [{8}]\r\nNotifyMessage [{9}]\r\nEndMessage [{10}]\r\nNotifyInterval [{11}]\r\nStartCount [{12}]\r\nUserName [{13}]", new object[]
			{
				this.Name,
				this.Feature,
				this.StartScript,
				this.EndScript,
				this.StartTime,
				this.EndTime,
				this.PeriodBegin,
				this.PeriodEnd,
				this.StartMessage,
				this.NotifyMessage,
				this.EndMessage,
				this.NotifyInterval,
				this.StartCount,
				this.UserName
			});
		}

		[Column(Storage = "_Name", DbType = "NVarChar(50) NOT NULL", CanBeNull = false, IsPrimaryKey = true)]
		public string Name
		{
			get
			{
				return this._Name;
			}
			set
			{
				if (this._Name != value)
				{
					this.SendPropertyChanging();
					this._Name = value;
					this.SendPropertyChanged("Name");
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
					this.SendPropertyChanging();
					this._Feature = value;
					this.SendPropertyChanged("Feature");
				}
			}
		}

		[Column(Storage = "_StartScript", DbType = "NVarChar(MAX)")]
		public string StartScript
		{
			get
			{
				return this._StartScript;
			}
			set
			{
				if (this._StartScript != value)
				{
					this.SendPropertyChanging();
					this._StartScript = value;
					this.SendPropertyChanged("StartScript");
				}
			}
		}

		[Column(Storage = "_EndScript", DbType = "NVarChar(MAX)")]
		public string EndScript
		{
			get
			{
				return this._EndScript;
			}
			set
			{
				if (this._EndScript != value)
				{
					this.SendPropertyChanging();
					this._EndScript = value;
					this.SendPropertyChanged("EndScript");
				}
			}
		}

		[Column(Storage = "_StartTime", DbType = "DateTime2")]
		public DateTime? StartTime
		{
			get
			{
				return this._StartTime;
			}
			set
			{
				if (this._StartTime != value)
				{
					this.SendPropertyChanging();
					this._StartTime = value;
					this.SendPropertyChanged("StartTime");
				}
			}
		}

		[Column(Storage = "_EndTime", DbType = "DateTime2")]
		public DateTime? EndTime
		{
			get
			{
				return this._EndTime;
			}
			set
			{
				if (this._EndTime != value)
				{
					this.SendPropertyChanging();
					this._EndTime = value;
					this.SendPropertyChanged("EndTime");
				}
			}
		}

		[Column(Storage = "_PeriodBegin", DbType = "Time")]
		public TimeSpan? PeriodBegin
		{
			get
			{
				return this._PeriodBegin;
			}
			set
			{
				if (this._PeriodBegin != value)
				{
					this.SendPropertyChanging();
					this._PeriodBegin = value;
					this.SendPropertyChanged("PeriodBegin");
				}
			}
		}

		[Column(Storage = "_PeriodEnd", DbType = "Time")]
		public TimeSpan? PeriodEnd
		{
			get
			{
				return this._PeriodEnd;
			}
			set
			{
				if (this._PeriodEnd != value)
				{
					this.SendPropertyChanging();
					this._PeriodEnd = value;
					this.SendPropertyChanged("PeriodEnd");
				}
			}
		}

		[Column(Storage = "_StartMessage", DbType = "NVarChar(256)")]
		public string StartMessage
		{
			get
			{
				return this._StartMessage;
			}
			set
			{
				if (this._StartMessage != value)
				{
					this.SendPropertyChanging();
					this._StartMessage = value;
					this.SendPropertyChanged("StartMessage");
				}
			}
		}

		[Column(Storage = "_NotifyMessage", DbType = "NVarChar(256)")]
		public string NotifyMessage
		{
			get
			{
				return this._NotifyMessage;
			}
			set
			{
				if (this._NotifyMessage != value)
				{
					this.SendPropertyChanging();
					this._NotifyMessage = value;
					this.SendPropertyChanged("NotifyMessage");
				}
			}
		}

		[Column(Storage = "_EndMessage", DbType = "NVarChar(256)")]
		public string EndMessage
		{
			get
			{
				return this._EndMessage;
			}
			set
			{
				if (this._EndMessage != value)
				{
					this.SendPropertyChanging();
					this._EndMessage = value;
					this.SendPropertyChanged("EndMessage");
				}
			}
		}

		[Column(Storage = "_NotifyInterval", DbType = "Int")]
		public int? NotifyInterval
		{
			get
			{
				return this._NotifyInterval;
			}
			set
			{
				if (this._NotifyInterval != value)
				{
					this.SendPropertyChanging();
					this._NotifyInterval = value;
					this.SendPropertyChanged("NotifyInterval");
				}
			}
		}

		[Column(Storage = "_StartCount", DbType = "Int")]
		public int? StartCount
		{
			get
			{
				return this._StartCount;
			}
			set
			{
				if (this._StartCount != value)
				{
					this.SendPropertyChanging();
					this._StartCount = value;
					this.SendPropertyChanged("StartCount");
				}
			}
		}

		[Column(Storage = "_CurrentCount", DbType = "Int NOT NULL")]
		public int CurrentCount
		{
			get
			{
				return this._CurrentCount;
			}
			set
			{
				if (this._CurrentCount != value)
				{
					this.SendPropertyChanging();
					this._CurrentCount = value;
					this.SendPropertyChanged("CurrentCount");
				}
			}
		}

		[Column(Storage = "_UserName", DbType = "NVarChar(50)")]
		public string UserName
		{
			get
			{
				return this._UserName;
			}
			set
			{
				if (this._UserName != value)
				{
					this.SendPropertyChanging();
					this._UserName = value;
					this.SendPropertyChanged("UserName");
				}
			}
		}

		public event PropertyChangingEventHandler PropertyChanging;

		public event PropertyChangedEventHandler PropertyChanged;

		protected virtual void SendPropertyChanging()
		{
			if (this.PropertyChanging != null)
			{
				this.PropertyChanging(this, Event.emptyChangingEventArgs);
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

		private string _Name;

		private string _Feature;

		private string _StartScript;

		private string _EndScript;

		private DateTime? _StartTime;

		private DateTime? _EndTime;

		private TimeSpan? _PeriodBegin;

		private TimeSpan? _PeriodEnd;

		private string _StartMessage;

		private string _NotifyMessage;

		private string _EndMessage;

		private int? _NotifyInterval;

		private int? _StartCount;

		private int _CurrentCount;

		private string _UserName;
	}
}
