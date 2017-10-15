using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Nexon.Com.Group.Game.Wrapper.HeroesGameGuild
{
	[GeneratedCode("System.Xml", "2.0.50727.5420")]
	[DesignerCategory("code")]
	[DebuggerStepThrough]
	[XmlType(Namespace = "http://tempuri.org/")]
	[Serializable]
	public class GroupUserInfo
	{
		public string Name
		{
			get
			{
				return this.nameField;
			}
			set
			{
				this.nameField = value;
			}
		}

		public byte isNewMember
		{
			get
			{
				return this.isNewMemberField;
			}
			set
			{
				this.isNewMemberField = value;
			}
		}

		public int NexonSN
		{
			get
			{
				return this.nexonSNField;
			}
			set
			{
				this.nexonSNField = value;
			}
		}

		public string NameInGroup_User
		{
			get
			{
				return this.nameInGroup_UserField;
			}
			set
			{
				this.nameInGroup_UserField = value;
			}
		}

		public int GuildSN
		{
			get
			{
				return this.guildSNField;
			}
			set
			{
				this.guildSNField = value;
			}
		}

		public string GuildID
		{
			get
			{
				return this.guildIDField;
			}
			set
			{
				this.guildIDField = value;
			}
		}

		public string GuildName
		{
			get
			{
				return this.guildNameField;
			}
			set
			{
				this.guildNameField = value;
			}
		}

		public string GuildIntro
		{
			get
			{
				return this.guildIntroField;
			}
			set
			{
				this.guildIntroField = value;
			}
		}

		public int NexonSN_Master
		{
			get
			{
				return this.nexonSN_MasterField;
			}
			set
			{
				this.nexonSN_MasterField = value;
			}
		}

		public string NameInGroup_Master
		{
			get
			{
				return this.nameInGroup_MasterField;
			}
			set
			{
				this.nameInGroup_MasterField = value;
			}
		}

		public GroupUserType emGroupUserType
		{
			get
			{
				return this.emGroupUserTypeField;
			}
			set
			{
				this.emGroupUserTypeField = value;
			}
		}

		public int RealUserCount
		{
			get
			{
				return this.realUserCountField;
			}
			set
			{
				this.realUserCountField = value;
			}
		}

		public DateTime dtLastLoginTimeDate
		{
			get
			{
				return this.dtLastLoginTimeDateField;
			}
			set
			{
				this.dtLastLoginTimeDateField = value;
			}
		}

		public string CharacterName
		{
			get
			{
				return this.characterNameField;
			}
			set
			{
				this.characterNameField = value;
			}
		}

		private string nameField;

		private byte isNewMemberField;

		private int nexonSNField;

		private string nameInGroup_UserField;

		private int guildSNField;

		private string guildIDField;

		private string guildNameField;

		private string guildIntroField;

		private int nexonSN_MasterField;

		private string nameInGroup_MasterField;

		private GroupUserType emGroupUserTypeField;

		private int realUserCountField;

		private DateTime dtLastLoginTimeDateField;

		private string characterNameField;
	}
}
