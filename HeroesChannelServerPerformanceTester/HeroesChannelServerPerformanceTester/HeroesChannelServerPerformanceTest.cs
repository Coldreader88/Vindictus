using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Devcat.Core;
using HeroesChannelServer.PerformanceTest;

namespace HeroesChannelServerPerformanceTester
{
	public partial class HeroesChannelServerPerformanceTest : Form
	{
		public HeroesChannelServerPerformanceTest()
		{
			this.InitializeComponent();
		}

		private void Form1_Load(object sender, EventArgs e)
		{
			this.runner = new PerformanceTestRunner();
			this.propertyRunner.SelectedObject = this.runner;
			this.runner.ExceptionOccur += this.runner_ExceptionOccur;
		}

		private void runner_ExceptionOccur(object sender, EventArgs<Exception> e)
		{
			base.Invoke(new Action<Exception>(this.ExceptionHandler), new object[]
			{
				e.Value
			});
		}

		private void ExceptionHandler(Exception e)
		{
			MessageBox.Show(e.StackTrace, e.Message);
		}

		private void buttonStart_Click(object sender, EventArgs e)
		{
			this.runner.Start();
			this.buttonStart.Text = "Restart";
		}

		private void Form1_FormClosed(object sender, FormClosedEventArgs e)
		{
			this.runner.Stop();
		}

		private void buttonRefresh_Click(object sender, EventArgs e)
		{
			this.propertyRunner.Refresh();
		}

		private void HeroesChannelServerPerformanceTest_Resize(object sender, EventArgs e)
		{
			this.propertyRunner.Size = new Size(base.Size.Width - 41, base.Size.Height - 92);
		}

		private PerformanceTestRunner runner;
	}
}
