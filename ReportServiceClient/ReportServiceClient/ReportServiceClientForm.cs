using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using UnifiedNetwork.EntityGraph;

namespace ReportServiceClient
{
	public partial class ReportServiceClientForm : Form
	{
		public bool OnWaiting
		{
			get
			{
				return this.currentNode != null;
			}
		}

		public int CurrentNodeTier
		{
			get
			{
				return this.currentTier;
			}
		}

		public event Action afterFinishInsertion;

		public ReportServiceClientForm()
		{
			this.InitializeComponent();
			this.currentNode = null;
			this.currentTier = -1;
			this.disableTrigger = false;
		}

		private void AddNodeOnLoad(string t)
		{
			this.TreeViewLeft.Nodes.Add(t);
			this.TreeViewRight.Nodes.Add(t);
			foreach (object obj in this.TreeViewLeft.Nodes)
			{
				TreeNode node = (TreeNode)obj;
				this.TreeViewLeft.Nodes[this.TreeViewLeft.Nodes.IndexOf(node)].Nodes.Add(this.WaitingNotifyString);
				this.TreeViewRight.Nodes[this.TreeViewLeft.Nodes.IndexOf(node)].Nodes.Add(this.WaitingNotifyString);
			}
		}

		private void OnConnected(object sender, EventArgs e)
		{
		}

		private void ReportServiceClientForm_Load(object sender, EventArgs e)
		{
			this.adminNode = new ReportServiceClientNode(this);
			this.adminNode.ConnectionSucceed += this.OnConnected;
			base.Invoke(new Action(delegate
			{
				this.AddNodeOnLoad(this.ServicesString);
			}));
		}

		public void QueryInsertion(TreeNode node)
		{
			node.Nodes.Clear();
			if (!this.adminNode.Valid)
			{
				this.AddNodeOnTree(node, this.ConnectionErrorString);
				return;
			}
			this.AddNodeOnTree(node, this.WaitingNotifyString);
			this.TreeViewLeft.Enabled = false;
			this.TreeViewRight.Enabled = false;
			this.currentNode = node;
			this.currentTier = this.GetTier(node);
			this.adminNode.QueryUnderingList(node.Text);
		}

		public void QueryInsertion(TreeNode node, long selectedEID)
		{
			node.Nodes.Clear();
			if (!this.adminNode.Valid)
			{
				this.AddNodeOnTree(node, this.ConnectionErrorString);
				return;
			}
			this.AddNodeOnTree(node, this.WaitingNotifyString);
			this.TreeViewLeft.Enabled = false;
			this.TreeViewRight.Enabled = false;
			this.currentNode = node;
			this.currentTier = this.GetTier(node);
			this.adminNode.QueryUnderingList(node.Text, selectedEID);
		}

        public void AdjustCurrentNode(int targetServiceID, long targetEntityID)
        {
            IEnumerator enumerator = this.currentNode.Nodes.GetEnumerator();
            {
                while (enumerator.MoveNext())
                {
                    TreeNode n = (TreeNode)enumerator.Current;
                    string[] array = n.Text.Split(this.delim);
                    if (int.Parse(array[1]) == targetServiceID)
                    {
                        this.afterFinishInsertion = delegate
                        {
                            n.Expand();
                            bool flag = true;
                            IEnumerator enumerator2 = n.Nodes.GetEnumerator();
                            {
                                while (enumerator2.MoveNext())
                                {
                                    TreeNode v = (TreeNode)enumerator2.Current;
                                    string[] array2 = v.Text.Split(this.delim);
                                    if ((long.Parse(array2[2]) << 32) + long.Parse(array2[3]) == targetEntityID)
                                    {
                                        this.afterFinishInsertion = delegate
                                        {
                                            v.Expand();
                                            v.TreeView.SelectedNode = v;
                                            v.TreeView.Focus();
                                            this.disableTrigger = false;
                                        };
                                        flag = false;
                                        this.QueryInsertion(v);
                                        break;
                                    }
                                }
                            }
                            if (flag)
                            {
                                this.SelectException();
                            }
                        };
                        this.QueryInsertion(n);
                        break;
                    }
                }
            }
        }

