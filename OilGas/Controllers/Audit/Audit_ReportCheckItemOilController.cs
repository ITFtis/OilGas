using Dou.Controllers;
using Dou.Misc;
using Dou.Misc.Attr;
using Dou.Models.DB;
using NPOI.XWPF.UserModel;
using OilGas.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static OilGas.Controllers.Audit.Audit_ReportCheck_counts_CrossAnalysisController;
using NPOI.OpenXmlFormats.Dml.Diagram;
using System.Data.Entity;

namespace OilGas.Controllers.Audit
{
    [Dou.Misc.Attr.MenuDef(Id = "Audit_ReportCheckItemOil", Name = "依加油站名稱及查核缺失項目查詢歷年該缺失查核結果", MenuPath = "查核輔導專區/G交叉分析報表", Action = "Index", Index = 12, Func = Dou.Misc.Attr.FuncEnum.None, AllowAnonymous = false)]
    public class Audit_ReportCheckItemOilController : APaginationModelController<vw_Audit_ReportCheckItemOil>
    {
        // GET: Audit_ReportCheckItemOil
        public ActionResult Index()
        {
            return View();
        }

        protected override Dou.Models.DB.IModelEntity<vw_Audit_ReportCheckItemOil> GetModelEntity()
        {
            return new Dou.Models.DB.ModelEntity<vw_Audit_ReportCheckItemOil>(new OilGasModelContextExt());
        }

        protected override IQueryable<vw_Audit_ReportCheckItemOil> BeforeIQueryToPagedList(IQueryable<vw_Audit_ReportCheckItemOil> iquery, params KeyValueParams[] paras)
        {
            //進入頁面不顯示清單(未使用查詢)
            KeyValueParams filter = paras.FirstOrDefault((KeyValueParams s) => s.key == "filter");
            if (filter == null)
            {
                return new List<vw_Audit_ReportCheckItemOil>().AsQueryable();
            }

            List<string> titles = new List<string>();
            var result = GetOutputData(ref titles, paras);

            //可能是查無對應CheckItemTitelSum (ex.A_Doesmeet)
            if (result == null)
            {
                return new List<vw_Audit_ReportCheckItemOil>().AsQueryable();
            }

            if (1 == 2) 
            {

            }
            else
            {
                //預設排序                
                result = result.OrderByDescending(a => a.CheckYear)
                                    .ThenBy(a => a.CityName)
                                    .ThenByDescending(a => a.CheckNo);
            }

            return base.BeforeIQueryToPagedList(result, paras);            
        }

