using System;
using System.Collections.Generic;

namespace Nexon.Com.Group.Game.Wrapper
{
	internal class Program
	{
		private static void Main(string[] args)
		{
			string str = string.Empty;
			try
			{
				int num;
				ICollection<GroupInfo> collection = HeroesGuild.GroupGetListByGuildName(5, 1, 1, 9, "M", out num);
				foreach (GroupInfo groupInfo in collection)
				{
					str += string.Format("GuildSN:{0}\r\nGuildName:{1}\r\n\r\n", groupInfo.GuildSN, groupInfo.GuildName);
				}
			}
			catch (Exception ex)
			{
				str = ex.Message;
			}
		}
	}
}
