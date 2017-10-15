using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Drawing;
using System.IO;
using System.Net;
using System.Windows.Forms;
using AdminService.Forms;
using Devcat.Core.Threading;
using ServiceCore;
using ServiceCore.EndPointNetwork;
using ServiceCore.ExecutionServiceOperations;
using ServiceCore.FrontendServiceOperations;
using UnifiedNetwork.Cooperation;

namespace AdminService
{
	public partial class AdminService : Form
	{
		public AdminService()
		{
			this.InitializeComponent();
			this.proxys = new Dictionary<int, ExecutionServiceProxy>();
		}

		private void QueryService(IPEndPoint info)
		{
			QueryService operation = new QueryService();
			operation.OnComplete += delegate(Operation op)
			{
				ExecAppDomain execop = new ExecAppDomain("", 0);
				execop.OnComplete += delegate(Operation op2)
				{
					this.ExecutionServices.Invoke(new AdminService.InvokeFormDelegate(delegate
					{
						this.ExecutionServices.Items.Add(info);
						int key = this.ExecutionServices.Items.Count - 1;
						this.proxys[key] = new ExecutionServiceProxy(info, operation.ServiceClasses, execop.LoadedAppDomains);
						this.ExecutionServices.Invalidate();
					}));
				};
				this.Service.RequestOperation(info, execop);
			};
			this.Service.RequestOperation(info, operation);
		}

		private void QueryButton_Click(object sender, EventArgs e)
		{
			this.ExecutionServices.Items.Clear();
			this.Services.Items.Clear();
			this.Service.QueryService("ExecutionServiceCore.ExecutionService", delegate(IEnumerable<IPEndPoint> infos)
			{
				IPEndPoint info;
				foreach (IPEndPoint info2 in infos)
				{
					info = info2;
					this.Services.Invoke(new AdminService.InvokeFormDelegate(delegate
					{
						this.QueryService(info);
					}));
				}
			});
		}

		private void AdminService_Load(object sender, EventArgs e)
		{
			try
			{
				this.Service = new AdaptorService();
				this.Service.Disposed += delegate(object sender2, EventArgs e2)
				{
					this.Service.Thread.Stop();
				};
				string text = ConfigurationManager.AppSettings["NamingService IP"];
				string text2 = ConfigurationManager.AppSettings["NamingService Port"];
				if (text == null || text2 == null)
				{
					text = "localhost";
					text2 = "42";
				}
				ServiceInvoker.StartService(text, text2, this.Service);
				this.CheckInterval.Start();
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
				Application.Exit();
			}
		}

