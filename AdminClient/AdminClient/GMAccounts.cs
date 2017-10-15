using System;
using System.ComponentModel;
using System.Data.Linq.Mapping;

namespace AdminClient
{
	[Table(Name = "dbo.GMAccounts")]
	public class GMAccounts : INotifyPropertyChanging, INotifyPropertyChanged
	{
		[Column(Storage = "_ID", DbType = "NVarChar(20) NOT NULL", CanBeNull = false, IsPrimaryKey = true)]
		public string ID
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
				this.PropertyChanging(this, GMAccounts.emptyChangingEventArgs);
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

		private string _ID;
	}
}
