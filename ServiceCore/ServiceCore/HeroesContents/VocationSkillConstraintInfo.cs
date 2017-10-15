using System;
using System.Data.Linq.Mapping;

namespace ServiceCore.HeroesContents
{
	[Table(Name = "dbo.VocationSkillConstraintInfo")]
	public class VocationSkillConstraintInfo
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

		[Column(Storage = "_RequiredSkillID", DbType = "NVarChar(50) NOT NULL", CanBeNull = false)]
		public string RequiredSkillID
		{
			get
			{
				return this._RequiredSkillID;
			}
			set
			{
				if (this._RequiredSkillID != value)
				{
					this._RequiredSkillID = value;
				}
			}
		}

		private string _SkillID;

		private string _RequiredSkillID;
	}
}
