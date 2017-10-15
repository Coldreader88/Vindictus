using System;

namespace MMOServer
{
	public interface IModifier
	{
		long ID { get; }

		string Category { get; }

		IComponentSpace Space { set; }

		void Apply();
	}
}
