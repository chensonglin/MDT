using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ICSharpCode.TextEditor.Document;
using System.Xml;

namespace MDT.Console
{
    public partial class UCTextEditor : UserControl
    {
        /// <summary>
        /// 信息内容
        /// </summary>
        public string Message
        {
            set
            {
                try
                {
                    XmlDocument doc = new XmlDocument();
                    doc.LoadXml(value);

                    StringBuilder sb = new StringBuilder();
                    XmlWriter xw = XmlWriter.Create(sb, new XmlWriterSettings() { Indent = true });
                    doc.WriteContentTo(xw);
                    xw.Flush();
                    xw.Close();

                    this.textEditorControl1.Text = sb.ToString();
                }
                catch
                {
                    this.textEditorControl1.Text = value;
                }

                textEditorControl1.Refresh();
            }
            get
            {
                return textEditorControl1.Text;
            }
        }

        public string HighlightingStrategy
        {
            set
            {
                this.textEditorControl1.Document.HighlightingStrategy = HighlightingStrategyFactory.CreateHighlightingStrategy(value);
            }
        }

        public UCTextEditor()
        {
            InitializeComponent();
        }

        private void UCTextEditor_Load(object sender, EventArgs e)
        {
            this.textEditorControl1.Document.HighlightingStrategy = HighlightingStrategyFactory.CreateHighlightingStrategy("XML");
            this.textEditorControl1.Document.TextEditorProperties.ShowEOLMarker = true;
            this.textEditorControl1.Document.TextEditorProperties.ShowMatchingBracket = true;
            this.textEditorControl1.Document.TextEditorProperties.EnableFolding = true;
            this.textEditorControl1.Document.TextEditorProperties.IndentStyle = IndentStyle.Auto;
        }
    }
}
