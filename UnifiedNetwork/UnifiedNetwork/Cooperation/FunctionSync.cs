using System;
using Utility;

namespace UnifiedNetwork.Cooperation
{
	public class FunctionSync : ISynchronizable
	{
		public FunctionSync(string name, Action<FunctionSync> func)
		{
			this.Name = name;
			this.func = func;
		}

		public void OnSync()
		{
			try
			{
				if (this.func != null)
				{
					this.func(this);
				}
				else
				{
					this.Result = false;
				}
			}
			catch (Exception ex)
			{
				this.Result = false;
				Log<FunctionSync>.Logger.ErrorFormat("Error while FunctionSync {0} : {1}", this.Name, ex.Message);
			}
			try
			{
				if (this.OnFinished != null)
				{
					this.OnFinished(this);
				}
			}
			catch (Exception ex2)
			{
				Log<FunctionSync>.Logger.ErrorFormat("Error while FunctionSync {0} OnFinished : {1}", this.Name, ex2.Message);
			}
		}

		public event Action<ISynchronizable> OnFinished;

		public string Name { get; set; }

		public bool Result { get; set; }

		private Action<FunctionSync> func;
	}
}
