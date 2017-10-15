using System;

namespace Devcat.Core.Net.Message
{
	internal static class ClassInfo<T>
	{
		public static int CategoryId
		{
			get
			{
				return ClassInfo<T>.categoryId;
			}
			set
			{
				if (ClassInfo<T>.categoryId != value && ClassInfo<T>.categoryId != 0)
				{
					throw new InvalidOperationException(string.Format("CategoryId is already set. T = {0}", typeof(T).AssemblyQualifiedName));
				}
				ClassInfo<T>.categoryId = value;
			}
		}

		private static int categoryId;
	}
}
