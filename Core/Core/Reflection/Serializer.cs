using System;

namespace Devcat.Core.Reflection
{
	public interface Serializer
	{
		void AddValue<T>(T value);

		void GetValue<T>(out T value);
	}
}