        private IQueryable<vw_Audit_ReportCheckItemOil> GetOutputData(ref List<string> titles, params KeyValueParams[] paras)
        {
            System.Data.Entity.DbContext dbContext = new OilGasModelContextExt();
            
            Dou.Models.DB.IModelEntity<Check_Basic_View> check_Basic_View = new Dou.Models.DB.ModelEntity<Check_Basic_View>(dbContext);
            Dou.Models.DB.IModelEntity<Check_Item> check_Item = new Dou.Models.DB.ModelEntity<Check_Item>(dbContext);

            var CheckYear = Dou.Misc.HelperUtilities.GetFilterParaValue(paras, "CheckYear");
            var CityCode1 = Dou.Misc.HelperUtilities.GetFilterParaValue(paras, "CityCode1");            
            var Gas_Name = Dou.Misc.HelperUtilities.GetFilterParaValue(paras, "Gas_Name");
            var CheckItemTitel = Dou.Misc.HelperUtilities.GetFilterParaValue(paras, "CheckItemTitel");
            var CheckItemDescNo = Dou.Misc.HelperUtilities.GetFilterParaValue(paras, "CheckItemDescNo");

            var iquery = check_Basic_View.GetAll().Join(check_Item.GetAll(), a => a.CheckNo, b => b.CheckNo, (o, c) => new
            {
                o.CheckNo,
                CheckYear = o.CheckDate.Year - 1911,
                o.CityName, o.Gas_Name
            });

            //權限查詢
            var pCitys = Dou.Context.CurrentUser<User>().PowerCitysNames();            
            iquery = iquery.Where(a => pCitys.Any(b => a.CityName == b)); ;

            //條件
            if (!string.IsNullOrEmpty(CheckYear))
            {
                int num = int.Parse(CheckYear);
                iquery = iquery.Where(a => a.CheckYear == num);

                titles.Add("查核年度:" + num.ToString());
            }

            if (!string.IsNullOrEmpty(CityCode1))
            {
                var codes = CityCode1.Split(',').ToList();
                codes = Code.ConvertTwCity(codes);

                Dou.Models.DB.IModelEntity<CityCode> cityCode = new Dou.Models.DB.ModelEntity<CityCode>(dbContext);
                var citys = cityCode.GetAll().Where(a => codes.Any(b => b == a.CityCode1));

                iquery = iquery.Where(a => citys.Any(b => a.CityName == b.CityName));

                //代碼-縣市別                                
                citys = citys.OrderBy(a => a.Rank);
                titles.Add("縣市:" + string.Join(",", citys.Select(a => a.CityName).ToList()));
            }
            
            if (!string.IsNullOrEmpty(Gas_Name))
            {
                string str = Gas_Name;
                iquery = iquery.Where(a => a.Gas_Name.Contains(str));

                titles.Add("加油站名稱:" + str);
            }

            if (!string.IsNullOrEmpty(CheckItemTitel))
            {
                string str = CheckItemTitel;
                
                //代碼-查核項目
                var codes = CheckItemListSelectItemsClassImp.Checks.Where(a => a.CheckItemTitelSum == str);
                titles.Add("查核項目:" + string.Join(",", codes.Select(a => a.CheckItemTitel).Distinct()));
            }

            if (!string.IsNullOrEmpty(CheckItemDescNo))
            {
                string str = CheckItemDescNo;

                //代碼-查核細項
                var codes = CheckItemListSelectItemsClassImp.Checks.Where(a => a.CheckItemDescNo == str);
                titles.Add("查核細項:" + string.Join(",", codes.Select(a => a.CheckItemDesc).Distinct()));
            }

            var tmp = iquery.AsEnumerable().Select(a => new 
            {
                a.CheckNo, a.CheckYear, a.CityName,
            });

            //查核細項結果
            var details = check_Item.GetAll().Where(a => iquery.Any(b => b.CheckNo == a.CheckNo)).ToList();
                        
            IEnumerable<KeyValuePair<string, string>> names = new List<KeyValuePair<string, string>>();
            names = names.Append(new KeyValuePair<string, string>("0", "未設置"));
            names = names.Append(new KeyValuePair<string, string>("1", "良好"));
            names = names.Append(new KeyValuePair<string, string>("2", "不良好"));
            names = names.Append(new KeyValuePair<string, string>("3", "無法檢查"));

            IEnumerable<vw_Audit_ReportCheckItemOil> result = null;

            #region  動態欄位(查核狀態)

            if (CheckItemTitel == "A_Doesmeet")
            { 
                result = tmp.GroupJoin(details, a => a.CheckNo, b => b.CheckNo, (o, c) => new vw_Audit_ReportCheckItemOil
                {
                    CheckNo = o.CheckNo, CheckYear = o.CheckYear, CityName = o.CityName,
                    Ch1 = (!string.IsNullOrEmpty(CheckItemDescNo) && CheckItemDescNo != "A01") ? null : names.Where(a => a.Key == c.FirstOrDefault().A01).FirstOrDefault().Value,
                    Ch2 = (!string.IsNullOrEmpty(CheckItemDescNo) && CheckItemDescNo != "A02") ? null : names.Where(a => a.Key == c.FirstOrDefault().A02).FirstOrDefault().Value,
                    Ch3 = (!string.IsNullOrEmpty(CheckItemDescNo) && CheckItemDescNo != "A03") ? null : names.Where(a => a.Key == c.FirstOrDefault().A03).FirstOrDefault().Value,
                    Ch4 = (!string.IsNullOrEmpty(CheckItemDescNo) && CheckItemDescNo != "A04") ? null : names.Where(a => a.Key == c.FirstOrDefault().A04).FirstOrDefault().Value,
                    Ch5 = (!string.IsNullOrEmpty(CheckItemDescNo) && CheckItemDescNo != "A05") ? null : names.Where(a => a.Key == c.FirstOrDefault().A05).FirstOrDefault().Value,
                    Ch6 = (!string.IsNullOrEmpty(CheckItemDescNo) && CheckItemDescNo != "A06") ? null : names.Where(a => a.Key == c.FirstOrDefault().A06).FirstOrDefault().Value,
                });
            }
            else if (CheckItemTitel == "B_Doesmeet")
            { 
                result = tmp.GroupJoin(details, a => a.CheckNo, b => b.CheckNo, (o, c) => new vw_Audit_ReportCheckItemOil
                {
                    CheckNo = o.CheckNo, CheckYear = o.CheckYear, CityName = o.CityName,
                    Ch1 = (!string.IsNullOrEmpty(CheckItemDescNo) && CheckItemDescNo != "B01") ? null : names.Where(a => a.Key == c.FirstOrDefault().B01).FirstOrDefault().Value,
                    Ch2 = (!string.IsNullOrEmpty(CheckItemDescNo) && CheckItemDescNo != "B02") ? null : names.Where(a => a.Key == c.FirstOrDefault().B02).FirstOrDefault().Value,
                    Ch3 = (!string.IsNullOrEmpty(CheckItemDescNo) && CheckItemDescNo != "B03") ? null : names.Where(a => a.Key == c.FirstOrDefault().B03).FirstOrDefault().Value,
                    Ch4 = (!string.IsNullOrEmpty(CheckItemDescNo) && CheckItemDescNo != "B04") ? null : names.Where(a => a.Key == c.FirstOrDefault().B04).FirstOrDefault().Value,
                    Ch5 = (!string.IsNullOrEmpty(CheckItemDescNo) && CheckItemDescNo != "B05") ? null : names.Where(a => a.Key == c.FirstOrDefault().B05).FirstOrDefault().Value,
                    Ch6 = (!string.IsNullOrEmpty(CheckItemDescNo) && CheckItemDescNo != "B06") ? null : names.Where(a => a.Key == c.FirstOrDefault().B06).FirstOrDefault().Value,
                    Ch7 = (!string.IsNullOrEmpty(CheckItemDescNo) && CheckItemDescNo != "B07") ? null : names.Where(a => a.Key == c.FirstOrDefault().B07).FirstOrDefault().Value,
                    Ch8 = (!string.IsNullOrEmpty(CheckItemDescNo) && CheckItemDescNo != "B08") ? null : names.Where(a => a.Key == c.FirstOrDefault().B08).FirstOrDefault().Value,
                    Ch9 = (!string.IsNullOrEmpty(CheckItemDescNo) && CheckItemDescNo != "B09") ? null : names.Where(a => a.Key == c.FirstOrDefault().B09).FirstOrDefault().Value,
                    Ch10 = (!string.IsNullOrEmpty(CheckItemDescNo) && CheckItemDescNo != "B10") ? null : names.Where(a => a.Key == c.FirstOrDefault().B10).FirstOrDefault().Value,                    
                });
            }
            else if (CheckItemTitel == "C_Doesmeet")
            { 
                result = tmp.GroupJoin(details, a => a.CheckNo, b => b.CheckNo, (o, c) => new vw_Audit_ReportCheckItemOil
                {
                    CheckNo = o.CheckNo, CheckYear = o.CheckYear, CityName = o.CityName,
                    Ch1 = (!string.IsNullOrEmpty(CheckItemDescNo) && CheckItemDescNo != "C01") ? null : names.Where(a => a.Key == c.FirstOrDefault().C01).FirstOrDefault().Value,
                    Ch2 = (!string.IsNullOrEmpty(CheckItemDescNo) && CheckItemDescNo != "C02") ? null : names.Where(a => a.Key == c.FirstOrDefault().C02).FirstOrDefault().Value,
                    Ch3 = (!string.IsNullOrEmpty(CheckItemDescNo) && CheckItemDescNo != "C03") ? null : names.Where(a => a.Key == c.FirstOrDefault().C03).FirstOrDefault().Value,
                    Ch4 = (!string.IsNullOrEmpty(CheckItemDescNo) && CheckItemDescNo != "C04") ? null : names.Where(a => a.Key == c.FirstOrDefault().C04).FirstOrDefault().Value,
                    Ch5 = (!string.IsNullOrEmpty(CheckItemDescNo) && CheckItemDescNo != "C05") ? null : names.Where(a => a.Key == c.FirstOrDefault().C05).FirstOrDefault().Value,
                    Ch6 = (!string.IsNullOrEmpty(CheckItemDescNo) && CheckItemDescNo != "C06") ? null : names.Where(a => a.Key == c.FirstOrDefault().C06).FirstOrDefault().Value,
                    Ch7 = (!string.IsNullOrEmpty(CheckItemDescNo) && CheckItemDescNo != "C07") ? null : names.Where(a => a.Key == c.FirstOrDefault().C07).FirstOrDefault().Value,
                    Ch8 = (!string.IsNullOrEmpty(CheckItemDescNo) && CheckItemDescNo != "C08") ? null : names.Where(a => a.Key == c.FirstOrDefault().C08).FirstOrDefault().Value,
                    Ch9 = (!string.IsNullOrEmpty(CheckItemDescNo) && CheckItemDescNo != "C09") ? null : names.Where(a => a.Key == c.FirstOrDefault().C09).FirstOrDefault().Value,
                    Ch10 = (!string.IsNullOrEmpty(CheckItemDescNo) && CheckItemDescNo != "C10") ? null : names.Where(a => a.Key == c.FirstOrDefault().C10).FirstOrDefault().Value,
                    Ch11 = (!string.IsNullOrEmpty(CheckItemDescNo) && CheckItemDescNo != "C11") ? null : names.Where(a => a.Key == c.FirstOrDefault().C11).FirstOrDefault().Value,
                    Ch12 = (!string.IsNullOrEmpty(CheckItemDescNo) && CheckItemDescNo != "C12") ? null : names.Where(a => a.Key == c.FirstOrDefault().C12).FirstOrDefault().Value,
                    Ch13 = (!string.IsNullOrEmpty(CheckItemDescNo) && CheckItemDescNo != "C13") ? null : names.Where(a => a.Key == c.FirstOrDefault().C13).FirstOrDefault().Value,
                    Ch14 = (!string.IsNullOrEmpty(CheckItemDescNo) && CheckItemDescNo != "C14") ? null : names.Where(a => a.Key == c.FirstOrDefault().C14).FirstOrDefault().Value,
                });
            }
            else if (CheckItemTitel == "D_Doesmeet")
            { 
                result = tmp.GroupJoin(details, a => a.CheckNo, b => b.CheckNo, (o, c) => new vw_Audit_ReportCheckItemOil
                {
                    CheckNo = o.CheckNo, CheckYear = o.CheckYear, CityName = o.CityName,
                    Ch1 = (!string.IsNullOrEmpty(CheckItemDescNo) && CheckItemDescNo != "D01") ? null : names.Where(a => a.Key == c.FirstOrDefault().D01).FirstOrDefault().Value,
                    Ch2 = (!string.IsNullOrEmpty(CheckItemDescNo) && CheckItemDescNo != "D02") ? null : names.Where(a => a.Key == c.FirstOrDefault().D02).FirstOrDefault().Value,
                    Ch3 = (!string.IsNullOrEmpty(CheckItemDescNo) && CheckItemDescNo != "D03") ? null : names.Where(a => a.Key == c.FirstOrDefault().D03).FirstOrDefault().Value,
                    Ch4 = (!string.IsNullOrEmpty(CheckItemDescNo) && CheckItemDescNo != "D04") ? null : names.Where(a => a.Key == c.FirstOrDefault().D04).FirstOrDefault().Value,
                    Ch5 = (!string.IsNullOrEmpty(CheckItemDescNo) && CheckItemDescNo != "D05") ? null : names.Where(a => a.Key == c.FirstOrDefault().D05).FirstOrDefault().Value,
                    Ch6 = (!string.IsNullOrEmpty(CheckItemDescNo) && CheckItemDescNo != "D06") ? null : names.Where(a => a.Key == c.FirstOrDefault().D06).FirstOrDefault().Value,
                    Ch7 = (!string.IsNullOrEmpty(CheckItemDescNo) && CheckItemDescNo != "D07") ? null : names.Where(a => a.Key == c.FirstOrDefault().D07).FirstOrDefault().Value,
                    Ch8 = (!string.IsNullOrEmpty(CheckItemDescNo) && CheckItemDescNo != "D08") ? null : names.Where(a => a.Key == c.FirstOrDefault().D08).FirstOrDefault().Value,
                    Ch9 = (!string.IsNullOrEmpty(CheckItemDescNo) && CheckItemDescNo != "D09") ? null : names.Where(a => a.Key == c.FirstOrDefault().D09).FirstOrDefault().Value,
                    Ch10 = (!string.IsNullOrEmpty(CheckItemDescNo) && CheckItemDescNo != "D10") ? null : names.Where(a => a.Key == c.FirstOrDefault().D10).FirstOrDefault().Value,
                    Ch11 = (!string.IsNullOrEmpty(CheckItemDescNo) && CheckItemDescNo != "D11") ? null : names.Where(a => a.Key == c.FirstOrDefault().D11).FirstOrDefault().Value,                    
                });
            }
            else if (CheckItemTitel == "E_Doesmeet")
            { 
                result = tmp.GroupJoin(details, a => a.CheckNo, b => b.CheckNo, (o, c) => new vw_Audit_ReportCheckItemOil
                {
                    CheckNo = o.CheckNo, CheckYear = o.CheckYear, CityName = o.CityName,
                    Ch1 = (!string.IsNullOrEmpty(CheckItemDescNo) && CheckItemDescNo != "E01") ? null : names.Where(a => a.Key == c.FirstOrDefault().E01).FirstOrDefault().Value,
                    Ch2 = (!string.IsNullOrEmpty(CheckItemDescNo) && CheckItemDescNo != "E02") ? null : names.Where(a => a.Key == c.FirstOrDefault().E02).FirstOrDefault().Value,
                    Ch3 = (!string.IsNullOrEmpty(CheckItemDescNo) && CheckItemDescNo != "E03") ? null : names.Where(a => a.Key == c.FirstOrDefault().E03).FirstOrDefault().Value,                    
                });
            }
            else if (CheckItemTitel == "F_Doesmeet")
            { 
                result = tmp.GroupJoin(details, a => a.CheckNo, b => b.CheckNo, (o, c) => new vw_Audit_ReportCheckItemOil
                {
                    CheckNo = o.CheckNo, CheckYear = o.CheckYear, CityName = o.CityName,
                    Ch1 = (!string.IsNullOrEmpty(CheckItemDescNo) && CheckItemDescNo != "F01") ? null : names.Where(a => a.Key == c.FirstOrDefault().F01).FirstOrDefault().Value,
                    Ch2 = (!string.IsNullOrEmpty(CheckItemDescNo) && CheckItemDescNo != "F02") ? null : names.Where(a => a.Key == c.FirstOrDefault().F02).FirstOrDefault().Value,
                    Ch3 = (!string.IsNullOrEmpty(CheckItemDescNo) && CheckItemDescNo != "F03") ? null : names.Where(a => a.Key == c.FirstOrDefault().F03).FirstOrDefault().Value,
                    Ch4 = (!string.IsNullOrEmpty(CheckItemDescNo) && CheckItemDescNo != "F04") ? null : names.Where(a => a.Key == c.FirstOrDefault().F04).FirstOrDefault().Value,
                    Ch5 = (!string.IsNullOrEmpty(CheckItemDescNo) && CheckItemDescNo != "F05") ? null : names.Where(a => a.Key == c.FirstOrDefault().F05).FirstOrDefault().Value,
                    Ch6 = (!string.IsNullOrEmpty(CheckItemDescNo) && CheckItemDescNo != "F06") ? null : names.Where(a => a.Key == c.FirstOrDefault().F06).FirstOrDefault().Value,
                    Ch7 = (!string.IsNullOrEmpty(CheckItemDescNo) && CheckItemDescNo != "F07") ? null : names.Where(a => a.Key == c.FirstOrDefault().F07).FirstOrDefault().Value,
                    Ch8 = (!string.IsNullOrEmpty(CheckItemDescNo) && CheckItemDescNo != "F08") ? null : names.Where(a => a.Key == c.FirstOrDefault().F08).FirstOrDefault().Value,
                    Ch9 = (!string.IsNullOrEmpty(CheckItemDescNo) && CheckItemDescNo != "F09") ? null : names.Where(a => a.Key == c.FirstOrDefault().F09).FirstOrDefault().Value,                    
                });
            }
            else if (CheckItemTitel == "G_Doesmeet")
            { 
                result = tmp.GroupJoin(details, a => a.CheckNo, b => b.CheckNo, (o, c) => new vw_Audit_ReportCheckItemOil
                {
                    CheckNo = o.CheckNo, CheckYear = o.CheckYear, CityName = o.CityName,
                    Ch1 = (!string.IsNullOrEmpty(CheckItemDescNo) && CheckItemDescNo != "G01") ? null : names.Where(a => a.Key == c.FirstOrDefault().G01).FirstOrDefault().Value,
                    Ch2 = (!string.IsNullOrEmpty(CheckItemDescNo) && CheckItemDescNo != "G02") ? null : names.Where(a => a.Key == c.FirstOrDefault().G02).FirstOrDefault().Value,
                    Ch3 = (!string.IsNullOrEmpty(CheckItemDescNo) && CheckItemDescNo != "G03") ? null : names.Where(a => a.Key == c.FirstOrDefault().G03).FirstOrDefault().Value,
                    Ch4 = (!string.IsNullOrEmpty(CheckItemDescNo) && CheckItemDescNo != "G04") ? null : names.Where(a => a.Key == c.FirstOrDefault().G04).FirstOrDefault().Value,
                    Ch5 = (!string.IsNullOrEmpty(CheckItemDescNo) && CheckItemDescNo != "G05") ? null : names.Where(a => a.Key == c.FirstOrDefault().G05).FirstOrDefault().Value,
                    Ch6 = (!string.IsNullOrEmpty(CheckItemDescNo) && CheckItemDescNo != "G06") ? null : names.Where(a => a.Key == c.FirstOrDefault().G06).FirstOrDefault().Value,                    
                });
            }
            else if (CheckItemTitel == "H_Doesmeet")
            { 
                result = tmp.GroupJoin(details, a => a.CheckNo, b => b.CheckNo, (o, c) => new vw_Audit_ReportCheckItemOil
                {
                    CheckNo = o.CheckNo, CheckYear = o.CheckYear, CityName = o.CityName,
                    Ch1 = (!string.IsNullOrEmpty(CheckItemDescNo) && CheckItemDescNo != "H01") ? null : names.Where(a => a.Key == c.FirstOrDefault().H01).FirstOrDefault().Value,
                    Ch2 = (!string.IsNullOrEmpty(CheckItemDescNo) && CheckItemDescNo != "H02") ? null : names.Where(a => a.Key == c.FirstOrDefault().H02).FirstOrDefault().Value,
                    Ch3 = (!string.IsNullOrEmpty(CheckItemDescNo) && CheckItemDescNo != "H03") ? null : names.Where(a => a.Key == c.FirstOrDefault().H03).FirstOrDefault().Value,
                    Ch4 = (!string.IsNullOrEmpty(CheckItemDescNo) && CheckItemDescNo != "H04") ? null : names.Where(a => a.Key == c.FirstOrDefault().H04).FirstOrDefault().Value,
                    Ch5 = (!string.IsNullOrEmpty(CheckItemDescNo) && CheckItemDescNo != "H05") ? null : names.Where(a => a.Key == c.FirstOrDefault().H05).FirstOrDefault().Value,                    
                });
            }
            else if (CheckItemTitel == "I_Doesmeet")
            { 
                result = tmp.GroupJoin(details, a => a.CheckNo, b => b.CheckNo, (o, c) => new vw_Audit_ReportCheckItemOil
                {
                    CheckNo = o.CheckNo, CheckYear = o.CheckYear, CityName = o.CityName,
                    Ch1 = (!string.IsNullOrEmpty(CheckItemDescNo) && CheckItemDescNo != "I01") ? null : names.Where(a => a.Key == c.FirstOrDefault().I01).FirstOrDefault().Value,
                    Ch2 = (!string.IsNullOrEmpty(CheckItemDescNo) && CheckItemDescNo != "I02") ? null : names.Where(a => a.Key == c.FirstOrDefault().I02).FirstOrDefault().Value,
                    Ch3 = (!string.IsNullOrEmpty(CheckItemDescNo) && CheckItemDescNo != "I03") ? null : names.Where(a => a.Key == c.FirstOrDefault().I03).FirstOrDefault().Value,
                    Ch4 = (!string.IsNullOrEmpty(CheckItemDescNo) && CheckItemDescNo != "I04") ? null : names.Where(a => a.Key == c.FirstOrDefault().I04).FirstOrDefault().Value,
                    Ch5 = (!string.IsNullOrEmpty(CheckItemDescNo) && CheckItemDescNo != "I05") ? null : names.Where(a => a.Key == c.FirstOrDefault().I05).FirstOrDefault().Value,
                    Ch6 = (!string.IsNullOrEmpty(CheckItemDescNo) && CheckItemDescNo != "I06") ? null : names.Where(a => a.Key == c.FirstOrDefault().I06).FirstOrDefault().Value,
                    Ch7 = (!string.IsNullOrEmpty(CheckItemDescNo) && CheckItemDescNo != "I07") ? null : names.Where(a => a.Key == c.FirstOrDefault().I07).FirstOrDefault().Value,
                    Ch8 = (!string.IsNullOrEmpty(CheckItemDescNo) && CheckItemDescNo != "I08") ? null : names.Where(a => a.Key == c.FirstOrDefault().I08).FirstOrDefault().Value,
                    Ch9 = (!string.IsNullOrEmpty(CheckItemDescNo) && CheckItemDescNo != "I09") ? null : names.Where(a => a.Key == c.FirstOrDefault().I09).FirstOrDefault().Value,
                    Ch10 = (!string.IsNullOrEmpty(CheckItemDescNo) && CheckItemDescNo != "I10") ? null : names.Where(a => a.Key == c.FirstOrDefault().I10).FirstOrDefault().Value,                    
                });
            }
            else if (CheckItemTitel == "J_Doesmeet")
            { 
                result = tmp.GroupJoin(details, a => a.CheckNo, b => b.CheckNo, (o, c) => new vw_Audit_ReportCheckItemOil
                {
                    CheckNo = o.CheckNo, CheckYear = o.CheckYear, CityName = o.CityName,
                    Ch1 = (!string.IsNullOrEmpty(CheckItemDescNo) && CheckItemDescNo != "J01") ? null : names.Where(a => a.Key == c.FirstOrDefault().J01).FirstOrDefault().Value,
                    Ch2 = (!string.IsNullOrEmpty(CheckItemDescNo) && CheckItemDescNo != "J02") ? null : names.Where(a => a.Key == c.FirstOrDefault().J02).FirstOrDefault().Value,
                    Ch3 = (!string.IsNullOrEmpty(CheckItemDescNo) && CheckItemDescNo != "J03") ? null : names.Where(a => a.Key == c.FirstOrDefault().J03).FirstOrDefault().Value,                    
                });
            }
            else if (CheckItemTitel == "K_Doesmeet")
            { 
                result = tmp.GroupJoin(details, a => a.CheckNo, b => b.CheckNo, (o, c) => new vw_Audit_ReportCheckItemOil
                {
                    CheckNo = o.CheckNo, CheckYear = o.CheckYear, CityName = o.CityName,
                    Ch1 = (!string.IsNullOrEmpty(CheckItemDescNo) && CheckItemDescNo != "K01") ? null : names.Where(a => a.Key == c.FirstOrDefault().K01).FirstOrDefault().Value,
                    Ch2 = (!string.IsNullOrEmpty(CheckItemDescNo) && CheckItemDescNo != "K02") ? null : names.Where(a => a.Key == c.FirstOrDefault().K02).FirstOrDefault().Value,                                        
                });
            }
            else if (CheckItemTitel == "L_Doesmeet")
            { 
                result = tmp.GroupJoin(details, a => a.CheckNo, b => b.CheckNo, (o, c) => new vw_Audit_ReportCheckItemOil
                {
                    CheckNo = o.CheckNo, CheckYear = o.CheckYear, CityName = o.CityName,
                    Ch1 = (!string.IsNullOrEmpty(CheckItemDescNo) && CheckItemDescNo != "L01") ? null : names.Where(a => a.Key == c.FirstOrDefault().L01).FirstOrDefault().Value,
                    Ch2 = (!string.IsNullOrEmpty(CheckItemDescNo) && CheckItemDescNo != "L02") ? null : names.Where(a => a.Key == c.FirstOrDefault().L02).FirstOrDefault().Value,
                    Ch3 = (!string.IsNullOrEmpty(CheckItemDescNo) && CheckItemDescNo != "L03") ? null : names.Where(a => a.Key == c.FirstOrDefault().L03).FirstOrDefault().Value,                    
                });
            }            

            #endregion

            return result.AsQueryable();
        }
    }

