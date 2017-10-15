using System;

namespace Devcat.Core.Net.Message
{
	public interface ICustomSerializableContainer
	{
		ICustomSerializable InitializeCustomSerializable(ref SerializeReader reader, string fieldName);
	}
}
