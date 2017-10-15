using System;
using System.Runtime.InteropServices;

namespace AdminClientServiceCore.Messages
{
	[Guid("D6D6F234-51D3-4ec3-B0DC-5F87CBCD48D0")]
	[Serializable]
	public class AdminEntendCashItemExpire
	{
		public DateTime FromDate { get; set; }

		public int Minutes { get; set; }

		public override string ToString()
		{
			return string.Format("AdminModifyCashItemExpire", new object[0]);
		}
	}
}
