using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using System.IO;
namespace MDT.Utility
{
    public class SecurityHelper
    {
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
                if (ms != null)
                {
                    ms.Close();
                    ms.Dispose();
                }
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
            catch (Exception)
            {
                throw new Exception("SourceLink字符串配置出错，请核对！"); ;
            }
            finally
            {
                if (ms != null && ms.Length > 0)
                {
                    ms.Close();
                    ms.Dispose();
                }
            }
            return strResult;
        }


        /// <summary>
        /// MD5加密字符串
        /// </summary>
        public static string CryptStringMD5(string str)
        {
            string pwd = "";
            //实例化一个md5对像
            System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create();
            // 加密后是一个字节类型的数组，这里要注意编码UTF8/Unicode等的选择　
            byte[] s = md5.ComputeHash(Encoding.UTF8.GetBytes(str));
            // 通过使用循环，将字节类型的数组转换为字符串，此字符串是常规字符格式化所得
            for (int i = 0; i < s.Length; i++)
            {
                // 将得到的字符串使用十六进制类型格式。格式后的字符是小写的字母，如果使用大写（X）则格式后的字符是大写字符
                pwd = pwd + s[i].ToString("X");
            }
            return pwd;
        }

    }
}
