using System;
using System.Collections.Specialized;
using System.Configuration;

namespace ServiceCore.Configuration
{
	public class ServiceReporterSettings
	{
		public static int Get(string key, int defaultValue)
		{
			NameValueCollection nameValueCollection = ConfigurationManager.GetSection("ServiceReporterSettings") as NameValueCollection;
			if (nameValueCollection == null)
			{
				return defaultValue;
			}
			if (nameValueCollection.Get(key) != null)
			{
				return Convert.ToInt32(nameValueCollection[key]);
			}
			return defaultValue;
		}

		public static int GetInterval(string key, int defaultValue)
		{
			string name = key + ".Interval";
			NameValueCollection nameValueCollection = ConfigurationManager.GetSection("ServiceReporterSettings") as NameValueCollection;
			if (nameValueCollection == null)
			{
				return defaultValue;
			}
			if (nameValueCollection.Get(name) != null)
			{
				return Convert.ToInt32(nameValueCollection[name]);
			}
			string[] array = key.Split(new char[]
			{
				'.'
			}, StringSplitOptions.RemoveEmptyEntries);
			if (array.Length >= 2)
			{
				return ServiceReporterSettings.GetInterval(array[0], defaultValue);
			}
			return defaultValue;
		}
	}
}
