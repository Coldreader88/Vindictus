using System;
using System.Xml;
using Nexon.Com.DAO;
using Nexon.Com.UserWrapper.UserAPI.SecureLogin;

namespace Nexon.Com.UserWrapper.SecureLogin
{
	internal class UserBasicGetInfoSoapWrapper : SoapWrapperBase<securelogin, UserBasicGetListSoapResult>
	{
		protected override ServiceCode serviceCode
		{
			get
			{
				return Nexon.Com.ServiceCode.member;
			}
		}

		public UserBasicGetInfoSoapWrapper()
		{
			this.WebServiceURLProvider = new UserAPIServerURLProvider();
		}

		protected override void ExecuteImpl(out int errorCode, out string errorMessage)
		{
			errorCode = 0;
			errorMessage = string.Empty;
			int value = this._n4NexonSN;
			string nexonID = this._strNexonID;
			string ssn = string.Empty;
			string name = string.Empty;
			bool? isIpinUser = new bool?(false);
			string text;
			if (this._n4NexonSN > 0)
			{
				errorCode = base.Soap.GetUserBasicInfo(this._n4ServiceCode, this._n4NexonSN, string.Empty, string.Empty, string.Empty, out text);
			}
			else
			{
				if (string.IsNullOrEmpty(this._strNexonID))
				{
					throw new UserWrapperException(ErrorCode.Common_InvalidInputData, "조회할 NexonSN 또는 NexonID를 입력해야 합니다.", null);
				}
				errorCode = base.Soap.GetUserBasicInfo(this._n4ServiceCode, 0, this._strNexonID, string.Empty, string.Empty, out text);
			}
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
								string value2 = xmlNode2.Attributes.Item(0).Value;
								string value3 = xmlNode2.Attributes.Item(1).Value;
								string a2;
								if ((a2 = value2) != null)
								{
									if (!(a2 == "n4NexonSN"))
									{
										if (!(a2 == "strNexonID"))
										{
											if (!(a2 == "strName"))
											{
												if (!(a2 == "strSsn"))
												{
													if (a2 == "strIpinNo")
													{
														string text2 = value3;
														if (text2 == null)
														{
															isIpinUser = new bool?(false);
														}
														isIpinUser = new bool?(!string.IsNullOrEmpty(text2.Trim()));
													}
												}
												else
												{
													ssn = Convert.ToString(value3);
												}
											}
											else
											{
												name = Convert.ToString(value3);
											}
										}
										else
										{
											nexonID = Convert.ToString(value3);
										}
									}
									else
									{
										value = Convert.ToInt32(value3);
									}
								}
							}
						}
					}
				}
				catch
				{
					throw new UserXMLParseException(text, null);
				}
				base.Result.AddUserBasic(new int?(value), nexonID, null, null, null, name, ssn, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, isIpinUser);
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

		internal string NexonID
		{
			set
			{
				this._strNexonID = value;
			}
		}

		private int _n4ServiceCode;

		private int _n4NexonSN;

		private string _strNexonID;
	}
}
