using System;
using System.Configuration;

namespace ServiceCore.Configuration
{
	public class ChannelElement : ConfigurationElement
	{
		[IntegerValidator(MinValue = 0)]
		[ConfigurationProperty("capacity", IsRequired = true, DefaultValue = 0)]
		public int Capacity
		{
			get
			{
				return (int)base["capacity"];
			}
			set
			{
				base["capacity"] = value;
			}
		}
	}
}
