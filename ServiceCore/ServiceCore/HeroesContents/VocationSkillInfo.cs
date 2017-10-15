using System;
using System.Data.Linq.Mapping;

namespace ServiceCore.HeroesContents
{
	[Table(Name = "dbo.VocationSkillInfo")]
	public class VocationSkillInfo
	{
		[Column(Storage = "_SkillID", DbType = "NVarChar(50) NOT NULL", CanBeNull = false)]
		public string SkillID
		{
			get
			{
				return this._SkillID;
			}
			set
			{
				if (this._SkillID != value)
				{
					this._SkillID = value;
				}
			}
		}

		[Column(Storage = "_Server", DbType = "NVarChar(50) NOT NULL", CanBeNull = false)]
		public string Server
		{
			get
			{
				return this._Server;
			}
			set
			{
				if (this._Server != value)
				{
					this._Server = value;
				}
			}
		}

		[Column(Storage = "_Character", DbType = "NVarChar(50) NOT NULL", CanBeNull = false)]
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

		[Column(Storage = "_VocationClass", DbType = "NVarChar(50) NOT NULL", CanBeNull = false)]
		public string VocationClass
		{
			get
			{
				return this._VocationClass;
			}
			set
			{
				if (this._VocationClass != value)
				{
					this._VocationClass = value;
				}
			}
		}

		[Column(Storage = "_MaxRank", DbType = "Int NOT NULL")]
		public int MaxRank
		{
			get
			{
				return this._MaxRank;
			}
			set
			{
				if (this._MaxRank != value)
				{
					this._MaxRank = value;
				}
			}
		}

		[Column(Storage = "_Tab", DbType = "Int NOT NULL")]
		public int Tab
		{
			get
			{
				return this._Tab;
			}
			set
			{
				if (this._Tab != value)
				{
					this._Tab = value;
				}
			}
		}

		[Column(Storage = "_XPos", DbType = "Int NOT NULL")]
		public int XPos
		{
			get
			{
				return this._XPos;
			}
			set
			{
				if (this._XPos != value)
				{
					this._XPos = value;
				}
			}
		}

		[Column(Storage = "_YPos", DbType = "Int NOT NULL")]
		public int YPos
		{
			get
			{
				return this._YPos;
			}
			set
			{
				if (this._YPos != value)
				{
					this._YPos = value;
				}
			}
		}

		[Column(Name = "[Constraint]", Storage = "_Constraint", DbType = "Int NOT NULL")]
		public int Constraint
		{
			get
			{
				return this._Constraint;
			}
			set
			{
				if (this._Constraint != value)
				{
					this._Constraint = value;
				}
			}
		}

		[Column(Storage = "_Icon", DbType = "NVarChar(50) NOT NULL", CanBeNull = false)]
		public string Icon
		{
			get
			{
				return this._Icon;
			}
			set
			{
				if (this._Icon != value)
				{
					this._Icon = value;
				}
			}
		}

		[Column(Storage = "_Name", DbType = "NVarChar(100) NOT NULL", CanBeNull = false)]
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

		[Column(Name = "[Desc]", Storage = "_Desc", DbType = "NVarChar(100) NOT NULL", CanBeNull = false)]
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

		private string _SkillID;

		private string _Server;

		private string _Character;

		private string _VocationClass;

		private int _MaxRank;

		private int _Tab;

		private int _XPos;

		private int _YPos;

		private int _Constraint;

		private string _Icon;

		private string _Name;

		private string _Desc;

		private string _Feature;
	}
}
