using System;
using System.ComponentModel;
using System.Data.Linq.Mapping;

namespace AdminClientServiceCore
{
	[Table(Name = "dbo.AdminClientAccessLog")]
	public class AdminClientAccessLog : INotifyPropertyChanging, INotifyPropertyChanged
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

		[Column(Storage = "_IP", DbType = "VarChar(20)")]
		public string IP
		{
			get
			{
				return this._IP;
			}
			set
			{
				if (this._IP != value)
				{
					this.SendPropertyChanging();
					this._IP = value;
					this.SendPropertyChanged("IP");
				}
			}
		}

		[Column(Storage = "_REQUEST", DbType = "VarChar(80)")]
		public string REQUEST
		{
			get
			{
				return this._REQUEST;
			}
			set
			{
				if (this._REQUEST != value)
				{
					this.SendPropertyChanging();
					this._REQUEST = value;
					this.SendPropertyChanged("REQUEST");
				}
			}
		}

		public event PropertyChangingEventHandler PropertyChanging;

		public event PropertyChangedEventHandler PropertyChanged;

		protected virtual void SendPropertyChanging()
		{
			if (this.PropertyChanging != null)
			{
				this.PropertyChanging(this, AdminClientAccessLog.emptyChangingEventArgs);
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

		private string _IP;

		private string _REQUEST;
	}
}
