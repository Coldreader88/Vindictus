using System;

namespace Devcat.Core.Threading
{
	public class LoadBalancer
	{
		public event EventHandler<EventArgs<Exception>> ExceptionOccur;

		public void Start(int threadCount)
		{
			if (this.balancedThreadList != null)
			{
				throw new InvalidOperationException("Already started");
			}
			if (threadCount < 0)
			{
				throw new ArgumentOutOfRangeException("threadCount", threadCount, "Must be 0 or larger");
			}
			if (threadCount == 0)
			{
				threadCount = Environment.ProcessorCount;
				if (threadCount == 0)
				{
					throw new NotSupportedException("Failed to get cpu count! Assign thread count manually.");
				}
			}
			this.balancedThreadList = new BalancedThread[threadCount];
			for (int i = 0; i < threadCount; i++)
			{
				this.balancedThreadList[i] = new BalancedThread(i);
			}
			foreach (BalancedThread balancedThread in this.balancedThreadList)
			{
				balancedThread.JobProcessor.ExceptionOccur += this.JobProcessor_ExceptionOccur;
				balancedThread.Start();
			}
			if (threadCount > 1)
			{
				this.threadBalancer = Job.Create(new Action(this.Balance));
				Scheduler.Schedule(this.balancedThreadList[0].JobProcessor, this.threadBalancer, 500);
			}
		}

		private void JobProcessor_ExceptionOccur(object sender, EventArgs<Exception> e)
		{
			if (this.ExceptionOccur != null)
			{
				this.ExceptionOccur(this, e);
			}
		}

		private void Balance()
		{
			BalancedThread[] array = this.balancedThreadList;
			if (array == null)
			{
				return;
			}
			Scheduler.Schedule(array[0].JobProcessor, this.threadBalancer, 500);
			ThreadLoad threadLoad = null;
			BalancedThread balancedThread = null;
			BalancedThread balancedThread2 = null;
			for (int i = 0; i < array.Length; i++)
			{
				ThreadLoad mostBurden = array[i].MostBurden;
				if (mostBurden != null && array[i].TotalLoad >= 128 && (threadLoad == null || mostBurden.PreviousLoad < threadLoad.PreviousLoad))
				{
					balancedThread = array[i];
					threadLoad = mostBurden;
				}
				else if (balancedThread2 == null || balancedThread2.TotalLoad > array[i].TotalLoad)
				{
					balancedThread2 = array[i];
				}
			}
			if (balancedThread != null && balancedThread2 != null && balancedThread.TotalLoad >= balancedThread2.TotalLoad * 3)
			{
				balancedThread.Change(threadLoad, balancedThread2);
			}
		}

		public void Stop()
		{
			if (this.balancedThreadList == null)
			{
				return;
			}
			foreach (BalancedThread balancedThread in this.balancedThreadList)
			{
				balancedThread.Stop();
			}
			this.insertIndex = 0;
			this.balancedThreadList = null;
		}

		public void Add(ThreadLoad threadLoad)
		{
			if (this.balancedThreadList == null)
			{
				throw new InvalidOperationException("Instance not initialized");
			}
			if (threadLoad.Parent != null)
			{
				throw new InvalidOperationException("ThreadLoad had been added to another LoadBalancer");
			}
			this.balancedThreadList[this.insertIndex].Add(threadLoad);
			if (this.insertIndex == this.balancedThreadList.Length - 1)
			{
				this.insertIndex = 0;
				return;
			}
			this.insertIndex++;
		}

		public void Remove(ThreadLoad threadLoad)
		{
			if (this.balancedThreadList == null)
			{
				throw new InvalidOperationException("Instance not initialized");
			}
			threadLoad.Parent.Remove(threadLoad);
		}

		private BalancedThread[] balancedThreadList;

		private int insertIndex;

		private IJob threadBalancer;
	}
}
