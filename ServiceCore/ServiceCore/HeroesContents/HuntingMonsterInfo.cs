using System;
using System.Data.Linq.Mapping;

namespace ServiceCore.HeroesContents
{
	[Table(Name = "dbo.HuntingMonsterInfo")]
	public class HuntingMonsterInfo
	{
		[Column(Storage = "_MonsterID", DbType = "NVarChar(50) NOT NULL", CanBeNull = false)]
		public string MonsterID
		{
			get
			{
				return this._MonsterID;
			}
			set
			{
				if (this._MonsterID != value)
				{
					this._MonsterID = value;
				}
			}
		}

		[Column(Storage = "_modelname", DbType = "NVarChar(50) NOT NULL", CanBeNull = false)]
		public string modelname
		{
			get
			{
				return this._modelname;
			}
			set
			{
				if (this._modelname != value)
				{
					this._modelname = value;
				}
			}
		}

		[Column(Storage = "_speed", DbType = "Int NOT NULL")]
		public int speed
		{
			get
			{
				return this._speed;
			}
			set
			{
				if (this._speed != value)
				{
					this._speed = value;
				}
			}
		}

		[Column(Storage = "_FishType", DbType = "NVarChar(10) NOT NULL", CanBeNull = false)]
		public string FishType
		{
			get
			{
				return this._FishType;
			}
			set
			{
				if (this._FishType != value)
				{
					this._FishType = value;
				}
			}
		}

		[Column(Storage = "_TextureBefore", DbType = "Int NOT NULL")]
		public int TextureBefore
		{
			get
			{
				return this._TextureBefore;
			}
			set
			{
				if (this._TextureBefore != value)
				{
					this._TextureBefore = value;
				}
			}
		}

		[Column(Storage = "_TextureAfter", DbType = "Int NOT NULL")]
		public int TextureAfter
		{
			get
			{
				return this._TextureAfter;
			}
			set
			{
				if (this._TextureAfter != value)
				{
					this._TextureAfter = value;
				}
			}
		}

		[Column(Storage = "_ThresholdSize", DbType = "Int NOT NULL")]
		public int ThresholdSize
		{
			get
			{
				return this._ThresholdSize;
			}
			set
			{
				if (this._ThresholdSize != value)
				{
					this._ThresholdSize = value;
				}
			}
		}

		[Column(Storage = "_MonsterVariation", DbType = "NVarChar(50)")]
		public string MonsterVariation
		{
			get
			{
				return this._MonsterVariation;
			}
			set
			{
				if (this._MonsterVariation != value)
				{
					this._MonsterVariation = value;
				}
			}
		}

		[Column(Storage = "_AIType", DbType = "NVarChar(50) NOT NULL", CanBeNull = false)]
		public string AIType
		{
			get
			{
				return this._AIType;
			}
			set
			{
				if (this._AIType != value)
				{
					this._AIType = value;
				}
			}
		}

		private string _MonsterID;

		private string _modelname;

		private int _speed;

		private string _FishType;

		private int _TextureBefore;

		private int _TextureAfter;

		private int _ThresholdSize;

		private string _MonsterVariation;

		private string _AIType;
	}
}
