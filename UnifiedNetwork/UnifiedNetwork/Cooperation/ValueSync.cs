using System;
using Utility;

namespace UnifiedNetwork.Cooperation
{
	public class ValueSync<ResultT> : ISynchronizable<ResultT>, ISynchronizable
	{
		public ValueSync(ResultT value)
		{
			this.ReturnValue = value;
		}

		public ResultT ReturnValue { get; private set; }

		public void OnSync()
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
				Log<ValueSync<ResultT>>.Logger.Error("Error while OnFinished : ", ex);
			}
		}

		public event Action<ISynchronizable> OnFinished;

		public bool Result
		{
			get
			{
				return true;
			}
		}
	}
}
