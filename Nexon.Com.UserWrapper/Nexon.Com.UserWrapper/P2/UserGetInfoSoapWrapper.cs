using System;
using System.Collections.Generic;
using System.Data;
using Nexon.Com.DAO;
using Nexon.Com.UserWrapper.UserAPI.P2;

namespace Nexon.Com.UserWrapper.P2
{
	internal class UserGetInfoSoapWrapper : SoapWrapperBase<p2, UserGetListSoapResult>
	{
		protected override ServiceCode serviceCode
		{
			get
			{
				return Nexon.Com.ServiceCode.p2;
			}
		}

		public UserGetInfoSoapWrapper()
		{
			this.WebServiceURLProvider = new UserAPIServerURLProvider();
		}

		protected override void ExecuteImpl(out int errorCode, out string errorMessage)
		{
			string empty = string.Empty;
			string empty2 = string.Empty;
			string empty3 = string.Empty;
			byte value = 0;
			string empty4 = string.Empty;
			byte value2 = 0;
			string empty5 = string.Empty;
			string empty6 = string.Empty;
			string empty7 = string.Empty;
			string empty8 = string.Empty;
			string empty9 = string.Empty;
			string empty10 = string.Empty;
			byte value3 = 0;
			byte value4 = 0;
			byte value5 = 0;
			byte value6 = 0;
			byte value7 = 0;
			byte value8 = 0;
			byte value9 = 0;
			byte value10 = 0;
			byte value11 = 0;
			DataSet dataSet = null;
			int num = 0;
			errorCode = base.Soap.GetUserInfo(this._n4ServiceCode, this._n4NexonSN, out empty, out empty2, out empty3, out value, out empty4, out value2, out empty5, out empty6, out empty7, out empty8, out empty9, out empty10, out value3, out value4, out value5, out value6, out value7, out value8, out value9, out value10, out value11, out dataSet, out num);
			errorMessage = string.Empty;
			if (errorCode == 0)
			{
				List<SchoolInfo> list = new List<SchoolInfo>();
				if (dataSet != null && dataSet.Tables[0] != null)
				{
					for (int i = 0; i < dataSet.Tables[0].Rows.Count; i++)
					{
						dataSet.Tables[0].Rows[i]["oidUser"].Parse(0);
						byte schoolCode = dataSet.Tables[0].Rows[i]["codeSchoolType"].Parse<byte>(0);
						int schoolSN = dataSet.Tables[0].Rows[i]["oidSchool"].Parse(0);
						string schoolRealName = dataSet.Tables[0].Rows[i]["strName"].Parse(string.Empty);
						list.Add(new SchoolInfo(schoolSN, (SchoolCode)schoolCode, schoolRealName));
					}
				}
				base.Result.AddUser(new int?(this._n4NexonSN), empty, null, null, empty2, empty4, null, new SexCode?((SexCode)value2), null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, new OpenConfigureCode?((OpenConfigureCode)value6), new OpenConfigureCode?((OpenConfigureCode)value7), null, new OpenConfigureCode?((OpenConfigureCode)value5), new OpenConfigureCode?((OpenConfigureCode)value11), new OpenConfigureCode?((OpenConfigureCode)value8), new OpenConfigureCode?((OpenConfigureCode)value10), null, new OpenConfigureCode?((OpenConfigureCode)value9), null, null, empty8, empty3, new RealBirthCode?((RealBirthCode)value), null, null, null, null, null, null, null, null, null, empty5, empty6, empty7, empty9, new MobileCompanyCode?((MobileCompanyCode)value3), empty10, new MainPageCode?((MainPageCode)value4), list, null, null);
			}
		}

		internal int ServiceCode
		{
			set
			{
				this._n4ServiceCode = value;
			}
		}

		internal int NexonSN
		{
			set
			{
				this._n4NexonSN = value;
			}
		}

		private int _n4ServiceCode;

		private int _n4NexonSN;
	}
}
