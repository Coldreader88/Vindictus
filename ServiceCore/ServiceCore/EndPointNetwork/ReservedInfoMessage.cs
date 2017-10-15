using System;
using System.Collections.Generic;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class ReservedInfoMessage : IMessage
	{
		public ICollection<string> ReservedName { get; set; }

		public ICollection<int> ReservedTitle { get; set; }

		public override string ToString()
		{
			return "ReservedInfoMessage";
		}
	}
}
