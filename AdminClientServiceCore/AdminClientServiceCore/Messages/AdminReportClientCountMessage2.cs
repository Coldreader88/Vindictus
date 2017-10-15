using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace AdminClientServiceCore.Messages
{
	[Guid("299DE19B-087A-4fb0-9479-6DD94575746D")]
	[Serializable]
	public class AdminReportClientCountMessage2
	{
		public Dictionary<string, Dictionary<string, int>> UserCount { get; private set; }

		public AdminReportClientCountMessage2()
		{
			this.UserCount = new Dictionary<string, Dictionary<string, int>>();
		}

		public void AddUserCount(string name, IEnumerable<KeyValuePair<string, int>> userCountDict)
		{
			Dictionary<string, int> dictionary;
			if (this.UserCount.ContainsKey(name))
			{
				dictionary = this.UserCount[name];
			}
			else
			{
				dictionary = new Dictionary<string, int>();
				this.UserCount.Add(name, dictionary);
			}
			foreach (KeyValuePair<string, int> keyValuePair in userCountDict)
			{
				if (dictionary.ContainsKey(keyValuePair.Key))
				{
					Dictionary<string, int> dictionary2;
					string key;
					(dictionary2 = dictionary)[key = keyValuePair.Key] = dictionary2[key] + keyValuePair.Value;
				}
				else
				{
					dictionary.Add(keyValuePair.Key, keyValuePair.Value);
				}
			}
		}

		public override string ToString()
		{
			return string.Format("AdminReportClientCountMessage2", new object[0]);
		}
	}
}
