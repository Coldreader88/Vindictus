using System;
using System.Configuration;
using System.Web;
using System.Web.UI;
using Nexon.Com.Net;

namespace Nexon.Com.Web
{
    public abstract class Page : System.Web.UI.Page
    {
        public virtual PageType emErrorPageType
        {
            get
            {
                return PageType.HTML;
            }
        }

        public Page()
        {
        }

        protected override void OnInit(EventArgs e)
        {
            this.CheckDOS();
            this.LoadNDeserializeSession();
            base.OnInit(e);
        }

        public bool IsCheck
        {
            get
            {
                return ConfigurationManager.AppSettings["Nexon.Com.Web.IsUseDosCheck"].Parse(false);
            }
        }

        public int n4MaxAccessCountPerSecond
        {
            get
            {
                return ConfigurationManager.AppSettings["Nexon.Com.Web.n4DosMaxAccessCountPerSecond"].Parse(10);
            }
        }

        public int n4SecondCheckDuration
        {
            get
            {
                return ConfigurationManager.AppSettings["Nexon.Com.Web.n4SecondCheckDuration"].Parse(5);
            }
        }

        public string strDOSErrorPageUrl
        {
            get
            {
                return ConfigurationManager.AppSettings["Nexon.Com.Web.ErrorPageUrl"].Parse("http://bulletin.nexon.com/nxk/error.html?error=503");
            }
        }

        protected virtual void CheckDOS()
        {
            string strCacheName = "Nexon.Com.Web.Page.RequestController";
            RequestController requestController = CacheUtil.Get(strCacheName) as RequestController;
            if (requestController == null)
            {
                requestController = new RequestController(this.n4MaxAccessCountPerSecond, this.n4SecondCheckDuration);
                if (requestController != null)
                {
                    CacheUtil.Insert(strCacheName, requestController, 1440);
                }
            }
            if (requestController != null && this.IsCheck)
            {
                string clientIP = Environment.ClientIP;
                if (clientIP != string.Empty && !NexonIPFilter.IsNexonIP(clientIP) && requestController.IsDoS(clientIP))
                {
                    HttpContext.Current.Response.Redirect(this.strDOSErrorPageUrl);
                }
            }
        }

        protected override void Render(HtmlTextWriter writer)
        {
            this.SerializeNSaveSession();
            base.Render(writer);
        }

        protected virtual void LoadNDeserializeSession()
        {
        }

        protected virtual void SerializeNSaveSession()
        {
        }
    }
}
