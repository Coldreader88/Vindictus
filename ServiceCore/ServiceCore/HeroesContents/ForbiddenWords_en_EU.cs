using System;
using System.Data.Linq.Mapping;

namespace ServiceCore.HeroesContents
{
	[Table(Name = "dbo.[ForbiddenWords_en-EU]")]
	public class ForbiddenWords_en_EU
	{
		[Column(Storage = "_ForbiddenWord", DbType = "Text", UpdateCheck = UpdateCheck.Never)]
		public string ForbiddenWord
		{
			get
			{
				return this._ForbiddenWord;
			}
			set
			{
				if (this._ForbiddenWord != value)
				{
					this._ForbiddenWord = value;
				}
			}
		}

		private string _ForbiddenWord;
	}
}
