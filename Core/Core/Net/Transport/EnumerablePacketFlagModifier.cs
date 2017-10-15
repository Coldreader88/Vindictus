using System;
using System.Collections;
using System.Collections.Generic;
using Devcat.Core.Net.Message;

namespace Devcat.Core.Net.Transport
{
	internal class EnumerablePacketFlagModifier : IEnumerable<Packet>, IEnumerable, IEnumerator<Packet>, IDisposable, IEnumerator
	{
		private EnumerablePacketFlagModifier()
		{
		}

		public static IEnumerable<Packet> Convert(IEnumerable<Packet> original, int newFlag)
		{
			if (EnumerablePacketFlagModifier.threadStaticInstance == null)
			{
				EnumerablePacketFlagModifier.threadStaticInstance = new EnumerablePacketFlagModifier();
			}
			EnumerablePacketFlagModifier.threadStaticInstance.packetEnumerator = original.GetEnumerator();
			EnumerablePacketFlagModifier.threadStaticInstance.flag = newFlag;
			return EnumerablePacketFlagModifier.threadStaticInstance;
		}

		IEnumerator<Packet> IEnumerable<Packet>.GetEnumerator()
		{
			return this;
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return this;
		}

		private Packet Current
		{
			get
			{
				Packet result = this.packetEnumerator.Current;
				result.InstanceId = (long)this.flag;
				return result;
			}
		}

		Packet IEnumerator<Packet>.Current
		{
			get
			{
				return this.Current;
			}
		}

		void IDisposable.Dispose()
		{
			this.packetEnumerator.Dispose();
		}

		object IEnumerator.Current
		{
			get
			{
				return this.Current;
			}
		}

		bool IEnumerator.MoveNext()
		{
			return this.packetEnumerator.MoveNext();
		}

		void IEnumerator.Reset()
		{
			this.packetEnumerator.Reset();
		}

		private IEnumerator<Packet> packetEnumerator;

		private int flag;

		[ThreadStatic]
		private static EnumerablePacketFlagModifier threadStaticInstance;
	}
}
