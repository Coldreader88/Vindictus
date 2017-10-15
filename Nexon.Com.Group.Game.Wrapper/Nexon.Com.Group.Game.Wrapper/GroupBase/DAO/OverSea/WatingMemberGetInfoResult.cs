using System;
using Nexon.Com.DAO;

namespace Nexon.Com.Group.Game.Wrapper.GroupBase.DAO.OverSea
{
	internal class WatingMemberGetInfoResult : SPResultBase
	{
		internal DateTime dtCreateDate { get; set; }

		internal string GuildName { get; set; }

		internal int GuildSN { get; set; }
	}
}
