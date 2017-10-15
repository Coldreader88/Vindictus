using System;
using System.Data.Linq.Mapping;

namespace ServiceCore.HeroesContents
{
	[Table(Name = "dbo.EnhanceInsteadMaterialInfo")]
	public class EnhanceInsteadMaterialInfo
	{
		[Column(Storage = "_ItemClass", DbType = "VarChar(50) NOT NULL", CanBeNull = false)]
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

		[Column(Storage = "_UseCount", DbType = "Int NOT NULL")]
		public int UseCount
		{
			get
			{
				return this._UseCount;
			}
			set
			{
				if (this._UseCount != value)
				{
					this._UseCount = value;
				}
			}
		}

		[Column(Storage = "_Slot", DbType = "TinyInt NOT NULL")]
		public byte Slot
		{
			get
			{
				return this._Slot;
			}
			set
			{
				if (this._Slot != value)
				{
					this._Slot = value;
				}
			}
		}

		[Column(Storage = "_EnhanceMin", DbType = "SmallInt NOT NULL")]
		public short EnhanceMin
		{
			get
			{
				return this._EnhanceMin;
			}
			set
			{
				if (this._EnhanceMin != value)
				{
					this._EnhanceMin = value;
				}
			}
		}

		[Column(Storage = "_EnhanceMax", DbType = "SmallInt NOT NULL")]
		public short EnhanceMax
		{
			get
			{
				return this._EnhanceMax;
			}
			set
			{
				if (this._EnhanceMax != value)
				{
					this._EnhanceMax = value;
				}
			}
		}

		private string _ItemClass;

		private int _UseCount;

		private byte _Slot;

		private short _EnhanceMin;

		private short _EnhanceMax;
	}
}
