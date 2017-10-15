using System;
using System.Data.Linq.Mapping;

namespace ServiceCore.HeroesContents
{
	[Table(Name = "dbo.SetItemInfo")]
	public class SetItemInfo
	{
		[Column(Storage = "_ItemClass", DbType = "NVarChar(50) NOT NULL", CanBeNull = false)]
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

		[Column(Storage = "_BaseItemClass", DbType = "NVarChar(50) NOT NULL", CanBeNull = false)]
		public string BaseItemClass
		{
			get
			{
				return this._BaseItemClass;
			}
			set
			{
				if (this._BaseItemClass != value)
				{
					this._BaseItemClass = value;
				}
			}
		}

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

		private string _ItemClass;

		private string _BaseItemClass;

		private string _SetID;
	}
}
