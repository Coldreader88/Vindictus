using System;
using System.Data.Linq.Mapping;

namespace ServiceCore.HeroesContents
{
	[Table(Name = "dbo.StoryTokenInfo")]
	public class StoryTokenInfo
	{
		[Column(Storage = "_token_name", DbType = "NVarChar(50) NOT NULL", CanBeNull = false)]
		public string token_name
		{
			get
			{
				return this._token_name;
			}
			set
			{
				if (this._token_name != value)
				{
					this._token_name = value;
				}
			}
		}

		[Column(Storage = "_token_id", DbType = "Int NOT NULL")]
		public int token_id
		{
			get
			{
				return this._token_id;
			}
			set
			{
				if (this._token_id != value)
				{
					this._token_id = value;
				}
			}
		}

		private string _token_name;

		private int _token_id;
	}
}
