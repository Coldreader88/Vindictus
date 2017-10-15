using System;

namespace Nexon.Com.Group.Game.Wrapper.GroupBase
{
	internal class GroupCreateSkin
	{
		public GroupCreateSkin(bool isSetDefault)
		{
			if (isSetDefault)
			{
				this.GroupSkinSyle = 4;
				this.IsFullGroupSkin = false;
				this.IsDefinedGroupSkin = false;
				this.BBSTitleColor = "#115C88";
				this.BGColor = "#EBEBEB";
				this.BGImage = "";
				this.LeftMenuColor = "#E5FAFF";
				this.LoginBottomColor = "#ffffff";
				this.LoginBoxColor = "#61C9ED";
				this.LoginBoxUpperColor = "#115C88";
				this.MidBarColor = "#B6E7F5";
				this.TitleColor = "#115C88";
				this.UpperColor = "#61C9ED";
				this.UpperImage = "";
				this.MenuFontColor = "4d4d4d";
			}
		}

		public string BBSTitleColor
		{
			get
			{
				return this._BBSTitleColor;
			}
			set
			{
				this._BBSTitleColor = value;
			}
		}

		public string BGColor
		{
			get
			{
				return this._BGColor;
			}
			set
			{
				this._BGColor = value;
			}
		}

		public string BGImage
		{
			get
			{
				return this._BGImage;
			}
			set
			{
				this._BGImage = value;
			}
		}

		public byte GroupSkinSyle
		{
			get
			{
				return this._GroupSkinStyle;
			}
			set
			{
				this._GroupSkinStyle = value;
			}
		}

		public bool IsDefinedGroupSkin
		{
			get
			{
				return this._IsDefinedGroupSkin;
			}
			set
			{
				this._IsDefinedGroupSkin = value;
			}
		}

		public bool IsFullGroupSkin
		{
			get
			{
				return this._IsFullGroupSkin;
			}
			set
			{
				this._IsFullGroupSkin = value;
			}
		}

		public string LeftMenuColor
		{
			get
			{
				return this._LeftMenuColor;
			}
			set
			{
				this._LeftMenuColor = value;
			}
		}

		public string LoginBottomColor
		{
			get
			{
				return this._LoginBottomColor;
			}
			set
			{
				this._LoginBottomColor = value;
			}
		}

		public string LoginBoxColor
		{
			get
			{
				return this._LoginBoxColor;
			}
			set
			{
				this._LoginBoxColor = value;
			}
		}

		public string LoginBoxUpperColor
		{
			get
			{
				return this._LoginBoxUpperColor;
			}
			set
			{
				this._LoginBoxUpperColor = value;
			}
		}

		public string MenuFontColor
		{
			get
			{
				return this._MenuFontColor;
			}
			set
			{
				this._MenuFontColor = value;
			}
		}

		public string MidBarColor
		{
			get
			{
				return this._MidBarColor;
			}
			set
			{
				this._MidBarColor = value;
			}
		}

		public string TitleColor
		{
			get
			{
				return this._TitleColor;
			}
			set
			{
				this._TitleColor = value;
			}
		}

		public string UpperColor
		{
			get
			{
				return this._UpperColor;
			}
			set
			{
				this._UpperColor = value;
			}
		}

		public string UpperImage
		{
			get
			{
				return this._UpperImage;
			}
			set
			{
				this._UpperImage = value;
			}
		}

		private string _BBSTitleColor = string.Empty;

		private string _BGColor = string.Empty;

		private string _BGImage = string.Empty;

		private byte _GroupSkinStyle;

		private bool _IsDefinedGroupSkin;

		private bool _IsFullGroupSkin;

		private string _LeftMenuColor = string.Empty;

		private string _LoginBottomColor = string.Empty;

		private string _LoginBoxColor = string.Empty;

		private string _LoginBoxUpperColor = string.Empty;

		private string _MenuFontColor = string.Empty;

		private string _MidBarColor = string.Empty;

		private string _TitleColor = string.Empty;

		private string _UpperColor = string.Empty;

		private string _UpperImage = string.Empty;
	}
}
