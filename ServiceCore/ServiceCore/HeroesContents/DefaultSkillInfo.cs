using System;
using System.Data.Linq.Mapping;

namespace ServiceCore.HeroesContents
{
	[Table(Name = "dbo.DefaultSkillInfo")]
	public class DefaultSkillInfo
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

		[Column(Storage = "_CharacterClass", DbType = "TinyInt NOT NULL")]
		public byte CharacterClass
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

		[Column(Storage = "_SkillID", DbType = "NVarChar(100) NOT NULL", CanBeNull = false)]
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

		private int _ID;

		private byte _CharacterClass;

		private string _SkillID;

		private int _Rank;
	}
}
