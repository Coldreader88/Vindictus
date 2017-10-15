using System;
using System.Collections.Generic;

namespace ServiceCore.EndPointNetwork.Housing
{
	[Serializable]
	public sealed class HousingGameHostedMessage : IMessage
	{
		public string Map { get; set; }

		public bool IsOwner { get; set; }

		public List<HousingPropInfo> HousingProps { get; set; }

		public GameJoinMemberInfo HostInfo { get; set; }

		public HousingGameHostedMessage(string map, bool isOwner, List<HousingPropInfo> housingProps, GameJoinMemberInfo hostInfo)
		{
			this.Map = map;
			this.IsOwner = isOwner;
			this.HousingProps = housingProps;
			this.HostInfo = hostInfo;
		}
	}
}
