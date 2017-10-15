using System;
using System.Data.Linq.Mapping;

namespace ServiceCore.HeroesContents
{
	[Table(Name = "dbo.RouletteBoardInfo")]
	public class RouletteBoardInfo
	{
		[Column(Storage = "_Country", DbType = "VarChar(8) NOT NULL", CanBeNull = false)]
		public string Country
		{
			get
			{
				return this._Country;
			}
			set
			{
				if (this._Country != value)
				{
					this._Country = value;
				}
			}
		}

		[Column(Storage = "_Feature", DbType = "VarChar(50)")]
		public string Feature
		{
			get
			{
				return this._Feature;
			}
			set
			{
				if (this._Feature != value)
				{
					this._Feature = value;
				}
			}
		}

		[Column(Storage = "_RouletteID", DbType = "Int NOT NULL")]
		public int RouletteID
		{
			get
			{
				return this._RouletteID;
			}
			set
			{
				if (this._RouletteID != value)
				{
					this._RouletteID = value;
				}
			}
		}

		[Column(Storage = "_Version", DbType = "Int NOT NULL")]
		public int Version
		{
			get
			{
				return this._Version;
			}
			set
			{
				if (this._Version != value)
				{
					this._Version = value;
				}
			}
		}

		[Column(Storage = "_TicketItemClass", DbType = "VarChar(50) NOT NULL", CanBeNull = false)]
		public string TicketItemClass
		{
			get
			{
				return this._TicketItemClass;
			}
			set
			{
				if (this._TicketItemClass != value)
				{
					this._TicketItemClass = value;
				}
			}
		}

		private string _Country;

		private string _Feature;

		private int _RouletteID;

		private int _Version;

		private string _TicketItemClass;
	}
}
