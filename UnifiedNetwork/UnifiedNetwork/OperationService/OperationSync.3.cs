using System;
using UnifiedNetwork.Cooperation;

namespace UnifiedNetwork.OperationService
{
	public class OperationSync<OperationT, ResultT> : ISynchronizable<ResultT>, ISynchronizable where OperationT : Operation
	{
		public string ServiceCategory { get; set; }

		public OperationT Operation { get; set; }

		public Service Service { get; set; }

		public bool Result { get; private set; }

		public ResultT ReturnValue { get; private set; }

		public Func<OperationT, ResultT> GetReturnValue { get; set; }

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
			this.Service.RequestOperation(this.ServiceCategory, this.Operation);
		}

		public event Action<ISynchronizable> OnFinished;
	}
}
