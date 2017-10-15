using System;
using System.Data.Linq.Mapping;

namespace ServiceCore.HeroesContents
{
	[Table(Name = "dbo.HotSpringPotionInfo")]
	public class HotSpringPotionInfo
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

		[Column(Storage = "_UntilEffectSec", DbType = "Int NOT NULL")]
		public int UntilEffectSec
		{
			get
			{
				return this._UntilEffectSec;
			}
			set
			{
				if (this._UntilEffectSec != value)
				{
					this._UntilEffectSec = value;
				}
			}
		}

		[Column(Storage = "_BathTimeSec", DbType = "Int NOT NULL")]
		public int BathTimeSec
		{
			get
			{
				return this._BathTimeSec;
			}
			set
			{
				if (this._BathTimeSec != value)
				{
					this._BathTimeSec = value;
				}
			}
		}

		[Column(Storage = "_OverlapMinSec", DbType = "Int NOT NULL")]
		public int OverlapMinSec
		{
			get
			{
				return this._OverlapMinSec;
			}
			set
			{
				if (this._OverlapMinSec != value)
				{
					this._OverlapMinSec = value;
				}
			}
		}

		[Column(Storage = "_AllAnnounce", DbType = "TinyInt NOT NULL")]
		public byte AllAnnounce
		{
			get
			{
				return this._AllAnnounce;
			}
			set
			{
				if (this._AllAnnounce != value)
				{
					this._AllAnnounce = value;
				}
			}
		}

		[Column(Storage = "_ShowName", DbType = "TinyInt NOT NULL")]
		public byte ShowName
		{
			get
			{
				return this._ShowName;
			}
			set
			{
				if (this._ShowName != value)
				{
					this._ShowName = value;
				}
			}
		}

		private string _ItemClass;

		private string _StatusEffect;

		private int _Level;

		private int _UntilEffectSec;

		private int _BathTimeSec;

		private int _OverlapMinSec;

		private byte _AllAnnounce;

		private byte _ShowName;
	}
}
