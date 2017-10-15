using System;

namespace MMOServer
{
	internal class MovePartition : IModifier
	{
		public long ID
		{
			get
			{
				return this.targetID;
			}
		}

		public long TargetID
		{
			get
			{
				return this.targetID;
			}
		}

		public string Category
		{
			get
			{
				return "Move";
			}
		}

		public IComponentSpace Space { get; set; }

		public Partition TargetPartition { get; set; }

		internal MovePartition(long targetID, Partition targetPartition)
		{
			this.targetID = targetID;
			this.TargetPartition = targetPartition;
		}

		public void Apply()
		{
			if (this.Space == null)
			{
				return;
			}
			Location location = this.Space.Find(this.targetID, "Location") as Location;
			if (location == null || location.NextPartition == this.TargetPartition)
			{
				return;
			}
			location.NextPartition = this.TargetPartition;
			this.Space.NotifyModified(location);
		}

		public override string ToString()
		{
			return string.Format("Move({0} -> {1})", this.targetID, this.TargetPartition);
		}

		private long targetID;
	}
}
