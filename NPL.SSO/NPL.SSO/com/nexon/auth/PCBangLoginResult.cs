using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace NPL.SSO.com.nexon.auth
{
	[XmlType(Namespace = "http://tempuri.org/")]
	[DesignerCategory("code")]
	[GeneratedCode("System.Xml", "2.0.50727.5476")]
	[DebuggerStepThrough]
	[Serializable]
	public class PCBangLoginResult
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

		public string strPassport
		{
			get
			{
				return this.strPassportField;
			}
			set
			{
				this.strPassportField = value;
			}
		}

		public long nNexonSN
		{
			get
			{
				return this.nNexonSNField;
			}
			set
			{
				this.nNexonSNField = value;
			}
		}

		public string strNexonID
		{
			get
			{
				return this.strNexonIDField;
			}
			set
			{
				this.strNexonIDField = value;
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

		private string strPassportField;

		private long nNexonSNField;

		private string strNexonIDField;

		private uint uUnreadNoteCountField;

		private uint uUpdateIntervalField;
	}
}
