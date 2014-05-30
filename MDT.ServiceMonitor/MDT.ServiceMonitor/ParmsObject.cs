using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MDT.ServiceMonitor
{

    /// <summary>
    /// 参数设置 
    /// </summary>
    public class ParmsObject
    {
        /// <summary>
        /// 服务名称
        /// </summary>
        public string WinServiceName { get; set; }

        /// <summary>
        ///发送的信息 
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// 预警时间
        /// </summary>
        public int MaxStopSeconds { get; set; }

        /// <summary>
        /// 轮询时间
        /// </summary>
        public double ScanSeconds { get; set; }

        /// <summary>
        /// 通知方式 （短信 邮件等）
        /// </summary>
        public string NoticeType { get; set; }

        /// <summary>
        /// 邮件地址
        /// </summary>
        public string ToAddress { get; set; }

        /// <summary>
        /// 邮件主题
        /// </summary>
        public string EmailTitle { get; set; }

        /// <summary>
        /// 短信地址
        /// </summary>
        public string ReceivePhoneNumber { get; set; }

        /// <summary>
        /// 邮件接口地址
        /// </summary>
        public string EmailServiceUrl { get; set; }

        /// <summary>
        /// 短信接口地址
        /// </summary>
        public string SMSSeriveUrl { get; set; }

    }
}
