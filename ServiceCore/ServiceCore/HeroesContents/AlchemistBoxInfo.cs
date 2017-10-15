using System;
using System.Data.Linq.Mapping;

namespace ServiceCore.HeroesContents
{
	[Table(Name = "dbo.AlchemistBoxInfo")]
	public class AlchemistBoxInfo
	{
		[Column(Storage = "_BoxItemClass", DbType = "NVarChar(50) NOT NULL", CanBeNull = false)]
		public string BoxItemClass
		{
			get
			{
				return this._BoxItemClass;
			}
			set
			{
				if (this._BoxItemClass != value)
				{
					this._BoxItemClass = value;
				}
			}
		}

		[Column(Storage = "_Attribute", DbType = "NVarChar(50) NOT NULL", CanBeNull = false)]
		public string Attribute
		{
			get
			{
				return this._Attribute;
			}
			set
			{
				if (this._Attribute != value)
				{
					this._Attribute = value;
				}
			}
		}

		[Column(Storage = "_Slot1", DbType = "NVarChar(50)")]
		public string Slot1
		{
			get
			{
				return this._Slot1;
			}
			set
			{
				if (this._Slot1 != value)
				{
					this._Slot1 = value;
				}
			}
		}

		[Column(Storage = "_Slot2", DbType = "NVarChar(50)")]
		public string Slot2
		{
			get
			{
				return this._Slot2;
			}
			set
			{
				if (this._Slot2 != value)
				{
					this._Slot2 = value;
				}
			}
		}

		[Column(Storage = "_Slot3", DbType = "NVarChar(50)")]
		public string Slot3
		{
			get
			{
				return this._Slot3;
			}
			set
			{
				if (this._Slot3 != value)
				{
					this._Slot3 = value;
				}
			}
		}

		[Column(Storage = "_Slot4", DbType = "NVarChar(50)")]
		public string Slot4
		{
			get
			{
				return this._Slot4;
			}
			set
			{
				if (this._Slot4 != value)
				{
					this._Slot4 = value;
				}
			}
		}

		[Column(Storage = "_Slot5", DbType = "NVarChar(50)")]
		public string Slot5
		{
			get
			{
				return this._Slot5;
			}
			set
			{
				if (this._Slot5 != value)
				{
					this._Slot5 = value;
				}
			}
		}

		[Column(Storage = "_Slot6", DbType = "NVarChar(50)")]
		public string Slot6
		{
			get
			{
				return this._Slot6;
			}
			set
			{
				if (this._Slot6 != value)
				{
					this._Slot6 = value;
				}
			}
		}

		[Column(Storage = "_Slot7", DbType = "NVarChar(50)")]
		public string Slot7
		{
			get
			{
				return this._Slot7;
			}
			set
			{
				if (this._Slot7 != value)
				{
					this._Slot7 = value;
				}
			}
		}

		[Column(Storage = "_Slot8", DbType = "NVarChar(50)")]
		public string Slot8
		{
			get
			{
				return this._Slot8;
			}
			set
			{
				if (this._Slot8 != value)
				{
					this._Slot8 = value;
				}
			}
		}

		[Column(Storage = "_Slot9", DbType = "NVarChar(50)")]
		public string Slot9
		{
			get
			{
				return this._Slot9;
			}
			set
			{
				if (this._Slot9 != value)
				{
					this._Slot9 = value;
				}
			}
		}

		[Column(Storage = "_Slot10", DbType = "NVarChar(50)")]
		public string Slot10
		{
			get
			{
				return this._Slot10;
			}
			set
			{
				if (this._Slot10 != value)
				{
					this._Slot10 = value;
				}
			}
		}

		private string _BoxItemClass;

		private string _Attribute;

		private string _Slot1;

		private string _Slot2;

		private string _Slot3;

		private string _Slot4;

		private string _Slot5;

		private string _Slot6;

		private string _Slot7;

		private string _Slot8;

		private string _Slot9;

		private string _Slot10;
	}
}
