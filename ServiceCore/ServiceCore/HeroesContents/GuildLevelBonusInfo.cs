using System;
using System.Data.Linq.Mapping;

namespace ServiceCore.HeroesContents
{
	[Table(Name = "dbo.GuildLevelBonusInfo")]
	public class GuildLevelBonusInfo
	{
		[Column(Name = "[Level]", Storage = "_Level", DbType = "Int NOT NULL")]
		public int Level
		{
			get
			{
				return this._Level;
			}
			set
			{
				if (this._Level != value)
				{
					this._Level = value;
				}
			}
		}

		[Column(Storage = "_LevelBonus", DbType = "NVarChar(50) NOT NULL", CanBeNull = false)]
		public string LevelBonus
		{
			get
			{
				return this._LevelBonus;
			}
			set
			{
				if (this._LevelBonus != value)
				{
					this._LevelBonus = value;
				}
			}
		}

		[Column(Storage = "_Arg", DbType = "Int NOT NULL")]
		public int Arg
		{
			get
			{
				return this._Arg;
			}
			set
			{
				if (this._Arg != value)
				{
					this._Arg = value;
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

		private int _Level;

		private string _LevelBonus;

		private int _Arg;

		private string _Feature;
	}
}
