using System;
using System.Data.Linq.Mapping;

namespace ServiceCore.HeroesContents
{
	[Table(Name = "dbo.Friend_TitleReward")]
	public class Friend_TitleReward
	{
		[Column(Storage = "_TitleID", DbType = "Int NOT NULL")]
		public int TitleID
		{
			get
			{
				return this._TitleID;
			}
			set
			{
				if (this._TitleID != value)
				{
					this._TitleID = value;
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

		private int _TitleID;

		private string _MyItemClass;

		private string _FriendItemClass;

		private string _Feature;
	}
}
