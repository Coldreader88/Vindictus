using System;
using System.IO;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

namespace Nexon.BlockParty.Utility
{
	public class HttpRequestUtility
	{
		public static string Get(string url)
		{
			return HttpRequestUtility.SendRequest(new HttpRequestUtility.RequestEntity(url, null, "GET", null, null, null));
		}

		public static string Get(HttpRequestUtility.RequestEntity req)
		{
			return HttpRequestUtility.SendRequest(req);
		}

		public static string Post(string url, string proxyIp, string values)
		{
			return HttpRequestUtility.SendRequest(new HttpRequestUtility.RequestEntity(url, proxyIp, "POST", "application/json", (values != null) ? Encoding.Default.GetBytes(values) : null, null));
		}

		public static string SendRequest(string url, string proxyIp, string requestType, string contentType, byte[] requestData)
		{
			return HttpRequestUtility.SendRequest(new HttpRequestUtility.RequestEntity(url, proxyIp, requestType, contentType, requestData, null));
		}

		public static string SendRequest(HttpRequestUtility.RequestEntity req)
		{
			HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(req.Url);
			httpWebRequest.Headers.Clear();
			if (!string.IsNullOrEmpty(req.ProxyIp))
			{
				httpWebRequest.Proxy = new WebProxy(req.ProxyIp);
			}
			if (req.Credential != null)
			{
				httpWebRequest.Credentials = req.Credential;
			}
			httpWebRequest.Method = req.RequestType;
			if (req.ContentType != null)
			{
				httpWebRequest.ContentType = req.ContentType;
			}
			if (req.RequestData != null)
			{
				httpWebRequest.ContentLength = (long)req.RequestData.Length;
				using (Stream requestStream = httpWebRequest.GetRequestStream())
				{
					requestStream.Write(req.RequestData, 0, req.RequestData.Length);
				}
			}
			string result;
			try
			{
				using (HttpWebResponse httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse())
				{
					using (Stream responseStream = httpWebResponse.GetResponseStream())
					{
						using (StreamReader streamReader = new StreamReader(responseStream))
						{
							result = streamReader.ReadToEnd();
						}
					}
				}
			}
			catch (WebException ex)
			{
				using (Stream responseStream2 = ex.Response.GetResponseStream())
				{
					using (StreamReader streamReader2 = new StreamReader(responseStream2))
					{
						result = streamReader2.ReadToEnd();
					}
				}
			}
			return result;
		}

		public static string SetQueryStringPair(string url, string name, string value)
		{
			Uri uri = new Uri(url);
			string leftPart = uri.GetLeftPart(UriPartial.Path);
			string text = uri.Query;
			if (!string.IsNullOrEmpty(text) && Regex.IsMatch(text, string.Format("((?<=[\\?&]){0}=[^&]*)", name)))
			{
				text = Regex.Replace(text, string.Format("((?<=[\\?&]){0}=[^&]*)", name), name + "=" + value);
			}
			else
			{
				string text2 = text;
				text = string.Concat(new string[]
				{
					text2,
					(text.Length > 0) ? "&" : "?",
					name,
					"=",
					value
				});
			}
			return leftPart + text;
		}

		public static string ExtractBody(HttpContext context)
		{
			int num = 0;
			StringBuilder stringBuilder = new StringBuilder();
			while (context.Request.InputStream.CanRead)
			{
				byte[] array = new byte[4096];
				int num2 = context.Request.InputStream.Read(array, 0, 4096);
				if (num2 <= 0)
				{
					break;
				}
				string @string = Encoding.ASCII.GetString(array, 0, num2);
				stringBuilder.Append(@string);
				num += num2;
			}
			return stringBuilder.ToString();
		}

		public const string DEFAULT_POST_CONTENT_TYPE = "application/json";

		private const string FORMAT_REGEX_QUERYSTRING_PAIR = "((?<=[\\?&]){0}=[^&]*)";

		public class RequestEntity
		{
			public RequestEntity()
			{
			}

			public RequestEntity(string url)
			{
				this._url = url;
			}

			public RequestEntity(string url, NetworkCredential credential)
			{
				this._url = url;
				this._credential = credential;
			}

			public RequestEntity(string url, byte[] requestData)
			{
				this._url = url;
				this._requestData = requestData;
			}

			public RequestEntity(string url, string requestData) : this(url, Encoding.UTF8.GetBytes(requestData))
			{
			}

			public RequestEntity(string url, byte[] requestData, NetworkCredential credential)
			{
				this._url = url;
				this._requestData = requestData;
				this._credential = credential;
			}

			public RequestEntity(string url, string requestData, NetworkCredential credential) : this(url, Encoding.UTF8.GetBytes(requestData), credential)
			{
			}

			public RequestEntity(string url, string proxyIp, string requestType, string contentType, byte[] requestData, NetworkCredential credential)
			{
				this._url = url;
				this._proxyIp = proxyIp;
				this._requestType = requestType;
				this._contentType = contentType;
				this._requestData = requestData;
				this._credential = credential;
			}

			public string Url
			{
				get
				{
					return this._url;
				}
				set
				{
					this._url = value;
				}
			}

			public string ProxyIp
			{
				get
				{
					return this._proxyIp;
				}
				set
				{
					this._proxyIp = value;
				}
			}

			public string RequestType
			{
				get
				{
					return this._requestType;
				}
				set
				{
					this._requestType = value;
				}
			}

			public string ContentType
			{
				get
				{
					return this._contentType;
				}
				set
				{
					this._contentType = value;
				}
			}

			public byte[] RequestData
			{
				get
				{
					return this._requestData;
				}
				set
				{
					this._requestData = value;
				}
			}

			public NetworkCredential Credential
			{
				get
				{
					return this._credential;
				}
				set
				{
					this._credential = value;
				}
			}

			private string _url;

			private string _proxyIp;

			private string _requestType = "GET";

			private string _contentType;

			private byte[] _requestData;

			private NetworkCredential _credential;
		}
	}
}
