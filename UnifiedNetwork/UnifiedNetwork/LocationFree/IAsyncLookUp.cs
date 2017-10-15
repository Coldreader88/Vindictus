using System;

namespace UnifiedNetwork.LocationFree
{
	public interface IAsyncLookUp<Key, Value>
	{
		IAsyncLookUp<Key, Value> BaseLookUp { get; set; }

		void FindLocation(Key key, Action<Value> callback);
	}
}
