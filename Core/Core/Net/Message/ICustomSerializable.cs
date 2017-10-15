using System;

namespace Devcat.Core.Net.Message
{
	public interface ICustomSerializable
	{
		int SerializedSize { get; }

		void CustomSerialize(ref SerializeWriter writer);

		void CustomDeserialize(ref SerializeReader reader);
	}
}
