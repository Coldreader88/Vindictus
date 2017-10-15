using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace NPL.SSO.com.nexon.auth
{
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://tempuri.org/")]
	[DebuggerStepThrough]
	[GeneratedCode("System.Xml", "2.0.50727.5476")]
	[Serializable]
	public class UpgradeGameTokenResult
	{
		public int nErrorCode
		{
			get
			{
				return this.nErrorCodeField;
			}
			set
			{
				this.nErrorCodeField = value;
			}
		}

		public string strErrorMessage
		{
			get
			{
				return this.strErrorMessageField;
			}
			set
			{
				this.strErrorMessageField = value;
			}
		}

		public string strToken
		{
			get
			{
				return this.strTokenField;
			}
			set
			{
				this.strTokenField = value;
			}
		}

		private int nErrorCodeField;

		private string strErrorMessageField;

		private string strTokenField;
	}
}
