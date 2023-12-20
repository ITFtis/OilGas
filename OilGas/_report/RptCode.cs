using OilGas.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace OilGas
{
    public class RptCode
    {
        /// <summary>
        /// 取得使用狀況條件集合
        /// </summary>
        /// <param name="BigUsage">使用狀況Master</param>
        /// <returns></returns>
        public static List<UsageStateM> GetAnyUsageStateM(string BigUsage = "")
        {
            List<UsageStateM> result = new List<UsageStateM>();

            switch (BigUsage)
            {
                case "1":
                    result.Add(new UsageStateM("BigUsage1", "0", "0", "", ""));
                    result.Add(new UsageStateM("BigUsage1", "0", "0", "", "4"));
                    result.Add(new UsageStateM("BigUsage1", "0", "0", "0", ""));
                    result.Add(new UsageStateM("BigUsage1", "0", "0", "0", "4"));
                    result.Add(new UsageStateM("BigUsage1", "0", "0", "7", ""));
                    result.Add(new UsageStateM("BigUsage1", "0", "0", "7", "4"));
                    result.Add(new UsageStateM("BigUsage1", "0", "0", "8", ""));
                    result.Add(new UsageStateM("BigUsage1", "0", "0", "8", "4"));
                    result.Add(new UsageStateM("BigUsage1", "0", "0", "10", ""));
                    result.Add(new UsageStateM("BigUsage1", "0", "0", "10", "4"));
                    break;

                case "2":
                    result.Add(new UsageStateM("BigUsage2", "0", "0", "0", "1"));
                    result.Add(new UsageStateM("BigUsage2", "0", "0", "0", "2"));
                    result.Add(new UsageStateM("BigUsage2", "0", "0", "0", "3"));
                    result.Add(new UsageStateM("BigUsage2", "0", "0", "1", ""));
                    result.Add(new UsageStateM("BigUsage2", "0", "0", "2", ""));
                    result.Add(new UsageStateM("BigUsage2", "0", "0", "9", ""));
                    break;

                case "3":
                    result.Add(new UsageStateM("BigUsage3", "0", "0", "0", "0"));
                    result.Add(new UsageStateM("BigUsage3", "1", "1", "", ""));
                    result.Add(new UsageStateM("BigUsage3", "1", "1", "", "5"));
                    result.Add(new UsageStateM("BigUsage3", "1", "1", "", "6"));
                    result.Add(new UsageStateM("BigUsage3", "1", "1", "", "7"));
                    result.Add(new UsageStateM("BigUsage3", "1", "1", "", "8"));
                    result.Add(new UsageStateM("BigUsage3", "1", "1", "6", ""));
                    result.Add(new UsageStateM("BigUsage3", "1", "1", "6", "5"));
                    result.Add(new UsageStateM("BigUsage3", "1", "1", "6", "6"));
                    result.Add(new UsageStateM("BigUsage3", "1", "1", "6", "7"));
                    result.Add(new UsageStateM("BigUsage3", "1", "1", "6", "8"));
                    break;

                case "4":
                    result.Add(new UsageStateM("BigUsage4", "1", "1", "3", ""));
                    result.Add(new UsageStateM("BigUsage4", "1", "1", "4", ""));
                    result.Add(new UsageStateM("BigUsage4", "1", "1", "5", ""));
                    result.Add(new UsageStateM("BigUsage4", "1", "2", "", ""));
                    break;
            }

            return result;
        }

        /// <summary>
        /// 取得使用狀況條件集合
        /// </summary>
        /// <param name="condition">使用狀況Detail</param>
        /// <returns></returns>
        public static List<UsageStateM> GetAnyUsageStateM(UsageStateM condition)
        {
            List<UsageStateM> result = new List<UsageStateM>();
            
            if(condition.UsageState == "1" && condition.UsageState_Second == "1" && condition.UsageState_Third == "" && condition.UsageState_Fourth == "")
            {
                result.Add(new UsageStateM("Detail_1", "0", "0", "0", "0"));
                result.Add(new UsageStateM("Detail_1", "1", "1", "", ""));
            }
            else if (condition.UsageState == "1" && condition.UsageState_Second == "2" && condition.UsageState_Third == "" && condition.UsageState_Fourth == "")
            {
                result.Add(new UsageStateM("Detail_2", "1", "1", "3", ""));
                result.Add(new UsageStateM("Detail_2", "1", "1", "4", ""));
                result.Add(new UsageStateM("Detail_2", "1", "1", "5", ""));
                result.Add(new UsageStateM("Detail_2", "1", "2", "", ""));
            }
            else if (condition.UsageState == "1" && condition.UsageState_Second == "1" && condition.UsageState_Third == "6")
            {
                result.Add(new UsageStateM("Detail_3", "1", "1", "6", ""));
                result.Add(new UsageStateM("Detail_3", "1", "1", "6", "5"));
                result.Add(new UsageStateM("Detail_3", "1", "1", "6", "6"));
                result.Add(new UsageStateM("Detail_3", "1", "1", "6", "7"));
                result.Add(new UsageStateM("Detail_3", "1", "1", "6", "8"));
            }
            else
            {
                result.Add(new UsageStateM("Detail_4", condition.UsageState, condition.UsageState_Second, condition.UsageState_Third, condition.UsageState_Fourth));
            }

            return result;
        }
    }

    public class RptCodeUsageState
    {
        public string BigUsage { get; set; }  //"",1,2,3,4        
        public List<UsageStateM> items = new List<UsageStateM>();
    }

    public class UsageStateM
    {
        public UsageStateM() { }

        public UsageStateM(string text, string usageState = "", string usageState_Second = "", string usageState_Third = "", string usageState_Fourth = "")
        {
            Text = text;
            UsageState = usageState;
            UsageState_Second = usageState_Second;
            UsageState_Third = usageState_Third;
            UsageState_Fourth = usageState_Fourth;
        }

        //申請設置-申請中-同意設置-同意使用展延
        public string Text { get; set; }

        //UsageState:0;UsageState_Second:0;UsageState_Third:0;UsageState_Fourth:4
        public string UsageState { get; set; }
        public string UsageState_Second { get; set; }
        public string UsageState_Third { get; set; }
        public string UsageState_Fourth { get; set; }
    }
}