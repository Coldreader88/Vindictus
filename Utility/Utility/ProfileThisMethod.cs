using System;
using System.Diagnostics;

namespace Utility
{
	[Serializable]
	public class ProfileThisMethod 
	{
		public ProfileThisMethod(string tag)
		{
			this.tag = tag;
		}
        
        private string tag;

		//[NonSerialized]
		//private Stopwatch stopwatch;
	}
}
