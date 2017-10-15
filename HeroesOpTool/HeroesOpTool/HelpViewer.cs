using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Resources;
using System.Windows.Forms;
using AxSHDocVw;

namespace HeroesOpTool
{
	public partial class HelpViewer : Form
	{
		public HelpViewer()
		{
			this.InitializeComponent();
			this.TViewHelpIndex.Nodes.AddRange(new TreeNode[]
			{
				new TreeNode(LocalizeText.Get(0), 0, 0, new TreeNode[]
				{
					new TreeNode(LocalizeText.Get(1), 1, 1, new TreeNode[]
					{
						new TreeNode(LocalizeText.Get(2)),
						new TreeNode(LocalizeText.Get(3)),
						new TreeNode(LocalizeText.Get(4))
					})
				})
			});
			this.TViewHelpIndex.Nodes[0].Text = LocalizeText.Get(0);
			this.TViewHelpIndex.Nodes[0].Nodes[0].Text = LocalizeText.Get(1);
			this.TViewHelpIndex.Nodes[0].Nodes[0].Nodes[0].Text = LocalizeText.Get(2);
			this.TViewHelpIndex.Nodes[0].Nodes[0].Nodes[1].Text = LocalizeText.Get(3);
			this.TViewHelpIndex.Nodes[0].Nodes[0].Nodes[2].Text = LocalizeText.Get(4);
			this.TViewHelpIndex.AfterExpand += this.NodeExpanded;
			this.TViewHelpIndex.AfterCollapse += this.NodeCollapsed;
			this.HtmlIndexList = new SortedList();
			this.HtmlIndexList.Add("0.0.0", "LogQuery.htm");
			this.HtmlIndexList.Add("0.0.1", "LogResult.htm");
			this.HtmlIndexList.Add("0.0.2", "LogAdvance.htm");
			this.TViewHelpIndex.ExpandAll();
		}

		private void NodeExpanded(object Sender, TreeViewEventArgs Args)
		{
			if (Args.Node.ImageIndex == 1)
			{
				Args.Node.ImageIndex = 2;
				Args.Node.SelectedImageIndex = 2;
			}
		}

		private void NodeCollapsed(object Sender, TreeViewEventArgs Args)
		{
			if (Args.Node.ImageIndex == 2)
			{
				Args.Node.ImageIndex = 1;
				Args.Node.SelectedImageIndex = 1;
			}
		}

		private string GetIndexNode(TreeNode Node)
		{
			if (Node.Parent == null)
			{
				return Node.Index.ToString();
			}
			return this.GetIndexNode(Node.Parent) + "." + Node.Index.ToString();
		}

		private void TViewHelpIndex_AfterSelect(object Sender, TreeViewEventArgs Args)
		{
			string text = this.HtmlIndexList[this.GetIndexNode(Args.Node)] as string;
			if (text == null)
			{
				text = "Error.htm";
			}
			string uRL = "file:///" + Directory.GetCurrentDirectory().Replace("\\", "/") + "/Help/" + text;
			object obj = new object();
			object obj2 = new object();
			object obj3 = new object();
			object obj4 = new object();
			this.HtmlHelpContent.Navigate(uRL, ref obj, ref obj2, ref obj3, ref obj4);
		}

		private SortedList HtmlIndexList;
	}
}
