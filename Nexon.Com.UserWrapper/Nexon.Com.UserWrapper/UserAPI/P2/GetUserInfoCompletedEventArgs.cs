using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;

namespace Nexon.Com.UserWrapper.UserAPI.P2
{
	[DebuggerStepThrough]
	[GeneratedCode("System.Web.Services", "2.0.50727.5420")]
	[DesignerCategory("code")]
	public class GetUserInfoCompletedEventArgs : AsyncCompletedEventArgs
	{
		internal GetUserInfoCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState) : base(exception, cancelled, userState)
		{
			this.results = results;
		}

		public int Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (int)this.results[0];
			}
		}

		public string strNexonID
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (string)this.results[1];
			}
		}

		public string strNexonName
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (string)this.results[2];
			}
		}

		public string strRealBirth
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (string)this.results[3];
			}
		}

		public byte n1RealBirthCode
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (byte)this.results[4];
			}
		}

		public string strName
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (string)this.results[5];
			}
		}

		public byte n1SexCode
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (byte)this.results[6];
			}
		}

		public string strAreaCode
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (string)this.results[7];
			}
		}

		public string strAddress1
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (string)this.results[8];
			}
		}

		public string strAddress2
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (string)this.results[9];
			}
		}

		public string strEmail
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (string)this.results[10];
			}
		}

		public string strPhone
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (string)this.results[11];
			}
		}

		public string strMobilePhone
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (string)this.results[12];
			}
		}

		public byte n1MobileCompanyCode
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (byte)this.results[13];
			}
		}

		public byte n1MainPageCode
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (byte)this.results[14];
			}
		}

		public byte n1OpenConfigure_Birth
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (byte)this.results[15];
			}
		}

		public byte n1OpenConfigure_Name
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (byte)this.results[16];
			}
		}

		public byte n1OpenConfigure_Sex
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (byte)this.results[17];
			}
		}

		public byte n1OpenConfigure_Area
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (byte)this.results[18];
			}
		}

		public byte n1OpenConfigure_Email
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (byte)this.results[19];
			}
		}

		public byte n1OpenConfigure_Phone
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (byte)this.results[20];
			}
		}

		public byte n1OpenConfigure_School
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (byte)this.results[21];
			}
		}

		public DataSet dsSchoolList
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (DataSet)this.results[22];
			}
		}

		public int n4SchoolTotalCount
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return (int)this.results[23];
			}
		}

		private object[] results;
	}
}