        public void ReadyInsertion()
		{
			this.currentNode.Nodes.Clear();
		}

		public void Insertion(string t)
		{
			if (this.currentNode.Nodes.Count == 1 && this.currentNode.Nodes[0].Text == this.WaitingNotifyString)
			{
				this.currentNode.Nodes.Clear();
			}
			this.AddNodeOnTree(this.currentNode, t);
		}

		public void Insertion(string t, string sub)
		{
			if (this.currentNode.Nodes.Count == 1 && this.currentNode.Nodes[0].Text == this.WaitingNotifyString)
			{
				this.currentNode.Nodes.Clear();
			}
			if (this.GetTier(this.currentNode) == 3)
			{
				this.AddNodeOnTree(this.currentNode, string.Format("{0} {1}", t, sub));
				return;
			}
			this.AddNodeOnTree(this.currentNode, t);
		}

		public void FinishInsertion()
		{
			if (this.currentNode.Nodes.Count == 1 && this.currentNode.Nodes[0].Text == this.WaitingNotifyString)
			{
				this.currentNode.Nodes.Clear();
			}
			this.TreeViewLeft.Enabled = true;
			this.TreeViewRight.Enabled = true;
			this.currentNode = null;
			this.currentTier = -1;
			if (this.afterFinishInsertion != null)
			{
				this.afterFinishInsertion();
			}
		}

		private void TreeViewLeft_AfterExpand(object sender, TreeViewEventArgs e)
		{
			if (this.disableTrigger)
			{
				return;
			}
			this.afterFinishInsertion = null;
			this.QueryInsertion(e.Node);
		}

		private void TreeViewRight_AfterExpand(object sender, TreeViewEventArgs e)
		{
			if (this.disableTrigger)
			{
				return;
			}
			this.afterFinishInsertion = null;
			this.QueryInsertion(e.Node);
		}

		private int GetTier(TreeNode node)
		{
			TreeNode treeNode = node;
			int num = 0;
			while (treeNode != null)
			{
				num++;
				treeNode = treeNode.Parent;
			}
			return num;
		}

		private void AddNodeOnTree(TreeNode node, string t)
		{
			int tier = this.GetTier(node);
			TreeNode treeNode = new TreeNode(t);
			if (tier < 3 && t != this.WaitingNotifyString && t != this.ConnectionErrorString)
			{
				treeNode.Nodes.Add(this.WaitingNotifyString);
			}
			node.Nodes.Add(treeNode);
		}

		private void TreeViewLeft_AfterSelect(object sender, TreeViewEventArgs e)
		{
			if (this.disableTrigger)
			{
				return;
			}
			string[] parse = e.Node.Text.Split(this.delim);
			if (this.GetTier(e.Node) == 4)
			{
				long eid = (long.Parse(parse[2]) << 32) + long.Parse(parse[3]);
				this.disableTrigger = true;
				this.afterFinishInsertion = delegate
				{
					this.TreeViewRight.Nodes[0].Expand();
					this.currentNode = this.TreeViewRight.Nodes[0];
					this.currentTier = this.GetTier(this.currentNode);
					this.adminNode.QuerySelect(parse[1], eid);
				};
				this.QueryInsertion(this.TreeViewRight.Nodes[0], eid);
			}
		}

		private void TreeViewRight_AfterSelect(object sender, TreeViewEventArgs e)
		{
			if (this.disableTrigger)
			{
				return;
			}
			string[] parse = e.Node.Text.Split(this.delim);
			if (this.GetTier(e.Node) == 4)
			{
				long eid = (long.Parse(parse[2]) << 32) + long.Parse(parse[3]);
				this.disableTrigger = true;
				this.afterFinishInsertion = delegate
				{
					this.TreeViewLeft.Nodes[0].Expand();
					this.currentNode = this.TreeViewLeft.Nodes[0];
					this.currentTier = this.GetTier(this.currentNode);
					this.adminNode.QuerySelect(parse[1], eid);
				};
				this.QueryInsertion(this.TreeViewLeft.Nodes[0], eid);
			}
		}

