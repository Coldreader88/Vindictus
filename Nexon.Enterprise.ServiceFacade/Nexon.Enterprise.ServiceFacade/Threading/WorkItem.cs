using System;
using System.Threading;

namespace Nexon.Enterprise.ServiceFacade.Threading
{
	[Serializable]
	internal class WorkItem
	{
		public WaitHandle AsyncWaitHandle
		{
			get
			{
				return this.m_AsyncWaitHandle;
			}
		}

		internal WorkItem(SendOrPostCallback method, object state)
		{
			this.m_Method = method;
			this.m_State = state;
			this.m_AsyncWaitHandle = new ManualResetEvent(false);
		}

		internal void CallBack()
		{
			this.m_Method(this.m_State);
			this.m_AsyncWaitHandle.Set();
		}

		private object m_State;

		private SendOrPostCallback m_Method;

		private ManualResetEvent m_AsyncWaitHandle;
	}
}
