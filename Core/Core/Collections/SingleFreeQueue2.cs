using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace Devcat.Core.Collections
{
	public class SingleFreeQueue2<T>
	{
		public SingleFreeQueue2() : this(32)
		{
		}

		public SingleFreeQueue2(int readCacheLength)
		{
			this.cacheLength = readCacheLength;
			this.readStatus = new SingleFreeQueue2<T>.ReadStatus();
			this.readStatus.CachedValueList = new SingleFreeQueue2<T>.Element[this.cacheLength];
			this.readStatus.ElementList = new SingleFreeQueue2<T>.ElementList(32);
			this.writeStatus = new SingleFreeQueue2<T>.WriteStatus();
			this.writeStatus.ElementList = this.readStatus.ElementList;
		}

		private void LoadCache()
		{
			bool flag = this.readStatus.ElementList.NextList != null;
			if (!this.readStatus.ElementList.ValueList[this.readStatus.ReadArrayIndex].Valid)
			{
				if (!flag)
				{
					return;
				}
				this.readStatus.ReadSequence = 0;
				this.readStatus.ReadArrayIndex = 0;
				this.readStatus.ElementList = this.readStatus.ElementList.NextList;
			}
			int num = this.readStatus.ElementList.ValueList.Length;
			SingleFreeQueue2<T>.WriteStatus writeStatus = this.writeStatus;
			int num2;
			if (writeStatus.ElementList == this.readStatus.ElementList)
			{
				num2 = writeStatus.WriteSequence - this.readStatus.ReadSequence;
				if (num2 > this.cacheLength)
				{
					num2 = this.cacheLength;
				}
			}
			else
			{
				num2 = this.cacheLength;
			}
			if (this.readStatus.ReadArrayIndex + num2 > num)
			{
				num2 = num - this.readStatus.ReadArrayIndex;
			}
			Array.Copy(this.readStatus.ElementList.ValueList, this.readStatus.ReadArrayIndex, this.readStatus.CachedValueList, 0, num2);
			Array.Clear(this.readStatus.ElementList.ValueList, this.readStatus.ReadArrayIndex, num2);
			this.readStatus.ReadSequence += num2;
			this.readStatus.ReadArrayIndex = (this.readStatus.ReadSequence & num - 1);
		}

		public bool Empty
		{
			get
			{
				if (this.readStatus.Index < this.cacheLength && this.readStatus.CachedValueList[this.readStatus.Index].Valid)
				{
					return false;
				}
				this.readStatus.Index = 0;
				this.LoadCache();
				return !this.readStatus.CachedValueList[this.readStatus.Index].Valid;
			}
		}

		public T Head
		{
			get
			{
				if (this.Empty)
				{
					throw new InvalidOperationException("Queue is Empty.");
				}
				return this.readStatus.CachedValueList[this.readStatus.Index].Value;
			}
		}

		public void Enqueue(T value)
		{
			if (this.writeStatus.ElementList.ValueList[this.writeStatus.WriteArrayIndex].Valid)
			{
				int num = this.writeStatus.ElementList.ValueList.Length << 1;
				if (num == -2147483648)
				{
					throw new OutOfMemoryException("Can't reserve more entry.");
				}
				SingleFreeQueue2<T>.WriteStatus writeStatus = new SingleFreeQueue2<T>.WriteStatus();
				writeStatus.ElementList = new SingleFreeQueue2<T>.ElementList(num);
				writeStatus.ElementList.ValueList[0].Value = value;
				writeStatus.ElementList.ValueList[0].Valid = true;
				writeStatus.WriteSequence = 1;
				writeStatus.WriteArrayIndex = 1;
				SingleFreeQueue2<T>.ElementList elementList = this.writeStatus.ElementList;
				this.writeStatus = writeStatus;
				elementList.NextList = writeStatus.ElementList;
			}
			else
			{
				this.writeStatus.ElementList.ValueList[this.writeStatus.WriteArrayIndex].Value = value;
				this.writeStatus.ElementList.ValueList[this.writeStatus.WriteArrayIndex].Valid = true;
				this.writeStatus.WriteSequence++;
				this.writeStatus.WriteArrayIndex = (this.writeStatus.WriteSequence & this.writeStatus.ElementList.ValueList.Length - 1);
			}
		}

		public void Enqueue<U>(IEnumerable<U> collection, Converter<U, T> converter)
		{
			SingleFreeQueue2<T>.ElementList elementList = null;
			SingleFreeQueue2<T>.ElementList elementList2 = this.writeStatus.ElementList;
			int num = this.writeStatus.WriteSequence;
			int num2 = this.writeStatus.WriteArrayIndex;
			int num3 = 0;
			try
			{
				foreach (U input in collection)
				{
					if (elementList2.ValueList[num2].Valid)
					{
						int num4 = elementList2.ValueList.Length << 1;
						if (num4 == -2147483648)
						{
							throw new OutOfMemoryException("Can't reserve more entry.");
						}
						SingleFreeQueue2<T>.ElementList elementList3 = new SingleFreeQueue2<T>.ElementList(num4);
						if (elementList == null)
						{
							elementList = elementList3;
							elementList2 = elementList3;
							num3 = num - this.writeStatus.WriteSequence;
						}
						else
						{
							elementList2.NextList = elementList3;
							elementList2 = elementList3;
						}
						num = 0;
						num2 = 0;
					}
					elementList2.ValueList[num2].Value = converter(input);
					elementList2.ValueList[num2].Valid = true;
					num++;
					num2 = (num & elementList2.ValueList.Length - 1);
				}
				if (elementList == null)
				{
					this.writeStatus.WriteSequence = num;
					this.writeStatus.WriteArrayIndex = num2;
				}
				else
				{
					SingleFreeQueue2<T>.WriteStatus writeStatus = new SingleFreeQueue2<T>.WriteStatus();
					writeStatus.ElementList = elementList2;
					writeStatus.WriteSequence = num;
					writeStatus.WriteArrayIndex = num2;
					SingleFreeQueue2<T>.ElementList elementList4 = this.writeStatus.ElementList;
					this.writeStatus = writeStatus;
					elementList4.NextList = elementList;
				}
			}
			catch (OutOfMemoryException)
			{
				if (elementList == null)
				{
					num3 = num - this.writeStatus.WriteSequence;
				}
				if (this.writeStatus.WriteArrayIndex + num3 > this.writeStatus.ElementList.ValueList.Length)
				{
					int num5 = this.writeStatus.ElementList.ValueList.Length - this.writeStatus.WriteArrayIndex;
					Array.Clear(this.writeStatus.ElementList.ValueList, this.writeStatus.WriteArrayIndex, num5);
					Array.Clear(this.writeStatus.ElementList.ValueList, 0, num3 - num5);
				}
				else
				{
					Array.Clear(this.writeStatus.ElementList.ValueList, this.writeStatus.WriteArrayIndex, num3);
				}
				throw;
			}
		}

		public void Enqueue<U>(ArraySegment<U> collection, Converter<U, T> converter)
		{
			SingleFreeQueue2<T>.ElementList elementList = null;
			SingleFreeQueue2<T>.ElementList elementList2 = this.writeStatus.ElementList;
			int num = this.writeStatus.WriteSequence;
			int num2 = this.writeStatus.WriteArrayIndex;
			int num3 = 0;
			try
			{
				for (int i = 0; i < collection.Count; i++)
				{
					if (elementList2.ValueList[num2].Valid)
					{
						int num4 = elementList2.ValueList.Length << 1;
						if (num4 == -2147483648)
						{
							throw new OutOfMemoryException("Can't reserve more entry.");
						}
						SingleFreeQueue2<T>.ElementList elementList3 = new SingleFreeQueue2<T>.ElementList(num4);
						if (elementList == null)
						{
							elementList = elementList3;
							elementList2 = elementList3;
							num3 = num - this.writeStatus.WriteSequence;
						}
						else
						{
							elementList2.NextList = elementList3;
							elementList2 = elementList3;
						}
						num = 0;
						num2 = 0;
					}
					elementList2.ValueList[num2].Value = converter(collection.Array[i + collection.Offset]);
					elementList2.ValueList[num2].Valid = true;
					num++;
					num2 = (num & elementList2.ValueList.Length - 1);
				}
				if (elementList == null)
				{
					this.writeStatus.WriteSequence = num;
					this.writeStatus.WriteArrayIndex = num2;
				}
				else
				{
					SingleFreeQueue2<T>.WriteStatus writeStatus = new SingleFreeQueue2<T>.WriteStatus();
					writeStatus.ElementList = elementList2;
					writeStatus.WriteSequence = num;
					writeStatus.WriteArrayIndex = num2;
					SingleFreeQueue2<T>.ElementList elementList4 = this.writeStatus.ElementList;
					this.writeStatus = writeStatus;
					elementList4.NextList = elementList;
				}
			}
			catch (OutOfMemoryException)
			{
				if (elementList == null)
				{
					num3 = num - this.writeStatus.WriteSequence;
				}
				if (this.writeStatus.WriteArrayIndex + num3 > this.writeStatus.ElementList.ValueList.Length)
				{
					int num5 = this.writeStatus.ElementList.ValueList.Length - this.writeStatus.WriteArrayIndex;
					Array.Clear(this.writeStatus.ElementList.ValueList, this.writeStatus.WriteArrayIndex, num5);
					Array.Clear(this.writeStatus.ElementList.ValueList, 0, num3 - num5);
				}
				else
				{
					Array.Clear(this.writeStatus.ElementList.ValueList, this.writeStatus.WriteArrayIndex, num3);
				}
				throw;
			}
		}

		public void Enqueue(IEnumerable<T> collection)
		{
			SingleFreeQueue2<T>.ElementList elementList = null;
			SingleFreeQueue2<T>.ElementList elementList2 = this.writeStatus.ElementList;
			int num = this.writeStatus.WriteSequence;
			int num2 = this.writeStatus.WriteArrayIndex;
			int num3 = 0;
			try
			{
				foreach (T value in collection)
				{
					if (elementList2.ValueList[num2].Valid)
					{
						int num4 = elementList2.ValueList.Length << 1;
						if (num4 == -2147483648)
						{
							throw new OutOfMemoryException("Can't reserve more entry.");
						}
						SingleFreeQueue2<T>.ElementList elementList3 = new SingleFreeQueue2<T>.ElementList(num4);
						if (elementList == null)
						{
							elementList = elementList3;
							elementList2 = elementList3;
							num3 = num - this.writeStatus.WriteSequence;
						}
						else
						{
							elementList2.NextList = elementList3;
							elementList2 = elementList3;
						}
						num = 0;
						num2 = 0;
					}
					elementList2.ValueList[num2].Value = value;
					elementList2.ValueList[num2].Valid = true;
					num++;
					num2 = (num & elementList2.ValueList.Length - 1);
				}
				if (elementList == null)
				{
					this.writeStatus.WriteSequence = num;
					this.writeStatus.WriteArrayIndex = num2;
				}
				else
				{
					SingleFreeQueue2<T>.WriteStatus writeStatus = new SingleFreeQueue2<T>.WriteStatus();
					writeStatus.ElementList = elementList2;
					writeStatus.WriteSequence = num;
					writeStatus.WriteArrayIndex = num2;
					SingleFreeQueue2<T>.ElementList elementList4 = this.writeStatus.ElementList;
					this.writeStatus = writeStatus;
					elementList4.NextList = elementList;
				}
			}
			catch (OutOfMemoryException)
			{
				if (elementList == null)
				{
					num3 = num - this.writeStatus.WriteSequence;
				}
				if (this.writeStatus.WriteArrayIndex + num3 > this.writeStatus.ElementList.ValueList.Length)
				{
					int num5 = this.writeStatus.ElementList.ValueList.Length - this.writeStatus.WriteArrayIndex;
					Array.Clear(this.writeStatus.ElementList.ValueList, this.writeStatus.WriteArrayIndex, num5);
					Array.Clear(this.writeStatus.ElementList.ValueList, 0, num3 - num5);
				}
				else
				{
					Array.Clear(this.writeStatus.ElementList.ValueList, this.writeStatus.WriteArrayIndex, num3);
				}
				throw;
			}
		}

		public T Dequeue()
		{
			if (this.Empty)
			{
				throw new InvalidOperationException("Queue is Empty.");
			}
			T value = this.readStatus.CachedValueList[this.readStatus.Index].Value;
			this.readStatus.CachedValueList[this.readStatus.Index] = default(SingleFreeQueue2<T>.Element);
			this.readStatus.Index++;
			return value;
		}

		public bool TryDequeue(out T value)
		{
			if (this.Empty)
			{
				value = default(T);
				return false;
			}
			value = this.readStatus.CachedValueList[this.readStatus.Index].Value;
			this.readStatus.CachedValueList[this.readStatus.Index] = default(SingleFreeQueue2<T>.Element);
			this.readStatus.Index++;
			return true;
		}

		public bool TryPeek(out T value)
		{
			if (this.Empty)
			{
				value = default(T);
				return false;
			}
			value = this.readStatus.CachedValueList[this.readStatus.Index].Value;
			return true;
		}

		public void Clear()
		{
			SingleFreeQueue2<T>.WriteStatus writeStatus = this.writeStatus;
			if (this.readStatus.ElementList != writeStatus.ElementList)
			{
				this.readStatus.ReadArrayIndex = 0;
				this.readStatus.ReadSequence = 0;
				while (this.readStatus.ElementList != writeStatus.ElementList)
				{
					this.readStatus.ElementList = this.readStatus.ElementList.NextList;
				}
			}
			int num = this.readStatus.ElementList.ValueList.Length;
			int writeSequence = writeStatus.WriteSequence;
			int num2 = writeSequence & num - 1;
			if (this.readStatus.ReadArrayIndex < num2)
			{
				Array.Clear(this.readStatus.ElementList.ValueList, this.readStatus.ReadArrayIndex, writeSequence - this.readStatus.ReadSequence);
			}
			else if (writeSequence - this.readStatus.ReadSequence > 0)
			{
				Array.Clear(this.readStatus.ElementList.ValueList, this.readStatus.ReadArrayIndex, num - this.readStatus.ReadArrayIndex);
				Array.Clear(this.readStatus.ElementList.ValueList, 0, num2);
			}
			this.readStatus.ReadSequence = writeSequence;
			this.readStatus.ReadArrayIndex = num2;
			Array.Clear(this.readStatus.CachedValueList, 0, this.readStatus.CachedValueList.Length);
			this.readStatus.Index = 0;
		}

		private SingleFreeQueue2<T>.ReadStatus readStatus;

		private SingleFreeQueue2<T>.WriteStatus writeStatus;

		private int cacheLength;

		[StructLayout(LayoutKind.Sequential, Size = 128)]
		private class ReadStatus
		{
			public int Index;

			public SingleFreeQueue2<T>.Element[] CachedValueList;

			public int ReadSequence;

			public int ReadArrayIndex;

			public SingleFreeQueue2<T>.ElementList ElementList;
		}

		[StructLayout(LayoutKind.Sequential, Size = 128)]
		private class WriteStatus
		{
			public int WriteSequence;

			public int WriteArrayIndex;

			public SingleFreeQueue2<T>.ElementList ElementList;
		}

		private struct Element
		{
			public T Value;

			public bool Valid;
		}

		private class ElementList
		{
			public ElementList(int capacity)
			{
				this.ValueList = new SingleFreeQueue2<T>.Element[capacity];
			}

			public SingleFreeQueue2<T>.Element[] ValueList;

			public SingleFreeQueue2<T>.ElementList NextList;
		}
	}
}
