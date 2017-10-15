using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Windows.Forms;
using AdminClient.Properties;
using AdminClientServiceCore.Messages;
using Devcat.Core.Net.Message;
using Devcat.Core.Threading;

namespace AdminClient
{
	public partial class AdminClientForm : Form
	{
		public JobProcessor Thread
		{
			get
			{
				return this.thread;
			}
		}

		public MessageHandlerFactory MF
		{
			get
			{
				return this.mf;
			}
		}

		public AdminClientForm()
		{
			this.InitializeComponent();
			this.mf.Register<AdminClientNode>(AdminClientServiceOperationMessages.TypeConverters, "ProcessMessage");
			this.thread.Start();
		}

		private void OnConnected(object sender, EventArgs e)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendLine("Admin is connected to the server.");
			MessageBox.Show(stringBuilder.ToString(), "Successful connection", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
		}

		private void OnFailed(object sender, EventArgs e)
		{
			base.Enabled = false;
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendLine("Admin failed to connected to the server.");
			stringBuilder.AppendLine();
			stringBuilder.AppendLine("Exiting the program.");
			MessageBox.Show(stringBuilder.ToString(), "Failed connection!", MessageBoxButtons.OK, MessageBoxIcon.Hand);
			base.Close();
		}

		private void OnDisconnected(object sender, EventArgs e)
		{
			base.Enabled = false;
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendLine("Admin server has been disconnected");
			stringBuilder.AppendLine();
			stringBuilder.AppendLine("Exiting the program.");
			MessageBox.Show(stringBuilder.ToString(), "Connection has been disconnected!", MessageBoxButtons.OK, MessageBoxIcon.Hand);
			base.Close();
		}

		private void AdminClientForm_Load(object sender, EventArgs e)
		{
			this.adminNode = new AdminClientNode(this);
			this.adminNode.ConnectionSucceed += this.OnConnected;
			this.adminNode.ConnectionFailed += this.OnFailed;
			this.adminNode.Disconnected += this.OnDisconnected;
			base.Height = (this.formHeight = 190);
			this.NotifyButton.Visible = (this.NotifyTextBox.Visible = false);
			this.ClientCommandButton.Visible = (this.ConsoleCommandTextBox.Visible = false);
			this.ShurinkButton_a.Visible = (this.ExpandButton_a.Visible = true);
			this.ShurinkButton_b.Visible = (this.ExpandButton_b.Visible = false);
			this.ShurinkButton_c.Visible = (this.ExpandButton_c.Visible = false);
			if (Settings.Default.ConnectPort == 27011)
			{
				this.Text += " : Main Server";
				return;
			}
			if (Settings.Default.ConnectPort == 27010)
			{
				this.Text += " : Test Server";
				return;
			}
			if (Settings.Default.ConnectPort == 27009)
			{
				this.Text += " : Staging Server";
			}
		}

		private void RefreshUserCountButton_Click(object sender, EventArgs e)
		{
			this.adminNode.RequestUserCount();
		}

