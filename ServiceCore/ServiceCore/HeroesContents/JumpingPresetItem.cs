using System;
using System.Data.Linq.Mapping;

namespace ServiceCore.HeroesContents
{
	[Table(Name = "dbo.JumpingPresetItem")]
	public class JumpingPresetItem
	{
		[Column(Storage = "_CharacterClass", DbType = "SmallInt NOT NULL")]
		public short CharacterClass
		{
			get
			{
				return this._CharacterClass;
			}
			set
			{
				if (this._CharacterClass != value)
				{
					this._CharacterClass = value;
				}
			}
		}

		[Column(Storage = "_ItemClassEx", DbType = "NVarChar(256) NOT NULL", CanBeNull = false)]
		public string ItemClassEx
		{
			get
			{
				return this._ItemClassEx;
			}
			set
			{
				if (this._ItemClassEx != value)
				{
					this._ItemClassEx = value;
				}
			}
		}

		[Column(Storage = "_Count", DbType = "Int NOT NULL")]
		public int Count
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

		[Column(Storage = "_Feature", DbType = "NVarChar(256)")]
		public string Feature
		{
			get
			{
				return this._Feature;
			}
			set
			{
				if (this._Feature != value)
				{
					this._Feature = value;
				}
			}
		}

		private short _CharacterClass;

		private string _ItemClassEx;

		private int _Count;

		private string _Feature;
	}
}
