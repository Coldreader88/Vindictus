using System;

namespace Nexon.Enterprise.ServiceFacade.Channels.ChunkingChannel
{
	internal class ChunkState
	{
		internal ChunkState()
		{
			this.Buffer = new byte[4096];
			this.Count = 0;
		}

		internal int AppendBytes(byte[] buffer, int index, int count)
		{
			int num = Math.Min(count, this.Buffer.Length - this.Count);
			Array.Copy(buffer, index, this.Buffer, this.Count, num);
			this.Count += num;
			return count - num;
		}

		internal byte[] Buffer;

		internal int Count;
	}
}
