using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace NPL.SSO.com.nexon.auth
{
	[DebuggerStepThrough]
	[GeneratedCode("System.Xml", "2.0.50727.5476")]
	[XmlType(Namespace = "http://tempuri.org/")]
	[DesignerCategory("code")]
	[Serializable]
	public class UpdateSessionResult
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

		public string strNewPassport
		{
			get
			{
				return this.strNewPassportField;
			}
			set
			{
				this.strNewPassportField = value;
			}
		}

		public uint uUnreadNoteCount
		{
			get
			{
				return this.uUnreadNoteCountField;
			}
			set
			{
				this.uUnreadNoteCountField = value;
			}
		}

		public uint uStatusFlag
		{
			get
			{
				return this.uStatusFlagField;
			}
			set
			{
				this.uStatusFlagField = value;
			}
		}

		public uint uUpdateInterval
		{
			get
			{
				return this.uUpdateIntervalField;
			}
			set
			{
				this.uUpdateIntervalField = value;
			}
		}

		private int nErrorCodeField;

		private string strErrorMessageField;

		private string strNewPassportField;

		private uint uUnreadNoteCountField;

		private uint uStatusFlagField;

		private uint uUpdateIntervalField;
	}
}
