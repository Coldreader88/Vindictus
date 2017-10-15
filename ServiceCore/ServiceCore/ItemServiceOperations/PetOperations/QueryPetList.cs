using System;
using System.Collections.Generic;
using ServiceCore.EndPointNetwork;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.ItemServiceOperations.PetOperations
{
	[Serializable]
	public sealed class QueryPetList : Operation
	{
		public ICollection<PetStatusInfo> PetList
		{
			get
			{
				return this.petList;
			}
		}

		public override OperationProcessor RequestProcessor()
		{
			return new OkOrFailProcessor(this);
		}

		public static QueryPetList MakeResult(ICollection<PetStatusInfo> list)
		{
			return new QueryPetList
			{
				petList = list
			};
		}

		[NonSerialized]
		private ICollection<PetStatusInfo> petList;
	}
}
