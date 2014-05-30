using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MDT.ManageCenter.DataContract;
using MDT.Utility;

namespace MDT.Console
{
    public partial class UCParameters : UserControl
    {
        public UCParameters()
        {
            InitializeComponent();
        }
        private List<EParameter> dbParameters;
        public List<EParameter> DBParameters
        {
            get {
                return this.dbParameters;
            }          
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.dbParameters.Add(new EParameter() { Name=this.textBox1.Text, Type=this.comboBox1.SelectedText });
            this.tec_parameters.Text = CommonUtility.SerializeXml<List<EParameter>>(this.dbParameters);
        }

        private void UCParameters_Load(object sender, EventArgs e)
        {
            this.dbParameters = new List<EParameter>();
        }
    }
}
