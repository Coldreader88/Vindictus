using System;
using System.Data.Linq.Mapping;

namespace ServiceCore.HeroesContents
{
	[Table(Name = "dbo.Friend_LevelupReward")]
	public class Friend_LevelupReward
	{
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

		[Column(Storage = "_MyItemClass", DbType = "NVarChar(128)")]
		public string MyItemClass
		{
			get
			{
				return this._MyItemClass;
			}
			set
			{
				if (this._MyItemClass != value)
				{
					this._MyItemClass = value;
				}
			}
		}

		[Column(Storage = "_MyItemCount", DbType = "Int")]
		public int? MyItemCount
		{
			get
			{
				return this._MyItemCount;
			}
			set
			{
				if (this._MyItemCount != value)
				{
					this._MyItemCount = value;
				}
			}
		}

		[Column(Storage = "_FriendItemClass", DbType = "NVarChar(128)")]
		public string FriendItemClass
		{
			get
			{
				return this._FriendItemClass;
			}
			set
			{
				if (this._FriendItemClass != value)
				{
					this._FriendItemClass = value;
				}
			}
		}

		[Column(Storage = "_FriendItemCount", DbType = "Int")]
		public int? FriendItemCount
		{
			get
			{
				return this._FriendItemCount;
			}
			set
			{
				if (this._FriendItemCount != value)
				{
					this._FriendItemCount = value;
				}
			}
		}

		[Column(Storage = "_Feature", DbType = "VarChar(50) NOT NULL", CanBeNull = false)]
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

		private int _Level;

		private string _MyItemClass;

		private int? _MyItemCount;

		private string _FriendItemClass;

		private int? _FriendItemCount;

		private string _Feature;
	}
}
