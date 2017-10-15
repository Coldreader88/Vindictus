using System;
using System.Collections.Generic;
using System.Linq;
using ServiceCore.HeroesContents;
using Utility;

namespace ServiceCore
{
	public static class ServiceCoreContents
	{
		public static Dictionary<string, Dictionary<string, string>> LocalizedTextDic { get; set; }

		public static List<LocalizedText> LocalizedText { get; set; }

		public static Dictionary<string, ServerConstants> ServerConstants { get; set; }

		static ServiceCoreContents()
		{
			ServiceCoreContents.Load();
		}

		private static void Load()
		{
			ServiceCoreContents.LocalizedTextDic = HeroesContentsLoader.GetTable<LocalizedText>().ToDoubleDictionary((LocalizedText x) => x.Lang.ToLower(), (LocalizedText x) => x.Key.ToLower(), (LocalizedText x) => x.Value);
			ServiceCoreContents.LocalizedText = HeroesContentsLoader.GetTable<LocalizedText>().ToList<LocalizedText>();
			ServiceCoreContents.ServerConstants = HeroesContentsLoader.GetTable<ServerConstants>().ToDictionary((ServerConstants x) => x.Property);
		}

		public static string Localize(string key, string lang)
		{
			string text = ServiceCoreContents.LocalizedTextDic.TryGetValue(lang.ToLower(), key.ToLower());
			if (string.IsNullOrEmpty(text))
			{
				Log<ServiceCore.HeroesContents.LocalizedText>.Logger.ErrorFormat(string.Format("Unable to find token [for key '{0}', lang '{1}']", key, lang), new object[0]);
				return "";
			}
			return text;
		}

		public static T GetServerConstant<T>(string name)
		{
			ServerConstants serverConstants;
			string type;
			if (ServiceCoreContents.ServerConstants.TryGetValue(name, out serverConstants) && (type = serverConstants.type) != null)
			{
				if (!(type == "string"))
				{
					if (!(type == "int"))
					{
						if (!(type == "long"))
						{
							if (!(type == "float"))
							{
								if (type == "double")
								{
									if (typeof(T) != typeof(double))
									{
										return default(T);
									}
									return (T)((object)double.Parse(serverConstants.Value));
								}
							}
							else
							{
								if (typeof(T) != typeof(float))
								{
									return default(T);
								}
								return (T)((object)float.Parse(serverConstants.Value));
							}
						}
						else
						{
							if (typeof(T) != typeof(long))
							{
								return default(T);
							}
							return (T)((object)long.Parse(serverConstants.Value));
						}
					}
					else
					{
						if (typeof(T) != typeof(int))
						{
							return default(T);
						}
						return (T)((object)int.Parse(serverConstants.Value));
					}
				}
				else
				{
					if (typeof(T) != typeof(string))
					{
						return default(T);
					}
					return (T)((object)serverConstants.Value);
				}
			}
			return default(T);
		}
	}
}
