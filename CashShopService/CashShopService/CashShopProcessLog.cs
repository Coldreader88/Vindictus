using System;
using System.ComponentModel;
using System.Data.Linq.Mapping;

namespace CashShopService
{
	[Table(Name = "dbo.CashShopProcessLog")]
	public class CashShopProcessLog : INotifyPropertyChanging, INotifyPropertyChanged
	{
		[Column(Storage = "_JournalID", AutoSync = AutoSync.OnInsert, DbType = "Int NOT NULL IDENTITY", IsPrimaryKey = true, IsDbGenerated = true)]
		public int JournalID
		{
			get
			{
				return this._JournalID;
			}
			set
			{
				if (this._JournalID != value)
				{
					this.SendPropertyChanging();
					this._JournalID = value;
					this.SendPropertyChanged("JournalID");
				}
			}
		}

		[Column(Storage = "_OrderID", DbType = "NVarChar(128)")]
		public string OrderID
		{
			get
			{
				return this._OrderID;
			}
			set
			{
				if (this._OrderID != value)
				{
					this.SendPropertyChanging();
					this._OrderID = value;
					this.SendPropertyChanged("OrderID");
				}
			}
		}

		[Column(Storage = "_OrderNo", DbType = "NVarChar(30)")]
		public string OrderNo
		{
			get
			{
				return this._OrderNo;
			}
			set
			{
				if (this._OrderNo != value)
				{
					this.SendPropertyChanging();
					this._OrderNo = value;
					this.SendPropertyChanged("OrderNo");
				}
			}
		}

		[Column(Storage = "_ProductNo", DbType = "Int NOT NULL")]
		public int ProductNo
		{
			get
			{
				return this._ProductNo;
			}
			set
			{
				if (this._ProductNo != value)
				{
					this.SendPropertyChanging();
					this._ProductNo = value;
					this.SendPropertyChanged("ProductNo");
				}
			}
		}

		[Column(Storage = "_Quantity", DbType = "Int NOT NULL")]
		public int Quantity
		{
			get
			{
				return this._Quantity;
			}
			set
			{
				if (this._Quantity != value)
				{
					this.SendPropertyChanging();
					this._Quantity = value;
					this.SendPropertyChanged("Quantity");
				}
			}
		}

		[Column(Storage = "_CID", DbType = "BigInt NOT NULL")]
		public long CID
		{
			get
			{
				return this._CID;
			}
			set
			{
				if (this._CID != value)
				{
					this.SendPropertyChanging();
					this._CID = value;
					this.SendPropertyChanged("CID");
				}
			}
		}

		[Column(Storage = "_NexonSN", DbType = "Int NOT NULL")]
		public int NexonSN
		{
			get
			{
				return this._NexonSN;
			}
			set
			{
				if (this._NexonSN != value)
				{
					this.SendPropertyChanging();
					this._NexonSN = value;
					this.SendPropertyChanged("NexonSN");
				}
			}
		}

		[Column(Storage = "_OrderType", DbType = "NVarChar(10) NOT NULL", CanBeNull = false)]
		public string OrderType
		{
			get
			{
				return this._OrderType;
			}
			set
			{
				if (this._OrderType != value)
				{
					this.SendPropertyChanging();
					this._OrderType = value;
					this.SendPropertyChanged("OrderType");
				}
			}
		}

		[Column(Storage = "_Event", DbType = "NVarChar(20) NOT NULL", CanBeNull = false)]
		public string Event
		{
			get
			{
				return this._Event;
			}
			set
			{
				if (this._Event != value)
				{
					this.SendPropertyChanging();
					this._Event = value;
					this.SendPropertyChanged("Event");
				}
			}
		}

		[Column(Storage = "_TimeStamp", DbType = "datetime2 not null")]
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

		public event PropertyChangingEventHandler PropertyChanging;

		public event PropertyChangedEventHandler PropertyChanged;

		protected virtual void SendPropertyChanging()
		{
			if (this.PropertyChanging != null)
			{
				this.PropertyChanging(this, CashShopProcessLog.emptyChangingEventArgs);
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

		private int _JournalID;

		private string _OrderID;

		private string _OrderNo;

		private int _ProductNo;

		private int _Quantity;

		private long _CID;

		private int _NexonSN;

		private string _OrderType;

		private string _Event;

		private DateTime _TimeStamp;
	}
}
