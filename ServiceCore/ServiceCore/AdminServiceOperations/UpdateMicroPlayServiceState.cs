using System;

namespace ServiceCore.AdminServiceOperations
{
	[Serializable]
	public sealed class UpdateMicroPlayServiceState
	{
		public int MicroPlayCount
		{
			get
			{
				return this.microPlayCount;
			}
		}

		public UpdateMicroPlayServiceState(int count)
		{
			this.microPlayCount = count;
		}

		private int microPlayCount;
	}
}
