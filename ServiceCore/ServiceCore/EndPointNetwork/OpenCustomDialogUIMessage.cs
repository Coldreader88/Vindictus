using System;
using System.Collections.Generic;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class OpenCustomDialogUIMessage : IMessage
	{
		public int DialogType { get; set; }

		public List<string> Arg { get; set; }

		public override string ToString()
		{
			return string.Format("OpenCustomDialogUIMessage : {0}", this.DialogType);
		}

		public enum CustomDialogUITypes
		{
			Dialog_FriendRecommend = 1,
			Dialog_Give_FriendRecommendScroll,
			Dialog_Tircoin_Refresh,
			Dialog_RouletteReward_NotifyALL
		}
	}
}
