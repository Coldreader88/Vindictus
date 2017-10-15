using System;
using System.Data.Linq.Mapping;

namespace ServiceCore.HeroesContents
{
	[Table(Name = "dbo.CaptchaWordInfo")]
	public class CaptchaWordInfo
	{
		[Column(Storage = "_Word", DbType = "NVarChar(12)")]
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
