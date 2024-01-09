using Newtonsoft.Json;
using OilGas.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace OilGas
{
    public class Code
    {
        public enum TempUploadFile
        {
            none = 0,
            加油站_A統計報表專區_現況資料_基本資料欄位清單 = 1,  //瀏覽檔案送出(前)
            加油站_A統計報表專區_變更歷程_基本資料欄位清單 = 2,
            汽車加氣站_B統計報表專區_現況資料_基本資料欄位清單 = 3,
            汽車加氣站_B統計報表專區_變更歷程_基本資料欄位清單 = 4,
            漁船加油站_C統計報表專區_現況資料_基本資料欄位清單 = 5,
            漁船加油站_C統計報表專區_變更歷程_基本資料欄位清單 = 6,
            自用加儲油_D統計報表專區_基本資料清單查詢 = 7,
            航港自用加儲油_E統計報表專區_現況資料 = 8,
            航港自用加儲油_E統計報表專區_變更歷程 = 9,
            取締管理作業_F違規案件維護_石油管理法取締案件資料查詢 = 10,
            查核輔導專區_G統計報表專區_儲槽總數差異勾稽報表 = 11,
            汽車加氣站_B統計報表專區_加氣站成長分析報表 = 12,
            漁船加油站_C統計報表專區_加氣站成長分析報表 = 13,
            查核輔導專區_G統計報表專區_設備檢查缺失 = 14,
            查核輔導專區_G統計報表專區_設備檢查缺失_合計 = 15,
            加油站_A統計報表專區_已開業汽機車加油站分析統計報表 = 16,
            汽車加氣站_B統計報表專區_已開業汽車加氣站分析統計報表 = 17,
            漁船加油站_C統計報表專區_已開業漁船加油站分析統計報表 = 18,
            查核輔導專區_G統計報表專區_石油設施缺失項目彙整表 = 19,
            加油站_A統計報表專區_各縣市暫停營業家數統計報表 = 20,
            汽車加氣站_B統計報表專區_各縣市暫停營業家數統計報表 = 21,
            漁船加油站_C統計報表專區_各縣市暫停營業家數統計報表 = 22,
            查核輔導專區_G統計報表專區_石油設施缺失統計表 = 23,
            查核輔導專區_G統計報表專區_石油設施各項設備檢查缺失數 = 24,
            加油站_A統計報表專區_各縣市停歇業與新設加油站家數統計報表 = 25,
            汽車加氣站_B統計報表專區_各縣市停歇業與新設加氣站家數統計報表 = 26,
            漁船加油站_C統計報表專區_各縣市停歇業與新設加油站家數統計報表 = 27,
            查核輔導專區_G統計報表專區_石油設施各檢查項目缺失數 = 28,
            查核輔導專區_G統計報表專區_已查核家數統計表 = 29,
            加油站_A統計報表專區_各縣市新設_停業與終止營運自用加儲油站家數統計報表 = 30,
            汽車加氣站_B統計報表專區_各縣市新設_停業與終止營運自用加儲油站家數統計報表 = 31,
            漁船加油站_C統計報表專區_各縣市新設_停業與終止營運自用加儲油站家數統計報表 = 32,
            查核輔導專區_G統計報表專區_石油設施查核結果彙整 = 33,
            自用加儲油站_D統計報表專區_新設與歇業之自用加儲油站家數統計報表 = 34,
            課程報名 = 35,
            簽到單 = 36,
            加油站_A統計報表專區_各縣市已發照及營業中家數統計報表 = 37,
            查核輔導專區_G交叉分析報表_石油設施查核名單篩選 = 38,
            查核輔導專區_G交叉分析報表_查核缺失趨勢交叉分析報表 = 39,
            汽車加氣站_B統計報表專區_申請設置案件同意認定_籌建到期報表 = 40,
            漁船加油站_C統計報表專區_申請設置案件同意認定_籌建到期報表 = 41,
            查核輔導專區_G交叉分析報表_分級管理趨勢交叉分析報表 = 42,           //未做，先跳過
            查核輔導專區_G交叉分析報表_分級管理及重要因子交叉分析清單報表 = 43,
            查核輔導專區_G交叉分析報表_歷年度查核次數與缺失分布 = 44,
            加油站_A統計報表專區_申請設置案件同意認定_籌建到期報表 = 45,
            加油站_A異常報表_營運主體分類異常之清單 = 46,
            加油站_A異常報表_系統最後發文與營運狀況不符合之清單 = 47,
            查核輔導專區_G交叉分析報表_特定查核項目歷年度缺失改善變化圖表 = 48,
            加油站_A異常報表_查核系統與油氣設施子系統連結異常之清單 = 49,
            查核輔導專區_G交叉分析報表_歷年加油站各檢查項目缺失統計 = 50,
            查核輔導專區_G交叉分析報表_歷年各縣市石油設施檢查缺失統計 = 51,
            查核輔導專區_G交叉分析報表_歷年各集團加油站檢查缺失統計 = 52,
            查核輔導專區_G交叉分析報表_歷年度查核零缺失比例統計 = 53,
            自用加儲油站_D統計報表專區_使用狀況統計報表 = 54,
            查核輔導專區_G交叉分析報表_年度及查核缺失項目查詢符合該條件之油氣設施名單 = 55,
            查核輔導專區_G交叉分析報表_依加油站名稱及查核缺失項目查詢歷年該缺失查核結果 = 56,
            查核輔導專區_G交叉分析報表_依各縣市篩選檢查發現缺失項目及統計報表 = 57,
            銷售分析表產出 = 58,
            地方政府查核結果填報 = 59,
            範本_日_自行安全檢查表_113年系統 = 60,
        }

        public enum UploadFile
        {
        }

        /// <summary>
        /// 取得油品供應商名稱
        /// </summary>
        /// <param name="no"></param>
        /// <returns></returns>
        public static string GetSoilServerName(string no)
        {
            ////if (string.IsNullOrEmpty(no)) 
            ////    return "";

            string result = "";
            if (no.Trim() == "1")
            {
                result = "台灣中油";
            }
            else if (no.Trim() == "2")
            {
                result = "台塑石化";
            }
            else
            {
                result = "其他";
            }

            return result;
        }

        /// <summary>
        /// 天氣
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<KeyValuePair<string, object>> GetWeather()
        {
            IEnumerable<KeyValuePair<string, object>> result = new List<KeyValuePair<string, object>>();

            result = result.Append(new KeyValuePair<string, object>("1", "晴天"));
            result = result.Append(new KeyValuePair<string, object>("2", "陰天"));
            result = result.Append(new KeyValuePair<string, object>("3", "雨天"));
            result = result.Append(new KeyValuePair<string, object>("4", "大雷雨"));

            return result;
        }

        /// <summary>
        /// 檢查方法
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<KeyValuePair<string, object>> GetCheckWay()
        {
            IEnumerable<KeyValuePair<string, object>> result = new List<KeyValuePair<string, object>>();

            result = result.Append(new KeyValuePair<string, object>("1", "目視"));
            result = result.Append(new KeyValuePair<string, object>("2", "目視手動"));
            result = result.Append(new KeyValuePair<string, object>("3", "目視耳聞"));
            result = result.Append(new KeyValuePair<string, object>("4", "實地操作"));
            result = result.Append(new KeyValuePair<string, object>("5", "手動檢查"));
            result = result.Append(new KeyValuePair<string, object>("6", "實地測試"));

            return result;
        }

        /// <summary>
        /// 年度
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<KeyValuePair<string, object>> GetYear()
        {
            IEnumerable<KeyValuePair<string, object>> result = new List<KeyValuePair<string, object>>();
            for (int i = 95; i <= DateTime.Now.Year - 1911; i++)
            {
                result = result.Append(new KeyValuePair<string, object>(i.ToString(), i.ToString()));
            }

            return result;
        }

        /// <summary>
        /// 查核年度
        /// </summary>
        /// <param name="startYear">開始年度</param>
        /// <returns></returns>
        public static IEnumerable<KeyValuePair<string, object>> GetCheckYear(int startYear)
        {
            IEnumerable<KeyValuePair<string, object>> result = new List<KeyValuePair<string, object>>();
            for (int i = startYear; i <= DateTime.Now.Year - 1911; i++)
            {
                result = result.Append(new KeyValuePair<string, object>(i.ToString(), i.ToString() + "年度加油站查核專案"));
            }

            return result;
        }

        /// <summary>
        /// 查核年度
        /// </summary>
        /// <param name="startYear">開始年度</param>
        /// <returns></returns>
        public static IEnumerable<KeyValuePair<string, object>> GetCheckYear2(int startYear)
        {
            IEnumerable<KeyValuePair<string, object>> result = new List<KeyValuePair<string, object>>();
            for (int i = startYear; i <= DateTime.Now.Year - 1911; i++)
            {
                result = result.Append(new KeyValuePair<string, object>(i.ToString(), i.ToString()));
            }

            return result;
        }

        /// <summary>
        /// 石油設施查核名單篩選 查核年度
        /// </summary>        
        /// <returns></returns>
        public static IEnumerable<KeyValuePair<string, object>> GetEndCheckYaer()
        {
            Dou.Models.DB.IModelEntity<CheckCaseList> model = new Dou.Models.DB.ModelEntity<CheckCaseList>(new OilGasModelContextExt());
            var tmp = model.GetAll()                
                .Select(s => new { s.End_CheckYaer })
                .Distinct()
                .ToList();

            var years = tmp.Where(a => int.Parse(a.End_CheckYaer) >= DateTime.Now.Year);

            return years.Select(s => new KeyValuePair<string, object>(s.End_CheckYaer.ToString(), s.End_CheckYaer));            
        }

        /// <summary>
        /// 分級管理及重要因子交叉分析清單報表 預計查核年度
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<KeyValuePair<string, object>> GetImportantCheckYaer()
        {
            IEnumerable<KeyValuePair<string, object>> result = new List<KeyValuePair<string, object>>();
            for (int i = 98; i <= DateTime.Now.Year - 1911 + 5; i++)                
            {
                result = result.Append(new KeyValuePair<string, object>(i.ToString(), i.ToString()));
            }

            return result;
        }

        /// <summary>
        /// 台灣5都縣市 轉換(台中市:B=>B,L 或 L=>B,L)
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public static List<string> ConvertTwCity(List<string> list)
        {
            //(CheckNo,1),'L','B'),'S','E'),'R','D')
            List<string> a1 = new List<string>() { "B", "L" };
            List<string> a2 = new List<string>() { "S", "E" };
            List<string> a3 = new List<string>() { "R", "D" };

            if (list.Any(a => a1.Contains(a)))
            {
                list.Remove("B");
                list.Remove("L");
                list.AddRange(a1);
            }

            if (list.Any(a => a2.Contains(a)))
            {
                list.Remove("S");
                list.Remove("E");
                list.AddRange(a2);
            }

            if (list.Any(a => a3.Contains(a)))
            {
                list.Remove("R");
                list.Remove("D");
                list.AddRange(a3);
            }

            return list;
        }

        /// <summary>
        /// 取得查核結果格式1 - YN
        /// </summary>
        /// <param name="str">Y/N</param>
        /// <param name="def">回傳預設值</param>
        /// <returns></returns>
        public static string GetCheckF1(string str, string def = "")
        {
            string result = def;

            if (str == "Y")
            {
                result = "☑";
            }
            else if (str == "N")
            {
                result = "☒";
            }

            return result;
        }

        /// <summary>
        /// 查核輔導專區_儲槽總數差異勾稽報表 --儲油槽
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<KeyValuePair<string, object>> GetCaseType()
        {
            IEnumerable<KeyValuePair<string, object>> result = new List<KeyValuePair<string, object>>();
            result = result.Append(new KeyValuePair<string, object>("CarFuel_BasicData", "汽/機車加油站"));
            result = result.Append(new KeyValuePair<string, object>("FishGas_BasicData", "漁船加油站"));
            result = result.Append(new KeyValuePair<string, object>("SelfFuel_Basic", "自用加儲油"));

            return result;
        }

        /// <summary>
        /// 查核輔導專區_石油設施缺失項目彙整表 --石油設施類型
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<KeyValuePair<string, object>> GetCaseType2()
        {
            IEnumerable<KeyValuePair<string, object>> result = new List<KeyValuePair<string, object>>();
            result = result.Append(new KeyValuePair<string, object>("CarFuel_BasicData", JsonConvert.SerializeObject(new { v = "汽/機車加油站", CheckItemTable = "Check_Item" })));
            result = result.Append(new KeyValuePair<string, object>("FishGas_BasicData", JsonConvert.SerializeObject(new { v = "漁船加油站", CheckItemTable = "Check_Item_Fish" })));
            result = result.Append(new KeyValuePair<string, object>("SelfFuel_Basic_Up", JsonConvert.SerializeObject(new { v = "自用加儲油設施(地上)", CheckItemTable = "Check_Item_SelfUP" })));
            result = result.Append(new KeyValuePair<string, object>("SelfFuel_Basic_Down", JsonConvert.SerializeObject(new { v = "自用加儲油設施(地下)", CheckItemTable = "Check_Item_SelfDown" })));

            return result;
        }

        public static IEnumerable<KeyValuePair<string, object>> GetCaseType3()
        {
            IEnumerable<KeyValuePair<string, object>> result = new List<KeyValuePair<string, object>>();
            result = result.Append(new KeyValuePair<string, object>("CarFuel_BasicData", "Check_Item"));
            result = result.Append(new KeyValuePair<string, object>("FishGas_BasicData", "Check_Item_Fish"));            

            return result;
        }

        public static IEnumerable<KeyValuePair<string, object>> GetCaseType4()
        {
            IEnumerable<KeyValuePair<string, object>> result = new List<KeyValuePair<string, object>>();
            result = result.Append(new KeyValuePair<string, object>("CarFuel_BasicData", JsonConvert.SerializeObject(new { v = "汽/機車加油站", CheckItemTable = "Check_Item" })));
            result = result.Append(new KeyValuePair<string, object>("FishGas_BasicData", JsonConvert.SerializeObject(new { v = "漁船加油站", CheckItemTable = "Check_Item_Fish" })));
            result = result.Append(new KeyValuePair<string, object>("SelfFuel_Basic", JsonConvert.SerializeObject(new { v = "自用加儲油設施(地上)", CheckItemTable = "Check_Item_SelfUP" })));
            result = result.Append(new KeyValuePair<string, object>("SelfFuel_Basic", JsonConvert.SerializeObject(new { v = "自用加儲油設施(地下)", CheckItemTable = "Check_Item_SelfDown" })));

            return result;
        }

        /// <summary>
        /// 取得營業主體Group
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<KeyValuePair<int, string>> GetBusinessOrganizationGroup()
        {
            IEnumerable<KeyValuePair<int, string>> result = new List<KeyValuePair<int, string>>();
            result = result.Append(new KeyValuePair<int, string>(1, "集團"));
            result = result.Append(new KeyValuePair<int, string>(2, "非集團"));

            return result;
        }

        //查核輔導專區_查核工作表
        public static string GetWorkTable(string workCaseType, int workYear)
        {
            string workTable = "Check_Item";

            if (workCaseType == "Check_Item")
            {
                //帶出汽機車加油站查核輔導的年度
                if (workYear <= 97)
                {
                    workTable = "Check_Item_97";
                }
                else
                {
                    workTable = "Check_Item";
                }

            }
            else if (workCaseType == "Check_Item_Fish")
            {
                //帶出漁船加油站查核輔導的年度
                if (workYear <= 103)
                {
                    workTable = "Check_Item_Fish103";
                }
                else
                {
                    workTable = "Check_Item_Fish";
                }
            }
            else if (workCaseType == "Check_Item_SelfUP")
            {
                workTable = "Check_Item_SelfUP";
            }
            else if (workCaseType == "Check_Item_SelfDown")
            {
                workTable = "Check_Item_SelfDown";
            }

            return workTable;
        }

        /// <summary>
        /// 查核輔導專區_G交叉分析報表_石油設施查核名單篩選 --高風險因子篩選項目
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<KeyValuePair<string, object>> GetPMListCheckTypeT()
        {
            IEnumerable<KeyValuePair<string, object>> result = new List<KeyValuePair<string, object>>();
            result = result.Append(new KeyValuePair<string, object>("1", "無非防爆性電氣設備或器具置於第一種場所或第二種場所範圍內使用"));
            result = result.Append(new KeyValuePair<string, object>("2", "陰井之油氣濃度未逾25%LEL"));
            result = result.Append(new KeyValuePair<string, object>("3", "陰井之油氣濃度有測值"));
            result = result.Append(new KeyValuePair<string, object>("4", "加油機內無積水"));
            result = result.Append(new KeyValuePair<string, object>("5", "無積油及無油氣，設備接頭無滲漏、加油機及電氣接線孔配電管接頭、防爆軟管接頭無鬆脫，預留管口密封"));
            result = result.Append(new KeyValuePair<string, object>("6", "電氣設備漏電斷路器試鈕作用正常"));
            result = result.Append(new KeyValuePair<string, object>("7", "洗車機漏電器測試正常"));
            result = result.Append(new KeyValuePair<string, object>("8", "洗車機緊急停止裝置測試正常"));

            return result;
        }

        /// <summary>
        /// 查核輔導專區_G交叉分析報表_石油設施查核名單篩選 --新設、變更營業主體及復業
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<KeyValuePair<string, object>> GetPMListCheckTypeN()
        {
            IEnumerable<KeyValuePair<string, object>> result = new List<KeyValuePair<string, object>>();
            result = result.Append(new KeyValuePair<string, object>("1", "上一年度新設之加油站"));
            result = result.Append(new KeyValuePair<string, object>("2", "上一年度變更營業主體之加油站"));
            result = result.Append(new KeyValuePair<string, object>("3", "上一年度復業之加油站"));

            return result;
        }

        /// <summary>
        /// 查核輔導專區_G交叉分析報表_分級管理及重要因子交叉分析清單報表 --重要因子
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<KeyValuePair<string, object>> GetImportantFactor()
        {
            IEnumerable<KeyValuePair<string, object>> result = new List<KeyValuePair<string, object>>();
            result = result.Append(new KeyValuePair<string, object>("1", "2-4：無非防爆性電氣設備或器具置於第一種場所或第二種場所範圍內使用"));
            result = result.Append(new KeyValuePair<string, object>("2", "3-12：陰井之油氣濃度未逾25%LEL"));
            result = result.Append(new KeyValuePair<string, object>("3", "4-7：加油機內無積水、無積油及無油氣，設備接頭無滲漏"));
            result = result.Append(new KeyValuePair<string, object>("4", "4-10：加油機及電氣接線孔配電管接頭、防爆軟管接頭無鬆脫，預留管口密封"));
            result = result.Append(new KeyValuePair<string, object>("5", "陰井油氣檢測有數值"));

            return result;
        }

        /// <summary>子系統類別</summary>
        public enum SubSystemType
        {
            /// <summary>汽、機車加油站</summary>
            CarFuel,
            /// <summary>汽車加氣站</summary>
            CarGas,
            /// <summary>漁船加油站</summary>
            FishGas
        }

        #region getBusOrgList [取得 營業主體 項目]
        /// <summary>取得 營業主體 項目</summary>
        /// <returns></returns>
        public static List<StatisticReportFunc.NameItem> getBusOrgList()
        {
            System.Data.Entity.DbContext dbContext = new OilGasModelContextExt();
            List<CarVehicleGas_BusinessOrganization> citys = new List<CarVehicleGas_BusinessOrganization>();
            Dou.Models.DB.IModelEntity<CarVehicleGas_BusinessOrganization> BusOrg = new Dou.Models.DB.ModelEntity<CarVehicleGas_BusinessOrganization>(dbContext);
            List<StatisticReportFunc.NameItem> BusOrgList = new List<StatisticReportFunc.NameItem>();
            foreach (var item in BusOrg.GetAll().OrderBy(x => x.Rank).ToList())
            {
                BusOrgList.Add(new StatisticReportFunc.NameItem(item.ShortName, item.Value));
            }
            return BusOrgList;
        }
        #endregion
    }
}