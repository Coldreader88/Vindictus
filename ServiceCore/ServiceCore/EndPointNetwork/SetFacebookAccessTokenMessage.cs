using System;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class SetFacebookAccessTokenMessage : IMessage
	{
		public string AccessToken { get; set; }

		public SetFacebookAccessTokenMessage(string token)
		{
			this.AccessToken = token;
		}

		public override string ToString()
		{
			return string.Format("SetFacebookAccessTokenMessage [ ]", new object[0]);
		}
	}
}
