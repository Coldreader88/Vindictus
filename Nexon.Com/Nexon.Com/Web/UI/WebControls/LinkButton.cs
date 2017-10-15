using System;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Nexon.Com.Web.UI.WebControls
{
    public class LinkButton : System.Web.UI.WebControls.LinkButton
    {
        public LinkButton()
        {
            this.EnableViewState = false;
        }

        protected override void Render(HtmlTextWriter writer)
        {
            writer.WriteBeginTag("a");
            writer.WriteAttribute("href", this.PostBackUrl);
            foreach (string text in base.Attributes.Keys)
            {
                writer.WriteAttribute(text, base.Attributes[text]);
            }
            writer.Write(">");
            writer.Write(this.Text);
            writer.WriteEndTag("a");
        }
    }
}
