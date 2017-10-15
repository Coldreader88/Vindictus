using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace NPL.SSO.com.nexon.auth
{
	[XmlType(Namespace = "http://tempuri.org/")]
	[GeneratedCode("System.Xml", "2.0.50727.5476")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[Serializable]
	public class IsLoginResult
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

		public string strClientIP
		{
			get
			{
				return this.strClientIPField;
			}
			set
			{
				this.strClientIPField = value;
			}
		}

		public ulong uLoginTime
		{
			get
			{
				return this.uLoginTimeField;
			}
			set
			{
				this.uLoginTimeField = value;
			}
		}

		private int nErrorCodeField;

		private string strErrorMessageField;

		private string strClientIPField;

		private ulong uLoginTimeField;
	}
}
