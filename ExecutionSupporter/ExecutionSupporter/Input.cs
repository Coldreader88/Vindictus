using System;
using System.ComponentModel;
using System.Data.Linq.Mapping;

namespace ExecutionSupporter
{
	[Table(Name = "dbo.Input")]
	public class Input : INotifyPropertyChanging, INotifyPropertyChanged
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

		[Column(Storage = "_Command", DbType = "NVarChar(MAX) NOT NULL", CanBeNull = false)]
		public string Command
		{
			get
			{
				return this._Command;
			}
			set
			{
				if (this._Command != value)
				{
					this.SendPropertyChanging();
					this._Command = value;
					this.SendPropertyChanged("Command");
				}
			}
		}

		[Column(Storage = "_ExecuteTime", DbType = "DateTime2 NOT NULL")]
		public DateTime ExecuteTime
		{
			get
			{
				return this._ExecuteTime;
			}
			set
			{
				if (this._ExecuteTime != value)
				{
					this.SendPropertyChanging();
					this._ExecuteTime = value;
					this.SendPropertyChanged("ExecuteTime");
				}
			}
		}

		[Column(Storage = "_Executed", DbType = "VarChar(50)")]
		public string Executed
		{
			get
			{
				return this._Executed;
			}
			set
			{
				if (this._Executed != value)
				{
					this.SendPropertyChanging();
					this._Executed = value;
					this.SendPropertyChanged("Executed");
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
				this.PropertyChanging(this, Input.emptyChangingEventArgs);
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

		private string _Command;

		private DateTime _ExecuteTime;

		private string _Executed;

		private DateTime _Time;
	}
}
