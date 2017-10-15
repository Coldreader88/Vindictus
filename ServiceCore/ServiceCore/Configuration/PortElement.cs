using System;
using System.Configuration;

namespace ServiceCore.Configuration
{
	public sealed class PortElement : ConfigurationElement
	{
		[ConfigurationProperty("start", IsRequired = true)]
		[IntegerValidator(MinValue = 0, MaxValue = 65535)]
		public int Start
		{
			get
			{
				return (int)base["start"];
			}
			set
			{
				base["start"] = value;
			}
		}

		[IntegerValidator(MinValue = 1, MaxValue = 65535)]
		[ConfigurationProperty("count", IsRequired = false, DefaultValue = 1)]
		public int Count
		{
			get
			{
				return (int)base["count"];
			}
			set
			{
				base["count"] = value;
			}
		}
	}
}
