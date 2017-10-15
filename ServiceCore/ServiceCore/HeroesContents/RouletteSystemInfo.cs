using System;
using System.Data.Linq.Mapping;

namespace ServiceCore.HeroesContents
{
	[Table(Name = "dbo.RouletteSystemInfo")]
	public class RouletteSystemInfo
	{
		[Column(Storage = "_Type", DbType = "VarChar(16) NOT NULL", CanBeNull = false)]
		public string Type
		{
			get
			{
				return this._Type;
			}
			set
			{
				if (this._Type != value)
				{
					this._Type = value;
				}
			}
		}

		[Column(Storage = "_probability", DbType = "Float NOT NULL")]
		public double probability
		{
			get
			{
				return this._probability;
			}
			set
			{
				if (this._probability != value)
				{
					this._probability = value;
				}
			}
		}

		[Column(Storage = "_SlotS", DbType = "Int NOT NULL")]
		public int SlotS
		{
			get
			{
				return this._SlotS;
			}
			set
			{
				if (this._SlotS != value)
				{
					this._SlotS = value;
				}
			}
		}

		[Column(Storage = "_SlotA", DbType = "Int NOT NULL")]
		public int SlotA
		{
			get
			{
				return this._SlotA;
			}
			set
			{
				if (this._SlotA != value)
				{
					this._SlotA = value;
				}
			}
		}

		[Column(Storage = "_SlotB", DbType = "Int NOT NULL")]
		public int SlotB
		{
			get
			{
				return this._SlotB;
			}
			set
			{
				if (this._SlotB != value)
				{
					this._SlotB = value;
				}
			}
		}

		[Column(Storage = "_SlotC", DbType = "Int NOT NULL")]
		public int SlotC
		{
			get
			{
				return this._SlotC;
			}
			set
			{
				if (this._SlotC != value)
				{
					this._SlotC = value;
				}
			}
		}

		private string _Type;

		private double _probability;

		private int _SlotS;

		private int _SlotA;

		private int _SlotB;

		private int _SlotC;
	}
}
