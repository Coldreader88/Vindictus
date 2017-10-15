using System;
using System.Diagnostics;

namespace Devcat.Core.Diagnostics.Contracts
{
	internal static class Contract
	{
		[Conditional("DEBUG")]
		internal static void Assert(bool condition)
		{
		}

		[Conditional("DEBUG")]
		internal static void Assert(bool condition, string message)
		{
		}

		[Conditional("DEBUG")]
		internal static void EndContractBlock()
		{
		}

		[Conditional("DEBUG")]
		internal static void Ensures(bool condition)
		{
		}
	}
}
