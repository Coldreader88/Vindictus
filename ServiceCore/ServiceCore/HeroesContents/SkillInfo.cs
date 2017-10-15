using System;
using System.Collections.Generic;
using System.Data.Linq.Mapping;
using Utility;

namespace ServiceCore.HeroesContents
{
	[Table(Name = "dbo.SkillInfo")]
	public class SkillInfo
	{
		public bool IsActiveSkill
		{
			get
			{
				return this._UseType != 0;
			}
		}

		public IEnumerable<int> GetAvailableSkillEnhanceGroup()
		{
			if (this.availableSkillEnhanceGroupCache == null)
			{
				this.availableSkillEnhanceGroupCache = new List<int>();
				if (!string.IsNullOrEmpty(this.SkillEnhanceGroup))
				{
					string[] array = this.SkillEnhanceGroup.Split(new char[]
					{
						';'
					});
					foreach (string s in array)
					{
						int item;
						if (int.TryParse(s, out item))
						{
							this.availableSkillEnhanceGroupCache.Add(item);
						}
						else
						{
							Log<SkillInfo>.Logger.ErrorFormat("Cannot parse SkillEnhanceGroup in SkillInfo Table [ {0} ]", this.SkillEnhanceGroup);
						}
					}
				}
			}
			return this.availableSkillEnhanceGroupCache;
		}

		public bool IsAvailableSkillEnhaceGroup(int groupKey)
		{
			foreach (int num in this.GetAvailableSkillEnhanceGroup())
			{
				if (num == groupKey)
				{
					return true;
				}
			}
			return false;
		}

		[Column(Storage = "_RowID", DbType = "Int NOT NULL")]
		public int RowID
		{
			get
			{
				return this._RowID;
			}
			set
			{
				if (this._RowID != value)
				{
					this._RowID = value;
				}
			}
		}

		[Column(Storage = "_SkillID", DbType = "NVarChar(50) NOT NULL", CanBeNull = false)]
		public string SkillID
		{
			get
			{
				return this._SkillID;
			}
			set
			{
				if (this._SkillID != value)
				{
					this._SkillID = value;
				}
			}
		}

		[Column(Storage = "_IconType", DbType = "NVarChar(50) NOT NULL", CanBeNull = false)]
		public string IconType
		{
			get
			{
				return this._IconType;
			}
			set
			{
				if (this._IconType != value)
				{
					this._IconType = value;
				}
			}
		}

		[Column(Storage = "_Category", DbType = "NVarChar(50) NOT NULL", CanBeNull = false)]
		public string Category
		{
			get
			{
				return this._Category;
			}
			set
			{
				if (this._Category != value)
				{
					this._Category = value;
				}
			}
		}

		[Column(Storage = "_SkillName", DbType = "NVarChar(70) NOT NULL", CanBeNull = false)]
		public string SkillName
		{
			get
			{
				return this._SkillName;
			}
			set
			{
				if (this._SkillName != value)
				{
					this._SkillName = value;
				}
			}
		}

		[Column(Storage = "_ClassRestriction", DbType = "Int NOT NULL")]
		public int ClassRestriction
		{
			get
			{
				return this._ClassRestriction;
			}
			set
			{
				if (this._ClassRestriction != value)
				{
					this._ClassRestriction = value;
				}
			}
		}

		[Column(Storage = "_VideoNameFlag", DbType = "TinyInt NOT NULL")]
		public byte VideoNameFlag
		{
			get
			{
				return this._VideoNameFlag;
			}
			set
			{
				if (this._VideoNameFlag != value)
				{
					this._VideoNameFlag = value;
				}
			}
		}

		[Column(Storage = "_Price", DbType = "Int NOT NULL")]
		public int Price
		{
			get
			{
				return this._Price;
			}
			set
			{
				if (this._Price != value)
				{
					this._Price = value;
				}
			}
		}

		[Column(Storage = "_IsFirstWeapon", DbType = "TinyInt NOT NULL")]
		public byte IsFirstWeapon
		{
			get
			{
				return this._IsFirstWeapon;
			}
			set
			{
				if (this._IsFirstWeapon != value)
				{
					this._IsFirstWeapon = value;
				}
			}
		}

		[Column(Storage = "_IsSecondWeapon", DbType = "TinyInt NOT NULL")]
		public byte IsSecondWeapon
		{
			get
			{
				return this._IsSecondWeapon;
			}
			set
			{
				if (this._IsSecondWeapon != value)
				{
					this._IsSecondWeapon = value;
				}
			}
		}

		[Column(Storage = "_UseType", DbType = "TinyInt NOT NULL")]
		public byte UseType
		{
			get
			{
				return this._UseType;
			}
			set
			{
				if (this._UseType != value)
				{
					this._UseType = value;
				}
			}
		}

		[Column(Storage = "_SkillEnhanceGroup", DbType = "NVarChar(64)")]
		public string SkillEnhanceGroup
		{
			get
			{
				return this._SkillEnhanceGroup;
			}
			set
			{
				if (this._SkillEnhanceGroup != value)
				{
					this._SkillEnhanceGroup = value;
				}
			}
		}

		[Column(Storage = "_SkillDescription", DbType = "NVarChar(70) NOT NULL", CanBeNull = false)]
		public string SkillDescription
		{
			get
			{
				return this._SkillDescription;
			}
			set
			{
				if (this._SkillDescription != value)
				{
					this._SkillDescription = value;
				}
			}
		}

		private List<int> availableSkillEnhanceGroupCache;

		private int _RowID;

		private string _SkillID;

		private string _IconType;

		private string _Category;

		private string _SkillName;

		private int _ClassRestriction;

		private byte _VideoNameFlag;

		private int _Price;

		private byte _IsFirstWeapon;

		private byte _IsSecondWeapon;

		private byte _UseType;

		private string _SkillEnhanceGroup;

		private string _SkillDescription;
	}
}
