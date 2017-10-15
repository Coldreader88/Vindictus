using System;
using System.Data.Linq.Mapping;

namespace ServiceCore.HeroesContents
{
	[Table(Name = "dbo.RandomItemV2")]
	public class RandomItemV2
	{
		[Column(Storage = "_ItemClass", DbType = "NVarChar(1024) NOT NULL", CanBeNull = false)]
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

		[Column(Storage = "_KeyItemClass", DbType = "NVarChar(1024)")]
		public string KeyItemClass
		{
			get
			{
				return this._KeyItemClass;
			}
			set
			{
				if (this._KeyItemClass != value)
				{
					this._KeyItemClass = value;
				}
			}
		}

		[Column(Storage = "_RandomItemID", DbType = "NVarChar(1024) NOT NULL", CanBeNull = false)]
		public string RandomItemID
		{
			get
			{
				return this._RandomItemID;
			}
			set
			{
				if (this._RandomItemID != value)
				{
					this._RandomItemID = value;
				}
			}
		}

		[Column(Storage = "_ShowPopUp", DbType = "Bit NOT NULL")]
		public bool ShowPopUp
		{
			get
			{
				return this._ShowPopUp;
			}
			set
			{
				if (this._ShowPopUp != value)
				{
					this._ShowPopUp = value;
				}
			}
		}

		[Column(Storage = "_IsTitiCore", DbType = "Bit NOT NULL")]
		public bool IsTitiCore
		{
			get
			{
				return this._IsTitiCore;
			}
			set
			{
				if (this._IsTitiCore != value)
				{
					this._IsTitiCore = value;
				}
			}
		}

		[Column(Storage = "_TitiCoreBaitProbability", DbType = "Float NOT NULL")]
		public double TitiCoreBaitProbability
		{
			get
			{
				return this._TitiCoreBaitProbability;
			}
			set
			{
				if (this._TitiCoreBaitProbability != value)
				{
					this._TitiCoreBaitProbability = value;
				}
			}
		}

		[Column(Storage = "_FeverPoint", DbType = "Int")]
		public int? FeverPoint
		{
			get
			{
				return this._FeverPoint;
			}
			set
			{
				if (this._FeverPoint != value)
				{
					this._FeverPoint = value;
				}
			}
		}

		[Column(Storage = "_RareItemCount", DbType = "Int")]
		public int? RareItemCount
		{
			get
			{
				return this._RareItemCount;
			}
			set
			{
				if (this._RareItemCount != value)
				{
					this._RareItemCount = value;
				}
			}
		}

		[Column(Storage = "_NormalItemCount", DbType = "Int")]
		public int? NormalItemCount
		{
			get
			{
				return this._NormalItemCount;
			}
			set
			{
				if (this._NormalItemCount != value)
				{
					this._NormalItemCount = value;
				}
			}
		}

		[Column(Storage = "_Feature", DbType = "NVarChar(50)")]
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

		[Column(Storage = "_CoolTime", DbType = "Int")]
		public int? CoolTime
		{
			get
			{
				return this._CoolTime;
			}
			set
			{
				if (this._CoolTime != value)
				{
					this._CoolTime = value;
				}
			}
		}

		[Column(Storage = "_ExculsiveID", DbType = "NVarChar(50)")]
		public string ExclusiveID
		{
			get
			{
				return this._ExculsiveID;
			}
			set
			{
				if (this._ExculsiveID != value)
				{
					this._ExculsiveID = value;
				}
			}
		}

		[Column(Storage = "_ExclusiveUseMins", DbType = "Int")]
		public int? ExclusiveUseMins
		{
			get
			{
				return this._ExclusiveUseMins;
			}
			set
			{
				if (this._ExclusiveUseMins != value)
				{
					this._ExclusiveUseMins = value;
				}
			}
		}

		private string _ItemClass;

		private string _KeyItemClass;

		private string _RandomItemID;

		private bool _ShowPopUp;

		private bool _IsTitiCore;

		private double _TitiCoreBaitProbability;

		private int? _FeverPoint;

		private int? _RareItemCount;

		private int? _NormalItemCount;

		private string _Feature;

		private int? _CoolTime;

		private string _ExculsiveID;

		private int? _ExclusiveUseMins;
	}
}
