using System;
using System.CodeDom.Compiler;
using System.Xml.Serialization;

namespace Nexon.Com.Group.Game.Wrapper.HeroesGameGuild
{
	[XmlType(Namespace = "http://tempuri.org/")]
	[GeneratedCode("System.Xml", "2.0.50727.5420")]
	[Serializable]
	public enum GroupUserType
	{
		alliedMember,
		associate_CSO,
		bookmarkedUser,
		deniedUser,
		guest,
		guildLeader,
		guildMember,
		master,
		member_lv1,
		member_lv2,
		member_lv3,
		member_lv4,
		member_lv5,
		memberSeceded,
		memberWaiting,
		rejectedUser,
		sysop,
		unknown,
		webmember,
		webmemberWaiting
	}
}
