using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;

namespace Utility
{
	public static class Network
	{
		public static IEnumerable<IPAddress> GetIPAddresses()
		{
			string hostName = Dns.GetHostName();
			if (hostName != null)
			{
				IPHostEntry hostEntry = Dns.GetHostEntry(hostName);
				if (hostEntry != null)
				{
					return from x in hostEntry.AddressList
					where x.AddressFamily == AddressFamily.InterNetwork
					select x;
				}
			}
			return null;
		}

		public static IPAddress GetIPAddress()
		{
			IEnumerable<IPAddress> ipaddresses = Network.GetIPAddresses();
			if (ipaddresses != null && ipaddresses.Count<IPAddress>() > 0)
			{
				return ipaddresses.First<IPAddress>();
			}
			return null;
		}

		public static IEnumerable<IPAddress> GetPrivateIPAddresses()
		{
			IEnumerable<IPAddress> ipaddresses = Network.GetIPAddresses();
			if (ipaddresses != null)
			{
				return from x in ipaddresses
				where x.IsPrivateNetwork()
				select x;
			}
			return null;
		}

		public static IPAddress GetPrivateIPAddress()
		{
			IEnumerable<IPAddress> privateIPAddresses = Network.GetPrivateIPAddresses();
			if (privateIPAddresses != null && privateIPAddresses.Count<IPAddress>() > 0)
			{
				return privateIPAddresses.First<IPAddress>();
			}
			return null;
		}
	}
}
