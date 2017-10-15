using System;
using System.Net;
using Devcat.Core.Threading;

namespace Devcat.Core.Net
{
	public interface ITcpServer
	{
		event EventHandler<AcceptEventArgs> ClientAccept;

		event EventHandler<EventArgs<Exception>> ExceptionOccur;

		Action<int, string> DoOnHugeMessage { get; set; }

		Action<string> WriteLogFunc { get; set; }

		object Tag { get; set; }

		IPEndPoint LocalEndPoint { get; }

		void Start(JobProcessor jobProcessor, int port);

		void Start(JobProcessor jobProcessor, IPAddress bindAddress, int port);

		void Start(JobProcessor jobProcessor, ServerBindType bindType, int port);

		void Stop();
	}
}
