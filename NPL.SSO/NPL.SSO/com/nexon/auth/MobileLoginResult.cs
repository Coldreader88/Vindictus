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
	public class MobileLoginResult
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

		public ushort uAge
		{
			get
			{
				return this.uAgeField;
			}
			set
			{
				this.uAgeField = value;
			}
		}

		public bool bNewMembership
		{
			get
			{
				return this.bNewMembershipField;
			}
			set
			{
				this.bNewMembershipField = value;
			}
		}

		public byte nMainAuthLevel
		{
			get
			{
				return this.nMainAuthLevelField;
			}
			set
			{
				this.nMainAuthLevelField = value;
			}
		}

		public byte nSubAuthLevel
		{
			get
			{
				return this.nSubAuthLevelField;
			}
			set
			{
				this.nSubAuthLevelField = value;
			}
		}

		private int nErrorCodeField;

		private string strErrorMessageField;

		private long nNexonSNField;

		private string strNexonIDField;

		private ushort uAgeField;

		private bool bNewMembershipField;

		private byte nMainAuthLevelField;

		private byte nSubAuthLevelField;
	}
}
