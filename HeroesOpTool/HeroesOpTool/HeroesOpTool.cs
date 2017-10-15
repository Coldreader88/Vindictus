using System;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Serialization;
using Devcat.Core;
using Devcat.Core.Threading;
using HeroesOpTool.RCUser;
using HeroesOpTool.RCUser.ServerMonitorSystem;

namespace HeroesOpTool
{
	internal class HeroesOpTool
	{
		public static JobProcessor Thread { get; private set; }

		[STAThread]
		private static void Main(string[] args)
		{
			Application.EnableVisualStyles();
			string text = "HeroesOpToolConfig.xml";
			if (args.Length > 0)
			{
				text = args[0];
			}
			XmlTextReader xmlTextReader = null;
			Configuration configuration;
			try
			{
				xmlTextReader = new XmlTextReader(text);
				configuration = (Configuration)new System.Xml.Serialization.XmlSerializer(typeof(Configuration)).Deserialize(xmlTextReader);
			}
			catch (Exception ex)
			{
				Utility.ShowErrorMessage(string.Format("Error occured while reading configuration file : {0}\nSource message : {1}", text, ex.Message));
				return;
			}
			finally
			{
				if (xmlTextReader != null)
				{
					xmlTextReader.Close();
				}
			}
			try
			{
				LocalizeText.LoadText(configuration.TextFile);
				LocalizeText.SetLocale(configuration.Language);
			}
			catch (Exception ex2)
			{
				Utility.ShowErrorMessage("Error occured while reading text file! - " + ex2.ToString());
				return;
			}
			HeroesOpTool.Thread = new JobProcessor();
			HeroesOpTool.Thread.ExceptionOccur += delegate(object sender, EventArgs<Exception> e)
			{
				Utility.ShowErrorMessage(e.Value.ToString());
			};
			HeroesOpTool.Thread.Start();
			RCUserHandler rcUser = new RCUserHandler(configuration);
			ServerMonitorControl serverMonitorControl = new ServerMonitorControl(rcUser);
			serverMonitorControl.CreateControl();
			Login login = new Login(configuration, rcUser, serverMonitorControl);
			if (login.ShowDialog() == DialogResult.OK)
			{
				try
				{
					Application.Run(new MainForm(configuration, rcUser, serverMonitorControl));
				}
				catch (Exception ex3)
				{
					Utility.ShowErrorMessage(ex3.ToString());
				}
			}
			HeroesOpTool.Thread.Stop();
		}
	}
}
