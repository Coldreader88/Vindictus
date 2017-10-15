using System;
using Devcat.Core.Collections;

namespace Devcat.Core.Threading
{
	public sealed class ThreadLoad
	{
		public event EventHandler<ThreadLoad, EventArgs> Operationable
		{
			add
			{
				this.operationable = (EventHandler<ThreadLoad, EventArgs>)Delegate.Combine(this.operationable, value);
			}
			remove
			{
				this.operationable = (EventHandler<ThreadLoad, EventArgs>)Delegate.Remove(this.operationable, value);
			}
		}

		public int Load
		{
			get
			{
				return this.load;
			}
			set
			{
				this.load = value;
				if (this.previousLoad == 0 && value != 0 && !this.requested)
				{
					this.requested = true;
					BalancedThread balancedThread = this.parent;
					if (balancedThread != null)
					{
						balancedThread.Reserve(this);
					}
				}
			}
		}

		public int Priority
		{
			get
			{
				return this.priority;
			}
			set
			{
				this.priority = value;
			}
		}

		public bool IsInThread
		{
			get
			{
				BalancedThread balancedThread = this.parent;
				return balancedThread != null && balancedThread.IsInThread;
			}
		}

		public object Tag
		{
			get
			{
				return this.tag;
			}
			set
			{
				this.tag = value;
			}
		}

		internal BalancedThread Parent
		{
			get
			{
				return this.parent;
			}
			set
			{
				this.parent = value;
			}
		}

		internal PriorityQueueElement<ThreadLoad> PriorityQueueElement
		{
			get
			{
				return this.priorityQueueElement;
			}
		}

		internal int PreviousLoad
		{
			get
			{
				return this.previousLoad;
			}
			set
			{
				this.previousLoad = value;
			}
		}

		public ThreadLoad()
		{
			this.priorityQueueElement = new PriorityQueueElement<ThreadLoad>();
		}

		internal void InvokeOperationable()
		{
			try
			{
				if (this.operationable != null)
				{
					this.operationable(this, EventArgs.Empty);
				}
			}
			finally
			{
				this.requested = false;
			}
		}

		private BalancedThread parent;

		private int load;

		private int previousLoad;

		private bool requested;

		private int priority;

		private object tag;

		private PriorityQueueElement<ThreadLoad> priorityQueueElement;

		private EventHandler<ThreadLoad, EventArgs> operationable;
	}
}
