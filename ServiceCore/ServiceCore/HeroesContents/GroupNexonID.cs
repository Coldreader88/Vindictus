using System;
using System.Data.Linq.Mapping;

namespace ServiceCore.HeroesContents
{
	[Table(Name = "dbo.GroupNexonID")]
	public class GroupNexonID
	{
		[Column(Storage = "_NexonID", DbType = "VarChar(32) NOT NULL", CanBeNull = false)]
		public string NexonID
		{
			get
			{
				return this._NexonID;
			}
			set
			{
				if (this._NexonID != value)
				{
					this._NexonID = value;
				}
			}
		}

		[Column(Storage = "_GroupID", DbType = "NVarChar(32) NOT NULL", CanBeNull = false)]
		public string GroupID
		{
			get
			{
				return this._GroupID;
			}
			set
			{
				if (this._GroupID != value)
				{
					this._GroupID = value;
				}
			}
		}

		private string _NexonID;

		private string _GroupID;
	}
}
