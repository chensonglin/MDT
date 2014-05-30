using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MOP.API.Model;
using MOP.API.BLL;

namespace MDT.WebUI.AppCode
{
    public class GetShopInfo
    {
        /// <summary>
        /// 获取商家平台信息
        /// </summary>
        /// <param name="platformId">平台ID</param>
        /// <returns></returns>
        public List<BasePlatform> GetBasePlatform(string platformId)
        {
            GetBasePlatformBLL bll = new GetBasePlatformBLL();
            return bll.GetBasePlatform(platformId);
        }

        /// <summary>
        /// 获取商家平台店铺的信息
        /// </summary>
        /// <param name="platformId">平台ID</param>
        /// <param name="tradeFlag">是否抓取订单</param>
        /// <param name="isTBFX">是否是分销</param>
        /// <returns></returns>
        public List<Shop> GetBaseShopPlatform(string platformId, bool tradeFlag, bool isTBFX)
        {
            ShopBLL bll = new ShopBLL();
            return bll.GetShops(platformId, tradeFlag, isTBFX);
        }
    }
}