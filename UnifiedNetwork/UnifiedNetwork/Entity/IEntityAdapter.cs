using System;

namespace UnifiedNetwork.Entity
{
	public interface IEntityAdapter
	{
		IEntity LocalEntity { get; }

		object Tag { get; set; }

		bool OwnerConnection { get; }

		long RemoteID { get; }

		string RemoteCategory { get; }

		void Close();

		bool IsClosed { get; }
	}
}
