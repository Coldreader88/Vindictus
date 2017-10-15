using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using MMOServer;

namespace MMOServerTest
{
	internal class VirtualClient : ICamera
	{
		private long MakePID(int x, int y)
		{
			int num = x / 10;
			int num2 = y / 10;
			if (num < 0 || num >= 5)
			{
				return -1L;
			}
			if (num2 < 0 || num2 >= 5)
			{
				return -1L;
			}
			return MMOServerTest.t01[num, num2];
		}

		public VirtualClient(long id, Channel channel, Panel panel)
		{
			this.ID = id;
			this.channel = channel;
			this.panel = panel;
			panel.Paint += this.Paint;
			panel.MouseUp += this.MouseUp;
			this.x = (int)(VirtualClient.random.NextDouble() * 50.0);
			this.y = (int)(VirtualClient.random.NextDouble() * 50.0);
			this.pid = this.MakePID(this.x, this.y);
			channel.AddComponent(channel.MakeLocation(id, 1, this.pid));
			channel.AddComponent(new Position2D(id, this.x, this.y, 1));
			channel.AddObserver(this.pid, this);
		}

		public void Paint(object sender, PaintEventArgs e)
		{
			Pen pen = new Pen(Color.Gray);
			for (int i = 0; i < 50; i += 10)
			{
				e.Graphics.DrawLine(pen, 0, i, 50, i);
				e.Graphics.DrawLine(pen, i, 0, i, 50);
			}
			Pen pen2 = new Pen(Color.Blue);
			Pen pen3 = new Pen(Color.Red);
			foreach (VirtualClient.OtherClient otherClient in this.otherClients.Values)
			{
				if (otherClient.id == this.ID)
				{
					e.Graphics.DrawLine(pen3, otherClient.x, otherClient.y, otherClient.x + 1, otherClient.y + 1);
				}
				else
				{
					e.Graphics.DrawLine(pen2, otherClient.x, otherClient.y, otherClient.x + 1, otherClient.y + 1);
				}
			}
		}

		public void MouseUp(object sender, MouseEventArgs e)
		{
			int targetX = e.X;
			int targetY = e.Y;
			long num = this.MakePID(targetX, targetY);
			if (num < 0L)
			{
				return;
			}
			this.x = targetX;
			this.y = targetY;
			this.pid = num;
			this.channel.ApplyModifier(this.channel.MakeMove(this.ID, this.pid));
			this.channel.ApplyModifier(new Position2DMove
			{
				ID = this.ID,
				TargetX = targetX,
				TargetY = targetY,
				Space = this.channel.Space
			});
			this.channel.MoveObserver(this.pid, this);
		}

		public long ID { get; set; }

		public long WatchingPartition { get; set; }

		public void Update(IMessage message)
		{
			if (message is Disappeared)
			{
				Disappeared disappeared = message as Disappeared;
				this.otherClients.Remove(disappeared.ID);
				this.panel.Invalidate();
				return;
			}
			if (message is Position2DUpdated)
			{
				Position2DUpdated position2DUpdated = message as Position2DUpdated;
				VirtualClient.OtherClient otherClient;
				if (this.otherClients.TryGetValue(position2DUpdated.ID, out otherClient))
				{
					otherClient.x = position2DUpdated.X;
					otherClient.y = position2DUpdated.Y;
				}
				else
				{
					this.otherClients[position2DUpdated.ID] = new VirtualClient.OtherClient
					{
						id = position2DUpdated.ID,
						x = position2DUpdated.X,
						y = position2DUpdated.Y
					};
				}
				if (position2DUpdated.ID == this.ID)
				{
					Console.WriteLine("Receive my Pos : {0} ({1},{2}), ({3},{4})", new object[]
					{
						this.ID,
						this.x,
						this.y,
						position2DUpdated.X,
						position2DUpdated.Y
					});
				}
				this.panel.Invalidate();
			}
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

		private Channel channel;

		private Dictionary<long, VirtualClient.OtherClient> otherClients = new Dictionary<long, VirtualClient.OtherClient>();

		private Panel panel;

		private long pid;

		private int x;

		private int y;

		private static Random random = new Random();

		private class OtherClient
		{
			public long id;

			public int x;

			public int y;
		}
	}
}
