using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Nexon.Com.Group.Game.Wrapper.HeroesGameGuild
{
	[XmlType(Namespace = "http://tempuri.org/")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[GeneratedCode("System.Xml", "2.0.50727.5420")]
	[Serializable]
	public class GroupMemberInfo
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

		public string NexonID
		{
			get
			{
				return this.nexonIDField;
			}
			set
			{
				this.nexonIDField = value;
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

		public long CharacterSN
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

		public DateTime dtLastLoginDate
		{
			get
			{
				return this.dtLastLoginDateField;
			}
			set
			{
				this.dtLastLoginDateField = value;
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

		private int guildSNField;

		private int nexonSNField;

		private string nexonIDField;

		private string nameInGroupField;

		private long characterSNField;

		private string introField;

		private DateTime dtLastLoginDateField;

		private GroupUserType emGroupUserTypeField;

		private string characterNameField;
	}
}
