using System;
using System.Data.Linq.Mapping;

namespace ServiceCore.HeroesContents
{
	[Table(Name = "dbo.MarbleDiceInfo")]
	public class MarbleDiceInfo
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

		[Column(Storage = "_DiceID", DbType = "Int NOT NULL")]
		public int DiceID
		{
			get
			{
				return this._DiceID;
			}
			set
			{
				if (this._DiceID != value)
				{
					this._DiceID = value;
				}
			}
		}

		[Column(Storage = "_DiceType", DbType = "Int NOT NULL")]
		public int DiceType
		{
			get
			{
				return this._DiceType;
			}
			set
			{
				if (this._DiceType != value)
				{
					this._DiceType = value;
				}
			}
		}

		[Column(Storage = "_DiceItemClass", DbType = "NVarChar(128) NOT NULL", CanBeNull = false)]
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

		private int _DiceID;

		private int _DiceType;

		private string _DiceItemClass;
	}
}
