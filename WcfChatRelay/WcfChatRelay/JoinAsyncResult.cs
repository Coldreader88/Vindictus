using System;

namespace WcfChatRelay
{
	internal class JoinAsyncResult : AsyncResult
	{
		public long GuildKey { get; set; }

		public long CID { get; set; }

		public string Sender { get; set; }

		public string Result { get; set; }

		public JoinAsyncResult(AsyncCallback callback, object state) : base(callback, state)
		{
		}
	}
}
