using System;
using System.Globalization;
using System.Management.Automation.Host;
using System.Threading;

namespace PSConsole
{
	internal class ConsoleHost : PSHost
	{
		public ConsoleHost(PSConsoleMain program)
		{
			this.program = program;
		}

		public override CultureInfo CurrentCulture
		{
			get
			{
				return this.originalCultureInfo;
			}
		}

		public override CultureInfo CurrentUICulture
		{
			get
			{
				return this.originalUICultureInfo;
			}
		}

		public override void EnterNestedPrompt()
		{
			throw new NotImplementedException("Cannot suspend the shell, EnterNestedPrompt() method is not implemented by MyHost.");
		}

		public override void ExitNestedPrompt()
		{
			throw new NotImplementedException("The ExitNestedPrompt() method is not implemented by MyHost.");
		}

		public override Guid InstanceId
		{
			get
			{
				return ConsoleHost.instanceId;
			}
		}

		public override string Name
		{
			get
			{
				return "MySampleConsoleHostImplementation";
			}
		}

		public override void NotifyBeginApplication()
		{
		}

		public override void NotifyEndApplication()
		{
		}

		public override void SetShouldExit(int exitCode)
		{
			this.program.ShouldExit = true;
			this.program.ExitCode = exitCode;
		}

		public override PSHostUserInterface UI
		{
			get
			{
				return this.hostUserInterface;
			}
		}

		public override Version Version
		{
			get
			{
				return new Version(1, 0, 0, 0);
			}
		}

		private PSConsoleMain program;

		private CultureInfo originalCultureInfo = Thread.CurrentThread.CurrentCulture;

		private CultureInfo originalUICultureInfo = Thread.CurrentThread.CurrentUICulture;

		private static Guid instanceId = Guid.NewGuid();

		private ConsoleHostUserInterface hostUserInterface = new ConsoleHostUserInterface();
	}
}
