using System;
using System.Runtime.InteropServices;

namespace AdminClientServiceCore.Messages
{
	[Guid("7A770DC5-A70B-4513-8A23-14127938236A")]
	public enum NotifyCode
	{
		ERROR = -1,
		INFO,
		SUCCESS
	}
}
