using System;
using System.Data.Linq.Mapping;

namespace ServiceCore.HeroesContents
{
	[Table(Name = "dbo.EnhanceRuneInfo")]
	public class EnhanceRuneInfo
	{
		[Column(Storage = "_itemClass", CanBeNull = false)]
		public string itemClass
		{
			get
			{
				return this._itemClass;
			}
			set
			{
				if (this._itemClass != value)
				{
					this._itemClass = value;
				}
			}
		}

		[Column(Storage = "_ReleationNum")]
		public int ReleationNum
		{
			get
			{
				return this._ReleationNum;
			}
			set
			{
				if (this._ReleationNum != value)
				{
					this._ReleationNum = value;
				}
			}
		}

		[Column(Storage = "_MinEnhanceLevel")]
		public int MinEnhanceLevel
		{
			get
			{
				return this._MinEnhanceLevel;
			}
			set
			{
				if (this._MinEnhanceLevel != value)
				{
					this._MinEnhanceLevel = value;
				}
			}
		}

		[Column(Storage = "_MaxEnhanceLevel")]
		public int MaxEnhanceLevel
		{
			get
			{
				return this._MaxEnhanceLevel;
			}
			set
			{
				if (this._MaxEnhanceLevel != value)
				{
					this._MaxEnhanceLevel = value;
				}
			}
		}

		[Column(Storage = "_Feature")]
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

		private string _itemClass;

		private int _ReleationNum;

		private int _MinEnhanceLevel;

		private int _MaxEnhanceLevel;

		private string _Feature;
	}
}
