using System;
using Nexon.CafeAuth;

namespace CafeAuthServiceCore
{
	public class CafeAuthResult
	{
		public AuthorizeResult Result;

		public Option Option;

		public long SessionNo;

		public int CafeLevel;

		public bool IsShutDownEnabled;

		public bool ReloginRequired;

		public int PCRoomNo;
	}
}
