using System;

namespace Devcat.Core
{
	internal static class Environment2
	{
		internal static string GetResourceString(string key)
		{
			if (key == "AggregateException_ToString")
			{
				return "{0}{1}---> (Inner Exception #{2}) {3}{4}{5}";
			}
			return key;
		}
	}
}
