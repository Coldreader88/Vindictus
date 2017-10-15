using System;
using System.Collections.Generic;
using System.Text;
using System.Web;

namespace Nexon.Com.Web
{
	public class Url
	{
		public string HttpHost { get; set; }

		public string BaseFolder { get; set; }

		public string PageName { get; set; }

		public string Scheme { get; set; }

		public QueryString QueryStrings { get; private set; }

		public string this[string key]
		{
			get
			{
				if (this.QueryStrings.ContainsKey(key))
				{
					return this.QueryStrings[key];
				}
				return string.Empty;
			}
			set
			{
				if (!this.QueryStrings.ContainsKey(key))
				{
					if (value != null)
					{
						this.QueryStrings.Add(key, value);
					}
					return;
				}
				if (value == null)
				{
					this.QueryStrings.Remove(key);
					return;
				}
				this.QueryStrings[key] = value;
			}
		}

		public virtual string URL
		{
			get
			{
				string text = this.QueryStrings.ToString(this.isUseUrlEncode);
				return string.Concat(new string[]
				{
					this.Scheme,
					"://",
					this.HttpHost,
					this.BaseFolder,
					this.PageName,
					(text.Length > 0) ? ("?" + text) : string.Empty
				});
			}
		}

		public Url(bool isUseUrlEncode)
		{
			this._uri = new Uri(HttpContext.Current.Request.Url.Scheme + "://" + HttpContext.Current.Request.ServerVariables["HTTP_HOST"] + HttpContext.Current.Request.RawUrl);
			this.isUseUrlEncode = isUseUrlEncode;
			this.Init();
		}

		public Url(string strUrl, bool isUseUrlEncode)
		{
			this._uri = new Uri(strUrl);
			this.isUseUrlEncode = isUseUrlEncode;
			this.Init();
		}

		public Url() : this(true)
		{
		}

		public Url(string strUrl) : this(strUrl, true)
		{
		}

		public void Init()
		{
			this.Scheme = this._uri.Scheme;
			this.HttpHost = this._uri.Host + ((this._uri.Port == 80) ? "" : (":" + this._uri.Port));
			this.BaseFolder = this._uri.AbsolutePath.Substring(0, this._uri.AbsolutePath.LastIndexOf("/") + 1);
			this.PageName = this._uri.AbsolutePath.Substring(this._uri.AbsolutePath.LastIndexOf("/") + 1);
			this.QueryStrings = new QueryString(this._uri.OriginalString);
		}

		public string GetDebugInfo()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendFormat("{0} = {1}\r\n", "AbsolutePath", this._uri.AbsolutePath);
			stringBuilder.AppendFormat("{0} = {1}\r\n", "AbsoluteUri", this._uri.AbsoluteUri);
			stringBuilder.AppendFormat("{0} = {1}\r\n", "Authority", this._uri.Authority);
			stringBuilder.AppendFormat("{0} = {1}\r\n", "DnsSafeHost", this._uri.DnsSafeHost);
			stringBuilder.AppendFormat("{0} = {1}\r\n", "Fragment", this._uri.Fragment);
			stringBuilder.AppendFormat("{0} = {1}\r\n", "Host", this._uri.Host);
			stringBuilder.AppendFormat("{0} = {1}\r\n", "HostNameType", this._uri.HostNameType);
			stringBuilder.AppendFormat("{0} = {1}\r\n", "IsAbsoluteUri", this._uri.IsAbsoluteUri);
			stringBuilder.AppendFormat("{0} = {1}\r\n", "IsDefaultPort", this._uri.IsDefaultPort);
			stringBuilder.AppendFormat("{0} = {1}\r\n", "IsFile", this._uri.IsFile);
			stringBuilder.AppendFormat("{0} = {1}\r\n", "IsLoopback", this._uri.IsLoopback);
			stringBuilder.AppendFormat("{0} = {1}\r\n", "IsUnc", this._uri.IsUnc);
			stringBuilder.AppendFormat("{0} = {1}\r\n", "LocalPath", this._uri.LocalPath);
			stringBuilder.AppendFormat("{0} = {1}\r\n", "OriginalString", this._uri.OriginalString);
			stringBuilder.AppendFormat("{0} = {1}\r\n", "PathAndQuery", this._uri.PathAndQuery);
			stringBuilder.AppendFormat("{0} = {1}\r\n", "Port", this._uri.Port);
			stringBuilder.AppendFormat("{0} = {1}\r\n", "Query", this._uri.Query);
			stringBuilder.AppendFormat("{0} = {1}\r\n", "Scheme", this._uri.Scheme);
			stringBuilder.AppendFormat("{0} = {1}\r\n", "Segments", this._uri.Segments);
			stringBuilder.AppendFormat("{0} = {1}\r\n", "UserEscaped", this._uri.UserEscaped);
			stringBuilder.AppendFormat("{0} = {1}\r\n", "UserInfo", this._uri.UserInfo);
			stringBuilder.AppendLine();
			stringBuilder.AppendFormat("{0} = {1}\r\n", "BaseFolder", this.BaseFolder);
			stringBuilder.AppendFormat("{0} = {1}\r\n", "PageName", this.PageName);
			if (this.QueryStrings != null)
			{
				foreach (KeyValuePair<string, string> keyValuePair in this.QueryStrings)
				{
					stringBuilder.AppendFormat("{0} = {1}\r\n", "QueryStrings." + keyValuePair.Key, keyValuePair.Value);
				}
			}
			return stringBuilder.ToString();
		}

		private Uri _uri;

		protected bool isUseUrlEncode = true;
	}
}
