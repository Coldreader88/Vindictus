using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Nexon.Com.Group.Game.Wrapper.HeroesGameGuild
{
	[XmlType(Namespace = "http://tempuri.org/")]
	[GeneratedCode("System.Xml", "2.0.50727.5420")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[Serializable]
	public class GroupInfo
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

		public DateTime dtCreateDate
		{
			get
			{
				return this.dtCreateDateField;
			}
			set
			{
				this.dtCreateDateField = value;
			}
		}

		public int CharacterSN_Master
		{
			get
			{
				return this.characterSN_MasterField;
			}
			set
			{
				this.characterSN_MasterField = value;
			}
		}

		private int guildSNField;

		private string guildNameField;

		private string introField;

		private string nameInGroup_MasterField;

		private string nexonID_MasterField;

		private string guildIDField;

		private int nexonSN_MasterField;

		private int realUserCountField;

		private DateTime dtCreateDateField;

		private int characterSN_MasterField;
	}
}
