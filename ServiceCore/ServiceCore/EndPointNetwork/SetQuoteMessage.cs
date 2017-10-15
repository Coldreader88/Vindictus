using System;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class SetQuoteMessage : IMessage
	{
		public string Quote
		{
			get
			{
				return this.quote;
			}
		}

		public SetQuoteMessage(string quote)
		{
			this.quote = quote;
		}

		public override string ToString()
		{
			return string.Format("SetQuoteMessage[ quote = {0} ]", this.quote);
		}

		private string quote;
	}
}
