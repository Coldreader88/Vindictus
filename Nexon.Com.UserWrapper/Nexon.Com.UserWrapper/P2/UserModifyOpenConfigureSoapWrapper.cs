using System;
using Nexon.Com.DAO;
using Nexon.Com.UserWrapper.UserAPI.P2;

namespace Nexon.Com.UserWrapper.P2
{
	internal class UserModifyOpenConfigureSoapWrapper : SoapWrapperBase<p2, UserModifyOpenConfigureSoapResult>
	{
		protected override ServiceCode serviceCode
		{
			get
			{
				return Nexon.Com.ServiceCode.p2;
			}
		}

		internal int ServiceCode { get; set; }

		internal int NexonSN { get; set; }

		internal byte OpenConfigure_Birth { get; set; }

		internal byte OpenConfigure_Name { get; set; }

		internal byte OpenConfigure_Sex { get; set; }

		internal byte OpenConfigure_Area { get; set; }

		internal byte OpenConfigure_Email { get; set; }

		internal byte OpenConfigure_Phone { get; set; }

		internal byte OpenConfigure_School { get; set; }

		public UserModifyOpenConfigureSoapWrapper()
		{
			this.WebServiceURLProvider = new UserAPIServerURLProvider();
		}

		protected override void ExecuteImpl(out int errorCode, out string errorMessage)
		{
			errorCode = base.Soap.ModifyOpenConfigure(this.ServiceCode, this.NexonSN, this.OpenConfigure_Birth, this.OpenConfigure_Name, this.OpenConfigure_Sex, this.OpenConfigure_Area, this.OpenConfigure_Email, this.OpenConfigure_Phone, this.OpenConfigure_School);
			errorMessage = string.Empty;
			if (errorCode == 0)
			{
				base.Result.IsSuccess = true;
				return;
			}
			base.Result.IsSuccess = false;
		}
	}
}
