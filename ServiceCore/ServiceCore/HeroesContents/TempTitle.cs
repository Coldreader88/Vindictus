using System;
using System.Data.Linq.Mapping;

namespace ServiceCore.HeroesContents
{
	[Table(Name = "dbo.TempTitle")]
	public class TempTitle
	{
		[Column(Storage = "_old_id", DbType = "NVarChar(50) NOT NULL", CanBeNull = false)]
		public string old_id
		{
			get
			{
				return this._old_id;
			}
			set
			{
				if (this._old_id != value)
				{
					this._old_id = value;
				}
			}
		}

		[Column(Storage = "_new_id", DbType = "Int NOT NULL")]
		public int new_id
		{
			get
			{
				return this._new_id;
			}
			set
			{
				if (this._new_id != value)
				{
					this._new_id = value;
				}
			}
		}

		private string _old_id;

		private int _new_id;
	}
}
