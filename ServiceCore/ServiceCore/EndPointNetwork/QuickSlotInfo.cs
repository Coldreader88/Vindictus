using System;
using System.Collections.Generic;
using System.Text;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class QuickSlotInfo
	{
		public Dictionary<int, string> SlotItemClasses
		{
			get
			{
				return this.slotItemClasses;
			}
		}

		public QuickSlotInfo(Dictionary<int, string> classes)
		{
			this.slotItemClasses = classes;
		}

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder("QuickSlotInfo(");
			return stringBuilder.Append(")").ToString();
		}

		private Dictionary<int, string> slotItemClasses;
	}
}
