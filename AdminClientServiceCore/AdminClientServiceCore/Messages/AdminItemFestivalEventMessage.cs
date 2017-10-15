using System;
using System.Runtime.InteropServices;

namespace AdminClientServiceCore.Messages
{
	[Guid("50C3BEF6-53F3-40b8-A2FF-536061416C28")]
	[Serializable]
	public class AdminItemFestivalEventMessage
	{
		public string ItemClass { get; set; }

		public int Amount { get; set; }

		public string Message { get; set; }

		public bool IsCafe { get; set; }

		public override string ToString()
		{
			return string.Format("AdminItemFestivalEventMessage [{0}x{1}]", this.ItemClass, this.Amount);
		}
	}
}
