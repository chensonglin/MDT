using System;

/// <summary>
/// LinkMaker 的摘要说明
/// </summary>
namespace BackGroundSystemControl.WebUI
{
    public class LinkMaker
    {
        /// <summary>
        /// LinkMaker 的摘要说明。
        /// </summary>
        const string FOMRAT_1 = @"<a href=""{0}"" onclick=""{1}"" target=""{2}"" class=""{3}"" style=""{4}"">{5}</a>";

        static public string LinkHtml(string linkText, string linkUrl, string target, string clickScript, string linkClassName, string linkStyles)
        {
            return string.Format(FOMRAT_1, linkUrl, clickScript, target, linkClassName, linkStyles, linkText);
        }

        const string FORMAT_2 = @"<a href=""{0}"">{1}</a>";
        static public string LinkHtml(string linkText, string linkUrl)
        {
            return string.Format(FORMAT_2, linkUrl, linkText);
        }

        const string FORMAT_3 = @"<a href=""{0}"" onclick=""{1}"">{2}</a>";
        static public string LinkHtml(string linkText, string linkUrl, string clickScript)
        {
            return string.Format(FORMAT_3, linkUrl, clickScript, linkUrl);
        }

        static public string LinkWithoutUrlHtml(string linkText, string clickScript)
        {
            return string.Format(FORMAT_3, "#", clickScript, linkText);
        }

        const string FORMAT_4 = @"<a href=""{0}"" class=""{1}"">{2}</a>";
        static public string LinkWithClassHtml(string linkText, string linkUrl, string linkClassName)
        {
            return string.Format(FORMAT_4, linkUrl, linkClassName, linkText);
        }
    }
}