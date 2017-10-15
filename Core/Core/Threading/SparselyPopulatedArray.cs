using System;
using System.Threading;

namespace Devcat.Core.Threading
{
	internal class SparselyPopulatedArray<T> where T : class
	{
		internal SparselyPopulatedArray(int initialSize)
		{
			this.m_head = (this.m_tail = new SparselyPopulatedArrayFragment<T>(initialSize));
		}

        internal SparselyPopulatedArrayAddInfo<T> Add(T element)
        {
            int num2;
            while (true)
            {
                SparselyPopulatedArrayFragment<T> mTail = m_tail;
                while (mTail.m_next != null)
                {
                    SparselyPopulatedArrayFragment<T> mNext = mTail.m_next;
                    mTail = mNext;
                    m_tail = mNext;
                }
                for (SparselyPopulatedArrayFragment<T> i = mTail; i != null; i = i.m_prev)
                {
                    if (i.m_freeCount < 1)
                    {
                        SparselyPopulatedArrayFragment<T> mFreeCount = i;
                        mFreeCount.m_freeCount = mFreeCount.m_freeCount - 1;
                    }
                    if (i.m_freeCount > 0 || i.m_freeCount < -10)
                    {
                        int length = i.Length;
                        int num = (length - i.m_freeCount) % length;
                        if (num < 0)
                        {
                            num = 0;
                            SparselyPopulatedArrayFragment<T> sparselyPopulatedArrayFragment = i;
                            sparselyPopulatedArrayFragment.m_freeCount = sparselyPopulatedArrayFragment.m_freeCount - 1;
                        }
                        for (int j = 0; j < length; j++)
                        {
                            int num1 = (num + j) % length;
                            if (i.m_elements[num1] == null)
                            {
                                T t = default(T);
                                if (Interlocked.CompareExchange<T>(ref i.m_elements[num1], element, t) == null)
                                {
                                    int mFreeCount1 = i.m_freeCount - 1;
                                    SparselyPopulatedArrayFragment<T> sparselyPopulatedArrayFragment1 = i;
                                    if (mFreeCount1 > 0)
                                    {
                                        num2 = mFreeCount1;
                                    }
                                    else
                                    {
                                        num2 = 0;
                                    }
                                    sparselyPopulatedArrayFragment1.m_freeCount = num2;
                                    return new SparselyPopulatedArrayAddInfo<T>(i, num1);
                                }
                            }
                        }
                    }
                }
                SparselyPopulatedArrayFragment<T> sparselyPopulatedArrayFragment2 = new SparselyPopulatedArrayFragment<T>(((int)mTail.m_elements.Length == 4096 ? 4096 : (int)mTail.m_elements.Length * 2), mTail);
                if (Interlocked.CompareExchange<SparselyPopulatedArrayFragment<T>>(ref mTail.m_next, sparselyPopulatedArrayFragment2, null) == null)
                {
                    m_tail = sparselyPopulatedArrayFragment2;
                }
            }
        }

        internal SparselyPopulatedArrayFragment<T> Head
		{
			get
			{
				return this.m_head;
			}
		}

		internal SparselyPopulatedArrayFragment<T> Tail
		{
			get
			{
				return this.m_tail;
			}
		}

		private readonly SparselyPopulatedArrayFragment<T> m_head;

		private volatile SparselyPopulatedArrayFragment<T> m_tail;
	}
}
