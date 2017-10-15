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
	public class GetOldFashionInfoResult
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

		public string strOldPassport
		{
			get
			{
				return this.strOldPassportField;
			}
			set
			{
				this.strOldPassportField = value;
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

		public uint uMaskInfo
		{
			get
			{
				return this.uMaskInfoField;
			}
			set
			{
				this.uMaskInfoField = value;
			}
		}

		public ulong uSessionKeyHigh
		{
			get
			{
				return this.uSessionKeyHighField;
			}
			set
			{
				this.uSessionKeyHighField = value;
			}
		}

		public ulong uSessionKeyLow
		{
			get
			{
				return this.uSessionKeyLowField;
			}
			set
			{
				this.uSessionKeyLowField = value;
			}
		}

		private int nErrorCodeField;

		private string strErrorMessageField;

		private string strOldPassportField;

		private string strNexonIDField;

		private int nNexonSNField;

		private ushort uAgeField;

		private byte uSexField;

		private uint uMaskInfoField;

		private ulong uSessionKeyHighField;

		private ulong uSessionKeyLowField;
	}
}
