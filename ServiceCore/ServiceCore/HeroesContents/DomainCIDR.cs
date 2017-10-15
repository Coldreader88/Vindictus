using System;
using System.Data.Linq.Mapping;

namespace ServiceCore.HeroesContents
{
	[Table(Name = "dbo.DomainCIDR")]
	public class DomainCIDR
	{
		[Column(Storage = "_IPAddress", DbType = "VarChar(40) NOT NULL", CanBeNull = false)]
		public string IPAddress
		{
			get
			{
				return this._IPAddress;
			}
			set
			{
				if (this._IPAddress != value)
				{
					this._IPAddress = value;
				}
			}
		}

		[Column(Storage = "_SignificantBits", DbType = "TinyInt NOT NULL")]
		public byte SignificantBits
		{
			get
			{
				return this._SignificantBits;
			}
			set
			{
				if (this._SignificantBits != value)
				{
					this._SignificantBits = value;
				}
			}
		}

		[Column(Storage = "_DomainID", DbType = "NVarChar(32) NOT NULL", CanBeNull = false)]
		public string DomainID
		{
			get
			{
				return this._DomainID;
			}
			set
			{
				if (this._DomainID != value)
				{
					this._DomainID = value;
				}
			}
		}

		private string _IPAddress;

		private byte _SignificantBits;

		private string _DomainID;
	}
}