		public void SelectException()
		{
			this.currentNode = null;
			this.currentTier = -1;
			this.disableTrigger = false;
		}

		private void TreeViewLeft_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
		{
		}

		public void InvokeMessageBox(string text)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendLine(text);
			MessageBox.Show(stringBuilder.ToString());
		}

		public void InvokeGridBox(string text)
		{
			GridNotifyForm gridNotifyForm = new GridNotifyForm();
			gridNotifyForm.Show(text);
		}

		private void ReportServiceClientForm_SizeChanged(object sender, EventArgs e)
		{
			this.TreeViewLeft.Width = (base.Width - 36) / 2;
			this.TreeViewLeft.Height = base.Height - 112;
			this.TreeViewRight.Width = (base.Width - 36) / 2;
			this.TreeViewRight.Height = base.Height - 112;
			this.TreeViewRight.Left = (base.Width - 36) / 2 + 18;
		}

		private void GetLookUpInfoButton_Click(object sender, EventArgs e)
		{
			if (this.TreeViewLeft.SelectedNode != null)
			{
				int tier = this.GetTier(this.TreeViewLeft.SelectedNode);
				if (tier == 2 || tier == 3)
				{
					string[] array = this.TreeViewLeft.SelectedNode.Text.Split(this.delim);
					this.currentNode = this.TreeViewLeft.SelectedNode;
					this.currentTier = this.GetTier(this.TreeViewLeft.SelectedNode);
					this.adminNode.QueryLookUpInfo(int.Parse(array[1]));
				}
			}
		}

		private void GetTimeReportButton_Click(object sender, EventArgs e)
		{
			if (this.TreeViewLeft.SelectedNode != null)
			{
				int tier = this.GetTier(this.TreeViewLeft.SelectedNode);
				if (tier == 2)
				{
					string[] array = this.TreeViewLeft.SelectedNode.Text.Split(this.delim);
					int tSID = int.Parse(array[1]);
					this.adminNode.QueryOperationTime(tSID, (long)EntityGraphNode.ServiceEntityID, EntityGraphNode.BadCategory, 0L);
				}
				if (tier == 3)
				{
					string[] array2 = this.TreeViewLeft.SelectedNode.Text.Split(this.delim);
					int tSID2 = int.Parse(array2[1]);
					long tEID = (long.Parse(array2[2]) << 32) + long.Parse(array2[3]);
					this.adminNode.QueryOperationTime(tSID2, tEID, EntityGraphNode.BadCategory, 0L);
				}
				if (tier == 4)
				{
					string[] array3 = this.TreeViewLeft.SelectedNode.Text.Split(this.delim);
					string[] array4 = this.TreeViewLeft.SelectedNode.Parent.Text.Split(this.delim);
					int tSID3 = int.Parse(array4[1]);
					long tEID2 = (long.Parse(array4[2]) << 32) + long.Parse(array4[3]);
					long uEID = (long.Parse(array3[2]) << 32) + long.Parse(array3[3]);
					this.adminNode.QueryOperationTime(tSID3, tEID2, array3[1], uEID);
				}
			}
		}

		private void EnableTimeReportButton_Click(object sender, EventArgs e)
		{
			if (this.TreeViewLeft.SelectedNode != null)
			{
				int tier = this.GetTier(this.TreeViewLeft.SelectedNode);
				if (tier == 2)
				{
					string[] array = this.TreeViewLeft.SelectedNode.Text.Split(this.delim);
					int tSID = int.Parse(array[1]);
					StringBuilder stringBuilder = new StringBuilder();
					stringBuilder.AppendLine(string.Format("Enables TimeReport of {0}.", array[2]));
					stringBuilder.AppendLine();
					stringBuilder.AppendLine("Press OK to enable time reporting.");
					stringBuilder.AppendLine("Press cancel to return.");
					DialogResult dialogResult = MessageBox.Show(stringBuilder.ToString(), "Warning", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk);
					if (dialogResult == DialogResult.OK)
					{
						this.adminNode.EnableOperationReport(tSID, true);
					}
				}
			}
		}

