using System;
using System.Collections.Generic;
using System.Reflection;

namespace Nexon.Com.Web
{
	public class UrlRequestParam : Url
	{
		public UrlRequestParam()
		{
			this.AddKeys();
		}

		public UrlRequestParam(bool isUseUrlEncode) : base(isUseUrlEncode)
		{
			this.AddKeys();
		}

		public UrlRequestParam(string url) : base(url)
		{
			this.AddKeys();
		}

		public UrlRequestParam(string url, bool isUseUrlEncode) : base(url, isUseUrlEncode)
		{
			this.AddKeys();
		}

		private void AddKeys()
		{
			this._type = base.GetType();
			this._properties = this._type.GetProperties();
			foreach (PropertyInfo propertyInfo in this._properties)
			{
				object[] customAttributes = propertyInfo.GetCustomAttributes(typeof(RequestParamAttribute), false);
				if (customAttributes.Length == 1)
				{
					RequestParamAttribute requestParamAttribute = customAttributes[0] as RequestParamAttribute;
					if (requestParamAttribute.Key != null && requestParamAttribute.Key != string.Empty && !this._keys.Contains(requestParamAttribute.Key))
					{
						this._keys.Add(requestParamAttribute.Key);
					}
				}
			}
		}

		public override string URL
		{
			get
			{
				string text = base.QueryStrings.ToString(this._keys, this.isUseUrlEncode);
				return string.Concat(new string[]
				{
					base.Scheme,
					"://",
					base.HttpHost,
					base.BaseFolder,
					base.PageName,
					(text.Length > 0) ? ("?" + text) : string.Empty
				});
			}
		}

		private List<string> _keys = new List<string>();

		private Type _type;

		private PropertyInfo[] _properties;
	}
}
