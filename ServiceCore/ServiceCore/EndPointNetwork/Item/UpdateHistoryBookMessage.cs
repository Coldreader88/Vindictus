using System;
using System.Collections.Generic;

namespace ServiceCore.EndPointNetwork.Item
{
	[Serializable]
	public sealed class UpdateHistoryBookMessage : IMessage
	{
		public UpdateHistoryBookMessage.UpdateType Type { get; set; }

		public IList<string> HistoryBooks { get; set; }

		public UpdateHistoryBookMessage(UpdateHistoryBookMessage.UpdateType updatetype, IList<string> historyBooks)
		{
			this.Type = updatetype;
			this.HistoryBooks = historyBooks;
		}

		public override string ToString()
		{
			return string.Format("UpdateHistoryBookMessage [ Count: {0} ]", this.HistoryBooks.Count);
		}

		public enum UpdateType
		{
			Unknown,
			Full,
			Add,
			Remove
		}
	}
}
