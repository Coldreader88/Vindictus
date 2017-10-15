using System;
using System.Data.Linq.Mapping;

namespace ServiceCore.HeroesContents
{
	[Table(Name = "dbo.PetFeedInfo")]
	public class PetFeedInfo
	{
		[Column(Storage = "_FeedType", DbType = "TinyInt NOT NULL")]
		public byte FeedType
		{
			get
			{
				return this._FeedType;
			}
			set
			{
				if (this._FeedType != value)
				{
					this._FeedType = value;
				}
			}
		}

		[Column(Storage = "_ItemClass", DbType = "NVarChar(256) NOT NULL", CanBeNull = false)]
		public string ItemClass
		{
			get
			{
				return this._ItemClass;
			}
			set
			{
				if (this._ItemClass != value)
				{
					this._ItemClass = value;
				}
			}
		}

		private byte _FeedType;

		private string _ItemClass;
	}
}
