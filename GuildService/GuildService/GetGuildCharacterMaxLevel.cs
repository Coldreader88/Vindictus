using System;
using System.Data.Linq.Mapping;

namespace GuildService
{
	public class GetGuildCharacterMaxLevel
	{
		[Column(Storage = "_MaxLevel", DbType = "Int NOT NULL")]
		public int MaxLevel
		{
			get
			{
				return this._MaxLevel;
			}
			set
			{
				if (this._MaxLevel != value)
				{
					this._MaxLevel = value;
				}
			}
		}

		private int _MaxLevel;
	}
}
