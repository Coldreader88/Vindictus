using System;
using System.Collections.Generic;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class AvatarSynthesisMaterialRecipesMessage : IMessage
	{
		public List<string> MaterialRecipes { get; set; }

		public AvatarSynthesisMaterialRecipesMessage(List<string> materialRecipes)
		{
			this.MaterialRecipes = materialRecipes;
		}
	}
}
