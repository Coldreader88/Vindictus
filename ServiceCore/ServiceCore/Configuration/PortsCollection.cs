using System;
using System.Configuration;

namespace ServiceCore.Configuration
{
	public sealed class PortsCollection : ConfigurationElementCollection
	{
		public PortsCollection()
		{
			this.Add(27015);
			this.Add(27005);
			this.Add(27011);
			this.Add(27020);
		}

		public void Add(int start)
		{
			this.BaseAdd(new PortElement
			{
				Start = start,
				Count = 1
			});
		}

		public void Add(int start, int count)
		{
			this.BaseAdd(new PortElement
			{
				Start = start,
				Count = count
			});
		}

		protected override ConfigurationElement CreateNewElement()
		{
			return new PortElement();
		}

		protected override object GetElementKey(ConfigurationElement element)
		{
			return element as PortElement;
		}
	}
}
