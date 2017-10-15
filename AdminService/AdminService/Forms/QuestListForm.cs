using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace AdminService.Forms
{
	public partial class QuestListForm : Form
	{
		public QuestListForm()
		{
			this.InitializeComponent();
		}

		public void SetQuestList(IEnumerable<string> list)
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
