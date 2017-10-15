using System;
using System.Data.SqlClient;
using System.Threading;

namespace Nexon.Com.DAO
{
	internal class AsyncSPParam
	{
		internal AsyncSPParam(int index, SqlCommand cmd, AutoResetEvent handel)
		{
			this.index = index;
			this.cmd = cmd;
			this.handel = handel;
		}

		internal int index;

		internal SqlCommand cmd;

		internal AutoResetEvent handel;
	}
}
