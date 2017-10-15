using System;
using System.Data.Linq.Mapping;

namespace ServiceCore.HeroesContents
{
	[Table(Name = "dbo.SkillConstraintInfo")]
	public class SkillConstraintInfo
	{
		[Column(Storage = "_RowID", DbType = "Int NOT NULL")]
		public int RowID
		{
			get
			{
				return this._RowID;
			}
			set
			{
				if (this._RowID != value)
				{
					this._RowID = value;
				}
			}
		}

		[Column(Storage = "_SkillId", DbType = "NVarChar(50) NOT NULL", CanBeNull = false)]
		public string SkillId
		{
			get
			{
				return this._SkillId;
			}
			set
			{
				if (this._SkillId != value)
				{
					this._SkillId = value;
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

		[Column(Storage = "_RequiredSkillId", DbType = "NVarChar(50) NOT NULL", CanBeNull = false)]
		public string RequiredSkillId
		{
			get
			{
				return this._RequiredSkillId;
			}
			set
			{
				if (this._RequiredSkillId != value)
				{
					this._RequiredSkillId = value;
				}
			}
		}

		[Column(Storage = "_RequiredRank", DbType = "Int NOT NULL")]
		public int RequiredRank
		{
			get
			{
				return this._RequiredRank;
			}
			set
			{
				if (this._RequiredRank != value)
				{
					this._RequiredRank = value;
				}
			}
		}

		private int _RowID;

		private string _SkillId;

		private int _Rank;

		private string _RequiredSkillId;

		private int _RequiredRank;
	}
}
