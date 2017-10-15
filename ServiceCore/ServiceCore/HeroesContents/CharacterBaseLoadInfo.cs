using System;
using System.Data.Linq.Mapping;

namespace ServiceCore.HeroesContents
{
	[Table(Name = "dbo.CharacterBaseLoadInfo")]
	public class CharacterBaseLoadInfo
	{
		[Column(Storage = "_Character", DbType = "TinyInt NOT NULL")]
		public byte Character
		{
			get
			{
				return this._Character;
			}
			set
			{
				if (this._Character != value)
				{
					this._Character = value;
				}
			}
		}

		[Column(Name = "[Level]", Storage = "_Level", DbType = "Int NOT NULL")]
		public int Level
		{
			get
			{
				return this._Level;
			}
			set
			{
				if (this._Level != value)
				{
					this._Level = value;
				}
			}
		}

		[Column(Storage = "_BaseCharacterLoadWeight", DbType = "Float NOT NULL")]
		public double BaseCharacterLoadWeight
		{
			get
			{
				return this._BaseCharacterLoadWeight;
			}
			set
			{
				if (this._BaseCharacterLoadWeight != value)
				{
					this._BaseCharacterLoadWeight = value;
				}
			}
		}

		private byte _Character;

		private int _Level;

		private double _BaseCharacterLoadWeight;
	}
}
