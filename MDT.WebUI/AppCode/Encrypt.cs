using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.IO;
using System.Security.Cryptography;


namespace MDT.WebUI
{
    /// <summary>
    /// Encrypt 的摘要说明
    /// </summary>
    public class Encrypt
    {
        private const string RIJI_KEY = "Bn5ABrQY2CFaQudHXM84PeU9BEV6yAvwO/ttR372jTE=";
        private const string RIJI_IV = "RlZNUnzt2l6hBsSoxyDb4g==";

        /// <summary>
        /// Encrypt String
        /// </summary>
        /// <param name="toEncryptString"></param>
        /// <returns></returns>
        public static string EncryptString(
            string toEncryptString)
        {
            string encryptedString = string.Empty;

            System.Text.UTF8Encoding textConverter = new System.Text.UTF8Encoding();
            byte[] toEncrypt = textConverter.GetBytes(toEncryptString);
            byte[] encrypted;

            RijndaelManaged myRijndael = new RijndaelManaged();

            ICryptoTransform encryptor = myRijndael.CreateEncryptor(
                Convert.FromBase64String(RIJI_KEY),
                Convert.FromBase64String(RIJI_IV));

            MemoryStream msEncrypt = new MemoryStream();
            CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write);

            //Write all data to the crypto stream and flush it.
            csEncrypt.Write(toEncrypt, 0, toEncrypt.Length);
            csEncrypt.FlushFinalBlock();

            //Get encrypted array of bytes.
            encrypted = msEncrypt.ToArray();

            encryptedString = Convert.ToBase64String(encrypted);

            return encryptedString;
        }

    }
}