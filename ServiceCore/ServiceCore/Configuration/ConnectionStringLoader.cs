using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using ServiceCore.HeroesContents;
using ServiceCore.Properties;
using Utility;

namespace ServiceCore.Configuration
{
	public class ConnectionStringLoader
	{
		static ConnectionStringLoader()
		{
			Log<ConnectionStringLoader>.Logger.Info("Testing Contents DB Connection...");
			HeroesContentsLoader.TestConnection();
		}

		public static void LoadFromServiceCore(ApplicationSettingsBase target)
		{
			try
			{
				Settings @default = Settings.Default;
				Type type = @default.GetType();
				Type type2 = target.GetType();
				Dictionary<string, string> dictionary = new Dictionary<string, string>();
				foreach (PropertyInfo propertyInfo in type.GetProperties())
				{
					object[] customAttributes = propertyInfo.GetCustomAttributes(typeof(SpecialSettingAttribute), false);
					int j = 0;
					while (j < customAttributes.Length)
					{
						SpecialSettingAttribute specialSettingAttribute = (SpecialSettingAttribute)customAttributes[j];
						if (specialSettingAttribute.SpecialSetting == SpecialSetting.ConnectionString)
						{
							string text = propertyInfo.GetValue(@default, null) as string;
							if (text != null)
							{
								dictionary.Add(propertyInfo.Name, StringEncrypter.Decrypt(text, propertyInfo.Name, false));
								@default[propertyInfo.Name] = dictionary[propertyInfo.Name];
								break;
							}
							Log<ConnectionStringLoader>.Logger.FatalFormat("Fail to read {0} in ServiceCore", propertyInfo.Name);
							break;
						}
						else
						{
							j++;
						}
					}
				}
				foreach (PropertyInfo propertyInfo2 in type2.GetProperties())
				{
					bool flag = false;
					object[] customAttributes2 = propertyInfo2.GetCustomAttributes(typeof(SpecialSettingAttribute), false);
					int l = 0;
					while (l < customAttributes2.Length)
					{
						SpecialSettingAttribute specialSettingAttribute2 = (SpecialSettingAttribute)customAttributes2[l];
						if (specialSettingAttribute2.SpecialSetting == SpecialSetting.ConnectionString)
						{
							flag = true;
							string text2 = dictionary.TryGetValue(propertyInfo2.Name);
							if (text2 != null)
							{
								target[propertyInfo2.Name] = text2;
								break;
							}
							Log<ConnectionStringLoader>.Logger.FatalFormat("No connectionstring defined in ServiceCore : {0}", propertyInfo2.Name);
							break;
						}
						else
						{
							l++;
						}
					}
					if (!flag && (propertyInfo2.GetCustomAttributes(typeof(ApplicationScopedSettingAttribute), false).FirstOrDefault<object>() != null || propertyInfo2.GetCustomAttributes(typeof(UserScopedSettingAttribute), false).FirstOrDefault<object>() != null))
					{
						Log<ConnectionStringLoader>.Logger.FatalFormat("Do NOT define Configuration Setting outside of ServiceCore : {0}", propertyInfo2.Name);
					}
				}
			}
			catch (Exception ex)
			{
				Log<ConnectionStringLoader>.Logger.Fatal(string.Format("Exception occurred while loading ConnectionStrings : {0}", target), ex);
			}
		}
	}
}
