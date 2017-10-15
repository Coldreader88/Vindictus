using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Threading;
using Devcat.Core.Math;
using Devcat.Core.WinNative;

namespace Devcat.Core.Memory
{
	public static class UnmanagedAllocator
	{
		private static UnmanagedAllocator.UnmanagedAllocatorContext Context
		{
			get
			{
				if (UnmanagedAllocator.context == null)
				{
					lock (typeof(UnmanagedAllocator))
					{
						if (UnmanagedAllocator.context == null)
						{
							if (UnmanagedAllocator.totalPoolSize == 0)
							{
								UnmanagedAllocator.Initialize();
							}
							short id = (short)UnmanagedAllocator.contextID;
							UnmanagedAllocator.contextID++;
							UnmanagedAllocator.context = new UnmanagedAllocator.UnmanagedAllocatorContext(id);
							UnmanagedAllocator.contextList.Add(UnmanagedAllocator.context);
						}
					}
				}
				return UnmanagedAllocator.context;
			}
		}

		public static int PageSize
		{
			get
			{
				return UnmanagedAllocator.pageSize;
			}
		}

		public static int ThreadPoolSize
		{
			get
			{
				return UnmanagedAllocator.threadPoolSize;
			}
		}

		public static int TotalPoolSize
		{
			get
			{
				return UnmanagedAllocator.totalPoolSize;
			}
		}

		public static void Initialize()
		{
			UnmanagedAllocator.Initialize(4096, 1048576, 67108864);
		}

		public static void Initialize(int pageSize, int threadPoolSize, int totalPoolSize)
		{
			lock (typeof(UnmanagedAllocator))
			{
				if (UnmanagedAllocator.pageSize != 0)
				{
					throw new InvalidOperationException("Already initialized.");
				}
			}
			if (pageSize <= 0 || BitOperation.SmallestPow2((uint)pageSize) != (uint)pageSize)
			{
				throw new ArgumentException("pageSize must be positive power of 2");
			}
			if (threadPoolSize <= 0 || threadPoolSize % pageSize != 0)
			{
				throw new ArgumentException("threadPoolSize must be multiple of pageSize");
			}
			if (totalPoolSize <= 0 || totalPoolSize % threadPoolSize != 0)
			{
				throw new ArgumentException("totalPoolSize must be multiple of threadPoolSize");
			}
			UnmanagedAllocator.pageSize = pageSize;
			UnmanagedAllocator.threadPoolSize = threadPoolSize;
			UnmanagedAllocator.totalPoolSize = totalPoolSize;
		}

		public unsafe static IntPtr Allocate(int size)
		{
			if (size == 0)
			{
				return IntPtr.Zero;
			}
			return new IntPtr((void*)UnmanagedAllocator.Context.Allocate(size));
		}

		internal unsafe static void Release(byte* address)
		{
			if (address == null)
			{
				return;
			}
			int index = (int)(*(short*)(address - 6));
			UnmanagedAllocator.contextList[index].Release(address);
		}

		public unsafe static void Release(IntPtr address)
		{
			UnmanagedAllocator.Release((byte*)address.ToPointer());
		}

		private unsafe static byte* RequestPool()
		{
			byte* result;
			lock (typeof(UnmanagedAllocator))
			{
				if (UnmanagedAllocator.poolAllocCount == 0)
				{
					UnmanagedAllocator.totalPool = (byte*)Marshal.AllocHGlobal(UnmanagedAllocator.totalPoolSize).ToPointer();
					UnmanagedAllocator.poolAllocCount = UnmanagedAllocator.totalPoolSize / UnmanagedAllocator.threadPoolSize;
				}
				byte* ptr = UnmanagedAllocator.totalPool;
				UnmanagedAllocator.totalPool += UnmanagedAllocator.threadPoolSize;
				UnmanagedAllocator.poolAllocCount--;
				result = ptr;
			}
			return result;
		}

		[ThreadStatic]
		private static UnmanagedAllocator.UnmanagedAllocatorContext context;

		private static int contextID;

		private static List<UnmanagedAllocator.UnmanagedAllocatorContext> contextList = new List<UnmanagedAllocator.UnmanagedAllocatorContext>();

		private static int pageSize;

		private static int threadPoolSize;

		private static int totalPoolSize;

		private unsafe static byte* totalPool;

