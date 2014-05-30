using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace MDT.WebUI
{
    public class JavaScriptFunction
    {
        public static string AddScript(string nakedScript)
        {
            StringBuilder script = new StringBuilder();
            script.Append("<SCRIPT language=\"JavaScript\">\r\n");
            script.Append(nakedScript);
            script.Append("</SCRIPT>");
            return script.ToString();
        }

        public static string AddHrefScript(string nakedScript)
        {
            StringBuilder script = new StringBuilder();
            script.Append("javascript:");
            script.Append(nakedScript);
            return script.ToString();
        }

        public static string Alert(string s)
        {
            StringBuilder script = new StringBuilder();
            script.Append("<SCRIPT language=\"JavaScript\">\r\n");
            script.Append("window.alert('" + s.Replace("\'", "\\\'").Replace("\n", "\\n") + "');\r\n");
            script.Append("</SCRIPT>");
            return script.ToString();
        }

        public static string ParentAlert(string s)
        {
            StringBuilder script = new StringBuilder();
            script.Append("<SCRIPT language=\"JavaScript\">\r\n");
            script.Append("window.opener.alert('" + s.Replace("\'", "\\\'").Replace("\n", "\\n") + "');\r\n");
            script.Append("</SCRIPT>");
            return script.ToString();
        }


        public static string Back()
        {
            StringBuilder script = new StringBuilder();
            script.Append("<SCRIPT language=\"JavaScript\">\r\n");
            script.Append("window.history.back()");
            script.Append("</SCRIPT>");
            return script.ToString();
        }

        public static string Close()
        {
            StringBuilder script = new StringBuilder();
            script.Append("<SCRIPT language=\"JavaScript\">\r\n");
            script.Append("window.close()");
            script.Append("</SCRIPT>");
            return script.ToString();
        }

        public static string Refresh()
        {
            StringBuilder script = new StringBuilder();
            script.Append("<SCRIPT language=\"JavaScript\">\r\n");
            script.Append("window.refresh();");
            script.Append("</SCRIPT>");
            return script.ToString();
        }

        public static string RefreshOpener()
        {
            StringBuilder script = new StringBuilder();
            script.Append("<SCRIPT language=\"JavaScript\">\r\n");
            script.Append("window.opener.RefreshWindows();");
            script.Append("</SCRIPT>");
            return script.ToString();
        }

        public static string RefreshOpenerAndClose()
        {
            StringBuilder script = new StringBuilder();
            script.Append("<SCRIPT language=\"JavaScript\">\r\n");
            script.Append("window.opener.refresh();");
            script.Append("window.close();");
            script.Append("</SCRIPT>");
            return script.ToString();
        }

        /// <summary>
        /// 关闭当前窗口并刷新父窗口的脚本
        /// </summary>
        /// <returns></returns>
        public static string CloseAndRefreshParent()
        {
            StringBuilder script = new StringBuilder();
            script.Append("<SCRIPT language=\"JavaScript\">\r\n");
            script.Append("window.opener.location.href = window.opener.location.href;\r\n");
            script.Append("if (window.opener.progressWindow)\r\n");
            script.Append("window.opener.progressWindow.close();\r\n");
            script.Append("window.close();\r\n");
            script.Append("</SCRIPT>");
            return script.ToString();
        }


        public static string GetArgumentString(params string[] param)
        {
            StringBuilder argString = new StringBuilder("");

            for (int i = 0; i < param.Length - 1; i++)
            {
                string paramArg = "'" + param[i].Replace("\'", "\\\'") + "',";
                argString.Append(paramArg);
            }

            if (param.Length > 0)
            {
                string paramArg = "'" + param[param.Length - 1].Replace("\'", "\\\'") + "'";
                argString.Append(paramArg);
            }

            return argString.ToString();
        }
    }
}