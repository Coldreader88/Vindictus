using System;
using System.Collections.Generic;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class RequestAddPeerMessage : IMessage
	{
		public List<long> PingEntityIDs { get; set; }

		public RequestAddPeerMessage(List<long> pingEntityIDs)
		{
			this.PingEntityIDs = pingEntityIDs;
		}

		public override string ToString()
		{
			string text = "\n";
			foreach (long num in this.PingEntityIDs)
			{
				text += string.Format("{0}\n", num);
			}
			return string.Format("RequestAddPeerMessage [ Count:{0}, List:{1} ]", this.PingEntityIDs.Count, text);
		}
	}
}
