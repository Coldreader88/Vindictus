using System;
using System.Data.Linq.Mapping;

namespace ServiceCore.HeroesContents
{
	[Table(Name = "dbo.SynthesisSkillBonus")]
	public class SynthesisSkillBonus
	{
		[Column(Storage = "_ID", DbType = "Int NOT NULL")]
		public int ID
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

		[Column(Storage = "_Grade", DbType = "NVarChar(50) NOT NULL", CanBeNull = false)]
		public string Grade
		{
			get
			{
				return this._Grade;
			}
			set
			{
				if (this._Grade != value)
				{
					this._Grade = value;
				}
			}
		}

		[Column(Storage = "_ClassRestriction", DbType = "Int NOT NULL")]
		public int ClassRestriction
		{
			get
			{
				return this._ClassRestriction;
			}
			set
			{
				if (this._ClassRestriction != value)
				{
					this._ClassRestriction = value;
				}
			}
		}

		[Column(Storage = "_SkillID", DbType = "NVarChar(128) NOT NULL", CanBeNull = false)]
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

		[Column(Storage = "_Dice", DbType = "Int NOT NULL")]
		public int Dice
		{
			get
			{
				return this._Dice;
			}
			set
			{
				if (this._Dice != value)
				{
					this._Dice = value;
				}
			}
		}

		[Column(Storage = "_Type", DbType = "NVarChar(50)")]
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

		[Column(Storage = "_Value", DbType = "Int NOT NULL")]
		public int Value
		{
			get
			{
				return this._Value;
			}
			set
			{
				if (this._Value != value)
				{
					this._Value = value;
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

		private int _ID;

		private string _Grade;

		private int _ClassRestriction;

		private string _SkillID;

		private int _Dice;

		private string _Type;

		private int _Value;

		private string _Feature;
	}
}
