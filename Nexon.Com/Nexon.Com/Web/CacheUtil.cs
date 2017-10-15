using System;
using System.Web;
using System.Web.Caching;

namespace Nexon.Com.Web
{
	public class CacheUtil
	{
		public static void Insert(string strCacheName, object objValue, int n4CachingMinute)
		{
			Random random = new Random();
			int num = random.Next() % 20 - 10;
			DateTime absoluteExpiration = DateTime.Now.AddSeconds((double)(n4CachingMinute * 60 + num));
			HttpContext.Current.Cache.Insert(strCacheName, objValue, null, absoluteExpiration, Cache.NoSlidingExpiration);
		}

		public static void Insert(string strCacheName, object objValue, DateTime dateExpire)
		{
			HttpContext.Current.Cache.Insert(strCacheName, objValue, null, dateExpire, Cache.NoSlidingExpiration);
		}

		public static object Get(string strCacheName)
		{
			return HttpContext.Current.Cache.Get(strCacheName);
		}

		public static object Remove(string strCacheName)
		{
			return HttpContext.Current.Cache.Remove(strCacheName);
		}
	}
}
