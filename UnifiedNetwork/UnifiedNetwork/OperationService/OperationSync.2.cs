using System;
using UnifiedNetwork.Cooperation;

namespace UnifiedNetwork.OperationService
{
	public class OperationSync<OperationT> : ISynchronizable<OperationT>, ISynchronizable where OperationT : Operation
	{
		public string ServiceCategory { get; set; }

		public OperationT Operation { get; set; }

		public Service Service { get; set; }

		public bool Result { get; private set; }

		public OperationT ReturnValue
		{
			get
			{
				return this.Operation;
			}
		}

		public void OnSync()
		{
			this.Operation.OnComplete += delegate(Operation op)
			{
				this.Result = true;
				if (this.OnFinished != null)
				{
					this.OnFinished(this);
				}
			};
			this.Operation.OnFail += delegate(Operation op)
			{
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
