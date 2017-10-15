using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace Utility
{
	public static class ProfileScope
	{
		public static ICollection<ProfileScopeData> DataList
		{
			get
			{
				return ProfileScope.dataDic.Values;
			}
		}

		static ProfileScope()
		{
			ProfileScope.Clear();
		}

		internal static void Update(string tag, double duration)
		{
			if (ProfileScope.dataDic == null)
			{
				return;
			}
			ProfileScopeData orAdd = ProfileScope.dataDic.GetOrAdd(tag, new ProfileScopeData(tag));
			if (orAdd != null)
			{
				orAdd.Update(duration);
			}
		}

		public static void Clear()
		{
			ProfileScope.dataDic = new ConcurrentDictionary<string, ProfileScopeData>();
		}

		private static ConcurrentDictionary<string, ProfileScopeData> dataDic;
	}
}
