using System;
using System.Data.Linq.Mapping;

namespace ServiceCore.HeroesContents
{
	[Table(Name = "dbo.UserToStoryLine")]
	public class UserToStoryLine
	{
		[Column(Storage = "_ID", DbType = "NChar(10) NOT NULL", CanBeNull = false)]
		public string ID
		{
			get
			{
				return this._ID;
			}
			set
			{
				if (this._ID != value)
				{
					this._ID = value;
				}
			}
		}

		[Column(Storage = "_UserAchievement", DbType = "NVarChar(50) NOT NULL", CanBeNull = false)]
		public string UserAchievement
		{
			get
			{
				return this._UserAchievement;
			}
			set
			{
				if (this._UserAchievement != value)
				{
					this._UserAchievement = value;
				}
			}
		}

		[Column(Storage = "_StoryLine", DbType = "NVarChar(70) NOT NULL", CanBeNull = false)]
		public string StoryLine
		{
			get
			{
				return this._StoryLine;
			}
			set
			{
				if (this._StoryLine != value)
				{
					this._StoryLine = value;
				}
			}
		}

		private string _ID;

		private string _UserAchievement;

		private string _StoryLine;
	}
}
