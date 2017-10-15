using System;

namespace UnifiedNetwork.ReportService.Operations
{
	[Serializable]
	public sealed class RequestSelectMessage
	{
		public string category { get; set; }

		public long entityID { get; set; }

		public RequestSelectMessage()
		{
		}

		public RequestSelectMessage(string c, long e)
		{
			this.category = c;
			this.entityID = e;
		}

		public override string ToString()
		{
			return string.Format("RequestSelectMessage[{0},{1}]", this.category, this.entityID);
		}
	}
}
