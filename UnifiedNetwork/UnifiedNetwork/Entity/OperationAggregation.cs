using System;
using System.Collections.Generic;
using Devcat.Core.Threading;
using UnifiedNetwork.Cooperation;

namespace UnifiedNetwork.Entity
{
	public class OperationAggregation : ISynchronizable
	{
		public int Count
		{
			get
			{
				return this.operationCompleteStatus.Count;
			}
		}

		public bool Empty
		{
			get
			{
				return this.Count == 0;
			}
		}

		public OperationAggregation(Service service)
		{
			this.service = service;
			this.Result = true;
		}

		public void AddElement(IEntityProxy conn, Operation op)
		{
			OperationAggregation.SyncElement newElement = new OperationAggregation.SyncElement(conn, op);
			this.operationCompleteStatus[newElement] = false;
			op.OnComplete += delegate(Operation x)
			{
				this.operationCompleteStatus[newElement] = true;
				this.CheckComplete();
			};
			op.OnFail += delegate(Operation x)
			{
				this.operationCompleteStatus[newElement] = true;
				this.Result = false;
				this.CheckComplete();
			};
		}

		private void CheckComplete()
		{
			if (this.operationCompleteStatus.ContainsValue(false))
			{
				return;
			}
			if (this.OnFinished != null)
			{
				this.OnFinished(this);
			}
		}

		public void OnSync()
		{
			foreach (OperationAggregation.SyncElement syncElement in this.operationCompleteStatus.Keys)
			{
				JobProcessor.Current.Enqueue(Job.Create<Operation>(new Action<Operation>(syncElement.Connection.RequestOperation), syncElement.SyncOperation));
			}
			this.CheckComplete();
		}

		public event Action<ISynchronizable> OnFinished;

		public bool Result { get; private set; }

		private Service service;

		private Dictionary<OperationAggregation.SyncElement, bool> operationCompleteStatus = new Dictionary<OperationAggregation.SyncElement, bool>();

		private class SyncElement
		{
			public SyncElement(IEntityProxy conn, Operation op)
			{
				this.Connection = conn;
				this.SyncOperation = op;
			}

			public IEntityProxy Connection;

			public Operation SyncOperation;
		}
	}
}
