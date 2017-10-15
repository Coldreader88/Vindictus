using System;
using UnifiedNetwork.Cooperation;

namespace UnifiedNetwork.Entity
{
	public class OperationSync<OperationT, ResultT> : ISynchronizable<ResultT>, ISynchronizable where OperationT : Operation
	{
		public IEntityProxy Connection { get; set; }

		public OperationT Operation { get; set; }

		public Func<OperationT, ResultT> GetReturnValue { get; set; }

		public bool Result { get; private set; }

		public ResultT ReturnValue { get; private set; }

		public void OnSync()
		{
			this.Operation.OnComplete += delegate(Operation op)
			{
				if (this.GetReturnValue != null)
				{
					this.ReturnValue = this.GetReturnValue(this.Operation);
				}
				this.Result = true;
				if (this.OnFinished != null)
				{
					this.OnFinished(this);
				}
			};
			this.Operation.OnFail += delegate(Operation op)
			{
				if (this.GetReturnValue != null)
				{
					this.ReturnValue = this.GetReturnValue(this.Operation);
				}
				this.Result = false;
				if (this.OnFinished != null)
				{
					this.OnFinished(this);
				}
			};
			this.Connection.RequestOperation(this.Operation);
		}

		public event Action<ISynchronizable> OnFinished;
	}
}
