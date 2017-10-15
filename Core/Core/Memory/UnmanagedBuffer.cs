using System;
using System.Threading;

namespace Devcat.Core.Memory
{
	public struct UnmanagedBuffer
	{
		public unsafe IntPtr Handle
		{
			get
			{
				return new IntPtr((void*)this.address);
			}
		}

		internal unsafe byte* Address
		{
			get
			{
				return this.address;
			}
		}

		public int Count
		{
			get
			{
				return this.count;
			}
		}

		internal unsafe UnmanagedBuffer(int count)
		{
			this.address = (byte*)UnmanagedAllocator.Allocate(count).ToPointer();
			this.count = count;
		}

		internal unsafe UnmanagedBuffer(byte* address, int count)
		{
			this.address = address;
			this.count = count;
		}

		internal unsafe void Release()
		{
			UnmanagedAllocator.Release(this.address);
		}

		public unsafe int IncrementReferenceCount()
		{
			return Interlocked.Increment(ref *(int*)(this.address - 4));
		}

		public unsafe int DecrementReferenceCount()
		{
			return Interlocked.Decrement(ref *(int*)(this.address - 4));
		}

        public override unsafe int GetHashCode()
        {
            int address = (int)this.address;
            return address.GetHashCode();
        }

        private unsafe byte* address;

		private int count;
	}
}
