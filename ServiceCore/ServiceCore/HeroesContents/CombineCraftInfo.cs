using System;
using System.Collections.Generic;
using System.Data.Linq.Mapping;
using System.Linq;

namespace ServiceCore.HeroesContents
{
	[Table(Name = "dbo.CombineCraftInfo")]
	public class CombineCraftInfo
	{
		public List<CombineCraftInfo.Parts> PartsList
		{
			get
			{
				if (this.partsList == null)
				{
					this.Build();
				}
				return this.partsList;
			}
		}

		public CombineCraftInfo.Parts MainParts
		{
			get
			{
				if (this.partsList == null)
				{
					this.Build();
				}
				if (this.partsList.Count > 0)
				{
					this.partsList.ElementAt(0);
				}
				return null;
			}
		}

		public int PartCount
		{
			get
			{
				if (this.partsList == null)
				{
					this.Build();
				}
				return this.partCount;
			}
		}

		public void Build()
		{
			this.partCount = 0;
			this.partsList = new List<CombineCraftInfo.Parts>();
			if (this.PartsGroup1 != null && this.PartsGroupIcon1 != null)
			{
				this.partsList.Add(new CombineCraftInfo.Parts(this.PartsGroup1, this.PartsGroupIcon1));
				this.partCount++;
			}
			if (this.PartsGroup2 != null && this.PartsGroupIcon2 != null)
			{
				this.partsList.Add(new CombineCraftInfo.Parts(this.PartsGroup2, this.PartsGroupIcon2));
				this.partCount++;
			}
			if (this.PartsGroup3 != null && this.PartsGroupIcon3 != null)
			{
				this.partsList.Add(new CombineCraftInfo.Parts(this.PartsGroup3, this.PartsGroupIcon3));
				this.partCount++;
			}
			if (this.PartsGroup4 != null && this.PartsGroupIcon4 != null)
			{
				this.partsList.Add(new CombineCraftInfo.Parts(this.PartsGroup4, this.PartsGroupIcon4));
				this.partCount++;
			}
			if (this.PartsGroup5 != null && this.PartsGroupIcon5 != null)
			{
				this.partsList.Add(new CombineCraftInfo.Parts(this.PartsGroup5, this.PartsGroupIcon5));
				this.partCount++;
			}
		}

		[Column(Storage = "_ItemClass", DbType = "NVarChar(128) NOT NULL", CanBeNull = false)]
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

		[Column(Storage = "_CombinationPrice", DbType = "Int NOT NULL")]
		public int CombinationPrice
		{
			get
			{
				return this._CombinationPrice;
			}
			set
			{
				if (this._CombinationPrice != value)
				{
					this._CombinationPrice = value;
				}
			}
		}

		[Column(Storage = "_PartsGroup1", DbType = "NVarChar(50)")]
		public string PartsGroup1
		{
			get
			{
				return this._PartsGroup1;
			}
			set
			{
				if (this._PartsGroup1 != value)
				{
					this._PartsGroup1 = value;
				}
			}
		}

		[Column(Storage = "_PartsGroupIcon1", DbType = "NVarChar(50)")]
		public string PartsGroupIcon1
		{
			get
			{
				return this._PartsGroupIcon1;
			}
			set
			{
				if (this._PartsGroupIcon1 != value)
				{
					this._PartsGroupIcon1 = value;
				}
			}
		}

		[Column(Storage = "_PartsGroup2", DbType = "NVarChar(50)")]
		public string PartsGroup2
		{
			get
			{
				return this._PartsGroup2;
			}
			set
			{
				if (this._PartsGroup2 != value)
				{
					this._PartsGroup2 = value;
				}
			}
		}

		[Column(Storage = "_PartsGroupIcon2", DbType = "NVarChar(50)")]
		public string PartsGroupIcon2
		{
			get
			{
				return this._PartsGroupIcon2;
			}
			set
			{
				if (this._PartsGroupIcon2 != value)
				{
					this._PartsGroupIcon2 = value;
				}
			}
		}

		[Column(Storage = "_PartsGroup3", DbType = "NVarChar(50)")]
		public string PartsGroup3
		{
			get
			{
				return this._PartsGroup3;
			}
			set
			{
				if (this._PartsGroup3 != value)
				{
					this._PartsGroup3 = value;
				}
			}
		}

		[Column(Storage = "_PartsGroupIcon3", DbType = "NVarChar(50)")]
		public string PartsGroupIcon3
		{
			get
			{
				return this._PartsGroupIcon3;
			}
			set
			{
				if (this._PartsGroupIcon3 != value)
				{
					this._PartsGroupIcon3 = value;
				}
			}
		}

		[Column(Storage = "_PartsGroup4", DbType = "NVarChar(50)")]
		public string PartsGroup4
		{
			get
			{
				return this._PartsGroup4;
			}
			set
			{
				if (this._PartsGroup4 != value)
				{
					this._PartsGroup4 = value;
				}
			}
		}

		[Column(Storage = "_PartsGroupIcon4", DbType = "NVarChar(50)")]
		public string PartsGroupIcon4
		{
			get
			{
				return this._PartsGroupIcon4;
			}
			set
			{
				if (this._PartsGroupIcon4 != value)
				{
					this._PartsGroupIcon4 = value;
				}
			}
		}

		[Column(Storage = "_PartsGroup5", DbType = "NVarChar(50)")]
		public string PartsGroup5
		{
			get
			{
				return this._PartsGroup5;
			}
			set
			{
				if (this._PartsGroup5 != value)
				{
					this._PartsGroup5 = value;
				}
			}
		}

		[Column(Storage = "_PartsGroupIcon5", DbType = "NVarChar(50)")]
		public string PartsGroupIcon5
		{
			get
			{
				return this._PartsGroupIcon5;
			}
			set
			{
				if (this._PartsGroupIcon5 != value)
				{
					this._PartsGroupIcon5 = value;
				}
			}
		}

		[Column(Storage = "_AppearQuestID", DbType = "NVarChar(50)")]
		public string AppearQuestID
		{
			get
			{
				return this._AppearQuestID;
			}
			set
			{
				if (this._AppearQuestID != value)
				{
					this._AppearQuestID = value;
				}
			}
		}

		[Column(Storage = "_Feature", DbType = "NVarChar(128)")]
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

		private List<CombineCraftInfo.Parts> partsList;

		private int partCount;

		private string _ItemClass;

		private int _CombinationPrice;

		private string _PartsGroup1;

		private string _PartsGroupIcon1;

		private string _PartsGroup2;

		private string _PartsGroupIcon2;

		private string _PartsGroup3;

		private string _PartsGroupIcon3;

		private string _PartsGroup4;

		private string _PartsGroupIcon4;

		private string _PartsGroup5;

		private string _PartsGroupIcon5;

		private string _AppearQuestID;

		private string _Feature;

		public class Parts
		{
			public Parts(string partsGroup, string partsGroupIcon)
			{
				this.PartsGroup = partsGroup;
				this.PartsGroupIcon = partsGroupIcon;
			}

			public string PartsGroup { get; set; }

			public string PartsGroupIcon { get; set; }
		}
	}
}
