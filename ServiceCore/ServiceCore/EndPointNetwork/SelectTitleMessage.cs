using System;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class SelectTitleMessage : IMessage
	{
		public int Title
		{
			get
			{
				return this.title;
			}
		}

		public SelectTitleMessage(int title)
		{
			this.title = title;
		}

		public override string ToString()
		{
			return string.Format("SelectTitleMessage[ title = {0} ]", this.title);
		}

		private int title;
	}
}
