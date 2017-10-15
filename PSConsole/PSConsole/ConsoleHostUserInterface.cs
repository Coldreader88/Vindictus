using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Management.Automation;
using System.Management.Automation.Host;
using System.Security;
using System.Text;

namespace PSConsole
{
	internal class ConsoleHostUserInterface : PSHostUserInterface
	{
		public override Dictionary<string, PSObject> Prompt(string caption, string message, Collection<FieldDescription> descriptions)
		{
			this.Write(ConsoleColor.Gray, ConsoleColor.Black, caption + "\n" + message + " ");
			Dictionary<string, PSObject> dictionary = new Dictionary<string, PSObject>();
			foreach (FieldDescription current in descriptions)
			{
				string[] hotkeyAndLabel = ConsoleHostUserInterface.GetHotkeyAndLabel(current.Label);
				this.WriteLine(hotkeyAndLabel[1]);
				string text = Console.ReadLine();
				if (text == null)
				{
					return null;
				}
				dictionary[current.Name] = PSObject.AsPSObject(text);
			}
			return dictionary;
		}

		public override int PromptForChoice(string caption, string message, Collection<ChoiceDescription> choices, int defaultChoice)
		{
			this.WriteLine(ConsoleColor.Gray, ConsoleColor.Black, caption + "\n" + message + "\n");
			new Dictionary<string, PSObject>();
			string[,] array = ConsoleHostUserInterface.BuildHotkeysAndPlainLabels(choices);
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = 0; i < choices.Count; i++)
			{
				stringBuilder.Append(string.Format("|{0}> {1} ", array[0, i], array[1, i]));
			}
			stringBuilder.Append(string.Format("[Default is ({0}]", array[0, defaultChoice]));
			while (true)
			{
				this.WriteLine(ConsoleColor.Gray, ConsoleColor.Black, stringBuilder.ToString());
				string text = Console.ReadLine().Trim().ToUpper(CultureInfo.CurrentCulture);
				if (text.Length == 0)
				{
					break;
				}
				for (int j = 0; j < choices.Count; j++)
				{
					if (array[0, j] == text)
					{
						return j;
					}
				}
				this.WriteErrorLine("Invalid choice: " + text);
			}
			return defaultChoice;
		}

		private static string[] GetHotkeyAndLabel(string input)
		{
			string[] array = new string[]
			{
				string.Empty,
				string.Empty
			};
			string[] array2 = input.Split(new char[]
			{
				'&'
			});
			if (array2.Length == 2)
			{
				if (array2[1].Length > 0)
				{
					array[0] = array2[1][0].ToString().ToUpper(CultureInfo.CurrentCulture);
				}
				array[1] = (array2[0] + array2[1]).Trim();
			}
			else
			{
				array[1] = input;
			}
			return array;
		}

		private static string[,] BuildHotkeysAndPlainLabels(Collection<ChoiceDescription> choices)
		{
			string[,] array = new string[2, choices.Count];
			for (int i = 0; i < choices.Count; i++)
			{
				string[] hotkeyAndLabel = ConsoleHostUserInterface.GetHotkeyAndLabel(choices[i].Label);
				array[0, i] = hotkeyAndLabel[0];
				array[1, i] = hotkeyAndLabel[1];
			}
			return array;
		}

		public override PSCredential PromptForCredential(string caption, string message, string userName, string targetName)
		{
			throw new NotImplementedException("The method PromptForCredential() is not implemented by MyHost.");
		}

		public override PSCredential PromptForCredential(string caption, string message, string userName, string targetName, PSCredentialTypes allowedCredentialTypes, PSCredentialUIOptions options)
		{
			throw new NotImplementedException("The method PromptForCredential() is not implemented by MyHost.");
		}

		public override PSHostRawUserInterface RawUI
		{
			get
			{
				return this.consoleRawUI;
			}
		}

		public override string ReadLine()
		{
			return Console.ReadLine();
		}

		public override SecureString ReadLineAsSecureString()
		{
			throw new NotImplementedException("The method ReadLineAsSecureString() is not implemented by MyHost.");
		}

		public override void Write(string value)
		{
			Console.Write(value);
		}

		public override void Write(ConsoleColor foregroundColor, ConsoleColor backgroundColor, string value)
		{
			ConsoleColor foregroundColor2 = Console.ForegroundColor;
			ConsoleColor backgroundColor2 = Console.BackgroundColor;
			Console.ForegroundColor = foregroundColor;
			Console.BackgroundColor = backgroundColor;
			Console.Write(value);
			Console.ForegroundColor = foregroundColor2;
			Console.BackgroundColor = backgroundColor2;
		}

		public override void WriteLine(ConsoleColor foregroundColor, ConsoleColor backgroundColor, string value)
		{
			ConsoleColor foregroundColor2 = Console.ForegroundColor;
			ConsoleColor backgroundColor2 = Console.BackgroundColor;
			Console.ForegroundColor = foregroundColor;
			Console.BackgroundColor = backgroundColor;
			Console.WriteLine(value);
			Console.ForegroundColor = foregroundColor2;
			Console.BackgroundColor = backgroundColor2;
		}

		public override void WriteDebugLine(string message)
		{
			this.WriteLine(ConsoleColor.DarkYellow, ConsoleColor.Black, string.Format("DEBUG: {0}", message));
		}

		public override void WriteErrorLine(string value)
		{
			this.WriteLine(ConsoleColor.Red, ConsoleColor.Black, string.Format("[ERROR]{0}[/ERROR]", value));
		}

		public override void WriteLine()
		{
			Console.WriteLine();
		}

		public override void WriteLine(string value)
		{
			Console.WriteLine(value);
		}

		public override void WriteVerboseLine(string message)
		{
			this.WriteLine(ConsoleColor.Green, ConsoleColor.Black, string.Format("[INFO]VERBOSE: {0}[/INFO]", message));
		}

		public override void WriteWarningLine(string message)
		{
			this.WriteLine(ConsoleColor.Yellow, ConsoleColor.Black, string.Format("[WARNING]WARNING: {0}[/WARNING]", message));
		}

		public override void WriteProgress(long sourceId, ProgressRecord record)
		{
		}

		private ConsoleRawUserInterface consoleRawUI = new ConsoleRawUserInterface();
	}
}
