using System;
using System.Collections.Generic;
using ServiceCore.EndPointNetwork.Housing;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.MicroPlayServiceOperations
{
	[Serializable]
	public sealed class SaveHousingProps : Operation
	{
		public List<HousingPropInfo> PropList { get; set; }

		public SaveHousingProps(List<HousingPropInfo> PropList)
		{
			this.PropList = PropList;
		}

		public override OperationProcessor RequestProcessor()
		{
			return new OkOrFailProcessor(this);
		}
	}
}
