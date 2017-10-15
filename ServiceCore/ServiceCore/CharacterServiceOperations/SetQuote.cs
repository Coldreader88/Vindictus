using System;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.CharacterServiceOperations
{
	[Serializable]
	public sealed class SetQuote : Operation
	{
		public string Quote
		{
			get
			{
				return this.quote;
			}
			set
			{
				this.quote = value;
			}
		}

		public SetQuote(string quote)
		{
			this.quote = quote;
		}

		public override OperationProcessor RequestProcessor()
		{
			return new OkOrFailProcessor(this);
		}

		private string quote;
	}
}
