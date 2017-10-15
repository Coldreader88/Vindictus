using System;
using System.Collections.Generic;
using System.IO;

namespace HeroesChannelServer
{
	internal class MapFile
	{
		private static int ReadInt(Stream stream, out bool eof)
		{
			eof = false;
			if (stream.Read(MapFile.buffer, 0, 4) == 0)
			{
				eof = true;
				return -1;
			}
			return BitConverter.ToInt32(MapFile.buffer, 0);
		}

		private static long ReadLong(Stream stream, out bool eof)
		{
			eof = false;
			if (stream.Read(MapFile.buffer, 0, 8) == 0)
			{
				eof = true;
				return -1L;
			}
			return BitConverter.ToInt64(MapFile.buffer, 0);
		}

		private static float ReadFloat(Stream stream, out bool eof)
		{
			eof = false;
			if (stream.Read(MapFile.buffer, 0, 4) == 0)
			{
				eof = true;
				return -1f;
			}
			return BitConverter.ToSingle(MapFile.buffer, 0);
		}

		public HashSet<long> Partitions
		{
			get
			{
				return this.partitions;
			}
		}

		public List<KeyValuePair<long, long>> VisibleLinks
		{
			get
			{
				return this.visibleLinks;
			}
		}

		public MapFile(string filename)
		{
			this.filename = filename;
		}

		public bool Load()
		{
			FileStream stream = new FileStream(this.filename, FileMode.Open, FileAccess.Read);
			default(MapFile.Header).Read(stream);
			bool flag;
			int num = MapFile.ReadInt(stream, out flag);
			if (flag)
			{
				return false;
			}
			for (int i = 0; i < num; i++)
			{
				long item = MapFile.ReadLong(stream, out flag);
				if (flag)
				{
					return false;
				}
				this.partitions.Add(item);
			}
			while (!flag)
			{
				long key = MapFile.ReadLong(stream, out flag);
				long value = MapFile.ReadLong(stream, out flag);
				if (flag)
				{
					return true;
				}
				this.visibleLinks.Add(new KeyValuePair<long, long>(key, value));
			}
			return true;
		}

		private string filename;

		private static byte[] buffer = new byte[256];

		private HashSet<long> partitions = new HashSet<long>();

		private List<KeyValuePair<long, long>> visibleLinks = new List<KeyValuePair<long, long>>();

		private struct Header
		{
			public bool Read(Stream stream)
			{
				bool flag;
				this.xTile = MapFile.ReadInt(stream, out flag);
				this.yTile = MapFile.ReadInt(stream, out flag);
				this.grid = MapFile.ReadFloat(stream, out flag);
				this.minX = MapFile.ReadFloat(stream, out flag);
				this.minY = MapFile.ReadFloat(stream, out flag);
				this.minZ = MapFile.ReadFloat(stream, out flag);
				return !flag;
			}

			private int xTile;

			private int yTile;

			private float grid;

			private float minX;

			private float minY;

			private float minZ;
		}
	}
}