		private void DisableTimeReportButton_Click(object sender, EventArgs e)
		{
			if (this.TreeViewLeft.SelectedNode != null)
			{
				int tier = this.GetTier(this.TreeViewLeft.SelectedNode);
				if (tier == 2)
				{
					string[] array = this.TreeViewLeft.SelectedNode.Text.Split(this.delim);
					int tSID = int.Parse(array[1]);
					StringBuilder stringBuilder = new StringBuilder();
					stringBuilder.AppendLine(string.Format("Disables TimeReport of {0}.", array[2]));
					stringBuilder.AppendLine("Also this command discards current TimeReport Samples!");
					stringBuilder.AppendLine();
					stringBuilder.AppendLine("Press OK to disable time reporting.");
					stringBuilder.AppendLine("Press cancel to return.");
					DialogResult dialogResult = MessageBox.Show(stringBuilder.ToString(), "Warning", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk);
					if (dialogResult == DialogResult.OK)
					{
						this.adminNode.EnableOperationReport(tSID, false);
					}
				}
			}
		}

		private void ShutDownEntity_Click(object sender, EventArgs e)
		{
			if (this.TreeViewLeft.SelectedNode != null)
			{
				int tier = this.GetTier(this.TreeViewLeft.SelectedNode);
				if (tier == 3)
				{
					string[] array = this.TreeViewLeft.SelectedNode.Text.Split(this.delim);
					int tSID = int.Parse(array[1]);
					long tEID = (long.Parse(array[2]) << 32) + long.Parse(array[3]);
					StringBuilder stringBuilder = new StringBuilder();
					stringBuilder.AppendLine(string.Format("Kicks Entity {0}/{1},{2}.", array[1], array[2], array[3]));
					stringBuilder.AppendLine();
					stringBuilder.AppendLine("Press OK to continue.");
					stringBuilder.AppendLine("Press cancel to return.");
					DialogResult dialogResult = MessageBox.Show(stringBuilder.ToString(), "Warning", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk);
					if (dialogResult == DialogResult.OK)
					{
						this.adminNode.QueryShutDownEntity(tSID, tEID);
						TreeNode selectedNode = this.TreeViewLeft.SelectedNode;
						TreeNode parent = selectedNode.Parent;
						parent.Nodes.Remove(selectedNode);
						this.TreeViewLeft.SelectedNode = parent;
					}
				}
			}
		}

		private void ClearButton_Click(object sender, EventArgs e)
		{
			this.textBoxSID.Text = "";
			this.textBoxEID1.Text = "";
			this.textBoxEID2.Text = "";
		}

		private void OpenEntityButton_Click(object sender, EventArgs e)
		{
			try
			{
				int targetSID = int.Parse(this.textBoxSID.Text);
				long targetEID = (long.Parse(this.textBoxEID1.Text) << 32) + long.Parse(this.textBoxEID2.Text);
				this.disableTrigger = true;
				this.afterFinishInsertion = delegate
				{
					this.TreeViewLeft.Nodes[0].Expand();
					this.currentNode = this.TreeViewLeft.Nodes[0];
					this.currentTier = this.GetTier(this.currentNode);
					this.AdjustCurrentNode(targetSID, targetEID);
				};
				this.QueryInsertion(this.TreeViewLeft.Nodes[0], targetEID);
			}
			catch (FormatException)
			{
			}
		}

		private void textBoxSID_Enter(object sender, EventArgs e)
		{
			this.textBoxSID.SelectAll();
		}

		private void textBoxEID1_Enter(object sender, EventArgs e)
		{
			this.textBoxEID1.SelectAll();
		}

		private void textBoxEID2_Enter(object sender, EventArgs e)
		{
			this.textBoxEID2.SelectAll();
		}

		public readonly string ServicesString = "Services";

		public readonly string WaitingNotifyString = "Accepting...";

		public readonly string ConnectionErrorString = "Service Unavailable";

		public readonly char[] delim = new char[]
		{
			',',
			'[',
			']',
			'/'
		};

		private ReportServiceClientNode adminNode;

		private TreeNode currentNode;

		private int currentTier;

		private bool disableTrigger;
	}
}
