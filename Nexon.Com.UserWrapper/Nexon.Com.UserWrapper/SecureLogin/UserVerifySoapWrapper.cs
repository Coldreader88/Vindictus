using System;
using System.Xml;
using Nexon.Com.DAO;
using Nexon.Com.UserWrapper.UserAPI.SecureLogin;

namespace Nexon.Com.UserWrapper.SecureLogin
{
	internal class UserVerifySoapWrapper : SoapWrapperBase<securelogin, UserVerifySoapResult>
	{
		protected override ServiceCode serviceCode
		{
			get
			{
				return Nexon.Com.ServiceCode.member;
			}
		}

		public UserVerifySoapWrapper()
		{
			this.WebServiceURLProvider = new UserAPIServerURLProvider();
		}

		protected override void ExecuteImpl(out int errorCode, out string errorMessage)
		{
			string text;
			errorCode = base.Soap.GetUserBasicInfo(this._n4ServiceCode, 0, this._strNexonID, string.Empty, this._strPassword, out text);
			errorMessage = string.Empty;
			if (errorCode == 0)
			{
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
								string value = xmlNode2.Attributes.Item(0).Value;
								string value2 = xmlNode2.Attributes.Item(1).Value;
								string a2;
								if ((a2 = value) != null && a2 == "isPwdHashMatch")
								{
									base.Result.PwdMatch = Convert.ToBoolean(value2);
								}
							}
						}
					}
				}
				catch
				{
					throw new UserXMLParseException(text, null);
				}
			}
		}

		internal int ServiceCode
		{
			set
			{
				this._n4ServiceCode = value;
			}
		}

		internal string NexonID
		{
			set
			{
				this._strNexonID = value;
			}
		}

		internal string Password
		{
			set
			{
				this._strPassword = value;
			}
		}

		private int _n4ServiceCode;

		private string _strNexonID;

		private string _strPassword;
	}
}
