using System;

namespace UnifiedNetwork.Entity
{
	public interface IEntityProcessor
	{
		IEntity Callee { get; set; }
	}
}
