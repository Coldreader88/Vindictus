using System;

namespace UnifiedNetwork.PipedNetwork
{
	[Serializable]
	internal sealed class OpenPipe
	{
		public int PipeID { get; private set; }

		public OpenPipe()
		{
		}

		public OpenPipe(int pipeID)
		{
			this.PipeID = pipeID;
		}
	}
}
