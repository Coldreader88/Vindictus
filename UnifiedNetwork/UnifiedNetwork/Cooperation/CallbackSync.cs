using System;
using Utility;

namespace UnifiedNetwork.Cooperation
{
	public class CallbackSync<ResultT> : ISynchronizable<ResultT>, ISynchronizable
	{
		public CallbackSync(string name, Action<CallbackSync<ResultT>, Action> func)
		{
			this.name = name;
			this.func = func;
		}

		private void Callback()
		{
			try
			{
				if (this.OnFinished != null)
				{
					this.OnFinished(this);
				}
			}
			catch (Exception ex)
			{
				Log<FunctionSync>.Logger.ErrorFormat("Error while FunctionSync {0} OnFinished : {1}", this.name, ex.Message);
			}
		}

		public void OnSync()
		{
			try
			{
				if (this.func != null)
				{
					this.func(this, new Action(this.Callback));
				}
				else
				{
					this.Result = false;
				}
			}
			catch (Exception ex)
			{
				this.Result = false;
				Log<CallbackSync<ResultT>>.Logger.ErrorFormat("Error while FunctionSync {0} : {1}", this.name, ex.Message);
			}
		}

		public event Action<ISynchronizable> OnFinished;

		public bool Result { get; set; }

		public ResultT ReturnValue { get; set; }

		private string name;

		private Action<CallbackSync<ResultT>, Action> func;
	}
}
