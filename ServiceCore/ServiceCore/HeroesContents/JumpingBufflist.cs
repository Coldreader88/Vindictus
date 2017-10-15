using System;
using System.Data.Linq.Mapping;

namespace ServiceCore.HeroesContents
{
	[Table(Name = "dbo.JumpingBufflist")]
	public class JumpingBufflist
	{
		[Column(Storage = "_StatusType", DbType = "NVarChar(256) NOT NULL", CanBeNull = false)]
		public string StatusType
		{
			get
			{
				return this._StatusType;
			}
			set
			{
				if (this._StatusType != value)
				{
					this._StatusType = value;
				}
			}
		}

		[Column(Storage = "_Feature", DbType = "NVarChar(32) NOT NULL", CanBeNull = false)]
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

		private string _StatusType;

		private string _Feature;
	}
}
