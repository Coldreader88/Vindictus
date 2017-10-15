using System;
using System.Web;
using System.Web.UI.WebControls;

namespace Nexon.Com.Web.UI.WebControls
{
	public class SelectBox : DropDownList
	{
		public SelectBox()
		{
			this.EnableViewState = false;
		}

		public override string SelectedValue
		{
			get
			{
				if (HttpContext.Current.Request.Form[this.UniqueID] != null)
				{
					return HttpContext.Current.Request.Form[this.UniqueID];
				}
				return base.SelectedValue;
			}
			set
			{
				base.SelectedValue = value;
			}
		}
	}
}
