using System;
using System.Linq;

namespace ExecutionSupporter.Component
{
	public class InputManager
	{
		private ExecutionSupportCore Core { get; set; }

		private ExecutionSupporterForm Form { get; set; }

		public InputManager(ExecutionSupportCore core)
		{
			this.Core = core;
			this.Form = this.Core.Form;
		}

		public void DoCommand(string cmd)
		{
			try
			{
				using (HeroesSupportDataContext heroesSupportDataContext = new HeroesSupportDataContext())
				{
					heroesSupportDataContext.Input.InsertOnSubmit(new Input
					{
						Command = cmd,
						ExecuteTime = DateTime.Now,
						Executed = null,
						Time = DateTime.Now
					});
					heroesSupportDataContext.SubmitChanges();
				}
				this.CheckCommand();
			}
			catch (Exception ex)
			{
				this.Core.LogManager.AddLog(LogType.ERROR, "Error occurred while adding command : {0}", new object[]
				{
					ex.Message
				});
			}
		}

		public void CheckCommand()
		{
			try
			{
				using (HeroesSupportDataContext heroesSupportDataContext = new HeroesSupportDataContext())
				{
					IOrderedQueryable<Input> orderedQueryable = from cmd in heroesSupportDataContext.Input
					where cmd.Executed == null && cmd.ExecuteTime <= DateTime.Now
					orderby cmd.ExecuteTime
					select cmd;
					foreach (Input input in orderedQueryable)
					{
						this.RunCommand(input);
					}
					heroesSupportDataContext.SubmitChanges();
				}
			}
			catch (Exception ex)
			{
				this.Core.LogManager.AddLog(LogType.ERROR, "Error occurred while checking command : {0}", new object[]
				{
					ex.Message
				});
			}
		}

		public void RunCommand(Input input)
		{
			if (input.Executed == null && input.ExecuteTime <= DateTime.Now)
			{
				this.Core.LogManager.AddLog(LogType.INFO, "> {0}", new object[]
				{
					input.Command
				});
				if (this.Core.ProcessCmd(input.Command))
				{
					input.Executed = "Executed";
					return;
				}
				input.Executed = "Failed";
			}
		}
	}
}
