using System;
using System.Collections;
using System.Collections.Generic;

namespace Devcat.Core.Net
{
	public interface IPacketAnalyzer : IEnumerable<ArraySegment<byte>>, IEnumerable
	{
		ICryptoTransform CryptoTransform { get; set; }

		void Add(ArraySegment<byte> segment);

		event Action<int, string> OnHugeMessageReceive;

		event Action<string> WriteLog;
	}
}
