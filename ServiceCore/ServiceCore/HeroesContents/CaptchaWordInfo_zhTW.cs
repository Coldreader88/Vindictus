using System;
using System.Data.Linq.Mapping;

namespace ServiceCore.HeroesContents
{
	[Table(Name = "dbo.CaptchaWordInfo_zhTW")]
	public class CaptchaWordInfo_zhTW
	{
		[Column(Storage = "_Word", DbType = "Text", UpdateCheck = UpdateCheck.Never)]
		public string Word
		{
			get
			{
				return this._Word;
			}
			set
			{
				if (this._Word != value)
				{
					this._Word = value;
				}
			}
		}

		private string _Word;
	}
}
