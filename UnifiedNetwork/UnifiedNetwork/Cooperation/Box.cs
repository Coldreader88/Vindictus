using System;

namespace UnifiedNetwork.Cooperation
{
	[Serializable]
	public sealed class Box<T> where T : struct
	{
		public T Value { get; set; }
	}
}
