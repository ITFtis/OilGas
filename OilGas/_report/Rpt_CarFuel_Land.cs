using OilGas.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OilGas
{
    /// <summary>
    /// 加油站/A統計報表專區/汽機車加油站已開業統計報表
    /// </summary>
    public class Rpt_CarFuel_Land
    {
        internal const int shortcacheduration = 5 * 60 * 1000;
        static object lockGetAllLandUsageZoneCode = new object();
        static object lockGetAllLandClassCode = new object();
        static object lockGetAllCityCode = new object();
        static object lockGetGSLCodeByCityCode = new object();
        static object lockGetAllAreaCode = new object();

        public static IEnumerable<LandUsageZoneCode> GetAllLandUsageZoneCode(int cachetimer = shortcacheduration)
        {
            string key = "OilGas.GetAllLandUsageZoneCode";
            var alldatas = DouHelper.Misc.GetCache<IEnumerable<LandUsageZoneCode>>(cachetimer, key);
            lock (lockGetAllLandUsageZoneCode)
            {
                if (alldatas == null)
                {
                    using (var cxt = new OilGasModelContextExt())
                    {
                        alldatas = cxt.LandUsageZoneCode.OrderBy(x => x.Rank).ToArray();
                        DouHelper.Misc.AddCache(alldatas, key);
                    }
                }
            }
            return alldatas;
        }

        public static void ResetGetAllLandUsageZoneCode()
        {
            string key = "OilGas.GetAllLandUsageZoneCode";
            DouHelper.Misc.ClearCache(key);
        }

        public static IEnumerable<LandClassCode> GetAllLandClassCode(int cachetimer = shortcacheduration)
        {
            string key = "OilGas.GetAllLandClassCode";
            var alldatas = DouHelper.Misc.GetCache<IEnumerable<LandClassCode>>(cachetimer, key);
            lock (lockGetAllLandClassCode)
            {
                if (alldatas == null)
                {
                    using (var cxt = new OilGasModelContextExt())
                    {
                        alldatas = cxt.LandClassCode.Where(x=> x.LandType == 1).OrderBy(x => x.Rank).ToArray();
                        DouHelper.Misc.AddCache(alldatas, key);
                    }
                }
            }
            return alldatas;
        }

        public static void ResetGetAllLandClassCode()
        {
            string key = "OilGas.GetAllLandClassCode";
            DouHelper.Misc.ClearCache(key);
        }

        public static IEnumerable<CityCode> GetAllCityCode(int cachetimer = shortcacheduration)
        {
            string key = "OilGas.CityCode";
            var alldatas = DouHelper.Misc.GetCache<IEnumerable<CityCode>>(cachetimer, key);
            lock (lockGetAllCityCode)
            {
                if (alldatas == null)
                {
                    using (var cxt = new OilGasModelContextExt())
                    {
                        alldatas = cxt.CityCode.OrderBy(x=>x.Rank).ToArray();
                        DouHelper.Misc.AddCache(alldatas, key);
                    }
                }
            }
            return alldatas;
        }

        public static void ResetGetAllCityCode()
        {
            string key = "OilGas.CityCode";
            DouHelper.Misc.ClearCache(key);
        }

        public static IEnumerable<CityCode> GetGSLCodeByCityCode(string citycode, int cachetimer = shortcacheduration)
        {
            string key = "OilGas.GSLCodeByCityCode";
            var alldatas = DouHelper.Misc.GetCache<IEnumerable<CityCode>>(cachetimer, key);
            lock (lockGetGSLCodeByCityCode)
            {
                if (alldatas == null)
                {
                    using (var cxt = new OilGasModelContextExt())
                    {
                        alldatas = cxt.CityCode.Where(x=>x.CityCode1==citycode).ToArray();
                        DouHelper.Misc.AddCache(alldatas, key);
                    }
                }
            }
            return alldatas;
        }

        public static void ResetGetGSLCodeByCityCode()
        {
            string key = "OilGas.GSLCodeByCityCode";
            DouHelper.Misc.ClearCache(key);
        }

        public static IEnumerable<AreaCode> GetAllAreaCode(int cachetimer = shortcacheduration)
        {
            string key = "OilGas.AreaCode";
            var alldatas = DouHelper.Misc.GetCache<IEnumerable<AreaCode>>(cachetimer, key);
            lock (lockGetAllAreaCode)
            {
                if (alldatas == null)
                {
                    using (var cxt = new OilGasModelContextExt())
                    {
                        alldatas = cxt.AreaCode.OrderBy(x => x.Rank).ToArray();
                        DouHelper.Misc.AddCache(alldatas, key);
                    }
                }
            }
            return alldatas;
        }

        public static void ResetGetAllAreaCode()
        {
            string key = "OilGas.AreaCode";
            DouHelper.Misc.ClearCache(key);
        }

        public class CarFuel_1
        {
            public string 案件編號 { get; set; }
            public string 加油站名稱 { get; set; }
            public string 縣市別 { get; set; }
            public string 鄉鎮市區 { get; set; }
            public string 地址 { get; set; }
            public DateTime? 營業日期 { get; set; }
            public DateTime? 收件日期 { get; set; }
            public string 負責人姓名 { get; set; }
            public string 汽車加油站電話 { get; set; }
            public string 經營許可證號 { get; set; }
            public string LicenseNo1 { get; set; }
            public string LicenseNo2 { get; set; }
            public string LicenseNo3 { get; set; }
            public string 汽車加油站地址 { get; set; }
            public string 汽車加油站地號 { get; set; }
            public DateTime? 核發證號日期 { get; set; }
            public string 油品供應商 { get; set; }
            public string SoilServerData { get; set; }
            public string 營業主體 { get; set; }
            public string Business_theme { get; set; }
            public string otherBusiness_theme { get; set; }
            public string 營業別 { get; set; }
            public string 營運狀態 { get; set; }
            public DateTime? 歇業日期 { get; set; }
            public string DispatchClass { get; set; }
            public string 附屬設施_項目 { get; set; }
            public string 保險公司名稱 { get; set; }
            public string Insurance_otherCompany { get; set; }
            public string 保險號碼 { get; set; }
            public string 保單有效期限 { get; set; }
            public DateTime? 保單有效期限_起 { get; set; }
            public DateTime? 保單有效期限_迄 { get; set; }
            public string 保單類型 { get; set; }
            public string 土地權屬 { get; set; }
            public double? 用地總面積 { get; set; }
            public string 用地類別 { get; set; }
            public string 土地使用分區 { get; set; }
            public string LandClass { get; set; }
            public string OtherLandClass { get; set; }
            public string LandUsageZone { get; set; }
            public string OtherLandUsageZone { get; set; }
            public string 兼營設施_項目 { get; set; }
            public int? 加油泵島數 { get; set; }
            public int? 單槍 { get; set; }
            public int? 雙槍 { get; set; }
            public int? 四槍 { get; set; }
            public int? 六槍 { get; set; }
            public int? 八槍 { get; set; }
            public string 自助加油機數Str { get; set; }
            public int? 自助加油機數 { get; set; }
            public int? self_單槍 { get; set; }
            public int? self_雙槍 { get; set; }
            public int? self_四槍 { get; set; }
            public int? self_六槍 { get; set; }
            public int? self_八槍 { get; set; }
            public int? 油槽總數 { get; set; }
            public int? 儲槽容量_公秉 { get; set; }
            public int? 儲槽數量_座 { get; set; }
            public string 販售油品種類 { get; set; }
            public DateTime? Mod_date { get; set; }
            public DateTime? Dispatch_date { get; set; }

            public string 公告污染場址類型 { get; set; }
            public DateTime? 公告污染場址類型公告日期 { get; set; }
            public DateTime? 公告污染場址類型解列日期 { get; set; }

            public DateTime? Limit_Date { get; set; }
            public DateTime? take_Date { get; set; }
            public DateTime? GW_Date { get; set; }
            public DateTime? Control_Date { get; set; }
            public DateTime? Rem_Date { get; set; }
            public DateTime? Situation_Date { get; set; }
            public string UsageState { get; set; }
        }
    }
}