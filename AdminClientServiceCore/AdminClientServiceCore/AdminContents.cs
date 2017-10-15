using System;
using System.Collections.Generic;
using System.Linq;
using ServiceCore.HeroesContents;

namespace AdminClientServiceCore
{
	public static class AdminContents
	{
		public static Dictionary<string, EventTemplate> EventTemplate { get; set; }

		static AdminContents()
		{
			AdminContents.Load();
			HeroesContentsLoader.DB3Changed += AdminContents.Load;
		}

		public static void Initialize()
		{
		}

		private static void Load()
		{
			AdminContents.EventTemplate = HeroesContentsLoader.GetTable<EventTemplate>().ToDictionary((EventTemplate x) => x.Name);
		}

		public static EventTemplate GetTemplate(string name)
		{
			EventTemplate result;
			if (AdminContents.EventTemplate.TryGetValue(name, out result))
			{
				return result;
			}
			return null;
		}
	}
}
