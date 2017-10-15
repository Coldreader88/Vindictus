using System;
using System.Collections.Generic;
using Devcat.Core;

namespace UnifiedNetwork.Entity
{
	public interface IEntity
	{
		long ID { get; }

		string Category { get; }

		object Tag { get; set; }

		T GetTag<T>();

		bool IsClosed { get; }

		IEnumerable<IEntityProxy> DependsOn { get; }

		IEnumerable<IEntityAdapter> UsedBy { get; }

		int UseCount { get; }

		bool EnableAutoClose { get; set; }

		event EventHandler Closed;

		event EventHandler<EventArgs<IEntityAdapter>> Using;

		event EventHandler<EventArgs<IEntityAdapter>> Used;

		void Close();

		void Close(bool isForced);
	}
}
