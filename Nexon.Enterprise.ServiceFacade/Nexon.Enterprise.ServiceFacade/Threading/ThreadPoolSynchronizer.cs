using System;
using System.Collections.Generic;
using System.Security.Permissions;
using System.Threading;

namespace Nexon.Enterprise.ServiceFacade.Threading
{
	[SecurityPermission(SecurityAction.Demand, ControlThread = true)]
	public class ThreadPoolSynchronizer : SynchronizationContext, IDisposable
	{
		protected Semaphore ItemAdded
		{
			get
			{
				return this.m_ItemAdded;
			}
			set
			{
				this.m_ItemAdded = value;
			}
		}

		public ThreadPoolSynchronizer(uint poolSize) : this(poolSize, "Pooled Thread: ")
		{
		}

		public ThreadPoolSynchronizer(uint poolSize, string poolName)
		{
			if (poolSize == 0u)
			{
				throw new InvalidOperationException("Pool size cannot be zero");
			}
			this.m_ItemAdded = new Semaphore(0, int.MaxValue);
			this.m_WorkItemQueue = new Queue<WorkItem>();
			this.m_WorkerThreads = new ThreadPoolSynchronizer.WorkerThread[poolSize];
			int num = 0;
			while ((long)num < (long)((ulong)poolSize))
			{
				this.m_WorkerThreads[num] = new ThreadPoolSynchronizer.WorkerThread(poolName + " " + (num + 1), this);
				num++;
			}
		}

		internal virtual void QueueWorkItem(WorkItem workItem)
		{
			lock (this.m_WorkItemQueue)
			{
				this.m_WorkItemQueue.Enqueue(workItem);
				this.ItemAdded.Release();
			}
		}

		protected virtual bool QueueEmpty
		{
			get
			{
				bool result;
				lock (this.m_WorkItemQueue)
				{
					if (this.m_WorkItemQueue.Count > 0)
					{
						result = false;
					}
					else
					{
						result = true;
					}
				}
				return result;
			}
		}

		internal virtual WorkItem GetNext()
		{
			this.ItemAdded.WaitOne();
			WorkItem result;
			lock (this.m_WorkItemQueue)
			{
				if (this.m_WorkItemQueue.Count == 0)
				{
					result = null;
				}
				else
				{
					result = this.m_WorkItemQueue.Dequeue();
				}
			}
			return result;
		}

		public void Dispose()
		{
			this.Close();
		}

		public override SynchronizationContext CreateCopy()
		{
			return this;
		}

		public override void Post(SendOrPostCallback method, object state)
		{
			WorkItem workItem = new WorkItem(method, state);
			this.QueueWorkItem(workItem);
		}

		public override void Send(SendOrPostCallback method, object state)
		{
			if (SynchronizationContext.Current == this)
			{
				method(state);
				return;
			}
			WorkItem workItem = new WorkItem(method, state);
			this.QueueWorkItem(workItem);
			workItem.AsyncWaitHandle.WaitOne();
		}

		public void Close()
		{
			if (this.ItemAdded.SafeWaitHandle.IsClosed)
			{
				return;
			}
			this.ItemAdded.Release(int.MaxValue);
			foreach (ThreadPoolSynchronizer.WorkerThread workerThread in this.m_WorkerThreads)
			{
				workerThread.Kill();
			}
			this.ItemAdded.Close();
		}

		public void Abort()
		{
			this.ItemAdded.Release(int.MaxValue);
			foreach (ThreadPoolSynchronizer.WorkerThread workerThread in this.m_WorkerThreads)
			{
				workerThread.m_ThreadObj.Abort();
			}
			this.ItemAdded.Close();
		}

		private ThreadPoolSynchronizer.WorkerThread[] m_WorkerThreads;

		private Queue<WorkItem> m_WorkItemQueue;

		private Semaphore m_ItemAdded;

		private class WorkerThread
		{
			public int ManagedThreadId
			{
				get
				{
					return this.m_ThreadObj.ManagedThreadId;
				}
			}

			internal WorkerThread(string name, ThreadPoolSynchronizer context)
			{
				this.m_Context = context;
				this.m_EndLoop = false;
				this.m_ThreadObj = null;
				this.m_ThreadObj = new Thread(new ThreadStart(this.Run));
				this.m_ThreadObj.IsBackground = true;
				this.m_ThreadObj.Name = name;
				this.m_ThreadObj.Start();
			}

			private bool EndLoop
			{
				get
				{
					bool endLoop;
					lock (this)
					{
						endLoop = this.m_EndLoop;
					}
					return endLoop;
				}
				set
				{
					lock (this)
					{
						this.m_EndLoop = value;
					}
				}
			}

			private void Start()
			{
				this.m_ThreadObj.Start();
			}

			private void Run()
			{
				SynchronizationContext.SetSynchronizationContext(this.m_Context);
				while (!this.EndLoop)
				{
					WorkItem next = this.m_Context.GetNext();
					if (next != null)
					{
						next.CallBack();
					}
				}
			}

			public void Kill()
			{
				if (!this.m_ThreadObj.IsAlive)
				{
					return;
				}
				this.EndLoop = true;
				this.m_ThreadObj.Join();
			}

			private ThreadPoolSynchronizer m_Context;

			public Thread m_ThreadObj;

			private bool m_EndLoop;
		}
	}
}
