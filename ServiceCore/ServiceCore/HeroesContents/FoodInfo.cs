using System;
using System.Data.Linq.Mapping;

namespace ServiceCore.HeroesContents
{
	[Table(Name = "dbo.FoodInfo")]
	public class FoodInfo
	{
		[Column(Storage = "_ItemClass", DbType = "NVarChar(50) NOT NULL", CanBeNull = false)]
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

		[Column(Storage = "_StatusEffect", DbType = "NVarChar(50) NOT NULL", CanBeNull = false)]
		public string StatusEffect
		{
			get
			{
				return this._StatusEffect;
			}
			set
			{
				if (this._StatusEffect != value)
				{
					this._StatusEffect = value;
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

		[Column(Storage = "_DurationSec", DbType = "Int NOT NULL")]
		public int DurationSec
		{
			get
			{
				return this._DurationSec;
			}
			set
			{
				if (this._DurationSec != value)
				{
					this._DurationSec = value;
				}
			}
		}

		[Column(Storage = "_Sharing", DbType = "TinyInt NOT NULL")]
		public byte Sharing
		{
			get
			{
				return this._Sharing;
			}
			set
			{
				if (this._Sharing != value)
				{
					this._Sharing = value;
				}
			}
		}

		private string _ItemClass;

		private string _StatusEffect;

		private int _Level;

		private int _DurationSec;

		private byte _Sharing;
	}
}
