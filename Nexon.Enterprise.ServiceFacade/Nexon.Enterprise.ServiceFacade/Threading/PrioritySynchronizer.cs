using System;
using System.Collections.Generic;
using System.Security.Permissions;
using System.Threading;

namespace Nexon.Enterprise.ServiceFacade.Threading
{
    [SecurityPermission(SecurityAction.Demand, ControlThread = true)]
    public class PrioritySynchronizer : ThreadPoolSynchronizer
    {
        public static CallPriority Priority
        {
            get
            {
                LocalDataStoreSlot namedDataSlot = Thread.GetNamedDataSlot("CallPriority");
                object data = Thread.GetData(namedDataSlot);
                if (data == null)
                {
                    return CallPriority.Normal;
                }
                return (CallPriority)data;
            }
            set
            {
                LocalDataStoreSlot namedDataSlot = Thread.GetNamedDataSlot("CallPriority");
                Thread.SetData(namedDataSlot, value);
            }
        }

        public PrioritySynchronizer(uint poolSize) : this(poolSize, "Pooled Thread: ")
        {
        }

        public PrioritySynchronizer(uint poolSize, string poolName) : base(poolSize, poolName)
        {
            this.m_LowPriorityItemQueue = new Queue<WorkItem>();
            this.m_NormalPriorityItemQueue = new Queue<WorkItem>();
            this.m_HighPriorityItemQueue = new Queue<WorkItem>();
        }

        internal override void QueueWorkItem(WorkItem workItem)
        {
            CallPriority value;
            GenericContext<CallPriority> current = GenericContext<CallPriority>.Current;
            if (current != null)
            {
                value = current.Value;
            }
            else
            {
                value = PrioritySynchronizer.Priority;
            }
            switch (value)
            {
                case CallPriority.Low:
                    {
                        lock (this.m_LowPriorityItemQueue)
                        {
                            this.m_LowPriorityItemQueue.Enqueue(workItem);
                            base.ItemAdded.Release();
                            break;
                        }
                    }
                case CallPriority.Normal:
                    {
                        lock (this.m_NormalPriorityItemQueue)
                        {
                            this.m_NormalPriorityItemQueue.Enqueue(workItem);
                            base.ItemAdded.Release();
                            break;
                        }
                    }
                case CallPriority.High:
                    {
                        lock (this.m_HighPriorityItemQueue)
                        {
                            this.m_HighPriorityItemQueue.Enqueue(workItem);
                            base.ItemAdded.Release();
                            break;
                        }
                    }
                default:
                    {
                        throw new InvalidOperationException(string.Concat("Unknown priority value: ", value));
                    }
            }
        }

        protected override bool QueueEmpty
        {
            get
            {
                lock (this.m_LowPriorityItemQueue)
                {
                    if (this.m_LowPriorityItemQueue.Count > 0)
                    {
                        return false;
                    }
                }
                lock (this.m_NormalPriorityItemQueue)
                {
                    if (this.m_NormalPriorityItemQueue.Count > 0)
                    {
                        return false;
                    }
                }
                lock (this.m_HighPriorityItemQueue)
                {
                    if (this.m_HighPriorityItemQueue.Count > 0)
                    {
                        return false;
                    }
                }
                return true;
            }
        }

        private const string SlotName = "CallPriority";

        private Queue<WorkItem> m_LowPriorityItemQueue;

        private Queue<WorkItem> m_NormalPriorityItemQueue;

        private Queue<WorkItem> m_HighPriorityItemQueue;
    }
}
