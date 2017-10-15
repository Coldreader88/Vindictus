using System;
using System.Data.Linq.Mapping;

namespace ServiceCore.HeroesContents
{
	[Table(Name = "dbo.DyeableParts")]
	public class DyeableParts
	{
		[Column(Storage = "_ItemClass", DbType = "NVarChar(50) NOT NULL", CanBeNull = false)]
		public string ItemClass
		{
			get
			{
				return this._ItemClass;
			}
			set
			{
				if (this._ItemClass != value)
				{
					this._ItemClass = value;
				}
			}
		}

		[Column(Storage = "_channel1", DbType = "TinyInt NOT NULL")]
		public byte channel1
		{
			get
			{
				return this._channel1;
			}
			set
			{
				if (this._channel1 != value)
				{
					this._channel1 = value;
				}
			}
		}

		[Column(Storage = "_channel2", DbType = "TinyInt NOT NULL")]
		public byte channel2
		{
			get
			{
				return this._channel2;
			}
			set
			{
				if (this._channel2 != value)
				{
					this._channel2 = value;
				}
			}
		}

		[Column(Storage = "_channel3", DbType = "TinyInt NOT NULL")]
		public byte channel3
		{
			get
			{
				return this._channel3;
			}
			set
			{
				if (this._channel3 != value)
				{
					this._channel3 = value;
				}
			}
		}

		private string _ItemClass;

		private byte _channel1;

		private byte _channel2;

		private byte _channel3;
	}
}
