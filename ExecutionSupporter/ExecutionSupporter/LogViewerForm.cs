using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ExecutionSupporter.Properties;

namespace ExecutionSupporter
{
	public partial class LogViewerForm : Form
	{
		public LogViewerForm()
		{
			this.InitializeComponent();
		}

		private IEnumerable<string> GetAllFiles(string path)
		{
			List<string> subDirs;
			List<string> files;
			try
			{
				subDirs = Directory.GetDirectories(path).ToList<string>();
				files = Directory.GetFiles(path).ToList<string>();
			}
			catch
			{
				yield break;
			}
			foreach (string dir in subDirs)
			{
				foreach (string file in this.GetAllFiles(dir))
				{
					yield return Path.Combine(Path.GetFileName(dir), file);
				}
			}
			foreach (string file2 in files)
			{
				yield return Path.GetFileName(file2);
			}
			yield break;
		}

		private void LogViewerForm_Load(object sender, EventArgs e)
		{
			this.LoadFiles();
		}

		public void LoadFiles()
		{
			this.ListFile.Items.Clear();
			string text = this.TextFilter.Text;
			foreach (string text2 in this.GetAllFiles(Settings.Default.LogDir))
			{
				if (text2.ToLower().Contains(text.ToLower()))
				{
					FileInfo fileInfo = new FileInfo(Path.Combine(Settings.Default.LogDir, text2));
					if (fileInfo.Length > 0L)
					{
						this.ListFile.Items.Add(text2);
					}
				}
			}
		}

		private void ListFile_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (this.ListFile.SelectedItem == null)
			{
				return;
			}
			string path = Path.Combine(Settings.Default.LogDir, this.ListFile.SelectedItem.ToString());
			StreamReader streamReader = new StreamReader(path, Encoding.Default);
			this.TextContent.Text = streamReader.ReadToEnd();
		}

		private void TextFilter_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (e.KeyChar == '\r')
			{
				this.LoadFiles();
			}
		}
	}
}
