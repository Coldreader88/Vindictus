using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;

namespace Utility
{
	public static class PortScanner
	{
		private static void EndConnect(IAsyncResult ar)
		{
			PortScanner.State value = (ar.AsyncState as PortScanner.State?).Value;
			using (TcpClient client = value.Client)
			{
				if (!client.Connected)
				{
					lock (value.Ports)
					{
						value.Ports.Add(value.Port);
					}
				}
			}
		}

		public static int ScanOne(IPAddress address, IEnumerable<int> ports, int minPort)
		{
			foreach (int num in ports)
			{
				if (num > minPort)
				{
					return num;
				}
			}
			return 0;
		}

		private struct State
		{
			public TcpClient Client { get; private set; }

			public int Port { get; private set; }

			public ICollection<int> Ports { get; private set; }
		}
	}
}
