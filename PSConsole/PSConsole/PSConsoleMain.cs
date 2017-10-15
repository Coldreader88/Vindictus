using System;
using System.IO;
using System.Management.Automation;
using System.Management.Automation.Runspaces;
using System.Text;
using System.Threading;

namespace PSConsole
{
	internal class PSConsoleMain
	{
		public bool ShouldExit
		{
			get
			{
				return this.shouldExit;
			}
			set
			{
				this.shouldExit = value;
			}
		}

		public int ExitCode
		{
			get
			{
				return this.exitCode;
			}
			set
			{
				this.exitCode = value;
			}
		}

		private PSConsoleMain()
		{
			this.consoleHost = new ConsoleHost(this);
			this.runSpace = RunspaceFactory.CreateRunspace(this.consoleHost);
			this.runSpace.Open();
		}

		private void executeHelper(string cmd, object input)
		{
			if (string.IsNullOrEmpty(cmd))
			{
				return;
			}
			lock (this.instanceLock)
			{
				this.currentPipeline = this.runSpace.CreatePipeline();
			}
			try
			{
				this.currentPipeline.Commands.AddScript(cmd);
				this.currentPipeline.Commands.Add("out-default");
				this.currentPipeline.Commands[0].MergeMyResults(PipelineResultTypes.Error, PipelineResultTypes.Output);
				if (input != null)
				{
					this.currentPipeline.Invoke(new object[]
					{
						input
					});
				}
				else
				{
					this.currentPipeline.Invoke();
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.ToString());
			}
			finally
			{
				lock (this.instanceLock)
				{
					this.currentPipeline.Dispose();
					this.currentPipeline = null;
				}
			}
		}

		private void Execute(string cmd)
		{
			try
			{
				this.executeHelper(cmd, null);
			}
			catch (RuntimeException ex)
			{
				this.executeHelper("$input | out-default", ex.ErrorRecord);
			}
		}

		private void HandleControlC(object sender, ConsoleCancelEventArgs e)
		{
			try
			{
				lock (this.instanceLock)
				{
					if (this.currentPipeline != null && this.currentPipeline.PipelineStateInfo.State == PipelineState.Running)
					{
						this.currentPipeline.Stop();
					}
				}
				e.Cancel = true;
			}
			catch (Exception ex)
			{
				this.consoleHost.UI.WriteErrorLine(ex.ToString());
			}
		}

		private void Run()
		{
			while (!this.ShouldExit)
			{
				this.consoleHost.UI.Write(ConsoleColor.Gray, ConsoleColor.Black, "[PSCon]: ");
				while (!this.consoleHost.UI.RawUI.KeyAvailable)
				{
					Thread.Sleep(10);
				}
				string cmd = this.consoleHost.UI.ReadLine();
				this.Execute(cmd);
			}
			Environment.Exit(this.ExitCode);
		}

		[STAThread]
		private static void Main(string[] args)
		{
			Console.Title = "PowerShell Console Host";
			PSConsoleMain pSConsoleMain = new PSConsoleMain();
			SyncTextReader @in = new SyncTextReader(new BufferedStream(Console.OpenStandardInput()), Encoding.Default);
			Console.SetIn(@in);
			if (args.Length < 1)
			{
				ConsoleColor foregroundColor = Console.ForegroundColor;
				Console.ForegroundColor = ConsoleColor.Gray;
				Console.WriteLine("    PowerShell Console Host");
				Console.WriteLine("    =====================================");
				Console.WriteLine("");
				Console.WriteLine("PowerShell.exe does not use StdIn/StdOut");
				Console.WriteLine("So I made console host");
				Console.WriteLine("PSConsole.exe [ps1 file] : Script Mode. Run PS Script and Exit.");
				Console.WriteLine("PSConsole.exe : Command line mode.");
				Console.WriteLine("");
				Console.ForegroundColor = foregroundColor;
				pSConsoleMain.Run();
				return;
			}
			if (!args[0].EndsWith(".ps1"))
			{
				Console.WriteLine("Script file must end with .ps1.");
				Environment.Exit(-1);
				return;
			}
			FileInfo fileInfo = new FileInfo(args[0]);
			if (fileInfo.Exists)
			{
				string text = fileInfo.FullName;
				if (args.Length > 1)
				{
					for (int i = 1; i < args.Length; i++)
					{
						text = text + " " + args[i];
					}
				}
				pSConsoleMain.Execute(text);
				Environment.Exit(pSConsoleMain.ExitCode);
				return;
			}
			Console.WriteLine("Script file [{0}] does not exist.", args[0]);
			Environment.Exit(-1);
		}

		private bool shouldExit;

		private int exitCode;

		private ConsoleHost consoleHost;

		private Runspace runSpace;

		private Pipeline currentPipeline;

		private object instanceLock = new object();
	}
}
