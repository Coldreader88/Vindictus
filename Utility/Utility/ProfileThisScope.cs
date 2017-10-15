using System;
using System.Diagnostics;

namespace Utility
{
	public class ProfileThisScope : IDisposable
	{
		public ProfileThisScope(string tag)
		{
			this.tag = tag;
			this.stopwatch = new Stopwatch();
			this.stopwatch.Restart();
		}

		public void Dispose()
		{
			this.stopwatch.Stop();
			ProfileScope.Update(this.tag, this.stopwatch.Elapsed.TotalMilliseconds);
		}

		private string tag;

		private Stopwatch stopwatch;
	}
}
