using System;
using System.Collections.Generic;

namespace UnifiedNetwork.LocationFree
{
	internal class SingleLookUp<Key, Value> : IAsyncLookUp<Key, Value>
	{
		public Value GetLocation(Key key)
		{
			Value result;
			this.Locations.TryGetValue(key, out result);
			return result;
		}

		public bool SetLocation(Key key, Value value)
		{
			if (this.Locations.ContainsKey(key))
			{
				return false;
			}
			this.Locations.Add(key, value);
			return true;
		}

		public void UpdateLocation(Key key, Value value)
		{
			this.Locations[key] = value;
		}

		public void FindLocation(Key key, Action<Value> callback)
		{
			Value location = this.GetLocation(key);
			if (location == null && this.BaseLookUp != null)
			{
				this.BaseLookUp.FindLocation(key, callback);
				return;
			}
			callback(location);
		}

		public IAsyncLookUp<Key, Value> BaseLookUp { get; set; }

		private Dictionary<Key, Value> Locations = new Dictionary<Key, Value>();
	}
}
