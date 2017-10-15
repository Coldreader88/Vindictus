using System;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Threading;

namespace Devcat.Core.Threading
{
	[ComVisible(true)]
	[HostProtection(SecurityAction.LinkDemand, Synchronization = true, ExternalThreading = true)]

	public static class LazyInitializer
	{
        private static volatile object s_barrier = null;

        public static T EnsureInitialized<T>(ref T target) where T : class
		{
			if (target != null)
			{
				object obj = LazyInitializer.s_barrier;
				return target;
			}
			return LazyInitializer.EnsureInitializedCore<T>(ref target, LazyInitializer.LazyHelpers<T>.s_activatorFactorySelector);
		}

		public static T EnsureInitialized<T>(ref T target, Func<T> valueFactory) where T : class
		{
			if (target != null)
			{
				object obj = LazyInitializer.s_barrier;
				return target;
			}
			return LazyInitializer.EnsureInitializedCore<T>(ref target, valueFactory);
		}

		public static T EnsureInitialized<T>(ref T target, ref bool initialized, ref object syncLock)
		{
			if (initialized)
			{
				object obj = LazyInitializer.s_barrier;
				return target;
			}
			return LazyInitializer.EnsureInitializedCore<T>(ref target, ref initialized, ref syncLock, LazyInitializer.LazyHelpers<T>.s_activatorFactorySelector);
		}

		public static T EnsureInitialized<T>(ref T target, ref bool initialized, ref object syncLock, Func<T> valueFactory)
		{
			if (initialized)
			{
				object obj = LazyInitializer.s_barrier;
				return target;
			}
			return LazyInitializer.EnsureInitializedCore<T>(ref target, ref initialized, ref syncLock, valueFactory);
		}

		private static T EnsureInitializedCore<T>(ref T target, Func<T> valueFactory) where T : class
		{
			T t = valueFactory();
			if (t == null)
			{
				throw new InvalidOperationException(Environment2.GetResourceString("Lazy_StaticInit_InvalidOperation"));
			}
			Interlocked.CompareExchange<T>(ref target, t, default(T));
			return target;
		}

		private static T EnsureInitializedCore<T>(ref T target, ref bool initialized, ref object syncLock, Func<T> valueFactory)
		{
			object obj = syncLock;
			if (obj == null)
			{
				object obj2 = new object();
				obj = Interlocked.CompareExchange(ref syncLock, obj2, null);
				if (obj == null)
				{
					obj = obj2;
				}
			}
			lock (obj)
			{
				if (!initialized)
				{
					target = valueFactory();
					initialized = true;
				}
			}
			return target;
		}

		

		private static class LazyHelpers<T>
		{
			private static T ActivatorFactorySelector()
			{
				T result;
				try
				{
					result = (T)((object)Activator.CreateInstance(typeof(T)));
				}
				catch (MissingMethodException)
				{
					throw new MissingMemberException(Environment2.GetResourceString("Lazy_CreateValue_NoParameterlessCtorForT"));
				}
				return result;
			}

			internal static Func<T> s_activatorFactorySelector = new Func<T>(LazyInitializer.LazyHelpers<T>.ActivatorFactorySelector);
		}
	}
}
