using System;
using System.Data.Linq.Mapping;

namespace ServiceCore.HeroesContents
{
	[Table(Name = "dbo.LevelUpExpInfo")]
	public class LevelUpExpInfo
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

		[Column(Storage = "_RequiredExp", DbType = "Int NOT NULL")]
		public int RequiredExp
		{
			get
			{
				return this._RequiredExp;
			}
			set
			{
				if (this._RequiredExp != value)
				{
					this._RequiredExp = value;
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

		private int _RequiredExp;

		private string _Feature;
	}
}
