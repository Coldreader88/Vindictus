using System;
using System.Web.UI.WebControls;

namespace Nexon.Com.Web.UI.WebControls
{
	public class TextBox : System.Web.UI.WebControls.TextBox
    {
		public TextBox()
		{
			this.EnableViewState = false;
		}
	}
}
