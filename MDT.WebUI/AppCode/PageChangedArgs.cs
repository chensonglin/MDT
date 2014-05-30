using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

namespace MDT.WebUI
{
    /// <summary>
    /// PageChangedArgs 的摘要说明
    /// </summary>
    /// <summary>
    /// 用来传递页号改变事件的参数
    /// </summary>
    public class PageChangedArgs : System.EventArgs
    {
        private int _pageNumber;
        private int _pageSize;

        public PageChangedArgs(int pageNumber, int pageSize)
        {
            this._pageNumber = pageNumber;
            this._pageSize = pageSize;
        }

        public int PageNumber
        {
            get { return _pageNumber; }
        }

        public int PageSize
        {
            get { return _pageSize; }

        }
    }
}