		private void NotifyButton_Click(object sender, EventArgs e)
		{
			base.Enabled = false;
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendLine(this.NotifyTextBox.Text);
			stringBuilder.AppendLine();
			stringBuilder.AppendLine("---");
			stringBuilder.AppendLine();
			stringBuilder.AppendLine("중간의 빈 줄은 자동적으로 무시됩니다.");
			stringBuilder.AppendLine("위 내용을 공지할까요?");
			DialogResult dialogResult = MessageBox.Show(stringBuilder.ToString(), "공지", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
			if (dialogResult == DialogResult.OK)
			{
				this.adminNode.RequestNotify(this.NotifyTextBox.Text);
			}
			base.Enabled = true;
		}

		private void ShutDownBottuon_Click(object sender, EventArgs e)
		{
			base.Enabled = false;
			StringBuilder stringBuilder = new StringBuilder();
			if (this.ShutDownTimeListBox.SelectedItem == null)
			{
				stringBuilder.AppendLine("Server is shuting down!");
				MessageBox.Show(stringBuilder.ToString(), "Server shutdown failed!", MessageBoxButtons.OK, MessageBoxIcon.Hand);
				base.Enabled = true;
				return;
			}
			char[] separator = new char[]
			{
				' '
			};
			string[] array = ((string)this.ShutDownTimeListBox.SelectedItem).Split(separator);
			stringBuilder.AppendLine(string.Format("{0}Shut down the server in seconds.", array[0]));
			stringBuilder.AppendLine();
			stringBuilder.AppendLine("Do you want to continue?");
			DialogResult dialogResult = MessageBox.Show(stringBuilder.ToString(), "Shuting down the server", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
			if (dialogResult == DialogResult.OK)
			{
				this.adminNode.RequestShutDown(int.Parse(array[0]) + 1, "");
			}
			base.Enabled = true;
		}

		private void KickUserButton_Click(object sender, EventArgs e)
		{
			base.Enabled = false;
			StringBuilder stringBuilder = new StringBuilder();
			if ((this.KickUserIDTextBox.Text != "" && this.KickCharacterIDTextBox.Text != "") || (this.KickCharacterIDTextBox.Text == "" && this.KickUserIDTextBox.Text == ""))
			{
				stringBuilder.AppendLine("ID 인식에 실패했습니다.");
				MessageBox.Show(stringBuilder.ToString(), "Kick failured!", MessageBoxButtons.OK, MessageBoxIcon.Hand);
				base.Enabled = true;
				return;
			}
			if (this.KickCharacterIDTextBox.Text != "")
			{
				stringBuilder.AppendLine(this.KickCharacterIDTextBox.Text);
			}
			else
			{
				stringBuilder.AppendLine(this.KickUserIDTextBox.Text);
			}
			stringBuilder.AppendLine();
			stringBuilder.AppendLine("---");
			stringBuilder.AppendLine();
			stringBuilder.AppendLine("이 유저를 Kick합니다!");
			DialogResult dialogResult = MessageBox.Show(stringBuilder.ToString(), "공지", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
			if (dialogResult == DialogResult.OK)
			{
				this.adminNode.RequestKick(this.KickUserIDTextBox.Text, this.KickCharacterIDTextBox.Text);
			}
			base.Enabled = true;
		}

		private void KickCharacterIDTextBox_Enter(object sender, EventArgs e)
		{
			this.KickCharacterIDTextBox.SelectAll();
		}

		private void KickUserIDTextBox_Enter(object sender, EventArgs e)
		{
			this.KickUserIDTextBox.SelectAll();
		}

		private void NotifyTextBox_Enter(object sender, EventArgs e)
		{
			this.NotifyTextBox.SelectAll();
		}

		public void UpdateUserCountText(int count, int total, int waiting, Dictionary<string, int> states)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendFormat("[{0}] {1:N0} Char / {2:N0} User ({3:N0} Wait) ", new object[]
			{
				DateTime.Now.ToString("yyMMdd HH:mm:ss"),
				count,
				total,
				waiting
			});
			foreach (KeyValuePair<string, int> keyValuePair in states)
			{
				stringBuilder.AppendFormat("[{0}={1}]", keyValuePair.Key, keyValuePair.Value);
			}
			stringBuilder.AppendLine();
			this.UserCountTextBox.AppendText(stringBuilder.ToString());
			if (this.LatestUserCount > 1000 && (this.LatestUserCount - total) * 20 > this.LatestUserCount)
			{
				string text = string.Format("동시 접속자 숫자가 {0:0.0}% 감소했습니다.", (double)(this.LatestUserCount - total) * 100.0 / (double)this.LatestUserCount);
				MessageBox.Show(text, "동시 접속자 수 감소!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
			this.LatestUserCount = total;
		}

		private void KickUserIDTextBox_TextChanged(object sender, EventArgs e)
		{
		}

		private void KickCharacterIDTextBox_TextChanged(object sender, EventArgs e)
		{
		}

		private void StopServiceButton_Click(object sender, EventArgs e)
		{
			base.Enabled = false;
			StringBuilder stringBuilder = new StringBuilder();
			if (this.StopServiceSelectComboBox.SelectedItem == null)
			{
				stringBuilder.AppendLine("정지 서비스가 선택되지 않았습니다!");
				MessageBox.Show(stringBuilder.ToString(), "긴급 정지 실패!", MessageBoxButtons.OK, MessageBoxIcon.Hand);
				base.Enabled = true;
				return;
			}
			char[] separator = new char[]
			{
				' '
			};
			string[] array = ((string)this.StopServiceSelectComboBox.SelectedItem).Split(separator);
			stringBuilder.AppendLine(string.Format("서비스를 긴급 정지합니다 : {0}.", array[0]));
			stringBuilder.AppendLine();
			stringBuilder.AppendLine("계속할까요?");
			DialogResult dialogResult = MessageBox.Show(stringBuilder.ToString(), "긴급 정지", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
			if (dialogResult == DialogResult.OK)
			{
				this.adminNode.RequestStopService(array[0], true);
			}
			base.Enabled = true;
		}

		private void ResumeServiceButton_Click(object sender, EventArgs e)
		{
			base.Enabled = false;
			StringBuilder stringBuilder = new StringBuilder();
			if (this.ResumeServiceSelectComboBox.SelectedItem == null)
			{
				stringBuilder.AppendLine("정지 서비스가 선택되지 않았습니다!");
				MessageBox.Show(stringBuilder.ToString(), "서비스 재개 실패!", MessageBoxButtons.OK, MessageBoxIcon.Hand);
				base.Enabled = true;
				return;
			}
			char[] separator = new char[]
			{
				' '
			};
			string[] array = ((string)this.ResumeServiceSelectComboBox.SelectedItem).Split(separator);
			stringBuilder.AppendLine(string.Format("서비스를 재개합니다 : {0}.", array[0]));
			stringBuilder.AppendLine();
			stringBuilder.AppendLine("계속할까요?");
			DialogResult dialogResult = MessageBox.Show(stringBuilder.ToString(), "서비스 재개", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
			if (dialogResult == DialogResult.OK)
			{
				this.adminNode.RequestStopService(array[0], false);
			}
			base.Enabled = true;
		}

		public void UpdateStoppedServices(List<string> target)
		{
			char[] separator = new char[]
			{
				' '
			};
			this.StopServiceSelectComboBox.SelectedItem = null;
			this.ResumeServiceSelectComboBox.SelectedItem = null;
			List<object> list = new List<object>();
			foreach (object obj in this.StopServiceSelectComboBox.Items)
			{
				if (obj is string)
				{
					string text = obj as string;
					string[] array = text.Split(separator);
					if (target.Contains(array[0]))
					{
						list.Add(obj);
					}
				}
			}
			foreach (object obj2 in list)
			{
				this.ResumeServiceSelectComboBox.Items.Add(obj2);
				this.StopServiceSelectComboBox.Items.Remove(obj2);
			}
			list.Clear();
			foreach (object obj3 in this.ResumeServiceSelectComboBox.Items)
			{
				if (obj3 is string)
				{
					string text2 = obj3 as string;
					string[] array2 = text2.Split(separator);
					if (!target.Contains(array2[0]))
					{
						list.Add(obj3);
					}
				}
			}
			foreach (object obj4 in list)
			{
				this.StopServiceSelectComboBox.Items.Add(obj4);
				this.ResumeServiceSelectComboBox.Items.Remove(obj4);
			}
			if (this.ResumeServiceSelectComboBox.Items.Count > 0)
			{
				this.ResumeServiceSelectComboBox.BackColor = Color.Red;
				this.StopServiceSelectComboBox.BackColor = Color.Red;
				return;
			}
			this.ResumeServiceSelectComboBox.BackColor = Color.White;
			this.StopServiceSelectComboBox.BackColor = Color.White;
		}

		private void GameServerCommandButton_Click(object sender, EventArgs e)
		{
			base.Enabled = false;
			string text = this.ConsoleCommandTextBox.Text.Split(new char[]
			{
				' '
			}).FirstOrDefault<string>();
			if (text != null)
			{
				string text2 = this.ConsoleCommandTextBox.Text.Substring(text.Length).Trim();
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.AppendLine(text);
				stringBuilder.AppendLine(text2);
				stringBuilder.AppendLine("---");
				stringBuilder.AppendLine();
				stringBuilder.AppendLine("위 명령을 실행할까요?");
				DialogResult dialogResult = MessageBox.Show(stringBuilder.ToString(), "서버 커맨드 실행", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
				if (dialogResult == DialogResult.OK)
				{
					this.adminNode.RequestServerCommand(text, text2);
				}
			}
			base.Enabled = true;
		}

		private void HostCommandButton_Click(object sender, EventArgs e)
		{
			base.Enabled = false;
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendLine(this.ConsoleCommandTextBox.Text);
			stringBuilder.AppendLine();
			stringBuilder.AppendLine("---");
			stringBuilder.AppendLine();
			stringBuilder.AppendLine("중간의 빈 줄은 자동적으로 무시됩니다.");
			stringBuilder.AppendLine("위 명령을 실행할까요?");
			DialogResult dialogResult = MessageBox.Show(stringBuilder.ToString(), "호스트 커맨드 실행", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
			if (dialogResult == DialogResult.OK)
			{
				this.adminNode.RequestConsoleCommand(this.ConsoleCommandTextBox.Text, true);
			}
			base.Enabled = true;
		}

		private void ClientCommandButton_Click(object sender, EventArgs e)
		{
			base.Enabled = false;
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendLine(this.ConsoleCommandTextBox.Text);
			stringBuilder.AppendLine();
			stringBuilder.AppendLine("---");
			stringBuilder.AppendLine();
			stringBuilder.AppendLine("중간의 빈 줄은 자동적으로 무시됩니다.");
			stringBuilder.AppendLine("위 명령을 실행할까요?");
			DialogResult dialogResult = MessageBox.Show(stringBuilder.ToString(), "클라이언트 커맨드 실행", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
			if (dialogResult == DialogResult.OK)
			{
				this.adminNode.RequestConsoleCommand(this.ConsoleCommandTextBox.Text, false);
			}
			base.Enabled = true;
		}

		private void ItemFestivalButton_Click(object sender, EventArgs e)
		{
			base.Enabled = false;
			StringBuilder stringBuilder = new StringBuilder();
			string text = this.ItemFestivalClassNameTextBox.Text;
			if (text == "")
			{
				text = "gold";
			}
			int num = (int)this.ItemFestivalAmountUpDown.Value;
			if (num <= 0)
			{
				num = 1;
			}
			string text2 = this.ItemFestivalMessageTextBox.Text;
			if (text2 == "")
			{
				stringBuilder.AppendLine("메시지가 없습니다!");
				MessageBox.Show(stringBuilder.ToString(), "아이템 지급 이벤트 실패!", MessageBoxButtons.OK, MessageBoxIcon.Hand);
				base.Enabled = true;
				return;
			}
			stringBuilder.AppendLine(text2);
			stringBuilder.AppendLine("---");
			stringBuilder.AppendLine(string.Format("{0} {1:n}개를 유저 전체에게 지급합니다.", text, num));
			stringBuilder.AppendLine("명령을 실행할까요?");
			DialogResult dialogResult = MessageBox.Show(stringBuilder.ToString(), "아이템 지급 이벤트", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
			if (dialogResult == DialogResult.OK)
			{
				this.adminNode.RequestItemFestival(text, num, text2, this.CafeCheckBox.Checked);
			}
			base.Enabled = true;
		}

		private void ExpandButton_a_Click(object sender, EventArgs e)
		{
			try
			{
				using (GMAccountDBDataContext gmaccountDBDataContext = new GMAccountDBDataContext())
				{
					IQueryable<GMAccounts> source = from line in gmaccountDBDataContext.GMAccounts
					where line.ID == WindowsIdentity.GetCurrent().Name.ToString()
					select line;
					if (source.Count<GMAccounts>() == 0)
					{
						MessageBox.Show("접근 권한이 없습니다!", "에러", MessageBoxButtons.OK, MessageBoxIcon.Hand);
					}
					else
					{
						base.Height = (this.formHeight = 370);
						this.NotifyButton.Visible = (this.NotifyTextBox.Visible = true);
						this.ClientCommandButton.Visible = (this.ConsoleCommandTextBox.Visible = false);
						this.ShurinkButton_a.Visible = (this.ExpandButton_a.Visible = false);
						this.ShurinkButton_b.Visible = (this.ExpandButton_b.Visible = true);
						this.ShurinkButton_c.Visible = (this.ExpandButton_c.Visible = false);
					}
				}
			}
			catch
			{
				MessageBox.Show("DB 접근에 실패했습니다!", "에러", MessageBoxButtons.OK, MessageBoxIcon.Hand);
			}
		}

		private void ExpandButton_b_Click(object sender, EventArgs e)
		{
			try
			{
				using (GMAccountDBDataContext gmaccountDBDataContext = new GMAccountDBDataContext())
				{
					IQueryable<GMAccounts> source = from line in gmaccountDBDataContext.GMAccounts
					where line.ID == WindowsIdentity.GetCurrent().Name.ToString()
					select line;
					if (source.Count<GMAccounts>() == 0)
					{
						MessageBox.Show("접근 권한이 없습니다!", "에러", MessageBoxButtons.OK, MessageBoxIcon.Hand);
					}
					else
					{
						base.Height = (this.formHeight = 569);
						this.NotifyButton.Visible = (this.NotifyTextBox.Visible = true);
						this.ClientCommandButton.Visible = (this.ConsoleCommandTextBox.Visible = true);
						this.ShurinkButton_a.Visible = (this.ExpandButton_a.Visible = false);
						this.ShurinkButton_b.Visible = (this.ExpandButton_b.Visible = false);
						this.ShurinkButton_c.Visible = (this.ExpandButton_c.Visible = true);
					}
				}
			}
			catch
			{
				MessageBox.Show("DB 접근에 실패했습니다!", "에러", MessageBoxButtons.OK, MessageBoxIcon.Hand);
			}
		}

		private void ShurinkButton_c_Click(object sender, EventArgs e)
		{
			base.Height = (this.formHeight = 370);
			this.NotifyButton.Visible = (this.NotifyTextBox.Visible = true);
			this.ClientCommandButton.Visible = (this.ConsoleCommandTextBox.Visible = false);
			this.ShurinkButton_a.Visible = (this.ExpandButton_a.Visible = false);
			this.ShurinkButton_b.Visible = (this.ExpandButton_b.Visible = true);
			this.ShurinkButton_c.Visible = (this.ExpandButton_c.Visible = false);
		}

		private void ShurinkButton_b_Click(object sender, EventArgs e)
		{
			base.Height = (this.formHeight = 190);
			this.NotifyButton.Visible = (this.NotifyTextBox.Visible = false);
			this.ClientCommandButton.Visible = (this.ConsoleCommandTextBox.Visible = false);
			this.ShurinkButton_a.Visible = (this.ExpandButton_a.Visible = true);
			this.ShurinkButton_b.Visible = (this.ExpandButton_b.Visible = false);
			this.ShurinkButton_c.Visible = (this.ExpandButton_c.Visible = false);
		}

		private void CopyToClipBoardButton_Click(object sender, EventArgs e)
		{
			char[] separator = new char[]
			{
				'@'
			};
			string[] array = this.UserCountTextBox.Text.Replace("\r\n", "@").Split(separator);
			if (array.Length > 2)
			{
				Clipboard.SetText(array[array.Length - 2]);
			}
		}

		private void AdminClientForm_Resize(object sender, EventArgs e)
		{
			base.Height = this.formHeight;
		}

		private void ExtendExpireTimeButton_Click(object sender, EventArgs e)
		{
			base.Enabled = false;
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendLine(string.Format("{0} 부터 {1} 분 연장", DateTime.Now, this.CashExtendMinutesUpDown1.Value));
			stringBuilder.AppendLine("---");
			stringBuilder.AppendLine("연장 작업은 대단히 오래 걸릴 수 있습니다. 중간에 프로그램을 끄지 마세요.");
			stringBuilder.AppendLine("캐시 아이템 연장 명령을 실행할까요?");
			DialogResult dialogResult = MessageBox.Show(stringBuilder.ToString(), "캐시 아이템 연장", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
			if (dialogResult == DialogResult.OK && this.CashExtendMinutesUpDown1.Value > 0m)
			{
				using (GMAccountDBDataContext gmaccountDBDataContext = new GMAccountDBDataContext())
				{
					gmaccountDBDataContext.ExtendCashItems(new DateTime?(DateTime.UtcNow), new int?((int)this.CashExtendMinutesUpDown1.Value));
				}
			}
			base.Enabled = true;
		}

		private JobProcessor thread = new JobProcessor();

		private MessageHandlerFactory mf = new MessageHandlerFactory();

		private AdminClientNode adminNode;

		private int formHeight = 190;

		private int LatestUserCount;
	}
}
