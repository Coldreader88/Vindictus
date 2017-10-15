using System;
using System.ComponentModel;
using Utility;

namespace HeroesServerHost
{
	[RunInstaller(true)]
	public class HeroesServiceInstaller : ServiceInstallerBase
	{
		public HeroesServiceInstaller() : base("HeroesServer")
		{
		}
	}
}
