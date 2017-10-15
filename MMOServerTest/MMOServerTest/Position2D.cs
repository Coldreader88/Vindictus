using System;
using MMOServer;

namespace MMOServerTest
{
	internal class Position2D : IComponent
	{
		public long ID { get; private set; }

		public string Category
		{
			get
			{
				return "Position2D";
			}
		}

		public bool IsTemporal
		{
			get
			{
				return false;
			}
		}

		public int SightDegree { get; private set; }

		public int X { get; set; }

		public int Y { get; set; }

		public Position2D(long id, int x, int y, int sight)
		{
			this.ID = id;
			this.X = x;
			this.oldX = x;
			this.Y = y;
			this.oldY = y;
			this.SightDegree = sight;
		}

		public IMessage AppearMessage()
		{
			return new Position2DUpdated
			{
				ID = this.ID,
				X = this.X,
				Y = this.Y
			};
		}

		public IMessage DifferenceMessage()
		{
			if (this.X != this.oldX || this.Y != this.oldY)
			{
				return this.AppearMessage();
			}
			return null;
		}

		public IMessage DisappearMessage()
		{
			return null;
		}

		public void Flatten()
		{
			this.oldX = this.X;
			this.oldY = this.Y;
		}

		private int oldX;

		private int oldY;
	}
}
