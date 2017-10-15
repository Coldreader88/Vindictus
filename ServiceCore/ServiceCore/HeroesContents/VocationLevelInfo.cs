using System;
using System.Data.Linq.Mapping;

namespace ServiceCore.HeroesContents
{
	[Table(Name = "dbo.VocationLevelInfo")]
	public class VocationLevelInfo
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

		[Column(Storage = "_ExpToNextLevel", DbType = "Int NOT NULL")]
		public int ExpToNextLevel
		{
			get
			{
				return this._ExpToNextLevel;
			}
			set
			{
				if (this._ExpToNextLevel != value)
				{
					this._ExpToNextLevel = value;
				}
			}
		}

		[Column(Storage = "_SkillPoint", DbType = "Int NOT NULL")]
		public int SkillPoint
		{
			get
			{
				return this._SkillPoint;
			}
			set
			{
				if (this._SkillPoint != value)
				{
					this._SkillPoint = value;
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

		private int _ExpToNextLevel;

		private int _SkillPoint;

		private string _Feature;
	}
}
