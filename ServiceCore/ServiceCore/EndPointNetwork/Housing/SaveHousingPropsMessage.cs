using System;
using System.Collections.Generic;

namespace ServiceCore.EndPointNetwork.Housing
{
	[Serializable]
	public sealed class SaveHousingPropsMessage : IMessage
	{
		public List<HousingPropInfo> PropList { get; set; }

		public override string ToString()
		{
			return string.Format("SaveHousingPropsMessage - Count [{0}]", this.PropList.Count);
		}
	}
}
