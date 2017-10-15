using System;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Nexon.Com.Web.UI.WebControls
{
    public class Label : System.Web.UI.WebControls.Label
    {
        public Label()
        {
            this.EnableViewState = false;
        }

        protected override void Render(HtmlTextWriter writer)
        {
            writer.Write(this.Text);
        }
    }
}
