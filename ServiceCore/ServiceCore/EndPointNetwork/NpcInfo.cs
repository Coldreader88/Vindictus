using System;
using System.Collections.Generic;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class NpcInfo
	{
		public string NpcID { get; set; }

		public string Feature { get; set; }

		public ICollection<StoryInfo> StoryLines { get; set; }

		public NpcInfo(string NpcID, string Feature, ICollection<StoryInfo> StoryLines)
		{
			this.NpcID = NpcID;
			this.Feature = Feature;
			this.StoryLines = StoryLines;
		}

		public override string ToString()
		{
			return this.NpcID;
		}
	}
}
