using System;

namespace MMOServer
{
	public class Location : IComponent
	{
		public long ID { get; set; }

		public string Category
		{
			get
			{
				return "Location";
			}
		}

		internal Partition CurPartition { get; set; }

		internal Partition NextPartition { get; set; }

		public bool IsTemporal
		{
			get
			{
				return false;
			}
		}

		public int SightDegree { get; set; }

		public IMessage AppearMessage()
		{
			return null;
		}

		public IMessage DifferenceMessage()
		{
			return null;
		}

		public IMessage DisappearMessage()
		{
			return new Disappeared(this.ID);
		}

		public void Flatten()
		{
			this.CurPartition = this.NextPartition;
		}

		public override string ToString()
		{
			return string.Format("Location {0}({1} -> {2})", this.ID, this.CurPartition, this.NextPartition);
		}
	}
}
