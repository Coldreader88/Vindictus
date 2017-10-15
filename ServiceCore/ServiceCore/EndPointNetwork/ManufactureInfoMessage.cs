using System;
using System.Collections.Generic;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class ManufactureInfoMessage : IMessage
	{
		public IDictionary<string, int> ExpDictionary { get; set; }

		public IDictionary<string, int> GradeDictionary { get; set; }

		public IList<string> Recipes { get; set; }

		public ManufactureInfoMessage(IDictionary<string, int> exp, IDictionary<string, int> grade, IList<string> recipes)
		{
			this.ExpDictionary = exp;
			this.GradeDictionary = grade;
			this.Recipes = recipes;
		}
	}
}
