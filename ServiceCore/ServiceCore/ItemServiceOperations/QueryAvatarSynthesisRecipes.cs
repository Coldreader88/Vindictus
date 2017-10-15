using System;
using System.Collections.Generic;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.ItemServiceOperations
{
	[Serializable]
	public sealed class QueryAvatarSynthesisRecipes : Operation
	{
		public string RandomItemClass { get; set; }

		public List<string> MaterialRecipes
		{
			get
			{
				return this.materialRecipes;
			}
		}

		public QueryAvatarSynthesisRecipes(string randomItemClass)
		{
			this.RandomItemClass = randomItemClass;
		}

		public override OperationProcessor RequestProcessor()
		{
			return new QueryAvatarSynthesisRecipes.Request(this);
		}

		[NonSerialized]
		private List<string> materialRecipes;

		private class Request : OperationProcessor<QueryAvatarSynthesisRecipes>
		{
			public Request(QueryAvatarSynthesisRecipes op) : base(op)
			{
			}

			public override IEnumerable<object> Run()
			{
				yield return null;
				if (base.Feedback is List<string>)
				{
					base.Operation.materialRecipes = (base.Feedback as List<string>);
				}
				else
				{
					base.Result = false;
				}
				yield break;
			}
		}
	}
}
