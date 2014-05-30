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
    /// SortEventArgs 的摘要说明
    /// </summary>
    public class SortEventArgs
    {
        private string _fieldName;
        private bool _isSortDesc;

        public SortEventArgs(string fieldName, bool isSortDesc)
        {
            _fieldName = fieldName;
            _isSortDesc = isSortDesc;
        }

        public string FieldName
        {
            get { return _fieldName; }
        }

        public bool IsSortDesc
        {
            get { return _isSortDesc; }
        }
    }
}