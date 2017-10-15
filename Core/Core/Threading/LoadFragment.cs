using System;
using System.Threading;
using Devcat.Core.Collections;

namespace Devcat.Core.Threading
{
	public sealed class LoadFragment
	{
		public LoadFragmentManager Manager
		{
			get
			{
				return this.manager;
			}
			set
			{
				if (this.manager == null)
				{
					if (value != null)
					{
						if (Interlocked.Exchange(ref this.beingManaged, 1) == 0)
						{
							value.Add(this);
							return;
						}
						throw new InvalidOperationException("Already registered with new LoadFragmentManager");
					}
				}
				else
				{
					if (!this.manager.IsInThread)
					{
						throw new InvalidOperationException("Must be called in same thread with current manager.");
					}
					if (this.manager != value)
					{
						this.manager.Remove(this);
						this.manager = null;
						if (value != null)
						{
							value.Add(this);
							return;
						}
						this.beingManaged = 0;
					}
				}
			}
		}

		public event EventHandler<LoadFragment, EventArgs> ManagerAssign
		{
			add
			{
				this.managerAssign = (EventHandler<LoadFragment, EventArgs>)Delegate.Combine(this.managerAssign, value);
			}
			remove
			{
				this.managerAssign = (EventHandler<LoadFragment, EventArgs>)Delegate.Remove(this.managerAssign, value);
			}
		}

		public event EventHandler<LoadFragment, EventArgs> Operationable
		{
			add
			{
				this.operationable = (EventHandler<LoadFragment, EventArgs>)Delegate.Combine(this.operationable, value);
			}
			remove
			{
				this.operationable = (EventHandler<LoadFragment, EventArgs>)Delegate.Remove(this.operationable, value);
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
					LoadFragmentManager loadFragmentManager = this.manager;
					if (loadFragmentManager != null)
					{
						loadFragmentManager.Reserve(this);
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
				LoadFragmentManager loadFragmentManager = this.manager;
				return loadFragmentManager != null && loadFragmentManager.IsInThread;
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

		internal PriorityQueueElement<LoadFragment> PriorityQueueElement
		{
			get
			{
				return this.priorityQueueElement;
			}
		}

		public LoadFragment()
		{
			this.priorityQueueElement = new PriorityQueueElement<LoadFragment>();
		}

		internal void InvokeOperationable()
		{
			if (this.operationable != null)
			{
				this.operationable(this, EventArgs.Empty);
			}
			this.requested = false;
		}

		internal void InvokeManagerAssign()
		{
			if (this.managerAssign != null)
			{
				this.managerAssign(this, EventArgs.Empty);
			}
		}

		internal void SetManagerInternal(LoadFragmentManager manager)
		{
			this.manager = manager;
		}

		private LoadFragmentManager manager;

		private int load;

		private int previousLoad;

		private bool requested;

		private int priority;

		private int beingManaged;

		private PriorityQueueElement<LoadFragment> priorityQueueElement;

		private EventHandler<LoadFragment, EventArgs> managerAssign;

		private EventHandler<LoadFragment, EventArgs> operationable;
	}
}
