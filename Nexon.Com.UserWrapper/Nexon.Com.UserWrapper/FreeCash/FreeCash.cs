using System;

namespace Nexon.Com.UserWrapper.FreeCash
{
	public static class FreeCash
	{
		public static UserBasicInfo GetUserBasicInfo(ServiceCode serviceCode, string strNexonID)
		{
			UserBasicGetListSoapResult userBasicGetListSoapResult = new UserBasicGetInfoSoapWrapper
			{
				ServiceCode = (int)serviceCode,
				NexonID = strNexonID
			}.Execute();
			if (userBasicGetListSoapResult.SoapErrorCode == 0 && userBasicGetListSoapResult.UserBasicList.Count == 1)
			{
				return userBasicGetListSoapResult.UserBasicList[0];
			}
			return null;
		}
	}
}
