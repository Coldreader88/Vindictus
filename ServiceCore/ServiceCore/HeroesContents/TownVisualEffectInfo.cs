using System;
using System.Data.Linq.Mapping;

namespace ServiceCore.HeroesContents
{
	[Table(Name = "dbo.TownVisualEffectInfo")]
	public class TownVisualEffectInfo
	{
		[Column(Storage = "_ID", DbType = "Int NOT NULL")]
		public int ID
		{
			get
			{
				return this._ID;
			}
			set
			{
				if (this._ID != value)
				{
					this._ID = value;
				}
			}
		}

		[Column(Storage = "_Effect", DbType = "Text NOT NULL", CanBeNull = false, UpdateCheck = UpdateCheck.Never)]
		public string Effect
		{
			get
			{
				return this._Effect;
			}
			set
			{
				if (this._Effect != value)
				{
					this._Effect = value;
				}
			}
		}

		[Column(Storage = "_Duration", DbType = "Int NOT NULL")]
		public int Duration
		{
			get
			{
				return this._Duration;
			}
			set
			{
				if (this._Duration != value)
				{
					this._Duration = value;
				}
			}
		}

		private int _ID;

		private string _Effect;

		private int _Duration;
	}
}
