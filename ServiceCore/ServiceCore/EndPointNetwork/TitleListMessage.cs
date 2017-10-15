using System;
using System.Collections.Generic;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class TitleListMessage : IMessage
	{
		public ICollection<TitleSlotInfo> AccountTitles { get; private set; }

		public ICollection<TitleSlotInfo> Titles { get; private set; }

		public TitleListMessage(ICollection<TitleSlotInfo> accountTitles, ICollection<TitleSlotInfo> titles)
		{
			this.AccountTitles = accountTitles;
			this.Titles = titles;
		}

		public override string ToString()
		{
			return string.Format("TitleListMessage [ titles x {0} accountTitles x {1}]", this.Titles.Count, this.AccountTitles.Count);
		}
	}
}
