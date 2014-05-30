using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
namespace MDT.Utility
{
    public class TextWriter
    {
        /// <summary>
        /// 日志路径
        /// </summary>
        public static string LogFilePath
        {
            get
            {
                return string.Format("{0}\\MdtLogs\\Log{1}\\{2}.txt",
                                      AppDomain.CurrentDomain.BaseDirectory,
                                      DateTime.Now.ToString("yyyyMMdd"),
                                      DateTime.Now.ToString("HHmm"));
            }
        }

        /// <summary>
        /// 以文本的方式记录日志
        /// </summary>
        /// <param name="MessageInfo">记录日志信息</param>
        public static void WriteExceptionLog(Exception MessageInfo, string traceData = "", bool FormatText = false)
        {
            string strMessge = String.Empty;
            if (FormatText)
                strMessge = String.Format("\n\rException:{0}\n\r{1}", MessageInfo.ToString(), traceData);
            else
                strMessge = FormatLogMessage.XmlErroMsgFormat(MessageInfo, traceData);

            WriteLog(strMessge);
        }

        /// <summary>
        /// 私有方法  写日志
        /// </summary>
        /// <param name="message"></param>
        private static void WriteLog(string message)
        {
            string[] arryPath = new string[5];
            int intNowMinute = DateTime.Now.Minute;
            int dirPosition = LogFilePath.LastIndexOf("\\");
            string tempPath = LogFilePath.Substring(0, dirPosition);
            if (!Directory.Exists(tempPath))
            {
                Directory.CreateDirectory(tempPath);
            }              
            for (int i = 0; i < 5; i++)
            {
                arryPath[i] = String.Format("{0}\\{1}{2}.txt", tempPath, DateTime.Now.ToString("HH"), (intNowMinute - i ).ToString().PadLeft(2, '0'));
            }
            foreach (string filePath in arryPath)
            {
                if (File.Exists(filePath))
                    return;
            }
            if (!File.Exists(LogFilePath))
            {
                File.Create(LogFilePath).Dispose();
            }
            try
            {
                using (StreamWriter sw = new StreamWriter(LogFilePath, true))
                {
                    sw.Write(message);
                    sw.Flush();
                    sw.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
