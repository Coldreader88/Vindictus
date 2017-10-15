using System;
using System.ComponentModel;
using System.Data.Linq.Mapping;

namespace UnifiedNetwork.ProfileService
{
	[Table(Name = "dbo.LogProfile")]
	public class LogProfile : INotifyPropertyChanging, INotifyPropertyChanged
	{
		[Column(Storage = "_TimeStamp", DbType = "DateTime NOT NULL")]
		public DateTime TimeStamp
		{
			get
			{
				return this._TimeStamp;
			}
			set
			{
				if (this._TimeStamp != value)
				{
					this.SendPropertyChanging();
					this._TimeStamp = value;
					this.SendPropertyChanged("TimeStamp");
				}
			}
		}

		[Column(Storage = "_Operation", DbType = "VarChar(80) NOT NULL", CanBeNull = false)]
		public string Operation
		{
			get
			{
				return this._Operation;
			}
			set
			{
				if (this._Operation != value)
				{
					this.SendPropertyChanging();
					this._Operation = value;
					this.SendPropertyChanged("Operation");
				}
			}
		}

		[Column(Storage = "_ElapsedTicks", DbType = "BigInt NOT NULL")]
		public long ElapsedTicks
		{
			get
			{
				return this._ElapsedTicks;
			}
			set
			{
				if (this._ElapsedTicks != value)
				{
					this.SendPropertyChanging();
					this._ElapsedTicks = value;
					this.SendPropertyChanged("ElapsedTicks");
				}
			}
		}

		[Column(Storage = "_ElapsedMillisecond", DbType = "BigInt NOT NULL")]
		public long ElapsedMillisecond
		{
			get
			{
				return this._ElapsedMillisecond;
			}
			set
			{
				if (this._ElapsedMillisecond != value)
				{
					this.SendPropertyChanging();
					this._ElapsedMillisecond = value;
					this.SendPropertyChanged("ElapsedMillisecond");
				}
			}
		}

		[Column(Storage = "_EntitySize", DbType = "Int NOT NULL")]
		public int EntitySize
		{
			get
			{
				return this._EntitySize;
			}
			set
			{
				if (this._EntitySize != value)
				{
					this.SendPropertyChanging();
					this._EntitySize = value;
					this.SendPropertyChanged("EntitySize");
				}
			}
		}

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

		public event PropertyChangingEventHandler PropertyChanging;

		public event PropertyChangedEventHandler PropertyChanged;

		protected virtual void SendPropertyChanging()
		{
			if (this.PropertyChanging != null)
			{
				this.PropertyChanging(this, LogProfile.emptyChangingEventArgs);
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

		private DateTime _TimeStamp;

		private string _Operation;

		private long _ElapsedTicks;

		private long _ElapsedMillisecond;

		private int _EntitySize;

		private long _ID;
	}
}
