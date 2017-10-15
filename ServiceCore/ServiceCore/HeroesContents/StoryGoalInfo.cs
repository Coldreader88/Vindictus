using System;
using System.Data.Linq.Mapping;

namespace ServiceCore.HeroesContents
{
	[Table(Name = "dbo.StoryGoalInfo")]
	public class StoryGoalInfo
	{
		[Column(Storage = "_StoryLine", DbType = "VarChar(128) NOT NULL", CanBeNull = false)]
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

		[Column(Storage = "_Phase", DbType = "VarChar(50) NOT NULL", CanBeNull = false)]
		public string Phase
		{
			get
			{
				return this._Phase;
			}
			set
			{
				if (this._Phase != value)
				{
					this._Phase = value;
				}
			}
		}

		[Column(Storage = "_Character", DbType = "VarChar(50) NOT NULL", CanBeNull = false)]
		public string Character
		{
			get
			{
				return this._Character;
			}
			set
			{
				if (this._Character != value)
				{
					this._Character = value;
				}
			}
		}

		[Column(Name = "[Group]", Storage = "_Group", DbType = "Int NOT NULL")]
		public int Group
		{
			get
			{
				return this._Group;
			}
			set
			{
				if (this._Group != value)
				{
					this._Group = value;
				}
			}
		}

		[Column(Storage = "_Type", DbType = "VarChar(50) NOT NULL", CanBeNull = false)]
		public string Type
		{
			get
			{
				return this._Type;
			}
			set
			{
				if (this._Type != value)
				{
					this._Type = value;
				}
			}
		}

		[Column(Storage = "_Command", DbType = "VarChar(50) NOT NULL", CanBeNull = false)]
		public string Command
		{
			get
			{
				return this._Command;
			}
			set
			{
				if (this._Command != value)
				{
					this._Command = value;
				}
			}
		}

		[Column(Storage = "_Arg", DbType = "VarChar(1024) NOT NULL", CanBeNull = false)]
		public string Arg
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

		[Column(Name = "[Desc]", Storage = "_Desc", DbType = "VarChar(256) NOT NULL", CanBeNull = false)]
		public string Desc
		{
			get
			{
				return this._Desc;
			}
			set
			{
				if (this._Desc != value)
				{
					this._Desc = value;
				}
			}
		}

		private string _StoryLine;

		private string _Phase;

		private string _Character;

		private int _Group;

		private string _Type;

		private string _Command;

		private string _Arg;

		private string _Desc;
	}
}
