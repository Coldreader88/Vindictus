using System;
using System.Collections.Generic;
using System.Text;

namespace ServiceCore.EndPointNetwork.Housing
{
	[Serializable]
	public sealed class HousingRoomListMessage : IMessage
	{
		public ICollection<HousingRoomInfo> HousingRoomList { get; set; }

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder("HousingRoomListMessage [ (\n");
			stringBuilder.Append(")]");
			return stringBuilder.ToString();
		}
	}
}