		private static int poolAllocCount;

		private class UnmanagedAllocatorContext
		{
			public unsafe UnmanagedAllocatorContext(short id)
			{
				this.id = id;
				int num = 27 - BitOperation.CountLeadingZeroes(UnmanagedAllocator.ThreadPoolSize);
				this.chunkHeader = new byte*[num];
				this.allocHandle = UnmanagedAllocator.RequestPool();
				this.currentPage = this.allocHandle;
				this.releaseHead = IntPtr.Zero;
			}

			public unsafe byte* Allocate(int size)
			{
				this.ReleasePendingList();
				int num = size + 8;
				int chunkIndex = this.GetChunkIndex(num);
				if (chunkIndex >= this.chunkHeader.Length)
				{
					byte* ptr = (byte*)Marshal.AllocHGlobal(num).ToPointer();
					*(short*)ptr = -1;
					*(short*)(ptr + 2) = this.id;
					*(int*)(ptr + 4) = 0;
					return ptr + 8;
				}
				if (this.chunkHeader[chunkIndex] == null)
				{
					this.AllocatePage(num, chunkIndex);
				}
				byte* ptr2 = this.chunkHeader[chunkIndex];
				this.chunkHeader[chunkIndex] = *((byte**)ptr2);
                *(int*)(ptr2 - 4) = 0;
				return ptr2;
			}

            private unsafe void AllocatePage(int reqSize, int index)
            {
                int num2;
                int size = (int)BitOperation.SmallestPow2((uint)reqSize);
                if ((this.allocHandle + UnmanagedAllocator.ThreadPoolSize) <= (this.currentPage + size))
                {
                    this.ReservePage(this.GetChunkIndex(UnmanagedAllocator.pageSize), UnmanagedAllocator.pageSize, ((int)((long)(((this.allocHandle + UnmanagedAllocator.ThreadPoolSize) - this.currentPage) / 1))) / UnmanagedAllocator.pageSize);
                    this.allocHandle = UnmanagedAllocator.RequestPool();
                    this.currentPage = this.allocHandle;
                }
                if (size > UnmanagedAllocator.pageSize)
                {
                    num2 = 1;
                }
                else
                {
                    num2 = UnmanagedAllocator.pageSize / size;
                }
                this.ReservePage(index, size, num2);
            }

            private unsafe void ReservePage(int index, int size, int count)
			{
				for (int i = 0; i < count; i++)
				{
					*(short*)this.currentPage = (short)index;
					*(short*)(this.currentPage + 2) = this.id;
					*(IntPtr*)(this.currentPage + 8) = (IntPtr)this.chunkHeader[index];
					this.chunkHeader[index] = this.currentPage + 8;
					this.currentPage += size;
				}
			}

			private int GetChunkIndex(int size)
			{
				int num = BitOperation.CountLeadingZeroes(size - 1);
				int num2 = 28 - num;
				if (num2 < 0)
				{
					num2 = 0;
				}
				return num2;
			}

			public unsafe void Release(byte* address)
			{
				for (;;)
				{
					byte* ptr = (byte*)this.releaseHead.ToPointer();
					*(IntPtr*)address = (IntPtr)ptr;
					if (Interlocked.CompareExchange(ref this.releaseHead, new IntPtr((void*)address), new IntPtr((void*)ptr)).ToPointer() == (void*)ptr)
					{
						break;
					}
					Devcat.Core.WinNative.Thread.SwitchToThread();
				}
			}

            private unsafe void ReleasePendingList()
            {
                byte* ptr = (byte*)releaseHead.ToPointer();
                if (ptr == null)
                {
                    return;
                }
                byte* ptr2 = (byte*)*(IntPtr*)ptr;
                *(IntPtr*)ptr = IntPtr.Zero;
                for (ptr = ptr2; ptr != null; ptr = ptr2)
                {
                    ptr2 = (byte*)*(IntPtr*)ptr;
                    int num = (int)(*(short*)(ptr - 8));
                    *(IntPtr*)ptr = (IntPtr)chunkHeader[num];
                    chunkHeader[num] = ptr;
                }
            }

            private short id;

			private unsafe byte* allocHandle;

			private unsafe byte* currentPage;

			private unsafe byte*[] chunkHeader;

			private IntPtr releaseHead;
		}
	}
}
