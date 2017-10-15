using System;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Nexon.Com.Web.UI.WebControls
{
	public class RadioButtonList : System.Web.UI.WebControls.RadioButtonList
    {
		public RadioButtonList()
		{
			this.EnableViewState = false;
		}

		protected override void Render(HtmlTextWriter writer)
		{
			foreach (object obj in this.Items)
			{
				ListItem listItem = (ListItem)obj;
				writer.WriteBeginTag("input");
				writer.WriteAttribute("type", "radio");
				writer.WriteAttribute("name", this.UniqueID);
				writer.WriteAttribute("value", listItem.Value);
				if (listItem.Selected)
				{
					writer.WriteAttribute("checked", "true");
				}
				writer.Write(" />");
				writer.Write(listItem.Text);
			}
		}
	}
}
