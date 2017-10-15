using System;

namespace ServiceCore.AdminServiceOperations
{
	[Serializable]
	public sealed class UpdatePartyServiceState
	{
		public int PartyCount
		{
			get
			{
				return this.partyCount;
			}
		}

		public UpdatePartyServiceState(int count)
		{
			this.partyCount = count;
		}

		private int partyCount;
	}
}
