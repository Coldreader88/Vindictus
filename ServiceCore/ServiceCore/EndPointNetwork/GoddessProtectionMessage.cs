using System;
using System.Collections.Generic;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class GoddessProtectionMessage : IMessage
	{
		public int Caster { get; set; }

		public List<int> Revived { get; set; }

		public override string ToString()
		{
			return string.Format("GoddessProtectionMessage", new object[0]);
		}
	}
}
