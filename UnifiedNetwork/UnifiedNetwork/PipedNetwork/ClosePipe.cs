using System;

namespace UnifiedNetwork.PipedNetwork
{
	[Serializable]
	internal sealed class ClosePipe
	{
		public int PipeID { get; private set; }

		public ClosePipe()
		{
		}

		public ClosePipe(int pipeID)
		{
			this.PipeID = pipeID;
		}
	}
}
