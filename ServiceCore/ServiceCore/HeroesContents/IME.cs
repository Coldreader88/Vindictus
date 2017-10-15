using System;
using System.Data.Linq.Mapping;

namespace ServiceCore.HeroesContents
{
	[Table(Name = "dbo.IME")]
	public class IME
	{
		[Column(Storage = "_ItemNameOnSystem", DbType = "NVarChar(256) NOT NULL", CanBeNull = false)]
		public string ItemNameOnSystem
		{
			get
			{
				return this._ItemNameOnSystem;
			}
			set
			{
				if (this._ItemNameOnSystem != value)
				{
					this._ItemNameOnSystem = value;
				}
			}
		}

		[Column(Storage = "_ItemNameCommon", DbType = "NVarChar(256)")]
		public string ItemNameCommon
		{
			get
			{
				return this._ItemNameCommon;
			}
			set
			{
				if (this._ItemNameCommon != value)
				{
					this._ItemNameCommon = value;
				}
			}
		}

		[Column(Storage = "_ItemTag", DbType = "NVarChar(256)")]
		public string ItemTag
		{
			get
			{
				return this._ItemTag;
			}
			set
			{
				if (this._ItemTag != value)
				{
					this._ItemTag = value;
				}
			}
		}

		[Column(Storage = "_TagType", DbType = "NVarChar(256)")]
		public string TagType
		{
			get
			{
				return this._TagType;
			}
			set
			{
				if (this._TagType != value)
				{
					this._TagType = value;
				}
			}
		}

		[Column(Storage = "_Enable", DbType = "Int")]
		public int? Enable
		{
			get
			{
				return this._Enable;
			}
			set
			{
				if (this._Enable != value)
				{
					this._Enable = value;
				}
			}
		}

		private string _ItemNameOnSystem;

		private string _ItemNameCommon;

		private string _ItemTag;

		private string _TagType;

		private int? _Enable;
	}
}
