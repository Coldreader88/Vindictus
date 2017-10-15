using System;
using System.Collections.Generic;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class UpdateTitleMessage : IMessage
	{
		public ICollection<TitleSlotInfo> Titles { get; private set; }

		public UpdateTitleMessage(ICollection<TitleSlotInfo> titles)
		{
			this.Titles = titles;
		}

		public override string ToString()
		{
			return string.Format("UpdateTitleMessage [ titles x {0} ]", this.Titles.Count);
		}
	}
}
