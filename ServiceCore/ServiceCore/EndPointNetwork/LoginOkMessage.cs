using System;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class LoginOkMessage : IMessage
	{
		public int RegionCode { get; set; }

		public int Time { get; set; }

		public int Limited { get; set; }

		public string FacebookToken { get; set; }

		public int UserCareType { get; set; }

		public int UserCareNextState { get; set; }

		public byte MapStateInfo { get; set; }

		public override string ToString()
		{
			return string.Format("LoginOkMessage[]", new object[0]);
		}
	}
}
