using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

namespace Nexon.Com.Web
{
	public class QueryString : Dictionary<string, string>
	{
		public QueryString()
		{
		}

		public QueryString(string QS)
		{
			Regex regex = new Regex("([^?&=]+)=([^&=]+)");
			foreach (object obj in regex.Matches(QS))
			{
				Match match = (Match)obj;
				this.Add(match.Groups[1].Value, HttpContext.Current.Server.UrlDecode(match.Groups[2].Value));
			}
		}

		public new string this[string Key]
		{
			get
			{
				if (!this.ContainsKey(Key))
				{
					return string.Empty;
				}
				return base[Key.ToLower()];
			}
			set
			{
				base[Key.ToLower()] = value;
			}
		}

		public new bool ContainsKey(string Key)
		{
			return base.ContainsKey(Key.ToLower());
		}

		public new void Add(string Key, string Value)
		{
			base.Add(Key.ToLower(), Value);
		}

		public new void Remove(string Key)
		{
			base.Remove(Key.ToLower());
		}

		public override string ToString()
		{
			return this.ToString(true);
		}

		public string ToString(bool IsUseEncode)
		{
			StringBuilder stringBuilder = new StringBuilder();
			foreach (KeyValuePair<string, string> keyValuePair in this)
			{
				stringBuilder.AppendFormat("&{0}={1}", keyValuePair.Key, IsUseEncode ? HttpContext.Current.Server.UrlEncode(keyValuePair.Value) : keyValuePair.Value);
			}
			if (stringBuilder.Length <= 1)
			{
				return string.Empty;
			}
			return stringBuilder.ToString(1, stringBuilder.Length - 1);
		}

		public string ToString(List<string> Keys)
		{
			return this.ToString(Keys, true);
		}

		public string ToString(List<string> Keys, bool IsUseEncode)
		{
			StringBuilder stringBuilder = new StringBuilder();
			foreach (string text in Keys)
			{
				if (this.ContainsKey(text))
				{
					stringBuilder.AppendFormat("&{0}={1}", text, IsUseEncode ? HttpContext.Current.Server.UrlEncode(this[text]) : this[text]);
				}
			}
			if (stringBuilder.Length <= 1)
			{
				return string.Empty;
			}
			return stringBuilder.ToString(1, stringBuilder.Length - 1);
		}
	}
}
