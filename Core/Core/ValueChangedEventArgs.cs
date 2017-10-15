using System;

namespace Devcat.Core
{
	[Serializable]
	public class ValueChangedEventArgs<T> : EventArgs
	{
		public T OldValue { get; private set; }

		public T NewValue { get; private set; }

		public ValueChangedEventArgs(T oldValue, T newValue)
		{
			this.OldValue = oldValue;
			this.NewValue = newValue;
		}
	}
}
