using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace HeroesOpTool.RCUser.ServerMonitorSystem
{
	public class TwoRadioButton : UserControl
	{
		public TwoRadioButton()
		{
			this.InitializeComponent();
		}

		public bool Checked
		{
			get
			{
				return this.radioButtonOn.Checked;
			}
		}

		private void radioButtonOff_CheckedChanged(object sender, EventArgs e)
		{
		}

		//protected override void Dispose(bool disposing)
		//{
		//	if (disposing && this.components != null)
		//	{
		//		this.components.Dispose();
		//	}
		//	base.Dispose(disposing);
		//}

		private void InitializeComponent()
		{
			this.radioButtonOn = new RadioButton();
			this.radioButtonOff = new RadioButton();
			base.SuspendLayout();
			this.radioButtonOn.AutoSize = true;
			this.radioButtonOn.Location = new Point(7, 4);
			this.radioButtonOn.Name = "radioButtonOn";
			this.radioButtonOn.Size = new Size(39, 16);
			this.radioButtonOn.TabIndex = 0;
			this.radioButtonOn.Text = "On";
			this.radioButtonOn.UseVisualStyleBackColor = true;
			this.radioButtonOff.AutoSize = true;
			this.radioButtonOff.Checked = true;
			this.radioButtonOff.Location = new Point(129, 4);
			this.radioButtonOff.Name = "radioButtonOff";
			this.radioButtonOff.Size = new Size(38, 16);
			this.radioButtonOff.TabIndex = 1;
			this.radioButtonOff.TabStop = true;
			this.radioButtonOff.Text = "Off";
			this.radioButtonOff.UseVisualStyleBackColor = true;
			this.radioButtonOff.CheckedChanged += this.radioButtonOff_CheckedChanged;
			base.AutoScaleDimensions = new SizeF(7f, 12f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.Controls.Add(this.radioButtonOff);
			base.Controls.Add(this.radioButtonOn);
			base.Name = "TwoRadioButton";
			base.Size = new Size(232, 25);
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		//private IContainer components;

		private RadioButton radioButtonOn;

		private RadioButton radioButtonOff;
	}
}
