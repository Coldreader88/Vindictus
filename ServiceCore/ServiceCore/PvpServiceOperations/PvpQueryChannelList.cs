using System;
using System.Collections.Generic;
using ServiceCore.EndPointNetwork.Pvp;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.PvpServiceOperations
{
	[Serializable]
	public sealed class PvpQueryChannelList : Operation
	{
		public string Key { get; set; }

		public string PvpMode { get; set; }

		public PvpQueryChannelList(string key, string pvpMode)
		{
			this.Key = key;
			this.PvpMode = pvpMode;
		}

		public override OperationProcessor RequestProcessor()
		{
			return new PvpQueryChannelList.Request(this);
		}

		[NonSerialized]
		public List<PvpChannelInfo> ChannelList;

		private class Request : OperationProcessor<PvpQueryChannelList>
		{
			public Request(PvpQueryChannelList op) : base(op)
			{
			}

			public override IEnumerable<object> Run()
			{
				yield return null;
				if (base.Feedback is List<PvpChannelInfo>)
				{
					base.Result = true;
					base.Operation.ChannelList = (base.Feedback as List<PvpChannelInfo>);
				}
				else
				{
					base.Result = false;
					base.Operation.ChannelList = null;
				}
				yield break;
			}
		}
	}
}
