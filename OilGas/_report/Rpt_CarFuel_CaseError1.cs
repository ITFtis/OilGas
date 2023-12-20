using OilGas.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OilGas
{
    /// <summary>
    /// 加油站/A統計報表專區/地下儲油槽列管狀況報表
    /// </summary>
    public class Rpt_CarFuel_CaseError1
    {
        internal const int shortcacheduration = 5 * 60 * 1000;
        static object lockGetAllvwCFCE1 = new object();

        public static IEnumerable<vw_CarFuel_CaseError1> GetAllvwCFCE1(int cachetimer = shortcacheduration)
        {
            string key = "OilGas.GetAllvwCFCE1";
            var alldatas = DouHelper.Misc.GetCache<IEnumerable<vw_CarFuel_CaseError1>>(cachetimer, key);
            lock (lockGetAllvwCFCE1)
            {
                if (alldatas == null)
                {
                    using (var cxt = new OilGasModelContextExt())
                    {
                        alldatas = cxt.vw_CarFuel_CaseError1.ToArray();
                        DouHelper.Misc.AddCache(alldatas, key);
                    }
                }
            }
            return alldatas;
        }

        public static void ResetGetAllvwCFCE1()
        {
            string key = "OilGas.GetAllvwCFCE1";
            DouHelper.Misc.ClearCache(key);
        }

    }
}