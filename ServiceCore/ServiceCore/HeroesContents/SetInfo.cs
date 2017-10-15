using System;
using System.Data.Linq.Mapping;

namespace ServiceCore.HeroesContents
{
	[Table(Name = "dbo.SetInfo")]
	public class SetInfo
	{
		[Column(Storage = "_SetID", DbType = "NVarChar(50) NOT NULL", CanBeNull = false)]
		public string SetID
		{
			get
			{
				return this._SetID;
			}
			set
			{
				if (this._SetID != value)
				{
					this._SetID = value;
				}
			}
		}

		private string _SetID;
	}
}
