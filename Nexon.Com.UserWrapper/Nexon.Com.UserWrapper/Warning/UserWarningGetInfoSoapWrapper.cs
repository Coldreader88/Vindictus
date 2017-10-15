using System;
using System.Xml;
using Nexon.Com.DAO;
using Nexon.Com.UserWrapper.UserAPI.Warning;

namespace Nexon.Com.UserWrapper.Warning
{
	internal class UserWarningGetInfoSoapWrapper : SoapWrapperBase<warning, UserWarningGetListSoapResult>
	{
		protected override ServiceCode serviceCode
		{
			get
			{
				return Nexon.Com.ServiceCode.warningsession;
			}
		}

		public UserWarningGetInfoSoapWrapper()
		{
			this.WebServiceURLProvider = new UserAPIServerURLProvider();
		}

		protected override void ExecuteImpl(out int errorCode, out string errorMessage)
		{
			string nexonID = string.Empty;
			DateTime createDate = DateTime.MinValue;
			DateTime lastModifyDate = DateTime.MinValue;
			byte totalWarningPoint = 0;
			DateTime endWarningDate = DateTime.MinValue;
			string backOfficeUser = string.Empty;
			string text;
			errorCode = base.Soap.GetWarningInfo(this._n4ServiceCode, this._n4NexonSN, this._n4GameCode, out text);
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
								string key;
								switch (key = value)
								{
								case "strNexonID":
									nexonID = Convert.ToString(value2);
									break;
								case "dtCreateDate":
									createDate = Convert.ToDateTime(value2);
									break;
								case "dtLastModifyDate":
									lastModifyDate = Convert.ToDateTime(value2);
									break;
								case "n1TotalWarningPoint":
									totalWarningPoint = Convert.ToByte(value2);
									break;
								case "dtEndWarningDate":
									endWarningDate = Convert.ToDateTime(value2);
									break;
								case "strBackOfficeUser":
									backOfficeUser = Convert.ToString(value2);
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
				base.Result.AddUserWarning(nexonID, createDate, lastModifyDate, totalWarningPoint, endWarningDate, backOfficeUser);
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

		internal int GameCode
		{
			set
			{
				this._n4GameCode = value;
			}
		}

		private int _n4ServiceCode;

		private int _n4NexonSN;

		private int _n4GameCode;
	}
}
