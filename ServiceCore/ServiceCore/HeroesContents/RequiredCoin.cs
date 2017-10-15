using System;
using System.Data.Linq.Mapping;

namespace ServiceCore.HeroesContents
{
	[Table(Name = "dbo.RequiredCoin")]
	public class RequiredCoin
	{
		[Column(Storage = "_PlayCount", DbType = "Int NOT NULL")]
		public int PlayCount
		{
			get
			{
				return this._PlayCount;
			}
			set
			{
				if (this._PlayCount != value)
				{
					this._PlayCount = value;
				}
			}
		}

		[Column(Storage = "_RequiredCoinCount", DbType = "Int NOT NULL")]
		public int RequiredCoinCount
		{
			get
			{
				return this._RequiredCoinCount;
			}
			set
			{
				if (this._RequiredCoinCount != value)
				{
					this._RequiredCoinCount = value;
				}
			}
		}

		private int _PlayCount;

		private int _RequiredCoinCount;
	}
}
