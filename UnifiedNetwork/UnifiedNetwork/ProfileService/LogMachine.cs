using System;
using System.ComponentModel;
using System.Data.Linq.Mapping;

namespace UnifiedNetwork.ProfileService
{
	[Table(Name = "dbo.LogMachine")]
	public class LogMachine : INotifyPropertyChanging, INotifyPropertyChanged
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

		[Column(Storage = "_CpuUse", DbType = "Float NOT NULL")]
		public double CpuUse
		{
			get
			{
				return this._CpuUse;
			}
			set
			{
				if (this._CpuUse != value)
				{
					this.SendPropertyChanging();
					this._CpuUse = value;
					this.SendPropertyChanged("CpuUse");
				}
			}
		}

		[Column(Storage = "_PhysicalMemoryUse", DbType = "BigInt NOT NULL")]
		public long PhysicalMemoryUse
		{
			get
			{
				return this._PhysicalMemoryUse;
			}
			set
			{
				if (this._PhysicalMemoryUse != value)
				{
					this.SendPropertyChanging();
					this._PhysicalMemoryUse = value;
					this.SendPropertyChanged("PhysicalMemoryUse");
				}
			}
		}

		[Column(Storage = "_PhysicalMemoryLimit", DbType = "BigInt NOT NULL")]
		public long PhysicalMemoryLimit
		{
			get
			{
				return this._PhysicalMemoryLimit;
			}
			set
			{
				if (this._PhysicalMemoryLimit != value)
				{
					this.SendPropertyChanging();
					this._PhysicalMemoryLimit = value;
					this.SendPropertyChanged("PhysicalMemoryLimit");
				}
			}
		}

		[Column(Storage = "_VirtualMemoryUse", DbType = "BigInt NOT NULL")]
		public long VirtualMemoryUse
		{
			get
			{
				return this._VirtualMemoryUse;
			}
			set
			{
				if (this._VirtualMemoryUse != value)
				{
					this.SendPropertyChanging();
					this._VirtualMemoryUse = value;
					this.SendPropertyChanged("VirtualMemoryUse");
				}
			}
		}

		[Column(Storage = "_VirtualMemoryLimit", DbType = "BigInt NOT NULL")]
		public long VirtualMemoryLimit
		{
			get
			{
				return this._VirtualMemoryLimit;
			}
			set
			{
				if (this._VirtualMemoryLimit != value)
				{
					this.SendPropertyChanging();
					this._VirtualMemoryLimit = value;
					this.SendPropertyChanged("VirtualMemoryLimit");
				}
			}
		}

		[Column(Storage = "_NetworkReceived", DbType = "BigInt NOT NULL")]
		public long NetworkReceived
		{
			get
			{
				return this._NetworkReceived;
			}
			set
			{
				if (this._NetworkReceived != value)
				{
					this.SendPropertyChanging();
					this._NetworkReceived = value;
					this.SendPropertyChanged("NetworkReceived");
				}
			}
		}

		[Column(Storage = "_NetworkSent", DbType = "BigInt NOT NULL")]
		public long NetworkSent
		{
			get
			{
				return this._NetworkSent;
			}
			set
			{
				if (this._NetworkSent != value)
				{
					this.SendPropertyChanging();
					this._NetworkSent = value;
					this.SendPropertyChanged("NetworkSent");
				}
			}
		}

		[Column(Storage = "_MachineName", DbType = "VarChar(MAX) NOT NULL", CanBeNull = false, UpdateCheck = UpdateCheck.Never)]
		public string MachineName
		{
			get
			{
				return this._MachineName;
			}
			set
			{
				if (this._MachineName != value)
				{
					this.SendPropertyChanging();
					this._MachineName = value;
					this.SendPropertyChanged("MachineName");
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
				this.PropertyChanging(this, LogMachine.emptyChangingEventArgs);
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

		private double _CpuUse;

		private long _PhysicalMemoryUse;

		private long _PhysicalMemoryLimit;

		private long _VirtualMemoryUse;

		private long _VirtualMemoryLimit;

		private long _NetworkReceived;

		private long _NetworkSent;

		private string _MachineName;

		private long _ID;
	}
}
