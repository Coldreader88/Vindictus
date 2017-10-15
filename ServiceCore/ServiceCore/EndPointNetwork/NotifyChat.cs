using System;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class NotifyChat : IMessage
	{
		public string Name { get; set; }

		public string Message { get; set; }

		public override string ToString()
		{
			return string.Format("NotifyChat {0} : {1}", this.Name, this.Message);
		}
	}
}
