using System;
using System.Data.Linq.Mapping;

namespace ServiceCore.HeroesContents
{
	[Table(Name = "dbo.RankCategory")]
	public class RankCategory
	{
		[Column(Storage = "_CategoryID", DbType = "NVarChar(50) NOT NULL", CanBeNull = false)]
		public string CategoryID
		{
			get
			{
				return this._CategoryID;
			}
			set
			{
				if (this._CategoryID != value)
				{
					this._CategoryID = value;
				}
			}
		}

		private string _CategoryID;
	}
}
