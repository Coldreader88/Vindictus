using System;

namespace Devcat.Core
{
	[Serializable]
	public class EventArgs<T> : EventArgs
	{
		public T Value { get; private set; }

		public EventArgs(T value)
		{
			this.Value = value;
		}
	}
}
