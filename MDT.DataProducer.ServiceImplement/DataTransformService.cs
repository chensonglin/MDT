using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.XPath;
using System.Xml.Xsl;
using System.IO;
using System.ServiceModel;
using Microsoft.Practices.EnterpriseLibrary.Caching;
using Microsoft.Practices.EnterpriseLibrary.Caching.Expirations;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using MDT.Utility;
using MDT.DataProducer.ServiceContract;
using MDT.DataProducer.ServiceImplement.DataConsumer;
using MDT.DataProducer.ServiceImplement.DataProducerCenter;

namespace MDT.DataProducer.ServiceImplement
{
    public class DataTransformService : IDataTransformService 
    {
        //IXSLTManageService _xslt;
        private XslCompiledTransform xslTransform;  //TODO:缓存转换器

        public DataTransformService()
        {
            xslTransform = new XslCompiledTransform();
        }

        /// <summary>
        /// 数据生产服务
        /// </summary>
        public IDataProducerCenterService Center { get; set; }

        /// <summary>
        /// 数据消费服务
        /// </summary>
        private IDataConsumerService _consumer;
        private IDataConsumerService consumer
        {
            get
            {
                ICommunicationObject obj = _consumer as ICommunicationObject;
                if (obj == null || obj.State == CommunicationState.Faulted || obj.State == CommunicationState.Closed)
                    _consumer = WCFServiceFactory.GetInstance<DataConsumerServiceClient>();
                return _consumer;
            }
        }

        /// <summary>
        /// 缓存管理服务
        /// </summary>
        private static ICacheManager _cache;
        private static ICacheManager cache
        {
            get
            {
                if (_cache == null)
                    _cache = EnterpriseLibraryContainer.Current.GetInstance<ICacheManager>();
                return _cache;
            }
        }

        /// <summary>
        /// 获取XSLT
        /// </summary>
        /// <param name="taskId"></param>
        /// <returns></returns>
        public string GetXSLT(int taskId)
        {
            if (!cache.Contains(taskId.ToString()))
            {
                var str = Center.GetXSLT(taskId);
                cache.Add(taskId.ToString(), str, CacheItemPriority.Normal, null, new AbsoluteTime(TimeSpan.FromMinutes(5)));
                return str;
            }
            else
            {
                var data = cache.GetData(taskId.ToString());
                if (data == null)
                {
                    data = GetXSLT(taskId);
                }
                return data.ToString();
            }
        }

        /// <summary>
        /// 数据转换
        /// </summary>
        /// <param name="taskId"></param>
        /// <param name="processLN"></param>
        /// <param name="data"></param>
        public void Send(int taskId, string processLN, string data)
        {
            try
            {
                string xslt = GetXSLT(taskId);

                StringReader dataReader = new StringReader(data);
                XmlReader xmlReader = XmlReader.Create(dataReader);

                StringBuilder sb = new StringBuilder();
                XmlWriter xmlWriter = XmlWriter.Create(sb);

                StringReader xsltReader = new StringReader(xslt);
                xslTransform.Load(XmlReader.Create(xsltReader));
                xslTransform.Transform(xmlReader, xmlWriter);

                xmlWriter.Flush();
                xmlWriter.Close();
                xmlReader.Close();
                dataReader.Dispose();
                xsltReader.Dispose();

                // 发送消息
                consumer.Send(taskId, processLN, sb.ToString());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 重发数据信息
        /// </summary>
        /// <param name="traceId"></param>
        public void ReSend(int traceId)
        {
            //var t = trace.Read(traceId);
            //Send(t.TaskId, t.ProcessLN, t.Data);
        }
    }
}
