using System;
using System.Collections.Generic;
using System.Text;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class EquipmentInfoMessage : IMessage
	{
		public IDictionary<int, long> EquipInfos { get; private set; }

		public EquipmentInfoMessage(IDictionary<int, long> infos)
		{
			this.EquipInfos = infos;
		}

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder("EquipmentInfoMessage [ equipInfos = ");
			stringBuilder.Append(" ]");
			return stringBuilder.ToString();
		}
	}
}
