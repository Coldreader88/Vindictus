using System;
using System.ComponentModel;
using System.Data.Linq.Mapping;

namespace ExecutionSupporter
{
	[Table(Name = "dbo.UserCount")]
	public class UserCount : INotifyPropertyChanging, INotifyPropertyChanged
	{
		[Column(Storage = "_RowID", AutoSync = AutoSync.OnInsert, DbType = "Int NOT NULL IDENTITY", IsPrimaryKey = true, IsDbGenerated = true)]
		public int RowID
		{
			get
			{
				return this._RowID;
			}
			set
			{
				if (this._RowID != value)
				{
					this.SendPropertyChanging();
					this._RowID = value;
					this.SendPropertyChanged("RowID");
				}
			}
		}

		[Column(Storage = "_Text", DbType = "NVarChar(MAX) NOT NULL", CanBeNull = false)]
		public string Text
		{
			get
			{
				return this._Text;
			}
			set
			{
				if (this._Text != value)
				{
					this.SendPropertyChanging();
					this._Text = value;
					this.SendPropertyChanged("Text");
				}
			}
		}

		[Column(Storage = "_Time", DbType = "DateTime2 NOT NULL")]
		public DateTime Time
		{
			get
			{
				return this._Time;
			}
			set
			{
				if (this._Time != value)
				{
					this.SendPropertyChanging();
					this._Time = value;
					this.SendPropertyChanged("Time");
				}
			}
		}

		public event PropertyChangingEventHandler PropertyChanging;

		public event PropertyChangedEventHandler PropertyChanged;

		protected virtual void SendPropertyChanging()
		{
			if (this.PropertyChanging != null)
			{
				this.PropertyChanging(this, UserCount.emptyChangingEventArgs);
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

		private int _RowID;

		private string _Text;

		private DateTime _Time;
	}
}
