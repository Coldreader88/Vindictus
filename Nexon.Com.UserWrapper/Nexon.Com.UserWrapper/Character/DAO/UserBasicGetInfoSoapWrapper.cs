using System;
using System.Xml;
using Nexon.Com.DAO;
using Nexon.Com.UserWrapper.UserAPI.Character;

namespace Nexon.Com.UserWrapper.Character.DAO
{
	internal class UserBasicGetInfoSoapWrapper : SoapWrapperBase<character, UserBasicGetListSoapResult>
	{
		protected override ServiceCode serviceCode
		{
			get
			{
				return Nexon.Com.ServiceCode.member;
			}
		}

		internal int ServiceCode { get; set; }

		internal int NexonSN { get; set; }

		internal string NexonID { get; set; }

		internal string Password { get; set; }

		public UserBasicGetInfoSoapWrapper()
		{
			this.WebServiceURLProvider = new UserAPIServerURLProvider();
		}

		protected override void ExecuteImpl(out int errorCode, out string errorMessage)
		{
			string text;
			errorCode = base.Soap.GetUserBasicInfo(this.ServiceCode, this.NexonSN, this.NexonID, this.Password, out text);
			errorMessage = string.Empty;
			if (errorCode != 0)
			{
				return;
			}
			bool value = false;
			bool value2 = false;
			bool value3 = false;
			bool value4 = false;
			bool value5 = false;
			bool value6 = false;
			bool value7 = false;
			int value8 = 0;
			string name = string.Empty;
			string nexonID = string.Empty;
			string ssn = string.Empty;
			byte value9 = 0;
			string email = string.Empty;
			try
			{
				XmlDocument xmlDocument = new XmlDocument();
				xmlDocument.LoadXml(text);
				if (!xmlDocument.DocumentElement.Name.Equals("nxaml"))
				{
					throw new UserXMLParseException(text, null);
				}
				if (!xmlDocument.DocumentElement.HasChildNodes)
				{
					throw new UserXMLParseException(text, null);
				}
				foreach (object obj in xmlDocument.DocumentElement.ChildNodes)
				{
					XmlNode xmlNode = obj as XmlNode;
					string a = (xmlNode.Attributes.Item(0).Value != null) ? xmlNode.Attributes.Item(0).Value.ToLower() : string.Empty;
					if (a == "object" || a == "request" || a == "result")
					{
						foreach (object obj2 in xmlNode.ChildNodes)
						{
							XmlNode xmlNode2 = obj2 as XmlNode;
							string value10 = xmlNode2.Attributes.Item(0).Value;
							string value11 = xmlNode2.Attributes.Item(1).Value;
							string key;
							switch (key = value10)
							{
							case "isExistUser":
								value = Convert.ToBoolean(value11);
								break;
							case "isPwdHashMatch":
								value2 = Convert.ToBoolean(value11);
								break;
							case "isNotOwnerCfm":
								value3 = Convert.ToBoolean(value11);
								break;
							case "isBlockByAdmin":
								value4 = Convert.ToBoolean(value11);
								break;
							case "isBlockByEvent":
								value5 = Convert.ToBoolean(value11);
								break;
							case "isTempBlockByLoginFail":
								value6 = Convert.ToBoolean(value11);
								break;
							case "isTempBlockByWarning":
								value7 = Convert.ToBoolean(value11);
								break;
							case "strName":
								name = Convert.ToString(value11);
								break;
							case "strNexonID":
								nexonID = Convert.ToString(value11);
								break;
							case "strSsn":
								ssn = Convert.ToString(value11);
								break;
							case "n1Age":
								value9 = Convert.ToByte(value11);
								break;
							case "strEmail":
								email = Convert.ToString(value11);
								break;
							case "n4NexonSN":
								value8 = Convert.ToInt32(value11);
								break;
							}
						}
					}
				}
			}
			catch
			{
				throw new UserXMLParseException(text, null);
			}
			base.Result.AddUserBasic(new int?(value8), nexonID, null, null, null, name, ssn, null, new byte?(value9), new bool?(value6), new bool?(value7), null, null, null, null, null, null, null, null, new bool?(value3), new bool?(value4), null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, email, null, null, null, null, null, null, null, null, null, null, null, null, new bool?(value), new bool?(value2), new bool?(value5));
		}
	}
}
