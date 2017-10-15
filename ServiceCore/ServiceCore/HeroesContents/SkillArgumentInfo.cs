using System;
using System.Data.Linq.Mapping;

namespace ServiceCore.HeroesContents
{
	[Table(Name = "dbo.SkillArgumentInfo")]
	public class SkillArgumentInfo
	{
		[Column(Storage = "_RowID", DbType = "Int")]
		public int? RowID
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

		[Column(Storage = "_SkillId", DbType = "NVarChar(50)")]
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

		[Column(Storage = "_Rank", DbType = "Int")]
		public int? Rank
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

		[Column(Storage = "_KeyWord", DbType = "NVarChar(50)")]
		public string KeyWord
		{
			get
			{
				return this._KeyWord;
			}
			set
			{
				if (this._KeyWord != value)
				{
					this._KeyWord = value;
				}
			}
		}

		[Column(Storage = "_Value", DbType = "Int")]
		public int? Value
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

		private int? _RowID;

		private string _SkillId;

		private int? _Rank;

		private string _KeyWord;

		private int? _Value;
	}
}
