using System;
using System.ComponentModel;
using System.Data.Linq.Mapping;

namespace UnifiedNetwork.ProfileService
{
	[Table(Name = "dbo.DailyCharts")]
	public class DailyChart : INotifyPropertyChanging, INotifyPropertyChanged
	{
		[Column(Storage = "_Timestamp", DbType = "DateTime NOT NULL", IsPrimaryKey = true)]
		public DateTime Timestamp
		{
			get
			{
				return this._Timestamp;
			}
			set
			{
				if (this._Timestamp != value)
				{
					this.SendPropertyChanging();
					this._Timestamp = value;
					this.SendPropertyChanged("Timestamp");
				}
			}
		}

		[Column(Storage = "_Volume", DbType = "Int NOT NULL")]
		public int Volume
		{
			get
			{
				return this._Volume;
			}
			set
			{
				if (this._Volume != value)
				{
					this.SendPropertyChanging();
					this._Volume = value;
					this.SendPropertyChanged("Volume");
				}
			}
		}

		[Column(Storage = "_Open", DbType = "Int NOT NULL")]
		public int Open
		{
			get
			{
				return this._Open;
			}
			set
			{
				if (this._Open != value)
				{
					this.SendPropertyChanging();
					this._Open = value;
					this.SendPropertyChanged("Open");
				}
			}
		}

		[Column(Storage = "_High", DbType = "Int NOT NULL")]
		public int High
		{
			get
			{
				return this._High;
			}
			set
			{
				if (this._High != value)
				{
					this.SendPropertyChanging();
					this._High = value;
					this.SendPropertyChanged("High");
				}
			}
		}

		[Column(Storage = "_Low", DbType = "Int NOT NULL")]
		public int Low
		{
			get
			{
				return this._Low;
			}
			set
			{
				if (this._Low != value)
				{
					this.SendPropertyChanging();
					this._Low = value;
					this.SendPropertyChanged("Low");
				}
			}
		}

		[Column(Storage = "_Close", DbType = "Int NOT NULL")]
		public int Close
		{
			get
			{
				return this._Close;
			}
			set
			{
				if (this._Close != value)
				{
					this.SendPropertyChanging();
					this._Close = value;
					this.SendPropertyChanged("Close");
				}
			}
		}

		[Column(Storage = "_Moment1", DbType = "Real NOT NULL")]
		public float Moment1
		{
			get
			{
				return this._Moment1;
			}
			set
			{
				if (this._Moment1 != value)
				{
					this.SendPropertyChanging();
					this._Moment1 = value;
					this.SendPropertyChanged("Moment1");
				}
			}
		}

		[Column(Storage = "_Moment2", DbType = "Real NOT NULL")]
		public float Moment2
		{
			get
			{
				return this._Moment2;
			}
			set
			{
				if (this._Moment2 != value)
				{
					this.SendPropertyChanging();
					this._Moment2 = value;
					this.SendPropertyChanged("Moment2");
				}
			}
		}

		[Column(Storage = "_Moment3", DbType = "Real NOT NULL")]
		public float Moment3
		{
			get
			{
				return this._Moment3;
			}
			set
			{
				if (this._Moment3 != value)
				{
					this.SendPropertyChanging();
					this._Moment3 = value;
					this.SendPropertyChanged("Moment3");
				}
			}
		}

		[Column(Storage = "_Moment4", DbType = "Real NOT NULL")]
		public float Moment4
		{
			get
			{
				return this._Moment4;
			}
			set
			{
				if (this._Moment4 != value)
				{
					this.SendPropertyChanging();
					this._Moment4 = value;
					this.SendPropertyChanged("Moment4");
				}
			}
		}

		[Column(Storage = "_Moment5", DbType = "Real NOT NULL")]
		public float Moment5
		{
			get
			{
				return this._Moment5;
			}
			set
			{
				if (this._Moment5 != value)
				{
					this.SendPropertyChanging();
					this._Moment5 = value;
					this.SendPropertyChanged("Moment5");
				}
			}
		}

		public event PropertyChangingEventHandler PropertyChanging;

		public event PropertyChangedEventHandler PropertyChanged;

		protected virtual void SendPropertyChanging()
		{
			if (this.PropertyChanging != null)
			{
				this.PropertyChanging(this, DailyChart.emptyChangingEventArgs);
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

		private DateTime _Timestamp;

		private int _Volume;

		private int _Open;

		private int _High;

		private int _Low;

		private int _Close;

		private float _Moment1;

		private float _Moment2;

		private float _Moment3;

		private float _Moment4;

		private float _Moment5;
	}
}
