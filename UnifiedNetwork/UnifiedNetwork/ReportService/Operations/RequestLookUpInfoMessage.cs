using System;

namespace UnifiedNetwork.ReportService.Operations
{
	[Serializable]
	public sealed class RequestLookUpInfoMessage
	{
		public RequestLookUpInfoMessage.ServerPair Target
		{
			get
			{
				return this.target;
			}
		}

		public RequestLookUpInfoMessage() : this("ReportService", 65536)
		{
		}

		public RequestLookUpInfoMessage(string t) : this(t, 0)
		{
		}

		public RequestLookUpInfoMessage(int t) : this("", t)
		{
		}

		private RequestLookUpInfoMessage(string t, int k)
		{
			this.target.category = t;
			this.target.code = k;
		}

		public override string ToString()
		{
			return string.Format("RequestLookUpInfoMessage [ {0} / {1} ]", this.target.category, this.target.code);
		}

		private RequestLookUpInfoMessage.ServerPair target = new RequestLookUpInfoMessage.ServerPair();

		[Serializable]
		public sealed class ServerPair
		{
			public string category;

			public int code;
		}
	}
}
