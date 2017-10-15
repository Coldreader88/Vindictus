using System;
using System.Data.Linq.Mapping;

namespace ServiceCore.HeroesContents
{
	[Table(Name = "dbo.Sectors")]
	public class Sectors
	{
		[Column(Storage = "_QuestID", DbType = "NVarChar(64) NOT NULL", CanBeNull = false)]
		public string QuestID
		{
			get
			{
				return this._QuestID;
			}
			set
			{
				if (this._QuestID != value)
				{
					this._QuestID = value;
				}
			}
		}

		[Column(Storage = "_Sequence", DbType = "Int NOT NULL")]
		public int Sequence
		{
			get
			{
				return this._Sequence;
			}
			set
			{
				if (this._Sequence != value)
				{
					this._Sequence = value;
				}
			}
		}

		[Column(Storage = "_SectorID", DbType = "Int NOT NULL")]
		public int SectorID
		{
			get
			{
				return this._SectorID;
			}
			set
			{
				if (this._SectorID != value)
				{
					this._SectorID = value;
				}
			}
		}

		[Column(Storage = "_BSPName", DbType = "NVarChar(50) NOT NULL", CanBeNull = false)]
		public string BSPName
		{
			get
			{
				return this._BSPName;
			}
			set
			{
				if (this._BSPName != value)
				{
					this._BSPName = value;
				}
			}
		}

		[Column(Storage = "_SubSector", DbType = "NVarChar(50)")]
		public string SubSector
		{
			get
			{
				return this._SubSector;
			}
			set
			{
				if (this._SubSector != value)
				{
					this._SubSector = value;
				}
			}
		}

		[Column(Storage = "_Weight", DbType = "Int NOT NULL")]
		public int Weight
		{
			get
			{
				return this._Weight;
			}
			set
			{
				if (this._Weight != value)
				{
					this._Weight = value;
				}
			}
		}

		[Column(Storage = "_MinPropItemNum", DbType = "Int NOT NULL")]
		public int MinPropItemNum
		{
			get
			{
				return this._MinPropItemNum;
			}
			set
			{
				if (this._MinPropItemNum != value)
				{
					this._MinPropItemNum = value;
				}
			}
		}

		[Column(Storage = "_MaxPropItemNum", DbType = "Int NOT NULL")]
		public int MaxPropItemNum
		{
			get
			{
				return this._MaxPropItemNum;
			}
			set
			{
				if (this._MaxPropItemNum != value)
				{
					this._MaxPropItemNum = value;
				}
			}
		}

		[Column(Storage = "_GoldDropTotal", DbType = "Int NOT NULL")]
		public int GoldDropTotal
		{
			get
			{
				return this._GoldDropTotal;
			}
			set
			{
				if (this._GoldDropTotal != value)
				{
					this._GoldDropTotal = value;
				}
			}
		}

		[Column(Storage = "_GoldDropPerProp", DbType = "Int NOT NULL")]
		public int GoldDropPerProp
		{
			get
			{
				return this._GoldDropPerProp;
			}
			set
			{
				if (this._GoldDropPerProp != value)
				{
					this._GoldDropPerProp = value;
				}
			}
		}

		private string _QuestID;

		private int _Sequence;

		private int _SectorID;

		private string _BSPName;

		private string _SubSector;

		private int _Weight;

		private int _MinPropItemNum;

		private int _MaxPropItemNum;

		private int _GoldDropTotal;

		private int _GoldDropPerProp;
	}
}
