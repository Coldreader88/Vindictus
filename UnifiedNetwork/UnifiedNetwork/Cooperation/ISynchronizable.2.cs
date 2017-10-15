using System;

namespace UnifiedNetwork.Cooperation
{
	public interface ISynchronizable<T> : ISynchronizable
	{
		T ReturnValue { get; }
	}
}
