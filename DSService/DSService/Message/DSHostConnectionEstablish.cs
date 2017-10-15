using System;
using System.Runtime.InteropServices;

namespace DSService.Message
{
	[Guid("17F39A36-07A2-4CAA-96AF-1162C79120C3")]
	[Serializable]
	public class DSHostConnectionEstablish
	{
		public int DSID { get; set; }

		private DSHostConnectionEstablish(int pid, int dsid)
		{
			this.DSID = dsid;
		}

		public override string ToString()
		{
			return string.Format("EstablishDSHostConnection {0} ", this.DSID);
		}
	}
}
