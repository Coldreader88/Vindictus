using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using MMOServer;

namespace MMOServerTest
{
	public partial class MMOServerTest : Form
	{
		public MMOServerTest()
		{
			this.InitializeComponent();
		}

		private void BuildChannel(Channel channel, string bsp, long[,] ps)
		{
			channel.AddRegion(bsp);
			int upperBound = ps.GetUpperBound(0);
			int upperBound2 = ps.GetUpperBound(1);
			for (int i = ps.GetLowerBound(0); i <= upperBound; i++)
			{
				for (int j = ps.GetLowerBound(1); j <= upperBound2; j++)
				{
					long id = ps[i, j];
					channel.AddPartition(bsp, id);
				}
			}
			for (int k = 0; k < ps.GetLength(0) - 1; k++)
			{
				for (int l = 0; l < ps.GetLength(1) - 1; l++)
				{
					channel.AddLink(bsp, ps[k, l], ps[k + 1, l]);
					channel.AddLink(bsp, ps[k, l], ps[k, l + 1]);
					channel.AddLink(bsp, ps[k, l], ps[k + 1, l + 1]);
					channel.AddLink(bsp, ps[k + 1, l], ps[k, l + 1]);
				}
			}
			for (int m = 0; m < ps.GetLength(0) - 1; m++)
			{
				channel.AddLink(bsp, ps[m, ps.GetLength(1) - 1], ps[m + 1, ps.GetLength(1) - 1]);
			}
			for (int n = 0; n < ps.GetLength(1) - 1; n++)
			{
				channel.AddLink(bsp, ps[ps.GetLength(0) - 1, n], ps[ps.GetLength(0) - 1, n + 1]);
			}
		}

		private void MMOServerTest_Load(object sender, EventArgs e)
		{
			Channel channel = new Channel();
			this.BuildChannel(channel, "t01", MMOServerTest.t01);
			channel.ConfirmRegions(3);
			foreach (string text in channel.GetRegionStrings())
			{
			}
			for (int i = 0; i < 50; i++)
			{
				Panel panel = new Panel();
				panel.BackColor = Color.White;
				panel.Top = i / 10 * 55 + 25;
				panel.Left = i % 10 * 55 + 25;
				panel.Width = 50;
				panel.Height = 50;
				base.Controls.Add(panel);
				if (base.Width < panel.Left + panel.Width + 50)
				{
					base.Width = panel.Left + panel.Width + 50;
				}
				if (base.Height < panel.Top + panel.Height + 75)
				{
					base.Height = panel.Top + panel.Height + 75;
				}
				new VirtualClient((long)(i + 1), channel, panel);
			}
		}

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
	}
}
