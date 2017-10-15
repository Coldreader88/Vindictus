using System;
using System.Data.Linq.Mapping;

namespace ServiceCore.HeroesContents
{
	[Table(Name = "dbo.[ForbiddenWords_zh-CN]")]
	public class ForbiddenWords_zh_CN
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
