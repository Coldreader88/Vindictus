using System;

namespace ExecutionSupporter
{
	public class Help : Attribute
	{
		public string Usage { get; set; }

		public string HelpText { get; set; }
	}
}
