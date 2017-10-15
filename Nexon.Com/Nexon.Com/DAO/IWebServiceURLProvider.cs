using System;

namespace Nexon.Com.DAO
{
	public interface IWebServiceURLProvider
	{
		string GetURL();

		string GetProxyIP();

		int GetProxyPort();
	}
}
