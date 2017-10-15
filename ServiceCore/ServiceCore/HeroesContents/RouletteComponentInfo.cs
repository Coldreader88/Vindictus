using System;
using System.Data.Linq.Mapping;

namespace ServiceCore.HeroesContents
{
	[Table(Name = "dbo.RouletteComponentInfo")]
	public class RouletteComponentInfo
	{
		[Column(Storage = "_ID", DbType = "Int NOT NULL")]
		public int ID
		{
			get
			{
				return this._ID;
			}
			set
			{
				if (this._ID != value)
				{
					this._ID = value;
				}
			}
		}

		[Column(Storage = "_RouletteID", DbType = "Int NOT NULL")]
		public int RouletteID
		{
			get
			{
				return this._RouletteID;
			}
			set
			{
				if (this._RouletteID != value)
				{
					this._RouletteID = value;
				}
			}
		}

		[Column(Storage = "_Grade", DbType = "NVarChar(4) NOT NULL", CanBeNull = false)]
		public string Grade
		{
			get
			{
				return this._Grade;
			}
			set
			{
				if (this._Grade != value)
				{
					this._Grade = value;
				}
			}
		}

		[Column(Storage = "_CharacterType", DbType = "Int")]
		public int? CharacterType
		{
			get
			{
				return this._CharacterType;
			}
			set
			{
				if (this._CharacterType != value)
				{
					this._CharacterType = value;
				}
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

		[Column(Storage = "_Number", DbType = "Int NOT NULL")]
		public int Number
		{
			get
			{
				return this._Number;
			}
			set
			{
				if (this._Number != value)
				{
					this._Number = value;
				}
			}
		}

		[Column(Storage = "_ProbabilityPerSlot", DbType = "Float NOT NULL")]
		public double ProbabilityPerSlot
		{
			get
			{
				return this._ProbabilityPerSlot;
			}
			set
			{
				if (this._ProbabilityPerSlot != value)
				{
					this._ProbabilityPerSlot = value;
				}
			}
		}

		[Column(Storage = "_AppearanceCount", DbType = "Int")]
		public int? AppearanceCount
		{
			get
			{
				return this._AppearanceCount;
			}
			set
			{
				if (this._AppearanceCount != value)
				{
					this._AppearanceCount = value;
				}
			}
		}

		[Column(Storage = "_Color1", DbType = "Int")]
		public int? Color1
		{
			get
			{
				return this._Color1;
			}
			set
			{
				if (this._Color1 != value)
				{
					this._Color1 = value;
				}
			}
		}

		[Column(Storage = "_Color2", DbType = "Int")]
		public int? Color2
		{
			get
			{
				return this._Color2;
			}
			set
			{
				if (this._Color2 != value)
				{
					this._Color2 = value;
				}
			}
		}

		[Column(Storage = "_Color3", DbType = "Int")]
		public int? Color3
		{
			get
			{
				return this._Color3;
			}
			set
			{
				if (this._Color3 != value)
				{
					this._Color3 = value;
				}
			}
		}

		[Column(Storage = "_NotifyMessage", DbType = "Int")]
		public int? NotifyMessage
		{
			get
			{
				return this._NotifyMessage;
			}
			set
			{
				if (this._NotifyMessage != value)
				{
					this._NotifyMessage = value;
				}
			}
		}

		[Column(Storage = "_Bind", DbType = "Int")]
		public int? Bind
		{
			get
			{
				return this._Bind;
			}
			set
			{
				if (this._Bind != value)
				{
					this._Bind = value;
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

		private int _ID;

		private int _RouletteID;

		private string _Grade;

		private int? _CharacterType;

		private string _ItemClass;

		private int _Number;

		private double _ProbabilityPerSlot;

		private int? _AppearanceCount;

		private int? _Color1;

		private int? _Color2;

		private int? _Color3;

		private int? _NotifyMessage;

		private int? _Bind;

		private string _Feature;
	}
}
