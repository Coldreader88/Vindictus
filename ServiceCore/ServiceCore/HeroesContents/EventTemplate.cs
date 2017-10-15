using System;
using System.Data.Linq.Mapping;

namespace ServiceCore.HeroesContents
{
	[Table(Name = "dbo.EventTemplate")]
	public class EventTemplate
	{
		[Column(Storage = "_Name", DbType = "NVarChar(50) NOT NULL", CanBeNull = false)]
		public string Name
		{
			get
			{
				return this._Name;
			}
			set
			{
				if (this._Name != value)
				{
					this._Name = value;
				}
			}
		}

		[Column(Storage = "_Feature", DbType = "NVarChar(256)")]
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

		[Column(Storage = "_StartScript", DbType = "NVarChar(4000)")]
		public string StartScript
		{
			get
			{
				return this._StartScript;
			}
			set
			{
				if (this._StartScript != value)
				{
					this._StartScript = value;
				}
			}
		}

		[Column(Storage = "_EndScript", DbType = "NVarChar(4000)")]
		public string EndScript
		{
			get
			{
				return this._EndScript;
			}
			set
			{
				if (this._EndScript != value)
				{
					this._EndScript = value;
				}
			}
		}

		private string _Name;

		private string _Feature;

		private string _StartScript;

		private string _EndScript;
	}
}
