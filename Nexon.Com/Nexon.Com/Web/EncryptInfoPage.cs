using System;
using System.Text;
using System.Web.UI;
using Nexon.Com.Encryption;
using Nexon.Com.Log;

namespace Nexon.Com.Web
{
	public abstract class EncryptInfoPage : Page
	{
		public EncryptInfoPage()
		{
			this._rsa = new RSAWrapper();
		}

		protected sealed override void Render(HtmlTextWriter writer)
		{
			base.Response.Clear();
			base.Response.Flush();
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("NgbSecurity.HandleResponse('");
			stringBuilder.Append("<nxaml>");
			stringBuilder.Append("<object name=\"result\">");
			try
			{
				stringBuilder.Append("<string name=\"e\" value=\"" + this.GetEncryptExponent() + "\" />");
				stringBuilder.Append("<string name=\"m\" value=\"" + this.GetModulus() + "\" />");
				stringBuilder.Append("<string name=\"h\" value=\"" + this.CreateHashKey() + "\" />");
			}
			catch (Exception ex)
			{
				int num;
				DateTime dateTime;
				ErrorLog.CreateErrorLog(ServiceCode.framework, 70000, null, ex.Message, ex.StackTrace, out num, out dateTime);
			}
			stringBuilder.Append("</object>");
			stringBuilder.Append("</nxaml>");
			stringBuilder.Append("');");
			writer.Write(stringBuilder.ToString());
		}

		protected virtual string GetEncryptExponent()
		{
			return this._rsa.GetEncryptExponent();
		}

		protected virtual string GetModulus()
		{
			return this._rsa.GetModulus();
		}

		protected virtual string CreateHashKey()
		{
			return this._rsa.CreateHashKey();
		}

		private RSAWrapper _rsa;
	}
}
