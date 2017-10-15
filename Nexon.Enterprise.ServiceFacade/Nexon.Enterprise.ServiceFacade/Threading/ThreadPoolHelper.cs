using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.ServiceModel.Dispatcher;

namespace Nexon.Enterprise.ServiceFacade.Threading
{
	public static class ThreadPoolHelper
	{
		[MethodImpl(MethodImplOptions.Synchronized)]
		internal static bool HasSynchronizer(Type type)
		{
			return ThreadPoolHelper.m_Synchronizers.ContainsKey(type);
		}

		[MethodImpl(MethodImplOptions.Synchronized)]
		internal static ThreadPoolSynchronizer GetSynchronizer(Type type)
		{
			return ThreadPoolHelper.m_Synchronizers[type];
		}

		[MethodImpl(MethodImplOptions.Synchronized)]
		internal static void ApplyDispatchBehavior(ThreadPoolSynchronizer synchronizer, uint poolSize, Type type, string poolName, DispatchRuntime dispatch)
		{
			int num = 16;
			if (dispatch.ChannelDispatcher.ServiceThrottle != null)
			{
				num = dispatch.ChannelDispatcher.ServiceThrottle.MaxConcurrentCalls;
			}
			if ((long)num < (long)((ulong)poolSize))
			{
				throw new InvalidOperationException("The throttle should allow at least as many concurrent calls as the pool size");
			}
			ThreadPoolHelper.HasSynchronizer(type);
			if (!ThreadPoolHelper.HasSynchronizer(type))
			{
				ThreadPoolHelper.m_Synchronizers[type] = synchronizer;
			}
			dispatch.SynchronizationContext = synchronizer;
		}

		[MethodImpl(MethodImplOptions.Synchronized)]
		public static void CloseThreads(Type type)
		{
			if (ThreadPoolHelper.m_Synchronizers.ContainsKey(type))
			{
				ThreadPoolHelper.m_Synchronizers[type].Dispose();
				ThreadPoolHelper.m_Synchronizers.Remove(type);
			}
		}

		private static Dictionary<Type, ThreadPoolSynchronizer> m_Synchronizers = new Dictionary<Type, ThreadPoolSynchronizer>();
	}
}
