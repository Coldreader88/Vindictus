using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Devcat.Core.Collections.Generic
{
	internal sealed class Mscorlib_DictionaryDebugView<K, V>
	{
		internal Mscorlib_DictionaryDebugView(IDictionary<K, V> dictionary)
		{
			if (dictionary == null)
			{
				throw new ArgumentNullException("dictionary");
			}
			this.dict = dictionary;
		}

		[DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
		internal KeyValuePair<K, V>[] Items
		{
			get
			{
				KeyValuePair<K, V>[] array = new KeyValuePair<K, V>[this.dict.Count];
				this.dict.CopyTo(array, 0);
				return array;
			}
		}

		private IDictionary<K, V> dict;
	}
}
