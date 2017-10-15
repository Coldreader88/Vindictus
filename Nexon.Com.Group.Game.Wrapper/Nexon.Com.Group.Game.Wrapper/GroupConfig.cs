using System;
using System.Configuration;

namespace Nexon.Com.Group.Game.Wrapper
{
	internal static class GroupConfig
	{
		static GroupConfig()
		{
			lock (GroupConfig._Configuator = new GroupConfigSection())
			{
				GroupConfig._Configuator = (ConfigurationManager.GetSection("Group.Config/GroupConfiguator") as GroupConfigSection);
			}
		}

		internal static GroupConfigSection GetSection()
		{
			return GroupConfig._Configuator;
		}

		private static GroupConfigSection _Configuator;
	}
}
