using System;
using System.Data.Linq.Mapping;

namespace ServiceCore.HeroesContents
{
	[Table(Name = "dbo.QualityDiceInfo")]
	public class QualityDiceInfo
	{
		[Column(Storage = "_ItemType", DbType = "NVarChar(50) NOT NULL", CanBeNull = false)]
		public string ItemType
		{
			get
			{
				return this._ItemType;
			}
			set
			{
				if (this._ItemType != value)
				{
					this._ItemType = value;
				}
			}
		}

		[Column(Storage = "_Quality", DbType = "Int NOT NULL")]
		public int Quality
		{
			get
			{
				return this._Quality;
			}
			set
			{
				if (this._Quality != value)
				{
					this._Quality = value;
				}
			}
		}

		[Column(Storage = "_Dice", DbType = "Int NOT NULL")]
		public int Dice
		{
			get
			{
				return this._Dice;
			}
			set
			{
				if (this._Dice != value)
				{
					this._Dice = value;
				}
			}
		}

		private string _ItemType;

		private int _Quality;

		private int _Dice;
	}
}
