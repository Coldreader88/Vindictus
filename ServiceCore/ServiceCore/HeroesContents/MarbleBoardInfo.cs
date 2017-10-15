using System;
using System.Data.Linq.Mapping;

namespace ServiceCore.HeroesContents
{
	[Table(Name = "dbo.MarbleBoardInfo")]
	public class MarbleBoardInfo
	{
		[Column(Storage = "_MarbleID", DbType = "Int NOT NULL")]
		public int MarbleID
		{
			get
			{
				return this._MarbleID;
			}
			set
			{
				if (this._MarbleID != value)
				{
					this._MarbleID = value;
				}
			}
		}

		[Column(Storage = "_BoardID", DbType = "Int NOT NULL")]
		public int BoardID
		{
			get
			{
				return this._BoardID;
			}
			set
			{
				if (this._BoardID != value)
				{
					this._BoardID = value;
				}
			}
		}

		[Column(Storage = "_MarbleType", DbType = "Int NOT NULL")]
		public int MarbleType
		{
			get
			{
				return this._MarbleType;
			}
			set
			{
				if (this._MarbleType != value)
				{
					this._MarbleType = value;
				}
			}
		}

		[Column(Storage = "_Title", DbType = "NVarChar(256) NOT NULL", CanBeNull = false)]
		public string Title
		{
			get
			{
				return this._Title;
			}
			set
			{
				if (this._Title != value)
				{
					this._Title = value;
				}
			}
		}

		[Column(Storage = "_Date", DbType = "NVarChar(256) NOT NULL", CanBeNull = false)]
		public string Date
		{
			get
			{
				return this._Date;
			}
			set
			{
				if (this._Date != value)
				{
					this._Date = value;
				}
			}
		}

		[Column(Storage = "_IsDT", DbType = "Bit NOT NULL")]
		public bool IsDT
		{
			get
			{
				return this._IsDT;
			}
			set
			{
				if (this._IsDT != value)
				{
					this._IsDT = value;
				}
			}
		}

		[Column(Storage = "_StartDiceCount", DbType = "Int")]
		public int? StartDiceCount
		{
			get
			{
				return this._StartDiceCount;
			}
			set
			{
				if (this._StartDiceCount != value)
				{
					this._StartDiceCount = value;
				}
			}
		}

		[Column(Storage = "_MaxDiceCount", DbType = "Int")]
		public int? MaxDiceCount
		{
			get
			{
				return this._MaxDiceCount;
			}
			set
			{
				if (this._MaxDiceCount != value)
				{
					this._MaxDiceCount = value;
				}
			}
		}

		[Column(Storage = "_ChargeInterval", DbType = "Int")]
		public int? ChargeInterval
		{
			get
			{
				return this._ChargeInterval;
			}
			set
			{
				if (this._ChargeInterval != value)
				{
					this._ChargeInterval = value;
				}
			}
		}

		[Column(Storage = "_DiceItemClass", DbType = "NVarChar(128)")]
		public string DiceItemClass
		{
			get
			{
				return this._DiceItemClass;
			}
			set
			{
				if (this._DiceItemClass != value)
				{
					this._DiceItemClass = value;
				}
			}
		}

		private int _MarbleID;

		private int _BoardID;

		private int _MarbleType;

		private string _Title;

		private string _Date;

		private bool _IsDT;

		private int? _StartDiceCount;

		private int? _MaxDiceCount;

		private int? _ChargeInterval;

		private string _DiceItemClass;
	}
}
