using System;
using System.Collections.Generic;

namespace ServiceCore.EndPointNetwork.Housing
{
	[Serializable]
	public sealed class UpdateHousingPropsMessage : IMessage
	{
		public List<HousingPropInfo> PropList { get; set; }

		public UpdateHousingPropsMessage(List<HousingPropInfo> PropList)
		{
			this.PropList = PropList;
		}

		public override string ToString()
		{
			return string.Format("UpdateHousingPropsMessage - Count [{0}]", this.PropList.Count);
		}
	}
}
