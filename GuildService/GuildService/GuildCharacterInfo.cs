using System;
using System.ComponentModel;
using System.Data.Linq.Mapping;

namespace GuildService
{
	[Table(Name = "dbo.GuildCharacterInfo")]
	public class GuildCharacterInfo : INotifyPropertyChanging, INotifyPropertyChanged
	{
		[Column(Storage = "_CID", DbType = "BigInt NOT NULL", IsPrimaryKey = true)]
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

		[Column(Storage = "_Point", DbType = "BigInt NOT NULL")]
		public long Point
		{
			get
			{
				return this._Point;
			}
			set
			{
				if (this._Point != value)
				{
					this.SendPropertyChanging();
					this._Point = value;
					this.SendPropertyChanged("Point");
				}
			}
		}

		public event PropertyChangingEventHandler PropertyChanging;

		public event PropertyChangedEventHandler PropertyChanged;

		protected virtual void SendPropertyChanging()
		{
			if (this.PropertyChanging != null)
			{
				this.PropertyChanging(this, GuildCharacterInfo.emptyChangingEventArgs);
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

		private long _CID;

		private long _Point;
	}
}
