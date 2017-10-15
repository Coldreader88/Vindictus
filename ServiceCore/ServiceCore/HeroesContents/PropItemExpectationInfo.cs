using System;
using System.Data.Linq.Mapping;

namespace ServiceCore.HeroesContents
{
	[Table(Name = "dbo.PropItemExpectationInfo")]
	public class PropItemExpectationInfo
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

		[Column(Storage = "_DropTableName", DbType = "NVarChar(50) NOT NULL", CanBeNull = false)]
		public string DropTableName
		{
			get
			{
				return this._DropTableName;
			}
			set
			{
				if (this._DropTableName != value)
				{
					this._DropTableName = value;
				}
			}
		}

		[Column(Storage = "_MaterialGroup", DbType = "NVarChar(50)")]
		public string MaterialGroup
		{
			get
			{
				return this._MaterialGroup;
			}
			set
			{
				if (this._MaterialGroup != value)
				{
					this._MaterialGroup = value;
				}
			}
		}

		[Column(Storage = "_DropExpectationPerSector", DbType = "Float NOT NULL")]
		public double DropExpectationPerSector
		{
			get
			{
				return this._DropExpectationPerSector;
			}
			set
			{
				if (this._DropExpectationPerSector != value)
				{
					this._DropExpectationPerSector = value;
				}
			}
		}

		private int _ID;

		private string _DropTableName;

		private string _MaterialGroup;

		private double _DropExpectationPerSector;
	}
}
