using System;

namespace MMOServer
{
	internal static class KeyUtil
	{
		public static Key GetKey(this IComponent component)
		{
			return new Key(component.ID, component.Category);
		}

		public static Key GetKey(this IModifier modifier)
		{
			return new Key(modifier.ID, modifier.Category);
		}
	}
}
