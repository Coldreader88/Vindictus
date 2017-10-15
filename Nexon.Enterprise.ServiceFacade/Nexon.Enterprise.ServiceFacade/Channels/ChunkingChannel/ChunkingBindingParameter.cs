using System;
using System.Collections.Generic;

namespace Nexon.Enterprise.ServiceFacade.Channels.ChunkingChannel
{
	internal class ChunkingBindingParameter
	{
		internal ChunkingBindingParameter()
		{
			this.operationParams = new List<string>();
		}

		internal void AddAction(string action)
		{
			if (!this.operationParams.Contains(action))
			{
				this.operationParams.Add(action);
			}
		}

		internal ICollection<string> OperationParams
		{
			get
			{
				return this.operationParams;
			}
		}

		private ICollection<string> operationParams;
	}
}
