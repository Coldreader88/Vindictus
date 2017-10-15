using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using ServiceCore.Configuration;
using ServiceCore.Properties;
using Utility;

namespace ServiceCore
{
	public class EventLoader
	{
		[MethodImpl(MethodImplOptions.Synchronized)]
		public static Dictionary<string, string> GetStartEventList()
		{
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			try
			{
				Settings @default = Settings.Default;
				Type type = @default.GetType();
				string connection = Settings.Default.heroesConnectionString;
				foreach (PropertyInfo propertyInfo in type.GetProperties())
				{
					bool flag = false;
					if (propertyInfo.Name.Equals("heroesConnectionString"))
					{
						string crypt = propertyInfo.GetValue(@default, null) as string;
						foreach (SpecialSettingAttribute specialSettingAttribute in propertyInfo.GetCustomAttributes(typeof(SpecialSettingAttribute), false))
						{
							if (specialSettingAttribute.SpecialSetting == SpecialSetting.ConnectionString)
							{
								connection = StringEncrypter.Decrypt(crypt, propertyInfo.Name, false);
								flag = true;
								break;
							}
						}
					}
					if (flag)
					{
						break;
					}
				}
				using (EventLoaderDataContext eventLoaderDataContext = new EventLoaderDataContext(connection))
				{
					DateTime now = DateTime.Now;
					List<EventGetResult> list = new List<EventGetResult>();
					ISingleResult<EventGetResult> singleResult = eventLoaderDataContext.EventGet();
					foreach (EventGetResult eventGetResult in singleResult)
					{
						if (eventGetResult.StartTime == null && !(eventGetResult.StartCount > 0))
						{
							if (eventGetResult.EndTime == null || now < eventGetResult.EndTime)
							{
								list.Add(eventGetResult);
							}
						}
						else if (eventGetResult.StartTime != null && now >= eventGetResult.StartTime && (eventGetResult.EndTime == null || now < eventGetResult.EndTime))
						{
							if (eventGetResult.PeriodBegin == null)
							{
								list.Add(eventGetResult);
							}
							else if (now.TimeOfDay >= eventGetResult.PeriodBegin && (eventGetResult.PeriodEnd == null || now.TimeOfDay < eventGetResult.PeriodEnd))
							{
								list.Add(eventGetResult);
							}
						}
					}
					foreach (EventGetResult eventGetResult2 in list)
					{
						if (eventGetResult2.Feature != null && eventGetResult2.Feature.Length > 0)
						{
							char[] separator = new char[]
							{
								';'
							};
							string[] array = eventGetResult2.Feature.Split(separator);
							for (int k = 0; k < array.Length; k++)
							{
								string text = array[k];
								string value = "0";
								int num = array[k].IndexOf('[');
								int num2 = array[k].IndexOf(']');
								if (num != -1 && num2 != -1 && num2 > num)
								{
									text = array[k].Substring(0, num);
									value = array[k].Substring(num + 1, num2 - num - 1);
								}
								if (text != null && text.Length > 0)
								{
									if (dictionary.ContainsKey(text))
									{
										dictionary[text] = value;
									}
									else
									{
										dictionary.Add(text, value);
									}
								}
							}
						}
					}
				}
			}
			catch (Exception ex)
			{
				Log<EventLoader>.Logger.Error("EventLoader load failed", ex);
				throw new InvalidOperationException("EventLoader load failed", ex);
			}
			return dictionary;
		}
	}
}