    public class vw_Audit_ReportCheckItemOil
    {
        [Key]
        [Display(Name = "查核編號", Order = 1)]        
        [System.ComponentModel.DataAnnotations.Schema.Column(Order = 1)]
        [ColumnDef(VisibleEdit = false)]
        public string CheckNo { get; set; }

        [Key]
        [Display(Name = "查核年度", Order = 2)]
        [System.ComponentModel.DataAnnotations.Schema.Column(Order = 2)]
        [ColumnDef(VisibleEdit = false, Filter = true)]
        public int CheckYear { get; set; }

        [Key]
        [Display(Name = "查核縣市", Order = 3)]
        [System.ComponentModel.DataAnnotations.Schema.Column(Order = 3)]
        [ColumnDef(Visible = false, VisibleEdit = false, EditType = EditType.Select,
                Filter = true, SelectItemsClassNamespace = UsercityCodeSelectItems.AssemblyQualifiedName)]
        public string CityCode1 { get; set; }

        [Display(Name = "縣市別")]
        public string CityName { get; set; }

        [Display(Name = "加油站名稱", Order = 4)]
        [ColumnDef(Visible = false, VisibleEdit = false,
                Filter = true, FilterAssign = FilterAssignType.Contains)]        
        public string Gas_Name { get; set; }

