using System;
using System.Collections.Generic;
using System.Text;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class PartyInfoMessage : IMessage
	{
		public long PartyID { get; set; }

		public int PartySize { get; set; }

		public PartyInfoState State { get; set; }

		public ICollection<PartyMemberInfo> Members { get; set; }

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendFormat("PartyInfoMessage[ PID = {0} ({1})", this.PartyID, this.State).AppendLine();
			stringBuilder.AppendFormat("Member({0}) = (\n", this.PartySize);
			foreach (PartyMemberInfo arg in this.Members)
			{
				stringBuilder.AppendFormat("    {0}\n", arg);
			}
			stringBuilder.Append(")]");
			return stringBuilder.ToString();
		}
	}
}
