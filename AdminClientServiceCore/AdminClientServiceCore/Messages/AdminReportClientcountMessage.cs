using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace AdminClientServiceCore.Messages
{
	[Guid("8ACE85A9-F74D-438f-AD60-396F3EC93C3C")]
	[Serializable]
	public class AdminReportClientcountMessage
	{
		public int Value { get; set; }

		public int Total { get; set; }

		public int Waiting { get; set; }

		public Dictionary<string, int> States { get; set; }

		public override string ToString()
		{
			return string.Format("AdminReportClientcountMessage [{0}/{1}/{2}]", this.Value, this.Total, this.Waiting);
		}
	}
}
