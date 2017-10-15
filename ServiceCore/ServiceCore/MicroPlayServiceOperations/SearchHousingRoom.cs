using System;
using System.Collections.Generic;
using ServiceCore.EndPointNetwork.Housing;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.MicroPlayServiceOperations
{
	[Serializable]
	public sealed class SearchHousingRoom : Operation
	{
		public string Keyword { get; set; }

		public int Option { get; set; }

		public ICollection<HousingRoomInfo> HousingRoomList { get; set; }

		public SearchHousingRoom(string keyword, int option)
		{
			this.Keyword = keyword;
			this.Option = option;
		}

		public override OperationProcessor RequestProcessor()
		{
			return new SearchHousingRoom.Request(this);
		}

		private class Request : OperationProcessor<SearchHousingRoom>
		{
			public Request(SearchHousingRoom op) : base(op)
			{
			}

			public override IEnumerable<object> Run()
			{
				yield return null;
				base.Operation.HousingRoomList = (base.Feedback as ICollection<HousingRoomInfo>);
				if (base.Operation.HousingRoomList == null)
				{
					base.Result = false;
				}
				yield break;
			}
		}
	}
}
