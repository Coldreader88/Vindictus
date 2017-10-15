using System;
using Devcat.Core.Collections;

namespace Devcat.Core.Threading
{
	internal class BalancedThread
	{
		public int ID
		{
			get
			{
				return this.id;
			}
		}

		public bool IsInThread
		{
			get
			{
				return this.jobProcessor.IsInThread();
			}
		}

		internal JobProcessor JobProcessor
		{
			get
			{
				return this.jobProcessor;
			}
		}

		public int TotalLoad
		{
			get
			{
				return this.totalLoad;
			}
		}

		public ThreadLoad MostBurden
		{
			get
			{
				return this.mostBurden;
			}
		}

		public BalancedThread(int id)
		{
			this.jobProcessor = new JobProcessor();
			this.jobList = new PriorityQueue<ThreadLoad>();
			this.id = id;
			this.job = Job.Create(new Action(this.Operate));
		}

		public void Start()
		{
			this.jobProcessor.Start();
		}

		public void Stop()
		{
			this.jobProcessor.Stop();
		}

		public void Add(ThreadLoad threadLoad)
		{
			if (!this.jobProcessor.IsInThread())
			{
				this.jobProcessor.Enqueue(Job.Create<ThreadLoad>(new Action<ThreadLoad>(this.Add), threadLoad));
				return;
			}
			if (threadLoad.Parent == this)
			{
				this.Reserve(threadLoad);
				return;
			}
			if (threadLoad.Parent != null)
			{
				throw new InvalidOperationException("ThreadLoad can be registered to one LoadBalancer at same time");
			}
			threadLoad.Parent = this;
			threadLoad.PreviousLoad = 0;
			this.Reserve(threadLoad);
		}

		public void Change(ThreadLoad threadLoad, BalancedThread newBalancedThread)
		{
			if (!this.jobProcessor.IsInThread())
			{
				this.jobProcessor.Enqueue(Job.Create<ThreadLoad, BalancedThread>(new Action<ThreadLoad, BalancedThread>(this.Change), threadLoad, newBalancedThread));
				return;
			}
			if (threadLoad.Parent != this)
			{
				return;
			}
			this.Remove(threadLoad);
			newBalancedThread.Add(threadLoad);
		}

		public void Remove(ThreadLoad threadLoad)
		{
			if (!this.jobProcessor.IsInThread())
			{
				this.jobProcessor.Enqueue(Job.Create<ThreadLoad>(new Action<ThreadLoad>(this.Remove), threadLoad));
				return;
			}
			if (threadLoad.Parent != this)
			{
				throw new InvalidOperationException("ThreadLoad must be registered to proper LoadBalancer before deregister.");
			}
			if (threadLoad.PreviousLoad != 0)
			{
				this.jobList.Remove(threadLoad.PriorityQueueElement);
				this.totalLoad -= threadLoad.PreviousLoad;
			}
			if (this.mostBurden == threadLoad)
			{
				this.mostBurden = null;
			}
			threadLoad.Parent = null;
		}

		public void Reserve(ThreadLoad threadLoad)
		{
			if (!this.jobProcessor.IsInThread())
			{
				this.jobProcessor.Enqueue(Job.Create<ThreadLoad>(new Action<ThreadLoad>(this.Reserve), threadLoad));
				return;
			}
			if (threadLoad.Parent != this)
			{
				throw new InvalidOperationException();
			}
			if (threadLoad.PriorityQueueElement.Valid)
			{
				return;
			}
			int previousLoad = threadLoad.PreviousLoad;
			int load = threadLoad.Load;
			threadLoad.PreviousLoad = load;
			this.totalLoad += load - previousLoad;
			if (load != 0)
			{
				threadLoad.PriorityQueueElement.Value = threadLoad;
				threadLoad.PriorityQueueElement.Priority = threadLoad.Priority;
				this.jobList.Add(threadLoad.PriorityQueueElement);
				if (this.jobList.Count > 0)
				{
					this.jobProcessor.Enqueue(this.job);
				}
				if (this.mostBurden != null && (this.mostBurden.PreviousLoad == 0 || this.mostBurden.PreviousLoad * 3 > this.totalLoad))
				{
					this.mostBurden = null;
				}
				if (threadLoad.Load * 3 <= this.totalLoad && (this.mostBurden == null || this.mostBurden.PreviousLoad < load))
				{
					this.mostBurden = threadLoad;
				}
			}
		}

		private void Operate()
		{
			if (this.jobList.Count == 0)
			{
				return;
			}
			ThreadLoad threadLoad = this.jobList.RemoveMin();
			try
			{
				threadLoad.InvokeOperationable();
			}
			finally
			{
				if (threadLoad.Parent == this)
				{
					this.Reserve(threadLoad);
				}
			}
		}

		private int id;

		private IJob job;

		private JobProcessor jobProcessor;

		private int totalLoad;

		private PriorityQueue<ThreadLoad> jobList;

		private ThreadLoad mostBurden;
	}
}
