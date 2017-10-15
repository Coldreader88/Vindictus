using System;
using System.Data.Linq.Mapping;

namespace ServiceCore.HeroesContents
{
	[Table(Name = "dbo.JumpingPresetSkill")]
	public class JumpingPresetSkill
	{
		[Column(Storage = "_CharacterClass", DbType = "SmallInt NOT NULL")]
		public short CharacterClass
		{
			get
			{
				return this._CharacterClass;
			}
			set
			{
				if (this._CharacterClass != value)
				{
					this._CharacterClass = value;
				}
			}
		}

		[Column(Storage = "_SkillType", DbType = "NVarChar(128) NOT NULL", CanBeNull = false)]
		public string SkillType
		{
			get
			{
				return this._SkillType;
			}
			set
			{
				if (this._SkillType != value)
				{
					this._SkillType = value;
				}
			}
		}

		[Column(Storage = "_Rank", DbType = "Int NOT NULL")]
		public int Rank
		{
			get
			{
				return this._Rank;
			}
			set
			{
				if (this._Rank != value)
				{
					this._Rank = value;
				}
			}
		}

		[Column(Storage = "_Feature", DbType = "VarChar(50)")]
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

		private short _CharacterClass;

		private string _SkillType;

		private int _Rank;

		private string _Feature;
	}
}
