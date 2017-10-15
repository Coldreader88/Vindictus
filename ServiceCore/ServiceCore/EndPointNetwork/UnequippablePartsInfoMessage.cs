using System;
using System.Collections.Generic;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class UnequippablePartsInfoMessage : IMessage
	{
		public ICollection<int> Parts { get; private set; }

		public UnequippablePartsInfoMessage(ICollection<int> parts)
		{
			this.Parts = parts;
		}

		public override string ToString()
		{
			return "UnequippablePartsMesage[]";
		}
	}
}