		private void AdminService_FormClosed(object sender, FormClosedEventArgs e)
		{
			try
			{
				this.Service.Thread.Enqueue(Job.Create(new Action(this.Service.Dispose)));
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		private void ExecutionServices_SelectedIndexChanged(object sender, EventArgs e)
		{
			this.Services.Items.Clear();
			this.AppDomains.Items.Clear();
			int selectedIndex = this.ExecutionServices.SelectedIndex;
			if (this.proxys.ContainsKey(selectedIndex))
			{
				ExecutionServiceProxy executionServiceProxy = this.proxys[selectedIndex];
				foreach (string item in executionServiceProxy.Services)
				{
					this.Services.Items.Add(item);
				}
				foreach (string item2 in executionServiceProxy.AppDomains)
				{
					this.AppDomains.Items.Add(item2);
				}
			}
			this.Services.Invalidate();
		}

		private void StartButton_Click(object sender, EventArgs e)
		{
			int selectedIndex = this.ExecutionServices.SelectedIndex;
			if (selectedIndex < 0)
			{
				return;
			}
			ExecutionServiceProxy executionServiceProxy;
			if (!this.proxys.TryGetValue(selectedIndex, out executionServiceProxy))
			{
				MessageBox.Show("익스큐션 서비스가 이상해요 " + selectedIndex);
				return;
			}
			foreach (object obj in this.Services.SelectedItems)
			{
				string text = (string)obj;
				if (text == "" || text == null)
				{
					MessageBox.Show("서비스가 이상해요 " + text);
					break;
				}
				ExecAppDomain execAppDomain = new ExecAppDomain(text, 1);
				execAppDomain.OnComplete += delegate(Operation op)
				{
					this.AppDomains.Invoke(new AdminService.InvokeFormDelegate(delegate
					{
						ExecAppDomain execAppDomain2 = op as ExecAppDomain;
						if (op == null)
						{
							MessageBox.Show("에러!!");
							return;
						}
						this.AppDomains.Items.Clear();
						foreach (string item in execAppDomain2.LoadedAppDomains)
						{
							this.AppDomains.Items.Add(item);
						}
					}));
					this.AppDomains.Invoke(new AdminService.InvokeFormDelegate(this.AppDomains.Invalidate));
				};
				this.Service.RequestOperation(executionServiceProxy.ExecutionServiceInfo, execAppDomain);
			}
		}

		private void StopButton_Click(object sender, EventArgs e)
		{
			int selectedIndex = this.ExecutionServices.SelectedIndex;
			if (selectedIndex < 0)
			{
				return;
			}
			ExecutionServiceProxy executionServiceProxy;
			if (!this.proxys.TryGetValue(selectedIndex, out executionServiceProxy))
			{
				return;
			}
			foreach (object obj in this.AppDomains.SelectedItems)
			{
				string text = (string)obj;
				if (text == null)
				{
					break;
				}
				ExecAppDomain execAppDomain = new ExecAppDomain(text, 0);
				execAppDomain.OnComplete += delegate(Operation op)
				{
					this.AppDomains.Invoke(new AdminService.InvokeFormDelegate(delegate
					{
						ExecAppDomain execAppDomain2 = op as ExecAppDomain;
						if (op == null)
						{
							MessageBox.Show("에러!!");
							return;
						}
						this.AppDomains.Items.Clear();
						foreach (string item in execAppDomain2.LoadedAppDomains)
						{
							this.AppDomains.Items.Add(item);
						}
					}));
					this.AppDomains.Invoke(new AdminService.InvokeFormDelegate(this.AppDomains.Invalidate));
				};
				this.Service.RequestOperation(executionServiceProxy.ExecutionServiceInfo, execAppDomain);
			}
		}

		private void Notice_Click(object sender, EventArgs e)
		{
			NotifyClient notifyClient = new NotifyClient
			{
				Category = SystemMessageCategory.Notice,
				Message = new HeroesString(this.textBoxNotice.Text)
			};
			notifyClient.OnComplete += delegate(Operation op)
			{
				this.Result.Text = "메세지 전송 성공!";
			};
			notifyClient.OnFail += delegate(Operation op)
			{
				this.Result.Text = "메세지 전송 실패!";
			};
			this.Service.Thread.Enqueue(Job.Create<string, NotifyClient>(new Action<string, NotifyClient>(this.Service.RequestOperation), "FrontendServiceCore.FrontendService", notifyClient));
		}

		private void KickUser_Click(object sender, EventArgs e)
		{
			ExecCommand execCommand = new ExecCommand("Kick", this.textBoxKick.Text);
			execCommand.OnComplete += delegate(Operation op)
			{
				this.Result2.Text = "Kick 성공!";
			};
			execCommand.OnFail += delegate(Operation op)
			{
				this.Result2.Text = "Kick 실패!";
			};
			this.Service.Thread.Enqueue(Job.Create<string, ExecCommand>(new Action<string, ExecCommand>(this.Service.RequestOperation), "FrontendServiceCore.FrontendService", execCommand));
		}

		private void CCU_Click(object sender, EventArgs e)
		{
			if (this.stateCounts == null)
			{
				return;
			}
			List<string> list = new List<string>();
			foreach (KeyValuePair<string, int> keyValuePair in this.stateCounts)
			{
				list.Add(string.Format("{0} : {1}", keyValuePair.Key, keyValuePair.Value));
			}
			UserListForm userListForm = new UserListForm();
			userListForm.ListBox.BeginUpdate();
			userListForm.ListBox.DataSource = list;
			userListForm.ListBox.EndUpdate();
			userListForm.Show();
		}

		private void Quests_Click(object sender, EventArgs e)
		{
		}

		private void Parties_Click(object sender, EventArgs e)
		{
		}

		private void CheckInterval_Tick(object sender, EventArgs e)
		{
			QueryClientCount queryop = new QueryClientCount();
			queryop.OnComplete += delegate(Operation op)
			{
				this.Invoke(new AdminService.InvokeFormDelegate(delegate
				{
					this.CCU.Text = string.Format("동시 접속자 : {0}", queryop.StateCounts[0]["Count"]);
					this.stateCounts = queryop.StateCounts[0];
					if (this.openSuccess)
					{
						try
						{
							using (FileStream fileStream = File.Open(this.filename, FileMode.Append))
							{
								this.openSuccess = (fileStream != null);
								if (this.openSuccess)
								{
									using (StreamWriter streamWriter = new StreamWriter(fileStream))
									{
										streamWriter.Write("{0},{1},", DateTime.Now.ToString(), queryop.StateCounts[0]["Count"]);
										foreach (KeyValuePair<int, Dictionary<string, int>> keyValuePair in queryop.StateCounts)
										{
											streamWriter.Write("{0}:{1},", keyValuePair.Key, keyValuePair.Value);
										}
										streamWriter.WriteLine();
									}
									this.ErrorLog.Text = string.Format("Last update : {0}", DateTime.Now.ToString());
								}
							}
						}
						catch (Exception ex)
						{
							this.ErrorLog.Text = ex.Message;
						}
					}
				}));
			};
			this.Service.Thread.Enqueue(Job.Create<string, QueryClientCount>(new Action<string, QueryClientCount>(this.Service.RequestOperation), "FrontendServiceCore.FrontendService", queryop));
		}

		private void LogFileUpdateButton_Click(object sender, EventArgs e)
		{
			this.filename = this.LogFileNameBox.Text;
			try
			{
				using (FileStream fileStream = File.Open(this.filename, FileMode.OpenOrCreate))
				{
					this.openSuccess = (fileStream != null);
					this.ErrorLog.Text = "Start Logging...";
				}
			}
			catch (Exception ex)
			{
				this.openSuccess = false;
				this.ErrorLog.Text = ex.Message;
			}
			finally
			{
				string arg;
				if (this.openSuccess)
				{
					arg = "Logging";
				}
				else
				{
					arg = "Error";
				}
				this.LogFileName.Text = string.Format("{0} : {1}", this.filename, arg);
			}
		}

		private Dictionary<int, ExecutionServiceProxy> proxys;

		private AdaptorService Service;

		private IDictionary<string, int> stateCounts;

		private string filename;

		private bool openSuccess;

		private delegate void InvokeFormDelegate();
	}
}
