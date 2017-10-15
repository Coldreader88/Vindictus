using System;
using System.Threading;

namespace Devcat.Core.Memory
{
	public static class ThreadStatic<T> where T : new()
	{
		public static T Instance
		{
			get
			{
				int managedThreadId = Thread.CurrentThread.ManagedThreadId;
				if (ThreadStatic<T>.instanceList.Length <= managedThreadId || ThreadStatic<T>.instanceList[managedThreadId] == null)
				{
					lock (typeof(ThreadStatic<T>))
					{
						if (ThreadStatic<T>.instanceList.Length <= managedThreadId)
						{
							int i;
							for (i = ThreadStatic<T>.instanceList.Length; i <= managedThreadId; i *= 2)
							{
							}
							T[] destinationArray = new T[i];
							Array.Copy(ThreadStatic<T>.instanceList, destinationArray, ThreadStatic<T>.instanceList.Length);
							ThreadStatic<T>.instanceList = destinationArray;
							T[] destinationArray2 = new T[i];
							Array.Copy(ThreadStatic<T>.visitList, destinationArray2, ThreadStatic<T>.visitList.Length);
							ThreadStatic<T>.visitList = destinationArray2;
						}
						if (ThreadStatic<T>.instanceList[managedThreadId] == null)
						{
							ThreadStatic<T>.instanceList[managedThreadId] = ((default(T) == null) ? Activator.CreateInstance<T>() : default(T));
							ThreadStatic<T>.visitList[ThreadStatic<T>.visitCount] = ThreadStatic<T>.instanceList[managedThreadId];
							ThreadStatic<T>.visitCount++;
						}
					}
				}
				return ThreadStatic<T>.instanceList[managedThreadId];
			}
		}

		public static T[] InstanceList
		{
			get
			{
				return ThreadStatic<T>.visitList;
			}
		}

		public static int InstanceCount
		{
			get
			{
				return ThreadStatic<T>.visitCount;
			}
		}

		private static T[] instanceList = new T[16];

		private static int visitCount;

		private static T[] visitList = new T[16];
	}
}
