using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Security.Cryptography;
using System.IO;

namespace MDT.ServiceMonitorEncrypt
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
            Clear();
            txtDecodePlainText.Text = String.Empty;
            txtDecodeSecurtyText.Text = String.Empty;
            btnDecodeCopy.Enabled = false;
        }

        private void Clear()
        {
            //foreach (Control txt in this.Controls)
            //{
            //    if (txt.GetType() == typeof(TextBox))
            //    {
            //        (txt as TextBox).Text = String.Empty;
            //    }
            //}
            txtPlaintext.Text = String.Empty;
            txtCiphertext.Text = String.Empty;
            btnCopy.Enabled = false;
        }

        private void txtCiphertext_TextChanged(object sender, EventArgs e)
        {
            btnCopy.Enabled = !String.IsNullOrEmpty(txtCiphertext.Text.Trim());
        }

        private void btnTrans_Click(object sender, EventArgs e)
        {
            string strPlainText = txtPlaintext.Text.TrimEnd();
            string strCryptText = Encode(strPlainText);
            txtCiphertext.Text = strCryptText;
        }
        private void btnCopy_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(txtCiphertext.Text.Trim()))
            {
                Clipboard.SetText(txtCiphertext.Text.TrimEnd());
            }
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            Clear();
        }

        #region 加密部分

        const string KEY_64 = "wz,m;d@t";
        const string IV_64 = "wz,m;d@t";

        /// <summary>
        /// 加密字符串
        /// </summary>
        /// <param name="data">数据</param>        
        public static string Encode(string data)
        {
            string strResult = string.Empty;

            byte[] byKey = System.Text.ASCIIEncoding.ASCII.GetBytes(KEY_64);
            byte[] byIV = System.Text.ASCIIEncoding.ASCII.GetBytes(IV_64);

            DESCryptoServiceProvider cryptoProvider = new DESCryptoServiceProvider();
            int i = cryptoProvider.KeySize;
            MemoryStream ms = new MemoryStream();
            StreamWriter sw = null;
            CryptoStream cst = null;
            try
            {
                cst = new CryptoStream(ms, cryptoProvider.CreateEncryptor(byKey, byIV), CryptoStreamMode.Write);
                sw = new StreamWriter(cst);
                sw.Write(data);
                sw.Flush();
                cst.FlushFinalBlock();
                sw.Flush();
                strResult = Convert.ToBase64String(ms.GetBuffer(), 0, (int)ms.Length);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                ms.Close();
                ms.Dispose();
                sw.Close();
                sw.Dispose();
                cst.Close();
                cst.Dispose();
            }
            return strResult;
        }

        /// <summary>
        /// 解密数据
        /// </summary>
        /// <param name="data">密文</param>       
        public static string Decode(string data)
        {
            string strResult = string.Empty;

            byte[] byKey = System.Text.ASCIIEncoding.ASCII.GetBytes(KEY_64);
            byte[] byIV = System.Text.ASCIIEncoding.ASCII.GetBytes(IV_64);
            DESCryptoServiceProvider cryptoProvider = new DESCryptoServiceProvider();
            MemoryStream ms = null;
            CryptoStream cst = null;
            StreamReader sr = null;
            byte[] byEnc;
            try
            {
                byEnc = Convert.FromBase64String(data);
                ms = new MemoryStream(byEnc);
                cst = new CryptoStream(ms, cryptoProvider.CreateDecryptor(byKey, byIV), CryptoStreamMode.Read);
                sr = new StreamReader(cst);
                strResult = sr.ReadToEnd();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                ms.Close();
                ms.Dispose();
                cst.Close();
                cst.Dispose();
                sr.Close();
                sr.Dispose();
            }
            return strResult;
        }
        #endregion

        private void btnDecodeClear_Click(object sender, EventArgs e)
        {
            txtDecodePlainText.Text = String.Empty;
            txtDecodeSecurtyText.Text = String.Empty;
            btnDecodeCopy.Enabled = false;
        }

        private void btnDecodeTrans_Click(object sender, EventArgs e)
        {
            string strPlainText = txtDecodePlainText.Text.TrimEnd();
            string strCryptText = Decode(strPlainText);
            txtDecodeSecurtyText.Text = strCryptText;
        }

        private void btnDecodeCopy_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(txtDecodeSecurtyText.Text.Trim()))
            {
                Clipboard.SetText(txtDecodeSecurtyText.Text.TrimEnd());
            }
        }

    }
}
