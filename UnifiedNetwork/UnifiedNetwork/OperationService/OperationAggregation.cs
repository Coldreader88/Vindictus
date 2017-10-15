using System;
using System.Collections.Generic;
using UnifiedNetwork.Cooperation;

namespace UnifiedNetwork.OperationService
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
			this.result = true;
			this.operationCompleteStatus = new Dictionary<OperationAggregation.SyncElement, bool>();
		}

		public void AddElement(string serviceClass, int? serviceID, Operation syncOperation)
		{
			OperationAggregation.SyncElement newElement = new OperationAggregation.SyncElement(serviceClass, serviceID, syncOperation);
			this.operationCompleteStatus[newElement] = false;
			syncOperation.OnComplete += delegate(Operation op)
			{
				this.operationCompleteStatus[newElement] = true;
				this.CheckComplete();
			};
			syncOperation.OnFail += delegate(Operation op)
			{
				this.operationCompleteStatus[newElement] = true;
				this.result = false;
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

		private void RequestElement(OperationAggregation.SyncElement element)
		{
			if (element.serviceID == null)
			{
				this.service.RequestOperation(element.serviceClass, element.syncOperation);
				return;
			}
			this.service.RequestOperation(element.serviceID.Value, element.syncOperation);
		}

		public void OnSync()
		{
			foreach (OperationAggregation.SyncElement element in this.operationCompleteStatus.Keys)
			{
				this.RequestElement(element);
			}
			this.CheckComplete();
		}

		public event Action<ISynchronizable> OnFinished;

		public bool Result
		{
			get
			{
				return this.result;
			}
		}

		private Service service;

		private Dictionary<OperationAggregation.SyncElement, bool> operationCompleteStatus;

		private bool result;

		private class SyncElement
		{
			public SyncElement(string sc, int? si, Operation so)
			{
				this.serviceClass = sc;
				this.serviceID = si;
				this.syncOperation = so;
			}

			public string serviceClass;

			public int? serviceID;

			public Operation syncOperation;
		}
	}
}
