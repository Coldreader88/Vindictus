using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace NPL.SSO.com.nexon.auth
{
	[DesignerCategory("code")]
	[GeneratedCode("System.Xml", "2.0.50727.5476")]
	[XmlType(Namespace = "http://tempuri.org/")]
	[DebuggerStepThrough]
	[Serializable]
	public class CheckSessionAndGetInfoResult
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

		public int nNexonSN
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

		public uint uIP
		{
			get
			{
				return this.uIPField;
			}
			set
			{
				this.uIPField = value;
			}
		}

		public byte uSex
		{
			get
			{
				return this.uSexField;
			}
			set
			{
				this.uSexField = value;
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

		public uint uPwdHash
		{
			get
			{
				return this.uPwdHashField;
			}
			set
			{
				this.uPwdHashField = value;
			}
		}

		public uint uSsnHash
		{
			get
			{
				return this.uSsnHashField;
			}
			set
			{
				this.uSsnHashField = value;
			}
		}

		public uint uFlag0
		{
			get
			{
				return this.uFlag0Field;
			}
			set
			{
				this.uFlag0Field = value;
			}
		}

		private int nErrorCodeField;

		private string strErrorMessageField;

		private uint uUnreadNoteCountField;

		private uint uStatusFlagField;

		private uint uUpdateIntervalField;

		private int nNexonSNField;

		private string strNexonIDField;

		private uint uIPField;

		private byte uSexField;

		private ushort uAgeField;

		private uint uPwdHashField;

		private uint uSsnHashField;

		private uint uFlag0Field;
	}
}
