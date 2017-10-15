using System;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class PvpReportMessage : IMessage
	{
		public int EventInt { get; set; }

		public int Subject { get; set; }

		public int Object { get; set; }

		public string Arg { get; set; }

		public PvpReportType Event
		{
			get
			{
				return (PvpReportType)this.EventInt;
			}
		}

		public override string ToString()
		{
			return string.Format("PvpReportMessage[ {0} : {1}->{2} : {3}]", new object[]
			{
				this.Event,
				this.Subject,
				this.Object,
				this.Arg
			});
		}
	}
}
