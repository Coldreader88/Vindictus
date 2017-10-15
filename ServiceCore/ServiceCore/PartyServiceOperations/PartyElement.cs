using System;
using System.Collections.Generic;
using ServiceCore.EndPointNetwork;

namespace ServiceCore.PartyServiceOperations
{
	[Serializable]
	public sealed class PartyElement
	{
		public long PartyID { get; private set; }

		public string Title { get; private set; }

		public long Master { get; private set; }

		public byte State { get; private set; }

		public ICollection<UserInfo> Members { get; private set; }

		public int RestTime { get; private set; }

		public PartyElement(long pid, string t, long u, byte s, ICollection<UserInfo> m, int res)
		{
			this.PartyID = pid;
			this.Title = t;
			this.Master = u;
			this.State = s;
			this.Members = m;
			this.RestTime = res;
		}

		public override string ToString()
		{
			return string.Format("{0}({1}) : {3}명 / 파티장 {2}", new object[]
			{
				this.Title,
				this.State,
				this.Master,
				this.Members.Count
			});
		}
	}
}
