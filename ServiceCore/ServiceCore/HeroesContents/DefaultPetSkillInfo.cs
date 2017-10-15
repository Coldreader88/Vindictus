using System;
using System.Data.Linq.Mapping;

namespace ServiceCore.HeroesContents
{
	[Table(Name = "dbo.DefaultPetSkillInfo")]
	public class DefaultPetSkillInfo
	{
		[Column(Storage = "_PetID", DbType = "Int NOT NULL")]
		public int PetID
		{
			get
			{
				return this._PetID;
			}
			set
			{
				if (this._PetID != value)
				{
					this._PetID = value;
				}
			}
		}

		[Column(Storage = "_SlotOrder", DbType = "Int NOT NULL")]
		public int SlotOrder
		{
			get
			{
				return this._SlotOrder;
			}
			set
			{
				if (this._SlotOrder != value)
				{
					this._SlotOrder = value;
				}
			}
		}

		[Column(Storage = "_PetSkillID", DbType = "Int NOT NULL")]
		public int PetSkillID
		{
			get
			{
				return this._PetSkillID;
			}
			set
			{
				if (this._PetSkillID != value)
				{
					this._PetSkillID = value;
				}
			}
		}

		[Column(Storage = "_OpenLevel", DbType = "Int NOT NULL")]
		public int OpenLevel
		{
			get
			{
				return this._OpenLevel;
			}
			set
			{
				if (this._OpenLevel != value)
				{
					this._OpenLevel = value;
				}
			}
		}

		private int _PetID;

		private int _SlotOrder;

		private int _PetSkillID;

		private int _OpenLevel;
	}
}
