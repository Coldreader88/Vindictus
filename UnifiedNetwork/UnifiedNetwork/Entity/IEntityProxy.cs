using System;
using Devcat.Core;
using UnifiedNetwork.Cooperation;

namespace UnifiedNetwork.Entity
{
	public interface IEntityProxy
	{
		IEntity LocalEntity { get; }

		object Tag { get; set; }

		bool OwnerConnection { get; }

		long RemoteID { get; }

		string RemoteCategory { get; }

		bool IsClosed { get; }

		void RequestOperation(Operation op);

		void Close();

		void Close(bool isForced);

		event EventHandler<EventArgs<IEntityProxy>> ConnectionSucceeded;

		event EventHandler<EventArgs<IEntityProxy>> ConnectionFailed;

		event EventHandler<EventArgs<IEntityProxy>> Closed;

		event EventHandler<EventArgs<IEntityProxy>> OperationQueueOversized;
	}
}
