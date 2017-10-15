using System;
using RemoteControlSystem;

namespace HeroesOpTool.RCUser
{
	public class Member
	{
		public string ID { get; private set; }

		public Authority Authority { get; set; }

		public Member(string id, Authority authority)
		{
			this.ID = id;
			this.Authority = authority;
		}
	}
}
