using System;
using System.Xml;
using Nexon.Com.DAO;
using Nexon.Com.UserWrapper.UserAPI.GuildInGame;

namespace Nexon.Com.UserWrapper.GuildInGame.DAO
{
	internal class UserGetInfonSchoolInfoSoapWrapper : SoapWrapperBase<guildingame, UserBasicGetListnRepresentSchoolResult>
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

		public UserGetInfonSchoolInfoSoapWrapper()
		{
			this.WebServiceURLProvider = new UserAPIServerURLProvider();
		}

		protected override void ExecuteImpl(out int errorCode, out string errorMessage)
		{
			string text;
			int value;
			string schoolRealName;
			errorCode = base.Soap.GetUserInfonSchoolInfo(this.ServiceCode, this.NexonSN, this.NexonID, this.Password, out text, out value, out schoolRealName);
			errorMessage = string.Empty;
			if (errorCode != 0)
			{
				return;
			}
			int value2 = 0;
			string nexonID = string.Empty;
			string nexonName = string.Empty;
			string name = string.Empty;
			bool value3 = false;
			string realBirth = string.Empty;
			byte value4 = 0;
			byte value5 = 0;
			bool value6 = false;
			bool value7 = false;
			bool value8 = false;
			bool value9 = false;
			string email = string.Empty;
			byte value10 = 0;
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
							string value11 = xmlNode2.Attributes.Item(0).Value;
							string value12 = xmlNode2.Attributes.Item(1).Value;
							string key;
							switch (key = value11)
							{
							case "n4NexonSN":
								value2 = Convert.ToInt32(value12);
								break;
							case "strNexonID":
								nexonID = Convert.ToString(value12);
								break;
							case "strNexonName":
								nexonName = Convert.ToString(value12);
								break;
							case "strName":
								name = Convert.ToString(value12);
								break;
							case "isExistUser":
								value3 = Convert.ToBoolean(value12);
								break;
							case "strRealBirth":
								realBirth = Convert.ToString(value12);
								break;
							case "n1RealBirthCode":
								value4 = Convert.ToByte(value12);
								break;
							case "n1SexCode":
								value5 = Convert.ToByte(value12);
								break;
							case "isPwdHashMatch":
								value6 = Convert.ToBoolean(value12);
								break;
							case "isNotOwnerCfm":
								value7 = Convert.ToBoolean(value12);
								break;
							case "isBlockByAdmin":
								value8 = Convert.ToBoolean(value12);
								break;
							case "isBlockByEvent":
								value9 = Convert.ToBoolean(value12);
								break;
							case "strEmail":
								email = Convert.ToString(value12);
								break;
							case "n1Age":
								value10 = Convert.ToByte(value12);
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
			base.Result.AddUserBasic(new int?(value2), nexonID, null, null, nexonName, name, null, new byte?(value5), new byte?(value10), null, null, null, null, null, null, null, null, null, null, new bool?(value7), new bool?(value8), null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, email, realBirth, new byte?(value4), null, null, null, null, null, null, null, null, null, null, new bool?(value3), new bool?(value6), new bool?(value9), new int?(value), null, schoolRealName);
		}
	}
}
