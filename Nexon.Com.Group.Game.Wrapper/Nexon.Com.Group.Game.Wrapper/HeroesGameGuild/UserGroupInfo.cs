using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Nexon.Com.Group.Game.Wrapper.HeroesGameGuild
{
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://tempuri.org/")]
	[GeneratedCode("System.Xml", "2.0.50727.5420")]
	[DebuggerStepThrough]
	[Serializable]
	public class UserGroupInfo
	{
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

		public string Intro
		{
			get
			{
				return this.introField;
			}
			set
			{
				this.introField = value;
			}
		}

		public int NeoxnSN_Master
		{
			get
			{
				return this.neoxnSN_MasterField;
			}
			set
			{
				this.neoxnSN_MasterField = value;
			}
		}

		public string NexonID_Master
		{
			get
			{
				return this.nexonID_MasterField;
			}
			set
			{
				this.nexonID_MasterField = value;
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

		public GroupUserType GroupUserType
		{
			get
			{
				return this.groupUserTypeField;
			}
			set
			{
				this.groupUserTypeField = value;
			}
		}

		public int RealUserCOunt
		{
			get
			{
				return this.realUserCOuntField;
			}
			set
			{
				this.realUserCOuntField = value;
			}
		}

		public DateTime dtLastContentUpdateDate
		{
			get
			{
				return this.dtLastContentUpdateDateField;
			}
			set
			{
				this.dtLastContentUpdateDateField = value;
			}
		}

		public string NameInGroup
		{
			get
			{
				return this.nameInGroupField;
			}
			set
			{
				this.nameInGroupField = value;
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

		public DateTime dateCreate
		{
			get
			{
				return this.dateCreateField;
			}
			set
			{
				this.dateCreateField = value;
			}
		}

		public int CharacterSN
		{
			get
			{
				return this.characterSNField;
			}
			set
			{
				this.characterSNField = value;
			}
		}

		private int guildSNField;

		private string guildIDField;

		private string guildNameField;

		private string introField;

		private int neoxnSN_MasterField;

		private string nexonID_MasterField;

		private string nameInGroup_MasterField;

		private GroupUserType groupUserTypeField;

		private int realUserCOuntField;

		private DateTime dtLastContentUpdateDateField;

		private string nameInGroupField;

		private string characterNameField;

		private DateTime dateCreateField;

		private int characterSNField;
	}
}
