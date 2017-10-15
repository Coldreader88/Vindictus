using System;

namespace ServiceCore.EndPointNetwork
{
	[Serializable]
	public sealed class XignCodeSecureDataMessage : IMessage
	{
		public byte[] SecureData { get; set; }

		public int HackCode { get; set; }

		public string HackParam { get; set; }

		public ulong CheckSum { get; set; }

		public override string ToString()
		{
			string text = "";
			for (int i = 0; i < this.SecureData.Length; i++)
			{
				text += string.Format("{0:X2} ", this.SecureData[i]);
			}
			return string.Format("XignCodeSecureDataMessage[ {0} ], CheckSum : {1}, HackCode : {2}", text, this.CheckSum, (XignCodeSecureDataMessage.HACK_CODE)this.HackCode);
		}

		public enum CATEGORY
		{
			CLEAR,
			LOG_ONLY,
			HACK_DETECT = 256
		}

		public enum HACK_CODE
		{
			NONE,
			DETECT_GAME_HACK,
			DETECT_MEM_MODIFY_FROM_LMP,
			DETECT_RMEM_MODIFY_FROM_LMP,
			CONVAR_DATA_MODIFY,
			CLIENT_MEMORY_MODIFY,
			HACK_WATCHER_STATE,
			ABNORMAL_HSHIELD_STATE,
			INVALID_COMMAND,
			CBUF_ADD_TEXT = 256,
			CONVAR_DISPATCH,
			CREATE_ENTITY_BY_NAME,
			REGISTER_CONCOMMAND,
			CHANGE_LEVEL,
			RESOURCE_MODIFY,
			INVALID_PACKET_MESSAGE
		}
	}
}
