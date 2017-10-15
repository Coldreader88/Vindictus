using System;

namespace Devcat.Core.Net.Message
{
	public delegate ICustomSerializable CustomTypeInitializer(ref SerializeReader reader, string fieldName);
}
