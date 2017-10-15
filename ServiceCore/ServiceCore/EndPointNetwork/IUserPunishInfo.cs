using System;

namespace ServiceCore.EndPointNetwork
{
	public interface IUserPunishInfo
	{
		UserPunishStatus Status { get; }

		DateTime? ExpireTime { get; }
	}
}
