using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Security.Permissions;

namespace Devcat.Core.Collections.Concurrent
{
	[ComVisible(true)]
	[HostProtection(SecurityAction.LinkDemand, Synchronization = true, ExternalThreading = true)]
	public abstract class Partitioner<TSource>
	{
		public virtual IEnumerable<TSource> GetDynamicPartitions()
		{
			throw new NotSupportedException(Environment2.GetResourceString("Partitioner_DynamicPartitionsNotSupported"));
		}

		public abstract IList<IEnumerator<TSource>> GetPartitions(int partitionCount);

		public virtual bool SupportsDynamicPartitions
		{
			get
			{
				return false;
			}
		}
	}
}
