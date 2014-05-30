using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using MOP.API.BLL;
using MOP.API.Factory;
using MOP.API.Model;

namespace MDT.WebUI.AppCode
{
    /// <summary>
    /// 订单对比
    /// </summary>
    public class OrderContrast
    {
        private string _platformId;
        private string _shopId;
        private bool _isFenxiao = false;

        public OrderContrast(string platformId,string shopId)
        {
            _platformId = platformId;
            if (_platformId == "fenxiao")
            {
                _isFenxiao = true;
            }
            _shopId = shopId;
        }

        public OrderContrast()
        {
            // TODO: Complete member initialization
        }

        /// <summary>
        /// 返回订单对比列表
        /// </summary>
        /// <param name="startTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <param name="dateType">查询的时间类型</param>
        /// <param name="status">订单状态(平台不同)</param>
        /// <returns></returns>
        internal List<global::MOP.API.Model.OrderIdInfo> GetTradeCompList(DateTime startTime, DateTime endTime, global::MOP.API.Model.ParamDateType dateType)
        {
            OrderHandler orderHandler = OrderFactory.CreateInstance(_shopId, _isFenxiao);
            List<global::MOP.API.Model.OrderIdInfo> orderList = orderHandler.GetOrderIdList(startTime, endTime).Where(c => c.Status != "WAIT_BUYER_PAY").ToList();
            List<global::MOP.API.Model.OrderIdInfo> scmOrderList = orderHandler.GetOrderSCMIdList(startTime, endTime, dateType, _isFenxiao);
            //OrderIdInfo[] orderList;
            //OrderIdInfo[] scmOrderList;
            //using (MOPServiceClient mopServ = new MOPServiceClient())
            //{
            //    orderList = mopServ.GetDefaultOrderIdList(_shopId, startTime, endTime, _isFenxiao);
            //    scmOrderList = mopServ.GetSCMOrderIdList(_shopId, startTime, endTime, _isFenxiao, dateType);
            //}
            if (orderList == null || scmOrderList == null)
            {
                throw new Exception("没有查询到订单信息!");
            }
            List<global::MOP.API.Model.OrderIdInfo> list = new List<global::MOP.API.Model.OrderIdInfo>();
            foreach (global::MOP.API.Model.OrderIdInfo info in orderList)
            {
                global::MOP.API.Model.OrderIdInfo orderIds = scmOrderList.FirstOrDefault(c => c.OrderId == info.OrderId);
                if (orderIds == null)
                {
                    list.Insert(0, info);
                }
                else
                {
                    if (info.LastUpdateTime == DateTime.MinValue)
                    {
                        info.LastUpdateTime = orderIds.LastUpdateTime;
                    }
                    if (info.CreateTime == DateTime.MinValue)
                    {
                        info.CreateTime = orderIds.CreateTime;
                    }
                    info.OuterSorderId = orderIds.OrderId;
                    info.ScmOrderStatus = orderIds.Status;
                    list.Add(info);
                }
            }
            return list;
        }

        internal void SaveOrderToScm(OrderIdInfo[] orderIdInfo)
        {
            new OrderExecutor().SaveOrderIdList(orderIdInfo.ToList());
        }
    }
}