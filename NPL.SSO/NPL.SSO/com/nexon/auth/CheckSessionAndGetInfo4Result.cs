using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace NPL.SSO.com.nexon.auth
{
	[GeneratedCode("System.Xml", "2.0.50727.5476")]
	[XmlType(Namespace = "http://tempuri.org/")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[Serializable]
	public class CheckSessionAndGetInfo4Result
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

		public long nNexonSN64
		{
			get
			{
				return this.nNexonSN64Field;
			}
			set
			{
				this.nNexonSN64Field = value;
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

		public string strNationCode
		{
			get
			{
				return this.strNationCodeField;
			}
			set
			{
				this.strNationCodeField = value;
			}
		}

		public string strMetaData
		{
			get
			{
				return this.strMetaDataField;
			}
			set
			{
				this.strMetaDataField = value;
			}
		}

		public byte uSecureCode
		{
			get
			{
				return this.uSecureCodeField;
			}
			set
			{
				this.uSecureCodeField = value;
			}
		}

		public byte uChannelCode
		{
			get
			{
				return this.uChannelCodeField;
			}
			set
			{
				this.uChannelCodeField = value;
			}
		}

		public string strChannelUID
		{
			get
			{
				return this.strChannelUIDField;
			}
			set
			{
				this.strChannelUIDField = value;
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

		private uint uUnreadNoteCountField;

		private uint uStatusFlagField;

		private uint uUpdateIntervalField;

		private long nNexonSN64Field;

		private string strNexonIDField;

		private uint uIPField;

		private byte uSexField;

		private ushort uAgeField;

		private string strNationCodeField;

		private string strMetaDataField;

		private byte uSecureCodeField;

		private byte uChannelCodeField;

		private string strChannelUIDField;

		private bool bNewMembershipField;

		private byte nMainAuthLevelField;

		private byte nSubAuthLevelField;
	}
}
