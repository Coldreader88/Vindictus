using System;
using System.Collections.Generic;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class DecomposeItemResultMessage : IMessage
	{
		public ICollection<string> GiveItemClassList { get; private set; }

		public DecomposeItemResultEXP ResultEXP { get; private set; }

		public DecomposeItemResultMessage(ICollection<string> giveItemClassList, DecomposeItemResultEXP resultEXP)
		{
			this.GiveItemClassList = giveItemClassList;
			this.ResultEXP = resultEXP;
		}

		public override string ToString()
		{
			return string.Format("DecomposeItemResultMessage[ GiveItem Count = {0}, ResultEXP = {1} ]", this.GiveItemClassList.Count, this.ResultEXP);
		}
	}
}
