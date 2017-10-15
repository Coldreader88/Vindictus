using System;
using System.Collections.Generic;
using MMOServer;

namespace HeroesChannelServer
{
	public class Map
	{
		public bool Vaild { get; private set; }

		public Map(string bsp, string filename)
		{
			this.bsp = bsp;
			this.file = new MapFile(filename);
			if (this.file.Load())
			{
				this.SetMapInfo(this.file.Partitions, this.file.VisibleLinks);
				this.Vaild = true;
				return;
			}
			this.Vaild = false;
		}

		private void SetMapInfo(HashSet<long> partitions, List<KeyValuePair<long, long>> visibleLinks)
		{
			this.partitions = partitions;
			this.visibleLinks = visibleLinks;
		}

		public bool Build(Channel channel)
		{
			if (this.partitions == null || this.visibleLinks == null || this.bsp == null)
			{
				return false;
			}
			channel.AddRegion(this.bsp);
			foreach (long id in this.partitions)
			{
				if (!channel.AddPartition(this.bsp, id))
				{
					return false;
				}
			}
			foreach (KeyValuePair<long, long> keyValuePair in this.visibleLinks)
			{
				if (!channel.AddLink(this.bsp, keyValuePair.Key, keyValuePair.Value))
				{
					return false;
				}
			}
			return true;
		}

		private string bsp;

		private MapFile file;

		private HashSet<long> partitions;

		private List<KeyValuePair<long, long>> visibleLinks;
	}
}
