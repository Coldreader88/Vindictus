namespace HeroesChannelServerPerformanceTester
{
	public partial class HeroesChannelServerPerformanceTest : global::System.Windows.Forms.Form
	{
		private void InitializeComponent()
		{
			this.buttonStart = new global::System.Windows.Forms.Button();
			this.propertyRunner = new global::System.Windows.Forms.PropertyGrid();
			this.buttonRefresh = new global::System.Windows.Forms.Button();
			base.SuspendLayout();
			this.buttonStart.Location = new global::System.Drawing.Point(13, 13);
			this.buttonStart.Name = "buttonStart";
			this.buttonStart.Size = new global::System.Drawing.Size(75, 23);
			this.buttonStart.TabIndex = 0;
			this.buttonStart.Text = "Start";
			this.buttonStart.UseVisualStyleBackColor = true;
			this.buttonStart.Click += new global::System.EventHandler(this.buttonStart_Click);
			this.propertyRunner.Location = new global::System.Drawing.Point(13, 42);
			this.propertyRunner.Name = "propertyRunner";
			this.propertyRunner.Size = new global::System.Drawing.Size(622, 495);
			this.propertyRunner.TabIndex = 1;
			this.buttonRefresh.Location = new global::System.Drawing.Point(95, 13);
			this.buttonRefresh.Name = "buttonRefresh";
			this.buttonRefresh.Size = new global::System.Drawing.Size(75, 23);
			this.buttonRefresh.TabIndex = 2;
			this.buttonRefresh.Text = "Refresh";
			this.buttonRefresh.UseVisualStyleBackColor = true;
			this.buttonRefresh.Click += new global::System.EventHandler(this.buttonRefresh_Click);
			base.AutoScaleDimensions = new global::System.Drawing.SizeF(7f, 12f);
			base.AutoScaleMode = global::System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new global::System.Drawing.Size(647, 549);
			base.Controls.Add(this.buttonRefresh);
			base.Controls.Add(this.propertyRunner);
			base.Controls.Add(this.buttonStart);
			base.Name = "HeroesChannelServerPerformanceTest";
			this.Text = "Heroes Channel Performance Test";
			base.Load += new global::System.EventHandler(this.Form1_Load);
			base.FormClosed += new global::System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
			base.Resize += new global::System.EventHandler(this.HeroesChannelServerPerformanceTest_Resize);
			base.ResumeLayout(false);
		}

		private global::System.Windows.Forms.Button buttonStart;

		private global::System.Windows.Forms.PropertyGrid propertyRunner;

		private global::System.Windows.Forms.Button buttonRefresh;
	}
}
