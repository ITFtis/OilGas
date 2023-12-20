using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OilGas
{
    public class WebUI
    {
        /// <summary>
        /// 取得DDL 自用加儲油-使用狀況Master
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<KeyValuePair<string, string>> GetDDLUsageStateMaster()
        {
            IEnumerable<KeyValuePair<string, string>> result = new List<KeyValuePair<string, string>>();
            
            result = result.Append(new KeyValuePair<string, string>("申請中", "1"));
            result = result.Append(new KeyValuePair<string, string>("申請中－失效", "2"));
            result = result.Append(new KeyValuePair<string, string>("使用中", "3"));
            result = result.Append(new KeyValuePair<string, string>("使用中－失效", "4"));

            return result;
        }

        /// <summary>
        /// 取得DDL 自用加儲油-使用狀況Detail(1.申請中,2.申請中－失效,3.使用中,4.使用中－失效)
        /// </summary>
        /// <param name="kind"></param>
        /// <returns></returns>
        public static IEnumerable<KeyValuePair<string, string>> GetDDLUsageStateDetail(string kind = "")
        {
            IEnumerable<KeyValuePair<string, string>> result = new List<KeyValuePair<string, string>>();

            List<UsageStateM> infos = new List<UsageStateM>();

            if (kind == "")
            {
                infos.Add(new UsageStateM("申請設置-申請中", "0", "0"));
                infos.Add(new UsageStateM("申請設置-申請中-同意備查", "0", "0", "7"));
                infos.Add(new UsageStateM("申請設置-申請中-同意陳報", "0", "0", "8"));
                infos.Add(new UsageStateM("申請設置-申請中-失效", "0", "0", "9"));
                infos.Add(new UsageStateM("申請設置-申請中-申請中變更", "0", "0", "10"));
                infos.Add(new UsageStateM("申請設置-申請中-同意設置", "0", "0", "0"));
                infos.Add(new UsageStateM("申請設置-申請中-同意設置-同意使用展延", "0", "0", "0", "4"));
                infos.Add(new UsageStateM("申請設置-申請中-同意設置-同意使用(進入使用管理-使用中)", "0", "0", "0", "0"));
                infos.Add(new UsageStateM("申請設置-申請中-同意設置-核駁", "0", "0", "0", "1"));
                infos.Add(new UsageStateM("申請設置-申請中-同意設置-撤案", "0", "0", "0", "2"));
                infos.Add(new UsageStateM("申請設置-申請中-同意設置-廢止", "0", "0", "0", "3"));
                infos.Add(new UsageStateM("申請設置-申請中-核駁", "0", "0", "1"));
                infos.Add(new UsageStateM("申請設置-申請中-撤案", "0", "0", "2"));
                infos.Add(new UsageStateM("使用管理-使用中", "1", "1"));
                infos.Add(new UsageStateM("使用管理-使用中-使用中變更", "1", "1", "6"));
                infos.Add(new UsageStateM("使用管理-使用中-撤案(進入已失效)", "1", "1", "3"));
                infos.Add(new UsageStateM("使用管理-使用中-廢止(進入已失效)", "1", "1", "4"));
                infos.Add(new UsageStateM("使用管理-使用中-失效(進入已失效)", "1", "1", "5"));
                infos.Add(new UsageStateM("使用管理-已失效", "1", "2"));
            }
            else if (kind == "1")
            {
                //申請中                
                infos.Add(new UsageStateM("申請設置-申請中", "0", "0"));
                infos.Add(new UsageStateM("申請設置-申請中-同意備查", "0", "0", "7"));
                infos.Add(new UsageStateM("申請設置-申請中-同意陳報", "0", "0", "8"));
                infos.Add(new UsageStateM("申請設置-申請中-申請中變更", "0", "0", "10"));
                infos.Add(new UsageStateM("申請設置-申請中-同意設置", "0", "0", "0"));
                infos.Add(new UsageStateM("申請設置-申請中-同意設置-同意使用展延", "0", "0", "0", "4"));
            }
            else if (kind == "2")
            {
                //申請中－失效                                
                infos.Add(new UsageStateM("申請設置-申請中-同意設置-核駁", "0", "0", "0", "1"));
                infos.Add(new UsageStateM("申請設置-申請中-同意設置-撤案", "0", "0", "0", "2"));
                infos.Add(new UsageStateM("申請設置-申請中-同意設置-廢止", "0", "0", "0", "3"));
                infos.Add(new UsageStateM("申請設置-申請中-核駁", "0", "0", "1"));
                infos.Add(new UsageStateM("申請設置-申請中-撤案", "0", "0", "2"));
                infos.Add(new UsageStateM("申請設置-申請中-失效", "0", "0", "9"));
            }
            else if (kind == "3")
            {
                //使用中                                
                infos.Add(new UsageStateM("使用管理-使用中", "1", "1"));
                infos.Add(new UsageStateM("使用管理-使用中-使用中變更", "1", "1", "6"));
            }
            else if (kind == "4")
            {
                //使用中－失效                
                infos.Add(new UsageStateM("使用管理-使用中-撤案(進入已失效)", "1", "1", "3"));
                infos.Add(new UsageStateM("使用管理-使用中-廢止(進入已失效)", "1", "1", "4"));
                infos.Add(new UsageStateM("使用管理-使用中-失效(進入已失效)", "1", "1", "5"));
                infos.Add(new UsageStateM("使用管理-已失效", "1", "2"));
            }

            foreach (var v in infos)
            {
                string text = v.Text;
                string value = string.Format("{0},{1},{2},{3}", v.UsageState, v.UsageState_Second, v.UsageState_Third, v.UsageState_Fourth);
                result = result.Append(new KeyValuePair<string, string>(text, value));
            }

            return result;
        }

        /// <summary>
        /// 取得DDL 自用加儲油-使用狀況Detail(1.申請中,2.申請中－失效,3.使用中,4.使用中－失效)
        /// </summary>
        /// <param name="kind"></param>
        /// <returns></returns>
        public static IEnumerable<KeyValuePair<string, object>> GetUsageStateDetail(string kind = "")
        {
            IEnumerable<KeyValuePair<string, object>> result = new List<KeyValuePair<string, object>>();

            List<UsageStateM> infos = new List<UsageStateM>();

            if (kind == "")
            {
                infos.Add(new UsageStateM("申請設置-申請中", "0", "0"));
                infos.Add(new UsageStateM("申請設置-申請中-同意備查", "0", "0", "7"));
                infos.Add(new UsageStateM("申請設置-申請中-同意陳報", "0", "0", "8"));
                infos.Add(new UsageStateM("申請設置-申請中-失效", "0", "0", "9"));
                infos.Add(new UsageStateM("申請設置-申請中-申請中變更", "0", "0", "10"));
                infos.Add(new UsageStateM("申請設置-申請中-同意設置", "0", "0", "0"));
                infos.Add(new UsageStateM("申請設置-申請中-同意設置-同意使用展延", "0", "0", "0", "4"));
                infos.Add(new UsageStateM("申請設置-申請中-同意設置-同意使用(進入使用管理-使用中)", "0", "0", "0", "0"));
                infos.Add(new UsageStateM("申請設置-申請中-同意設置-核駁", "0", "0", "0", "1"));
                infos.Add(new UsageStateM("申請設置-申請中-同意設置-撤案", "0", "0", "0", "2"));
                infos.Add(new UsageStateM("申請設置-申請中-同意設置-廢止", "0", "0", "0", "3"));
                infos.Add(new UsageStateM("申請設置-申請中-核駁", "0", "0", "1"));
                infos.Add(new UsageStateM("申請設置-申請中-撤案", "0", "0", "2"));
                infos.Add(new UsageStateM("使用管理-使用中", "1", "1"));
                infos.Add(new UsageStateM("使用管理-使用中-使用中變更", "1", "1", "6"));
                infos.Add(new UsageStateM("使用管理-使用中-撤案(進入已失效)", "1", "1", "3"));
                infos.Add(new UsageStateM("使用管理-使用中-廢止(進入已失效)", "1", "1", "4"));
                infos.Add(new UsageStateM("使用管理-使用中-失效(進入已失效)", "1", "1", "5"));
                infos.Add(new UsageStateM("使用管理-已失效", "1", "2"));
            }
            else if (kind == "1")
            {
                //申請中                
                infos.Add(new UsageStateM("申請設置-申請中", "0", "0"));
                infos.Add(new UsageStateM("申請設置-申請中-同意備查", "0", "0", "7"));
                infos.Add(new UsageStateM("申請設置-申請中-同意陳報", "0", "0", "8"));
                infos.Add(new UsageStateM("申請設置-申請中-申請中變更", "0", "0", "10"));
                infos.Add(new UsageStateM("申請設置-申請中-同意設置", "0", "0", "0"));
                infos.Add(new UsageStateM("申請設置-申請中-同意設置-同意使用展延", "0", "0", "0", "4"));
            }
            else if (kind == "2")
            {
                //申請中－失效                                
                infos.Add(new UsageStateM("申請設置-申請中-同意設置-核駁", "0", "0", "0", "1"));
                infos.Add(new UsageStateM("申請設置-申請中-同意設置-撤案", "0", "0", "0", "2"));
                infos.Add(new UsageStateM("申請設置-申請中-同意設置-廢止", "0", "0", "0", "3"));
                infos.Add(new UsageStateM("申請設置-申請中-核駁", "0", "0", "1"));
                infos.Add(new UsageStateM("申請設置-申請中-撤案", "0", "0", "2"));
                infos.Add(new UsageStateM("申請設置-申請中-失效", "0", "0", "9"));
            }
            else if (kind == "3")
            {
                //使用中                                
                infos.Add(new UsageStateM("使用管理-使用中", "1", "1"));
                infos.Add(new UsageStateM("使用管理-使用中-使用中變更", "1", "1", "6"));
            }
            else if (kind == "4")
            {
                //使用中－失效                
                infos.Add(new UsageStateM("使用管理-使用中-撤案(進入已失效)", "1", "1", "3"));
                infos.Add(new UsageStateM("使用管理-使用中-廢止(進入已失效)", "1", "1", "4"));
                infos.Add(new UsageStateM("使用管理-使用中-失效(進入已失效)", "1", "1", "5"));
                infos.Add(new UsageStateM("使用管理-已失效", "1", "2"));
            }

            foreach (var v in infos)
            {
                string text = v.Text;
                string value = string.Format("{0},{1},{2},{3}", v.UsageState, v.UsageState_Second, v.UsageState_Third, v.UsageState_Fourth);
                result = result.Append(new KeyValuePair<string, object>(text, value));
            }

            return result;
        }
    }
}