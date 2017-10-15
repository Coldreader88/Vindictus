using System;
using System.Data.Linq.Mapping;

namespace ServiceCore.HeroesContents
{
	[Table(Name = "dbo.GuildForbiddenWords")]
	public class GuildForbiddenWords
	{
		[Column(Storage = "_ForbiddenName", DbType = "Text NOT NULL", CanBeNull = false, UpdateCheck = UpdateCheck.Never)]
		public string ForbiddenName
		{
			get
			{
				return this._ForbiddenName;
			}
			set
			{
				if (this._ForbiddenName != value)
				{
					this._ForbiddenName = value;
				}
			}
		}

		[Column(Storage = "_Language", DbType = "NVarChar(50) NOT NULL", CanBeNull = false)]
		public string Language
		{
			get
			{
				return this._Language;
			}
			set
			{
				if (this._Language != value)
				{
					this._Language = value;
				}
			}
		}

		private string _ForbiddenName;

		private string _Language;
	}
}
