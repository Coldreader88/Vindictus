using System;
using System.Runtime.InteropServices;

namespace AdminClientServiceCore.Messages
{
	[Guid("51149312-1124-4e04-8223-645C13692023")]
	[Serializable]
	public class AdminItemFestivalEventMessage2
	{
		public string ItemClass { get; set; }

		public int Amount { get; set; }

		public string Message { get; set; }

		public bool IsCafe { get; set; }

		public override string ToString()
		{
			return string.Format("AdminItemFestivalEventMessage2 [{0}x{1}]", this.ItemClass, this.Amount);
		}
	}
}
