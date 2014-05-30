using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ICSharpCode.TextEditor.Document;
using System.Xml;

namespace MDT.Console
{
    public partial class FormMessage : Form
    {
        /// <summary>
        /// 标题信息
        /// </summary>
        public string Caption
        {
            set { Text = "信息内容 - " + value; }
        }

        /// <summary>
        /// 信息内容
        /// </summary>
        public string Message
        {
            set
            {
                ucTextEditor1.Message = value;
            }
        }

        public FormMessage()
        {
            InitializeComponent();
        }
    }
}
