using System;
using System.Data.Linq.Mapping;

namespace ServiceCore.HeroesContents
{
	[Table(Name = "dbo.AntiBindInfo")]
	public class AntiBindInfo
	{
		[Column(Storage = "_Target", DbType = "NVarChar(50) NOT NULL", CanBeNull = false)]
		public string Target
		{
			get
			{
				return this._Target;
			}
			set
			{
				if (this._Target != value)
				{
					this._Target = value;
				}
			}
		}

		[Column(Name = "[From]", Storage = "_From", DbType = "NVarChar(50) NULL")]
		public string From
		{
			get
			{
				return this._From;
			}
			set
			{
				if (this._From != value)
				{
					this._From = value;
				}
			}
		}

		[Column(Name = "[To]", Storage = "_To", DbType = "NVarChar(50) NULL")]
		public string To
		{
			get
			{
				return this._To;
			}
			set
			{
				if (this._To != value)
				{
					this._To = value;
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

		[Column(Storage = "_AP", DbType = "Int NOT NULL")]
		public int AP
		{
			get
			{
				return this._AP;
			}
			set
			{
				if (this._AP != value)
				{
					this._AP = value;
				}
			}
		}

		[Column(Storage = "_Feature", DbType = "NVarChar(50)")]
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

		private string _Target;

		private string _From;

		private string _To;

		private int _Count;

		private int _AP;

		private string _Feature;
	}
}
