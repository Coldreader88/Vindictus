using System;
using System.Collections.Generic;
using System.Text;

namespace ServiceCore.EndPointNetwork.Housing
{
	[Serializable]
	public sealed class HousingPartyInfoMessage : IMessage
	{
		public long HousingID { get; set; }

		public int PartySize { get; set; }

		public ICollection<HousingPartyMemberInfo> Members { get; set; }

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendFormat("HousingPartyInfoMessage[ HID = {0} ", this.HousingID).AppendLine();
			stringBuilder.AppendFormat("member({0}) = (\n", this.PartySize);
			foreach (HousingPartyMemberInfo arg in this.Members)
			{
				stringBuilder.AppendFormat("      {0}\n", arg);
			}
			stringBuilder.Append(")]");
			return stringBuilder.ToString();
		}
	}
}
