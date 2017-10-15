using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace HeroesOpTool
{
	public partial class InputBox : Form
	{
		private InputBox()
		{
			this.InitializeComponent();
			this.BtnOK.Text = LocalizeText.Get(5);
			this.BtnCancel.Text = LocalizeText.Get(6);
		}

		public static string Show(string Description, string DefaultString)
		{
			InputBox inputBox = new InputBox();
			SizeF sizeF = Graphics.FromHwnd(inputBox.Handle).MeasureString(Description, inputBox.LabelDesc.Font);
			inputBox.LabelDesc.Text = Description;
			inputBox.TBoxInput.Text = DefaultString;
			if (sizeF.Width < 280f)
			{
				sizeF.Width = 280f;
			}
			if (sizeF.Height < 16f)
			{
				sizeF.Height = 16f;
			}
			inputBox.Size = new Size((int)sizeF.Width + 30, (int)sizeF.Height + 110);
			if (inputBox.ShowDialog() == DialogResult.OK)
			{
				return inputBox.TBoxInput.Text;
			}
			return null;
		}
	}
}
