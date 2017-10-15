using System;
using System.Data.Linq.Mapping;

namespace ServiceCore.HeroesContents
{
	[Table(Name = "dbo.BingoReward")]
	public class BingoReward
	{
		[Column(Storage = "_BingoEvent", DbType = "Int NOT NULL")]
		public int BingoEvent
		{
			get
			{
				return this._BingoEvent;
			}
			set
			{
				if (this._BingoEvent != value)
				{
					this._BingoEvent = value;
				}
			}
		}

		[Column(Storage = "_RewardID", DbType = "NVarChar(50)")]
		public string RewardID
		{
			get
			{
				return this._RewardID;
			}
			set
			{
				if (this._RewardID != value)
				{
					this._RewardID = value;
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

		[Column(Storage = "_RewardItemClass", DbType = "NVarChar(250) NOT NULL", CanBeNull = false)]
		public string RewardItemClass
		{
			get
			{
				return this._RewardItemClass;
			}
			set
			{
				if (this._RewardItemClass != value)
				{
					this._RewardItemClass = value;
				}
			}
		}

		[Column(Storage = "_Count", DbType = "Int")]
		public int? Count
		{
			get
			{
				return this._Count;
			}
			set
			{
				if (this._Count != value)
				{
					this._Count = value;
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

		private int _BingoEvent;

		private string _RewardID;

		private int? _CharacterType;

		private string _RewardItemClass;

		private int? _Count;

		private int? _Color1;

		private int? _Color2;

		private int? _Color3;

		private int? _NotifyMessage;
	}
}
