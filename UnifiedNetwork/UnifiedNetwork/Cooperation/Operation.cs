using System;
using Utility;

namespace UnifiedNetwork.Cooperation
{
	[Serializable]
	public abstract class Operation
	{
		[field: NonSerialized]
		public event Action<Operation> OnComplete;

		[field: NonSerialized]
		public event Action<Operation> OnFail;

		public abstract OperationProcessor RequestProcessor();

		public virtual int TimeOut
		{
			get
			{
				return 0;
			}
			set
			{
				throw new NotSupportedException();
			}
		}

		public bool Result
		{
			get
			{
				return this.result;
			}
			set
			{
				this.result = value;
			}
		}

		internal void IssueCompleteEvent()
		{
			Log<Operation>.Logger.DebugFormat("{0} completed", this);
			this.Result = true;
			if (this.OnComplete != null && !this.eventCalled)
			{
				this.eventCalled = true;
				try
				{
					this.OnComplete(this);
				}
				catch (Exception ex)
				{
					Log<Operation>.Logger.Error("Exception occured in complete event", ex);
				}
			}
		}

		internal void IssueFailEvent()
		{
			this.Result = false;
			if (this.OnFail != null && !this.eventCalled)
			{
				Log<Operation>.Logger.InfoFormat("{0} failed", this);
				this.eventCalled = true;
				try
				{
					this.OnFail(this);
				}
				catch (Exception ex)
				{
					Log<Operation>.Logger.Error("Exception occured in fail event", ex);
				}
			}
		}

		public override string ToString()
		{
			return base.GetType().Name;
		}

		[NonSerialized]
		private bool eventCalled;

		[NonSerialized]
		private bool result;
	}
}
