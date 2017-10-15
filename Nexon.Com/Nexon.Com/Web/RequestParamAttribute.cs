using System;

namespace Nexon.Com.Web
{
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
	public class RequestParamAttribute : Attribute
	{
		public RequestParamAttribute(string key)
		{
			this.Key = key;
		}

		public string Key;
	}
}
