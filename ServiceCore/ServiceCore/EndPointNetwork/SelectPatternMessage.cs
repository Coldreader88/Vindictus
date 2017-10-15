using System;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class SelectPatternMessage : IMessage
	{
		public int Pattern
		{
			get
			{
				return this.pattern;
			}
		}

		public SelectPatternMessage(int pattern)
		{
			this.pattern = pattern;
		}

		public override string ToString()
		{
			return string.Format("SelectPatternMessage[ pattern = {0} ]", this.pattern);
		}

		private int pattern;
	}
}
