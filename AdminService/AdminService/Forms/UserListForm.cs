using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace AdminService.Forms
{
	public partial class UserListForm : Form
	{
		public UserListForm()
		{
			this.InitializeComponent();
		}

		public ListBox ListBox
		{
			get
			{
				return this.listBox1;
			}
		}

		public void SetUserList(IEnumerable<string> list)
		{
			if (list == null)
			{
				return;
			}
			foreach (string item in list)
			{
				this.listBox1.Items.Add(item);
			}
		}
	}
}
