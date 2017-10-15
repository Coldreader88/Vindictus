using System;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Nexon.Com.Web.UI.WebControls
{
	public class PagingContainer : Label
	{
		public PagingContainer()
		{
			this.EnableViewState = false;
		}

		protected virtual string RequestParam(int n4PageNo)
		{
			Url url = new Url();
			url[this.strPageNoParamName] = n4PageNo.ToString();
			return url.URL;
		}

		protected virtual void RenderFirstPage(bool isExist, HtmlTextWriter writer, int n4PageNo)
		{
			if (this.strPageGoFirstLabel != null && this.strPageGoFirstLabel != string.Empty)
			{
				if (isExist)
				{
					writer.Write("<a href='{1}'>{0}</a>", this.strPageGoFirstLabel, this.RequestParam(n4PageNo));
					return;
				}
				writer.Write("{0}", this.strPageGoFirstLabel);
			}
		}

		protected virtual void RenderPrevPage(bool isExist, HtmlTextWriter writer, int n4PageNo)
		{
			if (this.strPageGoPrevLabel != null && this.strPageGoPrevLabel != string.Empty)
			{
				if (isExist)
				{
					writer.Write(" <a href='{1}'>{0}</a>", this.strPageGoPrevLabel, this.RequestParam(n4PageNo));
					return;
				}
				writer.Write(" {0}", this.strPageGoPrevLabel);
			}
		}

		protected virtual void RenderPage(bool isSelected, HtmlTextWriter writer, int n4PageNo)
		{
			if (isSelected)
			{
				writer.Write(" <b>{0}</b> ", n4PageNo);
				return;
			}
			writer.Write(" <a href='{1}'>{0}</a> ", n4PageNo, this.RequestParam(n4PageNo));
		}

		protected virtual void RenderPageSeperater(HtmlTextWriter writer)
		{
			writer.Write("|");
		}

		protected virtual void RenderNextPage(bool isExist, HtmlTextWriter writer, int n4PageNo)
		{
			if (this.strPageGoPrevLabel != null && this.strPageGoPrevLabel != string.Empty)
			{
				if (isExist)
				{
					writer.Write("<a href='{1}'>{0}</a>", this.strPageGoNextLabel, this.RequestParam(n4PageNo));
					return;
				}
				writer.Write("{0}", this.strPageGoNextLabel);
			}
		}

		protected virtual void RenderLastPage(bool isExist, HtmlTextWriter writer, int n4PageNo)
		{
			if (this.strPageGoFirstLabel != null && this.strPageGoFirstLabel != string.Empty)
			{
				if (isExist)
				{
					writer.Write(" <a href='{1}'>{0}</a>", this.strPageGoLastLabel, this.RequestParam(n4PageNo));
					return;
				}
				writer.Write(" {0}", this.strPageGoLastLabel);
			}
		}

		protected override void Render(HtmlTextWriter writer)
		{
			int num = (this.n4TotalRowCount - 1) / this.n4PageSize + 1;
			int num2 = (this.n4PageNo - 1) / this.n4BlockSize * this.n4BlockSize + 1;
			int num3 = (num - 1) / this.n4BlockSize * this.n4BlockSize + 1;
			int num4 = (num2 + this.n4BlockSize > num) ? num : (num2 + this.n4BlockSize - 1);
			if (this.n4PageNo > this.n4BlockSize)
			{
				this.RenderFirstPage(true, writer, 1);
				this.RenderPrevPage(true, writer, num2 - this.n4BlockSize);
			}
			else
			{
				this.RenderFirstPage(false, writer, 1);
				this.RenderPrevPage(false, writer, num2 - this.n4BlockSize);
			}
			int i = num2;
			int num5 = 0;
			while (i <= num4)
			{
				if (this.n4PageNo == i)
				{
					this.RenderPage(true, writer, i);
				}
				else
				{
					this.RenderPage(false, writer, i);
				}
				if (i != num4)
				{
					this.RenderPageSeperater(writer);
				}
				i++;
				num5++;
			}
			if ((num3 - 1) / this.n4BlockSize + 1 > (num2 - 1) / this.n4BlockSize + 1)
			{
				this.RenderNextPage(true, writer, num2 + this.n4BlockSize);
				this.RenderLastPage(true, writer, num);
				return;
			}
			this.RenderNextPage(false, writer, num2 + this.n4BlockSize);
			this.RenderLastPage(false, writer, num);
		}

		public string strPageGoFirstLabel;

		public string strPageGoPrevLabel;

		public string strPageGoNextLabel;

		public string strPageGoLastLabel;

		public string strPageNoParamName = "n4PageNo";

		public int n4TotalRowCount;

		public int n4PageNo = 1;

		public int n4PageSize = 10;

		public int n4BlockSize = 10;
	}
}
