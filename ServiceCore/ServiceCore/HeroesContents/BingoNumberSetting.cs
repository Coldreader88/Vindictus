using System;
using System.Data.Linq.Mapping;

namespace ServiceCore.HeroesContents
{
	[Table(Name = "dbo.BingoNumberSetting")]
	public class BingoNumberSetting
	{
		[Column(Storage = "_BingoEvent", DbType = "Int NOT NULL")]
		public int BingoEvent
		{
			get
			{
				return this._BingoEvent;
			}
			set
			{
				if (this._BingoEvent != value)
				{
					this._BingoEvent = value;
				}
			}
		}

		[Column(Storage = "_RangeStart", DbType = "Int NOT NULL")]
		public int RangeStart
		{
			get
			{
				return this._RangeStart;
			}
			set
			{
				if (this._RangeStart != value)
				{
					this._RangeStart = value;
				}
			}
		}

		[Column(Storage = "_RangeEnd", DbType = "Int NOT NULL")]
		public int RangeEnd
		{
			get
			{
				return this._RangeEnd;
			}
			set
			{
				if (this._RangeEnd != value)
				{
					this._RangeEnd = value;
				}
			}
		}

		[Column(Storage = "_Count", DbType = "Int NOT NULL")]
		public int Count
		{
			get
			{
				return this._Count;
			}
			set
			{
				if (this._Count != value)
				{
					this._Count = value;
				}
			}
		}

		private int _BingoEvent;

		private int _RangeStart;

		private int _RangeEnd;

		private int _Count;
	}
}
