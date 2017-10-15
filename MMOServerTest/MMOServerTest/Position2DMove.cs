using System;
using MMOServer;

namespace MMOServerTest
{
	internal class Position2DMove : IModifier
	{
		public long ID { get; set; }

		public string Category
		{
			get
			{
				return "Position2DMove";
			}
		}

		public IComponentSpace Space { get; set; }

		public int TargetX { get; set; }

		public int TargetY { get; set; }

		public void Apply()
		{
			Position2D position2D = this.Space.Find(this.ID, "Position2D") as Position2D;
			if (position2D == null)
			{
				return;
			}
			if (position2D.X == this.TargetX && position2D.Y == this.TargetY)
			{
				return;
			}
			position2D.X = this.TargetX;
			position2D.Y = this.TargetY;
			this.Space.NotifyModified(position2D);
		}
	}
}
