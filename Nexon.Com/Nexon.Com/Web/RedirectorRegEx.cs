using System;
using System.Text.RegularExpressions;

namespace Nexon.Com.Web
{
	internal class RedirectorRegEx
	{
		public int Length { get; private set; }

		public RedirectorRegEx(string strPattern, string strReplacePattern)
		{
			this._regex = new Regex(strPattern, RegexOptions.IgnoreCase);
			this._strPattern = strPattern;
			this._strReplacePattern = strReplacePattern;
		}

		public RedirectorRegEx(string protocol, string domain, string strPattern, string strReplacePattern) : this(strPattern, strReplacePattern)
		{
			this._protocol = protocol;
			this._domain = domain;
		}

		public bool Match(string strValue)
		{
			this.Length = this._regex.Match(strValue).Length;
			return this.Length > 0;
		}

		public bool Replace(string strValue, out string strReturn)
		{
			if (this._regex.Match(strValue).Length > 0)
			{
				this.Length = this._regex.Match(strValue).Length;
				strReturn = this._regex.Replace(strValue, delegate(Match m)
				{
					string text = this._strReplacePattern;
					for (int i = 0; i < m.Groups.Count; i++)
					{
						text = text.Replace("{" + i + "}", m.Groups[i + 1].ToString());
					}
					if (this._protocol != null && this._domain != null)
					{
						text = string.Format("{0}://{1}{2}", this._protocol, this._domain, text);
					}
					return text;
				});
				return true;
			}
			strReturn = strValue;
			return false;
		}

		private string _protocol;

		private string _domain;

		private Regex _regex;

		private string _strPattern;

		private string _strReplacePattern;
	}
}
