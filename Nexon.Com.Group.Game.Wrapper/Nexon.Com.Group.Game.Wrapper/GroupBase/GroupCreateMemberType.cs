using System;

namespace Nexon.Com.Group.Game.Wrapper.GroupBase
{
	internal class GroupCreateMemberType
	{
		public GroupCreateMemberType(bool isSetDefault)
		{
			if (isSetDefault)
			{
				this._Master = "마스터";
				this._Manager = "운영진";
				this._Level1 = "일반회원LV1";
				this._Level2 = "일반회원LV2";
				this._Level3 = "일반회원LV3";
				this._Level4 = "일반회원LV4";
				this._Level5 = "일반회원LV5";
			}
		}

		internal string Level1
		{
			get
			{
				return this._Master;
			}
			set
			{
				this._Master = value;
			}
		}

		internal string Level2
		{
			get
			{
				return this._Master;
			}
			set
			{
				this._Master = value;
			}
		}

		internal string Level3
		{
			get
			{
				return this._Master;
			}
			set
			{
				this._Master = value;
			}
		}

		internal string Level4
		{
			get
			{
				return this._Master;
			}
			set
			{
				this._Master = value;
			}
		}

		internal string Level5
		{
			get
			{
				return this._Master;
			}
			set
			{
				this._Master = value;
			}
		}

		internal string Manager
		{
			get
			{
				return this._Master;
			}
			set
			{
				this._Master = value;
			}
		}

		internal string Master
		{
			get
			{
				return this._Master;
			}
			set
			{
				this._Master = value;
			}
		}

		private string _Level1 = string.Empty;

		private string _Level2 = string.Empty;

		private string _Level3 = string.Empty;

		private string _Level4 = string.Empty;

		private string _Level5 = string.Empty;

		private string _Manager = string.Empty;

		private string _Master = string.Empty;
	}
}
