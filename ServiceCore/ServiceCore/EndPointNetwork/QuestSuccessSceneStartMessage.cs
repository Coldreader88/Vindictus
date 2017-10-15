using System;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class QuestSuccessSceneStartMessage : IMessage
	{
		public override string ToString()
		{
			return string.Format("QuestSuccessSceneStartMessage[]", new object[0]);
		}
	}
}
