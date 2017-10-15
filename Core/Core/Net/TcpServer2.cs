using System;

namespace Devcat.Core.Net
{
	public class TcpServer2 : TcpServerBase<TcpClient2>
	{
		static TcpServer2()
		{
			TcpClient2.Init();
		}
	}
}
