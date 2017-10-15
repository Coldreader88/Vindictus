using System;
using System.Collections.Generic;
using Utility;

namespace UnifiedNetwork.LocationFree
{
	internal class MultipleLookUp<Key, Value> : IAsyncLookUp<Key, Value>
	{
		public List<Value> GetLocations(Key key)
		{
			return this.Locations.ToList(key);
		}

		public void AddLocation(Key key, Value value)
		{
			List<Value> locations = this.GetLocations(key);
			if (locations.Contains(value))
			{
				return;
			}
			this.Locations.Add(key, value);
		}

		public void RemoveLocation(Key key, Value value)
		{
			this.Locations.Remove(key, value);
		}

		public Value GetLocation(Key key)
		{
			List<Value> locations = this.GetLocations(key);
			if (locations == null)
			{
				return default(Value);
			}
			return locations[this.random.Next(locations.Count)];
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

		private MultiDictionary<Key, Value> Locations = new MultiDictionary<Key, Value>();

		private Random random = new Random();
	}
}
