using System;
using System.Data.Linq.Mapping;

namespace ServiceCore.HeroesContents
{
	[Table(Name = "dbo.EquipPartInfo")]
	public class EquipPartInfo
	{
		[Column(Storage = "_CostumeCategory", DbType = "VarChar(20)")]
		public string CostumeCategory
		{
			get
			{
				return this._CostumeCategory;
			}
			set
			{
				if (this._CostumeCategory != value)
				{
					this._CostumeCategory = value;
				}
			}
		}

		[Column(Storage = "_CostumePartNum", DbType = "Int")]
		public int? CostumePartNum
		{
			get
			{
				return this._CostumePartNum;
			}
			set
			{
				if (this._CostumePartNum != value)
				{
					this._CostumePartNum = value;
				}
			}
		}

		[Column(Storage = "_EquipPartName", DbType = "VarChar(20) NOT NULL", CanBeNull = false)]
		public string EquipPartName
		{
			get
			{
				return this._EquipPartName;
			}
			set
			{
				if (this._EquipPartName != value)
				{
					this._EquipPartName = value;
				}
			}
		}

		[Column(Storage = "_EquipPartNum", DbType = "Int NOT NULL")]
		public int EquipPartNum
		{
			get
			{
				return this._EquipPartNum;
			}
			set
			{
				if (this._EquipPartNum != value)
				{
					this._EquipPartNum = value;
				}
			}
		}

		[Column(Storage = "_ItemCategory", DbType = "VarChar(20) NOT NULL", CanBeNull = false)]
		public string ItemCategory
		{
			get
			{
				return this._ItemCategory;
			}
			set
			{
				if (this._ItemCategory != value)
				{
					this._ItemCategory = value;
				}
			}
		}

		[Column(Storage = "_IsConsumable", DbType = "Bit NOT NULL")]
		public bool IsConsumable
		{
			get
			{
				return this._IsConsumable;
			}
			set
			{
				if (this._IsConsumable != value)
				{
					this._IsConsumable = value;
				}
			}
		}

		[Column(Storage = "_IsExpandedSlot", DbType = "Bit NOT NULL")]
		public bool IsExpandedSlot
		{
			get
			{
				return this._IsExpandedSlot;
			}
			set
			{
				if (this._IsExpandedSlot != value)
				{
					this._IsExpandedSlot = value;
				}
			}
		}

		private string _CostumeCategory;

		private int? _CostumePartNum;

		private string _EquipPartName;

		private int _EquipPartNum;

		private string _ItemCategory;

		private bool _IsConsumable;

		private bool _IsExpandedSlot;
	}
}
