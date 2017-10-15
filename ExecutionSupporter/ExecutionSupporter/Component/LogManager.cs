using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AdminClientServiceCore.Messages;

namespace ExecutionSupporter.Component
{
	public class LogManager
	{
		public ExecutionSupportCore Core { get; set; }

		public ExecutionSupporterForm Form { get; set; }

		public LogManager(ExecutionSupportCore core)
		{
			this.Core = core;
			this.Form = core.Form;
		}

		public void AddLog(LogType type, string format, params object[] args)
		{
			this.Form.Invoke(new Action(delegate
			{
				string text = string.Format(format, args);
				string arg = DateTime.Now.ToString("yy/MM/dd HH:mm:ss");
				string text2 = string.Format("\r\n[{0} - {1}] {2}", arg, type, text);
				if (this.Form.TextLog.Lines.Length > 400)
				{
					this.Form.TextLog.Lines = this.Form.TextLog.Lines.Skip(200).ToArray<string>();
				}
				this.Form.TextLog.AppendText(text2);
				this.Form.TextLog.ScrollToCaret();
				try
				{
					using (HeroesSupportDataContext heroesSupportDataContext = new HeroesSupportDataContext())
					{
						heroesSupportDataContext.Output.InsertOnSubmit(new Output
						{
							Category = type.ToString(),
							Text = text,
							Time = DateTime.Now
						});
						heroesSupportDataContext.SubmitChanges();
					}
				}
				catch
				{
					this.Form.TextLog.AppendText(string.Format("\r\n[{0} - {1}] Error while saving log into db.", arg, LogType.ERROR));
				}
			}));
		}

		public void ClearUserCount()
		{
			this.Form.Invoke(new Action(delegate
			{
				this.Form.TextUserCount.Text = "Disconnected";
			}));
		}

		public void SetUserCount(AdminReportClientcountMessage message)
		{
			this.Form.Invoke(new Action(delegate
			{
				StringBuilder stringBuilder = new StringBuilder();
				string text = DateTime.Now.ToString("yy/MM/dd HH:mm:ss");
				stringBuilder.AppendFormat("[{0}] {1}/{2}(Wait {3})", new object[]
				{
					text,
					message.Value,
					message.Total,
					message.Waiting
				});
				foreach (KeyValuePair<string, int> keyValuePair in message.States)
				{
					stringBuilder.AppendFormat(" {0}({1})", keyValuePair.Key, keyValuePair.Value);
				}
				string text2 = stringBuilder.ToString();
				this.Form.TextUserCount.Text = text2;
				try
				{
					using (HeroesSupportDataContext heroesSupportDataContext = new HeroesSupportDataContext())
					{
						heroesSupportDataContext.UserCount.InsertOnSubmit(new UserCount
						{
							Text = text2,
							Time = DateTime.Now
						});
						heroesSupportDataContext.SubmitChanges();
					}
				}
				catch
				{
					this.Form.TextLog.AppendText(string.Format("\r\n[{0} - {1}] Error while saving user count into db.", text, LogType.ERROR));
				}
			}));
		}
	}
}
