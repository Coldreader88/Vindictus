using System;

namespace ServiceCore.LoginServiceOperations
{
	public static class PermissionsExtension
	{
		public static bool Contains(this Permissions value, Permissions permissions)
		{
			return (value & permissions) == permissions;
		}
	}
}