        [Display(Name = "查核項目", Order = 5)]
        [ColumnDef(Visible = false, VisibleEdit = false,
             Filter = true, EditType = EditType.Select,
             SelectGearingWith = "CheckItemDescNo,CheckItemTitel,true",
             SelectItemsClassNamespace = CheckItemListSelectItemsClassImp.AssemblyQualifiedName)]
        public string CheckItemTitel { get; }

        [Display(Name = "查核細項", Order = 9)]
        [ColumnDef(Visible = false, VisibleEdit = false,
             Filter = true, EditType = EditType.Select,
             SelectItemsClassNamespace = CheckItemListDetailSelectItemsClassImp.AssemblyQualifiedName)]
        public string CheckItemDescNo { get; }

        public string Ch1 { get; set; }
        public string Ch2 { get; set; }
        public string Ch3 { get; set; }
        public string Ch4 { get; set; }
        public string Ch5 { get; set; }
        public string Ch6 { get; set; }
        public string Ch7 { get; set; }
        public string Ch8 { get; set; }
        public string Ch9 { get; set; }
        public string Ch10 { get; set; }
        public string Ch11 { get; set; }
        public string Ch12 { get; set; }
        public string Ch13 { get; set; }
        public string Ch14 { get; set; }
        public string Ch15 { get; set; }
        public string Ch16 { get; set; }
    }
}