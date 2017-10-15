using System;
using System.Data.Linq.Mapping;

namespace ServiceCore.HeroesContents
{
	[Table(Name = "dbo.SkillRankEnum")]
	public class SkillRankEnum
	{
		[Column(Storage = "_Rank_Num", DbType = "Int NOT NULL")]
		public int Rank_Num
		{
			get
			{
				return this._Rank_Num;
			}
			set
			{
				if (this._Rank_Num != value)
				{
					this._Rank_Num = value;
				}
			}
		}

		[Column(Storage = "_Rank_String", DbType = "Text NOT NULL", CanBeNull = false, UpdateCheck = UpdateCheck.Never)]
		public string Rank_String
		{
			get
			{
				return this._Rank_String;
			}
			set
			{
				if (this._Rank_String != value)
				{
					this._Rank_String = value;
				}
			}
		}

		private int _Rank_Num;

		private string _Rank_String;
	}
}
