using System;
using System.ComponentModel;
using System.Data.Linq.Mapping;

namespace ExecutionSupporter
{
	[Table(Name = "dbo.MachineInfo")]
	public class MachineInfo : INotifyPropertyChanging, INotifyPropertyChanged
	{
		[Column(Storage = "_Address", DbType = "NVarChar(50) NOT NULL", CanBeNull = false, IsPrimaryKey = true)]
		public string Address
		{
			get
			{
				return this._Address;
			}
			set
			{
				if (this._Address != value)
				{
					this.SendPropertyChanging();
					this._Address = value;
					this.SendPropertyChanged("Address");
				}
			}
		}

		[Column(Storage = "_Status", DbType = "VarChar(50) NOT NULL", CanBeNull = false)]
		public string Status
		{
			get
			{
				return this._Status;
			}
			set
			{
				if (this._Status != value)
				{
					this.SendPropertyChanging();
					this._Status = value;
					this.SendPropertyChanged("Status");
				}
			}
		}

		[Column(Storage = "_Info", DbType = "NVarChar(MAX) NOT NULL", CanBeNull = false)]
		public string Info
		{
			get
			{
				return this._Info;
			}
			set
			{
				if (this._Info != value)
				{
					this.SendPropertyChanging();
					this._Info = value;
					this.SendPropertyChanged("Info");
				}
			}
		}

		public event PropertyChangingEventHandler PropertyChanging;

		public event PropertyChangedEventHandler PropertyChanged;

		protected virtual void SendPropertyChanging()
		{
			if (this.PropertyChanging != null)
			{
				this.PropertyChanging(this, MachineInfo.emptyChangingEventArgs);
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

		private string _Address;

		private string _Status;

		private string _Info;
	}
}
