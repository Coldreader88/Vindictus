using System;
using System.Runtime.InteropServices;

namespace RemoteControlSystem.ClientMessage
{
	[Guid("586802E1-2243-4cdb-973E-7A636435AE46")]
	[Serializable]
	public sealed class TemplateChangeMessage
	{
		public RCProcessCollection Template { get; private set; }

		public TemplateChangeMessage(RCProcessCollection template)
		{
			this.Template = template;
		}
	}
}
