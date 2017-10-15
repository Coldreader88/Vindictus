using System;
using System.Data.Linq.Mapping;

namespace ServiceCore.HeroesContents
{
	[Table(Name = "dbo.ItemClassExtraInfo")]
	public class ItemClassExtraInfo
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

		[Column(Storage = "_Feature", DbType = "NVarChar(256)")]
		public string Feature
		{
			get
			{
				return this._Feature;
			}
			set
			{
				if (this._Feature != value)
				{
					this._Feature = value;
				}
			}
		}

		[Column(Storage = "_ExtraData", DbType = "NVarChar(256)")]
		public string ExtraData
		{
			get
			{
				return this._ExtraData;
			}
			set
			{
				if (this._ExtraData != value)
				{
					this._ExtraData = value;
				}
			}
		}

		private string _ItemClass;

		private string _Feature;

		private string _ExtraData;
	}
}
