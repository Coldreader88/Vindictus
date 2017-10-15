using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace MMOServer
{
	[TestFixture]
	internal class ChannelTest
	{
		private void BuildChannel(string bsp, long[,] ps)
		{
			this.channel.AddRegion(bsp);
			int upperBound = ps.GetUpperBound(0);
			int upperBound2 = ps.GetUpperBound(1);
			for (int i = ps.GetLowerBound(0); i <= upperBound; i++)
			{
				for (int j = ps.GetLowerBound(1); j <= upperBound2; j++)
				{
					long id = ps[i, j];
					this.channel.AddPartition(bsp, id);
				}
			}
			for (int k = 0; k < ps.GetLength(0) - 1; k++)
			{
				for (int l = 0; l < ps.GetLength(1) - 1; l++)
				{
					this.channel.AddLink(bsp, ps[k, l], ps[k + 1, l]);
					this.channel.AddLink(bsp, ps[k, l], ps[k, l + 1]);
					this.channel.AddLink(bsp, ps[k, l], ps[k + 1, l + 1]);
					this.channel.AddLink(bsp, ps[k + 1, l], ps[k, l + 1]);
				}
			}
			for (int m = 0; m < ps.GetLength(0) - 1; m++)
			{
				this.channel.AddLink(bsp, ps[m, ps.GetLength(1) - 1], ps[m + 1, ps.GetLength(1) - 1]);
			}
			for (int n = 0; n < ps.GetLength(1) - 1; n++)
			{
				this.channel.AddLink(bsp, ps[ps.GetLength(0) - 1, n], ps[ps.GetLength(0) - 1, n + 1]);
			}
		}

		[SetUp]
		public void SetUp()
		{
			this.channel = new Channel();
			this.BuildChannel("t01", ChannelTest.t01);
			this.channel.ConfirmRegions(3);
		}

		[Test]
		public void Test()
		{
			ChannelTest.VirtualClient virtualClient = new ChannelTest.VirtualClient(this.channel, 1L, 1L, 1, 1);
			new ChannelTest.VirtualClient(this.channel, 2L, 1L, 3, 4);
			Console.WriteLine("1 - 1,5,6");
			virtualClient.Move(1L, 5, 6);
			Console.WriteLine("1 - 2,7,8");
			virtualClient.Move(2L, 7, 8);
			Console.WriteLine("1 - 3,9,10");
			virtualClient.Move(3L, 9, 10);
			Console.WriteLine("1 - 5,11,12");
			virtualClient.Move(5L, 11, 12);
		}

		private Channel channel;

		public static long[,] t01 = new long[,]
		{
			{
				1L,
				2L,
				3L,
				4L,
				5L
			},
			{
				6L,
				7L,
				8L,
				9L,
				10L
			},
			{
				11L,
				12L,
				13L,
				14L,
				15L
			},
			{
				16L,
				17L,
				18L,
				19L,
				20L
			},
			{
				21L,
				22L,
				23L,
				24L,
				25L
			}
		};

		private class Position2DUpdated : IMessage
		{
			public long ID { get; set; }

			public int X { get; set; }

			public int Y { get; set; }

			public override string ToString()
			{
				return string.Format("Position2DUpdated {0}({1},{2})", this.ID, this.X, this.Y);
			}
		}

		private class Position2DMove : IModifier
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
				ChannelTest.Position2D position2D = this.Space.Find(this.ID, "Position2D") as ChannelTest.Position2D;
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

		private class Position2D : IComponent
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
				return new ChannelTest.Position2DUpdated
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

			public override string ToString()
			{
				return string.Format("Position {0}({1}, {2})", this.ID, this.X, this.Y);
			}

			private int oldX;

			private int oldY;
		}

		private class VirtualClient : ICamera
		{
			public override string ToString()
			{
				return string.Format("VirtualClient{0}", this.ID);
			}

			public VirtualClient(Channel channel, long id, long pid, int x, int y)
			{
				this.ID = id;
				this.channel = channel;
				channel.AddComponent(channel.MakeLocation(id, 1, pid));
				channel.AddComponent(new ChannelTest.Position2D(id, x, y, 1));
				channel.AddObserver(pid, this);
			}

			public long ID { get; set; }

			public long WatchingPartition { get; set; }

			public void Update(IMessage message)
			{
				Console.WriteLine("enqueue {0} to {1}", message, this.ID);
				this.mq.Enqueue(message);
			}

			public void Update(IEnumerable<IMessage> messages)
			{
				foreach (IMessage message in messages)
				{
					this.Update(message);
				}
			}

			public void Flush()
			{
			}

			public IMessage Dequeue()
			{
				return this.mq.Dequeue();
			}

			public bool IsEmpty()
			{
				return this.mq.Count == 0;
			}

			public void Move(long pid, int x, int y)
			{
				this.channel.ApplyModifier(this.channel.MakeMove(this.ID, pid));
				this.channel.ApplyModifier(new ChannelTest.Position2DMove
				{
					ID = this.ID,
					TargetX = x,
					TargetY = y
				});
				this.channel.MoveObserver(pid, this);
			}

			private Channel channel;

			private Queue<IMessage> mq = new Queue<IMessage>();
		}
	}
}
