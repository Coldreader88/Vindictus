using System;
using System.Data.Linq.Mapping;

namespace ServiceCore.HeroesContents
{
	[Table(Name = "dbo.RankTitle")]
	public class RankTitle
	{
		[Column(Storage = "_EventID", DbType = "Int NOT NULL")]
		public int EventID
		{
			get
			{
				return this._EventID;
			}
			set
			{
				if (this._EventID != value)
				{
					this._EventID = value;
				}
			}
		}

		[Column(Storage = "_Rank", DbType = "Int NOT NULL")]
		public int Rank
		{
			get
			{
				return this._Rank;
			}
			set
			{
				if (this._Rank != value)
				{
					this._Rank = value;
				}
			}
		}

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

		[Column(Storage = "_TitleColor", DbType = "NVarChar(50)")]
		public string TitleColor
		{
			get
			{
				return this._TitleColor;
			}
			set
			{
				if (this._TitleColor != value)
				{
					this._TitleColor = value;
				}
			}
		}

		[Column(Storage = "_TitleIcon", DbType = "NVarChar(50)")]
		public string TitleIcon
		{
			get
			{
				return this._TitleIcon;
			}
			set
			{
				if (this._TitleIcon != value)
				{
					this._TitleIcon = value;
				}
			}
		}

		private int _EventID;

		private int _Rank;

		private int _TitleID;

		private string _TitleColor;

		private string _TitleIcon;
	}
}
