using System;
using Nexon.Com.UserWrapper.GuildInGame.DAO;

namespace Nexon.Com.UserWrapper.GuildInGame
{
	public static class GuildInGame
	{
		public static UserBasicInfonRepresentSchoolInfo GetUserInfonSchoolInfo(ServiceCode serviceCode, int nexonSN)
		{
			UserBasicGetListnRepresentSchoolResult userBasicGetListnRepresentSchoolResult = new UserGetInfonSchoolInfoSoapWrapper
			{
				ServiceCode = (int)serviceCode,
				NexonSN = nexonSN,
				NexonID = string.Empty,
				Password = string.Empty
			}.Execute();
			if (userBasicGetListnRepresentSchoolResult.SoapErrorCode != 0)
			{
				throw new UserWrapperException((ErrorCode)userBasicGetListnRepresentSchoolResult.SoapErrorCode, userBasicGetListnRepresentSchoolResult.SoapErrorMessage, null);
			}
			if (userBasicGetListnRepresentSchoolResult.UserBasicList.Count == 1)
			{
				return userBasicGetListnRepresentSchoolResult.UserBasicList[0];
			}
			return null;
		}
	}
}
