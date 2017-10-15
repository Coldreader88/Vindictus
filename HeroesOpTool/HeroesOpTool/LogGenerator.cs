using System;
using Devcat.Core;

namespace HeroesOpTool
{
	public class LogGenerator
	{
		public event EventHandler<EventArgs<string>> OnLog;

		public event EventHandler OnOpen;

		public event EventHandler OnClose;

		public string Key { get; private set; }

		public string Name { get; private set; }

		public LogGenerator(string key, string name)
		{
			this.Key = key;
			this.Name = name;
		}

		public void LogGenerated(object sender, string text)
		{
			if (this.OnLog != null)
			{
				this.OnLog(sender, new EventArgs<string>(text));
			}
		}

		public void Open()
		{
			if (this.OnOpen != null)
			{
				this.OnOpen(this, EventArgs.Empty);
			}
		}

		public void Close()
		{
			if (this.OnClose != null)
			{
				this.OnClose(this, EventArgs.Empty);
			}
		}
	}
}
