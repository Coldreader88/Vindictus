using System;

namespace Nexon.Com.UserWrapper
{
	public enum WriteStatusCode : byte
	{
		Unknown,
		ValidUser,
		NotParentJoinAuthUser,
		NotRealNameCfmUser
	}
}
