using System;
using System.Data.Linq.Mapping;

namespace ServiceCore.HeroesContents
{
	[Table(Name = "dbo.EntityAttribute")]
	public class EntityAttribute
	{
		[Column(Storage = "_EntityID", DbType = "Int NOT NULL")]
		public int EntityID
		{
			get
			{
				return this._EntityID;
			}
			set
			{
				if (this._EntityID != value)
				{
					this._EntityID = value;
				}
			}
		}

		[Column(Storage = "_Name", DbType = "VarChar(32) NOT NULL", CanBeNull = false)]
		public string Name
		{
			get
			{
				return this._Name;
			}
			set
			{
				if (this._Name != value)
				{
					this._Name = value;
				}
			}
		}

		[Column(Storage = "_Value", DbType = "VarChar(2048) NOT NULL", CanBeNull = false)]
		public string Value
		{
			get
			{
				return this._Value;
			}
			set
			{
				if (this._Value != value)
				{
					this._Value = value;
				}
			}
		}

		private int _EntityID;

		private string _Name;

		private string _Value;
	}
}
