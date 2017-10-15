using System;
using System.Data.Linq.Mapping;

namespace ServiceCore.HeroesContents
{
	[Table(Name = "dbo.ConditionReductionInfo")]
	public class ConditionReductionInfo
	{
		[Column(Storage = "_EquipClass", DbType = "NVarChar(50) NOT NULL", CanBeNull = false)]
		public string EquipClass
		{
			get
			{
				return this._EquipClass;
			}
			set
			{
				if (this._EquipClass != value)
				{
					this._EquipClass = value;
				}
			}
		}

		[Column(Storage = "_ConditionMoveSector", DbType = "Int NOT NULL")]
		public int ConditionMoveSector
		{
			get
			{
				return this._ConditionMoveSector;
			}
			set
			{
				if (this._ConditionMoveSector != value)
				{
					this._ConditionMoveSector = value;
				}
			}
		}

		[Column(Storage = "_ConditionEndBattle", DbType = "Int NOT NULL")]
		public int ConditionEndBattle
		{
			get
			{
				return this._ConditionEndBattle;
			}
			set
			{
				if (this._ConditionEndBattle != value)
				{
					this._ConditionEndBattle = value;
				}
			}
		}

		[Column(Storage = "_MaxConditionMoveSector", DbType = "Int NOT NULL")]
		public int MaxConditionMoveSector
		{
			get
			{
				return this._MaxConditionMoveSector;
			}
			set
			{
				if (this._MaxConditionMoveSector != value)
				{
					this._MaxConditionMoveSector = value;
				}
			}
		}

		[Column(Storage = "_MaxConditionEndBattle", DbType = "Int NOT NULL")]
		public int MaxConditionEndBattle
		{
			get
			{
				return this._MaxConditionEndBattle;
			}
			set
			{
				if (this._MaxConditionEndBattle != value)
				{
					this._MaxConditionEndBattle = value;
				}
			}
		}

		private string _EquipClass;

		private int _ConditionMoveSector;

		private int _ConditionEndBattle;

		private int _MaxConditionMoveSector;

		private int _MaxConditionEndBattle;
	}
}
