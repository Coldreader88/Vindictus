using System;
using System.Net;

namespace ExecutionSupporter
{
	public static class Extension
	{
		public static string ToSizedString(this object obj, int size)
		{
			string text = obj.ToString();
			int num = size - text.Length;
			if (num > 0)
			{
				return string.Format("{0}{1}", new string(' ', num), text);
			}
			return text;
		}

		public static string GetHostName(this IPAddress address)
		{
			string result;
			try
			{
				IPHostEntry hostEntry = Dns.GetHostEntry(address);
				result = hostEntry.HostName.Split(new char[]
				{
					'.'
				})[0].ToUpper();
			}
			catch
			{
				result = address.ToString();
			}
			return result;
		}
	}
}
