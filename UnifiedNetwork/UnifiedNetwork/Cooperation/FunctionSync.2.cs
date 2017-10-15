using System;
using Utility;

namespace UnifiedNetwork.Cooperation
{
	public class FunctionSync<ResultT> : ISynchronizable<ResultT>, ISynchronizable
	{
		public FunctionSync(string name, Action<FunctionSync<ResultT>> func)
		{
			this.name = name;
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
				Log<FunctionSync>.Logger.ErrorFormat("Error while FunctionSync {0} : {1}", this.name, ex.Message);
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
				Log<FunctionSync>.Logger.ErrorFormat("Error while FunctionSync {0} OnFinished : {1}", this.name, ex2.Message);
			}
		}

		public event Action<ISynchronizable> OnFinished;

		public bool Result { get; set; }

		public ResultT ReturnValue { get; set; }

		private string name;

		private Action<FunctionSync<ResultT>> func;
	}
}
