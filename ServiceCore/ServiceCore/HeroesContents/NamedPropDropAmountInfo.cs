using System;
using System.Data.Linq.Mapping;

namespace ServiceCore.HeroesContents
{
	[Table(Name = "dbo.NamedPropDropAmountInfo")]
	public class NamedPropDropAmountInfo
	{
		[Column(Storage = "_EntityName", DbType = "NVarChar(50) NOT NULL", CanBeNull = false)]
		public string EntityName
		{
			get
			{
				return this._EntityName;
			}
			set
			{
				if (this._EntityName != value)
				{
					this._EntityName = value;
				}
			}
		}

		[Column(Storage = "_minDrop", DbType = "BigInt NOT NULL")]
		public long minDrop
		{
			get
			{
				return this._minDrop;
			}
			set
			{
				if (this._minDrop != value)
				{
					this._minDrop = value;
				}
			}
		}

		[Column(Storage = "_maxDrop", DbType = "BigInt NOT NULL")]
		public long maxDrop
		{
			get
			{
				return this._maxDrop;
			}
			set
			{
				if (this._maxDrop != value)
				{
					this._maxDrop = value;
				}
			}
		}

		private string _EntityName;

		private long _minDrop;

		private long _maxDrop;
	}
}
