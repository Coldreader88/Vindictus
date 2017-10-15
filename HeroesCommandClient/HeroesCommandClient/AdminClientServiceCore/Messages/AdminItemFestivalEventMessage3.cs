using System;
using System.Runtime.InteropServices;

namespace AdminClientServiceCore.Messages
{
	[Guid("59AF7B14-A700-4654-A4D3-1034C95FD920")]
	[Serializable]
	public class AdminItemFestivalEventMessage3
	{
		public string ItemClass { get; set; }

		public int Amount { get; set; }

		public string Message { get; set; }

		public bool IsCafe { get; set; }

		public bool IsExprire { get; set; }

		public DateTime? ExpireTime { get; set; }

		public override string ToString()
		{
			return string.Format("AdminItemFestivalEventMessage3 [{0}x{1}]", this.ItemClass, this.Amount);
		}
	}
}
