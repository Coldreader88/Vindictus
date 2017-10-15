using System;
using ServiceCore.DSServiceOperations;

namespace DSService.DSEntityMaker
{
	public class DSEntityInfo
	{
		public DSEntityState State { get; set; }

		public DSType DSType { get; set; }

		public int TargetPvpServiceID { get; set; }

		public DSEntityInfo()
		{
			this.State = DSEntityState.UnUsed;
			this.DSType = DSType.None;
		}

		public void Update(DSEntityState state, DSType dsType)
		{
			this.State = state;
			this.DSType = dsType;
		}

		public void Reset()
		{
			this.State = DSEntityState.UnUsed;
			this.DSType = DSType.None;
		}

		public override string ToString()
		{
			return "\tState : " + this.State.ToString() + "\tType : " + this.DSType.ToString();
		}
	}
}
